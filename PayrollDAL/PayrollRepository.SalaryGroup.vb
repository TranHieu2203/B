Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Public Class PayrollRepository

#Region "SalaryGroup list"

    Public Function GetSalaryGroup(ByVal _filter As SalaryGroupDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryGroupDTO)

        Try
            Dim query = From p In Context.PA_SALARY_GROUP
                        Where p.IS_DELETED = 0

            If _filter.CODE <> "" Then
                query = query.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If

            If _filter.NAME <> "" Then
                query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If

            If _filter.EFFECT_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If _filter.IS_COEFFICIENT IsNot Nothing Then
                query = query.Where(Function(p) p.IS_COEFFICIENT = _filter.IS_COEFFICIENT)
            End If

            Dim lst = query.Select(Function(p) New SalaryGroupDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .IS_INCENTIVE = p.IS_INCENTIVE,
                                       .IS_COEFFICIENT = p.IS_COEFFICIENT,
                                       .ORDERS = p.ORDERS,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function GetEffectSalaryGroup() As SalaryGroupDTO
        Try
            Dim query = From p In Context.PA_SALARY_GROUP
                        Where p.EFFECT_DATE <= Date.Now And p.ACTFLG.ToUpper = "A" And p.IS_DELETED = 0
                        Order By p.EFFECT_DATE Descending, p.ORDERS, p.CREATED_DATE Descending

            Dim EffectSalaryGroup = query.Select(Function(p) New SalaryGroupDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .IS_INCENTIVE = p.IS_INCENTIVE,
                                       .IS_COEFFICIENT = p.IS_COEFFICIENT,
                                       .ORDERS = p.ORDERS,
                                       .CREATED_DATE = p.CREATED_DATE}).FirstOrDefault

            Return EffectSalaryGroup
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    ''' <summary>
    ''' Lay data vao combo cho bang luong
    ''' </summary>
    ''' <param name="dateValue">Ma bang luong</param>
    ''' <param name="isBlank">0: Khong lay dong empty; 1: Co lay dong empty</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSalaryGroupCombo(ByVal dateValue As Date, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_GROUP",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_DATE = dateValue,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Insert data cho Salary Group
    ''' </summary>
    ''' <param name="objSalaryGroup"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertSalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objSalaryGroupData As New PA_SALARY_GROUP
        Try
            objSalaryGroupData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_GROUP.EntitySet.Name)
            objSalaryGroupData.CODE = objSalaryGroup.CODE.Trim
            objSalaryGroupData.NAME = objSalaryGroup.NAME.Trim
            objSalaryGroupData.EFFECT_DATE = objSalaryGroup.EFFECT_DATE
            objSalaryGroupData.REMARK = objSalaryGroup.REMARK
            objSalaryGroupData.IS_INCENTIVE = objSalaryGroup.IS_INCENTIVE
            objSalaryGroupData.IS_COEFFICIENT = objSalaryGroup.IS_COEFFICIENT
            objSalaryGroupData.ORDERS = objSalaryGroup.ORDERS
            objSalaryGroupData.ACTFLG = objSalaryGroup.ACTFLG
            objSalaryGroupData.IS_DELETED = objSalaryGroup.IS_DELETED
            Context.PA_SALARY_GROUP.AddObject(objSalaryGroupData)
            Context.SaveChanges(log)


            Context.SaveChanges(log)
            gID = objSalaryGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateSalaryGroup(ByVal _validate As SalaryGroupDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.IS_DELETED = 0 _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.PA_SALARY_GROUP
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.IS_DELETED = 0).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_GROUP
                             Where p.ACTFLG.ToUpper = "A" _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifySalaryGroup(ByVal objSalaryGroup As SalaryGroupDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryGroupData As New PA_SALARY_GROUP With {.ID = objSalaryGroup.ID}
        Try
            Context.PA_SALARY_GROUP.Attach(objSalaryGroupData)
            objSalaryGroupData.CODE = objSalaryGroup.CODE.Trim
            objSalaryGroupData.NAME = objSalaryGroup.NAME.Trim
            objSalaryGroupData.EFFECT_DATE = objSalaryGroup.EFFECT_DATE
            objSalaryGroupData.REMARK = objSalaryGroup.REMARK
            objSalaryGroupData.IS_INCENTIVE = objSalaryGroup.IS_INCENTIVE
            objSalaryGroupData.IS_COEFFICIENT = objSalaryGroup.IS_COEFFICIENT
            objSalaryGroupData.ORDERS = objSalaryGroup.ORDERS

            Context.SaveChanges(log)
            gID = objSalaryGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveSalaryGroup(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstSalaryGroupData As List(Of PA_SALARY_GROUP)
        Try
            lstSalaryGroupData = (From p In Context.PA_SALARY_GROUP Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSalaryGroupData.Count - 1
                For Each item In lstSalaryGroupData(idx).PA_SALARY_LEVEL
                    For Each item1 In item.PA_SALARY_RANK
                        item1.ACTFLG = bActive
                    Next
                    item.ACTFLG = bActive
                Next
                lstSalaryGroupData(idx).ACTFLG = bActive
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSalaryGroup(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstSalaryGroupData As List(Of PA_SALARY_GROUP)
        Try
            lstSalaryGroupData = (From p In Context.PA_SALARY_GROUP Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSalaryGroupData.Count - 1
                For Each item In lstSalaryGroupData(idx).PA_SALARY_LEVEL
                    For Each item1 In item.PA_SALARY_RANK
                        Context.PA_SALARY_RANK.DeleteObject(item1)
                    Next
                    Context.PA_SALARY_LEVEL.DeleteObject(item)
                Next

                Context.PA_SALARY_GROUP.DeleteObject(lstSalaryGroupData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

#End Region

#Region "Don Vi Quy Luong"
    Public Function GetDonViQuyLuong(ByVal _filter As DonViQuyLuongDTO, ByVal PageIndex As Integer,
                                       ByVal PageSize As Integer,
                                       ByRef Total As Integer,
                                       Optional ByVal Sorts As String = "NAME ASC") As List(Of DonViQuyLuongDTO)

        Try
            Dim query = From p In Context.PA_DONVI_QUYLUONG
                        Where p.IS_DELETED = 0

            If _filter.CODE <> "" Then
                query = query.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If

            If _filter.NAME <> "" Then
                query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If

            If _filter.REMARK <> "" Then
                query = query.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            Dim lst = query.Select(Function(p) New DonViQuyLuongDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function InsertDonViQuyLuong(ByVal objSalaryGroup As DonViQuyLuongDTO,
                               ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objSalaryGroupData As New PA_DONVI_QUYLUONG
        Try
            objSalaryGroupData.ID = Utilities.GetNextSequence(Context, Context.PA_DONVI_QUYLUONG.EntitySet.Name)
            objSalaryGroupData.CODE = objSalaryGroup.CODE.Trim
            objSalaryGroupData.NAME = objSalaryGroup.NAME.Trim
            objSalaryGroupData.REMARK = objSalaryGroup.REMARK
            objSalaryGroupData.ACTFLG = objSalaryGroup.ACTFLG
            objSalaryGroupData.IS_DELETED = objSalaryGroup.IS_DELETED
            Context.PA_DONVI_QUYLUONG.AddObject(objSalaryGroupData)
            Context.SaveChanges(log)
            gID = objSalaryGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function


    Public Function ModifyDonViQuyLuong(ByVal objSalaryGroup As DonViQuyLuongDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryGroupData As New PA_DONVI_QUYLUONG With {.ID = objSalaryGroup.ID}
        Try
            Context.PA_DONVI_QUYLUONG.Attach(objSalaryGroupData)
            objSalaryGroupData.CODE = objSalaryGroup.CODE.Trim
            objSalaryGroupData.NAME = objSalaryGroup.NAME.Trim
            objSalaryGroupData.REMARK = objSalaryGroup.REMARK

            Context.SaveChanges(log)
            gID = objSalaryGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveDonViQuyLuong(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstSalaryGroupData As List(Of PA_DONVI_QUYLUONG)
        Try
            lstSalaryGroupData = (From p In Context.PA_DONVI_QUYLUONG Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSalaryGroupData.Count - 1
                lstSalaryGroupData(idx).ACTFLG = bActive
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteDonViQuyLuong(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstSalaryGroupData As List(Of PA_DONVI_QUYLUONG)
        Try
            lstSalaryGroupData = (From p In Context.PA_DONVI_QUYLUONG Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSalaryGroupData.Count - 1
                Context.PA_DONVI_QUYLUONG.DeleteObject(lstSalaryGroupData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateDonViQuyLuong(ByVal _validate As DonViQuyLuongDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_DONVI_QUYLUONG
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.IS_DELETED = 0 _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.PA_DONVI_QUYLUONG
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.IS_DELETED = 0).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_DONVI_QUYLUONG
                             Where p.ACTFLG.ToUpper = "A" _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetSalaryQuyLuong(ByVal _filter As SalaryQuyLuongDTO, ByVal PageIndex As Integer,
                                     ByVal PageSize As Integer,
                                     ByRef Total As Integer,
                                     Optional ByVal Sorts As String = "CREATED_DATE DESC") As List(Of SalaryQuyLuongDTO)

        Try
            Dim query = From p In Context.PA_SALARY_QUYLUONG
                        From a In Context.AT_PERIOD.Where(Function(e) e.ID = p.PERIOD_ID)
                        From d In Context.PA_DONVI_QUYLUONG.Where(Function(e) e.ID = p.DONVI_QUYLUONG_ID)

            If _filter.PERIOD_NAME <> "" Then
                query = query.Where(Function(p) p.a.PERIOD_T.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If

            If _filter.CODE <> "" Then
                query = query.Where(Function(p) p.d.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If

            If _filter.DONVI_QUYLUONG_NAME <> "" Then
                query = query.Where(Function(p) p.d.NAME.ToUpper.Contains(_filter.DONVI_QUYLUONG_NAME.ToUpper))
            End If

            If _filter.SALARY IsNot Nothing Then
                query = query.Where(Function(p) p.p.SALARY = _filter.SALARY)
            End If

            If _filter.PERIOD_SEARCH_ID IsNot Nothing Then
                query = query.Where(Function(p) p.p.PERIOD_ID = _filter.PERIOD_SEARCH_ID)
            End If

            If _filter.REMARK <> "" Then
                query = query.Where(Function(p) p.p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If

            Dim lst = query.Select(Function(p) New SalaryQuyLuongDTO With {
                                       .ID = p.p.ID,
                                       .PERIOD_ID = p.p.PERIOD_ID,
                                       .PERIOD_NAME = p.a.PERIOD_T,
                                       .SALARY = p.p.SALARY,
                                       .DONVI_QUYLUONG_ID = p.p.DONVI_QUYLUONG_ID,
                                       .DONVI_QUYLUONG_NAME = p.d.NAME,
                                       .REMARK = p.p.REMARK,
                                       .CODE = p.d.CODE,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .YEAR = p.a.YEAR})


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function InsertSalaryQuyLuong(ByVal objSalaryGroup As SalaryQuyLuongDTO,
                               ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objSalaryGroupData As New PA_SALARY_QUYLUONG
        Try
            objSalaryGroupData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_QUYLUONG.EntitySet.Name)
            objSalaryGroupData.PERIOD_ID = objSalaryGroup.PERIOD_ID
            objSalaryGroupData.DONVI_QUYLUONG_ID = objSalaryGroup.DONVI_QUYLUONG_ID
            objSalaryGroupData.SALARY = objSalaryGroup.SALARY
            objSalaryGroupData.REMARK = objSalaryGroup.REMARK
            objSalaryGroupData.IS_DELETED = objSalaryGroup.IS_DELETED
            Context.PA_SALARY_QUYLUONG.AddObject(objSalaryGroupData)
            Context.SaveChanges(log)
            gID = objSalaryGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function


    Public Function ModifySalaryQuyLuong(ByVal objSalaryGroup As SalaryQuyLuongDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryGroupData As New PA_SALARY_QUYLUONG With {.ID = objSalaryGroup.ID}
        Try
            Context.PA_SALARY_QUYLUONG.Attach(objSalaryGroupData)
            objSalaryGroupData.PERIOD_ID = objSalaryGroup.PERIOD_ID
            objSalaryGroupData.DONVI_QUYLUONG_ID = objSalaryGroup.DONVI_QUYLUONG_ID
            objSalaryGroupData.SALARY = objSalaryGroup.SALARY
            objSalaryGroupData.REMARK = objSalaryGroup.REMARK

            Context.SaveChanges(log)
            gID = objSalaryGroupData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeleteSalaryQuyLuong(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstSalaryGroupData As List(Of PA_SALARY_QUYLUONG)
        Try
            lstSalaryGroupData = (From p In Context.PA_SALARY_QUYLUONG Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSalaryGroupData.Count - 1
                Context.PA_SALARY_QUYLUONG.DeleteObject(lstSalaryGroupData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function LOAD_DONVI_QUYLUONG() As List(Of DonViQuyLuongDTO)
        Try
            Dim query = From p In Context.PA_DONVI_QUYLUONG Where p.ACTFLG = "A"

            Dim lst = query.Select(Function(p) New DonViQuyLuongDTO With {
                                       .ID = p.ID,
                                       .NAME = p.NAME})

            lst = lst.OrderBy("NAME ASC")

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex
        End Try
    End Function

    Public Function GET_IMPORT_QUYLUONG() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PA_SETTING.GET_IMPORT_QUYLUONG",
                                                         New With {.P_CUR = cls.OUT_CURSOR,
                                                                   .P_CUR1 = cls.OUT_CURSOR,
                                                                   .P_CUR2 = cls.OUT_CURSOR}, False)
                Return dsdata
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IMPORT_QUYLUONG(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PA_SETTING.IMPORT_QUYLUONG",
                                                         New With {.P_DOCXML = P_DOCXML,
                                                                   .P_USER = P_USER,
                                                                   .P_CUR = cls.OUT_CURSOR}, False)
                If dsdata.Tables(0).Rows(0)("RESULT") = "1" Then
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            Return False
            Throw ex
        End Try
    End Function

    Public Function GetEmpNotQuyLuong(ByVal _filter As EmpQuyLuongDTO, ByVal _param As PA_ParamDTO, ByVal log As UserLog, ByVal PageIndex As Integer,
                                 ByVal PageSize As Integer,
                                 ByRef Total As Integer,
                                 Optional ByVal Sorts As String = "CREATED_DATE DESC") As List(Of EmpQuyLuongDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_EMPLOYEE
                        From a In Context.PA_EMPLOYEE_QUYLUONG.Where(Function(e) e.EMPLOYEE_ID = p.ID And e.PERIOD_ID = _filter.PERIOD_ID And _filter.DONVI_QUYLUONG_ID = e.DONVI_QUYLUONG_ID).DefaultIfEmpty
                        From o In Context.HU_ORGANIZATION.Where(Function(e) e.ID = p.ORG_ID)
                        From t In Context.HU_TITLE.Where(Function(e) e.ID = p.TITLE_ID)
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)
                        Where p.JOIN_DATE IsNot Nothing And a.EMPLOYEE_ID Is Nothing Order By p.EMPLOYEE_CODE Ascending

            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.p.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            Dim lst = query.Select(Function(p) New EmpQuyLuongDTO With {
                                       .ID = p.p.ID,
                                       .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.p.FULLNAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN
                                       })


            'lst = lst.OrderBy(Sorts)
            Total = lst.Count
            'lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function GetEmpQuyLuong(ByVal _filter As EmpQuyLuongDTO, ByVal _param As PA_ParamDTO, ByVal log As UserLog, ByVal PageIndex As Integer,
                                  ByVal PageSize As Integer,
                                  ByRef Total As Integer,
                                  Optional ByVal Sorts As String = "CREATED_DATE DESC") As List(Of EmpQuyLuongDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = From p In Context.HU_EMPLOYEE
                        From a In Context.PA_EMPLOYEE_QUYLUONG.Where(Function(e) e.EMPLOYEE_ID = p.ID And e.PERIOD_ID = _filter.PERIOD_ID And e.DONVI_QUYLUONG_ID = _filter.DONVI_QUYLUONG_ID)
                        From ot In Context.PA_DONVI_QUYLUONG.Where(Function(e) e.ID = a.DONVI_QUYLUONG_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(e) e.ID = p.ORG_ID)
                        From t In Context.HU_TITLE.Where(Function(e) e.ID = p.TITLE_ID)
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)
                        Where p.JOIN_DATE IsNot Nothing
                        Order By p.EMPLOYEE_CODE Ascending

            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.p.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(p) p.t.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.DONVI_QUYLUONG_NAME <> "" Then
                query = query.Where(Function(p) p.ot.NAME.ToUpper.Contains(_filter.DONVI_QUYLUONG_NAME.ToUpper))
            End If


            Dim lst = query.Select(Function(p) New EmpQuyLuongDTO With {
                                       .ID = p.a.ID,
                                       .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                                       .EMPLOYEE_NAME = p.p.FULLNAME_VN,
                                       .ORG_NAME = p.o.NAME_VN,
                                       .TITLE_NAME = p.t.NAME_VN,
                                       .DONVI_QUYLUONG_ID = p.a.DONVI_QUYLUONG_ID,
                                       .DONVI_QUYLUONG_NAME = p.ot.NAME
                                       })


            'lst = lst.OrderBy(Sorts)
            Total = lst.Count
            'lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function


    Public Function InsertEmpQuyLuong(ByVal obj As EmpQuyLuongDTO, ByVal lstID As List(Of Decimal),
                           ByVal log As UserLog) As Boolean
        Try
            For Each item In lstID
                Dim objData As New PA_EMPLOYEE_QUYLUONG
                objData.ID = Utilities.GetNextSequence(Context, Context.PA_EMPLOYEE_QUYLUONG.EntitySet.Name)
                objData.DONVI_QUYLUONG_ID = obj.DONVI_QUYLUONG_ID
                objData.PERIOD_ID = obj.PERIOD_ID
                objData.EMPLOYEE_ID = item
                Context.PA_EMPLOYEE_QUYLUONG.AddObject(objData)
            Next

            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeleteEmpQuyLuong(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstSalaryGroupData As List(Of PA_EMPLOYEE_QUYLUONG)
        Try
            lstSalaryGroupData = (From p In Context.PA_EMPLOYEE_QUYLUONG Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSalaryGroupData.Count - 1
                Context.PA_EMPLOYEE_QUYLUONG.DeleteObject(lstSalaryGroupData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function


#End Region

End Class

