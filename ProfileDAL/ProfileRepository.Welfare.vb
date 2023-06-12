Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class ProfileRepository

#Region "WelfareMng"
    Public Function GetWelfareListAuto(ByVal _filter As WelfareMngDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer, ByVal log As UserLog) As DataTable

        Try
            Using Sql As New DataAccess.NonQueryData
                Using cls As New DataAccess.QueryData
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_WELFARE_AUTO",
                                               New With {.P_USERNAME = log.Username,
                                                         .P_ORGID = _filter.ORG_ID,
                                                         .P_ISDISSOLVE = _filter.PARAM.IS_DISSOLVE,
                                                         .P_WELFARE_ID = _filter.WELFARE_ID,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                    Return dtData
                End Using
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")

        End Try
    End Function
    Public Function GetWelfareMng(ByVal _filter As WelfareMngDTO, ByVal IsDissolve As Integer, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        ByVal UserLog As UserLog,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of WelfareMngDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = UserLog.Username,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = IsDissolve})
            End Using

            Dim query = From p In Context.HU_WELFARE_MNG
                        From ce In Context.HU_WELFARE_MNG_EMP.Where(Function(f) f.GROUP_ID = p.ID).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = ce.EMPLOYEE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(o) e.ORG_ID = o.ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.WELFARE_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(t) e.TITLE_ID = t.ID).DefaultIfEmpty
                        From se In Context.SE_CHOSEN_ORG.Where(Function(se) se.ORG_ID = o.ID And _
                                                                   se.USERNAME = UserLog.Username.ToUpper)

            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            'bo loc nhan vien nghỉ viec
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or _
                                        (p.e.WORK_STATUS.HasValue And _
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))
            End If


            ' lọc điều kiện
            If _filter.WELFARE_ID <> 0 Then
                query = query.Where(Function(p) p.p.WELFARE_ID = _filter.WELFARE_ID)
            End If

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.WELFARE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.ot.NAME_VN.ToUpper.Contains(_filter.WELFARE_NAME.ToUpper))
            End If
            If _filter.MONEY IsNot Nothing Then
                query = query.Where(Function(p) p.p.MONEY = _filter.MONEY)
            End If
            If _filter.EFFECT_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.p.EFFECT_DATE >= _filter.EFFECT_FROM)
            End If
            If _filter.EFFECT_TO IsNot Nothing Then
                query = query.Where(Function(p) p.p.EFFECT_DATE <= _filter.EFFECT_TO)
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            ' select thuộc tính
            Dim wel = query.Select(Function(p) New WelfareMngDTO With {
                                       .ID = p.p.ID,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .EFFECT_DATE = p.p.EFFECT_DATE,
                                       .EMPLOYEE_ID = p.e.ID,
                                       .IS_TAXION = p.p.IS_TAXION,
                                       .NAME_TAXION = If(p.p.IS_TAXION = 0, "Không tính vào lương", If(p.p.IS_TAXION = 1, "Tính vào lương (chịu thuế)", If(p.p.IS_TAXION = 2, "Tính vào lương (không chịu thuế)", ""))),
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                       .MONEY = p.ce.MONEY_TOTAL,
                                       .MONEY_PL = p.ce.MONEY_PL,
                                       .TOTAL_CHILD = p.ce.TOTAL_CHILD,
                                       .GENDER_NAME = p.ce.GENDER_NAME,
                                       .CONTRACT_NAME = p.ce.CONTRACT_NAME,
                                       .SENIORITY = p.ce.SENIORITY,
                                       .ORG_ID = p.o.ID,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .ORG_DESC = p.o.DESCRIPTION_PATH,
                                       .SDESC = p.p.SDESC,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .WELFARE_ID = p.p.WELFARE_ID,
                                       .WELFARE_NAME = p.ot.NAME_VN,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .TER_LAST_DATE = p.e.TER_EFFECT_DATE,
                                       .CREATED_DATE = p.p.CREATED_DATE})
            wel = wel.OrderBy(Sorts)
            Total = wel.Count
            wel = wel.Skip(PageIndex * PageSize).Take(PageSize)
            Return wel.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetlistWelfareEMP(ByVal Id As Integer) As List(Of Welfatemng_empDTO)
        Try
            Dim query = From p In Context.HU_WELFARE_MNG
                        From ce In Context.HU_WELFARE_MNG_EMP.Where(Function(f) f.GROUP_ID = p.ID).DefaultIfEmpty
                        From cttype In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = ce.CONTRACT_TYPE).DefaultIfEmpty
                        From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = ce.EMPLOYEE_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID).DefaultIfEmpty
                        From gender In Context.OT_OTHER_LIST.Where(Function(f) f.ID = ce.GENDER_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = ce.ORG_ID).DefaultIfEmpty
                        From ov In Context.HU_ORGANIZATION_V.Where(Function(f) f.ID = o.ID).DefaultIfEmpty
                        From w In Context.HU_WELFARE_LIST.Where(Function(w) w.ID = p.WELFARE_ID).DefaultIfEmpty
                        From pe In Context.AT_PERIOD.Where(Function(pe) pe.ID = p.PERIOD_ID).DefaultIfEmpty
                        From e_cv In Context.HU_EMPLOYEE_CV.Where(Function(e_cv) e_cv.EMPLOYEE_ID = e.ID).DefaultIfEmpty
                        Where p.ID = Id
                        Order By p.EMPLOYEE_ID()

            Dim lst = query.Select(Function(p) New Welfatemng_empDTO With {
                                       .ID = p.ce.ID,
                                      .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                      .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                      .TITLE_NAME = p.t.NAME_VN,
                                      .ORG_NAME = p.o.NAME_VN,
                                      .ORG_ID = p.ce.ORG_ID,
                                       .ORG_NAME2 = p.ov.NAME_C2,
                                      .TITLE_ID = p.ce.TITLE_ID,
                                      .TOTAL_CHILD = p.ce.TOTAL_CHILD,
                                      .MONEY_TOTAL = p.ce.MONEY_TOTAL,
                                      .MONEY_PL = p.ce.MONEY_PL,
                                      .GENDER_ID = p.ce.GENDER_ID,
                                      .GENDER_NAME = p.gender.NAME_VN,
                                      .CONTRACT_TYPE = p.ce.CONTRACT_TYPE,
                                      .CONTRACT_NAME = p.cttype.NAME,
                                      .SENIORITY = p.ce.SENIORITY,
                                      .EMPLOYEE_ID = p.ce.EMPLOYEE_ID,
                                      .GROUP_ID = p.ce.GROUP_ID,
                                      .REMARK = p.ce.REMARK,
                                       .BIRTH_DATE = p.e_cv.BIRTH_DATE,
                                       .WELFARE_ID = p.ce.WELFARE_ID
                                      })
            Return lst.ToList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetWelfareMngById(ByVal Id As Integer
                                        ) As WelfareMngDTO
        Try
            Dim query = From p In Context.HU_WELFARE_MNG
                        From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = p.EMPLOYEE_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(o) o.ID = e.ORG_ID)
                        From w In Context.HU_WELFARE_LIST.Where(Function(w) w.ID = p.WELFARE_ID).DefaultIfEmpty
                        From pe In Context.AT_PERIOD.Where(Function(pe) pe.ID = p.PERIOD_ID).DefaultIfEmpty
                        Where p.ID = Id
                        Order By p.EMPLOYEE_ID

            ' select thuộc tính
            Dim wel = query.Select(Function(p) New WelfareMngDTO With {
                                       .ID = p.p.ID,
                                       .EFFECT_DATE = p.p.EFFECT_DATE,
                                       .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                       .EMPLOYEE_ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .WELFARE_ID = p.p.WELFARE_ID,
                                       .WELFARE_NAME = p.w.NAME,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .YEAR_PERIOD = p.pe.YEAR,
                                       .MONEY = p.p.MONEY,
                                       .WORK_STATUS = p.e.WORK_STATUS,
                                       .SDESC = p.p.SDESC,
            .IS_TAXION = p.p.IS_TAXION
                                       })
            Return wel.SingleOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CheckWelfareMngEffect(ByVal _filter As List(Of WelfareMngDTO)) As Boolean
        Dim lst As List(Of WelfareMngDTO)
        Try
            If _filter(0).ID <> 0 Then
                Dim lstID As List(Of Decimal) = (From p In _filter Select p.ID).ToList
                Dim lstWelID As List(Of Decimal) = (From p In _filter Select p.WELFARE_ID).ToList
                Dim lstEmpID As List(Of Decimal) = (From p In _filter Select p.EMPLOYEE_ID).ToList
                ' lấy max phúc lợi theo ID phúc lợi + ID quản lý phúc lợi + ID nhân viên
                Dim welfare = (From p In Context.HU_WELFARE_MNG
                               Where lstWelID.Contains(p.WELFARE_ID) _
                               And Not lstID.Contains(p.ID) _
                               And lstEmpID.Contains(p.EMPLOYEE_ID)
                               Group p By p.WELFARE_ID, p.EMPLOYEE_ID Into pGroup = Group
                               Select New With {
                                  Key .WELFARE_ID = WELFARE_ID,
                                  Key .EMPLOYEE_ID = EMPLOYEE_ID,
                                  Key .EFFECT_DATE = pGroup.Max(Function(p) p.EFFECT_DATE)})
                'Logger.LogInfo(welfare)
                ' danh sách thông tin quản lý phúc lợi mới nhất
                Dim query = (From p In Context.HU_WELFARE_MNG
                             Join u In welfare
                             On p.WELFARE_ID Equals u.WELFARE_ID _
                             And p.EMPLOYEE_ID Equals u.EMPLOYEE_ID _
                             And p.EFFECT_DATE Equals u.EFFECT_DATE
                             Where Not lstID.Contains(p.ID)
                             Select New WelfareMngDTO With {
                                 .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                 .EFFECT_DATE = p.EFFECT_DATE,
                                 .EXPIRE_DATE = p.EXPIRE_DATE})
                ''Logger.LogError(ex)
                lst = query.ToList
            Else
                Dim lstWelID As List(Of Decimal) = (From p In _filter Select p.WELFARE_ID).ToList
                Dim lstEmpID As List(Of Decimal) = (From p In _filter Select p.EMPLOYEE_ID).ToList
                ' lấy max phúc lợi theo ID phúc lợi + ID nhân viên
                Dim welfare = (From p In Context.HU_WELFARE_MNG
                               Where lstWelID.Contains(p.WELFARE_ID) _
                               And lstEmpID.Contains(p.EMPLOYEE_ID)
                               Group p By p.WELFARE_ID, p.EMPLOYEE_ID Into pGroup = Group
                               Select New With {
                                  Key .WELFARE_ID = WELFARE_ID,
                                  Key .EMPLOYEE_ID = EMPLOYEE_ID,
                                  Key .EFFECT_DATE = pGroup.Max(Function(p) p.EFFECT_DATE)})
                ' danh sách thông tin quản lý phúc lợi mới nhất
                Dim query = (From p In Context.HU_WELFARE_MNG
                             Join u In welfare
                             On p.WELFARE_ID Equals u.WELFARE_ID _
                             And p.EMPLOYEE_ID Equals u.EMPLOYEE_ID _
                             And p.EFFECT_DATE Equals u.EFFECT_DATE
                             Select New WelfareMngDTO With {
                                 .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                 .EFFECT_DATE = p.EFFECT_DATE,
                                 .EXPIRE_DATE = p.EXPIRE_DATE})
                lst = query.ToList
            End If
            If lst.Count > 0 Then
                ' Check đồng loạt ngày hiệu lực của nhân viên
                For idx = 0 To lst.Count - 1
                    For idx1 = 0 To _filter.Count - 1
                        If lst(idx).EMPLOYEE_ID = _filter(idx1).EMPLOYEE_ID Then
                            If lst(idx).EXPIRE_DATE IsNot Nothing Then
                                If Format(_filter(idx1).EFFECT_DATE, "yyyyMMdd") <= Format(lst(idx).EXPIRE_DATE, "yyyyMMdd") Then
                                    Return False
                                End If
                            Else
                                If Format(_filter(idx1).EFFECT_DATE, "yyyyMMdd") <= Format(lst(idx).EFFECT_DATE, "yyyyMMdd") Then
                                    Return False
                                End If
                            End If
                            Exit For
                        End If
                    Next
                Next
                Return True
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertWelfareMng(ByVal lstWelfareMng As WelfareMngDTO,
                                   ByVal log As UserLog) As Boolean
        Dim iCount As Integer = 0
        Try
            Dim objWelfareMngData As New HU_WELFARE_MNG
            objWelfareMngData.ID = Utilities.GetNextSequence(Context, Context.HU_WELFARE_MNG.EntitySet.Name)
            lstWelfareMng.ID = objWelfareMngData.ID
            objWelfareMngData.EFFECT_DATE = lstWelfareMng.EFFECT_DATE
            objWelfareMngData.EMPLOYEE_ID = lstWelfareMng.EMPLOYEE_ID
            objWelfareMngData.SDESC = lstWelfareMng.SDESC
            objWelfareMngData.WELFARE_ID = lstWelfareMng.WELFARE_ID
            objWelfareMngData.CREATED_DATE = DateTime.Now
            objWelfareMngData.CREATED_BY = log.Username
            objWelfareMngData.CREATED_LOG = log.ComputerName
            objWelfareMngData.MODIFIED_DATE = DateTime.Now
            objWelfareMngData.MODIFIED_BY = log.Username
            objWelfareMngData.MODIFIED_LOG = log.ComputerName
            Context.HU_WELFARE_MNG.AddObject(objWelfareMngData)

            InsertObjectLstEmp(lstWelfareMng)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Private Sub InsertObjectLstEmp(ByVal lstWelfareMng As WelfareMngDTO)
        Dim objDataEmp As HU_WELFARE_MNG_EMP
        Try
            If lstWelfareMng.LST_WELFATE_EMP IsNot Nothing Then
                Dim objD = (From d In Context.HU_WELFARE_MNG_EMP Where d.GROUP_ID = lstWelfareMng.ID).ToList
                For Each obj As HU_WELFARE_MNG_EMP In objD
                    Context.HU_WELFARE_MNG_EMP.DeleteObject(obj)
                Next
                'insert nhan vien mới
                For Each obj As Welfatemng_empDTO In lstWelfareMng.LST_WELFATE_EMP
                    objDataEmp = New HU_WELFARE_MNG_EMP
                    objDataEmp.ID = Utilities.GetNextSequence(Context, Context.HU_WELFARE_MNG_EMP.EntitySet.Name)
                    objDataEmp.GROUP_ID = lstWelfareMng.ID
                    objDataEmp.EMPLOYEE_ID = obj.EMPLOYEE_ID
                    objDataEmp.TITLE_ID = obj.TITLE_ID
                    objDataEmp.ORG_ID = obj.ORG_ID
                    objDataEmp.TOTAL_CHILD = obj.TOTAL_CHILD
                    objDataEmp.MONEY_TOTAL = obj.MONEY_TOTAL
                    objDataEmp.MONEY_PL = obj.MONEY_PL
                    objDataEmp.CONTRACT_TYPE = obj.CONTRACT_TYPE
                    objDataEmp.GENDER_ID = obj.GENDER_ID
                    objDataEmp.GENDER_NAME = obj.GENDER_NAME
                    objDataEmp.CONTRACT_NAME = obj.CONTRACT_NAME
                    objDataEmp.SENIORITY = obj.SENIORITY
                    objDataEmp.WELFARE_ID = obj.WELFARE_ID
                    objDataEmp.REMARK = obj.REMARK
                    Context.HU_WELFARE_MNG_EMP.AddObject(objDataEmp)
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function ModifyWelfareMng(ByVal lstWelfareMng As WelfareMngDTO,
                                   ByVal log As UserLog) As Boolean
        'Dim lstID As List(Of Decimal) = (From p In lstWelfareMng Select p.ID).ToList
        Dim lstWelfareMngData As New HU_WELFARE_MNG With {.ID = lstWelfareMng.ID}
        Try
            'lstWelfareMngData = (From p In Context.HU_WELFARE_MNG Where lstID.Contains(p.ID)).ToList
            'lstWelfareMngData(idx).WELFARE_ID = lstWelfareMng(idx).WELFARE_ID
            'lstWelfareMngData(idx).EFFECT_DATE = lstWelfareMng(idx).EFFECT_DATE
            'lstWelfareMngData(idx).EXPIRE_DATE = lstWelfareMng(idx).EXPIRE_DATE
            'lstWelfareMngData(idx).MONEY = lstWelfareMng(idx).MONEY
            'lstWelfareMngData(idx).SDESC = lstWelfareMng(idx).SDESC
            'lstWelfareMngData(idx).IS_TAXION = lstWelfareMng(idx).IS_TAXION
            'lstWelfareMngData(idx).WELFARE_ID = lstWelfareMng(idx).WELFARE_ID
            'lstWelfareMngData(idx).PERIOD_ID = lstWelfareMng(idx).PERIOD_ID
            'lstWelfareMngData(idx).MODIFIED_DATE = DateTime.Now
            'lstWelfareMngData(idx).MODIFIED_BY = log.Username
            'lstWelfareMngData(idx).MODIFIED_LOG = log.ComputerName
            Context.HU_WELFARE_MNG.Attach(lstWelfareMngData)
            lstWelfareMngData.ID = lstWelfareMng.ID
            lstWelfareMngData.EFFECT_DATE = lstWelfareMng.EFFECT_DATE
            lstWelfareMngData.WELFARE_ID = lstWelfareMng.WELFARE_ID
            lstWelfareMngData.SDESC = lstWelfareMng.SDESC
            lstWelfareMngData.EMPLOYEE_ID = lstWelfareMng.EMPLOYEE_ID
            lstWelfareMngData.MODIFIED_DATE = DateTime.Now
            lstWelfareMngData.MODIFIED_BY = log.Username
            lstWelfareMngData.MODIFIED_LOG = log.ComputerName
            InsertObjectLstEmp(lstWelfareMng)
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteWelfareMng(ByVal lstWelfareMng() As WelfareMngDTO,
                                   ByVal log As UserLog) As Boolean
        Dim lstWelfareMngData As List(Of HU_WELFARE_MNG)
        Dim lstWelfareEmpData As List(Of HU_WELFARE_MNG_EMP)
        Dim lstIDWelfareMng As List(Of Decimal) = (From p In lstWelfareMng.ToList Select p.ID).ToList

        lstWelfareMngData = (From p In Context.HU_WELFARE_MNG Where lstIDWelfareMng.Contains(p.ID)).ToList
        For index = 0 To lstWelfareMngData.Count - 1
            Context.HU_WELFARE_MNG.DeleteObject(lstWelfareMngData(index))
        Next
        lstWelfareEmpData = (From p In Context.HU_WELFARE_MNG_EMP Where lstIDWelfareMng.Contains(p.GROUP_ID)).ToList
        For index = 0 To lstWelfareEmpData.Count - 1
            Context.HU_WELFARE_MNG_EMP.DeleteObject(lstWelfareEmpData(index))
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function GET_DETAILS_EMP(ByVal P_ID As Decimal, ByVal P_WELFARE_ID As Decimal, ByVal P_DATE As Date, ByVal log As UserLog) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.GET_DETAILS_EMP",
                                           New With {.P_EMP_ID = P_ID,
                                                     .P_WELFARE_ID = P_WELFARE_ID,
                                                     .P_DATE = P_DATE,
                                                     .P_USER_NAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GET_EXPORT_EMP(ByVal P_WELFARE_ID As Decimal, ByVal P_DATE As Date, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.GET_EXPORT_EMP",
                                           New With {.P_WELFARE_ID = P_WELFARE_ID,
                                                     .P_DATE = P_DATE,
                                                     .P_USER_NAME = log.Username,
                                                     .P_CUR = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GET_INFO_EMPLOYEE(ByVal P_EMP_CODE As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_LIST.GET_INFO_EMPLOYEE",
                                           New With {.P_EMP_CODE = P_EMP_CODE,
                                                     .P_CUR = cls.OUT_CURSOR})
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
#End Region

End Class
