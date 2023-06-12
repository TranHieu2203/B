Imports System.Data.Objects
Imports System.IO
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.DataAccess
Imports Framework.Data.System.Linq.Dynamic
Imports Oracle.DataAccess.Client
Imports Aspose.Cells
Imports Ionic.Zip
Partial Class ProfileRepository

    ' Trạng thái chờ nghỉ việc
    Private Const WORK_STATUS_257 As Integer = 257

#Region "Employee"
    Public Function GetEmpInfomations(ByVal orgIDs As List(Of Decimal),
                                      ByVal _filter As EmployeeDTO,
                                      ByVal PageIndex As Integer,
                                      ByVal PageSize As Integer,
                                      ByRef Total As Integer,
                                      Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                      Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        Try
            Dim Employees = (From e In Context.HU_EMPLOYEE
                             From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                             From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                             Where orgIDs.Contains(e.ORG_ID) Order By e.EMPLOYEE_CODE
                             Select New EmployeeDTO With {
                                 .ID = e.ID,
                                 .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                 .FULLNAME_VN = e.FULLNAME_VN,
                                 .TITLE_ID = e.TITLE_ID,
                                 .TITLE_NAME_VN = t.NAME_VN,
                                 .BIRTH_DATE = cv.BIRTH_DATE,
                                 .MOBILE_PHONE = cv.MOBILE_PHONE,
                                 .IMAGE = cv.IMAGE,
                                 .WORK_STATUS = e.WORK_STATUS,
                                 .JOIN_DATE = e.JOIN_DATE
                                 })
            If _filter.IS_TER = True Then
                'Return Employees.ToList()
            Else
                Employees = Employees.Where(Function(f) f.WORK_STATUS <> 257)
            End If
            If _filter.TITLE_NAME_VN <> "" Then
                Employees = Employees.Where(Function(f) f.TITLE_NAME_VN.ToUpper().IndexOf(_filter.TITLE_NAME_VN.ToUpper) >= 0)
            End If
            If _filter.FULLNAME_VN <> "" Then
                Employees = Employees.Where(Function(f) f.FULLNAME_VN.ToUpper().IndexOf(_filter.FULLNAME_VN.ToUpper) >= 0)
            End If
            If IsDate(_filter.BIRTH_DATE) Then
                Employees = Employees.Where(Function(f) f.BIRTH_DATE = _filter.BIRTH_DATE)
            End If
            If _filter.MOBILE_PHONE <> "" Then
                Employees = Employees.Where(Function(f) f.MOBILE_PHONE.ToUpper().IndexOf(_filter.MOBILE_PHONE.ToUpper) >= 0)
            End If
            Employees = Employees.OrderBy(Sorts)
            Total = Employees.Count
            Employees = Employees.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lstEmp = Employees.ToList

            For Each emp In lstEmp
                emp.IMAGE_BINARY = GetEmployeeImage(emp.ID, "", False, emp.IMAGE)
            Next
            Return lstEmp
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Lấy danh sách nhân viên ko phân trang
    ''' </summary>
    ''' <param name="_orgIds"></param>
    ''' <param name="_filter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListEmployee(ByVal _orgIds As List(Of Decimal), ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)
        Try

            Dim wstt = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            Dim query As ObjectQuery(Of EmployeeDTO)
            query = (From p In Context.HU_EMPLOYEE
                     From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                     From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                     From c In Context.HU_CONTRACT.Where(Function(c) c.ID = p.CONTRACT_ID)
                     From t In Context.HU_CONTRACT_TYPE.Where(Function(t) t.ID = c.CONTRACT_TYPE_ID)
                     Where _orgIds.Contains(p.ORG_ID) Order By p.EMPLOYEE_CODE
                     Select New EmployeeDTO With {
                      .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                      .ID = p.ID,
                      .FULLNAME_VN = p.FULLNAME_VN,
                      .FULLNAME_EN = p.FULLNAME_EN,
                      .ORG_ID = p.ORG_ID,
                      .ORG_NAME = org.NAME_VN,
                      .ORG_DESC = org.DESCRIPTION_PATH,
                      .TITLE_ID = p.TITLE_ID,
                      .JOIN_DATE = p.JOIN_DATE,
                      .TITLE_NAME_VN = title.NAME_VN,
                      .CONTRACT_TYPE_ID = c.CONTRACT_TYPE_ID,
                      .CONTRACT_TYPE_NAME = t.NAME,
                      .WORK_STATUS = p.WORK_STATUS,
                      .CONTRACT_NO = c.CONTRACT_NO})

            If _filter.CONTRACT_TYPE_ID IsNot Nothing Then
                query = query.Where(Function(p) p.CONTRACT_TYPE_ID = _filter.CONTRACT_TYPE_ID)
            End If

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0 Or
                                                p.FULLNAME_VN.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If

            If _filter.CONTRACT_NO <> "" Then
                query = query.Where(Function(p) p.CONTRACT_NO = _filter.CONTRACT_NO)
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")

            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy danh sách nhân viên ko phân trang bao gồm image để hiển thị lên org chart
    ''' </summary>
    ''' <param name="_orgIds"></param>
    ''' <param name="_filter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeOrgChart(ByVal lstOrg As List(Of Decimal), Optional ByVal log As UserLog = Nothing) As List(Of OrgChartDTO)

        Try
            Context.ExecuteStoreCommand("DELETE SE_CHOSEN_ORG WHERE USERNAME ='" & log.Username.ToUpper & "'")
            For Each i In lstOrg
                Dim obj = New SE_CHOSEN_ORG
                obj.ORG_ID = i
                obj.USERNAME = log.Username.ToUpper
                Context.SE_CHOSEN_ORG.AddObject(obj)
            Next
            Context.SaveChanges()
            Dim dateNow = Date.Now.Date
            Dim query = (From org In Context.HU_ORGANIZATION.Where(Function(f) f.CHK_ORGCHART = -1)
                         From p In Context.HU_EMPLOYEE.Where(Function(f) f.ID = org.REPRESENTATIVE_ID).DefaultIfEmpty
                         From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                         From title In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                         From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = org.ID And f.USERNAME = log.Username.ToUpper)
                         From orgcount In Context.HUV_ORGANIZATION_EMP_COUNT.Where(Function(f) f.ID = org.ID)
                         Where org.ACTFLG = "A" And (org.DISSOLVE_DATE Is Nothing Or
                                                     (org.DISSOLVE_DATE IsNot Nothing And
                                                      org.DISSOLVE_DATE > dateNow))
                         Order By org.ORD_NO
                         Select New OrgChartDTO With {
                             .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                             .FIRST_NAME_VN = p.FIRST_NAME_VN,
                             .LAST_NAME_VN = p.LAST_NAME_VN,
                             .FULLNAME_VN = p.FULLNAME_VN,
                             .TITLE_NAME_VN = title.NAME_VN,
                             .IMAGE = cv.IMAGE,
                             .ID = org.ID,
                             .ORG_NAME = org.NAME_VN,
                             .ORG_CODE = org.CODE,
                             .ORG_LEVEL = org.ORG_LEVEL,
                             .EMP_COUNT = orgcount.EMP_COUNT,
                             .PARENT_ID = org.PARENT_ID,
                             .MOBILE_PHONE = cv.MOBILE_PHONE,
                             .WORK_EMAIL = cv.WORK_EMAIL})

            Dim lstEmp = query.ToList
            For Each emp In lstEmp
                If emp.ORG_LEVEL = 860 Then
                    emp.ORG_NAME = If(emp.ORG_CODE <> "", emp.ORG_CODE, "") & " (" + "Tổng số nhân viên:" & emp.EMP_COUNT & ")"

                Else
                    emp.ORG_NAME = emp.ORG_NAME & " (" + "Tổng số nhân viên:" & emp.EMP_COUNT & ")"
                End If
                emp.IMAGE_BINARY = GetEmployeeImage(emp.ID, "", False, emp.IMAGE)
            Next
            Return lstEmp
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function GetListEmployeePortal(ByVal _filter As EmployeeDTO) As List(Of EmployeeDTO)
        Try
            Dim wstt = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            Dim query = From p In Context.HU_EMPLOYEE
                        Order By p.EMPLOYEE_CODE

            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                                       .ID = p.ID,
                                       .FULLNAME_VN = p.FULLNAME_VN,
                                       .TER_EFFECT_DATE = p.TER_EFFECT_DATE,
                                       .DIRECT_MANAGER = p.DIRECT_MANAGER,
                                       .WORK_STATUS = p.WORK_STATUS})

            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                lst = lst.Where(Function(p) Not p.WORK_STATUS.HasValue Or
                                    (p.WORK_STATUS.HasValue And
                                     ((p.WORK_STATUS <> terID) Or (p.WORK_STATUS = terID And p.TER_EFFECT_DATE > dateNow))))

            End If

            If _filter.DIRECT_MANAGER IsNot Nothing Then
                lst = lst.Where(Function(p) p.DIRECT_MANAGER = _filter.DIRECT_MANAGER)
            End If

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function GetListEmployeePagingEx(ByVal _filter As EmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG_EX",
                                     New With {.P_USERNAME = log.Username,
                                               .P_ORGID = _param.ORG_ID,
                                               .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                               .P_FID = "Profile/ManageEmployee/Index"})
            End Using
            Dim fileDirectory = ""
            Dim str As String = "Kiêm nhiệm"
            fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
            Dim wstt = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            Dim userId = (From p In Context.SE_USER Where p.USERNAME.ToUpper = log.Username.ToUpper Select p.ID).FirstOrDefault
            Dim fid = (From p In Context.SE_FUNCTION Where p.FID = "Profile/ManageEmployee/Index" Select p.ID).FirstOrDefault
            Dim shortName = "HĐCLTĐ"

            Dim query = From p In Context.HU_EMPLOYEE
                        From cmo In (From commitee In Context.HU_COMMITEE.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                                     From co In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = commitee.ORG_ID).DefaultIfEmpty
                                     Where co.UY_BAN = -1 And co.SHORT_NAME = shortName Select co).DefaultIfEmpty
                        From cmt In (From commitee In Context.HU_COMMITEE.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                                     From co In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = commitee.ORG_ID).DefaultIfEmpty
                                     From ct In Context.HU_TITLE.Where(Function(f) f.ID = commitee.TITLE_ID).DefaultIfEmpty
                                     Where co.UY_BAN = -1 And co.SHORT_NAME = shortName Select ct).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From dt In Context.HU_TITLE.Where(Function(f) title.LM = f.ID).DefaultIfEmpty
                        From d In Context.HU_EMPLOYEE.Where(Function(f) f.ID = dt.MASTER).DefaultIfEmpty
                        From pv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From s In Context.HU_JOB_BAND.Where(Function(F) p.STAFF_RANK_ID = F.ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) p.WORK_STATUS = f.ID And f.TYPE_ID = 59).DefaultIfEmpty
                        From org In Context.HUV_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) (p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper And f.WORK_LOCATION_ID = p.STAFF_RANK_ID) _
                                                                Or (p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper And f.WORK_LOCATION_ID = -1))
                        From c In Context.HU_CONTRACT.Where(Function(c) c.ID = p.CONTRACT_ID).DefaultIfEmpty
                        From ct In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From emp_stt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EMP_STATUS And f.TYPE_ID = 2235).DefaultIfEmpty
                        From labor In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_LABOR And f.TYPE_ID = 6963).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = pv.GENDER And f.TYPE_ID = 34).DefaultIfEmpty
                        From id_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = pv.ID_PLACE).DefaultIfEmpty
                        From bank In Context.HU_BANK.Where(Function(f) f.ID = pv.BANK_ID).DefaultIfEmpty
                        From object_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_EMPLOYEE_ID And f.TYPE_CODE = "OBJECT_EMPLOYEE").DefaultIfEmpty
                        From orgreg In Context.OT_OTHER_LIST.Where(Function(f) f.ID = org.ORG_REG_ID And f.TYPE_CODE = "REGION_CH").DefaultIfEmpty
                        From location In Context.HU_LOCATION.Where(Function(f) f.ID = p.CONTRACTED_UNIT).DefaultIfEmpty
                        Order By p.EMPLOYEE_CODE()

            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                             .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                             .ID_DATE = p.pv.ID_DATE,
                             .ID_PLACE = p.id_pro.NAME_VN,
                             .BANK_NO = p.pv.BANK_NO,
                             .BANK_ID = p.bank.NAME,
                             .ID = p.p.ID,
                             .EMPLOYEE_ID = p.p.ID,
                             .DIRECT_MANAGER = p.p.DIRECT_MANAGER,
                             .DIRECT_MANAGER_NAME = p.d.EMPLOYEE_CODE + " - " + p.d.FULLNAME_VN,
                             .FULLNAME_VN = p.p.FULLNAME_VN,
                             .FULLNAME_EN = p.p.FULLNAME_EN,
                             .ORG_ID = If(p.cmo IsNot Nothing, p.cmo.ID, p.cmo.ID),
                             .ORG_NAME = If(p.cmo IsNot Nothing, p.cmo.NAME_VN, p.org.NAME_VN),
                             .ORG_DESC = If(p.cmo IsNot Nothing, p.cmo.DESCRIPTION_PATH, p.cmo.DESCRIPTION_PATH),
                             .ORG_NAME1 = If(p.cmo IsNot Nothing, p.cmo.ORG_NAME1, p.org.ORG_NAME1),
                             .ORG_NAME2 = If(p.cmo IsNot Nothing, p.cmo.ORG_NAME2, p.org.ORG_NAME2),
                             .ORG_NAME3 = If(p.cmo IsNot Nothing, p.cmo.ORG_NAME3, p.org.ORG_NAME3),
                             .ORG_NAME4 = If(p.cmo IsNot Nothing, p.cmo.ORG_NAME4, p.org.ORG_NAME4),
                             .ORG_NAME5 = If(p.cmo IsNot Nothing, p.cmo.ORG_NAME5, p.org.ORG_NAME5),
                             .ORG_NAME6 = If(p.cmo IsNot Nothing, p.cmo.ORG_NAME6, p.org.ORG_NAME6),
                             .ORG_NAME7 = If(p.cmo IsNot Nothing, p.cmo.ORG_NAME7, p.org.ORG_NAME7),
                             .ORG_NAME8 = If(p.cmo IsNot Nothing, p.cmo.ORG_NAME8, p.org.ORG_NAME8),
                             .ORG_NAME9 = If(p.cmo IsNot Nothing, p.cmo.ORG_NAME9, p.org.ORG_NAME9),
                             .ORG_NAME10 = If(p.cmo IsNot Nothing, p.cmo.ORG_NAME10, p.org.ORG_NAME10),
                             .ORG_NAME11 = If(p.cmo IsNot Nothing, p.cmo.ORG_NAME11, p.org.ORG_NAME11),
                             .TITLE_ID = If(p.cmt IsNot Nothing, p.cmt.ID, p.title.ID),
                             .TITLE_NAME_VN = If(p.cmt IsNot Nothing, p.cmt.CODE + " - " + p.cmt.NAME_VN, p.title.CODE + " - " + p.title.NAME_VN),
                             .STAFF_RANK_ID = p.p.STAFF_RANK_ID,
                             .STAFF_RANK_NAME = p.s.LEVEL_FROM,
                             .CONTRACT_TYPE_ID = p.ct.ID,
                             .CONTRACT_TYPE_NAME = p.ct.NAME,
                             .TER_EFFECT_DATE = p.p.TER_EFFECT_DATE,
                             .WORK_STATUS = p.p.WORK_STATUS,
                             .WORK_STATUS_NAME = p.o.NAME_VN,
                             .JOIN_DATE = p.p.JOIN_DATE,
                             .JOIN_DATE_STATE = p.p.JOIN_DATE_STATE,
                             .LAST_WORKING_ID = p.p.LAST_WORKING_ID,
                             .CONTRACT_NO = p.p.HU_CONTRACT_NOW.CONTRACT_NO,
                             .CONTRACT_ID = p.p.CONTRACT_ID,
                             .ID_NO = p.pv.ID_NO,
                             .IMAGE = p.pv.IMAGE,
                             .ITIME_ID = p.p.ITIME_ID,
                             .OBJECT_LABOR = p.p.OBJECT_LABOR,
                             .OBJECT_LABOR_NAME = p.labor.NAME_VN,
                             .GENDER = p.pv.GENDER,
                             .GENDER_NAME = p.gender.NAME_VN,
                             .MODIFIED_DATE = p.p.MODIFIED_DATE,
                             .EMP_STATUS = p.p.EMP_STATUS,
                             .OBJECT_EMPLOYEE_NAME = p.object_type.NAME_VN,
                             .BIRTH_DATE = p.pv.BIRTH_DATE,
                             .SUBMIT_PROFILE = If(p.p.SUBMITED_ENOUGH Is Nothing Or p.p.SUBMITED_ENOUGH = 0, "Chưa hoàn thành", "Hoàn thành"),
                             .EMP_STATUS_NAME = If(p.p.IS_KIEM_NHIEM IsNot Nothing, str, p.emp_stt.NAME_VN),
                             .WORK_EMAIL = p.pv.WORK_EMAIL,
                             .MOBILE_PHONE = p.pv.MOBILE_PHONE,
                             .PER_EMAIL = p.pv.PER_EMAIL,
                             .PIT_CODE = p.pv.PIT_CODE,
                             .ORG_REG_NAME = p.orgreg.NAME_VN,
                             .CONTRACTED_UNIT = p.p.CONTRACTED_UNIT,
                             .CONTRACTED_UNIT_NAME = p.location.NAME_VN})

            If _filter.TITLE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)
            End If
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                lst = lst.Where(Function(p) Not p.WORK_STATUS.HasValue Or
                                    (p.WORK_STATUS.HasValue And
                                     ((p.WORK_STATUS <> terID) Or (p.WORK_STATUS = terID And p.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If

            If _filter.FULLNAME_VN <> "" Then
                lst = lst.Where(Function(p) p.FULLNAME_VN.ToUpper().IndexOf(_filter.FULLNAME_VN.ToUpper) >= 0)
            End If

            If _filter.TITLE_NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME_VN.ToUpper().IndexOf(_filter.TITLE_NAME_VN.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper().IndexOf(_filter.ORG_NAME.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME2 <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME2.ToUpper().IndexOf(_filter.ORG_NAME2.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME3 <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME3.ToUpper().IndexOf(_filter.ORG_NAME3.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME4 <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME4.ToUpper().IndexOf(_filter.ORG_NAME4.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME5 <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME5.ToUpper().IndexOf(_filter.ORG_NAME5.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME6 <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME6.ToUpper().IndexOf(_filter.ORG_NAME6.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME7 <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME7.ToUpper().IndexOf(_filter.ORG_NAME7.ToUpper) >= 0)
            End If

            If _filter.STAFF_RANK_NAME <> "" Then
                lst = lst.Where(Function(p) p.STAFF_RANK_NAME.ToUpper().IndexOf(_filter.STAFF_RANK_NAME.ToUpper) >= 0)
            End If

            If _filter.GHI_CHU_SUC_KHOE <> "" Then
                lst = lst.Where(Function(p) p.GHI_CHU_SUC_KHOE.ToUpper().IndexOf(_filter.GHI_CHU_SUC_KHOE.ToUpper) >= 0)
            End If

            If _filter.JOIN_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE = _filter.JOIN_DATE)
            End If

            If _filter.JOIN_DATE_STATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE_STATE = _filter.JOIN_DATE_STATE)
            End If

            If _filter.FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE >= _filter.FROM_DATE)
            End If

            If _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE <= _filter.TO_DATE)
            End If

            If _filter.WORK_STATUS_NAME <> "" Then
                lst = lst.Where(Function(p) p.WORK_STATUS_NAME.ToUpper().IndexOf(_filter.WORK_STATUS_NAME.ToUpper) >= 0)
            End If

            If _filter.ID_NO <> "" Then
                lst = lst.Where(Function(p) p.ID_NO.ToUpper().IndexOf(_filter.ID_NO.ToUpper) >= 0)
            End If

            If _filter.DIRECT_MANAGER IsNot Nothing Then
                lst = lst.Where(Function(p) p.DIRECT_MANAGER = _filter.DIRECT_MANAGER)
            End If

            If _filter.EMPLOYEE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.ID = _filter.EMPLOYEE_ID)
            End If

            If _filter.OBJECT_LABOR_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJECT_LABOR_NAME.ToUpper().IndexOf(_filter.OBJECT_LABOR_NAME.ToUpper) >= 0)
            End If

            If _filter.GENDER_NAME <> "" Then
                lst = lst.Where(Function(p) p.GENDER_NAME.ToUpper().IndexOf(_filter.GENDER_NAME.ToUpper) >= 0)
            End If

            If _filter.EMP_STATUS_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMP_STATUS_NAME.ToUpper().IndexOf(_filter.EMP_STATUS_NAME.ToUpper) >= 0)
            End If

            If _filter.BIRTH_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.BIRTH_DATE = _filter.BIRTH_DATE)
            End If

            If _filter.OBJECT_EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJECT_EMPLOYEE_NAME.ToUpper().IndexOf(_filter.OBJECT_EMPLOYEE_NAME.ToUpper) >= 0)
            End If

            If _filter.WORK_EMAIL <> "" Then
                lst = lst.Where(Function(p) p.WORK_EMAIL.ToUpper().Contains(_filter.WORK_EMAIL.ToUpper))
            End If

            If _filter.MustHaveContract Then
                lst = lst.Where(Function(p) p.CONTRACT_ID.HasValue)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lstEmp = lst.ToList
            If _filter.IS_GET_FOR_PORTAL Then
                For Each item In lstEmp
                    item.THAMNIEN = CalculateSeniority(item.JOIN_DATE)
                Next
            End If

            Return lstEmp
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Lấy sanh sách nhân viên có phân trang
    ''' </summary>
    ''' <param name="PageIndex"></param>
    ''' <param name="PageSize"></param>
    ''' <param name="Total"></param>
    ''' <param name="_orgIds"></param>
    ''' <param name="_filter"></param>
    ''' <param name="Sorts"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListEmployeePaging(ByVal _filter As EmployeeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                          Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        Try
            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG_EX",
            '                     New With {.P_USERNAME = log.Username,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = _param.IS_DISSOLVE,
            '                               .P_FID = _filter.FID})
            'End Using

            'From ew In Context.HUV_EMPLOYEE_WORKING.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
            '       From org1 In Context.SE_CHOSEN_ORG.Where(Function(f) (f.WORK_LOCATION_ID = -1 And p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper) _
            '                                                       Or (f.WORK_LOCATION_ID <> -1 And ew.ORG_ID = f.ORG_ID And ew.WORK_PLACE_ID = f.WORK_LOCATION_ID And f.USERNAME.ToUpper = log.Username.ToUpper))()


            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim fileDirectory = ""
            Dim str As String = "Kiêm nhiệm"
            fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
            Dim wstt = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            Dim query = From p In Context.HU_EMPLOYEE
                        From title In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From dt In Context.HU_TITLE.Where(Function(f) title.LM = f.ID).DefaultIfEmpty
                        From d In Context.HU_EMPLOYEE.Where(Function(f) f.ID = dt.MASTER).DefaultIfEmpty
                        From pv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From s In Context.HU_JOB_BAND.Where(Function(F) p.STAFF_RANK_ID = F.ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) p.WORK_STATUS = f.ID And f.TYPE_ID = 59).DefaultIfEmpty
                        From org In Context.HUV_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From c In Context.HU_CONTRACT.Where(Function(c) c.ID = p.CONTRACT_ID).DefaultIfEmpty
                        From ct In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                        From emp_stt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EMP_STATUS And f.TYPE_ID = 2235).DefaultIfEmpty
                        From labor In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_LABOR And f.TYPE_ID = 6963).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = pv.GENDER And f.TYPE_ID = 34).DefaultIfEmpty
                        From id_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = pv.ID_PLACE).DefaultIfEmpty
                        From bank In Context.HU_BANK.Where(Function(f) f.ID = pv.BANK_ID).DefaultIfEmpty
                        From object_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_EMPLOYEE_ID And f.TYPE_CODE = "OBJECT_EMPLOYEE").DefaultIfEmpty
                        From orgreg In Context.OT_OTHER_LIST.Where(Function(f) f.ID = org.ORG_REG_ID And f.TYPE_CODE = "REGION_CH").DefaultIfEmpty
                        From objAtt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANT_ID And f.TYPE_CODE = "OBJECT_ATTENDANT").DefaultIfEmpty
                        From otk In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECTTIMEKEEPING).DefaultIfEmpty
                        From location In Context.HU_LOCATION.Where(Function(f) f.ID = p.CONTRACTED_UNIT).DefaultIfEmpty
                        Order By p.EMPLOYEE_CODE()

            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                             .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                             .ID_DATE = p.pv.ID_DATE,
                             .ID_PLACE = p.id_pro.NAME_VN,
                             .BANK_NO = p.pv.BANK_NO,
                             .BANK_ID = p.bank.NAME,
                             .ID = p.p.ID,
                             .EMPLOYEE_ID = p.p.ID,
                             .DIRECT_MANAGER = p.p.DIRECT_MANAGER,
                             .DIRECT_MANAGER_NAME = p.d.EMPLOYEE_CODE + " - " + p.d.FULLNAME_VN,
                             .FULLNAME_VN = p.p.FULLNAME_VN,
                             .FULLNAME_EN = p.p.FULLNAME_EN,
                             .ORG_ID = p.p.ORG_ID,
                             .ORG_NAME = p.org.NAME_VN,
                             .ORG_DESC = p.org.DESCRIPTION_PATH,
                             .ORG_NAME1 = p.org.ORG_NAME1,
                             .ORG_NAME2 = p.org.ORG_NAME2,
                             .ORG_NAME3 = p.org.ORG_NAME3,
                             .ORG_NAME4 = p.org.ORG_NAME4,
                             .ORG_NAME5 = p.org.ORG_NAME5,
                             .ORG_NAME6 = p.org.ORG_NAME6,
                             .ORG_NAME7 = p.org.ORG_NAME7,
                             .ORG_NAME8 = p.org.ORG_NAME8,
                             .ORG_NAME9 = p.org.ORG_NAME9,
                             .ORG_NAME10 = p.org.ORG_NAME10,
                             .ORG_NAME11 = p.org.ORG_NAME11,
                             .TITLE_ID = p.p.TITLE_ID,
                             .TITLE_NAME_VN = p.title.CODE & " - " & p.title.NAME_VN,
                             .STAFF_RANK_ID = p.p.STAFF_RANK_ID,
                             .STAFF_RANK_NAME = p.s.LEVEL_FROM,
                             .CONTRACT_TYPE_ID = p.ct.ID,
                             .CONTRACT_TYPE_NAME = p.ct.NAME,
                             .TER_EFFECT_DATE = p.p.TER_EFFECT_DATE,
                             .WORK_STATUS = p.p.WORK_STATUS,
                             .WORK_STATUS_NAME = p.o.NAME_VN,
                             .JOIN_DATE = p.p.JOIN_DATE,
                             .JOIN_DATE_STATE = p.p.JOIN_DATE_STATE,
                             .LAST_WORKING_ID = p.p.LAST_WORKING_ID,
                             .CONTRACT_NO = p.p.HU_CONTRACT_NOW.CONTRACT_NO,
                             .CONTRACT_ID = p.p.CONTRACT_ID,
                             .ID_NO = p.pv.ID_NO,
                             .IMAGE = p.pv.IMAGE,
                             .ITIME_ID = p.p.ITIME_ID,
                             .OBJECT_LABOR = p.p.OBJECT_LABOR,
                             .OBJECT_LABOR_NAME = p.labor.NAME_VN,
                             .GENDER = p.pv.GENDER,
                             .GENDER_NAME = p.gender.NAME_VN,
                             .MODIFIED_DATE = p.p.MODIFIED_DATE,
                             .EMP_STATUS = p.p.EMP_STATUS,
                             .OBJECT_EMPLOYEE_NAME = p.object_type.NAME_VN,
                             .BIRTH_DATE = p.pv.BIRTH_DATE,
                             .SUBMIT_PROFILE = If(p.p.SUBMITED_ENOUGH Is Nothing Or p.p.SUBMITED_ENOUGH = 0, "Chưa hoàn thành", "Hoàn thành"),
                             .EMP_STATUS_NAME = If(p.p.IS_KIEM_NHIEM IsNot Nothing, str, p.emp_stt.NAME_VN),
                             .WORK_EMAIL = p.pv.WORK_EMAIL,
                             .MOBILE_PHONE = p.pv.MOBILE_PHONE,
                             .PER_EMAIL = p.pv.PER_EMAIL,
                             .PIT_CODE = p.pv.PIT_CODE,
                             .ORG_REG_NAME = p.orgreg.NAME_VN,
                             .OBJECT_ATTENDANT_ID = p.objAtt.ID,
                             .OBJECT_ATTENDANT_NAME = p.objAtt.NAME_VN,
                             .OBJECTTIMEKEEPING = p.p.OBJECTTIMEKEEPING,
                             .OBJECTTIMEKEEPING_NAME = p.otk.NAME_VN,
                             .CONTRACTED_UNIT = p.p.CONTRACTED_UNIT,
                             .CONTRACTED_UNIT_NAME = p.location.NAME_VN,
                             .DOAN_PHI = If(p.pv.DOAN_PHI = -1, "X", ""),
                             .DANG = If(p.pv.DANG = -1, "X", "")})

            If _filter.TITLE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)
            End If
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                lst = lst.Where(Function(p) Not p.WORK_STATUS.HasValue Or
                                    (p.WORK_STATUS.HasValue And
                                     ((p.WORK_STATUS <> terID) Or (p.WORK_STATUS = terID And p.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If

            If _filter.FULLNAME_VN <> "" Then
                lst = lst.Where(Function(p) p.FULLNAME_VN.ToUpper().IndexOf(_filter.FULLNAME_VN.ToUpper) >= 0)
            End If

            If _filter.TITLE_NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME_VN.ToUpper().IndexOf(_filter.TITLE_NAME_VN.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper().IndexOf(_filter.ORG_NAME.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME2 <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME2.ToUpper().IndexOf(_filter.ORG_NAME2.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME3 <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME3.ToUpper().IndexOf(_filter.ORG_NAME3.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME4 <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME4.ToUpper().IndexOf(_filter.ORG_NAME4.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME5 <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME5.ToUpper().IndexOf(_filter.ORG_NAME5.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME6 <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME6.ToUpper().IndexOf(_filter.ORG_NAME6.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME7 <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME7.ToUpper().IndexOf(_filter.ORG_NAME7.ToUpper) >= 0)
            End If

            If _filter.STAFF_RANK_NAME <> "" Then
                lst = lst.Where(Function(p) p.STAFF_RANK_NAME.ToUpper().IndexOf(_filter.STAFF_RANK_NAME.ToUpper) >= 0)
            End If

            If _filter.GHI_CHU_SUC_KHOE <> "" Then
                lst = lst.Where(Function(p) p.GHI_CHU_SUC_KHOE.ToUpper().IndexOf(_filter.GHI_CHU_SUC_KHOE.ToUpper) >= 0)
            End If

            If _filter.JOIN_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE = _filter.JOIN_DATE)
            End If

            If _filter.JOIN_DATE_STATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE_STATE = _filter.JOIN_DATE_STATE)
            End If

            If _filter.FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE >= _filter.FROM_DATE)
            End If

            If _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE <= _filter.TO_DATE)
            End If

            If _filter.WORK_STATUS_NAME <> "" Then
                lst = lst.Where(Function(p) p.WORK_STATUS_NAME.ToUpper().IndexOf(_filter.WORK_STATUS_NAME.ToUpper) >= 0)
            End If

            If _filter.ID_NO <> "" Then
                lst = lst.Where(Function(p) p.ID_NO.ToUpper().IndexOf(_filter.ID_NO.ToUpper) >= 0)
            End If

            If _filter.DIRECT_MANAGER IsNot Nothing Then
                lst = lst.Where(Function(p) p.DIRECT_MANAGER = _filter.DIRECT_MANAGER)
            End If

            If _filter.EMPLOYEE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.ID = _filter.EMPLOYEE_ID)
            End If

            If _filter.OBJECT_LABOR_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJECT_LABOR_NAME.ToUpper().IndexOf(_filter.OBJECT_LABOR_NAME.ToUpper) >= 0)
            End If

            If _filter.GENDER_NAME <> "" Then
                lst = lst.Where(Function(p) p.GENDER_NAME.ToUpper().IndexOf(_filter.GENDER_NAME.ToUpper) >= 0)
            End If

            If _filter.EMP_STATUS_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMP_STATUS_NAME.ToUpper().IndexOf(_filter.EMP_STATUS_NAME.ToUpper) >= 0)
            End If

            If _filter.BIRTH_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.BIRTH_DATE = _filter.BIRTH_DATE)
            End If

            If _filter.OBJECT_EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJECT_EMPLOYEE_NAME.ToUpper().IndexOf(_filter.OBJECT_EMPLOYEE_NAME.ToUpper) >= 0)
            End If

            If _filter.OBJECT_ATTENDANT_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJECT_ATTENDANT_NAME.ToUpper().IndexOf(_filter.OBJECT_ATTENDANT_NAME.ToUpper) >= 0)
            End If

            If _filter.WORK_EMAIL <> "" Then
                lst = lst.Where(Function(p) p.WORK_EMAIL.ToUpper().Contains(_filter.WORK_EMAIL.ToUpper))
            End If
            If _filter.DOAN_PHI <> "" Then
                lst = lst.Where(Function(p) p.DOAN_PHI.ToUpper().Contains(_filter.DOAN_PHI.ToUpper))
            End If
            If _filter.DANG <> "" Then
                lst = lst.Where(Function(p) p.DANG.ToUpper().Contains(_filter.DANG.ToUpper))
            End If
            If _filter.MustHaveContract Then
                lst = lst.Where(Function(p) p.CONTRACT_ID.HasValue)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lstEmp = lst.ToList
            'Dim value As Integer = 0
            'Dim value1 As Integer = 0
            'Using cls As New DataAccess.QueryData
            '    Dim obj = New With {.P_CODENAME = "APP_SETTING_4",
            '                        .P_OUT = cls.OUT_NUMBER}
            '    cls.ExecuteStore("PKG_PROFILE.GET_VALUE_CHECK_IS_SENIORITY", obj)
            '    value = Integer.Parse(obj.P_OUT)

            '    Dim obj1 = New With {.P_CODENAME = "APP_ISHIDE_IMAGE",
            '                       .P_OUT = cls.OUT_NUMBER}
            '    cls.ExecuteStore("PKG_PROFILE.GET_VALUE_CHECK_IS_SENIORITY", obj1)
            '    value1 = Integer.Parse(obj1.P_OUT)
            'End Using
            'If value1 = 0 Then
            '    For Each emp In lstEmp
            '        emp.IMAGE_BINARY = GetEmployeeImage(emp.ID, "", False, emp.IMAGE)
            '    Next
            'End If
            'For Each emp In lstEmp
            '    'If value1 = 0 Then
            '    '    emp.IMAGE_BINARY = GetEmployeeImage(emp.ID, "", False, emp.IMAGE)
            '    'End If
            '    emp.THAMNIEN = CalculateSeniority(emp.JOIN_DATE, emp.ID, value)
            'Next
            If _filter.IS_GET_FOR_PORTAL Then
                For Each item In lstEmp
                    item.THAMNIEN = CalculateSeniority(item.JOIN_DATE)
                Next
            End If

            Return lstEmp
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Private Function CalculateSeniority(ByVal dStart As Date?, ByVal emp_id As Decimal, ByVal value_check As Integer) As String
        Dim dSoNam As Double = 0
        Dim dSoThang As Double = 0
        Dim Cal_Month_Emp As Int32 = 0
        Dim str As String = ""
        Dim Cal1 As Integer = 0
        Dim Cal2 As Integer = 0
        Dim lastDayOfMonth As Integer = 0
        Dim Total_Month As Decimal = 0
        'Cong thuc tinh tham nien=Total_Month-2+Cal1+Cal2
        Try
            If IsDate(dStart) Then
                'Dim Cal_Day_Emp = Math.Round((CDate(Date.Now.Date).Subtract(CDate(dStart)).TotalDays) + 1, 2)
                'Dim Cal_Month_Emp = Math.Round(Cal_Day_Emp / 365 * 12, 2) ' tham nien nhan vien tai HSV
                'Dim Month_Work = Get_Month_Work_Before1(emp_id, value_check) ' Tham nien tai cty khac

                Dim Month_Work = Get_Month_Work_Before1(emp_id, value_check) ' Tham nien tai cty khac
                Cal_Month_Emp = (DateDiff(DateInterval.Month, CDate(dStart), CDate(Date.Now.Date))) + 1
                If CDate(dStart).Day <= 5 Then
                    Cal1 = 1
                Else
                    Cal1 = 0
                End If
                lastDayOfMonth = (DateTime.DaysInMonth(Date.Now.Year, Date.Now.Month)) - 5
                If Date.Now.Day >= lastDayOfMonth Then
                    Cal2 = 1
                Else
                    Cal2 = 0
                End If
                'str = Math.Round(((Cal_Month_Emp - 2 + Cal1 + Cal2) + Month_Work), 2).ToString
                Total_Month = Math.Round(((Cal_Month_Emp - 2 + Cal1 + Cal2) + Month_Work), 2)
                If IsNumeric(Total_Month) Then
                    dSoNam = Total_Month \ 12
                    dSoThang = Math.Round(CDec(Total_Month) Mod 12, 2)
                    str = If(dSoNam > 0, dSoNam.ToString + " Năm ", "") + If(Math.Round(CDec(dSoThang) Mod 12, 2) > 0, Math.Round(CDec(dSoThang) Mod 12, 2).ToString + " Tháng", "")
                End If
            End If
            Return str
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function Get_Month_Work_Before1(ByVal emp_id As Decimal, ByVal value As Integer) As Decimal
        Try
            Dim Month_Work As Decimal = 0
            If value = -1 Then
                Month_Work = If((From a In Context.HU_WORKING_BEFORE.Where(Function(f) f.EMPLOYEE_ID = emp_id And f.IS_THAMNIEN = True) Select a.THAM_NIEN).Sum Is Nothing, 0, (From a In Context.HU_WORKING_BEFORE.Where(Function(f) f.EMPLOYEE_ID = emp_id And f.IS_THAMNIEN = True) Select a.THAM_NIEN).Sum) ' Tham nien tai cty khac
            Else
                Month_Work = 0
            End If
            Return Month_Work
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Get_Month_Work_Before(ByVal emp_id As Decimal) As Decimal
        Try
            Dim value As Integer = 0
            Dim Month_Work As Decimal = 0
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_CODENAME = "APP_SETTING_4",
                                    .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_PROFILE.GET_VALUE_CHECK_IS_SENIORITY", obj)
                value = Integer.Parse(obj.P_OUT)
            End Using
            If value = -1 Then
                Month_Work = If((From a In Context.HU_WORKING_BEFORE.Where(Function(f) f.EMPLOYEE_ID = emp_id And f.IS_THAMNIEN = True) Select a.THAM_NIEN).Sum Is Nothing, 0, (From a In Context.HU_WORKING_BEFORE.Where(Function(f) f.EMPLOYEE_ID = emp_id And f.IS_THAMNIEN = True) Select a.THAM_NIEN).Sum) ' Tham nien tai cty khac
            Else
                Month_Work = 0
            End If
            Return Month_Work
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetListWorkingBefore(ByVal _filter As WorkingBeforeDTO,
                                          ByVal PageIndex As Integer,
                                          ByVal PageSize As Integer,
                                          ByRef Total As Integer, ByVal _param As ParamDTO,
                                          Optional ByVal Sorts As String = "CREATED_DATE DESC",
                                          Optional ByVal log As UserLog = Nothing) As List(Of WorkingBeforeDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From b In Context.HU_WORKING_BEFORE
                        From e In Context.HU_EMPLOYEE.Where(Function(p) p.ID = b.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(p) p.ID = e.ORG_ID).DefaultIfEmpty
                        From org In Context.HUV_ORGANIZATION.Where(Function(p) p.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(p) p.ID = e.TITLE_ID).DefaultIfEmpty
                        From file In Context.HU_USERFILES.Where(Function(p) p.NAME = b.FILE_NAME).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) e.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)


            Dim lst = query.Select(Function(p) New WorkingBeforeDTO With {
                             .ID = p.b.ID,
                             .EMPLOYEE_ID = p.e.ID,
                             .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                             .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                             .ORG_NAME = p.o.NAME_VN,
                             .TITLE_NAME = p.t.NAME_VN,
                             .ORG_DESC = p.org.DESCRIPTION_PATH,
                             .JOIN_DATE = p.b.JOIN_DATE,
                             .END_DATE = p.b.END_DATE,
                             .THAM_NIEN = p.b.THAM_NIEN,
                             .COMPANY_NAME = p.b.COMPANY_NAME,
                             .COMPANY_ADDRESS = p.b.COMPANY_ADDRESS,
                             .TITLE_NAME_BEFORE = p.b.TITLE_NAME,
                             .WORK = p.b.WORK,
                             .TER_REASON = p.b.TER_REASON,
                             .IS_HSV = p.b.IS_HSV,
                             .IS_THAMNIEN = p.b.IS_THAMNIEN,
                             .DEPARTMENT = p.b.DEPARTMENT,
                             .FILE_NAME = p.file.FILE_NAME,
                             .UPLOAD_FILE = p.file.NAME})

            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If

            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper().IndexOf(_filter.EMPLOYEE_NAME.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper().IndexOf(_filter.ORG_NAME.ToUpper) >= 0)
            End If

            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper().IndexOf(_filter.TITLE_NAME.ToUpper) >= 0)
            End If

            If _filter.EMPLOYEE_ID <> 0 Then
                lst = lst.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If

            If _filter.JOIN_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE >= _filter.JOIN_DATE)
            End If

            If _filter.END_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.END_DATE <= _filter.END_DATE)
            End If

            If _filter.THAM_NIEN IsNot Nothing Then
                lst = lst.Where(Function(p) p.THAM_NIEN = _filter.THAM_NIEN)
            End If

            If _filter.COMPANY_NAME <> "" Then
                lst = lst.Where(Function(p) p.COMPANY_NAME.ToUpper().IndexOf(_filter.COMPANY_NAME.ToUpper) >= 0)
            End If

            If _filter.DEPARTMENT <> "" Then
                lst = lst.Where(Function(p) p.DEPARTMENT.ToUpper().IndexOf(_filter.DEPARTMENT.ToUpper) >= 0)
            End If

            If _filter.COMPANY_ADDRESS <> "" Then
                lst = lst.Where(Function(p) p.COMPANY_ADDRESS.ToUpper().IndexOf(_filter.COMPANY_ADDRESS.ToUpper) >= 0)
            End If

            If _filter.TITLE_NAME_BEFORE <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME_BEFORE.ToUpper().IndexOf(_filter.TITLE_NAME_BEFORE.ToUpper) >= 0)
            End If

            If _filter.WORK <> "" Then
                lst = lst.Where(Function(p) p.WORK.ToUpper().IndexOf(_filter.WORK.ToUpper) >= 0)
            End If

            If _filter.TER_REASON <> "" Then
                lst = lst.Where(Function(p) p.TER_REASON.ToUpper().IndexOf(_filter.TER_REASON.ToUpper) >= 0)
            End If



            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lstEmp = lst.ToList
            For Each emp In lstEmp
                If emp.THAM_NIEN IsNot Nothing Then
                    emp.COL_THAM_NIEN = If(CInt(CDec(emp.THAM_NIEN) \ 12) > 0, CInt(CDec(emp.THAM_NIEN) \ 12).ToString + " Năm ", "") + If(Math.Round(CDec(emp.THAM_NIEN) Mod 12, 2) > 0, Math.Round(CDec(emp.THAM_NIEN) Mod 12, 2).ToString + " Tháng", "")
                End If
            Next
            Return lstEmp

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Private Function Calculate_(ByVal dStart As Date?) As String
        Dim dSoNam As Double
        Dim str As String = ""
        Dim dSoThang As Double
        Dim dDuThang As Double
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If IsDate(dStart) Then
                Dim clsDate As Framework.Data.DateDifference = New Framework.Data.DateDifference(dStart, Date.Now)
                dSoNam = clsDate.Years
                dSoThang = clsDate.Months
                dDuThang = Math.Round((clsDate.Days) / 30, 1)
                If dSoNam <> 0 Then
                    str += dSoNam & " năm "
                End If
                If dSoThang <> 0 Then
                    str += dSoThang + dDuThang & " tháng"
                End If
            End If
            Return str
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetChartEmployee(ByVal _filter As EmployeeDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim fileDirectory = ""
            fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
            Dim wstt = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            Dim query = From p In Context.HU_EMPLOYEE
                        From pv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(F) p.STAFF_RANK_ID = F.ID).DefaultIfEmpty
                        From health In Context.HU_EMPLOYEE_HEALTH.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) p.WORK_STATUS = f.ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From ot In Context.HU_ORG_TITLE.Where(Function(f) f.TITLE_ID = title.ID And f.ORG_ID = org.ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From c In Context.HU_CONTRACT.Where(Function(c) c.ID = p.CONTRACT_ID).DefaultIfEmpty
                        From ct In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                        Order By p.EMPLOYEE_CODE

            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                             .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                             .ID = p.p.ID,
                             .EMPLOYEE_ID = p.p.ID,
                             .DIRECT_MANAGER = p.p.DIRECT_MANAGER,
                             .FULLNAME_VN = p.p.FULLNAME_VN,
                             .FULLNAME_EN = p.p.FULLNAME_EN,
                             .ORG_ID = p.p.ORG_ID,
                             .ORG_NAME = p.org.NAME_VN,
                             .ORG_DESC = p.org.DESCRIPTION_PATH,
                             .TITLE_ID = p.p.TITLE_ID,
                             .TITLE_NAME_VN = p.title.NAME_VN,
                             .STAFF_RANK_ID = p.p.STAFF_RANK_ID,
                             .STAFF_RANK_NAME = p.s.NAME,
                             .CONTRACT_TYPE_ID = p.ct.ID,
                             .CONTRACT_TYPE_NAME = p.ct.NAME,
                             .TER_EFFECT_DATE = p.p.TER_EFFECT_DATE,
                             .WORK_STATUS = p.p.WORK_STATUS,
                             .WORK_STATUS_NAME = p.o.NAME_VN,
                             .JOIN_DATE = p.p.JOIN_DATE,
                             .JOIN_DATE_STATE = p.p.JOIN_DATE_STATE,
                             .LAST_WORKING_ID = p.p.LAST_WORKING_ID,
                             .CONTRACT_NO = p.p.HU_CONTRACT_NOW.CONTRACT_NO,
                             .ID_NO = p.pv.ID_NO,
                             .GHI_CHU_SUC_KHOE = p.health.GHI_CHU_SUC_KHOE,
                             .NHOM_MAU = p.health.NHOM_MAU,
                             .IMAGE = p.pv.IMAGE,
                             .ITIME_ID = p.p.ITIME_ID,
                             .MODIFIED_DATE = p.p.MODIFIED_DATE,
                             .PARENT_ID = p.ot.PARENT_ID
                             })
            ',            .PARENT_ID = p.ot.PARENT_ID
            If _filter.TITLE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.TITLE_ID = _filter.TITLE_ID)
            End If
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                lst = lst.Where(Function(p) Not p.WORK_STATUS.HasValue Or
                                    (p.WORK_STATUS.HasValue And
                                     ((p.WORK_STATUS <> terID) Or (p.WORK_STATUS = terID And p.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper().IndexOf(_filter.EMPLOYEE_CODE.ToUpper) >= 0)
            End If

            If _filter.FULLNAME_VN <> "" Then
                lst = lst.Where(Function(p) p.FULLNAME_VN.ToUpper().IndexOf(_filter.FULLNAME_VN.ToUpper) >= 0)
            End If

            If _filter.TITLE_NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME_VN.ToUpper().IndexOf(_filter.TITLE_NAME_VN.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper().IndexOf(_filter.ORG_NAME.ToUpper) >= 0)
            End If

            If _filter.STAFF_RANK_NAME <> "" Then
                lst = lst.Where(Function(p) p.STAFF_RANK_NAME.ToUpper().IndexOf(_filter.STAFF_RANK_NAME.ToUpper) >= 0)
            End If

            If _filter.GHI_CHU_SUC_KHOE <> "" Then
                lst = lst.Where(Function(p) p.GHI_CHU_SUC_KHOE.ToUpper().IndexOf(_filter.GHI_CHU_SUC_KHOE.ToUpper) >= 0)
            End If

            If _filter.JOIN_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE = _filter.JOIN_DATE)
            End If

            If _filter.JOIN_DATE_STATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE_STATE = _filter.JOIN_DATE_STATE)
            End If

            If _filter.FROM_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE >= _filter.FROM_DATE)
            End If

            If _filter.TO_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.JOIN_DATE <= _filter.TO_DATE)
            End If

            If _filter.WORK_STATUS_NAME <> "" Then
                lst = lst.Where(Function(p) p.WORK_STATUS_NAME.ToUpper().IndexOf(_filter.WORK_STATUS_NAME.ToUpper) >= 0)
            End If

            If _filter.ID_NO <> "" Then
                lst = lst.Where(Function(p) p.ID_NO.ToUpper().IndexOf(_filter.ID_NO.ToUpper) >= 0)
            End If

            If _filter.DIRECT_MANAGER IsNot Nothing Then
                lst = lst.Where(Function(p) p.DIRECT_MANAGER = _filter.DIRECT_MANAGER)
            End If

            If _filter.EMPLOYEE_ID IsNot Nothing Then
                lst = lst.Where(Function(p) p.ID = _filter.EMPLOYEE_ID)
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Dim lstEmp = lst.ToList

            For Each emp In lstEmp
                emp.IMAGE_BINARY = GetEmployeeImage(emp.ID, "", False, emp.IMAGE)
            Next
            Return lstEmp

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetListEmployeeChart(ByVal _filter As EmployeeDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        Try
            Dim fileDirectory = ""
            fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim wstt = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            Dim query = From p In Context.HU_EMPLOYEE
                        From pv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From s In Context.HU_STAFF_RANK.Where(Function(F) p.STAFF_RANK_ID = F.ID).DefaultIfEmpty
                        From health In Context.HU_EMPLOYEE_HEALTH.Where(Function(f) f.EMPLOYEE_ID = p.ID).DefaultIfEmpty
                        From o In Context.OT_OTHER_LIST.Where(Function(f) p.WORK_STATUS = f.ID).DefaultIfEmpty
                        From org In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From k In Context.SE_CHOSEN_ORG.Where(Function(f) p.ORG_ID = f.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                        From c In Context.HU_CONTRACT.Where(Function(c) c.ID = p.CONTRACT_ID).DefaultIfEmpty
                        From ct In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                        Order By p.EMPLOYEE_CODE

            Dim lst = query.Select(Function(p) New EmployeeDTO With {
                             .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                             .ID = p.p.ID,
                             .EMPLOYEE_ID = p.p.ID,
                             .DIRECT_MANAGER = p.p.DIRECT_MANAGER,
                             .FULLNAME_VN = p.p.FULLNAME_VN,
                             .FULLNAME_EN = p.p.FULLNAME_EN,
                             .ORG_ID = p.p.ORG_ID,
                             .ORG_NAME = p.org.NAME_VN,
                             .ORG_DESC = p.org.DESCRIPTION_PATH,
                             .TITLE_ID = p.p.TITLE_ID,
                             .TITLE_NAME_VN = p.title.NAME_VN,
                             .STAFF_RANK_ID = p.p.STAFF_RANK_ID,
                             .STAFF_RANK_NAME = p.s.NAME,
                             .CONTRACT_TYPE_ID = p.ct.ID,
                             .CONTRACT_TYPE_NAME = p.ct.NAME,
                             .TER_EFFECT_DATE = p.p.TER_EFFECT_DATE,
                             .WORK_STATUS = p.p.WORK_STATUS,
                             .WORK_STATUS_NAME = p.o.NAME_VN,
                             .JOIN_DATE = p.p.JOIN_DATE,
                             .JOIN_DATE_STATE = p.p.JOIN_DATE_STATE,
                             .LAST_WORKING_ID = p.p.LAST_WORKING_ID,
                             .CONTRACT_NO = p.p.HU_CONTRACT_NOW.CONTRACT_NO,
                             .ID_NO = p.pv.ID_NO,
                             .GHI_CHU_SUC_KHOE = p.health.GHI_CHU_SUC_KHOE,
                             .IMAGE = fileDirectory & "\" & If(p.pv.IMAGE.Trim().Length > 0, p.pv.IMAGE, "\NoImage.jpg"),
                             .ITIME_ID = p.p.ITIME_ID,
                             .MODIFIED_DATE = p.p.MODIFIED_DATE})
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy thông tin nhân viên từ EmployeeCode
    ''' </summary>
    ''' <param name="sEmployeeCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeByEmployeeIDPortal(ByVal empID As Decimal) As EmployeeDTO

        Try
            Dim str As String = "Kiêm nhiệm"
            If empID = 0 Then Return Nothing
            Dim shortName = "HĐCLTĐ"
            Dim query As New EmployeeDTO
            query =
                (From e In Context.HU_EMPLOYEE
                 From cmo In (From commitee In Context.HU_COMMITEE.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                              From co In Context.HU_ORGANIZATION.Where(Function(f) f.ID = commitee.ORG_ID).DefaultIfEmpty
                              Where co.UY_BAN = -1 And co.SHORT_NAME = shortName Select co).DefaultIfEmpty
                 From cmt In (From commitee In Context.HU_COMMITEE.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                              From co In Context.HU_ORGANIZATION.Where(Function(f) f.ID = commitee.ORG_ID).DefaultIfEmpty
                              From ct In Context.HU_TITLE.Where(Function(f) f.ID = commitee.TITLE_ID).DefaultIfEmpty
                              Where co.UY_BAN = -1 And co.SHORT_NAME = shortName Select ct).DefaultIfEmpty
                 From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                 From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                 From direct_title In Context.HU_TITLE.Where(Function(f) f.ID = title.LM).DefaultIfEmpty
                 From direct In Context.HU_EMPLOYEE.Where(Function(f) f.ID = direct_title.MASTER).DefaultIfEmpty
                 From level In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e.LEVEL_MANAGER).DefaultIfEmpty
                 From obj In Context.PA_OBJECT_SALARY.Where(Function(f) f.ID = e.PA_OBJECT_SALARY_ID).DefaultIfEmpty
                 From staffRank In Context.HU_JOB_BAND.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                 From c In Context.HU_CONTRACT.Where(Function(c) c.ID = e.CONTRACT_ID).DefaultIfEmpty
                 From org In Context.HU_ORGANIZATION.Where(Function(t) t.ID = e.ORG_ID).DefaultIfEmpty
                 From t In Context.HU_CONTRACT_TYPE.Where(Function(t) t.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                 From ins_info In Context.INS_INFORMATION.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                 From ce In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.OBJECTTIMEKEEPING).DefaultIfEmpty
                 From workstatus In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.WORK_STATUS And
                                                                    f.TYPE_ID = 59).DefaultIfEmpty
                 From empstatus In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.EMP_STATUS And
                                                                    f.TYPE_ID = 2235).DefaultIfEmpty
                 From objectLabor In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.OBJECT_LABOR And
                                                                    f.TYPE_ID = 6963).DefaultIfEmpty
                 From obj_emp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.OBJECT_EMPLOYEE_ID And f.TYPE_CODE = "OBJECT_EMPLOYEE").DefaultIfEmpty
                 From obj_place In Context.HU_WORK_PLACE.Where(Function(f) f.ID = e.WORK_PLACE_ID).DefaultIfEmpty
                 From obj_Atendant In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.OBJECT_ATTENDANT_ID And f.TYPE_CODE = "OBJECT_ATTENDANT").DefaultIfEmpty
                 From ur In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = org.ID).DefaultIfEmpty
                 From loc In Context.HU_LOCATION.Where(Function(f) f.ID = e.CONTRACTED_UNIT).DefaultIfEmpty
                 Where (e.ID = empID)
                 Select New EmployeeDTO With {
                     .ID = e.ID,
                     .FIRST_NAME_EN = e.FIRST_NAME_EN,
                     .FIRST_NAME_VN = e.FIRST_NAME_VN,
                     .LAST_NAME_EN = e.LAST_NAME_EN,
                     .LAST_NAME_VN = e.LAST_NAME_VN,
                     .FULLNAME_EN = e.FULLNAME_EN,
                     .HOLDING_CODE = e.HOLDING_CODE,
                     .OBJECTTIMEKEEPING = e.OBJECTTIMEKEEPING,
                     .OBJECTTIMEKEEPING_NAME = ce.NAME_VN,
                     .FULLNAME_VN = e.FULLNAME_VN,
                     .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                     .SENIORITY_DATE = e.SENIORITY_DATE,
                     .EMPLOYEE_CODE_OLD = e.EMPLOYEE_CODE_OLD,
                     .BOOKNO = ins_info.SOCIAL_NUMBER,
                     .HEALTH_NUMBER = ins_info.HEALTH_NUMBER,
                     .EMPLOYEE_NAME_OTHER = e.EMPLOYEE_NAME_OTHER,
                     .IMAGE = e.HU_EMPLOYEE_CV.IMAGE,
                     .TITLE_ID = e.TITLE_ID,
                     .LAST_WORKING_ID = e.LAST_WORKING_ID,
                     .TITLE_NAME_EN = title.NAME_EN,
                     .TITLE_NAME_VN = If(cmt IsNot Nothing, cmt.CODE + " - " + cmt.NAME_VN, title.CODE + " - " + title.NAME_VN),
                     .ORG_ID = If(cmo IsNot Nothing, cmo.ID, org.ID),
                     .ORG_NAME = If(cmo IsNot Nothing, cmo.NAME_VN, org.NAME_VN),
                     .ORG_DESC = If(cmo IsNot Nothing, cmo.DESCRIPTION_PATH, org.DESCRIPTION_PATH),
                     .CONTRACT_ID = e.CONTRACT_ID,
                     .WORK_STATUS = e.WORK_STATUS,
                     .WORK_STATUS_NAME = workstatus.NAME_VN,
                     .EMP_STATUS = e.EMP_STATUS,
                     .EMP_STATUS_NAME = If(e.IS_KIEM_NHIEM IsNot Nothing, str, empstatus.NAME_VN),
                     .DIRECT_MANAGER = e.DIRECT_MANAGER,
                     .DIRECT_MANAGER_TITLE_NAME = direct_title.CODE + " - " + direct_title.NAME_VN,
                     .DIRECT_MANAGER_NAME = direct.EMPLOYEE_CODE + " - " + direct.FULLNAME_VN,
                     .LEVEL_MANAGER = e.LEVEL_MANAGER,
                     .LEVEL_MANAGER_NAME = level.FULLNAME_VN,
                     .JOIN_DATE = e.JOIN_DATE,
                     .JOIN_DATE_STATE = e.JOIN_DATE_STATE,
                     .CONTRACT_TYPE_NAME = t.NAME_VISIBLE_ONFORM,
                     .CONTRACT_NO = e.HU_CONTRACT_NOW.CONTRACT_NO,
                     .CONTRACT_EFFECT_DATE = e.HU_CONTRACT_NOW.START_DATE,
                     .CONTRACT_EXPIRE_DATE = e.HU_CONTRACT_NOW.EXPIRE_DATE,
                     .PA_OBJECT_SALARY_ID = e.PA_OBJECT_SALARY_ID,
                     .PA_OBJECT_SALARY_NAME = obj.NAME_VN,
                     .STAFF_RANK_ID = e.STAFF_RANK_ID,
                     .STAFF_RANK_NAME = staffRank.LEVEL_FROM,
                     .ITIME_ID = e.ITIME_ID,
                     .TER_EFFECT_DATE = e.TER_EFFECT_DATE,
                     .OBJECT_LABOR = e.OBJECT_LABOR,
                     .OBJECT_LABOR_NAME = objectLabor.NAME_VN,
                     .OBJECT_EMPLOYEE_ID = e.OBJECT_EMPLOYEE_ID,
                     .WORK_PLACE_ID = e.WORK_PLACE_ID,
                     .OBJECT_ATTENDANT_ID = e.OBJECT_ATTENDANT_ID,
                     .OBJECT_EMPLOYEE_NAME = obj_emp.NAME_VN,
                     .WORK_PLACE_NAME = obj_place.NAME_VN,
                     .BOOK_NO_SOCIAL = e.BOOK_NO,
                     .OBJECT_ATTENDANT_NAME = obj_Atendant.NAME_VN,
                     .ORG_ID_2 = ur.ORG_ID2,
                     .ORG_NAME_2 = ur.ORG_NAME2,
                     .ORG_ID_3 = ur.ORG_ID3,
                     .ORG_NAME_3 = ur.ORG_NAME3,
                     .ORG_ID_4 = ur.ORG_ID4,
                     .ORG_NAME_4 = ur.ORG_NAME4,
                     .WORK_EMAIL = cv.WORK_EMAIL,
                     .MATHE = e.MATHE,
                     .COPORATION_DATE = e.COPORATION_DATE,
                     .CONTRACTED_UNIT = e.CONTRACTED_UNIT,
                     .FOREIGN = e.FOREIGN,
                     .CONTRACTED_UNIT_NAME = loc.LOCATION_SHORT_NAME & " - " & loc.NAME_VN
                 }).FirstOrDefault
            '???
            'From terminate In Context.HU_TERMINATE.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty

            'WriteExceptionLog(Nothing, "Getmployee1", "iProfile")
            'Dim emp As New EmployeeDTO

            ' WriteExceptionLog(Nothing, "Getmployee2", "iProfile")
            If (query IsNot Nothing) Then
                'emp.lstPaper = (From p In Context.HU_EMPLOYEE_PAPER
                '                Where p.EMPLOYEE_ID = emp.ID
                '                Select p.HU_PAPER_ID.Value).ToList
                'emp.lstPaperFiled = (From p In Context.HU_EMPLOYEE_PAPER_FILED
                '                     Where p.EMPLOYEE_ID = emp.ID
                '                     Select p.HU_PAPER_ID.Value).ToList
                query.THAMNIEN = CalculateSeniority(query.JOIN_DATE)
            End If
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetEmployeeByEmployeeID(ByVal empID As Decimal) As EmployeeDTO

        Try
            Dim str As String = "Kiêm nhiệm"
            If empID = 0 Then Return Nothing
            Dim shortName = "HĐCLTĐ"
            Dim query As New EmployeeDTO
            query =
                (From e In Context.HU_EMPLOYEE
                 From cmo In (From commitee In Context.HU_COMMITEE.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                              From co In Context.HU_ORGANIZATION.Where(Function(f) f.ID = commitee.ORG_ID).DefaultIfEmpty
                              Where co.UY_BAN = -1 And co.SHORT_NAME = shortName Select co).DefaultIfEmpty
                 From cmt In (From commitee In Context.HU_COMMITEE.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                              From co In Context.HU_ORGANIZATION.Where(Function(f) f.ID = commitee.ORG_ID).DefaultIfEmpty
                              From ct In Context.HU_TITLE.Where(Function(f) f.ID = commitee.TITLE_ID).DefaultIfEmpty
                              Where co.UY_BAN = -1 And co.SHORT_NAME = shortName Select ct).DefaultIfEmpty
                 From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                 From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                 From direct_title In Context.HU_TITLE.Where(Function(f) f.ID = title.LM).DefaultIfEmpty
                 From direct In Context.HU_EMPLOYEE.Where(Function(f) f.ID = direct_title.MASTER).DefaultIfEmpty
                 From level In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e.LEVEL_MANAGER).DefaultIfEmpty
                 From obj In Context.PA_OBJECT_SALARY.Where(Function(f) f.ID = e.PA_OBJECT_SALARY_ID).DefaultIfEmpty
                 From staffRank In Context.HU_JOB_BAND.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                 From c In Context.HU_CONTRACT.Where(Function(c) c.ID = e.CONTRACT_ID).DefaultIfEmpty
                 From org In Context.HU_ORGANIZATION.Where(Function(t) t.ID = e.ORG_ID).DefaultIfEmpty
                 From t In Context.HU_CONTRACT_TYPE.Where(Function(t) t.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                 From ins_info In Context.INS_INFORMATION.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                 From ce In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.OBJECTTIMEKEEPING).DefaultIfEmpty
                 From workstatus In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.WORK_STATUS And
                                                                    f.TYPE_ID = 59).DefaultIfEmpty
                 From empstatus In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.EMP_STATUS And
                                                                    f.TYPE_ID = 2235).DefaultIfEmpty
                 From objectLabor In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.OBJECT_LABOR And
                                                                    f.TYPE_ID = 6963).DefaultIfEmpty
                 From obj_emp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.OBJECT_EMPLOYEE_ID And f.TYPE_CODE = "OBJECT_EMPLOYEE").DefaultIfEmpty
                 From obj_place In Context.HU_WORK_PLACE.Where(Function(f) f.ID = e.WORK_PLACE_ID).DefaultIfEmpty
                 From obj_Atendant In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.OBJECT_ATTENDANT_ID And f.TYPE_CODE = "OBJECT_ATTENDANT").DefaultIfEmpty
                 From ur In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = org.ID).DefaultIfEmpty
                 From loc In Context.HU_LOCATION.Where(Function(f) f.ID = e.CONTRACTED_UNIT).DefaultIfEmpty
                 Where (e.ID = empID)
                 Select New EmployeeDTO With {
                     .ID = e.ID,
                     .FIRST_NAME_EN = e.FIRST_NAME_EN,
                     .FIRST_NAME_VN = e.FIRST_NAME_VN,
                     .LAST_NAME_EN = e.LAST_NAME_EN,
                     .LAST_NAME_VN = e.LAST_NAME_VN,
                     .FULLNAME_EN = e.FULLNAME_EN,
                     .HOLDING_CODE = e.HOLDING_CODE,
                     .OBJECTTIMEKEEPING = e.OBJECTTIMEKEEPING,
                     .OBJECTTIMEKEEPING_NAME = ce.NAME_VN,
                     .FULLNAME_VN = e.FULLNAME_VN,
                     .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                     .SENIORITY_DATE = e.SENIORITY_DATE,
                     .EMPLOYEE_CODE_OLD = e.EMPLOYEE_CODE_OLD,
                     .BOOKNO = ins_info.SOCIAL_NUMBER,
                     .HEALTH_NUMBER = ins_info.HEALTH_NUMBER,
                     .EMPLOYEE_NAME_OTHER = e.EMPLOYEE_NAME_OTHER,
                     .IMAGE = e.HU_EMPLOYEE_CV.IMAGE,
                     .TITLE_ID = e.TITLE_ID,
                     .LAST_WORKING_ID = e.LAST_WORKING_ID,
                     .TITLE_NAME_EN = title.NAME_EN,
                     .TITLE_NAME_VN = title.CODE + " - " + title.NAME_VN,
                     .ORG_ID = org.ID,
                     .ORG_NAME = org.NAME_VN,
                     .ORG_DESC = org.DESCRIPTION_PATH,
                     .CONTRACT_ID = e.CONTRACT_ID,
                     .WORK_STATUS = e.WORK_STATUS,
                     .WORK_STATUS_NAME = workstatus.NAME_VN,
                     .EMP_STATUS = e.EMP_STATUS,
                     .EMP_STATUS_NAME = If(e.IS_KIEM_NHIEM IsNot Nothing, str, empstatus.NAME_VN),
                     .DIRECT_MANAGER = e.DIRECT_MANAGER,
                     .DIRECT_MANAGER_TITLE_NAME = direct_title.CODE + " - " + direct_title.NAME_VN,
                     .DIRECT_MANAGER_NAME = direct.EMPLOYEE_CODE + " - " + direct.FULLNAME_VN,
                     .LEVEL_MANAGER = e.LEVEL_MANAGER,
                     .LEVEL_MANAGER_NAME = level.FULLNAME_VN,
                     .JOIN_DATE = e.JOIN_DATE,
                     .JOIN_DATE_STATE = e.JOIN_DATE_STATE,
                     .CONTRACT_TYPE_NAME = t.NAME_VISIBLE_ONFORM,
                     .CONTRACT_NO = e.HU_CONTRACT_NOW.CONTRACT_NO,
                     .CONTRACT_EFFECT_DATE = e.HU_CONTRACT_NOW.START_DATE,
                     .CONTRACT_EXPIRE_DATE = e.HU_CONTRACT_NOW.EXPIRE_DATE,
                     .PA_OBJECT_SALARY_ID = e.PA_OBJECT_SALARY_ID,
                     .PA_OBJECT_SALARY_NAME = obj.NAME_VN,
                     .STAFF_RANK_ID = e.STAFF_RANK_ID,
                     .STAFF_RANK_NAME = staffRank.LEVEL_FROM,
                     .ITIME_ID = e.ITIME_ID,
                     .TER_EFFECT_DATE = e.TER_EFFECT_DATE,
                     .OBJECT_LABOR = e.OBJECT_LABOR,
                     .OBJECT_LABOR_NAME = objectLabor.NAME_VN,
                     .OBJECT_EMPLOYEE_ID = e.OBJECT_EMPLOYEE_ID,
                     .WORK_PLACE_ID = e.WORK_PLACE_ID,
                     .OBJECT_ATTENDANT_ID = e.OBJECT_ATTENDANT_ID,
                     .OBJECT_EMPLOYEE_NAME = obj_emp.NAME_VN,
                     .WORK_PLACE_NAME = obj_place.NAME_VN,
                     .BOOK_NO_SOCIAL = e.BOOK_NO,
                     .OBJECT_ATTENDANT_NAME = obj_Atendant.NAME_VN,
                     .ORG_ID_2 = ur.ORG_ID2,
                     .ORG_NAME_2 = ur.ORG_NAME2,
                     .ORG_ID_3 = ur.ORG_ID3,
                     .ORG_NAME_3 = ur.ORG_NAME3,
                     .ORG_ID_4 = ur.ORG_ID4,
                     .ORG_NAME_4 = ur.ORG_NAME4,
                     .WORK_EMAIL = cv.WORK_EMAIL,
                     .MATHE = e.MATHE,
                     .COPORATION_DATE = e.COPORATION_DATE,
                     .CONTRACTED_UNIT = e.CONTRACTED_UNIT,
                     .FOREIGN = e.FOREIGN,
                     .CONTRACTED_UNIT_NAME = loc.LOCATION_SHORT_NAME & " - " & loc.NAME_VN
                 }).FirstOrDefault
            '???
            'From terminate In Context.HU_TERMINATE.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty

            'WriteExceptionLog(Nothing, "Getmployee1", "iProfile")
            'Dim emp As New EmployeeDTO

            ' WriteExceptionLog(Nothing, "Getmployee2", "iProfile")
            If (query IsNot Nothing) Then
                'emp.lstPaper = (From p In Context.HU_EMPLOYEE_PAPER
                '                Where p.EMPLOYEE_ID = emp.ID
                '                Select p.HU_PAPER_ID.Value).ToList
                'emp.lstPaperFiled = (From p In Context.HU_EMPLOYEE_PAPER_FILED
                '                     Where p.EMPLOYEE_ID = emp.ID
                '                     Select p.HU_PAPER_ID.Value).ToList
                query.THAMNIEN = CalculateSeniority(query.JOIN_DATE)
            End If
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CalculateSeniority(ByVal dStart As Date?) As String
        Dim dSoNam As Double = 0
        Dim dSoThang As Double = 0
        Dim Cal_Month_Emp As Int32 = 0
        Dim Total_Month As Decimal = 0
        Dim str As String = ""
        Try
            If IsDate(dStart) Then

                Cal_Month_Emp = (DateDiff(DateInterval.Month, CDate(dStart), CDate(Date.Now.Date)))

                Total_Month = Math.Round(Cal_Month_Emp, 2)
                If IsNumeric(Total_Month) Then
                    dSoNam = Total_Month \ 12
                    dSoThang = Math.Round(CDec(Total_Month) Mod 12, 2)
                    str = If(dSoNam > 0, dSoNam.ToString + " Năm ", "") + If(Math.Round(CDec(dSoThang) Mod 12, 2) > 0, Math.Round(CDec(dSoThang) Mod 12, 2).ToString + " Tháng", "")
                End If
            End If
            Return str
        Catch ex As Exception
            Return ""
        End Try
    End Function

    ''' <summary>
    ''' Hàm đọc ảnh hồ sơ của nhân viên thành binary
    ''' </summary>
    ''' <param name="gEmpID"></param>
    ''' <param name="sError"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeImage(ByVal gEmpID As Decimal, ByRef sError As String,
                                     Optional ByVal isOneEmployee As Boolean = True,
                                     Optional ByVal img_link As String = "") As Byte()
        Try


            Dim sEmployeeImage As String = ""
            If Not isOneEmployee Then
                sEmployeeImage = img_link
            Else
                sEmployeeImage = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = gEmpID
                                  Select p.IMAGE).FirstOrDefault
            End If

            Dim _imageBinary() As Byte = Nothing
            Dim fileDirectory = ""
            Dim filepath = ""
            Dim filepathDefault = ""
            fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
            Dim _fileInfo As IO.FileInfo
            If sEmployeeImage IsNot Nothing AndAlso sEmployeeImage.Trim().Length > 0 Then
                filepath = fileDirectory & "\" & sEmployeeImage
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeImageEdit(ByVal gEmpID As Decimal, ByVal Status As String) As Byte()
        Try
            Dim filepath = ""
            Dim _fileInfo As IO.FileInfo
            Dim _imageBinary() As Byte = Nothing

            If Status = "1" Or Status = "0" Then
                Dim userFiles = (From p In Context.HU_EMPLOYEE_EDIT
                                 From u In Context.HU_USERFILES.Where(Function(f) f.NAME = p.IMAGE).DefaultIfEmpty
                                 Where p.EMPLOYEE_ID = gEmpID And p.STATUS = Status
                                 Select New FileUploadDTO With {
                                    .LINK = u.LINK,
                                    .FILE_NAME = u.FILE_NAME
                                }).FirstOrDefault
                filepath = AppDomain.CurrentDomain.BaseDirectory & "\" & userFiles.LINK & userFiles.FILE_NAME
            End If

            If Status = "-1" Or Status = "2" Or Status = "3" Then
                Dim empCV = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = gEmpID
                             Select New EmployeeCVDTO With {
                                 .IMAGE = p.IMAGE
                            }).FirstOrDefault
                filepath = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage\" & empCV.IMAGE
            End If

            If File.Exists(filepath) Then
                _fileInfo = New FileInfo(filepath)
            Else
                _fileInfo = New FileInfo(AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage\NoImage.jpg")
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Hàm đọc ảnh hồ sơ của nhân viên thành binary
    ''' </summary>
    ''' <param name="gEmpID"></param>
    ''' <param name="sError"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EmployeeImage(ByVal userId As Decimal, ByRef sError As String,
                                     Optional ByVal isOneEmployee As Boolean = True,
                                     Optional ByVal img_link As String = "") As Byte()
        Try
            Dim employeeId As String = (From p In Context.SE_USER Where p.ID = userId Select p.EMPLOYEE_ID).FirstOrDefault()

            Dim sEmployeeImage As String = ""
            If Not isOneEmployee Then
                sEmployeeImage = img_link
            Else
                sEmployeeImage = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = employeeId
                                  Select p.IMAGE).FirstOrDefault
            End If

            Dim _imageBinary() As Byte = Nothing
            Dim fileDirectory = ""
            Dim filepath = ""
            Dim filepathDefault = ""

            'fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
            fileDirectory = (From P In Context.SE_CONFIG Where P.CODE.ToUpper = "IMAGE_EMP" Select P.VALUE).FirstOrDefault

            Dim _fileInfo As IO.FileInfo
            If sEmployeeImage IsNot Nothing AndAlso sEmployeeImage.Trim().Length > 0 Then
                filepath = fileDirectory & "\" & sEmployeeImage
            Else
                filepath = fileDirectory & "\NoImage.jpg"
            End If
            filepathDefault = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage" & "\NoImage.jpg"
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
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            sError = ex.ToString
        End Try
    End Function
    ''' <summary>
    ''' Hàm lấy đường dẫn ảnh HSNV để in CV trên portal
    ''' <creater>TUNGLD</creater>
    ''' </summary>
    ''' <param name="gEmpID"></param>
    ''' <param name="isOneEmployee"></param>
    ''' <param name="img_link"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeImage_PrintCV(ByVal gEmpID As Decimal,
                                             Optional ByVal isOneEmployee As Boolean = True,
                                             Optional ByVal img_link As String = "") As String
        Try
            Dim sEmployeeImage As String = ""
            If Not isOneEmployee Then
                sEmployeeImage = img_link
            Else
                sEmployeeImage = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = gEmpID
                                  Select p.IMAGE).FirstOrDefault
            End If

            Dim _imageBinary() As Byte = Nothing
            Dim fileDirectory = ""
            Dim filepath = ""
            fileDirectory = AppDomain.CurrentDomain.BaseDirectory & "EmployeeImage"
            If sEmployeeImage IsNot Nothing AndAlso sEmployeeImage.Trim().Length > 0 Then
                filepath = fileDirectory & "\" & sEmployeeImage
            Else
                filepath = fileDirectory & "\NoImage.jpg"
            End If
            Return filepath
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ' AUTO MÃ NHÂN VIÊN
    Public Function CreateNewEMPLOYEECode() As EmployeeDTO
        Dim objEmpData As New HU_EMPLOYEE
        Dim empData As New EmployeeDTO
        ' thêm kỷ luật
        Dim fileID As Decimal = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE.EntitySet.Name)
        'SaveCandidate(fileID, 222)

        'Sinh mã ứng viên động
        Dim checkEMP As Integer = 0
        Dim empCodeDB As Decimal = 0
        Dim EMPCODE As String

        Using query As New DataAccess.NonQueryData
            Dim temp = query.ExecuteSQLScalar("select EMPLOYEE_CODE from HU_EMPLOYEE " &
                                   "order by EMPLOYEE_CODE DESC",
                                   New Object)
            If temp IsNot Nothing Then
                empCodeDB = Decimal.Parse(temp)
            End If
        End Using
        Do
            empCodeDB += 1
            EMPCODE = String.Format("{0}", Format(empCodeDB, "000000"))
            checkEMP = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = EMPCODE Select p.ID).Count
        Loop Until checkEMP = 0

        Return (New EmployeeDTO With {.ID = fileID, .EMPLOYEE_CODE = EMPCODE})

    End Function

    ''' <summary>
    ''' Thêm mới nhân viên
    ''' </summary>
    ''' <param name="objEmp"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <param name="objEmpCV">Thông tin bảng HU_EMPLOYEE_CV</param>
    ''' <param name="objEmpSalary">Thông tin bảng HU_EMPLOYEE_SALARY</param>
    ''' <param name="objEmpEdu">Thông tin bảng HU_EMPLOYEE_EDUCATION</param>
    ''' <param name="objEmpOther">Thông tin bảng HU_EMPLOYEE_OTHER_INFO</param>
    ''' <param name="objEmpHealth">Thông tin bảng HU_EMPLOYEE_HEALTH</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertEmployee(ByVal objEmp As EmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal,
                                        ByRef _strEmpCode As String,
                                        ByVal _imageBinary As Byte(),
                                        Optional ByVal objEmpCV As EmployeeCVDTO = Nothing,
                                        Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing,
                                        Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing) As Boolean

        Try
            'Thông tin insert vào bảng HU_EMPLOYEE.
            Dim objEmpData As New HU_EMPLOYEE
            Dim EMPCODE As String = String.Empty
            'If EMPCODE.Length = 4 Then
            '    objEmpData.ITIME_ID = EMPCODE.Substring(1)
            'End If

            objEmpData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE.EntitySet.Name)
            'Sinh mã nhân viên động
            Dim empCodeDB As Double = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE.Length = 5
                                       Order By p.EMPLOYEE_CODE Descending Select p.EMPLOYEE_CODE).FirstOrDefault
            Dim checkEMP As Integer = 0


            Do
                empCodeDB += 1
                EMPCODE = String.Format("{0}", Format(empCodeDB, "00000"))
                checkEMP = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = EMPCODE Select p.ID).Count
            Loop Until checkEMP = 0

            'Sinh mã nv tự động theo giá trị se_case_config
            Dim strFormat As String = String.Empty
            Dim valueFormat As Integer = 0
            Using cls As New DataAccess.QueryData
                Dim obj = New With {.P_CODENAME = "ctrlHUAutoCreateEmpCode",
                                           .P_CODECASE = "ctrlHUAutoCreateEmpCode",
                                           .P_OUT = cls.OUT_NUMBER}
                cls.ExecuteStore("PKG_COMMON_LIST.GET_VALUE_CASE_CONFIG", obj)
                valueFormat = Integer.Parse(obj.P_OUT)
            End Using
            For i As Int16 = 0 To valueFormat - 1
                strFormat += "0"
            Next
            '=============================
            'update master, inter vi tri cong viec
            'hu_title
            'kiem tra title_id co nguoi ngoi master chua?
            Dim objTitle_check = (From p In Context.HU_TITLE Where p.ID = objEmp.TITLE_ID).FirstOrDefault()
            If objTitle_check IsNot Nothing Then
                If IsNumeric(objTitle_check.INTERIM) Then
                    objTitle_check.INTERIM = objTitle_check.INTERIM
                Else
                    objTitle_check.INTERIM = objTitle_check.MASTER

                End If
                objTitle_check.MASTER = objEmpData.ID
            End If
            '=============================
            Dim strITIME_ID = ""
            If objEmp.ITIME_ID <> "" Then
                strITIME_ID = objEmp.ITIME_ID
            Else
                strITIME_ID = empCodeDB.ToString
            End If
            objEmpData.HOLDING_CODE = objEmp.HOLDING_CODE
            objEmpData.EMPLOYEE_CODE = EMPCODE
            _strEmpCode = EMPCODE
            objEmpData.ITIME_ID = strITIME_ID
            objEmpData.EMPLOYEE_CODE_OLD = objEmp.EMPLOYEE_CODE_OLD
            objEmpData.EMPLOYEE_NAME_OTHER = objEmp.EMPLOYEE_NAME_OTHER
            objEmpData.EMPLOYEE_CODE_OLD = objEmp.EMPLOYEE_CODE_OLD
            objEmpData.BOOK_NO = objEmp.BOOKNO
            objEmpData.FIRST_NAME_VN = Trim(objEmp.FIRST_NAME_VN).Replace("  ", " ").Replace("   ", " ")
            objEmpData.LAST_NAME_VN = Trim(objEmp.LAST_NAME_VN).Replace("  ", " ").Replace("   ", " ")
            objEmpData.FIRST_NAME_EN = Trim(objEmp.FIRST_NAME_EN).Replace("  ", " ").Replace("   ", " ")
            objEmpData.LAST_NAME_EN = Trim(objEmp.LAST_NAME_EN).Replace("  ", " ").Replace("   ", " ")
            objEmpData.FULLNAME_EN = Trim(objEmpData.FIRST_NAME_EN & " " & objEmpData.LAST_NAME_EN).Replace("  ", " ").Replace("   ", " ")
            objEmpData.FULLNAME_VN = Trim(objEmpData.FIRST_NAME_VN & " " & objEmpData.LAST_NAME_VN).Replace("  ", " ").Replace("   ", " ")
            objEmpData.ORG_ID = objEmp.ORG_ID
            objEmpData.WORK_STATUS = objEmp.WORK_STATUS
            objEmpData.TITLE_ID = objEmp.TITLE_ID
            objEmpData.DIRECT_MANAGER = objEmp.DIRECT_MANAGER
            objEmpData.LEVEL_MANAGER = objEmp.LEVEL_MANAGER
            objEmpData.STAFF_RANK_ID = objEmp.STAFF_RANK_ID
            objEmpData.OBJECTTIMEKEEPING = objEmp.OBJECTTIMEKEEPING
            objEmpData.CONTRACTED_UNIT = objEmp.CONTRACTED_UNIT
            'objEmpData.PA_OBJECT_SALARY_ID = 1
            objEmpData.OBJECT_LABOR = objEmp.OBJECT_LABOR
            objEmpData.FOREIGN = objEmp.FOREIGN

            'objEmpData.ITIME_ID = empCodeDB
            'anhvn
            objEmpData.OBJECT_EMPLOYEE_ID = objEmp.OBJECT_EMPLOYEE_ID
            objEmpData.WORK_PLACE_ID = objEmp.WORK_PLACE_ID
            objEmpData.OBJECT_ATTENDANT_ID = objEmp.OBJECT_ATTENDANT_ID
            objEmpData.MATHE = objEmp.MATHE
            objEmpData.COPORATION_DATE = objEmp.COPORATION_DATE
            If objEmpData.COPORATION_DATE IsNot Nothing Then
                objEmpData.SENIORITY_DATE = objEmpData.COPORATION_DATE
            End If
            objEmpData.DM_ID = objEmp.DM_ID
            objEmpData.IDM_ID = objEmp.IDM_ID
            Context.HU_EMPLOYEE.AddObject(objEmpData)
            If objEmp.MATHE.HasValue Then
                Dim objThe = (From p In Context.HU_TITLE_BLD Where p.ID = objEmp.MATHE).FirstOrDefault
                If objThe IsNot Nothing Then
                    objThe.EMPLOYEE_ID = objEmpData.ID
                End If
            End If
            'End Thông tin insert vào bảng HU_EMPLOYEE.

            ' Insert bảng HU_EMPLOYEE_PAPER

            If objEmp.lstPaper IsNot Nothing AndAlso objEmp.lstPaper.Count > 0 Then
                For Each i In objEmp.lstPaper
                    Dim objEmpPaperData As New HU_EMPLOYEE_PAPER
                    objEmpPaperData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE_PAPER.EntitySet.Name)
                    objEmpPaperData.HU_PAPER_ID = i
                    objEmpPaperData.EMPLOYEE_ID = objEmpData.ID
                    Context.HU_EMPLOYEE_PAPER.AddObject(objEmpPaperData)
                Next
            End If
            If objEmp.lstPaperFiled IsNot Nothing AndAlso objEmp.lstPaperFiled.Count > 0 Then
                For Each i In objEmp.lstPaperFiled
                    Dim objEmpPaperData As New HU_EMPLOYEE_PAPER_FILED
                    objEmpPaperData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE_PAPER_FILED.EntitySet.Name)
                    objEmpPaperData.HU_PAPER_ID = i
                    objEmpPaperData.EMPLOYEE_ID = objEmpData.ID
                    Context.HU_EMPLOYEE_PAPER_FILED.AddObject(objEmpPaperData)
                Next
            End If
            'Start thông tin insert vào bảng HU_EMPLOYEE_CV
            Dim objEmpCVData As New HU_EMPLOYEE_CV

            If objEmpCV IsNot Nothing Then
                objEmpCVData.EMPLOYEE_ID = objEmpData.ID 'Khóa ngoại vừa mới tạo 
                objEmpCVData.GENDER = objEmpCV.GENDER
                If objEmpCV.IMAGE <> "" Then
                    objEmpCVData.IMAGE = objEmp.EMPLOYEE_CODE & objEmpCV.IMAGE 'Lưu Image thành dạng E10012.jpg.                    
                End If
                objEmpCVData.BIRTH_PLACE = objEmpCV.BIRTH_PLACE
                objEmpCVData.EXPIRE_DATE_IDNO = objEmpCV.EXPIRE_DATE_IDNO
                objEmpCVData.PIT_CODE_DATE = objEmpCV.PIT_CODE_DATE
                objEmpCVData.PIT_CODE_PLACE = objEmpCV.PIT_CODE_PLACE
                objEmpCVData.EFFECTDATE_BANK = objEmpCV.EFFECTDATE_BANK
                objEmpCVData.PERSON_INHERITANCE = objEmpCV.PERSON_INHERITANCE
                objEmpCVData.BIRTH_DATE = objEmpCV.BIRTH_DATE
                objEmpCVData.VILLAGE = objEmpCV.VILLAGE
                objEmpCVData.CONTACT_PER_MBPHONE = objEmpCV.CONTACT_PER_MBPHONE
                objEmpCVData.BIRTH_PLACE = objEmpCV.BIRTH_PLACE
                objEmpCVData.MARITAL_STATUS = objEmpCV.MARITAL_STATUS
                objEmpCVData.RELIGION = objEmpCV.RELIGION
                objEmpCVData.NATIVE = objEmpCV.NATIVE
                objEmpCVData.NATIONALITY = objEmpCV.NATIONALITY
                objEmpCVData.PER_ADDRESS = objEmpCV.PER_ADDRESS
                objEmpCVData.PER_PROVINCE = objEmpCV.PER_PROVINCE
                objEmpCVData.PER_DISTRICT = objEmpCV.PER_DISTRICT
                'objEmpCVData.WORKPLACE_ID = objEmpCV.WORKPLACE_ID
                objEmpCVData.INS_REGION_ID = objEmpCV.INS_REGION_ID
                objEmpCVData.PER_WARD = objEmpCV.PER_WARD
                objEmpCVData.HOME_PHONE = objEmpCV.HOME_PHONE
                objEmpCVData.MOBILE_PHONE = objEmpCV.MOBILE_PHONE
                objEmpCVData.ID_NO = objEmpCV.ID_NO

                objEmpCVData.ID_DATE = objEmpCV.ID_DATE
                objEmpCVData.ID_PLACE = objEmpCV.ID_PLACE
                objEmpCVData.ID_REMARK = objEmpCV.ID_REMARK
                objEmpCVData.PASS_NO = objEmpCV.PASS_NO
                objEmpCVData.PASS_DATE = objEmpCV.PASS_DATE
                objEmpCVData.PASS_EXPIRE = objEmpCV.PASS_EXPIRE
                objEmpCVData.PASS_PLACE = objEmpCV.PASS_PLACE
                objEmpCVData.VISA = objEmpCV.VISA
                objEmpCVData.VISA_DATE = objEmpCV.VISA_DATE
                objEmpCVData.VISA_EXPIRE = objEmpCV.VISA_EXPIRE
                objEmpCVData.VISA_PLACE = objEmpCV.VISA_PLACE
                objEmpCVData.WORK_PERMIT = objEmpCV.WORK_PERMIT
                objEmpCVData.WORK_PERMIT_DATE = objEmpCV.WORK_PERMIT_DATE
                objEmpCVData.WORK_PERMIT_EXPIRE = objEmpCV.WORK_PERMIT_EXPIRE
                objEmpCVData.WORK_PERMIT_PLACE = objEmpCV.WORK_PERMIT_PLACE
                objEmpCVData.WORK_EMAIL = objEmpCV.WORK_EMAIL
                objEmpCVData.NAV_ADDRESS = objEmpCV.NAV_ADDRESS
                objEmpCVData.NAV_PROVINCE = objEmpCV.NAV_PROVINCE
                objEmpCVData.NAV_DISTRICT = objEmpCV.NAV_DISTRICT
                objEmpCVData.NAV_WARD = objEmpCV.NAV_WARD
                objEmpCVData.PIT_CODE = objEmpCV.PIT_CODE
                objEmpCVData.PER_EMAIL = objEmpCV.PER_EMAIL
                objEmpCVData.CONTACT_PER = objEmpCV.CONTACT_PER
                objEmpCVData.CONTACT_PER_PHONE = objEmpCV.CONTACT_PER_PHONE
                objEmpCVData.CAREER = objEmpCV.CAREER
                objEmpCVData.NGAY_VAO_DOAN = objEmpCV.NGAY_VAO_DOAN
                objEmpCVData.NOI_VAO_DOAN = objEmpCV.NOI_VAO_DOAN
                objEmpCVData.CHUC_VU_DOAN = objEmpCV.CHUC_VU_DOAN
                objEmpCVData.DOAN_PHI = objEmpCV.DOAN_PHI
                objEmpCVData.NGAY_VAO_DANG = objEmpCV.NGAY_VAO_DANG
                objEmpCVData.NOI_VAO_DANG = objEmpCV.NOI_VAO_DANG
                objEmpCVData.CHUC_VU_DANG = objEmpCV.CHUC_VU_DANG
                objEmpCVData.DANG_PHI = objEmpCV.DANG_PHI
                objEmpCVData.BANK_ID = objEmpCV.BANK_ID
                objEmpCVData.BANK_BRANCH_ID = objEmpCV.BANK_BRANCH_ID
                objEmpCVData.BANK_NO = objEmpCV.BANK_NO
                objEmpCVData.IS_PERMISSION = objEmpCV.IS_PERMISSION
                objEmpCVData.IS_PAY_BANK = objEmpCV.IS_PAY_BANK
                '-----------------------------------------------
                objEmpCVData.PROVINCENQ_ID = objEmpCV.PROVINCENQ_ID
                objEmpCVData.OPPTION1 = objEmpCV.OPPTION1
                objEmpCVData.OPPTION2 = objEmpCV.OPPTION2
                objEmpCVData.OPPTION3 = objEmpCV.OPPTION3
                objEmpCVData.OPPTION4 = objEmpCV.OPPTION4
                objEmpCVData.OPPTION5 = objEmpCV.OPPTION5
                objEmpCVData.OPPTION6 = objEmpCV.OPPTION6
                objEmpCVData.OPPTION7 = objEmpCV.OPPTION7
                objEmpCVData.OPPTION8 = objEmpCV.OPPTION8
                objEmpCVData.OPPTION9 = objEmpCV.OPPTION9
                objEmpCVData.OPPTION10 = objEmpCV.OPPTION10
                objEmpCVData.PROVINCEEMP_ID = objEmpCV.PROVINCEEMP_ID
                objEmpCVData.DISTRICTEMP_ID = objEmpCV.DISTRICTEMP_ID
                objEmpCVData.WARDEMP_ID = objEmpCV.WARDEMP_ID

                objEmpCVData.HANG_THUONG_BINH = objEmpCV.HANG_THUONG_BINH
                objEmpCVData.THUONG_BINH = objEmpCV.THUONG_BINH
                objEmpCVData.DV_XUAT_NGU_QD = objEmpCV.DV_XUAT_NGU_QD
                objEmpCVData.NGAY_XUAT_NGU_QD = objEmpCV.NGAY_XUAT_NGU_QD
                objEmpCVData.NGAY_NHAP_NGU_QD = objEmpCV.NGAY_NHAP_NGU_QD
                objEmpCVData.QD = objEmpCV.QD
                objEmpCVData.DV_XUAT_NGU_CA = objEmpCV.DV_XUAT_NGU_CA
                objEmpCVData.NGAY_XUAT_NGU_CA = objEmpCV.NGAY_XUAT_NGU_CA
                objEmpCVData.NGAY_NHAP_NGU_CA = objEmpCV.NGAY_NHAP_NGU_CA
                objEmpCVData.NGAY_TG_BAN_NU_CONG = objEmpCV.NGAY_TG_BAN_NU_CONG
                objEmpCVData.CV_BAN_NU_CONG = objEmpCV.CV_BAN_NU_CONG
                objEmpCVData.NU_CONG = objEmpCV.NU_CONG
                objEmpCVData.NGAY_TG_BANTT = objEmpCV.NGAY_TG_BANTT
                objEmpCVData.CV_BANTT = objEmpCV.CV_BANTT
                objEmpCVData.BANTT = objEmpCV.BANTT
                objEmpCVData.CONG_DOAN = objEmpCV.CONG_DOAN
                objEmpCVData.CA = objEmpCV.CA
                objEmpCVData.DANG = objEmpCV.DANG
                objEmpCVData.SKILL = objEmpCV.SKILL
                objEmpCVData.NGAY_VAO_DANG_DB = objEmpCV.NGAY_VAO_DANG_DB
                objEmpCVData.NGAY_VAO_DANG = objEmpCV.NGAY_VAO_DANG
                objEmpCVData.CHUC_VU_DANG = objEmpCV.CHUC_VU_DANG
                objEmpCVData.DOAN_PHI = objEmpCV.DOAN_PHI
                objEmpCVData.CHUC_VU_DOAN = objEmpCV.CHUC_VU_DOAN
                objEmpCVData.NGAY_VAO_DOAN = objEmpCV.NGAY_VAO_DOAN
                objEmpCVData.GD_CHINH_SACH = objEmpCV.GD_CHINH_SACH
                objEmpCVData.WORKPLACE_NAME = objEmpCV.WORKPLACE_NAME
                objEmpCVData.HEALTH_NO = objEmpCV.HEALTH_NO
                objEmpCVData.NGAY_VAO_DTN = objEmpCV.NGAY_VAO_DTN
                objEmpCVData.NOI_VAO_DTN = objEmpCV.NOI_VAO_DTN
                objEmpCVData.CHUC_VU_DTN = objEmpCV.CHUC_VU_DTN
                objEmpCVData.TD_CHINHTRI = objEmpCV.TD_CHINHTRI
                objEmpCVData.CBO_SINHHOAT = objEmpCV.CBO_SINHHOAT
                objEmpCVData.SO_LYLICH = objEmpCV.SO_LYLICH
                objEmpCVData.SOTHE_DANG = objEmpCV.SOTHE_DANG

                objEmpCVData.PROVINCEEMP_BRITH = objEmpCV.PROVINCEEMP_BRITH
                objEmpCVData.DISTRICTEMP_BRITH = objEmpCV.DISTRICTEMP_BRITH
                objEmpCVData.WARDEMP_BRITH = objEmpCV.WARDEMP_BRITH
                objEmpCVData.OBJECT_INS = objEmpCV.OBJECT_INS
                objEmpCVData.IS_CHUHO = objEmpCV.IS_CHUHO
                objEmpCVData.NO_HOUSEHOLDS = objEmpCV.NO_HOUSEHOLDS
                objEmpCVData.CODE_HOUSEHOLDS = objEmpCV.CODE_HOUSEHOLDS
                objEmpCVData.RELATION_PER_CTR = objEmpCV.RELATION_PER_CTR
                objEmpCVData.ADDRESS_PER_CTR = objEmpCV.ADDRESS_PER_CTR

                objEmpCVData.FILENAME = objEmpCV.FILENAME
                objEmpCVData.ATTACH_FILE = objEmpCV.ATTACH_FILE

                objEmpCVData.OTHER_GENDER = objEmpCV.OTHER_GENDER
                objEmpCVData.BIRTH_PLACE_DETAIL = objEmpCV.BIRTH_PLACE_DETAIL
                objEmpCVData.COPY_ADDRESS = objEmpCV.COPY_ADDRESS
                objEmpCVData.CHECK_NAV = objEmpCV.CHECK_NAV
                objEmpCVData.BOOK_NO = objEmpCV.BOOK_NO
                objEmpCVData.BOOK_DATE = objEmpCV.BOOK_DATE
                objEmpCVData.BOOK_EXPIRE = objEmpCV.BOOK_EXPIRE
                objEmpCVData.SSLD_PLACE_ID = objEmpCV.SSLD_PLACE_ID

                objEmpCVData.PASS_PLACE_ID = objEmpCV.PASS_PLACE_ID
                objEmpCVData.VISA_PLACE_ID = objEmpCV.VISA_PLACE_ID

                objEmpCVData.HEALTH_AREA_INS_ID = objEmpCV.HEALTH_AREA_INS_ID

                objEmpCVData.CONTACT_PER_IDNO = objEmpCV.CONTACT_PER_IDNO
                objEmpCVData.CONTACT_PER_EFFECT_DATE_IDNO = objEmpCV.CONTACT_PER_EFFECT_DATE_IDNO
                objEmpCVData.CONTACT_PER_EXPIRE_DATE_IDNO = objEmpCV.CONTACT_PER_EXPIRE_DATE_IDNO
                objEmpCVData.CONTACT_PER_PLACE_IDNO = objEmpCV.CONTACT_PER_PLACE_IDNO
                objEmpCVData.PIT_ID_PLACE = objEmpCV.PIT_ID_PLACE
                objEmpCVData.RELATE_OWNER = objEmpCV.RELATE_OWNER

                '-----------------------------------------------

                Context.HU_EMPLOYEE_CV.AddObject(objEmpCVData)

            End If

            'End thông tin insert vào bảng HU_EMPLOYEE_CV

            'Start thông tin insert vào bảng HU_EMPLOYEE_EDUCATION
            If objEmpEdu IsNot Nothing Then
                Dim objEmpEduData As New HU_EMPLOYEE_EDUCATION
                objEmpEduData.EMPLOYEE_ID = objEmpData.ID
                objEmpEduData.ACADEMY = objEmpEdu.ACADEMY
                objEmpEduData.MAJOR = objEmpEdu.MAJOR
                objEmpEduData.MAJOR_REMARK = objEmpEdu.MAJOR_REMARK
                objEmpEduData.LANGUAGE = objEmpEdu.LANGUAGE
                objEmpEduData.LANGUAGE_LEVEL = objEmpEdu.LANGUAGE_LEVEL
                objEmpEduData.LANGUAGE_MARK = objEmpEdu.LANGUAGE_MARK
                objEmpEduData.GRADUATE_SCHOOL_ID = objEmpEdu.GRADUATE_SCHOOL_ID
                objEmpEduData.TRAINING_FORM = objEmpEdu.TRAINING_FORM
                objEmpEduData.LEARNING_LEVEL = objEmpEdu.LEARNING_LEVEL
                objEmpEduData.GRADUATION_YEAR = objEmpEdu.GRADUATION_YEAR
                objEmpEduData.QLNN = objEmpEdu.QLNN
                objEmpEduData.LLCT = objEmpEdu.LLCT
                objEmpEduData.TDTH = objEmpEdu.TDTH
                objEmpEduData.DIEM_XLTH = objEmpEdu.DIEM_XLTH
                objEmpEduData.NOTE_TDTH1 = objEmpEdu.NOTE_TDTH1

                objEmpEduData.LANGUAGE2 = objEmpEdu.LANGUAGE2
                objEmpEduData.LANGUAGE_LEVEL2 = objEmpEdu.LANGUAGE_LEVEL2
                objEmpEduData.LANGUAGE_MARK2 = objEmpEdu.LANGUAGE_MARK2

                objEmpEduData.TDTH2 = objEmpEdu.TDTH2
                objEmpEduData.DIEM_XLTH2 = objEmpEdu.DIEM_XLTH2
                objEmpEduData.NOTE_TDTH2 = objEmpEdu.NOTE_TDTH2
                objEmpEduData.COMPUTER_CERTIFICATE = objEmpEdu.COMPUTER_CERTIFICATE
                objEmpEduData.COMPUTER_MARK = objEmpEdu.COMPUTER_MARK
                objEmpEduData.COMPUTER_RANK = objEmpEdu.COMPUTER_RANK
                Context.HU_EMPLOYEE_EDUCATION.AddObject(objEmpEduData)
            End If


            'End thông tin insert vào bảng HU_EMPLOYEE_EDUCATION

            'Start thông tin insert vào bảng HU_EMPLOYEE_HEALTH
            If objEmpHealth IsNot Nothing Then
                Dim objEmpHealthData As New HU_EMPLOYEE_HEALTH
                objEmpHealthData.EMPLOYEE_ID = objEmpData.ID
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
                objEmpHealthData.TTSUCKHOE = objEmpHealth.TTSUCKHOE
                objEmpHealthData.LOAI_SUCKHOE = objEmpHealth.LOAI_SUCKHOE
                objEmpHealthData.NGAY_KHAM = objEmpHealth.NGAY_KHAM
                Context.HU_EMPLOYEE_HEALTH.AddObject(objEmpHealthData)
            End If

            'Ghi ảnh vào thư mục
            If _imageBinary IsNot Nothing AndAlso _imageBinary.Length > 0 Then
                Dim savepath = ""
                For _count As Integer = 0 To 1
                    If _count = 0 Then
                        savepath = objEmp.IMAGE_URL
                    Else
                        savepath = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
                    End If
                    If Not Directory.Exists(savepath) Then
                        Directory.CreateDirectory(savepath)
                    End If
                    'Xóa ảnh cũ của nhân viên.
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
                Next
            End If
            Context.SaveChanges(log)

            Dim user = (From p In Context.SE_USER Where p.EMPLOYEE_CODE = EMPCODE).FirstOrDefault
            If objEmpCV.ID_NO <> "" Then

                Dim acc_name = EMPCODE
                Dim acc_pass = objEmpCV.ID_NO

                If user Is Nothing Then
                    Dim _new As New SE_USER

                    Dim EncryptData As New EncryptData
                    _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER.EntitySet.Name)
                    _new.EFFECT_DATE = Date.Now
                    _new.EMPLOYEE_CODE = EMPCODE
                    _new.FULLNAME = objEmpData.FULLNAME_VN
                    _new.EMAIL = objEmpCVData.WORK_EMAIL
                    _new.TELEPHONE = objEmpCVData.MOBILE_PHONE
                    _new.IS_AD = False
                    _new.IS_APP = False
                    _new.IS_PORTAL = True
                    _new.IS_CHANGE_PASS = "-1"
                    _new.ACTFLG = "A"
                    _new.PASSWORD = EncryptData.EncryptString(acc_pass)
                    _new.EMPLOYEE_ID = objEmpData.ID
                    _new.USERNAME = acc_name 'dangnhap(0)

                    'INSERT USER
                    Insert_SE_User(_new, log)
                    Dim se_group = (From p In Context.SE_GROUP Where p.CODE = "PORTAL_USER").FirstOrDefault
                    If se_group IsNot Nothing Then
                        Insert_Permit(se_group.ID, _new.ID, log)
                    End If
                    SendMailCreateAccount(objEmpCVData.WORK_EMAIL, acc_name, acc_pass)
                End If
            End If

            'anhvn, 2020/07/16 auto thêm vào SE_GRP_SE_USR group portal user
            ' SE_GROUPS_ID = Portal User
            'Dim user_se = (From p In Context.SE_USER Where p.EMPLOYEE_CODE = EMPCODE).FirstOrDefault
            'Dim SE_GROUPID As Decimal
            'Dim USERSE As Decimal
            'If user_se IsNot Nothing Then
            '    Dim se_group = (From p In Context.SE_GROUP Where p.CODE = "PORTAL_USER").FirstOrDefault
            '    Dim user_grp = (From p In Context.SE_GRP_SE_USR Where p.SE_GROUPS_ID = se_group.ID And p.SE_USERS_ID = user_se.ID).FirstOrDefault
            '    If user_grp Is Nothing Then
            '        Dim _new_grp As New SE_GRP_SE_USR

            '        _new_grp.SE_GROUPS_ID = se_group.ID
            '        SE_GROUPID = se_group.ID
            '        _new_grp.SE_USERS_ID = user_se.ID
            '        USERSE = user_se.ID
            '        Context.SE_GRP_SE_USR.AddObject(_new_grp)

            '    End If
            'End If
            'Context.SaveChanges(log)
            'If user_se IsNot Nothing Then
            '    'them vao bang se_permit
            '    InsertUserGroup(SE_GROUPID, USERSE, log)
            'End If
            gID = objEmpData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function SendMailCreateAccount(ByVal _to As String, ByVal _username As String, ByVal _pass As String)
        Try
            Dim config = GetConfig(SystemConfig.ModuleID.iSecure)
            Dim emailFrom = If(config.ContainsKey("MailFrom"), config("MailFrom"), "")
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE.GET_TEMPLATE_MAIL",
                                                    New With {.P_CODE = "NEW_ACCOUNT",
                                                              .CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    Dim body = dtData.Rows(0)("CONTENT").ToString
                    body = String.Format(body, _username, _pass)
                    Dim _newMail As New SE_MAIL
                    _newMail.ID = Utilities.GetNextSequence(Context, Context.SE_MAIL.EntitySet.Name)
                    _newMail.MAIL_FROM = emailFrom
                    _newMail.MAIL_TO = _to
                    _newMail.MAIL_CC = If(dtData.Rows(0)("MAIL_CC").ToString <> "", dtData.Rows(0)("MAIL_CC").ToString, Nothing)
                    _newMail.SUBJECT = dtData.Rows(0)("TITLE").ToString
                    _newMail.CONTENT = body
                    _newMail.VIEW_NAME = "AUTO_CREATE_ACCOUNT"
                    _newMail.ACTFLG = "I"
                    Context.SE_MAIL.AddObject(_newMail)
                End If
            End Using
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Sửa thông tin nhân viên
    ''' </summary>
    ''' <param name="objEmp"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <param name="objEmpCV"></param>
    ''' <param name="objEmpEdu"></param>
    ''' <param name="objEmpOther"></param>
    ''' <param name="objEmpSalary"></param>
    ''' <param name="objEmpHealth"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ModifyEmployee(ByVal objEmp As EmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal,
                                        ByVal _imageBinary As Byte(),
                                        Optional ByVal objEmpCV As EmployeeCVDTO = Nothing,
                                        Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing,
                                        Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing) As Boolean

        Try
            Dim dateChange = DateTime.Now
            Using conMng As New ConnectionManager
                Using conn As New OracleConnection(conMng.GetConnectionString())
                    Using cmd As New OracleCommand()
                        Try
                            'Open connection
                            cmd.Connection = conn
                            cmd.CommandType = CommandType.StoredProcedure
                            conn.Open()
                            cmd.Transaction = cmd.Connection.BeginTransaction()
                            If objEmp.IS_HISTORY Then
                                cmd.CommandText = "PKG_PROFILE_BUSINESS.INSERT_EMP_HISTORY"

                                Dim obj = New With {.P_EMPLOYEE_ID = objEmp.ID,
                                                    .P_USER_BY = log.Username,
                                                    .P_USER_LOG = log.Ip & "-" & log.ComputerName,
                                                    .P_DATE_CHANGE = dateChange,
                                                    .P_TYPE_CHANGE = 1}

                                'Add parameter
                                If obj IsNot Nothing Then
                                    Dim idx As Integer = 0
                                    For Each info As PropertyInfo In obj.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim oraCom As New OracleCommon
                                        Dim para = oraCom.GetParameter(info.Name, info.GetValue(obj, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                            idx += 1
                                        End If
                                    Next
                                End If

                                cmd.ExecuteNonQuery()
                            End If

                            ModifyEmployeeByLinq(objEmp, log, gID, _imageBinary, objEmpCV, objEmpEdu, objEmpHealth)

                            If objEmp.IS_HISTORY Then
                                cmd.Parameters.Clear()
                                Dim obj = New With {.P_EMPLOYEE_ID = objEmp.ID,
                                                    .P_USER_BY = log.Username,
                                                    .P_USER_LOG = log.Ip & "-" & log.ComputerName,
                                                    .P_DATE_CHANGE = dateChange,
                                                    .P_TYPE_CHANGE = 2}
                                If obj IsNot Nothing Then
                                    Dim idx As Integer = 0
                                    For Each info As PropertyInfo In obj.GetType().GetProperties()
                                        Dim bOut As Boolean = False
                                        Dim oraCom As New OracleCommon
                                        Dim para = oraCom.GetParameter(info.Name, info.GetValue(obj, Nothing), bOut)
                                        If para IsNot Nothing Then
                                            cmd.Parameters.Add(para)
                                            idx += 1
                                        End If
                                    Next
                                End If
                                cmd.ExecuteNonQuery()
                            End If
                            cmd.Transaction.Commit()


                            'Dispose all resource
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()

                        Catch ex As Exception
                            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                            cmd.Transaction.Rollback()
                            cmd.Dispose()
                            conn.Close()
                            conn.Dispose()
                            Throw ex
                        End Try
                    End Using
                End Using
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

        Return True
    End Function
    ''' <summary>
    ''' anhvn, 2020/09/16
    ''' thêm vào bảng se_permit
    ''' </summary>
    ''' <param name="_groupID"></param>
    ''' <param name="_lstUserID"></param>
    ''' <param name="log"></param>
    ''' <returns></returns>
    Public Function InsertUserGroup(ByVal _groupID As Decimal, ByVal _lstUserID As Decimal, ByVal log As UserLog) As Boolean

        Using conMng As New ConnectionManager
            Using conn As New OracleConnection(conMng.GetConnectionString())
                conn.Open()
                Dim dem As Integer

                Using cmd As New OracleCommand()
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    Try
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PKG_COMMON_BUSINESS.TRANSFER_GROUP_TO_USER"
                        cmd.Parameters.Clear()
                        Using resource As New DataAccess.OracleCommon
                            Dim objParam = New With {.P_USER_ID = _lstUserID,
                                                         .P_GROUP_ID = _groupID,
                                                         .P_USERNAME = log.Username}
                            If objParam IsNot Nothing Then
                                For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                    Dim bOut As Boolean = False
                                    Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                    If para IsNot Nothing Then
                                        cmd.Parameters.Add(para)
                                    End If
                                Next
                            End If
                            dem = cmd.ExecuteNonQuery()
                        End Using
                        cmd.Transaction.Commit()
                    Catch ex As Exception
                        cmd.Transaction.Rollback()
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
    End Function

    Private Sub ModifyEmployeeByLinq(ByVal objEmp As EmployeeDTO, ByVal log As UserLog, ByRef gID As Decimal,
                                        ByVal _imageBinary As Byte(),
                                        Optional ByVal objEmpCV As EmployeeCVDTO = Nothing,
                                        Optional ByVal objEmpEdu As EmployeeEduDTO = Nothing,
                                        Optional ByVal objEmpHealth As EmployeeHealthDTO = Nothing)
        Try
            Dim objEmpData As New HU_EMPLOYEE With {.ID = objEmp.ID}
            '----Start modify HU_EMPLOYEE---

            objEmpData = (From p In Context.HU_EMPLOYEE Where p.ID = objEmp.ID).FirstOrDefault
            '----------------------------------------------
            If objEmpData.MATHE.HasValue Then
                Dim objTheOld = (From p In Context.HU_TITLE_BLD Where p.ID = objEmpData.MATHE).FirstOrDefault
                If objTheOld IsNot Nothing Then
                    objTheOld.EMPLOYEE_ID = Nothing
                End If
            End If

            If objEmp.MATHE.HasValue Then
                Dim objTheNew = (From p In Context.HU_TITLE_BLD Where p.ID = objEmp.MATHE).FirstOrDefault
                If objTheNew IsNot Nothing Then
                    objTheNew.EMPLOYEE_ID = objEmpData.ID
                End If
            End If

            objEmpData.EMPLOYEE_NAME_OTHER = objEmp.EMPLOYEE_NAME_OTHER
            objEmpData.EMPLOYEE_CODE_OLD = objEmp.EMPLOYEE_CODE_OLD
            objEmpData.HOLDING_CODE = objEmp.HOLDING_CODE
            objEmpData.BOOK_NO = objEmp.BOOKNO
            objEmpData.CONTRACTED_UNIT = objEmp.CONTRACTED_UNIT
            objEmpData.OBJECTTIMEKEEPING = objEmp.OBJECTTIMEKEEPING
            objEmpData.FOREIGN = objEmp.FOREIGN
            '==============================================
            objEmpData.EMPLOYEE_CODE = objEmp.EMPLOYEE_CODE
            objEmpData.FIRST_NAME_VN = Trim(objEmp.FIRST_NAME_VN).Replace("  ", " ").Replace("   ", " ")
            objEmpData.LAST_NAME_VN = Trim(objEmp.LAST_NAME_VN).Replace("  ", " ").Replace("   ", " ")
            objEmpData.FIRST_NAME_EN = Trim(objEmp.FIRST_NAME_EN).Replace("  ", " ").Replace("   ", " ")
            objEmpData.LAST_NAME_EN = Trim(objEmp.LAST_NAME_EN).Replace("  ", " ").Replace("   ", " ")
            objEmpData.FULLNAME_EN = Trim(objEmpData.FIRST_NAME_EN & " " & objEmpData.LAST_NAME_EN).Replace("  ", " ").Replace("   ", " ")
            objEmpData.FULLNAME_VN = Trim(objEmpData.FIRST_NAME_VN & " " & objEmpData.LAST_NAME_VN).Replace("  ", " ").Replace("   ", " ")
            objEmpData.ORG_ID = objEmp.ORG_ID
            objEmpData.WORK_STATUS = objEmp.WORK_STATUS
            objEmpData.TITLE_ID = objEmp.TITLE_ID
            objEmpData.DIRECT_MANAGER = objEmp.DIRECT_MANAGER
            objEmpData.LEVEL_MANAGER = objEmp.LEVEL_MANAGER
            objEmpData.STAFF_RANK_ID = objEmp.STAFF_RANK_ID
            objEmpData.ITIME_ID = CDec(objEmp.ITIME_ID)
            objEmpData.PA_OBJECT_SALARY_ID = 1 'objEmp.PA_OBJECT_SALARY_ID

            objEmpData.OBJECT_LABOR = objEmp.OBJECT_LABOR

            objEmpData.OBJECT_EMPLOYEE_ID = objEmp.OBJECT_EMPLOYEE_ID
            objEmpData.WORK_PLACE_ID = objEmp.WORK_PLACE_ID
            objEmpData.OBJECT_ATTENDANT_ID = objEmp.OBJECT_ATTENDANT_ID
            objEmpData.MATHE = objEmp.MATHE
            objEmpData.COPORATION_DATE = objEmp.COPORATION_DATE
            If objEmpData.COPORATION_DATE IsNot Nothing Then
                objEmpData.SENIORITY_DATE = objEmpData.COPORATION_DATE
            End If
            objEmpData.DM_ID = objEmp.DM_ID
            objEmpData.IDM_ID = objEmp.IDM_ID

            Dim lstPaperDelete = (From p In Context.HU_EMPLOYEE_PAPER Where p.EMPLOYEE_ID = objEmpData.ID).ToList
            For Each item In lstPaperDelete
                Context.HU_EMPLOYEE_PAPER.DeleteObject(item)
            Next
            If objEmp.lstPaper IsNot Nothing AndAlso objEmp.lstPaper.Count > 0 Then
                For Each i In objEmp.lstPaper
                    Dim objEmpPaperData As New HU_EMPLOYEE_PAPER
                    objEmpPaperData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE_PAPER.EntitySet.Name)
                    objEmpPaperData.HU_PAPER_ID = i
                    objEmpPaperData.EMPLOYEE_ID = objEmpData.ID
                    Context.HU_EMPLOYEE_PAPER.AddObject(objEmpPaperData)
                Next
            End If

            Dim lstPaperFiledDelete = (From p In Context.HU_EMPLOYEE_PAPER_FILED Where p.EMPLOYEE_ID = objEmpData.ID).ToList
            For Each item In lstPaperFiledDelete
                Context.HU_EMPLOYEE_PAPER_FILED.DeleteObject(item)
            Next
            If objEmp.lstPaperFiled IsNot Nothing AndAlso objEmp.lstPaperFiled.Count > 0 Then
                For Each i In objEmp.lstPaperFiled
                    Dim objEmpPaperData As New HU_EMPLOYEE_PAPER_FILED
                    objEmpPaperData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE_PAPER_FILED.EntitySet.Name)
                    objEmpPaperData.HU_PAPER_ID = i
                    objEmpPaperData.EMPLOYEE_ID = objEmpData.ID
                    Context.HU_EMPLOYEE_PAPER_FILED.AddObject(objEmpPaperData)
                Next
            End If
            'End Thông tin modify bảng HU_EMPLOYEE.
            Dim bUpdateCV As Boolean
            'Start thông tin modify vào bảng HU_EMPLOYEE_CV
            Dim objEmpCVData As HU_EMPLOYEE_CV
            If objEmpCV IsNot Nothing Then
                bUpdateCV = False
                objEmpCVData = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = objEmp.ID).SingleOrDefault
                If objEmpCVData Is Nothing Then 'Them moi
                    objEmpCVData = New HU_EMPLOYEE_CV With {.EMPLOYEE_ID = objEmp.ID}
                Else 'Update
                    bUpdateCV = True
                End If
                If objEmpCV.IMAGE <> "" Then
                    objEmpCVData.IMAGE = objEmp.EMPLOYEE_CODE & objEmpCV.IMAGE 'Lưu Image thành dạng E10012.jpg.                    
                End If
                objEmpCVData.BIRTH_PLACE = objEmpCV.BIRTH_PLACE
                objEmpCVData.EXPIRE_DATE_IDNO = objEmpCV.EXPIRE_DATE_IDNO
                objEmpCVData.EFFECTDATE_BANK = objEmpCV.EFFECTDATE_BANK
                objEmpCVData.PERSON_INHERITANCE = objEmpCV.PERSON_INHERITANCE
                objEmpCVData.PIT_CODE_DATE = objEmpCV.PIT_CODE_DATE
                objEmpCVData.PIT_CODE_PLACE = objEmpCV.PIT_CODE_PLACE
                objEmpCVData.GENDER = objEmpCV.GENDER
                objEmpCVData.VILLAGE = objEmpCV.VILLAGE
                objEmpCVData.CONTACT_PER_MBPHONE = objEmpCV.CONTACT_PER_MBPHONE
                objEmpCVData.BIRTH_DATE = objEmpCV.BIRTH_DATE
                objEmpCVData.BIRTH_PLACE = objEmpCV.BIRTH_PLACE
                objEmpCVData.MARITAL_STATUS = objEmpCV.MARITAL_STATUS
                objEmpCVData.RELIGION = objEmpCV.RELIGION
                objEmpCVData.NATIVE = objEmpCV.NATIVE
                objEmpCVData.NATIONALITY = objEmpCV.NATIONALITY
                objEmpCVData.PER_ADDRESS = objEmpCV.PER_ADDRESS
                objEmpCVData.PER_PROVINCE = objEmpCV.PER_PROVINCE
                objEmpCVData.PER_DISTRICT = objEmpCV.PER_DISTRICT
                'objEmpCVData.WORKPLACE_ID = objEmpCV.WORKPLACE_ID
                objEmpCVData.INS_REGION_ID = objEmpCV.INS_REGION_ID
                objEmpCVData.PER_WARD = objEmpCV.PER_WARD
                objEmpCVData.HOME_PHONE = objEmpCV.HOME_PHONE
                objEmpCVData.MOBILE_PHONE = objEmpCV.MOBILE_PHONE
                objEmpCVData.ID_NO = objEmpCV.ID_NO

                objEmpCVData.ID_DATE = objEmpCV.ID_DATE
                objEmpCVData.ID_PLACE = objEmpCV.ID_PLACE
                objEmpCVData.ID_REMARK = objEmpCV.ID_REMARK
                objEmpCVData.PASS_NO = objEmpCV.PASS_NO
                objEmpCVData.PASS_DATE = objEmpCV.PASS_DATE
                objEmpCVData.PASS_EXPIRE = objEmpCV.PASS_EXPIRE
                objEmpCVData.PASS_PLACE = objEmpCV.PASS_PLACE
                objEmpCVData.VISA = objEmpCV.VISA
                objEmpCVData.VISA_DATE = objEmpCV.VISA_DATE
                objEmpCVData.VISA_EXPIRE = objEmpCV.VISA_EXPIRE
                objEmpCVData.VISA_PLACE = objEmpCV.VISA_PLACE
                objEmpCVData.WORK_PERMIT = objEmpCV.WORK_PERMIT
                objEmpCVData.WORK_PERMIT_DATE = objEmpCV.WORK_PERMIT_DATE
                objEmpCVData.WORK_PERMIT_EXPIRE = objEmpCV.WORK_PERMIT_EXPIRE
                objEmpCVData.WORK_PERMIT_PLACE = objEmpCV.WORK_PERMIT_PLACE
                objEmpCVData.WORK_EMAIL = objEmpCV.WORK_EMAIL
                Dim lstUser = (From p In Context.SE_USER Where p.EMPLOYEE_ID = objEmp.ID).ToList
                For Each item In lstUser
                    item.EMAIL = objEmpCV.WORK_EMAIL
                Next
                objEmpCVData.NAV_ADDRESS = objEmpCV.NAV_ADDRESS
                objEmpCVData.NAV_PROVINCE = objEmpCV.NAV_PROVINCE
                objEmpCVData.NAV_DISTRICT = objEmpCV.NAV_DISTRICT
                objEmpCVData.NAV_WARD = objEmpCV.NAV_WARD
                objEmpCVData.PIT_CODE = objEmpCV.PIT_CODE
                objEmpCVData.PER_EMAIL = objEmpCV.PER_EMAIL
                objEmpCVData.CONTACT_PER = objEmpCV.CONTACT_PER
                objEmpCVData.CONTACT_PER_PHONE = objEmpCV.CONTACT_PER_PHONE
                objEmpCVData.CAREER = objEmpCV.CAREER

                objEmpCVData.NGAY_VAO_DOAN = objEmpCV.NGAY_VAO_DOAN
                objEmpCVData.NOI_VAO_DOAN = objEmpCV.NOI_VAO_DOAN
                objEmpCVData.CHUC_VU_DOAN = objEmpCV.CHUC_VU_DOAN
                objEmpCVData.DOAN_PHI = objEmpCV.DOAN_PHI
                objEmpCVData.NGAY_VAO_DANG = objEmpCV.NGAY_VAO_DANG
                objEmpCVData.NOI_VAO_DANG = objEmpCV.NOI_VAO_DANG
                objEmpCVData.CHUC_VU_DANG = objEmpCV.CHUC_VU_DANG
                objEmpCVData.DANG_PHI = objEmpCV.DANG_PHI
                objEmpCVData.BANK_ID = objEmpCV.BANK_ID
                objEmpCVData.BANK_BRANCH_ID = objEmpCV.BANK_BRANCH_ID
                objEmpCVData.BANK_NO = objEmpCV.BANK_NO
                objEmpCVData.IS_PERMISSION = objEmpCV.IS_PERMISSION
                objEmpCVData.IS_PAY_BANK = objEmpCV.IS_PAY_BANK
                objEmpCVData.PROVINCENQ_ID = objEmpCV.PROVINCENQ_ID
                '-------------------------------------------------
                objEmpCVData.OPPTION1 = objEmpCV.OPPTION1
                objEmpCVData.OPPTION2 = objEmpCV.OPPTION2
                objEmpCVData.OPPTION3 = objEmpCV.OPPTION3
                objEmpCVData.OPPTION4 = objEmpCV.OPPTION4
                objEmpCVData.OPPTION5 = objEmpCV.OPPTION5
                objEmpCVData.OPPTION6 = objEmpCV.OPPTION6
                objEmpCVData.OPPTION7 = objEmpCV.OPPTION7
                objEmpCVData.OPPTION8 = objEmpCV.OPPTION8
                objEmpCVData.OPPTION9 = objEmpCV.OPPTION9
                objEmpCVData.OPPTION10 = objEmpCV.OPPTION10
                objEmpCVData.PROVINCEEMP_ID = objEmpCV.PROVINCEEMP_ID
                objEmpCVData.DISTRICTEMP_ID = objEmpCV.DISTRICTEMP_ID
                objEmpCVData.WARDEMP_ID = objEmpCV.WARDEMP_ID

                objEmpCVData.HANG_THUONG_BINH = objEmpCV.HANG_THUONG_BINH
                objEmpCVData.THUONG_BINH = objEmpCV.THUONG_BINH
                objEmpCVData.DV_XUAT_NGU_QD = objEmpCV.DV_XUAT_NGU_QD
                objEmpCVData.NGAY_XUAT_NGU_QD = objEmpCV.NGAY_XUAT_NGU_QD
                objEmpCVData.NGAY_NHAP_NGU_QD = objEmpCV.NGAY_NHAP_NGU_QD
                objEmpCVData.QD = objEmpCV.QD
                objEmpCVData.DV_XUAT_NGU_CA = objEmpCV.DV_XUAT_NGU_CA
                objEmpCVData.NGAY_XUAT_NGU_CA = objEmpCV.NGAY_XUAT_NGU_CA
                objEmpCVData.NGAY_NHAP_NGU_CA = objEmpCV.NGAY_NHAP_NGU_CA
                objEmpCVData.NGAY_TG_BAN_NU_CONG = objEmpCV.NGAY_TG_BAN_NU_CONG
                objEmpCVData.CV_BAN_NU_CONG = objEmpCV.CV_BAN_NU_CONG
                objEmpCVData.NU_CONG = objEmpCV.NU_CONG
                objEmpCVData.BANTT = objEmpCV.BANTT
                objEmpCVData.NGAY_TG_BANTT = objEmpCV.NGAY_TG_BANTT
                objEmpCVData.CV_BANTT = objEmpCV.CV_BANTT
                objEmpCVData.CONG_DOAN = objEmpCV.CONG_DOAN
                objEmpCVData.CA = objEmpCV.CA
                objEmpCVData.DANG = objEmpCV.DANG
                objEmpCVData.SKILL = objEmpCV.SKILL
                objEmpCVData.NGAY_VAO_DANG_DB = objEmpCV.NGAY_VAO_DANG_DB
                objEmpCVData.NGAY_VAO_DANG = objEmpCV.NGAY_VAO_DANG
                objEmpCVData.CHUC_VU_DANG = objEmpCV.CHUC_VU_DANG
                objEmpCVData.DOAN_PHI = objEmpCV.DOAN_PHI
                objEmpCVData.CHUC_VU_DOAN = objEmpCV.CHUC_VU_DOAN
                objEmpCVData.NGAY_VAO_DOAN = objEmpCV.NGAY_VAO_DOAN
                objEmpCVData.GD_CHINH_SACH = objEmpCV.GD_CHINH_SACH
                objEmpCVData.WORKPLACE_NAME = objEmpCV.WORKPLACE_NAME
                objEmpCVData.HEALTH_NO = objEmpCV.HEALTH_NO
                objEmpCVData.NGAY_VAO_DTN = objEmpCV.NGAY_VAO_DTN
                objEmpCVData.NOI_VAO_DTN = objEmpCV.NOI_VAO_DTN
                objEmpCVData.CHUC_VU_DTN = objEmpCV.CHUC_VU_DTN
                objEmpCVData.TD_CHINHTRI = objEmpCV.TD_CHINHTRI
                objEmpCVData.CBO_SINHHOAT = objEmpCV.CBO_SINHHOAT
                objEmpCVData.SO_LYLICH = objEmpCV.SO_LYLICH
                objEmpCVData.SOTHE_DANG = objEmpCV.SOTHE_DANG

                objEmpCVData.PROVINCEEMP_BRITH = objEmpCV.PROVINCEEMP_BRITH
                objEmpCVData.DISTRICTEMP_BRITH = objEmpCV.DISTRICTEMP_BRITH
                objEmpCVData.WARDEMP_BRITH = objEmpCV.WARDEMP_BRITH
                objEmpCVData.OBJECT_INS = objEmpCV.OBJECT_INS
                objEmpCVData.IS_CHUHO = objEmpCV.IS_CHUHO
                objEmpCVData.NO_HOUSEHOLDS = objEmpCV.NO_HOUSEHOLDS
                objEmpCVData.CODE_HOUSEHOLDS = objEmpCV.CODE_HOUSEHOLDS
                objEmpCVData.RELATION_PER_CTR = objEmpCV.RELATION_PER_CTR
                objEmpCVData.ADDRESS_PER_CTR = objEmpCV.ADDRESS_PER_CTR
                objEmpCVData.FILENAME = objEmpCV.FILENAME
                objEmpCVData.ATTACH_FILE = objEmpCV.ATTACH_FILE

                objEmpCVData.OTHER_GENDER = objEmpCV.OTHER_GENDER
                objEmpCVData.BIRTH_PLACE_DETAIL = objEmpCV.BIRTH_PLACE_DETAIL
                objEmpCVData.COPY_ADDRESS = objEmpCV.COPY_ADDRESS
                objEmpCVData.CHECK_NAV = objEmpCV.CHECK_NAV
                objEmpCVData.BOOK_NO = objEmpCV.BOOK_NO
                objEmpCVData.BOOK_DATE = objEmpCV.BOOK_DATE
                objEmpCVData.BOOK_EXPIRE = objEmpCV.BOOK_EXPIRE
                objEmpCVData.SSLD_PLACE_ID = objEmpCV.SSLD_PLACE_ID
                objEmpCVData.PASS_PLACE_ID = objEmpCV.PASS_PLACE_ID
                objEmpCVData.VISA_PLACE_ID = objEmpCV.VISA_PLACE_ID
                objEmpCVData.HEALTH_AREA_INS_ID = objEmpCV.HEALTH_AREA_INS_ID

                objEmpCVData.CONTACT_PER_IDNO = objEmpCV.CONTACT_PER_IDNO
                objEmpCVData.CONTACT_PER_EFFECT_DATE_IDNO = objEmpCV.CONTACT_PER_EFFECT_DATE_IDNO
                objEmpCVData.CONTACT_PER_EXPIRE_DATE_IDNO = objEmpCV.CONTACT_PER_EXPIRE_DATE_IDNO
                objEmpCVData.CONTACT_PER_PLACE_IDNO = objEmpCV.CONTACT_PER_PLACE_IDNO
                objEmpCVData.PIT_ID_PLACE = objEmpCV.PIT_ID_PLACE
                objEmpCVData.RELATE_OWNER = objEmpCV.RELATE_OWNER
                Dim objFamily = (From p In Context.HU_FAMILY Where p.EMPLOYEE_ID = objEmpCVData.EMPLOYEE_ID AndAlso p.IS_OWNER = -1).FirstOrDefault
                If objFamily IsNot Nothing Then
                    objFamily.CERTIFICATE_NUM = objEmpCV.NO_HOUSEHOLDS
                End If
                '------------------------------------------------
                If bUpdateCV = False Then
                    Context.HU_EMPLOYEE_CV.AddObject(objEmpCVData)
                End If
            End If
            'End thông tin insert vào bảng HU_EMPLOYEE_EDUCATION


            'Start thông tin modify vào bảng HU_EMPLOYEE_EDUCATION
            If objEmpEdu IsNot Nothing Then
                Dim bUpdateEdu As Boolean
                Dim objEmpEduData As HU_EMPLOYEE_EDUCATION
                bUpdateEdu = False
                objEmpEduData = (From p In Context.HU_EMPLOYEE_EDUCATION Where p.EMPLOYEE_ID = objEmp.ID).SingleOrDefault
                If objEmpEduData Is Nothing Then 'Them moi
                    objEmpEduData = New HU_EMPLOYEE_EDUCATION With {.EMPLOYEE_ID = objEmp.ID}
                Else 'Update
                    bUpdateEdu = True
                End If
                objEmpEduData.COMPUTER_CERTIFICATE = objEmpEdu.COMPUTER_CERTIFICATE
                objEmpEduData.COMPUTER_MARK = objEmpEdu.COMPUTER_MARK
                objEmpEduData.COMPUTER_RANK = objEmpEdu.COMPUTER_RANK
                objEmpEduData.ACADEMY = objEmpEdu.ACADEMY
                objEmpEduData.MAJOR = objEmpEdu.MAJOR
                objEmpEduData.MAJOR_REMARK = objEmpEdu.MAJOR_REMARK
                objEmpEduData.LANGUAGE = objEmpEdu.LANGUAGE
                objEmpEduData.LANGUAGE_LEVEL = objEmpEdu.LANGUAGE_LEVEL
                objEmpEduData.LANGUAGE_MARK = objEmpEdu.LANGUAGE_MARK
                objEmpEduData.GRADUATE_SCHOOL_ID = objEmpEdu.GRADUATE_SCHOOL_ID
                objEmpEduData.TRAINING_FORM = objEmpEdu.TRAINING_FORM
                objEmpEduData.LEARNING_LEVEL = objEmpEdu.LEARNING_LEVEL
                objEmpEduData.GRADUATION_YEAR = objEmpEdu.GRADUATION_YEAR
                objEmpEduData.QLNN = objEmpEdu.QLNN
                objEmpEduData.LLCT = objEmpEdu.LLCT
                objEmpEduData.TDTH = objEmpEdu.TDTH
                objEmpEduData.DIEM_XLTH = objEmpEdu.DIEM_XLTH
                objEmpEduData.NOTE_TDTH1 = objEmpEdu.NOTE_TDTH1

                objEmpEduData.LANGUAGE2 = objEmpEdu.LANGUAGE2
                objEmpEduData.LANGUAGE_LEVEL2 = objEmpEdu.LANGUAGE_LEVEL2
                objEmpEduData.LANGUAGE_MARK2 = objEmpEdu.LANGUAGE_MARK2

                objEmpEduData.TDTH2 = objEmpEdu.TDTH2
                objEmpEduData.DIEM_XLTH2 = objEmpEdu.DIEM_XLTH2
                objEmpEduData.NOTE_TDTH2 = objEmpEdu.NOTE_TDTH2

                If bUpdateEdu = False Then
                    Context.HU_EMPLOYEE_EDUCATION.AddObject(objEmpEduData)
                End If
            End If
            'End thông tin insert vào bảng HU_EMPLOYEE_EDUCATION

            'Start thông tin modify vào bảng HU_EMPLOYEE_HEALTH
            If objEmpHealth IsNot Nothing Then
                Dim bUpdateHealth As Boolean
                Dim objEmpHealthData As HU_EMPLOYEE_HEALTH
                bUpdateHealth = False
                objEmpHealthData = (From p In Context.HU_EMPLOYEE_HEALTH Where p.EMPLOYEE_ID = objEmp.ID).SingleOrDefault
                If objEmpHealthData Is Nothing Then 'Them moi
                    objEmpHealthData = New HU_EMPLOYEE_HEALTH With {.EMPLOYEE_ID = objEmp.ID}
                Else 'Update
                    bUpdateHealth = True
                End If
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
                objEmpHealthData.TTSUCKHOE = objEmpHealth.TTSUCKHOE
                objEmpHealthData.LOAI_SUCKHOE = objEmpHealth.LOAI_SUCKHOE
                objEmpHealthData.NGAY_KHAM = objEmpHealth.NGAY_KHAM
                If bUpdateHealth = False Then
                    Context.HU_EMPLOYEE_HEALTH.AddObject(objEmpHealthData)
                End If
            End If
            'End thông tin insert vào bảng HU_EMPLOYEE_HEALTH
            'anhvn, cap nhat so so bao hiem, noi kham chua benh vao bang ins_infomation
            If objEmp IsNot Nothing Then
                Dim objInsInfoData As INS_INFORMATION
                objInsInfoData = (From p In Context.INS_INFORMATION Where p.EMPLOYEE_ID = objEmp.ID).SingleOrDefault
                If objInsInfoData IsNot Nothing Then
                    If objEmp.BOOKNO IsNot Nothing AndAlso objEmp.BOOKNO <> "" Then
                        objInsInfoData.SOCIAL_NUMBER = objEmp.BOOKNO
                    End If
                    If IsNumeric(objEmpCV.HEALTH_AREA_INS_ID) Then
                        objInsInfoData.HEALTH_AREA_INS_ID = objEmpCV.HEALTH_AREA_INS_ID
                    End If
                    If objEmpCV.HEALTH_NO IsNot Nothing AndAlso objEmpCV.HEALTH_NO <> "" Then
                        objInsInfoData.HEALTH_NUMBER = objEmpCV.HEALTH_NO
                    End If
                End If
            End If
            '-----------------------------------------------------------------------
            If _imageBinary IsNot Nothing AndAlso _imageBinary.Length > 0 Then
                Dim savepath = ""
                For _count As Integer = 0 To 1
                    If _count = 0 Then
                        savepath = objEmp.IMAGE_URL
                    Else
                        savepath = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
                    End If

                    If Not Directory.Exists(savepath) Then
                        Directory.CreateDirectory(savepath)
                    End If
                    'Xóa ảnh cũ của nhân viên.
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
                Next
            End If

            Dim lstCon = (From p In Context.HU_CONCURRENTLY Where p.EMPLOYEE_ID = objEmp.ID _
                                    AndAlso (DateTime.Now <= p.EXPIRE_DATE_CON OrElse p.EXPIRE_DATE_CON Is Nothing)).ToList
            For Each itemCon In lstCon
                Dim objConEmpId = (From p In Context.HU_EMPLOYEE Where p.IS_KIEM_NHIEM = itemCon.ID Select p.ID).FirstOrDefault

                If objEmpCV IsNot Nothing Then
                    Dim objConEmpCV = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = objConEmpId).FirstOrDefault
                    If objConEmpCV IsNot Nothing Then
                        objConEmpCV.BIRTH_DATE = objEmpCV.BIRTH_DATE
                        objConEmpCV.GENDER = objEmpCV.GENDER
                        objConEmpCV.NATIONALITY = objEmpCV.NATIONALITY
                        objConEmpCV.PROVINCENQ_ID = objEmpCV.PROVINCENQ_ID
                        objConEmpCV.PROVINCEEMP_ID = objEmpCV.PROVINCEEMP_ID
                        objConEmpCV.MARITAL_STATUS = objEmpCV.MARITAL_STATUS
                        objConEmpCV.ID_NO = objEmpCV.ID_NO
                        objConEmpCV.PIT_CODE = objEmpCV.PIT_CODE
                        objConEmpCV.DISTRICTEMP_ID = objEmpCV.DISTRICTEMP_ID
                        objConEmpCV.NATIVE = objEmpCV.NATIVE
                        objConEmpCV.ID_DATE = objEmpCV.ID_DATE
                        objConEmpCV.PIT_CODE_DATE = objEmpCV.PIT_CODE_DATE
                        objConEmpCV.WARDEMP_ID = objEmpCV.WARDEMP_ID
                        objConEmpCV.RELIGION = objEmpCV.RELIGION
                        objConEmpCV.ID_PLACE = objEmpCV.ID_PLACE
                        objConEmpCV.PIT_ID_PLACE = objEmpCV.PIT_ID_PLACE
                        objConEmpCV.NO_HOUSEHOLDS = objEmpCV.NO_HOUSEHOLDS
                        objConEmpCV.CODE_HOUSEHOLDS = objEmpCV.CODE_HOUSEHOLDS
                        objConEmpCV.RELATE_OWNER = objEmpCV.RELATE_OWNER
                        objConEmpCV.PER_ADDRESS = objEmpCV.PER_ADDRESS
                        objConEmpCV.PER_PROVINCE = objEmpCV.PER_PROVINCE
                        objConEmpCV.PER_DISTRICT = objEmpCV.PER_DISTRICT
                        objConEmpCV.PER_WARD = objEmpCV.PER_WARD
                        objConEmpCV.NAV_ADDRESS = objEmpCV.NAV_ADDRESS
                        objConEmpCV.NAV_PROVINCE = objEmpCV.NAV_PROVINCE
                        objConEmpCV.NAV_DISTRICT = objEmpCV.NAV_DISTRICT
                        objConEmpCV.NAV_WARD = objEmpCV.NAV_WARD
                        objConEmpCV.MOBILE_PHONE = objEmpCV.MOBILE_PHONE
                        objConEmpCV.HOME_PHONE = objEmpCV.HOME_PHONE
                        objConEmpCV.PER_EMAIL = objEmpCV.PER_EMAIL
                        objConEmpCV.WORK_EMAIL = objEmpCV.WORK_EMAIL
                        objConEmpCV.CONTACT_PER = objEmpCV.CONTACT_PER
                        objConEmpCV.RELATION_PER_CTR = objEmpCV.RELATION_PER_CTR
                        objConEmpCV.CONTACT_PER_PHONE = objEmpCV.CONTACT_PER_PHONE
                        objConEmpCV.CONTACT_PER_MBPHONE = objEmpCV.CONTACT_PER_MBPHONE
                        objConEmpCV.CONTACT_PER_IDNO = objEmpCV.CONTACT_PER_IDNO
                        objConEmpCV.CONTACT_PER_EFFECT_DATE_IDNO = objEmpCV.CONTACT_PER_EFFECT_DATE_IDNO
                        objConEmpCV.CONTACT_PER_EXPIRE_DATE_IDNO = objEmpCV.CONTACT_PER_EXPIRE_DATE_IDNO
                        objConEmpCV.CONTACT_PER_PLACE_IDNO = objEmpCV.CONTACT_PER_PLACE_IDNO
                        objConEmpCV.ADDRESS_PER_CTR = objEmpCV.ADDRESS_PER_CTR
                        objConEmpCV.PASS_NO = objEmpCV.PASS_NO
                        objConEmpCV.PASS_DATE = objEmpCV.PASS_DATE
                        objConEmpCV.PASS_EXPIRE = objEmpCV.PASS_EXPIRE
                        objConEmpCV.PASS_PLACE_ID = objEmpCV.PASS_PLACE_ID
                        objConEmpCV.VISA = objEmpCV.VISA
                        objConEmpCV.VISA_DATE = objEmpCV.VISA_DATE
                        objConEmpCV.VISA_EXPIRE = objEmpCV.VISA_EXPIRE
                        objConEmpCV.VISA_PLACE_ID = objEmpCV.VISA_PLACE_ID
                        objConEmpCV.PERSON_INHERITANCE = objEmpCV.PERSON_INHERITANCE
                        objConEmpCV.BANK_NO = objEmpCV.BANK_NO
                        objConEmpCV.BANK_ID = objEmpCV.BANK_ID
                        objConEmpCV.BANK_BRANCH_ID = objEmpCV.BANK_BRANCH_ID
                        objConEmpCV.BOOK_NO = objEmpCV.BOOK_NO
                        objConEmpCV.BOOK_DATE = objEmpCV.BOOK_DATE
                        objConEmpCV.BOOK_EXPIRE = objEmpCV.BOOK_EXPIRE
                        objConEmpCV.SSLD_PLACE_ID = objEmpCV.SSLD_PLACE_ID
                    Else
                        Dim objConEmpCVData As New HU_EMPLOYEE_CV
                        objConEmpCVData.BIRTH_DATE = objEmpCV.BIRTH_DATE
                        objConEmpCVData.GENDER = objEmpCV.GENDER
                        objConEmpCVData.NATIONALITY = objEmpCV.NATIONALITY
                        objConEmpCVData.PROVINCENQ_ID = objEmpCV.PROVINCENQ_ID
                        objConEmpCVData.PROVINCEEMP_ID = objEmpCV.PROVINCEEMP_ID
                        objConEmpCVData.MARITAL_STATUS = objEmpCV.MARITAL_STATUS
                        objConEmpCVData.ID_NO = objEmpCV.ID_NO
                        objConEmpCVData.PIT_CODE = objEmpCV.PIT_CODE
                        objConEmpCVData.DISTRICTEMP_ID = objEmpCV.DISTRICTEMP_ID
                        objConEmpCVData.NATIVE = objEmpCV.NATIVE
                        objConEmpCVData.ID_DATE = objEmpCV.ID_DATE
                        objConEmpCVData.PIT_CODE_DATE = objEmpCV.PIT_CODE_DATE
                        objConEmpCVData.WARDEMP_ID = objEmpCV.WARDEMP_ID
                        objConEmpCVData.RELIGION = objEmpCV.RELIGION
                        objConEmpCVData.ID_PLACE = objEmpCV.ID_PLACE
                        objConEmpCVData.PIT_ID_PLACE = objEmpCV.PIT_ID_PLACE
                        objConEmpCVData.NO_HOUSEHOLDS = objEmpCV.NO_HOUSEHOLDS
                        objConEmpCVData.CODE_HOUSEHOLDS = objEmpCV.CODE_HOUSEHOLDS
                        objConEmpCVData.RELATE_OWNER = objEmpCV.RELATE_OWNER
                        objConEmpCVData.PER_ADDRESS = objEmpCV.PER_ADDRESS
                        objConEmpCVData.PER_PROVINCE = objEmpCV.PER_PROVINCE
                        objConEmpCVData.PER_DISTRICT = objEmpCV.PER_DISTRICT
                        objConEmpCVData.PER_WARD = objEmpCV.PER_WARD
                        objConEmpCVData.NAV_ADDRESS = objEmpCV.NAV_ADDRESS
                        objConEmpCVData.NAV_PROVINCE = objEmpCV.NAV_PROVINCE
                        objConEmpCVData.NAV_DISTRICT = objEmpCV.NAV_DISTRICT
                        objConEmpCVData.NAV_WARD = objEmpCV.NAV_WARD
                        objConEmpCVData.MOBILE_PHONE = objEmpCV.MOBILE_PHONE
                        objConEmpCVData.HOME_PHONE = objEmpCV.HOME_PHONE
                        objConEmpCVData.PER_EMAIL = objEmpCV.PER_EMAIL
                        objConEmpCVData.WORK_EMAIL = objEmpCV.WORK_EMAIL
                        objConEmpCVData.CONTACT_PER = objEmpCV.CONTACT_PER
                        objConEmpCVData.RELATION_PER_CTR = objEmpCV.RELATION_PER_CTR
                        objConEmpCVData.CONTACT_PER_PHONE = objEmpCV.CONTACT_PER_PHONE
                        objConEmpCVData.CONTACT_PER_MBPHONE = objEmpCV.CONTACT_PER_MBPHONE
                        objConEmpCVData.CONTACT_PER_IDNO = objEmpCV.CONTACT_PER_IDNO
                        objConEmpCVData.CONTACT_PER_EFFECT_DATE_IDNO = objEmpCV.CONTACT_PER_EFFECT_DATE_IDNO
                        objConEmpCVData.CONTACT_PER_EXPIRE_DATE_IDNO = objEmpCV.CONTACT_PER_EXPIRE_DATE_IDNO
                        objConEmpCVData.CONTACT_PER_PLACE_IDNO = objEmpCV.CONTACT_PER_PLACE_IDNO
                        objConEmpCVData.ADDRESS_PER_CTR = objEmpCV.ADDRESS_PER_CTR
                        objConEmpCVData.PASS_NO = objEmpCV.PASS_NO
                        objConEmpCVData.PASS_DATE = objEmpCV.PASS_DATE
                        objConEmpCVData.PASS_EXPIRE = objEmpCV.PASS_EXPIRE
                        objConEmpCVData.PASS_PLACE_ID = objEmpCV.PASS_PLACE_ID
                        objConEmpCVData.VISA = objEmpCV.VISA
                        objConEmpCVData.VISA_DATE = objEmpCV.VISA_DATE
                        objConEmpCVData.VISA_EXPIRE = objEmpCV.VISA_EXPIRE
                        objConEmpCVData.VISA_PLACE_ID = objEmpCV.VISA_PLACE_ID
                        objConEmpCVData.PERSON_INHERITANCE = objEmpCV.PERSON_INHERITANCE
                        objConEmpCVData.BANK_NO = objEmpCV.BANK_NO
                        objConEmpCVData.BANK_ID = objEmpCV.BANK_ID
                        objConEmpCVData.BANK_BRANCH_ID = objEmpCV.BANK_BRANCH_ID
                        objConEmpCVData.BOOK_NO = objEmpCV.BOOK_NO
                        objConEmpCVData.BOOK_DATE = objEmpCV.BOOK_DATE
                        objConEmpCVData.BOOK_EXPIRE = objEmpCV.BOOK_EXPIRE
                        objConEmpCVData.SSLD_PLACE_ID = objEmpCV.SSLD_PLACE_ID
                        Context.HU_EMPLOYEE_CV.AddObject(objConEmpCVData)
                    End If
                End If

                If objEmpHealth IsNot Nothing Then
                    Dim objConEmpHealth = (From p In Context.HU_EMPLOYEE_HEALTH Where p.EMPLOYEE_ID = objConEmpId).FirstOrDefault
                    If objConEmpHealth IsNot Nothing Then
                        objConEmpHealth.NHOM_MAU = objEmpHealth.NHOM_MAU
                        objConEmpHealth.LOAI_SUCKHOE = objEmpHealth.LOAI_SUCKHOE
                        objConEmpHealth.NGAY_KHAM = objEmpHealth.NGAY_KHAM
                        objConEmpHealth.CHIEU_CAO = objEmpHealth.CHIEU_CAO
                        objConEmpHealth.MAT_TRAI = objEmpHealth.MAT_TRAI
                        objConEmpHealth.GHI_CHU_SUC_KHOE = objEmpHealth.GHI_CHU_SUC_KHOE
                        objConEmpHealth.CAN_NANG = objEmpHealth.CAN_NANG
                        objConEmpHealth.MAT_PHAI = objEmpHealth.MAT_PHAI
                        objConEmpHealth.HUYET_AP = objEmpHealth.HUYET_AP
                        objConEmpHealth.TIM = objEmpHealth.TIM
                    Else
                        Dim objConEmpHealthData As New HU_EMPLOYEE_HEALTH
                        objConEmpHealthData.EMPLOYEE_ID = objConEmpId
                        objConEmpHealthData.NHOM_MAU = objEmpHealth.NHOM_MAU
                        objConEmpHealthData.LOAI_SUCKHOE = objEmpHealth.LOAI_SUCKHOE
                        objConEmpHealthData.NGAY_KHAM = objEmpHealth.NGAY_KHAM
                        objConEmpHealthData.CHIEU_CAO = objEmpHealth.CHIEU_CAO
                        objConEmpHealthData.MAT_TRAI = objEmpHealth.MAT_TRAI
                        objConEmpHealthData.GHI_CHU_SUC_KHOE = objEmpHealth.GHI_CHU_SUC_KHOE
                        objConEmpHealthData.CAN_NANG = objEmpHealth.CAN_NANG
                        objConEmpHealthData.MAT_PHAI = objEmpHealth.MAT_PHAI
                        objConEmpHealthData.HUYET_AP = objEmpHealth.HUYET_AP
                        objConEmpHealthData.TIM = objEmpHealth.TIM
                        Context.HU_EMPLOYEE_HEALTH.AddObject(objConEmpHealthData)
                    End If
                End If
                Context.SaveChanges(log)
            Next

            Context.SaveChanges(log)
            ' Sua vao tai khoan
            Dim Se_user = (From p In Context.SE_USER Where p.EMPLOYEE_CODE = objEmp.EMPLOYEE_CODE).FirstOrDefault

            If objEmpCV.ID_NO <> "" Then
                Dim _new As New SE_USER
                Dim acc_name = objEmp.EMPLOYEE_CODE
                Dim acc_pass = objEmpCVData.ID_NO

                If Se_user Is Nothing Then

                    Dim EncryptData As New EncryptData
                    _new.ID = Utilities.GetNextSequence(Context, Context.SE_USER.EntitySet.Name)
                    _new.EFFECT_DATE = Date.Now
                    _new.EMPLOYEE_CODE = objEmp.EMPLOYEE_CODE
                    _new.FULLNAME = objEmp.FULLNAME_VN
                    _new.EMAIL = objEmpCVData.WORK_EMAIL
                    _new.TELEPHONE = objEmpCVData.MOBILE_PHONE
                    _new.IS_AD = False
                    _new.IS_APP = False
                    _new.IS_PORTAL = True
                    _new.IS_CHANGE_PASS = "-1"
                    _new.ACTFLG = "A"
                    _new.PASSWORD = EncryptData.EncryptString(acc_pass)
                    _new.USERNAME = acc_name 'user(0)
                    _new.EMPLOYEE_ID = objEmp.ID

                    'INSERT USER
                    Insert_SE_User(_new, log)

                    Dim se_group = (From p In Context.SE_GROUP Where p.CODE = "PORTAL_USER").FirstOrDefault
                    If se_group IsNot Nothing Then
                        Insert_Permit(se_group.ID, _new.ID, log)
                    End If

                Else
                    Se_user.EMAIL = objEmpCVData.WORK_EMAIL
                    Se_user.FULLNAME = objEmp.FULLNAME_VN
                    Context.SaveChanges(log)
                End If
            End If

            'anhvn, 2020/07/16 auto thêm vào SE_GRP_SE_USR group portal user
            ' SE_GROUPS_ID = 163:Portal User
            'Dim user_se As SE_USER
            'Dim user_grp As SE_GRP_SE_USR
            'user_se = (From p In Context.SE_USER Where p.EMPLOYEE_CODE = objEmp.EMPLOYEE_CODE).FirstOrDefault
            ''Dim se_group = (From p In Context.se Where p.EMPLOYEE_CODE = objEmp.EMPLOYEE_CODE).FirstOrDefault
            'Dim SE_GROUPID As Decimal
            'Dim USERSE As Decimal
            'If user_se IsNot Nothing Then
            '    Dim se_group = (From p In Context.SE_GROUP Where p.CODE = "PORTAL_USER").FirstOrDefault
            '    user_grp = (From p In Context.SE_GRP_SE_USR Where p.SE_GROUPS_ID = se_group.ID And p.SE_USERS_ID = user_se.ID).FirstOrDefault
            '    If user_grp Is Nothing Then
            '        Dim _new_grp As New SE_GRP_SE_USR
            '        _new_grp.SE_GROUPS_ID = se_group.ID
            '        SE_GROUPID = se_group.ID
            '        _new_grp.SE_USERS_ID = user_se.ID
            '        USERSE = user_se.ID
            '        Context.SE_GRP_SE_USR.AddObject(_new_grp)

            '    End If
            'End If
            'Context.SaveChanges(log)
            'If user_se IsNot Nothing Then
            '    'them vao bang se_permit
            '    InsertUserGroup(SE_GROUPID, USERSE, log)
            'End If
            gID = objEmpData.ID

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, "Profile.InsertEmployeeByLinq")
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' anhvn
    ''' insert se_user and SE_GRP_SE_USR 
    ''' </summary>
    ''' <param name="users"></param>
    Private Sub Insert_SE_User(users As SE_USER, ByVal log As UserLog)
        Using conMng As New ConnectionManager
            Using conn As New OracleConnection(conMng.GetConnectionString())
                conn.Open()
                Using cmd As New OracleCommand()
                    Dim dem As Integer
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    Try
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PKG_COMMON_BUSINESS.INSERT_SE_GRP_SE_USR"
                        cmd.Parameters.Clear()
                        Using resource As New DataAccess.OracleCommon
                            Dim objParam = New With {.P_ID = users.ID,
                                                        .P_EFFECT_DATE = users.EFFECT_DATE,
                                                        .P_EMPLOYEE_CODE = users.EMPLOYEE_CODE,
                                                        .P_FULLNAME = users.FULLNAME,
                                                        .P_EMAIL = users.EMAIL,
                                                        .P_TELEPHONE = users.TELEPHONE,
                                                        .P_IS_AD = users.IS_AD,
                                                        .P_IS_APP = users.IS_APP,
                                                        .P_IS_PORTAL = users.IS_PORTAL,
                                                        .P_IS_CHANGE_PASS = users.IS_CHANGE_PASS,
                                                        .P_ACTFLG = users.ACTFLG,
                                                        .P_PASSWORD = users.PASSWORD,
                                                        .P_USERNAME = users.USERNAME,
                                                        .P_MODULE_ADMIN = users.MODULE_ADMIN,
                                                        .P_SE_USERS_ID = users.ID,
                                                        .P_USERLOG = log.Username,
                                                        .P_EMPLOYEE_ID = users.EMPLOYEE_ID}

                            If objParam IsNot Nothing Then
                                For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                    Dim bOut As Boolean = False
                                    Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                    If para IsNot Nothing Then
                                        cmd.Parameters.Add(para)
                                    End If
                                Next
                            End If
                            dem = cmd.ExecuteNonQuery()
                        End Using
                        cmd.Transaction.Commit()
                    Catch ex As Exception
                        cmd.Transaction.Rollback()
                    Finally
                        'Dispose all resource
                        cmd.Dispose()
                        conn.Close()
                        conn.Dispose()
                    End Try
                End Using
            End Using
        End Using
    End Sub


    Public Function Insert_Permit(ByVal _groupID As Decimal, ByVal _lstUserID As Decimal, ByVal log As UserLog) As Boolean

        Using conMng As New ConnectionManager
            Using conn As New OracleConnection(conMng.GetConnectionString())
                conn.Open()
                Dim dem As Integer

                Using cmd As New OracleCommand()
                    cmd.Connection = conn
                    cmd.Transaction = cmd.Connection.BeginTransaction()
                    Try
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.CommandText = "PKG_COMMON_BUSINESS.TRANSFER_GROUP_TO_USER"
                        cmd.Parameters.Clear()
                        Using resource As New DataAccess.OracleCommon
                            Dim objParam = New With {.P_USER_ID = _lstUserID,
                                                         .P_GROUP_ID = _groupID,
                                                         .P_USERNAME = log.Username}
                            If objParam IsNot Nothing Then
                                For Each info As PropertyInfo In objParam.GetType().GetProperties()
                                    Dim bOut As Boolean = False
                                    Dim para = resource.GetParameter(info.Name, info.GetValue(objParam, Nothing), bOut)
                                    If para IsNot Nothing Then
                                        cmd.Parameters.Add(para)
                                    End If
                                Next
                            End If
                            dem = cmd.ExecuteNonQuery()
                        End Using
                        cmd.Transaction.Commit()
                    Catch ex As Exception
                        cmd.Transaction.Rollback()
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
    End Function


    Public Function DeleteNVBlackList(ByVal id_no As String, ByVal log As UserLog) As Boolean
        Dim lstID As List(Of Decimal)
        Dim dsBlacklist As List(Of HU_TERMINATE)
        Try
            lstID = (From p In Context.HU_EMPLOYEE_CV Where p.ID_NO = id_no Select p.EMPLOYEE_ID).ToList
            dsBlacklist = (From p In Context.HU_TERMINATE Where lstID.Contains(p.EMPLOYEE_ID)).ToList
            For i = 0 To dsBlacklist.Count - 1
                Context.HU_TERMINATE.DeleteObject(dsBlacklist(i))
            Next
            Context.SaveChanges(log)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Hàm xóa nhân viên
    ''' </summary>
    ''' <param name="lstEmpID"></param>
    ''' <param name="log"></param>
    ''' <param name="sError"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteEmployee(ByVal lstEmpID As List(Of Decimal), ByVal log As UserLog, ByRef sError As String) As Boolean
        Dim lstEmpDelete As List(Of HU_EMPLOYEE)
        Try
            lstEmpDelete = (From p In Context.HU_EMPLOYEE Where lstEmpID.Contains(p.ID)).ToList
            For i As Int16 = 0 To lstEmpDelete.Count - 1
                'Kiểm tra nhân viên đó có hợp đồng ko. Nếu ko có thì xóa, nếu có thì lưu và cảnh báo.
                Dim objDelete = lstEmpDelete(i)
                Dim empID As Decimal = objDelete.ID
                Dim query As ObjectQuery(Of ContractDTO)
                Try
                    query = (From p In Context.HU_CONTRACT Where p.EMPLOYEE_ID = empID
                             Select New ContractDTO With {
                            .EMPLOYEE_ID = p.EMPLOYEE_ID})

                    If query.ToList.Count > 0 Then 'Nếu có hợp đồng
                        sError = sError & "," & objDelete.EMPLOYEE_CODE 'Lưu lại EMPLOYEE_CODE để cảnh báo.
                    Else 'Nếu ko có thì xóa nhân viên
                        '---Start xóa nhân viên-----------------------------------------------------------
                        '1. Xóa EMPLOYEE_CV.
                        Dim lstEmpCVDelete = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstEmpCVDelete.Count - 1
                            Context.HU_EMPLOYEE_CV.DeleteObject(lstEmpCVDelete(idx))
                        Next
                        '3. Xóa HU_EMPLOYEE_HEALTH
                        Dim lstEmpHealthDelete = (From p In Context.HU_EMPLOYEE_HEALTH Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstEmpHealthDelete.Count - 1
                            Context.HU_EMPLOYEE_HEALTH.DeleteObject(lstEmpHealthDelete(idx))
                        Next

                        '6. Xóa HU_FAMILY
                        Dim lstFamilyDelete = (From p In Context.HU_FAMILY Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstFamilyDelete.Count - 1
                            Context.HU_FAMILY.DeleteObject(lstFamilyDelete(idx))
                        Next
                        '7. Xóa HU_WORKING_BEFORE
                        Dim lstWorkingBeforeDelete = (From p In Context.HU_WORKING_BEFORE Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstWorkingBeforeDelete.Count - 1
                            Context.HU_WORKING_BEFORE.DeleteObject(lstWorkingBeforeDelete(idx))
                        Next

                        '9. Xóa HU_WELFARE_MNG
                        Dim lstWelfareMngDelete = (From p In Context.HU_WELFARE_MNG Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstWelfareMngDelete.Count - 1
                            Context.HU_WELFARE_MNG.DeleteObject(lstWelfareMngDelete(idx))
                        Next
                        '10. Xóa HU_EMPLOYEE_EDUCATION
                        Dim lstEduDelete = (From p In Context.HU_EMPLOYEE_EDUCATION Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstEduDelete.Count - 1
                            Context.HU_EMPLOYEE_EDUCATION.DeleteObject(lstEduDelete(idx))
                        Next

                        ''11. Xóa SE_GRP_SE_USR. Anhvn,2020/07/17 SE_GROUPS_ID=163 'PORTAL_USER'
                        'Dim se_group = (From p In Context.SE_GROUP Where p.CODE = "PORTAL_USER").FirstOrDefault
                        'Dim User_SE_temp = (From p In Context.SE_USER Where p.EMPLOYEE_CODE = objDelete.EMPLOYEE_CODE).FirstOrDefault
                        'Dim lstSegrpseUserDelete = (From p In Context.SE_GRP_SE_USR Where p.SE_GROUPS_ID = se_group.ID And p.SE_USERS_ID = User_SE_temp.ID).ToList
                        'For idx As Int16 = 0 To lstSegrpseUserDelete.Count - 1
                        '    Context.SE_GRP_SE_USR.DeleteObject(lstSegrpseUserDelete(idx))
                        'Next

                        '12. Xóa SE_USER.
                        Dim lstSeUserDelete = (From p In Context.SE_USER Where p.EMPLOYEE_CODE = objDelete.EMPLOYEE_CODE).ToList
                        For idx As Int16 = 0 To lstSeUserDelete.Count - 1
                            Context.SE_USER.DeleteObject(lstSeUserDelete(idx))
                        Next

                        '13. Xóa HU_EMPLOYEE_TRAIN
                        Dim lstTrainDelete = (From p In Context.HU_EMPLOYEE_TRAIN Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstTrainDelete.Count - 1
                            Context.HU_EMPLOYEE_TRAIN.DeleteObject(lstTrainDelete(idx))
                        Next



                        'Xóa Working
                        Dim lstWorkingUserDelete = (From p In Context.HU_WORKING Where p.EMPLOYEE_ID = empID).ToList
                        For idx As Int16 = 0 To lstWorkingUserDelete.Count - 1
                            Context.HU_WORKING.DeleteObject(lstWorkingUserDelete(idx))
                        Next

                        Context.HU_EMPLOYEE.DeleteObject(lstEmpDelete(i))

                        '---End xóa nhân viên ------------------------------------------------------------
                    End If
                Catch ex As Exception
                    WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                    Throw ex
                End Try
            Next
            If sError = "" Then
                Context.SaveChanges()
                Return True
            End If
            Return False
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
        Return True
    End Function

    Public Function GetEmployeeAllByID(ByVal sEmployeeID As Decimal,
                                  ByRef empCV As EmployeeCVDTO,
                                  ByRef empEdu As EmployeeEduDTO,
                                  ByRef empHealth As EmployeeHealthDTO) As Boolean
        Try
            empCV = (From cv In Context.HU_EMPLOYEE_CV
                     From g In Context.OT_OTHER_LIST.Where(Function(f) cv.GENDER = f.ID).DefaultIfEmpty
                     From ft In Context.OT_OTHER_LIST.Where(Function(f) cv.MARITAL_STATUS = f.ID).DefaultIfEmpty
                     From rl In Context.OT_OTHER_LIST.Where(Function(f) cv.RELIGION = f.ID).DefaultIfEmpty
                     From nt In Context.OT_OTHER_LIST.Where(Function(f) cv.NATIVE = f.ID).DefaultIfEmpty
                     From na In Context.HU_NATION.Where(Function(f) cv.NATIONALITY = f.ID).DefaultIfEmpty
                     From bank In Context.HU_BANK.Where(Function(f) cv.BANK_ID = f.ID).DefaultIfEmpty
                     From bankbranch In Context.HU_BANK_BRANCH.Where(Function(f) cv.BANK_BRANCH_ID = f.ID).DefaultIfEmpty
                     From per_pro In Context.HU_PROVINCE.Where(Function(f) cv.PER_PROVINCE = f.ID).DefaultIfEmpty
                     From per_dis In Context.HU_DISTRICT.Where(Function(f) cv.PER_DISTRICT = f.ID).DefaultIfEmpty
                     From per_ward In Context.HU_WARD.Where(Function(f) cv.PER_WARD = f.ID).DefaultIfEmpty
                     From nav_pro In Context.HU_PROVINCE.Where(Function(f) cv.NAV_PROVINCE = f.ID).DefaultIfEmpty
                     From nav_dis In Context.HU_DISTRICT.Where(Function(f) cv.NAV_DISTRICT = f.ID).DefaultIfEmpty
                     From nav_ward In Context.HU_WARD.Where(Function(f) cv.NAV_WARD = f.ID).DefaultIfEmpty
                     From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = cv.EMPLOYEE_ID).DefaultIfEmpty
                     From location In Context.HU_LOCATION.Where(Function(f) f.ID = emp.CONTRACTED_UNIT).DefaultIfEmpty
                     From ins_region In Context.OT_OTHER_LIST.Where(Function(f) f.ID = location.REGION).DefaultIfEmpty
                     From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = emp.ORG_ID).DefaultIfEmpty
                     From region In Context.OT_OTHER_LIST.Where(Function(f) f.ID = org.REGION_ID).DefaultIfEmpty
                     From emp_pro In Context.HU_PROVINCE.Where(Function(f) cv.PER_PROVINCE = f.ID).DefaultIfEmpty
                     From id_place In Context.HU_PROVINCE.Where(Function(f) cv.ID_PLACE = f.ID).DefaultIfEmpty
                     From pit_place In Context.HU_PROVINCE.Where(Function(f) cv.PIT_ID_PLACE = f.ID).DefaultIfEmpty
                     From emp_birthplace In Context.HU_PROVINCE.Where(Function(f) cv.BIRTH_PLACE = f.ID).DefaultIfEmpty
                     From emp_dis In Context.HU_DISTRICT.Where(Function(f) cv.PER_DISTRICT = f.ID).DefaultIfEmpty
                     From emp_ward In Context.HU_WARD.Where(Function(f) cv.PER_WARD = f.ID).DefaultIfEmpty
                     From nguyenquan In Context.HU_PROVINCE.Where(Function(f) f.ID = cv.PROVINCENQ_ID).DefaultIfEmpty
                     From thuongbinh In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.HANG_THUONG_BINH).DefaultIfEmpty
                     From gdchinhsach In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.GD_CHINH_SACH).DefaultIfEmpty
                     From bir_pro In Context.HU_PROVINCE.Where(Function(f) cv.PROVINCEEMP_ID = f.ID).DefaultIfEmpty
                     From bir_dis In Context.HU_DISTRICT.Where(Function(f) cv.DISTRICTEMP_ID = f.ID).DefaultIfEmpty
                     From bir_ward In Context.HU_WARD.Where(Function(f) cv.WARDEMP_ID = f.ID).DefaultIfEmpty
                     From relation_per In Context.OT_OTHER_LIST.Where(Function(f) cv.RELATION_PER_CTR = f.ID).DefaultIfEmpty
                     From objectIns In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.OBJECT_INS And f.TYPE_ID = 6894).DefaultIfEmpty
                     From ks_pro In Context.HU_PROVINCE.Where(Function(f) cv.PROVINCEEMP_ID = f.ID).DefaultIfEmpty
                     From ks_dis In Context.HU_DISTRICT.Where(Function(f) cv.DISTRICTEMP_ID = f.ID).DefaultIfEmpty
                     From ks_ward In Context.HU_WARD.Where(Function(f) cv.WARDEMP_ID = f.ID).DefaultIfEmpty
                     From ins_where In Context.INS_WHEREHEALTH.Where(Function(f) f.ID = cv.HEALTH_AREA_INS_ID).DefaultIfEmpty
                     From ro In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.RELATE_OWNER).DefaultIfEmpty
                     From pass_place In Context.HU_PROVINCE.Where(Function(f) cv.PASS_PLACE_ID = f.ID).DefaultIfEmpty
                     From visa_place In Context.HU_PROVINCE.Where(Function(f) cv.VISA_PLACE_ID = f.ID).DefaultIfEmpty
                     From book_place In Context.HU_PROVINCE.Where(Function(f) cv.SSLD_PLACE_ID = f.ID).DefaultIfEmpty
                     Where (cv.EMPLOYEE_ID = sEmployeeID)
                     Select New EmployeeCVDTO With {
                         .EMPLOYEE_ID = cv.EMPLOYEE_ID,
                         .GENDER = cv.GENDER,
                         .EXPIRE_DATE_IDNO = cv.EXPIRE_DATE_IDNO,
                         .VILLAGE = cv.VILLAGE,
                         .PIT_CODE_DATE = cv.PIT_CODE_DATE,
                         .PIT_CODE_PLACE = pit_place.NAME_VN,
                         .PERSON_INHERITANCE = cv.PERSON_INHERITANCE,
                         .CONTACT_PER_MBPHONE = cv.CONTACT_PER_MBPHONE,
                         .GENDER_NAME = g.NAME_VN,
                         .BIRTH_DATE = cv.BIRTH_DATE,
                         .BIRTH_PLACE = cv.BIRTH_PLACE,
                         .BIRTH_PLACENAME = emp_birthplace.NAME_VN,
                         .MARITAL_STATUS = cv.MARITAL_STATUS,
                         .MARITAL_STATUS_NAME = ft.NAME_VN,
                         .RELIGION = cv.RELIGION,
                         .RELIGION_NAME = rl.NAME_VN,
                         .HEALTH_AREA_INS_ID = cv.HEALTH_AREA_INS_ID,
                         .HEALTH_AREA_INS_NAME = ins_where.NAME_VN,
                         .HEALTH_NO = cv.HEALTH_NO,
                         .NATIVE = cv.NATIVE,
                         .NATIVE_NAME = nt.NAME_VN,
                         .NATIONALITY = cv.NATIONALITY,
                         .NATIONALITY_NAME = na.NAME_VN,
                         .PER_ADDRESS = cv.PER_ADDRESS,
                         .PER_PROVINCE = cv.PER_PROVINCE,
                         .PER_PROVINCE_NAME = per_pro.NAME_VN,
                         .PER_DISTRICT = cv.PER_DISTRICT,
                         .PER_DISTRICT_NAME = per_dis.NAME_VN,
                         .PER_WARD = cv.PER_WARD,
                         .PER_WARD_NAME = per_ward.NAME_VN,
                         .INS_REGION_ID = ins_region.ID,
                         .INS_REGION_NAME = ins_region.NAME_VN,
                         .HOME_PHONE = cv.HOME_PHONE,
                         .MOBILE_PHONE = cv.MOBILE_PHONE,
                         .ID_NO = cv.ID_NO,
                         .ID_DATE = cv.ID_DATE,
                         .ID_PLACE = cv.ID_PLACE,
                         .PLACE_NAME = id_place.NAME_VN,
                         .ID_REMARK = cv.ID_REMARK,
                         .PASS_NO = cv.PASS_NO,
                         .PASS_DATE = cv.PASS_DATE,
                         .PASS_EXPIRE = cv.PASS_EXPIRE,
                         .PASS_PLACE = pass_place.NAME_VN,
                         .VISA = cv.VISA,
                         .VISA_DATE = cv.VISA_DATE,
                         .VISA_EXPIRE = cv.VISA_EXPIRE,
                         .VISA_PLACE = visa_place.NAME_VN,
                         .WORK_PERMIT = cv.WORK_PERMIT,
                         .WORK_PERMIT_DATE = cv.WORK_PERMIT_DATE,
                         .WORK_PERMIT_EXPIRE = cv.WORK_PERMIT_EXPIRE,
                         .WORK_PERMIT_PLACE = cv.WORK_PERMIT_PLACE,
                         .WORK_EMAIL = cv.WORK_EMAIL,
                         .NAV_ADDRESS = cv.NAV_ADDRESS,
                         .NAV_PROVINCE = cv.NAV_PROVINCE,
                         .NAV_PROVINCE_NAME = nav_pro.NAME_VN,
                         .NAV_DISTRICT = cv.NAV_DISTRICT,
                         .NAV_DISTRICT_NAME = nav_dis.NAME_VN,
                         .NAV_WARD = cv.NAV_WARD,
                         .NAV_WARD_NAME = nav_ward.NAME_VN,
                         .PIT_CODE = cv.PIT_CODE,
                         .PER_EMAIL = cv.PER_EMAIL,
                         .CONTACT_PER = cv.CONTACT_PER,
                         .CONTACT_PER_PHONE = cv.CONTACT_PER_PHONE,
                         .CAREER = cv.CAREER,
                         .DANG_PHI = cv.DANG_PHI,
                         .DOAN_PHI = cv.DOAN_PHI,
                         .NGAY_VAO_DANG = cv.NGAY_VAO_DANG,
                         .NGAY_VAO_DOAN = cv.NGAY_VAO_DOAN,
                         .CHUC_VU_DANG = cv.CHUC_VU_DANG,
                         .CHUC_VU_DOAN = cv.CHUC_VU_DOAN,
                         .NOI_VAO_DANG = cv.NOI_VAO_DANG,
                         .NOI_VAO_DOAN = cv.NOI_VAO_DOAN,
                         .BANK_BRANCH_ID = cv.BANK_BRANCH_ID,
                         .BANK_BRANCH_NAME = bankbranch.NAME,
                         .BANK_ID = cv.BANK_ID,
                         .BANK_NAME = bank.NAME,
                         .IS_PERMISSION = cv.IS_PERMISSION,
                         .OPPTION1 = cv.OPPTION1,
                         .OPPTION2 = cv.OPPTION2,
                         .OPPTION3 = cv.OPPTION3,
                         .OPPTION4 = cv.OPPTION4,
                         .OPPTION5 = cv.OPPTION5,
                         .OPPTION6 = cv.OPPTION6,
                         .OPPTION7 = cv.OPPTION7,
                         .OPPTION8 = cv.OPPTION8,
                         .OPPTION9 = cv.OPPTION9,
                         .OPPTION10 = cv.OPPTION10,
                         .GD_CHINH_SACH = cv.GD_CHINH_SACH,
                         .GD_CHINH_SACH_NAME = gdchinhsach.NAME_VN,
                         .THUONG_BINH = CType(cv.THUONG_BINH, Boolean),
                         .DV_XUAT_NGU_QD = cv.DV_XUAT_NGU_QD,
                         .NGAY_XUAT_NGU_QD = cv.NGAY_XUAT_NGU_QD,
                         .NGAY_NHAP_NGU_QD = cv.NGAY_NHAP_NGU_QD,
                         .QD = CType(cv.QD, Boolean),
                         .DV_XUAT_NGU_CA = cv.DV_XUAT_NGU_CA,
                         .NGAY_XUAT_NGU_CA = cv.NGAY_XUAT_NGU_CA,
                         .NGAY_NHAP_NGU_CA = cv.NGAY_NHAP_NGU_CA,
                         .NGAY_TG_BAN_NU_CONG = cv.NGAY_TG_BAN_NU_CONG,
                         .CV_BAN_NU_CONG = cv.CV_BAN_NU_CONG,
                         .NU_CONG = CType(cv.NU_CONG, Boolean),
                         .NGAY_TG_BANTT = cv.NGAY_TG_BANTT,
                         .CV_BANTT = cv.CV_BANTT,
                         .CONG_DOAN = CType(cv.CONG_DOAN, Boolean),
                         .CA = cv.CA,
                         .NGAY_VAO_DTN = cv.NGAY_VAO_DTN,
                         .NOI_VAO_DTN = cv.NOI_VAO_DTN,
                         .CHUC_VU_DTN = cv.CHUC_VU_DTN,
                         .TD_CHINHTRI = cv.TD_CHINHTRI,
                         .CBO_SINHHOAT = cv.CBO_SINHHOAT,
                         .SO_LYLICH = cv.SO_LYLICH,
                         .SOTHE_DANG = cv.SOTHE_DANG,
                         .DANG = cv.DANG,
                         .SKILL = cv.SKILL,
                         .BANTT = cv.BANTT,
                         .WORKPLACE_NAME = cv.WORKPLACE_NAME,
                         .NGAY_VAO_DANG_DB = cv.NGAY_VAO_DANG_DB,
                         .HANG_THUONG_BINH = cv.HANG_THUONG_BINH,
                         .HANG_THUONG_BINH_NAME = thuongbinh.NAME_VN,
                         .PROVINCEEMP_ID = cv.PROVINCEEMP_ID,
                         .PROVINCEEMP_NAME = bir_pro.NAME_VN,
                         .DISTRICTEMP_NAME = bir_dis.NAME_VN,
                         .WARDEMP_NAME = bir_ward.NAME_VN,
                         .DISTRICTEMP_ID = cv.DISTRICTEMP_ID,
                         .WARDEMP_ID = cv.WARDEMP_ID,
                         .PROVINCENQ_ID = cv.PROVINCENQ_ID,
                         .PROVINCENQ_NAME = nguyenquan.NAME_VN,
                         .BANK_NO = cv.BANK_NO,
                         .IS_PAY_BANK = cv.IS_PAY_BANK,
                         .PROVINCEEMP_BRITH = cv.PROVINCEEMP_ID,
                         .PROVINCEEMP_BRITH_NAME = ks_pro.NAME_VN,
                         .DISTRICTEMP_BRITH = cv.DISTRICTEMP_ID,
                         .DISTRICTEMP_BRITH_NAME = ks_dis.NAME_VN,
                         .WARDEMP_BRITH = cv.WARDEMP_ID,
                         .WARDEMP_BRITH_NAME = ks_ward.NAME_VN,
                         .OBJECT_INS = cv.OBJECT_INS,
                         .OBJECT_INS_NAME = objectIns.NAME_VN,
                         .IS_CHUHO = CType(cv.IS_CHUHO, Boolean),
                         .NO_HOUSEHOLDS = cv.NO_HOUSEHOLDS,
                         .CODE_HOUSEHOLDS = cv.CODE_HOUSEHOLDS,
                         .RELATION_PER_CTR = cv.RELATION_PER_CTR,
                         .RELATION_PER_CTR_NAME = relation_per.NAME_VN,
                         .ADDRESS_PER_CTR = cv.ADDRESS_PER_CTR,
                         .ATTACH_FILE = cv.ATTACH_FILE,
                         .FILENAME = cv.FILENAME,
                         .OTHER_GENDER = cv.OTHER_GENDER,
                         .BIRTH_PLACE_DETAIL = cv.BIRTH_PLACE_DETAIL,
                        .COPY_ADDRESS = cv.COPY_ADDRESS,
                        .CHECK_NAV = cv.CHECK_NAV,
                        .BOOK_NO = cv.BOOK_NO,
                        .BOOK_DATE = cv.BOOK_DATE,
                        .BOOK_EXPIRE = cv.BOOK_EXPIRE,
                        .PASS_PLACE_ID = cv.PASS_PLACE_ID,
                        .VISA_PLACE_ID = cv.VISA_PLACE_ID,
                        .CONTACT_PER_IDNO = cv.CONTACT_PER_IDNO,
                        .CONTACT_PER_EFFECT_DATE_IDNO = cv.CONTACT_PER_EFFECT_DATE_IDNO,
                       .CONTACT_PER_EXPIRE_DATE_IDNO = cv.CONTACT_PER_EXPIRE_DATE_IDNO,
                       .CONTACT_PER_PLACE_IDNO = cv.CONTACT_PER_PLACE_IDNO,
                         .PIT_ID_PLACE = cv.PIT_ID_PLACE,
                       .SSLD_PLACE_ID = cv.SSLD_PLACE_ID,
                         .SSLD_PLACE_NAME = book_place.NAME_VN,
                         .RELATE_OWNER = cv.RELATE_OWNER,
                         .RELATE_OWNER_NAME = ro.NAME_VN
                         }).FirstOrDefault

            'Dim is_main = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.EMPLOYEE_ID = sEmployeeID And p.IS_MAIN = -1).FirstOrDefault
            'Dim is_major = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.EMPLOYEE_ID = sEmployeeID And p.IS_MAJOR = -1).FirstOrDefault
            'If is_main IsNot Nothing Or is_major IsNot Nothing Then
            '    Dim id = If(is_main IsNot Nothing, is_main.ID, is_major.ID)
            '    empEdu = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY
            '              From level In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEVEL_ID).DefaultIfEmpty
            '              From major In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MAJOR).DefaultIfEmpty
            '              From school In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GRADUATE_SCHOOL).DefaultIfEmpty
            '              Where p.ID = id
            '              Select New EmployeeEduDTO With {
            '                  .LEARNING_LEVEL_NAME = level.NAME_VN,
            '                  .MAJOR_NAME = major.NAME_VN,
            '                  .GRADUATE_SCHOOL_NAME = school.NAME_VN,
            '                  .GRADUATION_YEAR = p.YEAR_GRA,
            '                  .LEARNING_LEVEL_NAME_2 = level.NAME_VN,
            '                  .MAJOR_NAME_2 = major.NAME_VN,
            '                  .GRADUATE_SCHOOL_NAME_2 = school.NAME_VN,
            '    .GRADUATION_YEAR_2 = p.YEAR_GRA
            '                  }).FirstOrDefault
            'Else
            'End If
            empEdu = (From edu In Context.HU_EMPLOYEE_EDUCATION
                      From a In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.ACADEMY).DefaultIfEmpty
                      From m In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.MAJOR).DefaultIfEmpty
                      From train In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.TRAINING_FORM).DefaultIfEmpty
                      From learn In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.LEARNING_LEVEL).DefaultIfEmpty
                      From lang In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.LANGUAGE_LEVEL).DefaultIfEmpty
                      From school In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.GRADUATE_SCHOOL_ID).DefaultIfEmpty
                      From out In Context.HU_PRO_TRAIN_OUT_COMPANY.Where(Function(F) F.EMPLOYEE_ID = edu.EMPLOYEE_ID And F.IS_MAJOR = -1).DefaultIfEmpty
                      From learn2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = out.LEVEL_ID).DefaultIfEmpty
                      From m2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = out.MAJOR).DefaultIfEmpty
                      From school2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = out.GRADUATE_SCHOOL).DefaultIfEmpty
                      Where edu.EMPLOYEE_ID = sEmployeeID
                      Select New EmployeeEduDTO With {
                                        .EMPLOYEE_ID = edu.EMPLOYEE_ID,
                                        .ACADEMY = edu.ACADEMY,
                                        .ACADEMY_NAME = a.NAME_VN,
                                        .MAJOR = edu.MAJOR,
                                        .MAJOR_NAME = m.NAME_VN,
                                        .MAJOR_REMARK = edu.MAJOR_REMARK,
                                        .LANGUAGE_LEVEL = edu.LANGUAGE_LEVEL,
                                        .LANGUAGE_LEVEL_NAME = lang.NAME_VN,
                                        .LANGUAGE_MARK = edu.LANGUAGE_MARK,
                                        .TRAINING_FORM = edu.TRAINING_FORM,
                                        .TRAINING_FORM_NAME = train.NAME_VN,
                                        .LEARNING_LEVEL = edu.LEARNING_LEVEL,
                                        .LEARNING_LEVEL_NAME = learn.NAME_VN,
                                        .GRADUATE_SCHOOL_ID = edu.GRADUATE_SCHOOL_ID,
                                        .GRADUATE_SCHOOL_NAME = school.NAME_VN,
                                        .GRADUATION_YEAR = edu.GRADUATION_YEAR,
                                      .LEARNING_LEVEL_NAME_2 = learn2.NAME_VN,
                                      .MAJOR_NAME_2 = m2.NAME_VN,
                                      .GRADUATE_SCHOOL_NAME_2 = school2.NAME_VN,
                                      .GRADUATION_YEAR_2 = out.YEAR_GRA}).FirstOrDefault


            empHealth = (From e In Context.HU_EMPLOYEE_HEALTH
                         From OT In Context.OT_OTHER_LIST.Where(Function(F) F.ID = e.LOAI_SUCKHOE).DefaultIfEmpty
                         From OT1 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = e.STATUS_SUCKHOE).DefaultIfEmpty
                         Where e.EMPLOYEE_ID = sEmployeeID
                         Select New EmployeeHealthDTO With {
                             .EMPLOYEE_ID = e.EMPLOYEE_ID,
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
                             .TTSUCKHOE = e.TTSUCKHOE,
                             .GHI_CHU_SUC_KHOE = e.GHI_CHU_SUC_KHOE,
                             .LOAI_SUCKHOE = e.LOAI_SUCKHOE,
                             .LOAI_SUCKHOE_NAME = OT.NAME_VN,
                             .NGAY_KHAM = e.NGAY_KHAM}).FirstOrDefault

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


    Public Function ValidateEmployee(ByVal sType As String, ByVal sEmpCode As String, ByVal value As String) As Boolean
        Try
            Select Case sType
                Case "EXIST_ID_NO_TERMINATE"
                    If sEmpCode <> "" Then
                        Return (From p In Context.HU_TERMINATE
                                From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                                From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                                Where cv.ID_NO = value And p.IS_NOHIRE = True And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID And e.EMPLOYEE_CODE <> sEmpCode).Count = 0
                    Else
                        Return (From p In Context.HU_TERMINATE
                                From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                                From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                                Where cv.ID_NO = value And p.IS_NOHIRE = True And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID).Count = 0
                    End If



                Case "EXIST_ID_NO"
                    If sEmpCode <> "" Then
                        Return (From e In Context.HU_EMPLOYEE
                                From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                Where cv.ID_NO = value And e.EMPLOYEE_CODE <> sEmpCode).Count = 0
                    Else

                        Return (From e In Context.HU_EMPLOYEE
                                From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                                Where cv.ID_NO = value).Count = 0
                    End If


                Case "EXIST_TIME_ID"
                    If sEmpCode <> "" Then
                        Return (From e In Context.HU_EMPLOYEE
                                Where e.ITIME_ID = value And e.EMPLOYEE_CODE <> sEmpCode).Count = 0
                    Else
                        Return (From e In Context.HU_EMPLOYEE
                                Where e.ITIME_ID = value).Count = 0
                    End If

                Case "EXIST_WORK_EMAIL"
                    'Return (From p In Context.HU_EMPLOYEE
                    '        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                    '        Where p.EMPLOYEE_CODE <> sEmpCode And cv.WORK_EMAIL.ToUpper = value.ToUpper).Count = 0

                    If sEmpCode <> "" Then
                        Return (From p In Context.HU_EMPLOYEE
                                From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                                Where p.EMPLOYEE_CODE <> sEmpCode And cv.WORK_EMAIL.ToUpper = value.ToUpper).Count = 0
                    Else
                        Return (From p In Context.HU_EMPLOYEE
                                From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                                Where cv.WORK_EMAIL.ToUpper = value.ToUpper).Count = 0
                    End If
                Case "EXIST_BANK_NO"
                    Return (From p In Context.HU_EMPLOYEE
                            From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = p.ID)
                            Where cv.BANK_NO.ToUpper = value.ToUpper).Count = 0
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateEmployee")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Hàm kiểm tra nhân viên có hợp đồng chưa.
    ''' </summary>
    ''' <param name="strEmpCode"></param>
    ''' <returns>True: Nếu có hợp đồng</returns>
    ''' <remarks></remarks>
    Public Function CheckEmpHasContract(ByVal strEmpCode As String) As Boolean
        Try
            Return (From p In Context.HU_CONTRACT Where p.HU_EMPLOYEE.EMPLOYEE_CODE = strEmpCode And p.OT_STATUS.CODE = "1").Count > 0
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeePerInfo(ByVal _emp_id As Decimal) As EmployeeCVDTO
        Try
            Dim query = (From p In Context.HU_EMPLOYEE_CV
                         Where p.EMPLOYEE_ID = _emp_id
                         Select New EmployeeCVDTO With {.EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                       .IS_CHUHO = p.IS_CHUHO,
                                                       .PER_ADDRESS = p.PER_ADDRESS,
                                                       .PER_PROVINCE = p.PER_PROVINCE,
                                                       .PER_DISTRICT = p.PER_DISTRICT,
                                                       .PER_WARD = p.PER_WARD,
                                                       .NAV_ADDRESS = p.NAV_ADDRESS,
                                                       .NAV_PROVINCE = p.NAV_PROVINCE,
                                                       .NAV_DISTRICT = p.NAV_DISTRICT,
                                                       .NAV_WARD = p.NAV_WARD}).FirstOrDefault
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ValidateEmpWorkEmail(ByVal _email As String) As Boolean
        Try
            Dim query = From e In Context.HU_EMPLOYEE
                        From ecv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                        Where ecv.WORK_EMAIL IsNot Nothing AndAlso ecv.WORK_EMAIL.ToLower.Equals(_email.ToLower) AndAlso e.WORK_STATUS <> 257

            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeByEmail(ByVal _email As String,
                                       ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer, ByVal _param As ParamDTO,
                                       Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                       Optional ByVal log As UserLog = Nothing) As List(Of EmployeeDTO)
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            'And _param.EMP_CODE.ToUpper <> e.EMPLOYEE_CODE.ToUpper
            Dim query = From e In Context.HU_EMPLOYEE
                        From ecv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.WORK_STATUS).DefaultIfEmpty
                        From org In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME.ToUpper.Equals(log.Username.ToUpper))
                        Where ecv.WORK_EMAIL IsNot Nothing AndAlso ecv.WORK_EMAIL.ToUpper.Equals(_email.ToUpper)
                        Select New EmployeeDTO With {
                             .ID = e.ID,
                             .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                             .FULLNAME_VN = e.FULLNAME_VN,
                             .TITLE_NAME_VN = t.NAME_VN,
                             .ORG_NAME = o.NAME_VN,
                             .WORK_STATUS_NAME = ot.NAME_VN,
                             .TER_EFFECT_DATE = e.TER_EFFECT_DATE,
                             .WORK_EMAIL = ecv.WORK_EMAIL}
            If _param.EMP_CODE IsNot Nothing And _param.EMP_CODE <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE <> _param.EMP_CODE)
            End If
            query = query.OrderBy(Sorts)
            Total = query.Count
            query = query.Skip(PageIndex * PageSize).Take(PageSize)
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    ' portal thay avatar
    Public Function PortalSendImage(ByVal employeeCode As String, ByVal userID As Decimal, ByVal _imageBinary As Byte(), ByVal imageEx As String) As Boolean
        Try
            If _imageBinary IsNot Nothing AndAlso _imageBinary.Length > 0 Then
                'Dim userInf = (From u In Context.SE_USER Where u.EMPLOYEE_ID = userID).FirstOrDefault()
                Dim employeeID = userID
                Dim objEmpCVData = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = employeeID).FirstOrDefault()
                ' với mobile truyền user vào employeeID
                If employeeCode = "" Then
                    employeeCode = (From p In Context.HU_EMPLOYEE Where p.ID = employeeID).FirstOrDefault().EMPLOYEE_CODE
                End If
                Dim savepath = ""
                Dim savepath_app = ""
                Dim imageName = employeeCode & "." & imageEx
                savepath = AppDomain.CurrentDomain.BaseDirectory & "EmployeeImage"
                'savepath = (From P In Context.SE_CONFIG Where P.CODE.ToUpper = "IMAGE_EMP" Select P.VALUE).FirstOrDefault
                savepath_app = (From P In Context.SE_CONFIG Where P.CODE.ToUpper = "IMAGE_EMP_APP" Select P.VALUE).FirstOrDefault
                'savepath_host = (From P In Context.SE_CONFIG Where P.CODE.ToUpper = "IMAGE_EMP_HOST" Select P.VALUE).FirstOrDefault

                If Not Directory.Exists(savepath) Then
                    Directory.CreateDirectory(savepath)
                End If
                If Not Directory.Exists(savepath_app) Then
                    Directory.CreateDirectory(savepath_app)
                End If
                'Xóa ảnh cũ của nhân viên.
                Try
                    Dim sFile() As String = Directory.GetFiles(savepath, objEmpCVData.IMAGE)
                    Dim sFile_app() As String = Directory.GetFiles(savepath_app, objEmpCVData.IMAGE)
                    If sFile.Length > 0 Then
                        For Each s In sFile
                            File.Delete(s)
                        Next
                    End If
                    If sFile_app.Length > 0 Then
                        For Each s In sFile_app
                            File.Delete(s)
                        Next
                    End If
                Catch ex As Exception

                End Try



                Dim ms As New MemoryStream(_imageBinary)
                ' đọc lại từ base lưu sang file vật lý
                ' mệt vl
                Dim fs As New FileStream(savepath & "\" & imageName, FileMode.Create)

                ms.WriteTo(fs)
                Dim fs_app As New FileStream(savepath_app & "\" & imageName, FileMode.Create)
                ms.WriteTo(fs_app)
                ' clean up
                ms.Close()

                fs.Close()
                fs.Dispose()

                fs_app.Close()
                fs_app.Dispose()

                objEmpCVData.IMAGE = imageName
                Context.SaveChanges()

                Return True
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function GetEmployeeCuriculumVitae(ByVal empID As Decimal, ByRef empCV As EmployeeCVDTO, ByRef empHealth As EmployeeHealthDTO) As EmployeeDTO
        Try
            Try
                Dim str As String = "Kiêm nhiệm"
                If empID = 0 Then Return Nothing
                Dim query As New EmployeeDTO
                query =
                    (From e In Context.HU_EMPLOYEE
                     From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = e.ID)
                     From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID)
                     From c In Context.HU_CONTRACT.Where(Function(c) c.ID = e.CONTRACT_ID).DefaultIfEmpty
                     From org In Context.HU_ORGANIZATION.Where(Function(t) t.ID = e.ORG_ID).DefaultIfEmpty
                     From t In Context.HU_CONTRACT_TYPE.Where(Function(t) t.ID = c.CONTRACT_TYPE_ID).DefaultIfEmpty
                     From empstatus In Context.OT_OTHER_LIST.Where(Function(f) f.ID = e.EMP_STATUS And f.TYPE_ID = 2235).DefaultIfEmpty
                     Where (e.ID = empID)
                     Select New EmployeeDTO With {
                         .ID = e.ID,
                         .FULLNAME_EN = e.FULLNAME_EN,
                         .FULLNAME_VN = e.FULLNAME_VN,
                         .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                         .EMPLOYEE_CODE_OLD = e.EMPLOYEE_CODE_OLD,
                         .EMPLOYEE_NAME_OTHER = e.EMPLOYEE_NAME_OTHER,
                         .IMAGE = e.HU_EMPLOYEE_CV.IMAGE,
                         .TITLE_ID = e.TITLE_ID,
                         .TITLE_NAME_VN = title.NAME_VN,
                         .ORG_ID = e.ORG_ID,
                         .ORG_NAME = org.NAME_VN,
                         .EMP_STATUS_NAME = If(e.IS_KIEM_NHIEM IsNot Nothing, str, empstatus.NAME_VN),
                         .JOIN_DATE = e.JOIN_DATE,
                         .JOIN_DATE_STATE = e.JOIN_DATE_STATE,
                         .CONTRACT_TYPE_NAME = t.NAME_VISIBLE_ONFORM,
                         .OBJECT_EMPLOYEE_ID = e.OBJECT_EMPLOYEE_ID,
                         .BOOK_NO_SOCIAL = e.BOOK_NO
                     }).FirstOrDefault

                empCV = (From cv In Context.HU_EMPLOYEE_CV
                         From g In Context.OT_OTHER_LIST.Where(Function(f) cv.GENDER = f.ID).DefaultIfEmpty
                         From ft In Context.OT_OTHER_LIST.Where(Function(f) cv.MARITAL_STATUS = f.ID).DefaultIfEmpty
                         From rl In Context.OT_OTHER_LIST.Where(Function(f) cv.RELIGION = f.ID).DefaultIfEmpty
                         From nt In Context.OT_OTHER_LIST.Where(Function(f) cv.NATIVE = f.ID).DefaultIfEmpty
                         From na In Context.HU_NATION.Where(Function(f) cv.NATIONALITY = f.ID).DefaultIfEmpty
                         From idpl In Context.HU_PROVINCE.Where(Function(f) cv.ID_PLACE = f.ID).DefaultIfEmpty
                         From ssldpl In Context.HU_PROVINCE.Where(Function(f) cv.SSLD_PLACE_ID = f.ID).DefaultIfEmpty
                         From per_pro In Context.HU_PROVINCE.Where(Function(f) cv.PER_PROVINCE = f.ID).DefaultIfEmpty
                         From per_dis In Context.HU_DISTRICT.Where(Function(f) cv.PER_DISTRICT = f.ID).DefaultIfEmpty
                         From per_ward In Context.HU_WARD.Where(Function(f) cv.PER_WARD = f.ID).DefaultIfEmpty
                         From nav_pro In Context.HU_PROVINCE.Where(Function(f) cv.NAV_PROVINCE = f.ID).DefaultIfEmpty
                         From nav_dis In Context.HU_DISTRICT.Where(Function(f) cv.NAV_DISTRICT = f.ID).DefaultIfEmpty
                         From nav_ward In Context.HU_WARD.Where(Function(f) cv.NAV_WARD = f.ID).DefaultIfEmpty
                         From emp_pro In Context.HU_PROVINCE.Where(Function(f) cv.PER_PROVINCE = f.ID).DefaultIfEmpty
                         From emp_birthplace In Context.HU_PROVINCE.Where(Function(f) cv.BIRTH_PLACE = f.ID).DefaultIfEmpty
                         From emp_dis In Context.HU_DISTRICT.Where(Function(f) cv.PER_DISTRICT = f.ID).DefaultIfEmpty
                         From emp_ward In Context.HU_WARD.Where(Function(f) cv.PER_WARD = f.ID).DefaultIfEmpty
                         From nguyenquan In Context.HU_PROVINCE.Where(Function(f) f.ID = cv.PROVINCENQ_ID).DefaultIfEmpty
                         From bir_pro In Context.HU_PROVINCE.Where(Function(f) cv.PROVINCEEMP_ID = f.ID).DefaultIfEmpty
                         From bir_dis In Context.HU_DISTRICT.Where(Function(f) cv.DISTRICTEMP_ID = f.ID).DefaultIfEmpty
                         From bir_ward In Context.HU_WARD.Where(Function(f) cv.WARDEMP_ID = f.ID).DefaultIfEmpty
                         From relation_per In Context.OT_OTHER_LIST.Where(Function(f) cv.RELATION_PER_CTR = f.ID).DefaultIfEmpty
                         From ks_pro In Context.HU_PROVINCE.Where(Function(f) cv.PROVINCEEMP_ID = f.ID).DefaultIfEmpty
                         From ks_dis In Context.HU_DISTRICT.Where(Function(f) cv.DISTRICTEMP_ID = f.ID).DefaultIfEmpty
                         From ks_ward In Context.HU_WARD.Where(Function(f) cv.WARDEMP_ID = f.ID).DefaultIfEmpty
                         From ro In Context.OT_OTHER_LIST.Where(Function(f) f.ID = cv.RELATE_OWNER).DefaultIfEmpty
                         From ins_where In Context.INS_WHEREHEALTH.Where(Function(f) f.ID = cv.HEALTH_AREA_INS_ID).DefaultIfEmpty
                         Where (cv.EMPLOYEE_ID = empID)
                         Select New EmployeeCVDTO With {
                         .EMPLOYEE_ID = cv.EMPLOYEE_ID,
                         .GENDER = cv.GENDER,
                         .VILLAGE = cv.VILLAGE,
                         .PIT_CODE_DATE = cv.PIT_CODE_DATE,
                         .PIT_CODE_PLACE = cv.PIT_CODE_PLACE,
                         .CONTACT_PER_MBPHONE = cv.CONTACT_PER_MBPHONE,
                         .GENDER_NAME = g.NAME_VN,
                         .BIRTH_DATE = cv.BIRTH_DATE,
                         .BIRTH_PLACENAME = emp_birthplace.NAME_VN,
                         .MARITAL_STATUS_NAME = ft.NAME_VN,
                         .RELIGION_NAME = rl.NAME_VN,
                         .NATIVE_NAME = nt.NAME_VN,
                         .NATIONALITY_NAME = na.NAME_VN,
                         .PER_ADDRESS = cv.PER_ADDRESS,
                         .PER_PROVINCE_NAME = per_pro.NAME_VN,
                         .PER_DISTRICT_NAME = per_dis.NAME_VN,
                         .PER_WARD_NAME = per_ward.NAME_VN,
                         .HOME_PHONE = cv.HOME_PHONE,
                         .MOBILE_PHONE = cv.MOBILE_PHONE,
                         .ID_NO = cv.ID_NO,
                         .ID_DATE = cv.ID_DATE,
                         .ID_PLACE = cv.ID_PLACE,
                         .PLACE_NAME = idpl.NAME_VN,
                         .HEALTH_AREA_INS_NAME = ins_where.NAME_VN,
                         .ID_REMARK = cv.ID_REMARK,
                         .PASS_NO = cv.PASS_NO,
                         .PASS_DATE = cv.PASS_DATE,
                         .PASS_EXPIRE = cv.PASS_EXPIRE,
                         .PASS_PLACE = cv.PASS_PLACE,
                         .VISA = cv.VISA,
                         .VISA_DATE = cv.VISA_DATE,
                         .VISA_EXPIRE = cv.VISA_EXPIRE,
                         .VISA_PLACE = cv.VISA_PLACE,
                         .WORK_EMAIL = cv.WORK_EMAIL,
                         .NAV_ADDRESS = cv.NAV_ADDRESS,
                         .NAV_PROVINCE = cv.NAV_PROVINCE,
                         .NAV_PROVINCE_NAME = nav_pro.NAME_VN,
                         .NAV_DISTRICT = cv.NAV_DISTRICT,
                         .NAV_DISTRICT_NAME = nav_dis.NAME_VN,
                         .NAV_WARD = cv.NAV_WARD,
                         .NAV_WARD_NAME = nav_ward.NAME_VN,
                         .PIT_CODE = cv.PIT_CODE,
                         .PER_EMAIL = cv.PER_EMAIL,
                         .CONTACT_PER = cv.CONTACT_PER,
                         .CONTACT_PER_PHONE = cv.CONTACT_PER_PHONE,
                         .CAREER = cv.CAREER,
                         .IS_PERMISSION = cv.IS_PERMISSION,
                         .WORKPLACE_NAME = cv.WORKPLACE_NAME,
                         .PROVINCEEMP_ID = cv.PROVINCEEMP_ID,
                         .PROVINCEEMP_NAME = bir_pro.NAME_VN,
                         .DISTRICTEMP_NAME = bir_dis.NAME_VN,
                         .WARDEMP_NAME = bir_ward.NAME_VN,
                         .DISTRICTEMP_ID = cv.DISTRICTEMP_ID,
                         .WARDEMP_ID = cv.WARDEMP_ID,
                         .PROVINCENQ_ID = cv.PROVINCENQ_ID,
                         .PROVINCENQ_NAME = nguyenquan.NAME_VN,
                         .BANK_NO = cv.BANK_NO,
                         .PROVINCEEMP_BRITH = cv.PROVINCEEMP_ID,
                         .PROVINCEEMP_BRITH_NAME = ks_pro.NAME_VN,
                         .DISTRICTEMP_BRITH = cv.DISTRICTEMP_ID,
                         .DISTRICTEMP_BRITH_NAME = ks_dis.NAME_VN,
                         .WARDEMP_BRITH = cv.WARDEMP_ID,
                         .WARDEMP_BRITH_NAME = ks_ward.NAME_VN,
                         .IS_CHUHO = CType(cv.IS_CHUHO, Boolean),
                         .NO_HOUSEHOLDS = cv.NO_HOUSEHOLDS,
                         .CODE_HOUSEHOLDS = cv.CODE_HOUSEHOLDS,
                         .RELATION_PER_CTR = cv.RELATION_PER_CTR,
                         .RELATION_PER_CTR_NAME = relation_per.NAME_VN,
                         .ADDRESS_PER_CTR = cv.ADDRESS_PER_CTR,
                         .ATTACH_FILE = cv.ATTACH_FILE,
                         .FILENAME = cv.FILENAME,
                         .OTHER_GENDER = cv.OTHER_GENDER,
                         .BIRTH_PLACE_DETAIL = cv.BIRTH_PLACE_DETAIL,
                         .COPY_ADDRESS = cv.COPY_ADDRESS,
                         .CHECK_NAV = cv.CHECK_NAV,
                         .BOOK_NO = cv.BOOK_NO,
                         .BOOK_DATE = cv.BOOK_DATE,
                         .BOOK_EXPIRE = cv.BOOK_EXPIRE,
                         .PASS_PLACE_ID = cv.PASS_PLACE_ID,
                         .VISA_PLACE_ID = cv.VISA_PLACE_ID,
                         .CONTACT_PER_IDNO = cv.CONTACT_PER_IDNO,
                         .CONTACT_PER_EFFECT_DATE_IDNO = cv.CONTACT_PER_EFFECT_DATE_IDNO,
                         .CONTACT_PER_EXPIRE_DATE_IDNO = cv.CONTACT_PER_EXPIRE_DATE_IDNO,
                         .CONTACT_PER_PLACE_IDNO = cv.CONTACT_PER_PLACE_IDNO,
                         .PIT_ID_PLACE = cv.PIT_ID_PLACE,
                         .SSLD_PLACE_ID = cv.SSLD_PLACE_ID,
                         .SSLD_PLACE_NAME = ssldpl.NAME_VN,
                         .RELATE_OWNER = cv.RELATE_OWNER,
                         .RELATE_OWNER_NAME = ro.NAME_VN
                         }).FirstOrDefault

                empHealth = (From e In Context.HU_EMPLOYEE_HEALTH
                             Where e.EMPLOYEE_ID = empID
                             Select New EmployeeHealthDTO With {
                             .EMPLOYEE_ID = e.EMPLOYEE_ID,
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
                             .TTSUCKHOE = e.TTSUCKHOE,
                             .GHI_CHU_SUC_KHOE = e.GHI_CHU_SUC_KHOE}).FirstOrDefault

                Return query
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                Throw ex
            End Try
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#Region "EmployeeTrain"
    Public Function InsertEmployeeTrain(ByVal objEmployeeTrain As EmployeeTrainDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objEmployeeTrainData As New HU_EMPLOYEE_TRAIN
            objEmployeeTrainData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE_TRAIN.EntitySet.Name)
            objEmployeeTrainData.EMPLOYEE_ID = objEmployeeTrain.EMPLOYEE_ID
            objEmployeeTrainData.FROM_DATE = objEmployeeTrain.FROM_DATE
            objEmployeeTrainData.TO_DATE = objEmployeeTrain.TO_DATE
            objEmployeeTrainData.SCHOOL_NAME = objEmployeeTrain.SCHOOL_NAME
            objEmployeeTrainData.TRAINING_FORM = objEmployeeTrain.TRAINING_FORM
            objEmployeeTrainData.HIGHEST_LEVEL = objEmployeeTrain.HIGHEST_LEVEL
            objEmployeeTrainData.LEARNING_LEVEL = objEmployeeTrain.LEARNING_LEVEL
            objEmployeeTrainData.MAJOR = objEmployeeTrain.MAJOR
            objEmployeeTrainData.GRADUATE_YEAR = objEmployeeTrain.GRADUATE_YEAR
            objEmployeeTrainData.MARK = objEmployeeTrain.MARK
            objEmployeeTrainData.TRAINING_CONTENT = objEmployeeTrain.TRAINING_CONTENT

            objEmployeeTrainData.CREATED_DATE = DateTime.Now
            objEmployeeTrainData.CREATED_BY = log.Username
            objEmployeeTrainData.CREATED_LOG = log.ComputerName
            objEmployeeTrainData.MODIFIED_DATE = DateTime.Now
            objEmployeeTrainData.MODIFIED_BY = log.Username
            objEmployeeTrainData.MODIFIED_LOG = log.ComputerName
            Context.HU_EMPLOYEE_TRAIN.AddObject(objEmployeeTrainData)
            Context.SaveChanges(log)
            gID = objEmployeeTrainData.ID
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyEmployeeTrain(ByVal objEmployeeTrain As EmployeeTrainDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objEmployeeTrainData As New HU_EMPLOYEE_TRAIN With {.ID = objEmployeeTrain.ID}
        Try
            objEmployeeTrainData = (From p In Context.HU_EMPLOYEE_TRAIN Where p.ID = objEmployeeTrain.ID).FirstOrDefault
            objEmployeeTrainData.EMPLOYEE_ID = objEmployeeTrain.EMPLOYEE_ID
            objEmployeeTrainData.FROM_DATE = objEmployeeTrain.FROM_DATE
            objEmployeeTrainData.TO_DATE = objEmployeeTrain.TO_DATE
            objEmployeeTrainData.SCHOOL_NAME = objEmployeeTrain.SCHOOL_NAME
            objEmployeeTrainData.TRAINING_FORM = objEmployeeTrain.TRAINING_FORM
            objEmployeeTrainData.HIGHEST_LEVEL = objEmployeeTrain.HIGHEST_LEVEL
            objEmployeeTrainData.LEARNING_LEVEL = objEmployeeTrain.LEARNING_LEVEL
            objEmployeeTrainData.MAJOR = objEmployeeTrain.MAJOR
            objEmployeeTrainData.GRADUATE_YEAR = objEmployeeTrain.GRADUATE_YEAR
            objEmployeeTrainData.MARK = objEmployeeTrain.MARK
            objEmployeeTrainData.TRAINING_CONTENT = objEmployeeTrain.TRAINING_CONTENT

            objEmployeeTrainData.MODIFIED_DATE = DateTime.Now
            objEmployeeTrainData.MODIFIED_BY = log.Username
            objEmployeeTrainData.MODIFIED_LOG = log.ComputerName
            Context.SaveChanges(log)
            gID = objEmployeeTrainData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ValidateEmployeeTrain(ByVal objValidate As EmployeeTrainDTO) As Boolean
        Try
            Dim query As ObjectQuery(Of EmployeeTrainDTO)
            Dim lstEmpTrain As New List(Of EmployeeTrainDTO)
            'Kiểm tra đã có mức học vấn cao nhất chưa.
            If objValidate.ID > 0 Then
                query = (From p In Context.HU_EMPLOYEE_TRAIN Where p.EMPLOYEE_ID = objValidate.EMPLOYEE_ID _
                     And p.HIGHEST_LEVEL = -1 _
                     And p.ID <> objValidate.ID
                         Select New EmployeeTrainDTO With {
                        .ID = p.ID,
                        .EMPLOYEE_ID = p.EMPLOYEE_ID})
            Else
                query = (From p In Context.HU_EMPLOYEE_TRAIN Where p.EMPLOYEE_ID = objValidate.EMPLOYEE_ID _
                     And p.HIGHEST_LEVEL = -1
                         Select New EmployeeTrainDTO With {
                        .ID = p.ID,
                        .EMPLOYEE_ID = p.EMPLOYEE_ID})
            End If

            lstEmpTrain = query.ToList
            If lstEmpTrain.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeTrain(ByVal _filter As EmployeeTrainDTO) As List(Of EmployeeTrainDTO)
        Dim query As ObjectQuery(Of EmployeeTrainDTO)
        Try
            query = (From p In Context.HU_EMPLOYEE_TRAIN
                     From q In Context.OT_OTHER_LIST.Where(Function(f) p.TRAINING_FORM = f.ID).DefaultIfEmpty
                     From o In Context.OT_OTHER_LIST.Where(Function(f) p.LEARNING_LEVEL = f.ID).DefaultIfEmpty
                     From r In Context.OT_OTHER_LIST.Where(Function(f) p.MAJOR = f.ID).DefaultIfEmpty
                     From s In Context.OT_OTHER_LIST.Where(Function(f) p.MARK = f.ID).DefaultIfEmpty
                     Select New EmployeeTrainDTO With {
                    .ID = p.ID,
                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                    .FROM_DATE = p.FROM_DATE,
                    .TO_DATE = p.TO_DATE,
                    .SCHOOL_NAME = p.SCHOOL_NAME,
                    .TRAINING_FORM = p.TRAINING_FORM,
                    .TRAINING_FORM_NAME = q.NAME_VN,
                    .HIGHEST_LEVEL = p.HIGHEST_LEVEL,
                    .LEARNING_LEVEL = p.LEARNING_LEVEL,
                    .LEARNING_LEVEL_NAME = o.NAME_VN,
                    .MAJOR = p.MAJOR,
                    .MAJOR_NAME = r.NAME_VN,
                    .GRADUATE_YEAR = p.GRADUATE_YEAR,
                    .MARK = p.MARK,
                    .MARK_NAME = s.NAME_VN,
                    .TRAINING_CONTENT = p.TRAINING_CONTENT,
                    .CREATED_DATE = p.CREATED_DATE,
                    .CREATED_BY = p.CREATED_BY,
                    .CREATED_LOG = p.CREATED_LOG,
                    .MODIFIED_DATE = p.MODIFIED_DATE,
                    .MODIFIED_BY = p.MODIFIED_BY,
                    .MODIFIED_LOG = p.MODIFIED_LOG})
            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If
            'If _filter.HIGHEST_LEVEL = True Or _filter.HIGHEST_LEVEL = False Then
            '    query = query.Where(Function(p) p.HIGHEST_LEVEL = _filter.HIGHEST_LEVEL)
            'End If 
            Dim ret = query.ToList
            For Each item As EmployeeTrainDTO In ret
                If item.FROM_DATE.HasValue Then
                    item.FMONTH = item.FROM_DATE.Value.Month
                    item.FYEAR = item.FROM_DATE.Value.Year
                End If
                If item.TO_DATE.HasValue Then
                    item.TMONTH = item.TO_DATE.Value.Month
                    item.TYEAR = item.TO_DATE.Value.Year
                End If
            Next

            Return ret
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeTrainByID(ByVal EmployeeID As Decimal) As EmployeeTrainDTO
        Dim query As EmployeeTrainDTO
        Try
            query = (From p In Context.HU_EMPLOYEE_TRAIN
                     From q In Context.OT_OTHER_LIST.Where(Function(f) p.TRAINING_FORM = f.ID).DefaultIfEmpty
                     From o In Context.OT_OTHER_LIST.Where(Function(f) p.LEARNING_LEVEL = f.ID).DefaultIfEmpty
                     From r In Context.OT_OTHER_LIST.Where(Function(f) p.MAJOR = f.ID).DefaultIfEmpty
                     From s In Context.OT_OTHER_LIST.Where(Function(f) p.MARK = f.ID).DefaultIfEmpty
                     Where p.EMPLOYEE_ID = EmployeeID
                     Select New EmployeeTrainDTO With {
                    .ID = p.ID,
                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                    .FROM_DATE = p.FROM_DATE,
                    .TO_DATE = p.TO_DATE,
                    .SCHOOL_NAME = p.SCHOOL_NAME,
                    .TRAINING_FORM = p.TRAINING_FORM,
                    .TRAINING_FORM_NAME = q.NAME_VN,
                    .HIGHEST_LEVEL = p.HIGHEST_LEVEL,
                    .LEARNING_LEVEL = p.LEARNING_LEVEL,
                    .LEARNING_LEVEL_NAME = o.NAME_VN,
                    .MAJOR = p.MAJOR,
                    .MAJOR_NAME = r.NAME_VN,
                    .GRADUATE_YEAR = p.GRADUATE_YEAR,
                    .MARK = p.MARK,
                    .MARK_NAME = s.NAME_VN,
                    .TRAINING_CONTENT = p.TRAINING_CONTENT,
                    .CREATED_DATE = p.CREATED_DATE,
                    .CREATED_BY = p.CREATED_BY,
                    .CREATED_LOG = p.CREATED_LOG,
                    .MODIFIED_DATE = p.MODIFIED_DATE,
                    .MODIFIED_BY = p.MODIFIED_BY,
                    .MODIFIED_LOG = p.MODIFIED_LOG}).SingleOrDefault
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteEmployeeTrain(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_EMPLOYEE_TRAIN)
        Try
            lst = (From p In Context.HU_EMPLOYEE_TRAIN Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_EMPLOYEE_TRAIN.DeleteObject(lst(i))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region

#Region "WorkingBefore"
    Public Function GetEmpWorkingBefore(ByVal _filter As WorkingBeforeDTO) As List(Of WorkingBeforeDTO)
        Dim query As ObjectQuery(Of WorkingBeforeDTO)
        Try
            query = (From p In Context.HU_WORKING_BEFORE
                     Select New WorkingBeforeDTO With {
                    .ID = p.ID,
                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                    .COMPANY_NAME = p.COMPANY_NAME,
                    .COMPANY_ADDRESS = p.COMPANY_ADDRESS,
                    .TELEPHONE = p.TELEPHONE,
                    .JOIN_DATE = p.JOIN_DATE,
                    .END_DATE = p.END_DATE,
                    .SALARY = p.SALARY,
                    .TITLE_NAME = p.TITLE_NAME,
                    .LEVEL_NAME = p.LEVEL_NAME,
                    .TER_REASON = p.REMARK,
                    .DEPARTMENT = p.DEPARTMENT})
            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertWorkingBefore(ByVal objWorkingBefore As WorkingBeforeDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objWorkingBeforeData As New HU_WORKING_BEFORE
            objWorkingBeforeData.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING_BEFORE.EntitySet.Name)
            objWorkingBeforeData.EMPLOYEE_ID = objWorkingBefore.EMPLOYEE_ID
            objWorkingBeforeData.COMPANY_ADDRESS = objWorkingBefore.COMPANY_ADDRESS
            objWorkingBeforeData.COMPANY_NAME = objWorkingBefore.COMPANY_NAME
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.SALARY = objWorkingBefore.SALARY
            objWorkingBeforeData.TELEPHONE = objWorkingBefore.TELEPHONE
            'đang có sãn trường lý do nên làm nhanh update vao cho remark lun
            objWorkingBeforeData.TER_REASON = objWorkingBefore.TER_REASON
            objWorkingBeforeData.REMARK = objWorkingBefore.TER_REASON
            objWorkingBeforeData.LEVEL_NAME = objWorkingBefore.LEVEL_NAME
            objWorkingBeforeData.TITLE_NAME = objWorkingBefore.TITLE_NAME
            objWorkingBeforeData.JOIN_DATE = objWorkingBefore.JOIN_DATE
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.CREATED_DATE = DateTime.Now
            objWorkingBeforeData.CREATED_BY = log.Username
            objWorkingBeforeData.CREATED_LOG = log.ComputerName
            objWorkingBeforeData.MODIFIED_DATE = DateTime.Now
            objWorkingBeforeData.MODIFIED_BY = log.Username
            objWorkingBeforeData.MODIFIED_LOG = log.ComputerName
            objWorkingBeforeData.IS_HSV = objWorkingBefore.IS_HSV
            objWorkingBeforeData.TITLE_NAME = objWorkingBefore.TITLE_NAME_BEFORE
            objWorkingBeforeData.WORK = objWorkingBefore.WORK
            objWorkingBeforeData.THAM_NIEN = objWorkingBefore.THAM_NIEN
            objWorkingBeforeData.IS_THAMNIEN = objWorkingBefore.IS_THAMNIEN
            objWorkingBeforeData.DEPARTMENT = objWorkingBefore.DEPARTMENT
            objWorkingBeforeData.FILE_NAME = objWorkingBefore.FILE_NAME
            Context.HU_WORKING_BEFORE.AddObject(objWorkingBeforeData)
            Context.SaveChanges(log)
            gID = objWorkingBeforeData.ID
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyWorkingBefore(ByVal objWorkingBefore As WorkingBeforeDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWorkingBeforeData As New HU_WORKING_BEFORE With {.ID = objWorkingBefore.ID}
        Try
            objWorkingBeforeData = (From p In Context.HU_WORKING_BEFORE Where p.ID = objWorkingBefore.ID).FirstOrDefault
            objWorkingBeforeData.EMPLOYEE_ID = objWorkingBefore.EMPLOYEE_ID
            objWorkingBeforeData.COMPANY_ADDRESS = objWorkingBefore.COMPANY_ADDRESS
            objWorkingBeforeData.COMPANY_NAME = objWorkingBefore.COMPANY_NAME
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.SALARY = objWorkingBefore.SALARY
            objWorkingBeforeData.TELEPHONE = objWorkingBefore.TELEPHONE
            'đang có sãn trường lý do nên làm nhanh update vao cho remark lun
            objWorkingBeforeData.TER_REASON = objWorkingBefore.TER_REASON
            objWorkingBeforeData.REMARK = objWorkingBefore.TER_REASON
            objWorkingBeforeData.LEVEL_NAME = objWorkingBefore.LEVEL_NAME
            objWorkingBeforeData.TITLE_NAME = objWorkingBefore.TITLE_NAME
            objWorkingBeforeData.JOIN_DATE = objWorkingBefore.JOIN_DATE
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.MODIFIED_DATE = DateTime.Now
            objWorkingBeforeData.MODIFIED_BY = log.Username
            objWorkingBeforeData.MODIFIED_LOG = log.ComputerName
            objWorkingBeforeData.IS_HSV = objWorkingBefore.IS_HSV
            objWorkingBeforeData.TITLE_NAME = objWorkingBefore.TITLE_NAME_BEFORE
            objWorkingBeforeData.WORK = objWorkingBefore.WORK
            objWorkingBeforeData.THAM_NIEN = objWorkingBefore.THAM_NIEN
            objWorkingBeforeData.IS_THAMNIEN = objWorkingBefore.IS_THAMNIEN
            objWorkingBeforeData.DEPARTMENT = objWorkingBefore.DEPARTMENT
            objWorkingBeforeData.FILE_NAME = objWorkingBefore.FILE_NAME
            Context.SaveChanges(log)
            gID = objWorkingBeforeData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteWorkingBefore(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_WORKING_BEFORE)
        Try
            lst = (From p In Context.HU_WORKING_BEFORE Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_WORKING_BEFORE.DeleteObject(lst(i))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#End Region

#Region "Employee Proccess"

    ''' <summary>
    ''' Lấy danh sách nhân thân
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFamily(ByVal _empId As Decimal) As List(Of FamilyDTO)
        Try
            Dim query As List(Of FamilyDTO)
            query = (From p In Context.HU_FAMILY
                     From r In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RELATION_ID).DefaultIfEmpty
                     Where p.EMPLOYEE_ID = _empId
                     Order By p.CREATED_DATE
                     Select New FamilyDTO With {
                     .RELATION_ID = p.RELATION_ID,
                     .RELATION_NAME = r.NAME_VN,
                     .FULLNAME = p.FULLNAME,
                     .BIRTH_DATE = p.BIRTH_DATE,
                     .ID_NO = p.ID_NO,
                     .DEDUCT_REG = p.DEDUCT_REG,
                     .IS_DEDUCT = p.IS_DEDUCT,
                     .DEDUCT_FROM = p.DEDUCT_FROM,
                     .DEDUCT_TO = p.DEDUCT_TO,
                     .ADDRESS = p.ADDRESS,
                     .REMARK = p.REMARK,
                     .CREATED_DATE = p.CREATED_DATE}).ToList
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình công tác trước khi vào công ty
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWorkingBefore(ByVal _empId As Decimal) As List(Of WorkingBeforeDTO)
        Try
            Dim query As List(Of WorkingBeforeDTO)
            query = (From p In Context.HU_WORKING_BEFORE
                     From o In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_NAME).DefaultIfEmpty
                     Where p.EMPLOYEE_ID = _empId
                     Order By p.JOIN_DATE
                     Select New WorkingBeforeDTO With {
                         .ID = p.ID,
                         .EMPLOYEE_ID = p.EMPLOYEE_ID,
                         .COMPANY_NAME = p.COMPANY_NAME,
                         .DEPARTMENT = p.DEPARTMENT,
                         .COMPANY_ADDRESS = p.COMPANY_ADDRESS,
                         .TELEPHONE = p.TELEPHONE,
                         .JOIN_DATE = p.JOIN_DATE,
                         .END_DATE = p.END_DATE,
                         .SALARY = p.SALARY,
                         .TITLE_NAME = p.TITLE_NAME,
                         .LEVEL_NAME = p.LEVEL_NAME,
                         .TER_REASON = p.TER_REASON,
                         .IS_HSV = p.IS_HSV,
                         .THAM_NIEN = p.THAM_NIEN,
                         .FILE_NAME = p.FILE_NAME,
                         .UPLOAD_FILE = o.NAME,
                         .WORK = p.WORK}).ToList()
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình công tác trong công ty, bao gồm quá trình công tác trước khi dùng phần mềm vào sau khi dùng phần mềm.
    ''' </summary>
    ''' <param name="_empCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWorkingProccess(ByVal _empId As Decimal?,
                                       Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_EMP_3B",
                                 New With {.P_USERNAME = log.Username,
                                           .P_EMPLOYEE_ID = _empId})
            End Using

            Dim query = From p In Context.HU_WORKING
                        From chosen In Context.SE_CHOSEN_EMP_3B.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And
                                                                          f.USERNAME = log.Username.ToUpper)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) p.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From staffRank In Context.HU_JOB_BAND.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DECISION_TYPE_ID).DefaultIfEmpty
                        From obj_att In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.OBJECT_ATTENDANCE).DefaultIfEmpty
                        Where p.IS_MISSION = True And p.IS_PROCESS <> 0 And
                        p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                        Order By p.EFFECT_DATE Descending
                        Select New WorkingDTO With {.ID = p.ID,
                                                    .DECISION_NO = p.DECISION_NO,
                                                    .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                                                    .DECISION_TYPE_NAME = deci_type.NAME_VN,
                                                    .OBJECT_ATTENDANCE = p.OBJECT_ATTENDANCE,
                                                    .OBJECT_ATTENDANCE_NAME = obj_att.NAME_VN,
                                                    .FILING_DATE = p.FILING_DATE,
                                                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                    .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                    .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                    .ORG_NAME2 = o.ORG_NAME2,
                                                    .ORG_NAME = o.NAME_VN,
                                                    .ORG_DESC = o.DESCRIPTION_PATH,
                                                    .TITLE_NAME = t.NAME_VN,
                                                    .EFFECT_DATE = p.EFFECT_DATE,
                                                    .EXPIRE_DATE = p.EXPIRE_DATE,
                                                    .STATUS_ID = p.STATUS_ID,
                                                    .STATUS_NAME = status.NAME_VN,
                                                    .SAL_BASIC = p.SAL_BASIC,
                                                    .IS_MISSION = p.IS_MISSION,
                                                    .IS_WAGE = p.IS_WAGE,
                                                    .IS_PROCESS = p.IS_PROCESS,
                                                    .IS_MISSION_SHORT = p.IS_MISSION,
                                                    .IS_WAGE_SHORT = p.IS_WAGE,
                                                    .IS_PROCESS_SHORT = p.IS_PROCESS,
                                                    .SAL_GROUP_ID = p.SAL_GROUP_ID,
                                                    .SAL_GROUP_NAME = sal_group.NAME,
                                                    .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                                                    .SAL_LEVEL_NAME = sal_level.NAME,
                                                    .SAL_RANK_ID = p.SAL_RANK_ID,
                                                    .SAL_RANK_NAME = sal_rank.RANK,
                                                    .SIGN_DATE = p.SIGN_DATE,
                                                    .SIGN_NAME = p.SIGN_NAME,
                                                    .SIGN_TITLE = p.SIGN_TITLE,
                                                    .STAFF_RANK_NAME = staffRank.LEVEL_FROM,
                                                    .COST_SUPPORT = p.COST_SUPPORT,
                                                    .CREATED_DATE = p.CREATED_DATE}


            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình lương
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSalaryProccess(ByVal _empId As Decimal,
                                       Optional ByVal log As UserLog = Nothing) As List(Of WorkingDTO)
        Try


            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_EMP_3B",
                                 New With {.P_USERNAME = log.Username,
                                           .P_EMPLOYEE_ID = _empId})
            End Using

            Dim query = From p In Context.HU_WORKING
                        From chosen In Context.SE_CHOSEN_EMP_3B.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID And
                                                                      f.USERNAME = log.Username.ToUpper)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) p.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) p.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_FRAME_SALARY.Where(Function(f) p.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_FRAME_SALARY.Where(Function(f) sal_rank.CODE_NGACHLUONG.ToUpper.Equals(f.CODE.ToUpper)).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From deci_type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DECISION_TYPE_ID).DefaultIfEmpty
                        From taxTable In Context.PA_FRAME_SALARY.Where(Function(f) sal_rank.PARENT_ID = f.ID AndAlso f.IS_LEVEL2 = -1).DefaultIfEmpty
                        From staffrak In Context.HU_STAFF_RANK.Where(Function(f) f.ID = p.STAFF_RANK_ID).DefaultIfEmpty
                        Where p.IS_WAGE = True And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                        Order By p.EFFECT_DATE Descending
                        Select New WorkingDTO With {.ID = p.ID,
                                                    .DECISION_NO = p.DECISION_NO,
                                                    .DECISION_TYPE_ID = p.DECISION_TYPE_ID,
                                                    .DECISION_TYPE_NAME = deci_type.NAME_VN,
                                                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                    .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                    .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                    .ORG_NAME = o.NAME_VN,
                                                     .SAL_TYPE_NAME = sal_type.NAME,
                                                    .ORG_DESC = o.DESCRIPTION_PATH,
                                                    .TITLE_NAME = t.NAME_VN,
                                                    .EFFECT_DATE = p.EFFECT_DATE,
                                                    .EXPIRE_DATE = p.EXPIRE_DATE,
                                                    .STATUS_ID = p.STATUS_ID,
                                                    .STATUS_NAME = status.NAME_VN,
                                                    .STAFF_RANK_ID = p.STAFF_RANK_ID,
                                                    .STAFF_RANK_NAME = staffrak.NAME,
                                                    .SAL_BASIC = p.SAL_BASIC,
                                                    .SALARY_BHXH = p.SALARY_BHXH,
                                                    .IS_MISSION = p.IS_MISSION,
                                                    .IS_WAGE = p.IS_WAGE,
                                                      .TAX_TABLE_Name = taxTable.NAME_VN,
                                                    .IS_PROCESS = p.IS_PROCESS,
                                                    .IS_MISSION_SHORT = p.IS_MISSION,
                                                    .IS_WAGE_SHORT = p.IS_WAGE,
                                                    .IS_PROCESS_SHORT = p.IS_PROCESS,
                                                    .SAL_GROUP_ID = p.SAL_GROUP_ID,
                                                    .SAL_GROUP_NAME = sal_group.NAME,
                                                    .SAL_LEVEL_ID = p.SAL_LEVEL_ID,
                                                    .SAL_LEVEL_NAME = sal_level.NAME_VN,
                                                    .SAL_RANK_ID = p.SAL_RANK_ID,
                                                    .SAL_RANK_NAME = sal_rank.NAME_VN,
                                                    .TOXIC_RATE = p.TOXIC_RATE,
                                                    .TOXIC_SALARY = p.TOXIC_SALARY,
                                                    .COEFFICIENT = sal_rank.COEFFICIENT,
                                                    .SIGN_DATE = p.SIGN_DATE,
                                                    .SIGN_NAME = p.SIGN_NAME,
                                                    .SIGN_TITLE = p.SIGN_TITLE,
                                                    .OTHERSALARY1 = p.OTHERSALARY1,
                                                    .OTHERSALARY2 = p.OTHERSALARY2,
                                                    .OTHERSALARY3 = p.OTHERSALARY3,
                                                    .OTHERSALARY4 = p.OTHERSALARY4,
                                                    .OTHERSALARY5 = p.OTHERSALARY5,
                                                    .PERCENTSALARY = p.PERCENTSALARY,
                                                    .FACTORSALARY = p.FACTORSALARY,
                                                    .SAL_TOTAL = p.SAL_TOTAL + If((From a In Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = p.ID) Select a.AMOUNT).Sum Is Nothing, 0, (From a In Context.HU_WORKING_ALLOW.Where(Function(f) f.HU_WORKING_ID = p.ID) Select a.AMOUNT).Sum),
                                                    .COST_SUPPORT = p.COST_SUPPORT,
                                                    .SHARE_SAL = p.SHARE_SAL,
                                                    .PERCENT_SALARY = p.PERCENT_SALARY,
                                                    .CREATED_DATE = p.CREATED_DATE,
                                                    .SAL_INS = p.SAL_INS}

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình phúc lợi
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWelfareProccess(ByVal _empId As Decimal) As List(Of WelfareMngDTO)
        Try
            Dim query As List(Of WelfareMngDTO)
            query = (From p In Context.HU_WELFARE_MNG
                     From l In Context.HU_WELFARE_LIST.Where(Function(l) l.ID = p.WELFARE_ID)
                     Where p.EMPLOYEE_ID = _empId Order By p.EFFECT_DATE
                     Select New WelfareMngDTO With {
                     .WELFARE_NAME = l.NAME,
                     .EFFECT_DATE = p.EFFECT_DATE,
                     .EXPIRE_DATE = p.EXPIRE_DATE,
                     .MONEY = p.MONEY}).ToList()
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình hợp đồng
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractProccess(ByVal _empId As Decimal) As List(Of ContractDTO)
        Try
            Dim query As List(Of ContractDTO)
            query = (From p In Context.HU_CONTRACT
                     From t In Context.HU_CONTRACT_TYPE.Where(Function(t) t.ID = p.CONTRACT_TYPE_ID)
                     From lo In Context.HU_LOCATION.Where(Function(f) f.ID = p.COMPANY_REG).DefaultIfEmpty
                     Where p.EMPLOYEE_ID = _empId _
                     And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Order By p.START_DATE
                     Select New ContractDTO With {
                     .CONTRACTTYPE_NAME = t.NAME,
                     .START_DATE = p.START_DATE,
                     .EXPIRE_DATE = p.EXPIRE_DATE,
                     .CONTRACT_NO = p.CONTRACT_NO,
                     .SIGNER_NAME = p.SIGNER_NAME,
                     .SIGNER_TITLE = p.SIGNER_TITLE,
                     .SIGN_DATE = p.SIGN_DATE,
                     .COMPANY_REG_NAME = lo.LOCATION_VN_NAME}).ToList()
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình khen thưởng
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCommendProccess(ByVal _empId As Decimal) As List(Of CommendDTO)
        Try
            Dim query As List(Of CommendDTO)
            query = (From p In Context.HU_COMMEND
                     From ce In Context.HU_COMMEND_EMP.Where(Function(ce) ce.HU_COMMEND_ID = p.ID).DefaultIfEmpty
                     From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ce.HU_EMPLOYEE_ID).DefaultIfEmpty
                     From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = emp.ORG_ID).DefaultIfEmpty
                     From lv In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMMEND_LEVEL).DefaultIfEmpty
                     From t In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMMEND_TYPE).DefaultIfEmpty
                     From title In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                     From dhkt In Context.HU_COMMEND_LIST.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                     From com_obj In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMMEND_OBJ).DefaultIfEmpty
                     From httt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMMEND_PAY And f.TYPE_CODE = "COMMEND_PAY" And f.ACTFLG = "A").DefaultIfEmpty
                     Where (ce.HU_EMPLOYEE_ID = _empId And p.STATUS_ID = 714)
                     Order By p.EFFECT_DATE
                     Select New CommendDTO With {
                     .NO = p.NO,
                     .DECISION_NO = p.NO,
                     .EFFECT_DATE = p.EFFECT_DATE,
                     .EXPIRE_DATE = p.EXPIRE_DATE,
                     .TITLE_NAME = title.NAME_VN,
                     .COMMEND_LEVEL_NAME = lv.NAME_VN,
                     .ORG_NAME = o.NAME_VN,
                     .COMMEND_TYPE_NAME = t.NAME_VN,
                     .REMARK = p.REMARK,
                     .MONEY = ce.MONEY,
                     .COMMEND_TITLE_NAME = dhkt.NAME,
                     .YEAR = p.YEAR,
                     .COMMEND_PAY_NAME = httt.NAME_VN,
                     .SIGNER_NAME = p.SIGNER_NAME,
                     .SIGNER_TITLE = p.SIGNER_TITLE,
                     .SIGN_DATE = p.SIGN_DATE,
                     .COMMEND_OBJ_NAME = com_obj.NAME_VN,
                     .NOTE = p.NOTE}).ToList()

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình kỷ luật
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDisciplineProccess(ByVal _empId As Decimal) As List(Of DisciplineDTO)
        Try
            Dim query As List(Of DisciplineDTO)
            query = (From p In Context.HU_DISCIPLINE
                     From de In Context.HU_DISCIPLINE_EMP.Where(Function(de) de.HU_DISCIPLINE_ID = p.ID)
                     From lv In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DISCIPLINE_LEVEL).DefaultIfEmpty
                     From t In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DISCIPLINE_TYPE).DefaultIfEmpty
                     From emp In Context.HU_EMPLOYEE.Where(Function(f) f.ID = de.HU_EMPLOYEE_ID)
                     From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = emp.ORG_ID)
                     From title In Context.HU_TITLE.Where(Function(f) f.ID = emp.TITLE_ID).DefaultIfEmpty
                     From obj In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DISCIPLINE_OBJ).DefaultIfEmpty
                     From type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DISCIPLINE_TYPE).DefaultIfEmpty
                     From level In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEVEL_ID).DefaultIfEmpty
                     From reason In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.DISCIPLINE_REASON And f.ACTFLG = "A").DefaultIfEmpty
                     From c In Context.SE_USER.Where(Function(f) f.USERNAME = p.CREATED_BY).DefaultIfEmpty
                     From m In Context.SE_USER.Where(Function(f) f.USERNAME = p.MODIFIED_BY).DefaultIfEmpty
                     Where de.HU_EMPLOYEE_ID = _empId And p.STATUS_ID = ProfileCommon.OT_DISCIPLINE_STATUS.APPROVE_ID Order By p.EFFECT_DATE
                     Select New DisciplineDTO With {
                     .NO = p.NO,
                     .DECISION_NO = p.NO,
                     .EFFECT_DATE = p.EFFECT_DATE,
                     .DISCIPLINE_DATE = p.EFFECT_DATE,
                     .EXPIRE_DATE = p.EXPIRE_DATE,
                     .DISCIPLINE_LEVEL_NAME = lv.NAME_VN,
                     .TITLE_NAME = title.NAME_VN,
                     .ORG_NAME = o.NAME_VN,
                     .DISCIPLINE_TYPE_NAME = t.NAME_VN,
                     .MONEY = de.MONEY,
                     .YEAR = p.YEAR,
                     .DISCIPLINE_OBJ_NAME = obj.NAME_VN,
                     .SIGN_DATE = p.SIGN_DATE,
                     .DISCIPLINE_REASON_NAME = reason.NAME_VN,
                     .PERFORM_TIME = p.PERFORM_TIME,
                     .VIOLATION_DATE = p.VIOLATION_DATE,
                     .LEVEL_NAME = level.NAME_VN,
                     .DISCIPLINE_REASON_DETAIL = p.DISCIPLINE_REASON_DETAIL,
                     .CREATED_BY = c.FULLNAME,
                     .CREATED_DATE = p.CREATED_DATE,
                     .MODIFIED_BY = m.FULLNAME,
                     .MODIFIED_DATE = p.MODIFIED_DATE}).ToList()
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetConcurrentlyProccess(ByVal _empId As Decimal) As List(Of TitleConcurrentDTO)
        Try
            Dim query As List(Of TitleConcurrentDTO)
            query = (From p In Context.HU_TITLE_CONCURRENT
                     From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                     Where p.EMPLOYEE_ID = _empId Order By p.EFFECT_DATE Descending
                     Select New TitleConcurrentDTO With {
                      .ID = p.ID,
                                   .ORG_ID = p.ORG_ID,
                                   .ORG_NAME = org.NAME_VN,
                                   .TITLE_ID = p.TITLE_ID,
                                   .NAME = p.NAME,
                                   .DECISION_NO = p.DECISION_NO,
                                   .EFFECT_DATE = p.EFFECT_DATE,
                                   .EXPIRE_DATE = p.EXPIRE_DATE,
                                   .NOTE = p.NOTE,
                                   .CREATED_DATE = p.CREATED_DATE}).ToList()
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy quá trình đóng bảo hiểm
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInsuranceProccess(ByVal _empId As Decimal) As DataTable
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.INSURANCE_PROCESSS",
                                                    New With {.P_EMPLOYEEID = _empId,
                                                                .CUR = cls.OUT_CURSOR})
                Return dtData
            End Using

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetEmployeeHistory(ByVal _empId As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_EMP_HISTORY",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Lấy Qua trinh danh gia KPI
    ''' </summary>
    ''' <param name="_empId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAssessKPIEmployee(ByVal _empId As Decimal) As List(Of EmployeeAssessmentDTO)
        Try
            Dim lst As List(Of EmployeeAssessmentDTO) = New List(Of EmployeeAssessmentDTO)
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PERFORMANCE_BUSINESS.GET_PE_ASSESS_EMP_BY_EMP",
                                           New With {.P_EMPLOYEE_ID = _empId,
                                                    .P_CUR = cls.OUT_CURSOR})
                If dtData IsNot Nothing Then
                    lst = (From row As DataRow In dtData.Rows
                           Select New EmployeeAssessmentDTO With {.ID = row("ID").ToString(),
                                                       .EMPLOYEE_ID = row("EMPLOYEE_ID").ToString(),
                                                       .EMPLOYEE_CODE = row("EMPLOYEE_CODE").ToString(),
                                                       .EMPLOYEE_NAME = row("EMPLOYEE_NAME").ToString(),
                                                       .PE_PERIO_YEAR = row("PE_PERIO_YEAR").ToString(),
                                                       .PE_PERIO_NAME = row("PE_PERIO_NAME").ToString(),
                                                        .PE_PERIO_TYPE_ASS = row("PE_PERIO_TYPE_ASS").ToString(),
                                                       .PE_PERIO_TYPE_ASS_NAME = row("PE_PERIO_TYPE_ASS_NAME").ToString(),
                                                       .PE_PERIO_START_DATE = ToDate(row("PE_PERIO_START_DATE")),
                                                       .PE_PERIO_END_DATE = ToDate(row("PE_PERIO_END_DATE")),
                                                       .RESULT_CONVERT = row("RESULT_CONVERT").ToString(),
                                                       .PE_STATUS_ID = row("PE_STATUS_ID").ToString(),
                                                       .PE_STATUS_NAME = row("PE_STATUS_NAME").ToString(),
                                                        .CLASSIFICATION_NAME = row("CLASSIFICATION_NAME").ToString()
                                                      }).Where(Function(f) f.PE_STATUS_ID = ProfileCommon.OT_PEASSESSMENT.STATUS_ASS).ToList
                End If
            End Using

            Return lst.OrderByDescending(Function(f) f.PE_PERIO_END_DATE)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    'Qua trinh nang luc
    Public Function GetCompetencyEmployee(ByVal _empId As Decimal) As List(Of EmployeeCompetencyDTO)
        Try
            Dim query = From stand In Context.HU_COMPETENCY_STANDARD
                        From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = stand.COMPETENCY_ID)
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = Competency.COMPETENCY_GROUP_ID)
                        From ass In Context.HU_COMPETENCY_ASS.Where(Function(f) stand.TITLE_ID = f.TITLE_ID).DefaultIfEmpty
                        From p In Context.HU_COMPETENCY_ASSDTL.Where(Function(f) f.COMPETENCY_ASS_ID = ass.ID And
                                                                      f.COMPETENCY_ID = stand.COMPETENCY_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ass.EMPLOYEE_ID).DefaultIfEmpty
                        From period In Context.HU_COMPETENCY_PERIOD.Where(Function(f) f.ID = ass.COMPETENCY_PERIOD_ID).DefaultIfEmpty
                        Where ass.EMPLOYEE_ID = _empId
                        Select New EmployeeCompetencyDTO With {
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EMPLOYEE_ID = ass.EMPLOYEE_ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .TITLE_ID = stand.TITLE_ID,
                            .COMPETENCY_GROUP_ID = Competency.COMPETENCY_GROUP_ID,
                            .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                            .COMPETENCY_PERIOD_ID = period.ID,
                            .COMPETENCY_PERIOD_NAME = period.NAME,
                            .COMPETENCY_PERIOD_YEAR = period.YEAR,
                            .COMPETENCY_ID = stand.COMPETENCY_ID,
                            .COMPETENCY_NAME = Competency.NAME,
                            .LEVEL_NUMBER_STANDARD = stand.LEVEL_NUMBER,
                            .LEVEL_NUMBER_EMP = p.LEVEL_NUMBER,
                            .REMARK = p.REMARK,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GetCompetencyEmployee")
            Throw ex
        End Try
    End Function


#End Region

#Region "Qua trinh dao tao trong cong ty"
    Public Function GetEmployeeTrainForCompany(ByVal _filter As EmployeeTrainForCompanyDTO) As List(Of EmployeeTrainForCompanyDTO)
        Try
            Dim query = From re In Context.TR_REQUEST_EMPLOYEE
                        From r In Context.TR_REQUEST.Where(Function(f) f.ID = re.TR_REQUEST_ID).DefaultIfEmpty
                        From c In Context.TR_COURSE.Where(Function(f) f.ID = r.TR_COURSE_ID).DefaultIfEmpty
                        From ce In Context.TR_CERTIFICATE.Where(Function(f) f.ID = c.TR_CERTIFICATE_ID)
                        From pr In Context.TR_PROGRAM.Where(Function(f) f.TR_REQUEST_ID = r.ID).DefaultIfEmpty
                        From prg In Context.TR_PROGRAM_GROUP.Where(Function(f) f.ID = c.TR_PROGRAM_GROUP).DefaultIfEmpty
                        From tf In Context.OT_OTHER_LIST.Where(Function(f) f.ID = c.TR_TRAIN_FIELD).DefaultIfEmpty
                        From tfr In Context.OT_OTHER_LIST.Where(Function(f) f.ID = r.TRAIN_FORM_ID).DefaultIfEmpty
                        From lang In Context.OT_OTHER_LIST.Where(Function(f) f.ID = pr.TR_LANGUAGE_ID).DefaultIfEmpty
                        From result In Context.TR_PROGRAM_RESULT.Where(Function(f) f.TR_PROGRAM_ID = pr.ID And f.EMPLOYEE_ID = _filter.EMPLOYEE_ID).DefaultIfEmpty
                        From rank In Context.OT_OTHER_LIST.Where(Function(f) f.ID = result.TR_RANK_ID).DefaultIfEmpty
                        From pcomit In Context.TR_PROGRAM_COMMIT.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID And f.TR_PROGRAM_ID = pr.ID).DefaultIfEmpty
                        Where re.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                        Order By re.ID Descending

            Dim lst = query.Select(Function(p) New EmployeeTrainForCompanyDTO With {
                                         .ID = _filter.EMPLOYEE_ID,
                                         .EMPLOYEE_ID = _filter.EMPLOYEE_ID,
                                         .TR_COURSE_ID = p.r.TR_COURSE_ID,
                                         .TR_COURSE_NAME = p.c.NAME,
                                         .TR_PROGRAM_ID = p.pr.ID,
                                         .TR_PROGRAM_NAME = p.pr.NAME,
                                         .TR_PROGRAM_GROUP_ID = p.prg.ID,
                                         .TR_PROGRAM_GROUP_NAME = p.prg.NAME,
                                         .FIELDS_ID = p.tf.ID,
                                         .TR_TRAIN_FIELD_NAME = p.tf.NAME_VN,
                                         .TR_TRAIN_FORM_ID = p.r.TRAIN_FORM_ID,
                                         .TR_TRAIN_FORM_NAME = p.tfr.NAME_VN,
                                         .DURATION = p.pr.DURATION,
                                         .START_DATE = p.pr.START_DATE,
                                         .END_DATE = p.pr.END_DATE,
                                         .DURATION_HC = p.pr.DURATION_HC,
                                         .DURATION_OT = p.pr.DURATION_OT,
                                         .COST_TOTAL = p.pr.COST_TOTAL,
                                         .COST_OF_STUDENT = p.pr.COST_STUDENT,
                                         .COST_TOTAL_USD = p.pr.COST_TOTAL_US,
                                         .COST_OF_STUDENT_USD = p.pr.COST_STUDENT_US,
                                         .NO_OF_STUDENT = p.pr.STUDENT_NUMBER,
                                         .IS_REIMBURSE = p.pr.IS_REIMBURSE,
                                         .TR_LANGUAGE_ID = p.pr.TR_LANGUAGE_ID,
                                         .TR_LANGUAGE_NAME = p.lang.NAME_VN,
                                         .TR_UNIT_NAME = p.pr.CENTERS,
                                         .CONTENT = p.pr.CONTENT,
                                         .TARGET_TRAIN = p.pr.TARGET_TRAIN,
                                         .VENUE = p.pr.VENUE,
                                         .IS_EXAMS = If(p.result.RETEST_SCORE Is Nothing, "Không", "Có"),
                                         .IS_END = If(p.result.IS_END = -1, True, False),
                                         .IS_REACH = If(p.result.IS_REACH = -1, "Đạt", "Không đạt"),
                                         .IS_CERTIFICATE = p.result.IS_CERTIFICATE,
                                         .CERTIFICATE_NO = p.result.CERTIFICATE_NO,
                                         .CERTIFICATE_DATE = p.result.CERTIFICATE_DATE,
                                         .CER_RECEIVE_DATE = p.result.CER_RECEIVE_DATE,
                                         .CERTIFICATE_DURATION = p.ce.DURATION,
                                         .COMITMENT_TRAIN_NO = p.pcomit.COMMIT_NO,
                                         .COMMIT_WORK = p.result.COMMIT_WORKMONTH,
                                         .COMITMENT_START_DATE = p.result.COMMIT_STARTDATE,
                                         .COMITMENT_END_DATE = p.result.COMMIT_ENDDATE,
                                         .RANK_ID = p.rank.ID,
                                         .RANK_NAME = p.rank.NAME_VN,
                                         .TOEIC_FINAL_SCORE = p.result.FINAL_SCORE,
                                        .REMARK = p.r.REMARK})
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "quá trình đào tạo ngoài công ty "
    Public Function GetProcessTraining(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTO,
                                Optional ByRef PageIndex As Integer = 0,
                                  Optional ByVal PageSize As Integer = Integer.MaxValue,
                                  Optional ByRef Total As Integer = 0,
                                  Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
        Try

            Dim query = From p In Context.HU_PRO_TRAIN_OUT_COMPANY
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                        From title In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.FORM_TRAIN_ID).DefaultIfEmpty
                        From ott In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.TYPE_TRAIN_ID).DefaultIfEmpty
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE).DefaultIfEmpty
                        From ot2 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE_GROUP_ID).DefaultIfEmpty
                        From ot3 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE_TYPE_ID).DefaultIfEmpty
                        From ot_train In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = ot.TYPE_ID And f.CODE = "TRAINING_FORM").DefaultIfEmpty
                        From ot_type In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = ott.TYPE_ID And f.CODE = "TRAINING_TYPE").DefaultIfEmpty
                        From ot_level In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEVEL_ID And f.TYPE_CODE = "LEARNING_LEVEL").DefaultIfEmpty
                        From tp In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TRAIN_PLACE).DefaultIfEmpty
                        From f In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_NAME).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.MAJOR).DefaultIfEmpty
                        From o2 In Context.OT_OTHER_LIST.Where(Function(x) x.ID = p.GRADUATE_SCHOOL).DefaultIfEmpty
            If IsNumeric(_filter.EMPLOYEE_ID) Then
                query = query.Where(Function(f) f.p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If IsNumeric(_filter.ID) Then
                query = query.Where(Function(f) f.p.ID = _filter.ID)
            End If

            Dim lst = query.Select(Function(p) New HU_PRO_TRAIN_OUT_COMPANYDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .ORG_ID = p.e.ORG_ID,
                                       .ORG_NAME = p.org.NAME_VN,
                                       .TITLE_NAME = p.title.NAME_VN,
                                       .TITLE_ID = p.e.TITLE_ID,
                                       .FROM_DATE = p.p.FROM_DATE,
                                       .TO_DATE = p.p.TO_DATE,
                                       .YEAR_GRA = p.p.YEAR_GRA,
                                       .NAME_SHOOLS = p.p.NAME_SHOOLS,
                                       .FORM_TRAIN_ID = p.p.FORM_TRAIN_ID,
                                       .FORM_TRAIN_NAME = p.ot.NAME_VN,
                                       .UPLOAD_FILE = p.f.NAME,
                                       .FILE_NAME = p.f.FILE_NAME,
                                       .SPECIALIZED_TRAIN = p.p.SPECIALIZED_TRAIN,
                                       .RESULT_TRAIN = p.p.RESULT_TRAIN,
                                       .CERTIFICATE = p.ot1.NAME_VN,
                                       .CERTIFICATE_ID = p.p.CERTIFICATE,
                                       .EFFECTIVE_DATE_FROM = p.p.EFFECTIVE_DATE_FROM,
                                       .EFFECTIVE_DATE_TO = p.p.EFFECTIVE_DATE_TO,
                                       .RECEIVE_DEGREE_DATE = p.p.RECEIVE_DEGREE_DATE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .IS_RENEWED = p.p.IS_RENEWED,
                                       .RENEWED_NAME = If(p.p.IS_RENEWED = 0, "Không", "Có"),
                                       .LEVEL_ID = p.p.LEVEL_ID,
                                       .LEVEL_NAME = p.ot_level.NAME_VN,
                                       .POINT_LEVEL = p.p.POINT_LEVEL,
                                       .CONTENT_LEVEL = p.p.CONTENT_LEVEL,
                                       .NOTE = p.p.NOTE,
                                       .CERTIFICATE_CODE = p.p.CERTIFICATE_CODE,
                                       .TYPE_TRAIN_NAME = p.p.TYPE_TRAIN_NAME,
                                       .CERTIFICATE_NAME = p.p.CERTIFICATE_NAME,
                                       .COMMITMENT_TIME = p.p.COMMITMENT_TIME,
                                       .TRAIN_PLACE = p.p.TRAIN_PLACE,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LASTDATE = p.e.TER_LAST_DATE,
                                       .TRAIN_PLACE_NAME = p.tp.NAME_VN,
                                       .IS_MAJOR = If(p.p.IS_MAJOR = -1, True, False),
                                       .IS_MAIN = If(p.p.IS_MAIN = -1, True, False),
                                       .CERTIFICATE_GROUP_ID = p.p.CERTIFICATE_GROUP_ID,
                                       .CERTIFICATE_GROUP_NAME = p.ot2.NAME_VN,
                                       .CERTIFICATE_TYPE_ID = p.p.CERTIFICATE_TYPE_ID,
                                       .CERTIFICATE_TYPE_NAME = p.ot3.NAME_VN,
                                       .MAJOR = p.p.MAJOR,
                                       .MAJOR_NAME = p.o1.NAME_VN,
                                       .GRADUATE_SCHOOL = p.p.GRADUATE_SCHOOL,
                                       .GRADUATE_SCHOOL_NAME = p.o2.NAME_VN})

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_CODE) Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.ORG_NAME) Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.FROM_DATE.HasValue Then
                lst = lst.Where(Function(p) p.FROM_DATE.Value.ToString("MM/yyyy") = _filter.FROM_DATE.Value.ToString("MM/yyyy"))
            End If

            If _filter.TO_DATE.HasValue Then
                lst = lst.Where(Function(p) p.TO_DATE.Value.ToString("MM/yyyy") = _filter.TO_DATE.Value.ToString("MM/yyyy"))
            End If

            If Not String.IsNullOrEmpty(_filter.NAME_SHOOLS) Then
                lst = lst.Where(Function(p) p.NAME_SHOOLS.ToUpper.Contains(_filter.NAME_SHOOLS.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.SPECIALIZED_TRAIN) Then
                lst = lst.Where(Function(p) p.SPECIALIZED_TRAIN.ToUpper.Contains(_filter.SPECIALIZED_TRAIN.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.TYPE_TRAIN_NAME) Then
                lst = lst.Where(Function(p) p.TYPE_TRAIN_NAME.ToUpper.Contains(_filter.TYPE_TRAIN_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.FORM_TRAIN_NAME) Then
                lst = lst.Where(Function(p) p.FORM_TRAIN_NAME.ToUpper.Contains(_filter.FORM_TRAIN_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.CERTIFICATE_CODE) Then
                lst = lst.Where(Function(p) p.CERTIFICATE_CODE.ToUpper.Contains(_filter.CERTIFICATE_CODE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.CERTIFICATE) Then
                lst = lst.Where(Function(p) p.CERTIFICATE.ToUpper.Contains(_filter.CERTIFICATE.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.CERTIFICATE_GROUP_NAME) Then
                lst = lst.Where(Function(p) p.CERTIFICATE_GROUP_NAME.ToUpper.Contains(_filter.CERTIFICATE_GROUP_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.CERTIFICATE_TYPE_NAME) Then
                lst = lst.Where(Function(p) p.CERTIFICATE_TYPE_NAME.ToUpper.Contains(_filter.CERTIFICATE_TYPE_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.LEVEL_NAME) Then
                lst = lst.Where(Function(p) p.LEVEL_NAME.ToUpper.Contains(_filter.LEVEL_NAME.ToUpper))
            End If

            If _filter.EFFECTIVE_DATE_FROM.HasValue Then
                lst = lst.Where(Function(p) p.EFFECTIVE_DATE_FROM = _filter.EFFECTIVE_DATE_FROM)
            End If

            If _filter.EFFECTIVE_DATE_TO.HasValue Then
                lst = lst.Where(Function(p) p.EFFECTIVE_DATE_TO = _filter.EFFECTIVE_DATE_TO)
            End If

            If Not String.IsNullOrEmpty(_filter.RENEWED_NAME) Then
                lst = lst.Where(Function(p) p.RENEWED_NAME.ToUpper.Contains(_filter.RENEWED_NAME.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.RESULT_TRAIN) Then
                lst = lst.Where(Function(p) p.RESULT_TRAIN.ToUpper.Contains(_filter.RESULT_TRAIN.ToUpper))
            End If

            If _filter.YEAR_GRA.HasValue Then
                lst = lst.Where(Function(p) p.YEAR_GRA = _filter.YEAR_GRA)
            End If

            If Not String.IsNullOrEmpty(_filter.POINT_LEVEL) Then
                lst = lst.Where(Function(p) p.POINT_LEVEL.ToUpper.Contains(_filter.POINT_LEVEL.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.CONTENT_LEVEL) Then
                lst = lst.Where(Function(p) p.CONTENT_LEVEL.ToUpper.Contains(_filter.CONTENT_LEVEL.ToUpper))
            End If

            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(p) p.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
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

    Public Function InsertProcessTraining(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_PRO_TRAIN_OUT_COMPANY
        Dim iCount As Integer = 0
        Try
            If objTitle.IS_MAJOR Then
                Dim obj = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.EMPLOYEE_ID = objTitle.EMPLOYEE_ID Select p).ToList
                For Each i In obj
                    i.IS_MAJOR = 0
                Next
                Context.SaveChanges(log)
            End If

            If objTitle.IS_MAIN Then
                UpdateMainCertificate(objTitle, log)
            End If
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_PRO_TRAIN_OUT_COMPANY.EntitySet.Name)

            objTitleData.MAJOR = objTitle.MAJOR
            objTitleData.IS_MAJOR = objTitle.IS_MAJOR
            objTitleData.GRADUATE_SCHOOL = objTitle.GRADUATE_SCHOOL

            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.YEAR_GRA = objTitle.YEAR_GRA
            objTitleData.NAME_SHOOLS = objTitle.NAME_SHOOLS
            objTitleData.UPLOAD_FILE = objTitle.UPLOAD_FILE
            objTitleData.FILE_NAME = objTitle.FILE_NAME
            objTitleData.FORM_TRAIN_ID = objTitle.FORM_TRAIN_ID
            objTitleData.SPECIALIZED_TRAIN = objTitle.SPECIALIZED_TRAIN
            objTitleData.RESULT_TRAIN = objTitle.RESULT_TRAIN
            objTitleData.CERTIFICATE = objTitle.CERTIFICATE_ID
            objTitleData.CERTIFICATE_GROUP_ID = objTitle.CERTIFICATE_GROUP_ID
            objTitleData.CERTIFICATE_TYPE_ID = objTitle.CERTIFICATE_TYPE_ID
            objTitleData.EFFECTIVE_DATE_FROM = objTitle.EFFECTIVE_DATE_FROM
            objTitleData.EFFECTIVE_DATE_TO = objTitle.EFFECTIVE_DATE_TO
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.RECEIVE_DEGREE_DATE = objTitle.RECEIVE_DEGREE_DATE
            objTitleData.IS_RENEWED = objTitle.IS_RENEWED
            objTitleData.LEVEL_ID = objTitle.LEVEL_ID
            objTitleData.POINT_LEVEL = objTitle.POINT_LEVEL
            objTitleData.CONTENT_LEVEL = objTitle.CONTENT_LEVEL
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.CERTIFICATE_CODE = objTitle.CERTIFICATE_CODE
            objTitleData.TYPE_TRAIN_NAME = objTitle.TYPE_TRAIN_NAME
            objTitleData.CERTIFICATE_NAME = objTitle.CERTIFICATE_NAME
            objTitleData.TRAIN_PLACE = objTitle.TRAIN_PLACE
            objTitleData.IS_MAIN = objTitle.IS_MAIN

            Context.HU_PRO_TRAIN_OUT_COMPANY.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyProcessTraining(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_PRO_TRAIN_OUT_COMPANY With {.ID = objTitle.ID}
        Try
            If objTitle.IS_MAJOR Then
                Dim obj = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.EMPLOYEE_ID = objTitle.EMPLOYEE_ID Select p).ToList
                For Each i In obj
                    i.IS_MAJOR = 0
                Next
                Context.SaveChanges(log)
            End If
            If objTitle.IS_MAIN Then
                UpdateMainCertificate(objTitle, log)
            End If
            objTitleData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.ID = objTitle.ID).SingleOrDefault

            objTitleData.MAJOR = objTitle.MAJOR
            objTitleData.IS_MAJOR = objTitle.IS_MAJOR
            objTitleData.GRADUATE_SCHOOL = objTitle.GRADUATE_SCHOOL

            objTitleData.ID = objTitle.ID
            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.UPLOAD_FILE = objTitle.UPLOAD_FILE
            objTitleData.FILE_NAME = objTitle.FILE_NAME
            objTitleData.YEAR_GRA = objTitle.YEAR_GRA
            objTitleData.NAME_SHOOLS = objTitle.NAME_SHOOLS
            objTitleData.FORM_TRAIN_ID = objTitle.FORM_TRAIN_ID
            objTitleData.SPECIALIZED_TRAIN = objTitle.SPECIALIZED_TRAIN
            objTitleData.RESULT_TRAIN = objTitle.RESULT_TRAIN
            objTitleData.CERTIFICATE = objTitle.CERTIFICATE_ID
            objTitleData.CERTIFICATE_GROUP_ID = objTitle.CERTIFICATE_GROUP_ID
            objTitleData.CERTIFICATE_TYPE_ID = objTitle.CERTIFICATE_TYPE_ID
            objTitleData.EFFECTIVE_DATE_FROM = objTitle.EFFECTIVE_DATE_FROM
            objTitleData.EFFECTIVE_DATE_TO = objTitle.EFFECTIVE_DATE_TO
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.TYPE_TRAIN_ID = objTitle.TYPE_TRAIN_ID
            objTitleData.RECEIVE_DEGREE_DATE = objTitle.RECEIVE_DEGREE_DATE
            objTitleData.IS_RENEWED = objTitle.IS_RENEWED
            objTitleData.LEVEL_ID = objTitle.LEVEL_ID
            objTitleData.POINT_LEVEL = objTitle.POINT_LEVEL
            objTitleData.CONTENT_LEVEL = objTitle.CONTENT_LEVEL
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.CERTIFICATE_CODE = objTitle.CERTIFICATE_CODE
            objTitleData.TYPE_TRAIN_NAME = objTitle.TYPE_TRAIN_NAME
            objTitleData.CERTIFICATE_NAME = objTitle.CERTIFICATE_NAME
            objTitleData.TRAIN_PLACE = objTitle.TRAIN_PLACE
            objTitleData.IS_MAIN = objTitle.IS_MAIN
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function UPDATE_MAIN_CERTIFICATE(ByVal ID_EMP As Decimal, ByVal LEVEL_ID As Decimal, ByVal MAJOR As Decimal, ByVal GRADUATE_SCHOOL_ID As Decimal, ByVal GRADUATION_YEAR As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.UPDATE_MAIN_CERTIFICATE",
                                                    New With {.P_ID = ID_EMP,
                                                              .P_LEARNING_LEVEL = LEVEL_ID,
                                                              .P_MAJOR = LEVEL_ID,
                                                              .P_GRADUATE_SCHOOL_ID = LEVEL_ID,
                                                              .P_GRADUATION_YEAR = LEVEL_ID,
                                                              .CUR = cls.OUT_CURSOR})


                If dtData IsNot Nothing AndAlso dtData.Rows(0)("RES").ToString.Contains("1") Then
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function UpdateMainCertificate(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTO, ByVal log As UserLog) As Boolean
        Try
            Dim lstEmpEdu As List(Of HU_EMPLOYEE_EDUCATION)
            lstEmpEdu = (From p In Context.HU_EMPLOYEE_EDUCATION Where p.EMPLOYEE_ID = objTitle.EMPLOYEE_ID).ToList
            If lstEmpEdu IsNot Nothing AndAlso lstEmpEdu.Count > 0 Then
                For index = 0 To lstEmpEdu.Count - 1
                    lstEmpEdu(index).LEARNING_LEVEL = objTitle.LEVEL_ID
                    lstEmpEdu(index).MAJOR = objTitle.MAJOR
                    lstEmpEdu(index).GRADUATE_SCHOOL_ID = objTitle.GRADUATE_SCHOOL
                    lstEmpEdu(index).GRADUATION_YEAR = objTitle.YEAR_GRA
                    lstEmpEdu(index).LANGUAGE_MARK = objTitle.POINT_LEVEL
                Next
                Context.SaveChanges(log)
            Else
                Dim newEmpEdu As New HU_EMPLOYEE_EDUCATION
                newEmpEdu.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
                newEmpEdu.LEARNING_LEVEL = objTitle.LEVEL_ID
                newEmpEdu.MAJOR = objTitle.MAJOR
                newEmpEdu.GRADUATE_SCHOOL_ID = objTitle.GRADUATE_SCHOOL
                newEmpEdu.GRADUATION_YEAR = objTitle.YEAR_GRA
                newEmpEdu.LANGUAGE_MARK = objTitle.POINT_LEVEL
                Context.HU_EMPLOYEE_EDUCATION.AddObject(newEmpEdu)
                Context.SaveChanges(log)
            End If

            Dim lstEmpTrain As List(Of HU_PRO_TRAIN_OUT_COMPANY)
            lstEmpTrain = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.EMPLOYEE_ID = objTitle.EMPLOYEE_ID).ToList
            If lstEmpTrain IsNot Nothing AndAlso lstEmpTrain.Count > 0 Then
                For index = 0 To lstEmpTrain.Count - 1
                    lstEmpTrain(index).IS_MAIN = 0
                Next
                Context.SaveChanges(log)
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteProcessTraining(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstInsChangeTypeData As List(Of HU_PRO_TRAIN_OUT_COMPANY)
        Try
            lstInsChangeTypeData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstInsChangeTypeData.Count - 1
                Context.HU_PRO_TRAIN_OUT_COMPANY.DeleteObject(lstInsChangeTypeData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
    Public Function GetCertificateType() As List(Of OtherListDTO)
        Try
            Dim query As List(Of OtherListDTO)
            query = (From p In Context.OT_OTHER_LIST
                     From r In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                     Where (r.CODE = "CERTIFICATE_TYPE" And p.ACTFLG = "A")
                     Order By p.ID
                     Select New OtherListDTO With {
                     .ID = p.ID,
                     .NAME_VN = p.NAME_VN}).ToList
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CheckExistEmployeeCertificate_IsMain(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTO) As Boolean
        Try
            Dim count As Integer
            If objTitle.ID <> 0 Then
                count = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.EMPLOYEE_ID = objTitle.EMPLOYEE_ID AndAlso p.IS_MAIN = -1 AndAlso p.ID <> objTitle.ID).Count
            Else
                count = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.EMPLOYEE_ID = objTitle.EMPLOYEE_ID AndAlso p.IS_MAIN = -1).Count
            End If
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CheckExistEmployeeCertificate_IsMajor(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTO) As Boolean
        Try
            Dim count As Integer
            If objTitle.ID <> 0 Then
                count = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.EMPLOYEE_ID = objTitle.EMPLOYEE_ID AndAlso p.IS_MAJOR = -1 AndAlso p.ID <> objTitle.ID).Count
            Else
                count = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.EMPLOYEE_ID = objTitle.EMPLOYEE_ID AndAlso p.IS_MAJOR = -1).Count
            End If
            If count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

#Region "EmployeeEdit"

    Public Function GetChangedCVList(ByVal lstEmpEdit As List(Of EmployeeEditDTO)) As Dictionary(Of String, String)
        Try
            Dim dic As New Dictionary(Of String, String)
            For Each empEdit As EmployeeEditDTO In lstEmpEdit
                Dim colNames As String = String.Empty
                Dim empCV = Context.HU_EMPLOYEE_CV.Where(Function(f) f.EMPLOYEE_ID = empEdit.EMPLOYEE_ID).FirstOrDefault
                Dim empEdu = Context.HU_EMPLOYEE_EDUCATION.Where(Function(f) f.EMPLOYEE_ID = empEdit.EMPLOYEE_ID).FirstOrDefault
                If empEdit.ID_NO <> empCV.ID_NO Then
                    colNames = "ID_NO"
                End If
                If (If(empEdit.ID_DATE Is Nothing, "", empEdit.ID_DATE.ToString()) <> If(empCV.ID_DATE Is Nothing, "", empCV.ID_DATE.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "ID_DATE", "ID_DATE")
                End If
                If (If(empEdit.EXPIRE_DATE_IDNO Is Nothing, "", empEdit.EXPIRE_DATE_IDNO.ToString()) <> If(empCV.EXPIRE_DATE_IDNO Is Nothing, "", empCV.EXPIRE_DATE_IDNO.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "EXPIRE_DATE_IDNO", "EXPIRE_DATE_IDNO")
                End If
                If (If(empEdit.ID_PLACE Is Nothing, "", empEdit.ID_PLACE.ToString()) <> If(empCV.ID_PLACE Is Nothing, "", empCV.ID_PLACE.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "ID_PLACE_NAME", "ID_PLACE_NAME")
                End If
                If (If(empEdit.MARITAL_STATUS Is Nothing, "", empEdit.MARITAL_STATUS.ToString()) <> If(empCV.MARITAL_STATUS Is Nothing, "", empCV.MARITAL_STATUS.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "MARITAL_STATUS_NAME", "MARITAL_STATUS_NAME")
                End If
                If (If(empEdit.RELIGION Is Nothing, "", empEdit.RELIGION.ToString()) <> If(empCV.RELIGION Is Nothing, "", empCV.RELIGION.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "RELIGION_NAME", "RELIGION_NAME")
                End If
                If (If(empEdit.NATIVE Is Nothing, "", empEdit.NATIVE.ToString()) <> If(empCV.NATIVE Is Nothing, "", empCV.NATIVE.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "NATIVE_NAME", "NATIVE_NAME")
                End If
                If (If(empEdit.PER_ADDRESS Is Nothing, "", empEdit.PER_ADDRESS) <> If(empCV.PER_ADDRESS Is Nothing, "", empCV.PER_ADDRESS)) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "PER_ADDRESS", "PER_ADDRESS")
                End If
                If (If(empEdit.PER_PROVINCE Is Nothing, "", empEdit.PER_PROVINCE.ToString()) <> If(empCV.PER_PROVINCE Is Nothing, "", empCV.PER_PROVINCE.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "PER_PROVINCE_NAME", "PER_PROVINCE_NAME")
                End If
                If (If(empEdit.PER_DISTRICT Is Nothing, "", empEdit.PER_DISTRICT.ToString()) <> If(empCV.PER_DISTRICT Is Nothing, Nothing, empCV.PER_DISTRICT.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "PER_DISTRICT_NAME", "PER_DISTRICT_NAME")
                End If
                If (If(empEdit.PER_WARD Is Nothing, "", empEdit.PER_WARD.ToString()) <> If(empCV.PER_WARD Is Nothing, "", empCV.PER_WARD.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "PER_WARD_NAME", "PER_WARD_NAME")
                End If
                If (If(empEdit.NAV_ADDRESS Is Nothing, "", empEdit.NAV_ADDRESS) <> If(empCV.NAV_ADDRESS Is Nothing, "", empCV.NAV_ADDRESS)) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "NAV_ADDRESS", "NAV_ADDRESS")
                End If
                If (If(empEdit.NAV_PROVINCE Is Nothing, "", empEdit.NAV_PROVINCE.ToString()) <> If(empCV.NAV_PROVINCE Is Nothing, "", empCV.NAV_PROVINCE.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "NAV_PROVINCE_NAME", "NAV_PROVINCE_NAME")
                End If
                If (If(empEdit.NAV_DISTRICT Is Nothing, "", empEdit.NAV_DISTRICT.ToString()) <> If(empCV.NAV_DISTRICT Is Nothing, "", empCV.NAV_DISTRICT.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "NAV_DISTRICT_NAME", "NAV_DISTRICT_NAME")
                End If
                If (If(empEdit.NAV_WARD Is Nothing, "", empEdit.NAV_WARD.ToString()) <> If(empCV.NAV_WARD Is Nothing, "", empCV.NAV_WARD.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "NAV_WARD_NAME", "NAV_WARD_NAME")
                End If
                If (If(empEdit.EXPIRE_DATE_IDNO Is Nothing, "", empEdit.EXPIRE_DATE_IDNO.ToString()) <> If(empCV.EXPIRE_DATE_IDNO Is Nothing, "", empCV.EXPIRE_DATE_IDNO.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "EXPIRE_DATE_IDNO", "EXPIRE_DATE_IDNO")
                End If
                If (If(empEdit.CONTACT_PER Is Nothing, "", empEdit.CONTACT_PER) <> If(empCV.CONTACT_PER Is Nothing, "", empCV.CONTACT_PER)) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "CONTACT_PER", "CONTACT_PER")
                End If
                If (If(empEdit.RELATION_PER_CTR Is Nothing, "", empEdit.RELATION_PER_CTR.ToString()) <> If(empCV.RELATION_PER_CTR Is Nothing, "", empCV.RELATION_PER_CTR.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "RELATION_PER_CTR_NAME", "RELATION_PER_CTR_NAME")
                End If
                If (If(empEdit.CONTACT_PER_MBPHONE Is Nothing, "", empEdit.CONTACT_PER_MBPHONE) <> If(empCV.CONTACT_PER_MBPHONE Is Nothing, "", empCV.CONTACT_PER_MBPHONE)) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "CONTACT_PER_MBPHONE", "CONTACT_PER_MBPHONE")
                End If
                If (If(empEdit.ADDRESS_PER_CTR Is Nothing, "", empEdit.ADDRESS_PER_CTR) <> If(empCV.ADDRESS_PER_CTR Is Nothing, "", empCV.ADDRESS_PER_CTR)) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "ADDRESS_PER_CTR", "ADDRESS_PER_CTR")
                End If
                If (If(empEdit.VILLAGE Is Nothing, "", empEdit.VILLAGE) <> If(empCV.VILLAGE Is Nothing, "", empCV.VILLAGE)) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "VILLAGE", "VILLAGE")
                End If
                If (If(empEdit.HOME_PHONE Is Nothing, "", empEdit.HOME_PHONE) <> If(empCV.HOME_PHONE Is Nothing, "", empCV.HOME_PHONE)) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "HOME_PHONE", "HOME_PHONE")
                End If
                If (If(empEdit.MOBILE_PHONE Is Nothing, "", empEdit.MOBILE_PHONE) <> If(empCV.MOBILE_PHONE Is Nothing, "", empCV.MOBILE_PHONE)) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "MOBILE_PHONE", "MOBILE_PHONE")
                End If
                If (If(empEdit.WORK_EMAIL Is Nothing, "", empEdit.WORK_EMAIL) <> If(empCV.WORK_EMAIL Is Nothing, "", empCV.WORK_EMAIL)) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "WORK_EMAIL", "WORK_EMAIL")
                End If
                If (If(empEdit.PER_EMAIL Is Nothing, "", empEdit.PER_EMAIL) <> If(empCV.PER_EMAIL Is Nothing, "", empCV.PER_EMAIL)) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "PER_EMAIL", "PER_EMAIL")
                End If
                If (If(empEdit.PERSON_INHERITANCE Is Nothing, "", empEdit.PERSON_INHERITANCE) <> If(empCV.PERSON_INHERITANCE Is Nothing, "", empCV.PERSON_INHERITANCE)) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "PERSON_INHERITANCE", "PERSON_INHERITANCE")
                End If
                If (If(empEdit.BANK_NO Is Nothing, "", empEdit.BANK_NO) <> If(empCV.BANK_NO Is Nothing, "", empCV.BANK_NO)) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "BANK_NO", "BANK_NO")
                End If
                If (If(empEdit.BANK_ID Is Nothing, "", empEdit.BANK_ID.ToString()) <> If(empCV.BANK_ID Is Nothing, "", empCV.BANK_ID.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "BANK_NAME", "BANK_NAME")
                End If
                If (If(empEdit.BANK_BRANCH_ID Is Nothing, "", empEdit.BANK_BRANCH_ID.ToString()) <> If(empCV.BANK_BRANCH_ID Is Nothing, "", empCV.BANK_BRANCH_ID.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "BANK_BRANCH_NAME", "BANK_BRANCH_NAME")
                End If
                If (If(empEdit.NOTE_CHANGE_CMND Is Nothing, "", empEdit.NOTE_CHANGE_CMND.ToString()) <> If(empCV.ID_REMARK Is Nothing, "", empCV.ID_REMARK.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "NOTE_CHANGE_CMND", "NOTE_CHANGE_CMND")
                End If
                'If (If(empEdit.NO_HOUSEHOLDS Is Nothing, "", empEdit.NO_HOUSEHOLDS.ToString()) <> If(empCV.NO_HOUSEHOLDS Is Nothing, "", empCV.NO_HOUSEHOLDS.ToString())) Then
                '    colNames = If(colNames <> String.Empty, colNames + "," + "NO_HOUSEHOLDS", "NO_HOUSEHOLDS")
                'End If
                If (If(empEdit.PASS_PLACE_ID Is Nothing, "", empEdit.PASS_PLACE_ID.ToString()) <> If(empCV.PASS_PLACE_ID Is Nothing, "", empCV.PASS_PLACE_ID.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "PASS_PLACE", "PASS_PLACE")
                End If
                If (If(empEdit.PASS_NO Is Nothing, "", empEdit.PASS_NO.ToString()) <> If(empCV.PASS_NO Is Nothing, "", empCV.PASS_NO.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "PASS_NO", "PASS_NO")
                End If
                If (If(empEdit.PASS_DATE Is Nothing, "", empEdit.PASS_DATE.ToString()) <> If(empCV.PASS_DATE Is Nothing, "", empCV.PASS_DATE.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "PASS_DATE", "PASS_DATE")
                End If
                If (If(empEdit.PASS_EXPIRE Is Nothing, "", empEdit.PASS_EXPIRE.ToString()) <> If(empCV.PASS_EXPIRE Is Nothing, "", empCV.PASS_EXPIRE.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "PASS_EXPIRE", "PASS_EXPIRE")
                End If
                If (If(empEdit.VISA_PLACE_ID Is Nothing, "", empEdit.VISA_PLACE_ID.ToString()) <> If(empCV.VISA_PLACE_ID Is Nothing, "", empCV.VISA_PLACE_ID.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "VISA_PLACE", "VISA_PLACE")
                End If
                If (If(empEdit.VISA Is Nothing, "", empEdit.VISA.ToString()) <> If(empCV.VISA Is Nothing, "", empCV.VISA.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "VISA", "VISA")
                End If
                If (If(empEdit.VISA_DATE Is Nothing, "", empEdit.VISA_DATE.ToString()) <> If(empCV.VISA_DATE Is Nothing, "", empCV.VISA_DATE.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "VISA_DATE", "VISA_DATE")
                End If
                If (If(empEdit.VISA_EXPIRE Is Nothing, "", empEdit.VISA_EXPIRE.ToString()) <> If(empCV.VISA_EXPIRE Is Nothing, "", empCV.VISA_EXPIRE.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "VISA_EXPIRE", "VISA_EXPIRE")
                End If
                If (If(empEdit.SSLD_PLACE_ID Is Nothing, "", empEdit.SSLD_PLACE_ID.ToString()) <> If(empCV.SSLD_PLACE_ID Is Nothing, "", empCV.SSLD_PLACE_ID.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "SSLD_PLACE", "SSLD_PLACE")
                End If
                If (If(empEdit.BOOK_NO Is Nothing, "", empEdit.BOOK_NO.ToString()) <> If(empCV.BOOK_NO Is Nothing, "", empCV.BOOK_NO.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "BOOK_NO", "BOOK_NO")
                End If
                If (If(empEdit.BOOK_DATE Is Nothing, "", empEdit.BOOK_DATE.ToString()) <> If(empCV.BOOK_DATE Is Nothing, "", empCV.BOOK_DATE.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "BOOK_DATE", "BOOK_DATE")
                End If
                If (If(empEdit.BOOK_EXPIRE Is Nothing, "", empEdit.BOOK_EXPIRE.ToString()) <> If(empCV.BOOK_EXPIRE Is Nothing, "", empCV.BOOK_EXPIRE.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "BOOK_EXPIRE", "BOOK_EXPIRE")
                End If
                If empEdu IsNot Nothing AndAlso (If(empEdit.GRADUATION_YEAR Is Nothing, "", empEdit.GRADUATION_YEAR.ToString()) <> If(empEdu.GRADUATION_YEAR Is Nothing, "", empEdu.GRADUATION_YEAR.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "GRADUATION_YEAR", "GRADUATION_YEAR")
                End If
                If empEdu IsNot Nothing AndAlso (If(empEdit.GRADUATE_SCHOOL_ID Is Nothing, "", empEdit.GRADUATE_SCHOOL_ID.ToString()) <> If(empEdu.GRADUATE_SCHOOL_ID Is Nothing, "", empEdu.GRADUATE_SCHOOL_ID.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "GRADUATE_SCHOOL_NAME", "GRADUATE_SCHOOL_NAME")
                End If
                If empEdu IsNot Nothing AndAlso (If(empEdit.ACADEMY Is Nothing, "", empEdit.ACADEMY.ToString()) <> If(empEdu.ACADEMY Is Nothing, "", empEdu.ACADEMY.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "ACADEMY_NAME", "ACADEMY_NAME")
                End If
                If empEdu IsNot Nothing AndAlso (If(empEdit.LEARNING_LEVEL Is Nothing, "", empEdit.LEARNING_LEVEL.ToString()) <> If(empEdu.LEARNING_LEVEL Is Nothing, "", empEdu.LEARNING_LEVEL.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "LEARNING_LEVEL_NAME", "LEARNING_LEVEL_NAME")
                End If
                If empEdu IsNot Nothing AndAlso (If(empEdit.MAJOR Is Nothing, "", empEdit.MAJOR.ToString()) <> If(empEdu.MAJOR Is Nothing, "", empEdu.MAJOR.ToString())) Then
                    colNames = If(colNames <> String.Empty, colNames + "," + "MAJOR_NAME", "MAJOR_NAME")
                End If
                dic.Add(empEdit.ID.ToString, colNames)
            Next
            Return dic
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertEmployeeEdit(ByVal objEmployeeEdit As EmployeeEditDTO,
                                             ByVal log As UserLog,
                                             ByRef gID As Decimal) As Boolean
        Try

            Dim objEmployeeEditData As New HU_EMPLOYEE_EDIT
            objEmployeeEditData.ID = Utilities.GetNextSequence(Context, Context.HU_EMPLOYEE_EDIT.EntitySet.Name)
            objEmployeeEditData.EMPLOYEE_ID = objEmployeeEdit.EMPLOYEE_ID
            objEmployeeEditData.ID_DATE = objEmployeeEdit.ID_DATE
            objEmployeeEditData.ID_NO = objEmployeeEdit.ID_NO
            objEmployeeEditData.ID_PLACE = objEmployeeEdit.ID_PLACE
            objEmployeeEditData.MARITAL_STATUS = objEmployeeEdit.MARITAL_STATUS
            objEmployeeEditData.NAV_ADDRESS = objEmployeeEdit.NAV_ADDRESS
            objEmployeeEditData.NAV_DISTRICT = objEmployeeEdit.NAV_DISTRICT
            objEmployeeEditData.NAV_PROVINCE = objEmployeeEdit.NAV_PROVINCE
            objEmployeeEditData.NAV_WARD = objEmployeeEdit.NAV_WARD
            objEmployeeEditData.PER_ADDRESS = objEmployeeEdit.PER_ADDRESS
            objEmployeeEditData.PER_DISTRICT = objEmployeeEdit.PER_DISTRICT
            objEmployeeEditData.PER_PROVINCE = objEmployeeEdit.PER_PROVINCE
            objEmployeeEditData.PER_WARD = objEmployeeEdit.PER_WARD

            objEmployeeEditData.EXPIRE_DATE_IDNO = objEmployeeEdit.EXPIRE_DATE_IDNO
            objEmployeeEditData.CONTACT_PER = objEmployeeEdit.CONTACT_PER
            objEmployeeEditData.CONTACT_PER_MBPHONE = objEmployeeEdit.CONTACT_PER_MBPHONE
            objEmployeeEditData.RELATION_PER_CTR = objEmployeeEdit.RELATION_PER_CTR
            objEmployeeEditData.VILLAGE = objEmployeeEdit.VILLAGE
            objEmployeeEditData.HOME_PHONE = objEmployeeEdit.HOME_PHONE
            objEmployeeEditData.MOBILE_PHONE = objEmployeeEdit.MOBILE_PHONE
            objEmployeeEditData.WORK_EMAIL = objEmployeeEdit.WORK_EMAIL
            objEmployeeEditData.PER_EMAIL = objEmployeeEdit.PER_EMAIL
            objEmployeeEditData.PERSON_INHERITANCE = objEmployeeEdit.PERSON_INHERITANCE
            objEmployeeEditData.BANK_NO = objEmployeeEdit.BANK_NO
            objEmployeeEditData.BANK_ID = objEmployeeEdit.BANK_ID
            objEmployeeEditData.BANK_BRANCH_ID = objEmployeeEdit.BANK_BRANCH_ID

            objEmployeeEditData.ACADEMY = objEmployeeEdit.ACADEMY
            objEmployeeEditData.LEARNING_LEVEL = objEmployeeEdit.LEARNING_LEVEL
            objEmployeeEditData.MAJOR = objEmployeeEdit.MAJOR
            objEmployeeEditData.GRADUATE_SCHOOL_ID = objEmployeeEdit.GRADUATE_SCHOOL_ID
            objEmployeeEditData.GRADUATION_YEAR = objEmployeeEdit.GRADUATION_YEAR
            objEmployeeEditData.COMPUTER_RANK = objEmployeeEdit.COMPUTER_RANK
            objEmployeeEditData.COMPUTER_MARK = objEmployeeEdit.COMPUTER_MARK
            objEmployeeEditData.COMPUTER_CERTIFICATE = objEmployeeEdit.COMPUTER_CERTIFICATE
            objEmployeeEditData.LANGUAGE = objEmployeeEdit.LANGUAGE
            objEmployeeEditData.LANGUAGE_LEVEL = objEmployeeEdit.LANGUAGE_LEVEL
            objEmployeeEditData.LANGUAGE_MARK = objEmployeeEdit.LANGUAGE_MARK
            objEmployeeEditData.NO_HOUSEHOLDS = objEmployeeEdit.NO_HOUSEHOLDS
            objEmployeeEditData.NATIVE = objEmployeeEdit.NATIVE
            objEmployeeEditData.RELIGION = objEmployeeEdit.RELIGION
            objEmployeeEditData.NOTE_CHANGE_CMND = objEmployeeEdit.NOTE_CHANGE_CMND
            objEmployeeEditData.FILE_CMND = objEmployeeEdit.FILE_CMND
            objEmployeeEditData.FILE_CMND_BACK = objEmployeeEdit.FILE_CMND_BACK
            objEmployeeEditData.FILE_ADDRESS = objEmployeeEdit.FILE_ADDRESS
            objEmployeeEditData.FILE_BANK = objEmployeeEdit.FILE_BANK
            objEmployeeEditData.ADDRESS_PER_CTR = objEmployeeEdit.ADDRESS_PER_CTR
            objEmployeeEditData.PASS_NO = objEmployeeEdit.PASS_NO
            objEmployeeEditData.PASS_DATE = objEmployeeEdit.PASS_DATE
            objEmployeeEditData.PASS_EXPIRE = objEmployeeEdit.PASS_EXPIRE
            objEmployeeEditData.PASS_PLACE_ID = objEmployeeEdit.PASS_PLACE_ID
            objEmployeeEditData.VISA_NO = objEmployeeEdit.VISA
            objEmployeeEditData.VISA_DATE = objEmployeeEdit.VISA_DATE
            objEmployeeEditData.VISA_EXPIRE = objEmployeeEdit.VISA_EXPIRE
            objEmployeeEditData.VISA_PLACE_ID = objEmployeeEdit.VISA_PLACE_ID
            objEmployeeEditData.BOOK_NO = objEmployeeEdit.BOOK_NO
            objEmployeeEditData.BOOK_DATE = objEmployeeEdit.BOOK_DATE
            objEmployeeEditData.BOOK_EXPIRE = objEmployeeEdit.BOOK_EXPIRE
            objEmployeeEditData.SSLD_PLACE_ID = objEmployeeEdit.SSLD_PLACE_ID
            objEmployeeEditData.FILE_OTHER = objEmployeeEdit.FILE_OTHER
            If Not String.IsNullOrEmpty(objEmployeeEdit.IMAGE) Then
                objEmployeeEditData.IMAGE = objEmployeeEdit.IMAGE
            Else
                objEmployeeEditData.IMAGE = CopyEmployeeImageToEmployeeImageEdit(objEmployeeEdit.EMPLOYEE_ID)
            End If
            objEmployeeEditData.STATUS = 0
            Context.HU_EMPLOYEE_EDIT.AddObject(objEmployeeEditData)
            Context.SaveChanges(log)
            gID = objEmployeeEditData.ID

            If objEmployeeEdit.IS_SEND = 1 Then
                Dim lst = New List(Of Decimal)
                lst.Add(gID)
                SendEmployeeEdit(lst, log)
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function UpdateImg_EmployeeEdit_Mobile(ByVal _ID As Decimal, ByVal log As UserLog) As Boolean
        Try
            Dim empEditCV = (From p In Context.HU_EMPLOYEE_EDIT Where p.ID = _ID
                             Select New EmployeeCVDTO With {
                             .EMPLOYEE_ID = p.EMPLOYEE_ID,
                             .IMAGE = p.IMAGE
                        }).FirstOrDefault
            If empEditCV IsNot Nothing AndAlso empEditCV.IMAGE Is Nothing Then
                Dim objEmployeeEditData As New HU_EMPLOYEE_EDIT With {.ID = _ID}
                objEmployeeEditData = (From p In Context.HU_EMPLOYEE_EDIT Where p.ID = _ID).FirstOrDefault
                objEmployeeEditData.IMAGE = CopyEmployeeImageToEmployeeImageEdit(empEditCV.EMPLOYEE_ID)
                Context.SaveChanges(log)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function
    Public Function CopyEmployeeImageToEmployeeImageEdit(ByVal _empID As Decimal) As String
        'anhvn, neu user không thay đổi hình, vẫn lấy hình cũ từ hu_employee
        Try
            Dim strGuid = Guid.NewGuid().ToString()
            Dim empCV = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = _empID
                         Select New EmployeeCVDTO With {
                             .IMAGE = p.IMAGE
                        }).FirstOrDefault
            If empCV IsNot Nothing AndAlso empCV.IMAGE IsNot Nothing Then
                Dim filepath = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage\" & empCV.IMAGE
                If File.Exists(filepath) Then
                    Dim _fileInfo = New FileInfo(filepath)
                    Dim LinkDetail = AppDomain.CurrentDomain.BaseDirectory & "Profile\UploadFile\StaffProfile\" & strGuid
                    If Not Directory.Exists(LinkDetail.Trim) Then
                        Directory.CreateDirectory(LinkDetail.Trim)
                    End If
                    Dim fileCopy = LinkDetail & "\" & empCV.IMAGE
                    _fileInfo.CopyTo(fileCopy, True)
                    Dim strLink = "Profile\UploadFile\StaffProfile\" & strGuid & "\"
                    Dim fileupload As FileUploadDTO = New FileUploadDTO
                    fileupload.NAME = strGuid
                    fileupload.LINK = strLink.Trim
                    fileupload.FILE_NAME = empCV.IMAGE
                    AddFileUpload(fileupload)
                End If
            End If
            Return strGuid
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return ""
        End Try
    End Function

    Public Function ModifyEmployeeEdit(ByVal objEmployeeEdit As EmployeeEditDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objEmployeeEditData As New HU_EMPLOYEE_EDIT With {.ID = objEmployeeEdit.ID}
        Try
            objEmployeeEditData = (From p In Context.HU_EMPLOYEE_EDIT Where p.ID = objEmployeeEdit.ID).FirstOrDefault
            objEmployeeEditData.EMPLOYEE_ID = objEmployeeEdit.EMPLOYEE_ID
            objEmployeeEditData.ID_DATE = objEmployeeEdit.ID_DATE
            objEmployeeEditData.ID_NO = objEmployeeEdit.ID_NO
            objEmployeeEditData.ID_PLACE = objEmployeeEdit.ID_PLACE
            objEmployeeEditData.MARITAL_STATUS = objEmployeeEdit.MARITAL_STATUS
            objEmployeeEditData.NAV_ADDRESS = objEmployeeEdit.NAV_ADDRESS
            objEmployeeEditData.NAV_DISTRICT = objEmployeeEdit.NAV_DISTRICT
            objEmployeeEditData.NAV_PROVINCE = objEmployeeEdit.NAV_PROVINCE
            objEmployeeEditData.NAV_WARD = objEmployeeEdit.NAV_WARD
            objEmployeeEditData.PER_ADDRESS = objEmployeeEdit.PER_ADDRESS
            objEmployeeEditData.PER_DISTRICT = objEmployeeEdit.PER_DISTRICT
            objEmployeeEditData.PER_PROVINCE = objEmployeeEdit.PER_PROVINCE
            objEmployeeEditData.PER_WARD = objEmployeeEdit.PER_WARD

            objEmployeeEditData.EXPIRE_DATE_IDNO = objEmployeeEdit.EXPIRE_DATE_IDNO
            objEmployeeEditData.CONTACT_PER = objEmployeeEdit.CONTACT_PER
            objEmployeeEditData.CONTACT_PER_MBPHONE = objEmployeeEdit.CONTACT_PER_MBPHONE
            objEmployeeEditData.RELATION_PER_CTR = objEmployeeEdit.RELATION_PER_CTR
            objEmployeeEditData.VILLAGE = objEmployeeEdit.VILLAGE
            objEmployeeEditData.HOME_PHONE = objEmployeeEdit.HOME_PHONE
            objEmployeeEditData.MOBILE_PHONE = objEmployeeEdit.MOBILE_PHONE
            objEmployeeEditData.WORK_EMAIL = objEmployeeEdit.WORK_EMAIL
            objEmployeeEditData.PER_EMAIL = objEmployeeEdit.PER_EMAIL
            objEmployeeEditData.PERSON_INHERITANCE = objEmployeeEdit.PERSON_INHERITANCE
            objEmployeeEditData.BANK_NO = objEmployeeEdit.BANK_NO
            objEmployeeEditData.BANK_ID = objEmployeeEdit.BANK_ID
            objEmployeeEditData.BANK_BRANCH_ID = objEmployeeEdit.BANK_BRANCH_ID

            objEmployeeEditData.ACADEMY = objEmployeeEdit.ACADEMY
            objEmployeeEditData.LEARNING_LEVEL = objEmployeeEdit.LEARNING_LEVEL
            objEmployeeEditData.MAJOR = objEmployeeEdit.MAJOR
            objEmployeeEditData.GRADUATE_SCHOOL_ID = objEmployeeEdit.GRADUATE_SCHOOL_ID
            objEmployeeEditData.GRADUATION_YEAR = objEmployeeEdit.GRADUATION_YEAR
            objEmployeeEditData.COMPUTER_RANK = objEmployeeEdit.COMPUTER_RANK
            objEmployeeEditData.COMPUTER_MARK = objEmployeeEdit.COMPUTER_MARK
            objEmployeeEditData.COMPUTER_CERTIFICATE = objEmployeeEdit.COMPUTER_CERTIFICATE
            objEmployeeEditData.LANGUAGE = objEmployeeEdit.LANGUAGE
            objEmployeeEditData.LANGUAGE_LEVEL = objEmployeeEdit.LANGUAGE_LEVEL
            objEmployeeEditData.LANGUAGE_MARK = objEmployeeEdit.LANGUAGE_MARK
            objEmployeeEditData.NO_HOUSEHOLDS = objEmployeeEdit.NO_HOUSEHOLDS
            objEmployeeEditData.NATIVE = objEmployeeEdit.NATIVE
            objEmployeeEditData.RELIGION = objEmployeeEdit.RELIGION
            objEmployeeEditData.NOTE_CHANGE_CMND = objEmployeeEdit.NOTE_CHANGE_CMND
            objEmployeeEditData.FILE_CMND = objEmployeeEdit.FILE_CMND
            objEmployeeEditData.FILE_CMND_BACK = objEmployeeEdit.FILE_CMND_BACK
            objEmployeeEditData.FILE_ADDRESS = objEmployeeEdit.FILE_ADDRESS
            objEmployeeEditData.FILE_BANK = objEmployeeEdit.FILE_BANK
            objEmployeeEditData.ADDRESS_PER_CTR = objEmployeeEdit.ADDRESS_PER_CTR

            objEmployeeEditData.PASS_NO = objEmployeeEdit.PASS_NO
            objEmployeeEditData.PASS_DATE = objEmployeeEdit.PASS_DATE
            objEmployeeEditData.PASS_EXPIRE = objEmployeeEdit.PASS_EXPIRE
            objEmployeeEditData.PASS_PLACE_ID = objEmployeeEdit.PASS_PLACE_ID
            objEmployeeEditData.VISA_NO = objEmployeeEdit.VISA
            objEmployeeEditData.VISA_DATE = objEmployeeEdit.VISA_DATE
            objEmployeeEditData.VISA_EXPIRE = objEmployeeEdit.VISA_EXPIRE
            objEmployeeEditData.VISA_PLACE_ID = objEmployeeEdit.VISA_PLACE_ID
            objEmployeeEditData.BOOK_NO = objEmployeeEdit.BOOK_NO
            objEmployeeEditData.BOOK_DATE = objEmployeeEdit.BOOK_DATE
            objEmployeeEditData.BOOK_EXPIRE = objEmployeeEdit.BOOK_EXPIRE
            objEmployeeEditData.SSLD_PLACE_ID = objEmployeeEdit.SSLD_PLACE_ID
            objEmployeeEditData.FILE_OTHER = objEmployeeEdit.FILE_OTHER

            If Not String.IsNullOrEmpty(objEmployeeEdit.IMAGE) Then
                objEmployeeEditData.IMAGE = objEmployeeEdit.IMAGE
            End If
            objEmployeeEditData.STATUS = 0

            Context.SaveChanges(log)
            gID = objEmployeeEditData.ID

            If objEmployeeEdit.IS_SEND = 1 Then
                Dim lst = New List(Of Decimal)
                lst.Add(gID)
                SendEmployeeEdit(lst, log)
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetEmployeeEditByID(ByVal _filter As EmployeeEditDTO) As EmployeeEditDTO
        Dim objEmpEdit As EmployeeEditDTO
        Try
            If _filter.STATUS = "1" Then
                objEmpEdit = (From p In Context.HU_EMPLOYEE_EDIT
                              From nav_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.NAV_PROVINCE).DefaultIfEmpty
                              From nav_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.NAV_DISTRICT).DefaultIfEmpty
                              From nav_ward In Context.HU_WARD.Where(Function(f) f.ID = p.NAV_WARD).DefaultIfEmpty
                              From per_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PER_PROVINCE).DefaultIfEmpty
                              From per_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.PER_DISTRICT).DefaultIfEmpty
                              From per_ward In Context.HU_WARD.Where(Function(f) f.ID = p.PER_WARD).DefaultIfEmpty
                              From marital In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MARITAL_STATUS).DefaultIfEmpty
                              From relation_per In Context.OT_OTHER_LIST.Where(Function(f) p.RELATION_PER_CTR = f.ID And f.TYPE_ID = 48 And f.ACTFLG = "A").DefaultIfEmpty
                              From bank In Context.HU_BANK.Where(Function(f) p.BANK_ID = f.ID).DefaultIfEmpty
                              From bankbranch In Context.HU_BANK_BRANCH.Where(Function(f) p.BANK_BRANCH_ID = f.ID).DefaultIfEmpty
                              From academy In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.ACADEMY).DefaultIfEmpty
                              From learn_lv In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEARNING_LEVEL).DefaultIfEmpty
                              From major In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MAJOR).DefaultIfEmpty
                              From school In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GRADUATE_SCHOOL_ID).DefaultIfEmpty
                              From computer In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMPUTER_RANK).DefaultIfEmpty
                              From com_mark In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMPUTER_MARK).DefaultIfEmpty
                              From language In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LANGUAGE).DefaultIfEmpty
                              From language_lv In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LANGUAGE_LEVEL).DefaultIfEmpty
                              From rl In Context.OT_OTHER_LIST.Where(Function(f) p.RELIGION = f.ID).DefaultIfEmpty
                              From nt In Context.OT_OTHER_LIST.Where(Function(f) p.NATIVE = f.ID).DefaultIfEmpty
                              From fileCMND In Context.HU_USERFILES.Where(Function(f) p.FILE_CMND = f.NAME).DefaultIfEmpty
                              From fileCMND_back In Context.HU_USERFILES.Where(Function(f) p.FILE_CMND_BACK = f.NAME).DefaultIfEmpty
                              From pass_place In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PASS_PLACE_ID).DefaultIfEmpty
                              From visa_place In Context.HU_PROVINCE.Where(Function(f) f.ID = p.VISA_PLACE_ID).DefaultIfEmpty
                              From book_place In Context.HU_PROVINCE.Where(Function(f) f.ID = p.SSLD_PLACE_ID).DefaultIfEmpty
                              Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID And p.STATUS = "1"
                              Select New EmployeeEditDTO With {
                                 .ID = p.ID,
                                 .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                 .ID_DATE = p.ID_DATE,
                                 .ID_NO = p.ID_NO,
                                 .ID_PLACE = p.ID_PLACE,
                                 .RELIGION = p.RELIGION,
                                 .RELIGION_NAME = rl.NAME_VN,
                                 .NATIVE = p.NATIVE,
                                 .NATIVE_NAME = nt.NAME_VN,
                                 .NOTE_CHANGE_CMND = p.NOTE_CHANGE_CMND,
                                 .FILE_CMND = fileCMND.FILE_NAME,
                                 .FILE_CMND_BACK = fileCMND_back.FILE_NAME,
                                 .FILE_ADDRESS = p.FILE_ADDRESS,
                                 .FILE_BANK = p.FILE_BANK,
                                 .ADDRESS_PER_CTR = p.ADDRESS_PER_CTR,
                                 .MARITAL_STATUS = p.MARITAL_STATUS,
                                 .MARITAL_STATUS_NAME = marital.NAME_VN,
                                 .NAV_ADDRESS = p.NAV_ADDRESS,
                                 .NAV_DISTRICT = p.NAV_DISTRICT,
                                 .NAV_DISTRICT_NAME = nav_dis.NAME_VN,
                                 .NAV_PROVINCE = p.NAV_PROVINCE,
                                 .NAV_PROVINCE_NAME = nav_pro.NAME_VN,
                                 .NAV_WARD = p.NAV_WARD,
                                 .NAV_WARD_NAME = nav_ward.NAME_VN,
                                 .PER_ADDRESS = p.PER_ADDRESS,
                                 .PER_DISTRICT = p.PER_DISTRICT,
                                 .PER_DISTRICT_NAME = per_dis.NAME_VN,
                                 .PER_PROVINCE = p.PER_PROVINCE,
                                 .PER_PROVINCE_NAME = per_pro.NAME_VN,
                                 .PER_WARD = p.PER_WARD,
                                 .PER_WARD_NAME = per_ward.NAME_VN,
                                 .REASON_UNAPROVE = p.REASON_UNAPROVE,
                                 .STATUS = p.STATUS,
                                 .EXPIRE_DATE_IDNO = p.EXPIRE_DATE_IDNO,
                                 .CONTACT_PER = p.CONTACT_PER,
                                 .RELATION_PER_CTR = p.RELATION_PER_CTR,
                                 .RELATION_PER_CTR_NAME = relation_per.NAME_VN,
                                 .CONTACT_PER_MBPHONE = p.CONTACT_PER_MBPHONE,
                                 .VILLAGE = p.VILLAGE,
                                 .HOME_PHONE = p.HOME_PHONE,
                                 .MOBILE_PHONE = p.MOBILE_PHONE,
                                 .WORK_EMAIL = p.WORK_EMAIL,
                                 .PER_EMAIL = p.PER_EMAIL,
                                 .PERSON_INHERITANCE = p.PERSON_INHERITANCE,
                                 .CMND_NOTE_CHANGE = p.NOTE_CHANGE_CMND,
                                 .ACADEMY = p.ACADEMY,
                                 .ACADEMY_NAME = academy.NAME_VN,
                                 .LEARNING_LEVEL = p.LEARNING_LEVEL,
                                 .LEARNING_LEVEL_NAME = learn_lv.NAME_VN,
                                 .MAJOR = p.MAJOR,
                                 .MAJOR_NAME = major.NAME_VN,
                                 .GRADUATE_SCHOOL_ID = p.GRADUATE_SCHOOL_ID,
                                 .GRADUATE_SCHOOL_NAME = school.NAME_VN,
                                 .GRADUATION_YEAR = p.GRADUATION_YEAR,
                                 .COMPUTER_RANK = p.COMPUTER_RANK,
                                 .COMPUTER_RANK_NAME = computer.NAME_VN,
                                 .COMPUTER_MARK = p.COMPUTER_MARK,
                                 .COMPUTER_MARK_NAME = com_mark.NAME_VN,
                                 .COMPUTER_CERTIFICATE = p.COMPUTER_CERTIFICATE,
                                 .LANGUAGE = p.LANGUAGE,
                                 .LANGUAGE_NAME = language.NAME_VN,
                                 .LANGUAGE_LEVEL = p.LANGUAGE_LEVEL,
                                 .LANGUAGE_LEVEL_NAME = language_lv.NAME_VN,
                                 .LANGUAGE_MARK = p.LANGUAGE_MARK,
                                 .BANK_NO = p.BANK_NO,
                                 .BANK_ID = p.BANK_ID,
                                 .BANK_NAME = bank.NAME,
                                 .BANK_BRANCH_ID = p.BANK_BRANCH_ID,
                                 .BANK_BRANCH_NAME = bankbranch.NAME,
                                 .NO_HOUSEHOLDS = p.NO_HOUSEHOLDS,
                                 .PASS_NO = p.PASS_NO,
                                 .PASS_DATE = p.PASS_DATE,
                                 .PASS_EXPIRE = p.PASS_EXPIRE,
                                 .PASS_PLACE_ID = p.PASS_PLACE_ID,
                                 .PASS_PLACE = pass_place.NAME_VN,
                                 .VISA = p.VISA_NO,
                                 .VISA_DATE = p.VISA_DATE,
                                 .VISA_EXPIRE = p.VISA_EXPIRE,
                                 .VISA_PLACE_ID = p.VISA_PLACE_ID,
                                 .VISA_PLACE = visa_place.NAME_VN,
                                 .BOOK_NO = p.BOOK_NO,
                                 .BOOK_DATE = p.BOOK_DATE,
                                 .BOOK_EXPIRE = p.BOOK_EXPIRE,
                                 .SSLD_PLACE_ID = p.SSLD_PLACE_ID,
                                 .SSLD_PLACE = book_place.NAME_VN,
                                 .STATUS_NAME = If(p.STATUS = "0", "Chưa gửi duyệt",
                                                   If(p.STATUS = "1", "Chờ phê duyệt",
                                                      If(p.STATUS = "2", "Phê duyệt",
                                                         If(p.STATUS = "3", "Không phê duyệt", ""))))}).FirstOrDefault
                Return objEmpEdit
            End If

            If _filter.STATUS = "0" Then
                Dim count = (From p In Context.HU_EMPLOYEE_EDIT Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID AndAlso p.STATUS = "0").Count
                If count > 0 Then
                    objEmpEdit = (From p In Context.HU_EMPLOYEE_EDIT
                                  From nav_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.NAV_PROVINCE).DefaultIfEmpty
                                  From nav_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.NAV_DISTRICT).DefaultIfEmpty
                                  From nav_ward In Context.HU_WARD.Where(Function(f) f.ID = p.NAV_WARD).DefaultIfEmpty
                                  From per_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PER_PROVINCE).DefaultIfEmpty
                                  From per_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.PER_DISTRICT).DefaultIfEmpty
                                  From per_ward In Context.HU_WARD.Where(Function(f) f.ID = p.PER_WARD).DefaultIfEmpty
                                  From marital In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MARITAL_STATUS).DefaultIfEmpty
                                  From relation_per In Context.OT_OTHER_LIST.Where(Function(f) p.RELATION_PER_CTR = f.ID And f.TYPE_ID = 48 And f.ACTFLG = "A").DefaultIfEmpty
                                  From bank In Context.HU_BANK.Where(Function(f) p.BANK_ID = f.ID).DefaultIfEmpty
                                  From bankbranch In Context.HU_BANK_BRANCH.Where(Function(f) p.BANK_BRANCH_ID = f.ID).DefaultIfEmpty
                                  From academy In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.ACADEMY).DefaultIfEmpty
                                  From learn_lv In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEARNING_LEVEL).DefaultIfEmpty
                                  From major In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MAJOR).DefaultIfEmpty
                                  From school In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GRADUATE_SCHOOL_ID).DefaultIfEmpty
                                  From computer In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMPUTER_RANK).DefaultIfEmpty
                                  From com_mark In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMPUTER_MARK).DefaultIfEmpty
                                  From language In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LANGUAGE).DefaultIfEmpty
                                  From language_lv In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LANGUAGE_LEVEL).DefaultIfEmpty
                                  From rl In Context.OT_OTHER_LIST.Where(Function(f) p.RELIGION = f.ID).DefaultIfEmpty
                                  From nt In Context.OT_OTHER_LIST.Where(Function(f) p.NATIVE = f.ID).DefaultIfEmpty
                                  From pass_place In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PASS_PLACE_ID).DefaultIfEmpty
                                  From visa_place In Context.HU_PROVINCE.Where(Function(f) f.ID = p.VISA_PLACE_ID).DefaultIfEmpty
                                  From book_place In Context.HU_PROVINCE.Where(Function(f) f.ID = p.SSLD_PLACE_ID).DefaultIfEmpty
                                  Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID And p.STATUS = "0"
                                  Select New EmployeeEditDTO With {
                                     .ID = p.ID,
                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                     .ID_DATE = p.ID_DATE,
                                     .ID_NO = p.ID_NO,
                                     .ID_PLACE = p.ID_PLACE,
                                     .RELIGION = p.RELIGION,
                                     .RELIGION_NAME = rl.NAME_VN,
                                     .NATIVE = p.NATIVE,
                                     .NATIVE_NAME = nt.NAME_VN,
                                     .NOTE_CHANGE_CMND = p.NOTE_CHANGE_CMND,
                                     .FILE_CMND = p.FILE_CMND,
                                     .FILE_CMND_BACK = p.FILE_CMND_BACK,
                                     .FILE_ADDRESS = p.FILE_ADDRESS,
                                     .FILE_BANK = p.FILE_BANK,
                                     .ADDRESS_PER_CTR = p.ADDRESS_PER_CTR,
                                     .MARITAL_STATUS = p.MARITAL_STATUS,
                                     .MARITAL_STATUS_NAME = marital.NAME_VN,
                                     .NAV_ADDRESS = p.NAV_ADDRESS,
                                     .NAV_DISTRICT = p.NAV_DISTRICT,
                                     .NAV_DISTRICT_NAME = nav_dis.NAME_VN,
                                     .NAV_PROVINCE = p.NAV_PROVINCE,
                                     .NAV_PROVINCE_NAME = nav_pro.NAME_VN,
                                     .NAV_WARD = p.NAV_WARD,
                                     .NAV_WARD_NAME = nav_ward.NAME_VN,
                                     .PER_ADDRESS = p.PER_ADDRESS,
                                     .PER_DISTRICT = p.PER_DISTRICT,
                                     .PER_DISTRICT_NAME = per_dis.NAME_VN,
                                     .PER_PROVINCE = p.PER_PROVINCE,
                                     .PER_PROVINCE_NAME = per_pro.NAME_VN,
                                     .PER_WARD = p.PER_WARD,
                                     .PER_WARD_NAME = per_ward.NAME_VN,
                                     .REASON_UNAPROVE = p.REASON_UNAPROVE,
                                     .STATUS = p.STATUS,
                                     .EXPIRE_DATE_IDNO = p.EXPIRE_DATE_IDNO,
                                     .CONTACT_PER = p.CONTACT_PER,
                                     .RELATION_PER_CTR = p.RELATION_PER_CTR,
                                     .RELATION_PER_CTR_NAME = relation_per.NAME_VN,
                                     .CONTACT_PER_MBPHONE = p.CONTACT_PER_MBPHONE,
                                     .VILLAGE = p.VILLAGE,
                                     .HOME_PHONE = p.HOME_PHONE,
                                     .MOBILE_PHONE = p.MOBILE_PHONE,
                                     .WORK_EMAIL = p.WORK_EMAIL,
                                     .PER_EMAIL = p.PER_EMAIL,
                                     .PERSON_INHERITANCE = p.PERSON_INHERITANCE,
                                     .CMND_NOTE_CHANGE = p.NOTE_CHANGE_CMND,
                                     .ACADEMY = p.ACADEMY,
                                     .ACADEMY_NAME = academy.NAME_VN,
                                     .LEARNING_LEVEL = p.LEARNING_LEVEL,
                                     .LEARNING_LEVEL_NAME = learn_lv.NAME_VN,
                                     .MAJOR = p.MAJOR,
                                     .MAJOR_NAME = major.NAME_VN,
                                     .GRADUATE_SCHOOL_ID = p.GRADUATE_SCHOOL_ID,
                                     .GRADUATE_SCHOOL_NAME = school.NAME_VN,
                                     .GRADUATION_YEAR = p.GRADUATION_YEAR,
                                     .COMPUTER_RANK = p.COMPUTER_RANK,
                                     .COMPUTER_RANK_NAME = computer.NAME_VN,
                                     .COMPUTER_MARK = p.COMPUTER_MARK,
                                     .COMPUTER_MARK_NAME = com_mark.NAME_VN,
                                     .COMPUTER_CERTIFICATE = p.COMPUTER_CERTIFICATE,
                                     .LANGUAGE = p.LANGUAGE,
                                     .LANGUAGE_NAME = language.NAME_VN,
                                     .LANGUAGE_LEVEL = p.LANGUAGE_LEVEL,
                                     .LANGUAGE_LEVEL_NAME = language_lv.NAME_VN,
                                     .LANGUAGE_MARK = p.LANGUAGE_MARK,
                                     .BANK_NO = p.BANK_NO,
                                     .BANK_ID = p.BANK_ID,
                                     .BANK_NAME = bank.NAME,
                                     .BANK_BRANCH_ID = p.BANK_BRANCH_ID,
                                     .BANK_BRANCH_NAME = bankbranch.NAME,
                                     .NO_HOUSEHOLDS = p.NO_HOUSEHOLDS,
                                     .PASS_NO = p.PASS_NO,
                                     .PASS_DATE = p.PASS_DATE,
                                     .PASS_EXPIRE = p.PASS_EXPIRE,
                                     .PASS_PLACE_ID = p.PASS_PLACE_ID,
                                     .PASS_PLACE = pass_place.NAME_VN,
                                     .VISA = p.VISA_NO,
                                     .VISA_DATE = p.VISA_DATE,
                                     .VISA_EXPIRE = p.VISA_EXPIRE,
                                     .VISA_PLACE_ID = p.VISA_PLACE_ID,
                                     .VISA_PLACE = visa_place.NAME_VN,
                                     .BOOK_NO = p.BOOK_NO,
                                     .BOOK_DATE = p.BOOK_DATE,
                                     .BOOK_EXPIRE = p.BOOK_EXPIRE,
                                     .SSLD_PLACE_ID = p.SSLD_PLACE_ID,
                                     .SSLD_PLACE = book_place.NAME_VN,
                                     .STATUS_NAME = If(p.STATUS = "0", "Chưa gửi duyệt",
                                                       If(p.STATUS = "1", "Chờ phê duyệt",
                                                          If(p.STATUS = "2", "Phê duyệt",
                                                             If(p.STATUS = "3", "Không phê duyệt", ""))))}).FirstOrDefault
                    Return objEmpEdit
                Else
                    objEmpEdit = (From p In Context.HU_EMPLOYEE_CV
                                  From nav_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.NAV_PROVINCE).DefaultIfEmpty
                                  From nav_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.NAV_DISTRICT).DefaultIfEmpty
                                  From nav_ward In Context.HU_WARD.Where(Function(f) f.ID = p.NAV_WARD).DefaultIfEmpty
                                  From per_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PER_PROVINCE).DefaultIfEmpty
                                  From per_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.PER_DISTRICT).DefaultIfEmpty
                                  From per_ward In Context.HU_WARD.Where(Function(f) f.ID = p.PER_WARD).DefaultIfEmpty
                                  From marital In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MARITAL_STATUS).DefaultIfEmpty
                                  From relation_per In Context.OT_OTHER_LIST.Where(Function(f) p.RELATION_PER_CTR = f.ID And f.TYPE_ID = 48 And f.ACTFLG = "A").DefaultIfEmpty
                                  From bank In Context.HU_BANK.Where(Function(f) p.BANK_ID = f.ID).DefaultIfEmpty
                                  From bankbranch In Context.HU_BANK_BRANCH.Where(Function(f) p.BANK_BRANCH_ID = f.ID).DefaultIfEmpty
                                  From edu In Context.HU_EMPLOYEE_EDUCATION.Where(Function(f) f.EMPLOYEE_ID = p.EMPLOYEE_ID).DefaultIfEmpty
                                  From academy In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.ACADEMY).DefaultIfEmpty
                                  From learn_lv In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.LEARNING_LEVEL).DefaultIfEmpty
                                  From major In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.MAJOR).DefaultIfEmpty
                                  From school In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.GRADUATE_SCHOOL_ID).DefaultIfEmpty
                                  From computer In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.COMPUTER_RANK).DefaultIfEmpty
                                  From com_mark In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.COMPUTER_MARK).DefaultIfEmpty
                                  From language In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.LANGUAGE).DefaultIfEmpty
                                  From language_lv In Context.OT_OTHER_LIST.Where(Function(f) f.ID = edu.LANGUAGE_LEVEL).DefaultIfEmpty
                                  From rl In Context.OT_OTHER_LIST.Where(Function(f) p.RELIGION = f.ID).DefaultIfEmpty
                                  From nt In Context.OT_OTHER_LIST.Where(Function(f) p.NATIVE = f.ID).DefaultIfEmpty
                                  From pass_place In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PASS_PLACE_ID).DefaultIfEmpty
                                  From visa_place In Context.HU_PROVINCE.Where(Function(f) f.ID = p.VISA_PLACE_ID).DefaultIfEmpty
                                  From book_place In Context.HU_PROVINCE.Where(Function(f) f.ID = p.SSLD_PLACE_ID).DefaultIfEmpty
                                  Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID
                                  Select New EmployeeEditDTO With {
                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                     .ID_DATE = p.ID_DATE,
                                     .ID_NO = p.ID_NO,
                                     .ID_PLACE = p.ID_PLACE,
                                     .RELIGION = p.RELIGION,
                                     .RELIGION_NAME = rl.NAME_VN,
                                     .NATIVE = p.NATIVE,
                                     .NATIVE_NAME = nt.NAME_VN,
                                     .MARITAL_STATUS = p.MARITAL_STATUS,
                                     .MARITAL_STATUS_NAME = marital.NAME_VN,
                                     .NAV_ADDRESS = p.NAV_ADDRESS,
                                     .NAV_DISTRICT = p.NAV_DISTRICT,
                                     .NAV_DISTRICT_NAME = nav_dis.NAME_VN,
                                     .NAV_PROVINCE = p.NAV_PROVINCE,
                                     .NAV_PROVINCE_NAME = nav_pro.NAME_VN,
                                     .NAV_WARD = p.NAV_WARD,
                                     .NAV_WARD_NAME = nav_ward.NAME_VN,
                                     .PER_ADDRESS = p.PER_ADDRESS,
                                     .PER_DISTRICT = p.PER_DISTRICT,
                                     .PER_DISTRICT_NAME = per_dis.NAME_VN,
                                     .PER_PROVINCE = p.PER_PROVINCE,
                                     .PER_PROVINCE_NAME = per_pro.NAME_VN,
                                     .PER_WARD = p.PER_WARD,
                                     .PER_WARD_NAME = per_ward.NAME_VN,
                                     .EXPIRE_DATE_IDNO = p.EXPIRE_DATE_IDNO,
                                     .CONTACT_PER = p.CONTACT_PER,
                                     .RELATION_PER_CTR = p.RELATION_PER_CTR,
                                     .RELATION_PER_CTR_NAME = relation_per.NAME_VN,
                                     .CONTACT_PER_MBPHONE = p.CONTACT_PER_MBPHONE,
                                     .VILLAGE = p.VILLAGE,
                                     .HOME_PHONE = p.HOME_PHONE,
                                     .MOBILE_PHONE = p.MOBILE_PHONE,
                                     .WORK_EMAIL = p.WORK_EMAIL,
                                     .PER_EMAIL = p.PER_EMAIL,
                                     .PERSON_INHERITANCE = p.PERSON_INHERITANCE,
                                     .ACADEMY = edu.ACADEMY,
                                     .ACADEMY_NAME = academy.NAME_VN,
                                     .LEARNING_LEVEL = edu.LEARNING_LEVEL,
                                     .LEARNING_LEVEL_NAME = learn_lv.NAME_VN,
                                     .MAJOR = edu.MAJOR,
                                     .MAJOR_NAME = major.NAME_VN,
                                     .GRADUATE_SCHOOL_ID = edu.GRADUATE_SCHOOL_ID,
                                     .GRADUATE_SCHOOL_NAME = school.NAME_VN,
                                     .GRADUATION_YEAR = edu.GRADUATION_YEAR,
                                     .COMPUTER_RANK = edu.COMPUTER_RANK,
                                     .COMPUTER_RANK_NAME = computer.NAME_VN,
                                     .COMPUTER_MARK = edu.COMPUTER_MARK,
                                     .COMPUTER_MARK_NAME = com_mark.NAME_VN,
                                     .COMPUTER_CERTIFICATE = edu.COMPUTER_CERTIFICATE,
                                     .LANGUAGE = edu.LANGUAGE,
                                     .LANGUAGE_NAME = language.NAME_VN,
                                     .LANGUAGE_LEVEL = edu.LANGUAGE_LEVEL,
                                     .ADDRESS_PER_CTR = p.ADDRESS_PER_CTR,
                                     .LANGUAGE_LEVEL_NAME = language_lv.NAME_VN,
                                     .LANGUAGE_MARK = edu.LANGUAGE_MARK,
                                     .BANK_NO = p.BANK_NO,
                                     .BANK_ID = p.BANK_ID,
                                     .BANK_NAME = bank.NAME,
                                     .BANK_BRANCH_ID = p.BANK_BRANCH_ID,
                                     .BANK_BRANCH_NAME = bankbranch.NAME,
                                     .NO_HOUSEHOLDS = p.NO_HOUSEHOLDS,
                                     .PASS_NO = p.PASS_NO,
                                     .PASS_DATE = p.PASS_DATE,
                                     .PASS_EXPIRE = p.PASS_EXPIRE,
                                     .PASS_PLACE_ID = p.PASS_PLACE_ID,
                                     .PASS_PLACE = pass_place.NAME_VN,
                                     .VISA = p.VISA,
                                     .VISA_DATE = p.VISA_DATE,
                                     .VISA_EXPIRE = p.VISA_EXPIRE,
                                     .VISA_PLACE_ID = p.VISA_PLACE_ID,
                                     .VISA_PLACE = visa_place.NAME_VN,
                                     .BOOK_NO = p.BOOK_NO,
                                     .BOOK_DATE = p.BOOK_DATE,
                                     .BOOK_EXPIRE = p.BOOK_EXPIRE,
                                     .SSLD_PLACE_ID = p.SSLD_PLACE_ID,
                                     .SSLD_PLACE = book_place.NAME_VN}).FirstOrDefault

                    Dim count2 = (From p In Context.HU_EMPLOYEE_EDIT Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID).Count
                    If count2 = 0 Then
                        objEmpEdit.STATUS = "-1"
                    Else
                        Dim editLastest = (From p In Context.HU_EMPLOYEE_EDIT Where p.EMPLOYEE_ID = _filter.EMPLOYEE_ID AndAlso (p.STATUS = "2" OrElse p.STATUS = "3") Order By p.MODIFIED_DATE Descending).FirstOrDefault
                        objEmpEdit.STATUS = editLastest.STATUS
                        objEmpEdit.REASON_UNAPROVE = editLastest.REASON_UNAPROVE
                    End If

                    Return objEmpEdit
                End If
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetApproveEmployeeEdit(ByVal _filter As EmployeeEditDTO,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of EmployeeEditDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = (From p In Context.HU_EMPLOYEE_EDIT
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                         From place_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.ID_PLACE).DefaultIfEmpty
                         From nav_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.NAV_PROVINCE).DefaultIfEmpty
                         From nav_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.NAV_DISTRICT).DefaultIfEmpty
                         From nav_ward In Context.HU_WARD.Where(Function(f) f.ID = p.NAV_WARD).DefaultIfEmpty
                         From per_pro In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PER_PROVINCE).DefaultIfEmpty
                         From per_dis In Context.HU_DISTRICT.Where(Function(f) f.ID = p.PER_DISTRICT).DefaultIfEmpty
                         From per_ward In Context.HU_WARD.Where(Function(f) f.ID = p.PER_WARD).DefaultIfEmpty
                         From marital In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MARITAL_STATUS).DefaultIfEmpty
                         From relation_per In Context.OT_OTHER_LIST.Where(Function(f) p.RELATION_PER_CTR = f.ID And f.TYPE_ID = 48 And f.ACTFLG = "A").DefaultIfEmpty
                         From bank In Context.HU_BANK.Where(Function(f) p.BANK_ID = f.ID).DefaultIfEmpty
                         From bankbranch In Context.HU_BANK_BRANCH.Where(Function(f) p.BANK_BRANCH_ID = f.ID).DefaultIfEmpty
                         From academy In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.ACADEMY).DefaultIfEmpty
                         From learn_lv In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEARNING_LEVEL).DefaultIfEmpty
                         From major In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MAJOR).DefaultIfEmpty
                         From school In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GRADUATE_SCHOOL_ID).DefaultIfEmpty
                         From computer In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMPUTER_RANK).DefaultIfEmpty
                         From com_mark In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMPUTER_MARK).DefaultIfEmpty
                         From language In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LANGUAGE).DefaultIfEmpty
                         From language_lv In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LANGUAGE_LEVEL).DefaultIfEmpty
                         From rl In Context.OT_OTHER_LIST.Where(Function(f) p.RELIGION = f.ID).DefaultIfEmpty
                         From nt In Context.OT_OTHER_LIST.Where(Function(f) p.NATIVE = f.ID).DefaultIfEmpty
                         From fileBank In Context.HU_USERFILES.Where(Function(f) p.FILE_BANK = f.NAME).DefaultIfEmpty
                         From fileAddress In Context.HU_USERFILES.Where(Function(f) p.FILE_ADDRESS = f.NAME).DefaultIfEmpty
                         From fileImage In Context.HU_USERFILES.Where(Function(f) p.IMAGE = f.NAME).DefaultIfEmpty
                         From fileCMND In Context.HU_USERFILES.Where(Function(f) p.FILE_CMND = f.NAME).DefaultIfEmpty
                         From fileCMND_back In Context.HU_USERFILES.Where(Function(f) p.FILE_CMND_BACK = f.NAME).DefaultIfEmpty
                         From fileOther In Context.HU_USERFILES.Where(Function(f) p.FILE_OTHER = f.NAME).DefaultIfEmpty
                         From pass_place In Context.HU_PROVINCE.Where(Function(f) f.ID = p.PASS_PLACE_ID).DefaultIfEmpty
                         From visa_place In Context.HU_PROVINCE.Where(Function(f) f.ID = p.VISA_PLACE_ID).DefaultIfEmpty
                         From book_place In Context.HU_PROVINCE.Where(Function(f) f.ID = p.SSLD_PLACE_ID).DefaultIfEmpty
                         From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID _
                                                                             And f.USERNAME = log.Username.ToUpper)
                         Where p.STATUS = 1
                         Select New EmployeeEditDTO With {
                            .ID = p.ID,
                            .EMPLOYEE_ID = p.EMPLOYEE_ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .ID_DATE = p.ID_DATE,
                            .ID_NO = p.ID_NO,
                            .ID_PLACE = p.ID_PLACE,
                            .ID_PLACE_NAME = place_pro.NAME_VN,
                            .RELIGION = p.RELIGION,
                            .RELIGION_NAME = rl.NAME_VN,
                            .NATIVE = p.NATIVE,
                            .NATIVE_NAME = nt.NAME_VN,
                             .NOTE_CHANGE_CMND = p.NOTE_CHANGE_CMND,
                            .FILE_CMND = fileCMND.FILE_NAME,
                            .FILE_CMND_BACK = fileCMND_back.FILE_NAME,
                            .FILE_ADDRESS = fileAddress.FILE_NAME,
                            .FILE_BANK = fileBank.FILE_NAME,
                            .FILE_OTHER = fileOther.FILE_NAME,
                            .IMAGE = fileImage.FILE_NAME,
                            .UPLOAD_FILE_ADDRESS = fileAddress.NAME,
                            .UPLOAD_FILE_BANK = fileBank.NAME,
                            .UPLOAD_IMAGE = fileImage.NAME,
                            .UPLOAD_FILE_CMND = fileCMND.NAME,
                             .UPLOAD_FILE_CMND_BACK = fileCMND_back.NAME,
                            .UPLOAD_FILE_OTHER = fileOther.NAME,
                            .ADDRESS_PER_CTR = p.ADDRESS_PER_CTR,
                            .MARITAL_STATUS = p.MARITAL_STATUS,
                            .MARITAL_STATUS_NAME = marital.NAME_VN,
                            .NAV_ADDRESS = p.NAV_ADDRESS,
                            .NAV_DISTRICT = p.NAV_DISTRICT,
                            .NAV_DISTRICT_NAME = nav_dis.NAME_VN,
                            .NAV_PROVINCE = p.NAV_PROVINCE,
                            .NAV_PROVINCE_NAME = nav_pro.NAME_VN,
                            .NAV_WARD = p.NAV_WARD,
                            .NAV_WARD_NAME = nav_ward.NAME_VN,
                            .PER_ADDRESS = p.PER_ADDRESS,
                            .PER_DISTRICT = p.PER_DISTRICT,
                            .PER_DISTRICT_NAME = per_dis.NAME_VN,
                            .PER_PROVINCE = p.PER_PROVINCE,
                            .PER_PROVINCE_NAME = per_pro.NAME_VN,
                            .PER_WARD = p.PER_WARD,
                            .PER_WARD_NAME = per_ward.NAME_VN,
                            .STATUS = p.STATUS,
                            .EXPIRE_DATE_IDNO = p.EXPIRE_DATE_IDNO,
                            .CONTACT_PER = p.CONTACT_PER,
                            .RELATION_PER_CTR = p.RELATION_PER_CTR,
                            .RELATION_PER_CTR_NAME = relation_per.NAME_VN,
                            .CONTACT_PER_MBPHONE = p.CONTACT_PER_MBPHONE,
                            .VILLAGE = p.VILLAGE,
                            .HOME_PHONE = p.HOME_PHONE,
                            .MOBILE_PHONE = p.MOBILE_PHONE,
                            .WORK_EMAIL = p.WORK_EMAIL,
                            .PER_EMAIL = p.PER_EMAIL,
                            .PERSON_INHERITANCE = p.PERSON_INHERITANCE,
                            .BANK_NO = p.BANK_NO,
                            .BANK_ID = p.BANK_ID,
                            .BANK_NAME = bank.NAME,
                            .BANK_BRANCH_ID = p.BANK_BRANCH_ID,
                            .BANK_BRANCH_NAME = bankbranch.NAME,
                            .CMND_NOTE_CHANGE = p.NOTE_CHANGE_CMND,
                            .ACADEMY = p.ACADEMY,
                            .ACADEMY_NAME = academy.NAME_VN,
                            .LEARNING_LEVEL = p.LEARNING_LEVEL,
                            .LEARNING_LEVEL_NAME = learn_lv.NAME_VN,
                            .MAJOR = p.MAJOR,
                            .MAJOR_NAME = major.NAME_VN,
                            .GRADUATE_SCHOOL_ID = p.GRADUATE_SCHOOL_ID,
                            .GRADUATE_SCHOOL_NAME = school.NAME_VN,
                            .GRADUATION_YEAR = p.GRADUATION_YEAR,
                            .COMPUTER_RANK = p.COMPUTER_RANK,
                            .COMPUTER_RANK_NAME = computer.NAME_VN,
                            .COMPUTER_MARK = p.COMPUTER_MARK,
                            .COMPUTER_MARK_NAME = com_mark.NAME_VN,
                            .COMPUTER_CERTIFICATE = p.COMPUTER_CERTIFICATE,
                            .LANGUAGE = p.LANGUAGE,
                            .LANGUAGE_NAME = language.NAME_VN,
                            .LANGUAGE_LEVEL = p.LANGUAGE_LEVEL,
                            .LANGUAGE_LEVEL_NAME = language_lv.NAME_VN,
                            .LANGUAGE_MARK = p.LANGUAGE_MARK,
                             .NO_HOUSEHOLDS = p.NO_HOUSEHOLDS,
                            .PASS_NO = p.PASS_NO,
                            .PASS_DATE = p.PASS_DATE,
                            .PASS_EXPIRE = p.PASS_EXPIRE,
                            .PASS_PLACE_ID = p.PASS_PLACE_ID,
                            .PASS_PLACE = pass_place.NAME_VN,
                            .VISA = p.VISA_NO,
                            .VISA_DATE = p.VISA_DATE,
                            .VISA_EXPIRE = p.VISA_EXPIRE,
                            .VISA_PLACE_ID = p.VISA_PLACE_ID,
                            .VISA_PLACE = visa_place.NAME_VN,
                            .BOOK_NO = p.BOOK_NO,
                            .BOOK_DATE = p.BOOK_DATE,
                            .BOOK_EXPIRE = p.BOOK_EXPIRE,
                            .SSLD_PLACE_ID = p.SSLD_PLACE_ID,
                            .SSLD_PLACE = book_place.NAME_VN,
                            .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                              If(p.STATUS = 1, "Chờ phê duyệt",
                                                 If(p.STATUS = 2, "Phê duyệt",
                                                    If(p.STATUS = 3, "Không phê duyệt", ""))))})

            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            Dim working = query

            working = working.OrderBy(Sorts)
            Total = working.Count
            working = working.Skip(PageIndex * PageSize).Take(PageSize)

            Return working.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function DeleteEmployeeEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_EMPLOYEE_EDIT)
        Try
            lst = (From p In Context.HU_EMPLOYEE_EDIT Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_EMPLOYEE_EDIT.DeleteObject(lst(i))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function SendEmployeeEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        Try
            Dim lstObj = (From p In Context.HU_EMPLOYEE_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lstObj
                item.SEND_DATE = Date.Now
                item.STATUS = 1
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusEmployeeEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_EMPLOYEE_EDIT)
        Dim sStatus() As String = status.Split(":")
        Dim emp_from = (From p In Context.SE_USER Where p.USERNAME = log.Username.ToUpper Select p.EMPLOYEE_ID).FirstOrDefault
        Try
            lst = (From p In Context.HU_EMPLOYEE_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lst
                item.STATUS = sStatus(0)
                item.REASON_UNAPROVE = sStatus(1)
                item.MODIFIED_DATE = Date.Now

                Dim noti As New SE_NOTIFICATION
                noti.ID = Utilities.GetNextSequence(Context, Context.SE_NOTIFICATION.EntitySet.Name)
                noti.FROM_EMPLOYEE_ID = emp_from
                noti.TO_EMPLOYEE_ID = item.EMPLOYEE_ID
                noti.PROCESS_TYPE = "EMP_EDIT"
                noti.SEND_STATUS = sStatus(0)
                noti.MESSAGE = sStatus(1)
                noti.SENT_DATE = System.DateTime.Now
                Context.SE_NOTIFICATION.AddObject(noti)

                If sStatus(0) = 2 Then

                    Dim objEmployeeData As HU_EMPLOYEE_CV
                    objEmployeeData = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = item.EMPLOYEE_ID).FirstOrDefault
                    objEmployeeData.ID_DATE = item.ID_DATE
                    objEmployeeData.ID_NO = item.ID_NO
                    objEmployeeData.ID_PLACE = item.ID_PLACE
                    objEmployeeData.MARITAL_STATUS = item.MARITAL_STATUS
                    objEmployeeData.NAV_ADDRESS = item.NAV_ADDRESS
                    objEmployeeData.ADDRESS_PER_CTR = item.ADDRESS_PER_CTR
                    objEmployeeData.NAV_DISTRICT = item.NAV_DISTRICT
                    objEmployeeData.NAV_PROVINCE = item.NAV_PROVINCE
                    objEmployeeData.NAV_WARD = item.NAV_WARD
                    objEmployeeData.PER_ADDRESS = item.PER_ADDRESS
                    objEmployeeData.PER_DISTRICT = item.PER_DISTRICT
                    objEmployeeData.PER_PROVINCE = item.PER_PROVINCE
                    objEmployeeData.PER_WARD = item.PER_WARD

                    objEmployeeData.EXPIRE_DATE_IDNO = item.EXPIRE_DATE_IDNO
                    objEmployeeData.CONTACT_PER = item.CONTACT_PER
                    objEmployeeData.CONTACT_PER_MBPHONE = item.CONTACT_PER_MBPHONE
                    objEmployeeData.RELATION_PER_CTR = item.RELATION_PER_CTR
                    objEmployeeData.VILLAGE = item.VILLAGE
                    objEmployeeData.HOME_PHONE = item.HOME_PHONE
                    objEmployeeData.MOBILE_PHONE = item.MOBILE_PHONE
                    objEmployeeData.WORK_EMAIL = item.WORK_EMAIL
                    Dim objUser = (From p In Context.SE_USER Where p.EMPLOYEE_ID = item.EMPLOYEE_ID).FirstOrDefault
                    If objUser IsNot Nothing Then
                        objUser.EMAIL = item.WORK_EMAIL
                    End If
                    objEmployeeData.PER_EMAIL = item.PER_EMAIL
                    objEmployeeData.PERSON_INHERITANCE = item.PERSON_INHERITANCE
                    objEmployeeData.BANK_NO = item.BANK_NO
                    objEmployeeData.BANK_ID = item.BANK_ID
                    objEmployeeData.BANK_BRANCH_ID = item.BANK_BRANCH_ID
                    objEmployeeData.NO_HOUSEHOLDS = item.NO_HOUSEHOLDS
                    objEmployeeData.RELIGION = item.RELIGION
                    objEmployeeData.NATIVE = item.NATIVE

                    objEmployeeData.PASS_NO = item.PASS_NO
                    objEmployeeData.PASS_DATE = item.PASS_DATE
                    objEmployeeData.PASS_EXPIRE = item.PASS_EXPIRE
                    objEmployeeData.PASS_PLACE_ID = item.PASS_PLACE_ID
                    objEmployeeData.VISA = item.VISA_NO
                    objEmployeeData.VISA_DATE = item.VISA_DATE
                    objEmployeeData.VISA_EXPIRE = item.VISA_EXPIRE
                    objEmployeeData.VISA_PLACE_ID = item.VISA_PLACE_ID
                    objEmployeeData.BOOK_NO = item.BOOK_NO
                    objEmployeeData.BOOK_DATE = item.BOOK_DATE
                    objEmployeeData.BOOK_EXPIRE = item.BOOK_EXPIRE
                    objEmployeeData.SSLD_PLACE_ID = item.SSLD_PLACE_ID

                    Dim objEmployee As HU_EMPLOYEE
                    objEmployee = (From p In Context.HU_EMPLOYEE Where p.ID = item.EMPLOYEE_ID).FirstOrDefault
                    objEmployee.FILE_OTHER = item.FILE_OTHER

                    objEmployeeData.IMAGE = CopyEmployeeImageEditToEmployeeImage(item.ID)
                    Dim empEdu = (From p In Context.HU_EMPLOYEE_EDUCATION Where p.EMPLOYEE_ID = item.EMPLOYEE_ID).FirstOrDefault
                    If IsNothing(empEdu) Then
                        empEdu = New HU_EMPLOYEE_EDUCATION
                    End If
                    empEdu.ACADEMY = item.ACADEMY
                    empEdu.LEARNING_LEVEL = item.LEARNING_LEVEL
                    empEdu.MAJOR = item.MAJOR
                    empEdu.GRADUATE_SCHOOL_ID = item.GRADUATE_SCHOOL_ID
                    empEdu.GRADUATION_YEAR = item.GRADUATION_YEAR
                    empEdu.COMPUTER_RANK = item.COMPUTER_RANK
                    empEdu.COMPUTER_MARK = item.COMPUTER_MARK
                    empEdu.COMPUTER_CERTIFICATE = item.COMPUTER_CERTIFICATE
                    empEdu.LANGUAGE = item.LANGUAGE
                    empEdu.LANGUAGE_LEVEL = item.LANGUAGE_LEVEL
                    empEdu.LANGUAGE_MARK = item.LANGUAGE_MARK
                    If IsNothing(empEdu.EMPLOYEE_ID) Then
                        empEdu.EMPLOYEE_ID = item.EMPLOYEE_ID
                        Context.HU_EMPLOYEE_EDUCATION.AddObject(empEdu)
                    End If
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CopyEmployeeImageEditToEmployeeImage(ByVal Id As Decimal) As String
        Try
            Dim filepath = ""
            Dim _fileInfo As IO.FileInfo
            Dim imageName = ""

            Dim employeeCode = (From e1 In Context.HU_EMPLOYEE_EDIT
                                From e2 In Context.HU_EMPLOYEE.Where(Function(f) f.ID = e1.EMPLOYEE_ID).DefaultIfEmpty
                                Where e1.ID = Id
                                Select e2.EMPLOYEE_CODE).FirstOrDefault

            Dim userFiles = (From p In Context.HU_EMPLOYEE_EDIT
                             From u In Context.HU_USERFILES.Where(Function(f) f.NAME = p.IMAGE).DefaultIfEmpty
                             Where p.ID = Id
                             Select New FileUploadDTO With {
                                .LINK = u.LINK,
                                .FILE_NAME = u.FILE_NAME
                            }).FirstOrDefault

            filepath = AppDomain.CurrentDomain.BaseDirectory & "\" & userFiles.LINK & userFiles.FILE_NAME

            If File.Exists(filepath) Then
                _fileInfo = New FileInfo(filepath)
                imageName = employeeCode & _fileInfo.Extension
                Dim destinationFile = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage\" & imageName
                _fileInfo.CopyTo(destinationFile, True)
                Return imageName
            Else
                Return ""
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "IPORTAL - Quá trình đào tạo ngoài vào công ty"

    Public Function GetProcessTrainingEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Dim query As ObjectQuery(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)
        Try
            query = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT
                     From c In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE).DefaultIfEmpty
                     From ot In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.FORM_TRAIN_ID).DefaultIfEmpty
                     From ot2 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE_GROUP_ID).DefaultIfEmpty
                     From ot3 In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.CERTIFICATE_TYPE_ID).DefaultIfEmpty
                     From ot_level In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.LEVEL_ID).DefaultIfEmpty
                     From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.MAJOR).DefaultIfEmpty
                     From o2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GRADUATE_SCHOOL).DefaultIfEmpty
                     From f In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_NAME).DefaultIfEmpty
                     Select New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT With {
                        .ID = p.ID,
                        .EMPLOYEE_ID = p.EMPLOYEE_ID,
                        .FROM_DATE = p.FROM_DATE,
                        .TO_DATE = p.TO_DATE,
                        .YEAR_GRA = p.YEAR_GRA,
                        .NAME_SHOOLS = p.NAME_SHOOLS,
                        .FORM_TRAIN_ID = p.FORM_TRAIN_ID,
                        .FORM_TRAIN_NAME = ot.NAME_VN,
                        .CERTIFICATE_GROUP_NAME = ot2.NAME_VN,
                        .CERTIFICATE_TYPE_NAME = ot3.NAME_VN,
                        .SPECIALIZED_TRAIN = p.SPECIALIZED_TRAIN,
                        .POINT_LEVEL = p.POINT_LEVEL,
                        .RESULT_TRAIN = p.RESULT_TRAIN,
                        .SCORE = p.SCORE,
                        .CERTIFICATE_ID = p.CERTIFICATE,
                        .CERTIFICATE = c.NAME_VN,
                        .CERTIFICATE_NAME = p.CERTIFICATE_NAME,
                        .EFFECTIVE_DATE_FROM = p.EFFECTIVE_DATE_FROM,
                        .EFFECTIVE_DATE_TO = p.EFFECTIVE_DATE_TO,
                        .CREATED_BY = p.CREATED_BY,
                        .CREATED_DATE = p.CREATED_DATE,
                        .CREATED_LOG = p.CREATED_LOG,
                        .MODIFIED_BY = p.MODIFIED_BY,
                        .MODIFIED_DATE = p.MODIFIED_DATE,
                        .MODIFIED_LOG = p.MODIFIED_LOG,
                        .REASON_UNAPROVE = p.REASON_UNAPROVE,
                        .FK_PKEY = p.FK_PKEY,
                        .LEVEL_ID = p.LEVEL_ID,
                        .LEVEL_NAME = ot_level.NAME_VN,
                        .TYPE_TRAIN_ID = p.TYPE_TRAIN_ID,
                        .TYPE_TRAIN_NAME = p.TYPE_TRAIN_NAME,
                        .CODE_CERTIFICATE = p.CODE_CERTIFICATE,
                        .CONTENT_TRAIN = p.CONTENT_TRAIN,
                        .IS_RENEWED = p.IS_RENEW,
                        .NOTE = p.NOTE,
                        .COMMITMENT_TIME = p.COMMITMENT_TIME,
                        .STATUS = p.STATUS,
                        .UPLOAD_FILE = f.NAME,
                        .FILE_NAME = f.FILE_NAME,
                        .MAJOR = p.MAJOR,
                        .MAJOR_NAME = o1.NAME_VN,
                        .CONTENT_LEVEL = p.CONTENT_LEVEL,
                        .GRADUATE_SCHOOL = p.GRADUATE_SCHOOL,
                        .GRADUATE_SCHOOL_NAME = o2.NAME_VN,
                        .IS_MAJOR = If(p.IS_MAJOR = -1, True, False),
                        .IS_MAIN = If(p.IS_MAIN = -1, True, False),
                        .CERTIFICATE_GROUP_ID = p.CERTIFICATE_GROUP_ID,
                        .CERTIFICATE_TYPE_ID = p.CERTIFICATE_TYPE_ID,
                        .STATUS_NAME = If(p.STATUS = "0", "Chưa gửi duyệt",
                                           If(p.STATUS = "1", "Chờ phê duyệt",
                                              If(p.STATUS = "2", "Phê duyệt",
                                                 If(p.STATUS = "3", "Không phê duyệt", ""))))})

            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.STATUS <> "" Then
                query = query.Where(Function(p) p.STATUS = _filter.STATUS)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetApproveProcessTrainingEdit(ByVal _filter As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of HU_PRO_TRAIN_OUT_COMPANYDTOEDIT)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT
                         From ot In Context.OT_OTHER_LIST.Where(Function(F) F.ID = p.FORM_TRAIN_ID And F.TYPE_ID = 142).DefaultIfEmpty
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                         From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                         Where p.STATUS = 1
                         Select New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT With {
                             .ID = p.ID,
                             .EMPLOYEE_ID = p.EMPLOYEE_ID,
                             .FROM_DATE = p.FROM_DATE,
                             .TO_DATE = p.TO_DATE,
                             .YEAR_GRA = p.YEAR_GRA,
                             .NAME_SHOOLS = p.NAME_SHOOLS,
                             .FORM_TRAIN_ID = p.FORM_TRAIN_ID,
                             .FORM_TRAIN_NAME = ot.NAME_VN,
                             .SPECIALIZED_TRAIN = p.SPECIALIZED_TRAIN,
                             .RESULT_TRAIN = p.RESULT_TRAIN,
                             .CERTIFICATE = p.CERTIFICATE,
                             .EFFECTIVE_DATE_FROM = p.EFFECTIVE_DATE_FROM,
                             .EFFECTIVE_DATE_TO = p.EFFECTIVE_DATE_TO,
                             .CREATED_BY = p.CREATED_BY,
                             .CREATED_DATE = p.CREATED_DATE,
                             .CREATED_LOG = p.CREATED_LOG,
                             .MODIFIED_BY = p.MODIFIED_BY,
                             .MODIFIED_DATE = p.MODIFIED_DATE,
                             .MODIFIED_LOG = p.MODIFIED_LOG,
                             .FK_PKEY = p.FK_PKEY,
                             .POINT_LEVEL = p.POINT_LEVEL,
                             .LEVEL_ID = p.LEVEL_ID,
                             .TYPE_TRAIN_ID = p.TYPE_TRAIN_ID,
                             .TYPE_TRAIN_NAME = p.TYPE_TRAIN_NAME,
                             .CERTIFICATE_ID = p.CERTIFICATE,
                             .CODE_CERTIFICATE = p.CODE_CERTIFICATE,
                             .CONTENT_TRAIN = p.CONTENT_TRAIN,
                             .IS_RENEWED = p.IS_RENEW,
                             .NOTE = p.NOTE,
                             .COMMITMENT_TIME = p.COMMITMENT_TIME,
                             .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                               If(p.STATUS = 1, "Chờ phê duyệt",
                                                  If(p.STATUS = 2, "Phê duyệt",
                                                     If(p.STATUS = 3, "Không phê duyệt", ""))))})


            If _filter.YEAR_GRA IsNot Nothing Then
                query = query.Where(Function(p) p.YEAR_GRA = _filter.YEAR_GRA)
            End If

            If _filter.NAME_SHOOLS IsNot Nothing Then
                query = query.Where(Function(p) p.NAME_SHOOLS.ToUpper.Contains(_filter.NAME_SHOOLS.ToUpper))
            End If

            If _filter.FORM_TRAIN_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.FORM_TRAIN_NAME.ToUpper.Contains(_filter.FORM_TRAIN_NAME.ToUpper))
            End If

            If _filter.SPECIALIZED_TRAIN IsNot Nothing Then
                query = query.Where(Function(p) p.SPECIALIZED_TRAIN.ToUpper.Contains(_filter.SPECIALIZED_TRAIN.ToUpper))
            End If

            If _filter.RESULT_TRAIN IsNot Nothing Then
                query = query.Where(Function(p) p.RESULT_TRAIN.ToUpper.Contains(_filter.RESULT_TRAIN.ToUpper))
            End If

            If _filter.CERTIFICATE IsNot Nothing Then
                query = query.Where(Function(p) p.CERTIFICATE.ToUpper.Contains(_filter.CERTIFICATE.ToUpper))
            End If

            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            If _filter.EFFECTIVE_DATE_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECTIVE_DATE_FROM = _filter.EFFECTIVE_DATE_FROM)
            End If

            If _filter.EFFECTIVE_DATE_TO IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECTIVE_DATE_TO = _filter.EFFECTIVE_DATE_TO)
            End If

            Dim working = query

            working = working.OrderBy(Sorts)
            Total = working.Count
            working = working.Skip(PageIndex * PageSize).Take(PageSize)

            Return working.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertProcessTrainingEdit(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_PRO_TRAIN_OUT_COMPANY_EDIT
        Dim iCount As Integer = 0
        Try
            ' do tên bảng với tên sequence khác nhau
            Dim seq As Decimal?
            Dim tbl_name As String = "HU_PRO_TRAIN_OUT_COMPANY_EDIT"
            While (True)
                Try
                    seq = Context.ExecuteStoreQuery(Of Decimal?)("select SEQ_HU_PRO_TRAINOUTCOMPANYEDIT.nextval from DUAL").FirstOrDefault
                    Dim maxID As Decimal? = Context.ExecuteStoreQuery(Of Decimal?)("select Max(ID) from " & tbl_name).FirstOrDefault
                    If maxID IsNot Nothing AndAlso maxID >= seq Then
                        Continue While
                    End If
                    Exit While
                Catch ex As Exception
                    WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                End Try
            End While

            objTitleData.ID = seq
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.YEAR_GRA = objTitle.YEAR_GRA
            objTitleData.NAME_SHOOLS = objTitle.NAME_SHOOLS
            objTitleData.FORM_TRAIN_ID = If(objTitle.FORM_TRAIN_ID IsNot Nothing AndAlso objTitle.FORM_TRAIN_ID <> 0, objTitle.FORM_TRAIN_ID, Nothing)
            objTitleData.SPECIALIZED_TRAIN = objTitle.SPECIALIZED_TRAIN
            objTitleData.RESULT_TRAIN = objTitle.RESULT_TRAIN
            objTitleData.EFFECTIVE_DATE_FROM = objTitle.EFFECTIVE_DATE_FROM
            objTitleData.EFFECTIVE_DATE_TO = objTitle.EFFECTIVE_DATE_TO
            objTitleData.POINT_LEVEL = objTitle.POINT_LEVEL
            objTitleData.STATUS = "0"
            objTitleData.REASON_UNAPROVE = objTitle.REASON_UNAPROVE
            objTitleData.LEVEL_ID = objTitle.LEVEL_ID
            objTitleData.TYPE_TRAIN_ID = objTitle.TYPE_TRAIN_ID
            objTitleData.TYPE_TRAIN_NAME = objTitle.TYPE_TRAIN_NAME
            objTitleData.CERTIFICATE = objTitle.CERTIFICATE_ID
            objTitleData.CODE_CERTIFICATE = objTitle.CODE_CERTIFICATE
            objTitleData.CONTENT_TRAIN = objTitle.CONTENT_TRAIN
            objTitleData.IS_RENEW = objTitle.IS_RENEWED
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.COMMITMENT_TIME = objTitle.COMMITMENT_TIME
            objTitleData.FK_PKEY = If(objTitle.FK_PKEY IsNot Nothing AndAlso objTitle.FK_PKEY <> 0, objTitle.FK_PKEY, Nothing)
            If Not String.IsNullOrEmpty(objTitle.FILE_NAME) Then
                objTitleData.FILE_NAME = objTitle.FILE_NAME
            End If
            objTitleData.IS_MAIN = objTitle.IS_MAIN
            objTitleData.IS_MAIN = objTitle.IS_MAIN
            objTitleData.CERTIFICATE_GROUP_ID = objTitle.CERTIFICATE_GROUP_ID
            objTitleData.CERTIFICATE_TYPE_ID = objTitle.CERTIFICATE_TYPE_ID
            objTitleData.CERTIFICATE_NAME = objTitle.CERTIFICATE_NAME
            objTitleData.MAJOR = objTitle.MAJOR
            objTitleData.IS_MAJOR = objTitle.IS_MAJOR
            objTitleData.CONTENT_LEVEL = objTitle.CONTENT_LEVEL
            objTitleData.GRADUATE_SCHOOL = objTitle.GRADUATE_SCHOOL
            Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID

            If objTitle.IS_SEND = 1 Then
                Dim lst = New List(Of Decimal)
                lst.Add(gID)
                SendProcessTrainingEdit(lst, log)
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyProcessTrainingEdit(ByVal objTitle As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_PRO_TRAIN_OUT_COMPANY_EDIT With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where p.ID = objTitle.ID).FirstOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.YEAR_GRA = objTitle.YEAR_GRA
            objTitleData.NAME_SHOOLS = objTitle.NAME_SHOOLS
            objTitleData.FORM_TRAIN_ID = If(objTitle.FORM_TRAIN_ID IsNot Nothing AndAlso objTitle.FORM_TRAIN_ID <> 0, objTitle.FORM_TRAIN_ID, Nothing)
            objTitleData.SPECIALIZED_TRAIN = objTitle.SPECIALIZED_TRAIN
            objTitleData.RESULT_TRAIN = objTitle.RESULT_TRAIN
            objTitleData.EFFECTIVE_DATE_FROM = objTitle.EFFECTIVE_DATE_FROM
            objTitleData.EFFECTIVE_DATE_TO = objTitle.EFFECTIVE_DATE_TO
            objTitleData.POINT_LEVEL = objTitle.POINT_LEVEL
            objTitleData.STATUS = "0"
            objTitleData.REASON_UNAPROVE = objTitle.REASON_UNAPROVE
            objTitleData.LEVEL_ID = objTitle.LEVEL_ID
            objTitleData.TYPE_TRAIN_ID = objTitle.TYPE_TRAIN_ID
            objTitleData.TYPE_TRAIN_NAME = objTitle.TYPE_TRAIN_NAME
            objTitleData.CERTIFICATE = objTitle.CERTIFICATE_ID
            objTitleData.CODE_CERTIFICATE = objTitle.CODE_CERTIFICATE
            objTitleData.CONTENT_TRAIN = objTitle.CONTENT_TRAIN
            objTitleData.IS_RENEW = objTitle.IS_RENEWED
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.COMMITMENT_TIME = objTitle.COMMITMENT_TIME
            objTitleData.FK_PKEY = If(objTitle.FK_PKEY IsNot Nothing AndAlso objTitle.FK_PKEY <> 0, objTitle.FK_PKEY, Nothing)
            If Not String.IsNullOrEmpty(objTitle.FILE_NAME) Then
                objTitleData.FILE_NAME = objTitle.FILE_NAME
            End If
            objTitleData.IS_MAIN = objTitle.IS_MAIN
            objTitleData.IS_MAIN = objTitle.IS_MAIN
            objTitleData.CERTIFICATE_GROUP_ID = objTitle.CERTIFICATE_GROUP_ID
            objTitleData.CERTIFICATE_TYPE_ID = objTitle.CERTIFICATE_TYPE_ID
            objTitleData.CERTIFICATE_NAME = objTitle.CERTIFICATE_NAME
            objTitleData.MAJOR = objTitle.MAJOR
            objTitleData.IS_MAJOR = objTitle.IS_MAJOR
            objTitleData.CONTENT_LEVEL = objTitle.CONTENT_LEVEL
            objTitleData.GRADUATE_SCHOOL = objTitle.GRADUATE_SCHOOL
            Context.SaveChanges(log)
            gID = objTitleData.ID

            If objTitle.IS_SEND = 1 Then
                Dim lst = New List(Of Decimal)
                lst.Add(gID)
                SendProcessTrainingEdit(lst, log)
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteProcessTrainingEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstInsChangeTypeData As List(Of HU_PRO_TRAIN_OUT_COMPANY_EDIT)
        Try
            lstInsChangeTypeData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where lstDecimals.Contains(p.ID)).ToList
            For index = 0 To lstInsChangeTypeData.Count - 1
                Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT.DeleteObject(lstInsChangeTypeData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function CheckExistProcessTrainingEdit(ByVal pk_key As Decimal) As HU_PRO_TRAIN_OUT_COMPANYDTOEDIT
        Try
            Dim query = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT
                         Where p.STATUS <> "2" And p.STATUS <> "3" And p.FK_PKEY = pk_key
                         Select New HU_PRO_TRAIN_OUT_COMPANYDTOEDIT With {
                             .ID = p.ID,
                             .STATUS = p.STATUS}).FirstOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function SendProcessTrainingEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        Try
            Dim lstObj = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lstObj
                item.SEND_DATE = Date.Now
                item.STATUS = "1"
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusProcessTrainingEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean

        Dim lst As List(Of HU_PRO_TRAIN_OUT_COMPANY_EDIT)
        Dim sStatus() As String = status.Split(":")

        Try
            lst = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lst
                item.STATUS = sStatus(0)
                item.REASON_UNAPROVE = sStatus(1)

                If sStatus(0) = 2 Then
                    If item.FK_PKEY IsNot Nothing Then
                        Dim objProcessTrainData As New HU_PRO_TRAIN_OUT_COMPANY
                        objProcessTrainData = (From p In Context.HU_PRO_TRAIN_OUT_COMPANY Where p.ID = item.FK_PKEY).FirstOrDefault
                        objProcessTrainData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objProcessTrainData.FROM_DATE = item.FROM_DATE
                        objProcessTrainData.TO_DATE = item.TO_DATE
                        objProcessTrainData.YEAR_GRA = item.YEAR_GRA
                        objProcessTrainData.NAME_SHOOLS = item.NAME_SHOOLS
                        objProcessTrainData.FORM_TRAIN_ID = item.FORM_TRAIN_ID
                        objProcessTrainData.SPECIALIZED_TRAIN = item.SPECIALIZED_TRAIN
                        objProcessTrainData.RESULT_TRAIN = item.RESULT_TRAIN
                        objProcessTrainData.CERTIFICATE = item.CERTIFICATE
                        objProcessTrainData.POINT_LEVEL = item.POINT_LEVEL
                        objProcessTrainData.EFFECTIVE_DATE_FROM = item.EFFECTIVE_DATE_FROM
                        objProcessTrainData.EFFECTIVE_DATE_TO = item.EFFECTIVE_DATE_TO
                        objProcessTrainData.LEVEL_ID = item.LEVEL_ID
                        objProcessTrainData.TYPE_TRAIN_ID = item.TYPE_TRAIN_ID
                        objProcessTrainData.TYPE_TRAIN_NAME = item.TYPE_TRAIN_NAME
                        objProcessTrainData.CERTIFICATE = item.CERTIFICATE
                        objProcessTrainData.CERTIFICATE_CODE = item.CODE_CERTIFICATE
                        objProcessTrainData.CONTENT_LEVEL = item.CONTENT_TRAIN
                        objProcessTrainData.IS_RENEWED = item.IS_RENEW
                        objProcessTrainData.NOTE = item.NOTE
                        objProcessTrainData.COMMITMENT_TIME = item.COMMITMENT_TIME
                    Else
                        Dim objProcessTrainData As New HU_PRO_TRAIN_OUT_COMPANY
                        objProcessTrainData.ID = Utilities.GetNextSequence(Context, Context.HU_PRO_TRAIN_OUT_COMPANY.EntitySet.Name)
                        objProcessTrainData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objProcessTrainData.FROM_DATE = item.FROM_DATE
                        objProcessTrainData.TO_DATE = item.TO_DATE
                        objProcessTrainData.YEAR_GRA = item.YEAR_GRA
                        objProcessTrainData.NAME_SHOOLS = item.NAME_SHOOLS
                        objProcessTrainData.FORM_TRAIN_ID = item.FORM_TRAIN_ID
                        objProcessTrainData.SPECIALIZED_TRAIN = item.SPECIALIZED_TRAIN
                        objProcessTrainData.RESULT_TRAIN = item.RESULT_TRAIN
                        objProcessTrainData.CERTIFICATE = item.CERTIFICATE
                        objProcessTrainData.POINT_LEVEL = item.POINT_LEVEL
                        objProcessTrainData.EFFECTIVE_DATE_FROM = item.EFFECTIVE_DATE_FROM
                        objProcessTrainData.EFFECTIVE_DATE_TO = item.EFFECTIVE_DATE_TO
                        objProcessTrainData.LEVEL_ID = item.LEVEL_ID
                        objProcessTrainData.TYPE_TRAIN_ID = item.TYPE_TRAIN_ID
                        objProcessTrainData.TYPE_TRAIN_NAME = item.TYPE_TRAIN_NAME
                        objProcessTrainData.CERTIFICATE = item.CERTIFICATE
                        objProcessTrainData.CERTIFICATE_CODE = item.CODE_CERTIFICATE
                        objProcessTrainData.CONTENT_LEVEL = item.CONTENT_TRAIN
                        objProcessTrainData.IS_RENEWED = item.IS_RENEW
                        objProcessTrainData.NOTE = item.NOTE
                        objProcessTrainData.COMMITMENT_TIME = item.COMMITMENT_TIME
                        Context.HU_PRO_TRAIN_OUT_COMPANY.AddObject(objProcessTrainData)
                    End If
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function


#End Region

#Region "IPORTAL - Qúa trình công tác trước khi vào công ty"
    Public Function GetWorkingBeforeEdit(ByVal _filter As WorkingBeforeDTOEdit) As List(Of WorkingBeforeDTOEdit)
        Dim query As ObjectQuery(Of WorkingBeforeDTOEdit)
        Try
            query = (From p In Context.HU_WORKING_BEFORE_EDIT
                     From f In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_NAME).DefaultIfEmpty
                     Select New WorkingBeforeDTOEdit With {
                      .ID = p.ID,
                      .EMPLOYEE_ID = p.EMPLOYEE_ID,
                      .COMPANY_NAME = p.COMPANY_NAME,
                      .DEPARTMENT = p.DEPARTMENT,
                      .COMPANY_ADDRESS = p.COMPANY_ADDRESS,
                      .TELEPHONE = p.TELEPHONE,
                      .JOIN_DATE = p.JOIN_DATE,
                      .END_DATE = p.END_DATE,
                      .SALARY = p.SALARY,
                      .TITLE_NAME = p.TITLE_NAME,
                      .LEVEL_NAME = p.LEVEL_NAME,
                      .TER_REASON = p.TER_REASON,
                      .REASON_UNAPROVE = p.REASON_UNAPROVE,
                      .FK_PKEY = p.FK_PKEY,
                      .STATUS = p.STATUS,
                      .UPLOAD_FILE = f.NAME,
                      .FILE_NAME = f.FILE_NAME,
                     .WORK = p.WORK,
                     .THAM_NIEN = p.THAM_NIEN,
                      .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                        If(p.STATUS = 1, "Chờ phê duyệt",
                                        If(p.STATUS = 2, "Phê duyệt",
                                            If(p.STATUS = 3, "Không phê duyệt", ""))))})
            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.ID <> 0 Then
                query = query.Where(Function(p) p.ID = _filter.ID)
            End If
            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetApproveWorkingBeforeEdit(ByVal _filter As WorkingBeforeDTOEdit,
                                         ByVal PageIndex As Integer,
                                         ByVal PageSize As Integer,
                                         ByRef Total As Integer, ByVal _param As ParamDTO,
                                         Optional ByVal Sorts As String = "EMPLOYEE_ID desc",
                                         Optional ByVal log As UserLog = Nothing) As List(Of WorkingBeforeDTOEdit)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = (From p In Context.HU_WORKING_BEFORE_EDIT
                         From i In Context.HU_USERFILES.Where(Function(f) f.NAME = p.FILE_NAME).DefaultIfEmpty
                         From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                         From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And f.USERNAME = log.Username.ToUpper)
                         Where p.STATUS = 1
                         Select New WorkingBeforeDTOEdit With {
                             .ID = p.ID,
                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                             .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                             .EMPLOYEE_NAME = e.FULLNAME_VN,
                     .COMPANY_NAME = p.COMPANY_NAME,
                     .DEPARTMENT = p.DEPARTMENT,
                     .COMPANY_ADDRESS = p.COMPANY_ADDRESS,
                     .TELEPHONE = p.TELEPHONE,
                     .JOIN_DATE = p.JOIN_DATE,
                     .END_DATE = p.END_DATE,
                     .SALARY = p.SALARY,
                     .TITLE_NAME = p.TITLE_NAME,
                     .LEVEL_NAME = p.LEVEL_NAME,
                     .TER_REASON = p.TER_REASON,
                     .FILE_NAME = i.FILE_NAME,
                     .UPLOAD_FILE = i.NAME,
                     .WORK = p.WORK,
                     .FK_PKEY = p.FK_PKEY,
                          .STATUS = p.STATUS,
                             .STATUS_NAME = If(p.STATUS = 0, "Chưa gửi duyệt",
                                               If(p.STATUS = 1, "Chờ phê duyệt",
                                                  If(p.STATUS = 2, "Phê duyệt",
                                                     If(p.STATUS = 3, "Không phê duyệt", ""))))})



            If _filter.COMPANY_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.COMPANY_NAME.ToUpper.Contains(_filter.COMPANY_NAME.ToUpper))
            End If

            If _filter.DEPARTMENT IsNot Nothing Then
                query = query.Where(Function(p) p.DEPARTMENT.ToUpper.Contains(_filter.DEPARTMENT.ToUpper))
            End If

            If _filter.COMPANY_ADDRESS IsNot Nothing Then
                query = query.Where(Function(p) p.COMPANY_ADDRESS.ToUpper.Contains(_filter.COMPANY_ADDRESS.ToUpper))
            End If

            If _filter.TELEPHONE IsNot Nothing Then
                query = query.Where(Function(p) p.TELEPHONE.ToUpper.Contains(_filter.TELEPHONE.ToUpper))
            End If

            If _filter.SALARY IsNot Nothing Then
                query = query.Where(Function(p) p.SALARY = _filter.SALARY)
            End If

            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.LEVEL_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.LEVEL_NAME.ToUpper.Contains(_filter.LEVEL_NAME.ToUpper))
            End If

            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.STATUS_NAME.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            If _filter.TER_REASON IsNot Nothing Then
                query = query.Where(Function(p) p.TER_REASON.ToUpper.Contains(_filter.TER_REASON.ToUpper))
            End If

            If _filter.JOIN_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.JOIN_DATE = _filter.JOIN_DATE)
            End If

            If _filter.END_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.END_DATE = _filter.END_DATE)
            End If



            Dim working = query

            working = working.OrderBy(Sorts)
            Total = working.Count
            working = working.Skip(PageIndex * PageSize).Take(PageSize)

            Return working.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetChangedWorkingBeforeList(ByVal lstWorkingBeforeEdit As List(Of WorkingBeforeDTOEdit)) As Dictionary(Of String, String)
        Try
            Dim dic As New Dictionary(Of String, String)
            For Each WorkingBeforeEdit As WorkingBeforeDTOEdit In lstWorkingBeforeEdit
                Dim colNames As String = String.Empty
                Dim Working = Context.HU_WORKING_BEFORE.Where(Function(f) f.ID = WorkingBeforeEdit.FK_PKEY).FirstOrDefault
                If Working IsNot Nothing Then
                    If (If(WorkingBeforeEdit.COMPANY_NAME Is Nothing, "", WorkingBeforeEdit.COMPANY_NAME) <> If(Working.COMPANY_NAME Is Nothing, "", Working.COMPANY_NAME)) Then
                        colNames = "COMPANY_NAME"
                    End If
                    If (If(WorkingBeforeEdit.DEPARTMENT Is Nothing, "", WorkingBeforeEdit.DEPARTMENT) <> If(Working.DEPARTMENT Is Nothing, "", Working.DEPARTMENT)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "DEPARTMENT", "DEPARTMENT")
                    End If
                    If (If(WorkingBeforeEdit.TITLE_NAME Is Nothing, "", WorkingBeforeEdit.TITLE_NAME) <> If(Working.TITLE_NAME Is Nothing, "", Working.TITLE_NAME)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "TITLE_NAME", "TITLE_NAME")
                    End If
                    If (If(WorkingBeforeEdit.COMPANY_ADDRESS Is Nothing, "", WorkingBeforeEdit.COMPANY_ADDRESS) <> If(Working.COMPANY_ADDRESS Is Nothing, "", Working.COMPANY_ADDRESS)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "COMPANY_ADDRESS", "COMPANY_ADDRESS")
                    End If
                    If (If(WorkingBeforeEdit.JOIN_DATE Is Nothing, "", WorkingBeforeEdit.JOIN_DATE.ToString) <> If(Working.JOIN_DATE Is Nothing, "", Working.JOIN_DATE.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "JOIN_DATE", "JOIN_DATE")
                    End If
                    If (If(WorkingBeforeEdit.END_DATE Is Nothing, "", WorkingBeforeEdit.END_DATE.ToString) <> If(Working.END_DATE Is Nothing, "", Working.END_DATE.ToString)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "END_DATE", "END_DATE")
                    End If
                    If (If(WorkingBeforeEdit.TER_REASON Is Nothing, "", WorkingBeforeEdit.TER_REASON) <> If(Working.TER_REASON Is Nothing, "", Working.TER_REASON)) Then
                        colNames = If(colNames <> String.Empty, colNames + "," + "TER_REASON", "TER_REASON")
                    End If
                    dic.Add(WorkingBeforeEdit.ID.ToString, colNames)
                End If
            Next
            Return dic
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertWorkingBeforeEdit(ByVal objWorkingBefore As WorkingBeforeDTOEdit, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Try

            Dim objWorkingBeforeData As New HU_WORKING_BEFORE_EDIT
            objWorkingBeforeData.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING_BEFORE_EDIT.EntitySet.Name)
            objWorkingBeforeData.EMPLOYEE_ID = objWorkingBefore.EMPLOYEE_ID
            objWorkingBeforeData.COMPANY_ADDRESS = objWorkingBefore.COMPANY_ADDRESS
            objWorkingBeforeData.COMPANY_NAME = objWorkingBefore.COMPANY_NAME
            objWorkingBeforeData.DEPARTMENT = objWorkingBefore.DEPARTMENT
            objWorkingBeforeData.SALARY = objWorkingBefore.SALARY
            objWorkingBeforeData.TELEPHONE = objWorkingBefore.TELEPHONE
            objWorkingBeforeData.TER_REASON = objWorkingBefore.TER_REASON
            objWorkingBeforeData.LEVEL_NAME = objWorkingBefore.LEVEL_NAME
            objWorkingBeforeData.TITLE_NAME = objWorkingBefore.TITLE_NAME
            objWorkingBeforeData.JOIN_DATE = objWorkingBefore.JOIN_DATE
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.THAM_NIEN = objWorkingBefore.THAM_NIEN
            objWorkingBeforeData.CREATED_DATE = DateTime.Now
            objWorkingBeforeData.CREATED_BY = log.Username
            objWorkingBeforeData.CREATED_LOG = log.ComputerName
            objWorkingBeforeData.MODIFIED_DATE = DateTime.Now
            objWorkingBeforeData.MODIFIED_BY = log.Username
            objWorkingBeforeData.MODIFIED_LOG = log.ComputerName
            objWorkingBeforeData.STATUS = 0
            objWorkingBeforeData.REASON_UNAPROVE = objWorkingBefore.REASON_UNAPROVE
            If objWorkingBefore.FK_PKEY IsNot Nothing AndAlso objWorkingBefore.FK_PKEY <> 0 Then
                objWorkingBeforeData.FK_PKEY = objWorkingBefore.FK_PKEY
            End If
            If Not String.IsNullOrEmpty(objWorkingBefore.FILE_NAME) Then
                objWorkingBeforeData.FILE_NAME = objWorkingBefore.FILE_NAME
            End If
            objWorkingBeforeData.WORK = objWorkingBefore.WORK
            Context.HU_WORKING_BEFORE_EDIT.AddObject(objWorkingBeforeData)
            Context.SaveChanges(log)
            gID = objWorkingBeforeData.ID

            If objWorkingBefore.IS_SEND = 1 Then
                Dim lst = New List(Of Decimal)
                lst.Add(gID)
                SendWorkingBeforeEdit(lst, log)
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ModifyWorkingBeforeEdit(ByVal objWorkingBefore As WorkingBeforeDTOEdit,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objWorkingBeforeData As New HU_WORKING_BEFORE_EDIT With {.ID = objWorkingBefore.ID}
        Try
            objWorkingBeforeData = (From p In Context.HU_WORKING_BEFORE_EDIT Where p.ID = objWorkingBefore.ID).FirstOrDefault
            objWorkingBeforeData.EMPLOYEE_ID = objWorkingBefore.EMPLOYEE_ID
            objWorkingBeforeData.COMPANY_ADDRESS = objWorkingBefore.COMPANY_ADDRESS
            objWorkingBeforeData.COMPANY_NAME = objWorkingBefore.COMPANY_NAME
            objWorkingBeforeData.DEPARTMENT = objWorkingBefore.DEPARTMENT
            objWorkingBeforeData.SALARY = objWorkingBefore.SALARY
            objWorkingBeforeData.TELEPHONE = objWorkingBefore.TELEPHONE
            objWorkingBeforeData.TER_REASON = objWorkingBefore.TER_REASON
            objWorkingBeforeData.LEVEL_NAME = objWorkingBefore.LEVEL_NAME
            objWorkingBeforeData.TITLE_NAME = objWorkingBefore.TITLE_NAME
            objWorkingBeforeData.JOIN_DATE = objWorkingBefore.JOIN_DATE
            objWorkingBeforeData.END_DATE = objWorkingBefore.END_DATE
            objWorkingBeforeData.THAM_NIEN = objWorkingBefore.THAM_NIEN
            objWorkingBeforeData.STATUS = "0"
            objWorkingBeforeData.MODIFIED_DATE = DateTime.Now
            objWorkingBeforeData.MODIFIED_BY = log.Username
            objWorkingBeforeData.MODIFIED_LOG = log.ComputerName
            objWorkingBeforeData.REASON_UNAPROVE = objWorkingBefore.REASON_UNAPROVE
            If objWorkingBefore.FK_PKEY IsNot Nothing AndAlso objWorkingBefore.FK_PKEY <> 0 Then
                objWorkingBeforeData.FK_PKEY = objWorkingBefore.FK_PKEY
            End If
            If Not String.IsNullOrEmpty(objWorkingBefore.FILE_NAME) Then
                objWorkingBeforeData.FILE_NAME = objWorkingBefore.FILE_NAME
            End If
            objWorkingBeforeData.WORK = objWorkingBefore.WORK
            Context.SaveChanges(log)
            gID = objWorkingBeforeData.ID

            If objWorkingBefore.IS_SEND = 1 Then
                Dim lst = New List(Of Decimal)
                lst.Add(gID)
                SendWorkingBeforeEdit(lst, log)
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteWorkingBeforeEdit(ByVal lstDecimals As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_WORKING_BEFORE_EDIT)
        Try
            lst = (From p In Context.HU_WORKING_BEFORE_EDIT Where lstDecimals.Contains(p.ID)).ToList
            For i As Int16 = 0 To lst.Count - 1
                Context.HU_WORKING_BEFORE_EDIT.DeleteObject(lst(i))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckExistWorkingBeforeEdit(ByVal pk_key As Decimal) As WorkingBeforeDTOEdit
        Try
            Dim query = (From p In Context.HU_WORKING_BEFORE_EDIT
                         Where p.STATUS <> 2 And p.STATUS <> 3 And p.FK_PKEY = pk_key And p.FK_PKEY <> 0
                         Select New WorkingBeforeDTOEdit With {
                             .ID = p.ID,
                             .STATUS = p.STATUS}).FirstOrDefault

            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function SendWorkingBeforeEdit(ByVal lstID As List(Of Decimal),
                                           ByVal log As UserLog) As Boolean

        Try
            Dim lstObj = (From p In Context.HU_WORKING_BEFORE_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lstObj
                item.SEND_DATE = Date.Now
                item.STATUS = 1
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function UpdateStatusWorkingBeforeEdit(ByVal lstID As List(Of Decimal),
                                                   ByVal status As String,
                                                   ByVal log As UserLog) As Boolean
        Dim lst As List(Of HU_WORKING_BEFORE_EDIT)
        Dim sStatus() As String = status.Split(":")
        Dim emp_from = (From p In Context.SE_USER Where p.USERNAME = log.Username.ToUpper Select p.EMPLOYEE_ID).FirstOrDefault

        Try
            lst = (From p In Context.HU_WORKING_BEFORE_EDIT Where lstID.Contains(p.ID)).ToList
            For Each item In lst
                item.STATUS = sStatus(0)
                item.REASON_UNAPROVE = sStatus(1)


                Dim noti As New SE_NOTIFICATION
                noti.ID = Utilities.GetNextSequence(Context, Context.SE_NOTIFICATION.EntitySet.Name)
                noti.FROM_EMPLOYEE_ID = emp_from
                noti.TO_EMPLOYEE_ID = item.EMPLOYEE_ID
                noti.PROCESS_TYPE = "EMP_WORK"
                noti.SEND_STATUS = sStatus(0)
                noti.MESSAGE = sStatus(1)
                noti.SENT_DATE = System.DateTime.Now
                Context.SE_NOTIFICATION.AddObject(noti)


                If sStatus(0) = 2 Then
                    If item.FK_PKEY IsNot Nothing And item.FK_PKEY <> 0 Then
                        Dim objWorkingBeforeData As New HU_WORKING_BEFORE
                        objWorkingBeforeData = (From p In Context.HU_WORKING_BEFORE Where p.ID = item.FK_PKEY).FirstOrDefault
                        objWorkingBeforeData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objWorkingBeforeData.COMPANY_ADDRESS = item.COMPANY_ADDRESS
                        objWorkingBeforeData.COMPANY_NAME = item.COMPANY_NAME
                        objWorkingBeforeData.DEPARTMENT = item.DEPARTMENT
                        objWorkingBeforeData.END_DATE = item.END_DATE
                        objWorkingBeforeData.SALARY = item.SALARY
                        objWorkingBeforeData.TELEPHONE = item.TELEPHONE
                        objWorkingBeforeData.TER_REASON = item.TER_REASON
                        objWorkingBeforeData.LEVEL_NAME = item.LEVEL_NAME
                        objWorkingBeforeData.TITLE_NAME = item.TITLE_NAME
                        objWorkingBeforeData.JOIN_DATE = item.JOIN_DATE
                        objWorkingBeforeData.END_DATE = item.END_DATE
                        objWorkingBeforeData.THAM_NIEN = item.THAM_NIEN
                        objWorkingBeforeData.WORK = item.WORK

                        Dim oldFileName = (From p In Context.HU_USERFILES Where p.NAME = objWorkingBeforeData.FILE_NAME Select p.FILE_NAME).FirstOrDefault
                        Dim newFileName = item.FILE_NAME
                        If oldFileName <> newFileName Then
                            objWorkingBeforeData.FILE_NAME = item.FILE_NAME
                        End If
                    Else
                        Dim objWorkingBeforeData As New HU_WORKING_BEFORE
                        objWorkingBeforeData.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING_BEFORE.EntitySet.Name)

                        item.FK_PKEY = objWorkingBeforeData.ID

                        objWorkingBeforeData.EMPLOYEE_ID = item.EMPLOYEE_ID
                        objWorkingBeforeData.COMPANY_ADDRESS = item.COMPANY_ADDRESS
                        objWorkingBeforeData.COMPANY_NAME = item.COMPANY_NAME
                        objWorkingBeforeData.DEPARTMENT = item.DEPARTMENT
                        objWorkingBeforeData.END_DATE = item.END_DATE
                        objWorkingBeforeData.SALARY = item.SALARY
                        objWorkingBeforeData.TELEPHONE = item.TELEPHONE
                        objWorkingBeforeData.TER_REASON = item.TER_REASON
                        objWorkingBeforeData.LEVEL_NAME = item.LEVEL_NAME
                        objWorkingBeforeData.TITLE_NAME = item.TITLE_NAME
                        objWorkingBeforeData.JOIN_DATE = item.JOIN_DATE
                        objWorkingBeforeData.END_DATE = item.END_DATE
                        objWorkingBeforeData.THAM_NIEN = item.THAM_NIEN
                        objWorkingBeforeData.WORK = item.WORK
                        objWorkingBeforeData.FILE_NAME = item.FILE_NAME
                        Context.HU_WORKING_BEFORE.AddObject(objWorkingBeforeData)
                    End If
                End If
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function GetFileForView(ByVal fileUpload As String) As FileUploadDTO
        Dim path = AppDomain.CurrentDomain.BaseDirectory
        Dim query = From p In Context.HU_USERFILES Where p.NAME = fileUpload
                    Select New FileUploadDTO With {.LINK = path & p.LINK,
                                                   .FILE_NAME = p.FILE_NAME,
                                                   .NAME = p.NAME}

        Return query.FirstOrDefault
    End Function
    Public Function GetFileByte_Userfile(ByVal fileUpload As String) As Byte()
        Using rep As New ProfileRepository
            Dim dsFile As New Byte()
            Dim _filterEdit As New WorkingBeforeDTOEdit
            Try
                Dim fileInfor = (From p In Context.HU_USERFILES Where p.NAME = fileUpload)
                If fileInfor IsNot Nothing Then

                    Dim dsFileName = fileInfor.FirstOrDefault.FILE_NAME.Split(",")

                    Dim target As String = AppDomain.CurrentDomain.BaseDirectory & "UserfileExport\UserfileExport" & Format(Date.Now, "yyyyMMddHHmmss")

                    If Not Directory.Exists(target) Then
                        Directory.CreateDirectory(target)
                    End If

                    Dim FILENAME As String = Format(Date.Now, "yyyyMMddHHmmss") & ".zip"

                    Using zip As New ZipFile
                        For Each item In dsFileName
                            Dim rootPath = AppDomain.CurrentDomain.BaseDirectory & fileInfor.FirstOrDefault.LINK & item.ToString()

                            Dim file = New FileInfo(rootPath)
                            file.CopyTo(Path.Combine(target + "\" & item.ToString()), True)

                            zip.AddFile(target + "\" & item.ToString(), "")
                        Next
                        zip.Save(target & FILENAME)
                    End Using
                    Dim bytes = System.IO.File.ReadAllBytes(target & FILENAME)

                    File.Delete(target & FILENAME)

                    Return bytes
                End If
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Portal Xem diem, chuc nang tuong ung voi khoa hoc"
    Public Function GetPortalCompetencyCourse(ByVal _empId As Decimal) As List(Of EmployeeCriteriaRecordDTO)
        Try
            Dim query = From stand In Context.HU_COMPETENCY_STANDARD
                        From Competency In Context.HU_COMPETENCY.Where(Function(f) f.ID = stand.COMPETENCY_ID)
                        From Competencygroup In Context.HU_COMPETENCY_GROUP.Where(Function(f) f.ID = Competency.COMPETENCY_GROUP_ID)
                        From ass In Context.HU_COMPETENCY_ASS.Where(Function(f) stand.TITLE_ID = f.TITLE_ID).DefaultIfEmpty
                        From p In Context.HU_COMPETENCY_ASSDTL.Where(Function(f) f.COMPETENCY_ASS_ID = ass.ID And
                                                                      f.COMPETENCY_ID = stand.COMPETENCY_ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = ass.EMPLOYEE_ID).DefaultIfEmpty
                        From period In Context.HU_COMPETENCY_PERIOD.Where(Function(f) f.ID = ass.COMPETENCY_PERIOD_ID).DefaultIfEmpty
                        From compCourse In Context.HU_COMPETENCY_COURSE.Where(Function(f) f.COMPETENCY_ID = Competency.ID).DefaultIfEmpty
                        From Course In Context.TR_COURSE.Where(Function(f) f.ID = compCourse.TR_COURSE_ID).DefaultIfEmpty
                        Where ass.EMPLOYEE_ID = _empId
                        Select New EmployeeCriteriaRecordDTO With {
                            .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                            .EMPLOYEE_ID = ass.EMPLOYEE_ID,
                            .EMPLOYEE_NAME = e.FULLNAME_VN,
                            .TITLE_ID = stand.TITLE_ID,
                            .COMPETENCY_GROUP_ID = Competency.COMPETENCY_GROUP_ID,
                            .COMPETENCY_GROUP_NAME = Competencygroup.NAME,
                            .COMPETENCY_PERIOD_ID = period.ID,
                            .COMPETENCY_PERIOD_NAME = period.NAME,
                            .COMPETENCY_PERIOD_YEAR = period.YEAR,
                            .COMPETENCY_ID = stand.COMPETENCY_ID,
                            .COMPETENCY_NAME = Competency.NAME,
                            .LEVEL_NUMBER_STANDARD = stand.LEVEL_NUMBER,
                            .LEVEL_NUMBER_ASS = p.LEVEL_NUMBER,
                            .TR_COURSE_ID = compCourse.TR_COURSE_ID,
                            .TR_COURSE_NAME = Course.NAME,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query
            'Return lst.ToList
            Dim lstData = lst.ToList
            For Each item In lstData
                If item.LEVEL_NUMBER_STANDARD IsNot Nothing Then
                    item.LEVEL_NUMBER_STANDARD_NAME = item.LEVEL_NUMBER_STANDARD.Value.ToString & "/4"
                End If
                If item.LEVEL_NUMBER_ASS IsNot Nothing Then
                    item.LEVEL_NUMBER_ASS_NAME = item.LEVEL_NUMBER_ASS.Value.ToString & "/4"
                End If
                Dim levelStandar As Decimal = If(item.LEVEL_NUMBER_STANDARD IsNot Nothing, item.LEVEL_NUMBER_STANDARD, 0)
                Dim levelEmployee As Decimal = If(item.LEVEL_NUMBER_ASS IsNot Nothing, item.LEVEL_NUMBER_ASS, 0)
                If levelEmployee >= levelStandar Then
                    item.TR_COURSE_NAME = ""
                    item.TR_COURSE_ID = Nothing
                End If
            Next
            Return lstData
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "GetCompetencyEmployee")
            Throw ex
        End Try
    End Function



#End Region


    Public Function ModifyEmployeeHuFile(ByVal objEmployeeHuF As HuFileDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objEmployeeHuFile As New HU_FILE With {.ID = objEmployeeHuF.ID}
        Try
            objEmployeeHuFile = (From p In Context.HU_FILE Where p.ID = objEmployeeHuF.ID).FirstOrDefault
            objEmployeeHuFile.EMPLOYEE_ID = objEmployeeHuF.EMPLOYEE_ID
            objEmployeeHuFile.MODIFIED_DATE = DateTime.Now
            objEmployeeHuFile.MODIFIED_BY = log.Username
            objEmployeeHuFile.MODIFIED_LOG = log.ComputerName
            Context.SaveChanges(log)
            gID = objEmployeeHuFile.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ChangeImage(ByVal _EmpID As Decimal, ByVal _SavePath As String, ByVal _ImageName As String, ByVal _imageBinary As Byte(), ByVal log As UserLog) As Boolean
        Dim objEmpData As New HU_EMPLOYEE With {.ID = _EmpID}
        Dim objCV As New HU_EMPLOYEE_CV With {.EMPLOYEE_ID = _EmpID}
        Try
            objEmpData = (From p In Context.HU_EMPLOYEE Where p.ID = _EmpID).FirstOrDefault
            objCV = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = _EmpID).FirstOrDefault


            If _ImageName <> "" Then
                objCV.IMAGE = objEmpData.EMPLOYEE_CODE & _ImageName 'Lưu Image thành dạng E10012.jpg.                    
            End If

            If _imageBinary IsNot Nothing AndAlso _imageBinary.Length > 0 Then
                Dim savepath = ""
                For _count As Integer = 0 To 1
                    If _count = 0 Then
                        savepath = _SavePath
                    Else
                        savepath = AppDomain.CurrentDomain.BaseDirectory & "\EmployeeImage"
                    End If

                    If Not Directory.Exists(savepath) Then
                        Directory.CreateDirectory(savepath)
                    End If
                    'Xóa ảnh cũ của nhân viên.
                    Dim sFile() As String = Directory.GetFiles(savepath, objCV.IMAGE)
                    For Each s In sFile
                        File.Delete(s)
                    Next
                    Dim ms As New MemoryStream(_imageBinary)
                    ' instance a filestream pointing to the
                    ' storatge folder, use the original file name
                    ' to name the resulting file

                    Dim fs As New FileStream(savepath & "\" & objCV.IMAGE, FileMode.Create)
                    ms.WriteTo(fs)

                    ' clean up
                    ms.Close()
                    fs.Close()
                    fs.Dispose()
                Next
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetTitleNamePage(ByVal Code As String) As String
        Try
            Dim query As SE_CONFIG
            query = (From p In Context.SE_CONFIG
                     Where p.CODE.ToUpper = Code.ToUpper
                     Select p).FirstOrDefault
            Return query.VALUE
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CheckExistsEmpEdit(ByVal _EMPLOYEE_ID As Decimal) As Boolean
        Try
            'Dim count = (From p In Context.HU_EMPLOYEE_EDIT
            '             Where p.EMPLOYEE_ID = _EMPLOYEE_ID).Count()
            'Dim count1 = (From p In Context.HU_EMPLOYEE_EDIT
            '              Where p.EMPLOYEE_ID = _EMPLOYEE_ID And
            '                 (p.STATUS = 0 Or p.STATUS = 2)).Count()
            'If count <= 0 Or count1 > 0 Then
            '    Return True
            'End If

            'Return False

            Dim count = (From p In Context.HU_EMPLOYEE_EDIT
                         Where p.EMPLOYEE_ID = _EMPLOYEE_ID And p.STATUS = "1").Count
            If count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetEmpDocumentList(ByVal _emp_id As Decimal) As List(Of MngProfileSavedDTO)
        Try
            Dim query = From p In Context.HU_DOCUMENT
                        From m In Context.HU_MNGPROFILE_SAVED.Where(Function(f) f.DOCUMENT_ID = p.ID AndAlso f.EMPLOYEE_ID = _emp_id).DefaultIfEmpty
                        Where p.ACTFLG = "A"
                        Select New MngProfileSavedDTO With {.DOCUMENT_ID = p.ID,
                                                           .DOCUMENT_NAME = p.NAME_VN,
                                                           .EMPLOYEE_ID = m.EMPLOYEE_ID,
                                                           .IS_SUBMITED = m.IS_SUBMITED}

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateEmployee")
            Throw ex
        End Try

    End Function

    Public Function GetDataForPrintEmployeeCV(ByVal EmployeeID As Decimal) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_PROFILE.PRINT_CV",
                                               New With {.P_EMP_ID = EmployeeID,
                                                         .P_OUT = cls.OUT_CURSOR,
                                                         .P_OUT1 = cls.OUT_CURSOR,
                                                         .P_OUT2 = cls.OUT_CURSOR,
                                                         .P_OUT3 = cls.OUT_CURSOR,
                                                         .P_OUT4 = cls.OUT_CURSOR,
                                                         .P_OUT5 = cls.OUT_CURSOR,
                                                         .P_OUT6 = cls.OUT_CURSOR,
                                                         .P_OUT7 = cls.OUT_CURSOR,
                                                         .P_OUT8 = cls.OUT_CURSOR,
                                                         .P_OUT9 = cls.OUT_CURSOR,
                                                         .P_OUT10 = cls.OUT_CURSOR,
                                                         .P_OUT11 = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function UpdateEmployeeHoldingCode(ByVal lstEmpID As List(Of Decimal), ByVal HoldingCode As String) As Boolean
        Try
            Dim lstEmp As List(Of HU_EMPLOYEE)
            lstEmp = (From p In Context.HU_EMPLOYEE Where lstEmpID.Contains(p.ID)).ToList
            For i = 0 To lstEmp.Count - 1
                lstEmp(i).HOLDING_CODE = HoldingCode
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
End Class
