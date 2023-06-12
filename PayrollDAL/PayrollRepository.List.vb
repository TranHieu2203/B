Imports System.Data.Objects
Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Public Class PayrollRepository

#Region "Allowance_list "
    Public Function GetAllowanceList(ByVal _filter As AllowanceListDTO,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of AllowanceListDTO)

        Try
            Dim query = From p In Context.HU_ALLOWANCE_LIST
                        From s In Context.OT_OTHER_LIST.Where(Function(e) e.ID = p.ALLOWANCE_TYPE).DefaultIfEmpty()
            'Join s In Context.OT_OTHER_LIST On p.ALLOWANCE_TYPE Equals (s.ID)

            Dim lst = query.Select(Function(e) New AllowanceListDTO With {
                                       .ID = e.p.ID,
                                       .CODE = e.p.CODE,
                                       .NAME = e.p.NAME,
                                       .REMARK = e.p.REMARK,
                                       .ACTFLG = e.p.ACTFLG,
                                       .CREATED_DATE = e.p.CREATED_DATE,
                                       .ALLOWANCE_TYPE = e.p.ALLOWANCE_TYPE,
                                       .ORDERS = e.p.ORDERS,
                                       .IS_CONTRACT = e.p.IS_CONTRACT,
                                       .IS_INSURANCE = e.p.IS_INSURANCE,
                                       .IS_PAY = e.p.IS_PAY,
                                       .ALLOWANCE_TYPE_NAME = e.s.NAME_VN
                                   })

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If
            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If _filter.ALLOWANCE_TYPE <> 0 Then
                lst = lst.Where(Function(p) p.ALLOWANCE_TYPE.Equals(_filter.ALLOWANCE_TYPE))
            End If

            lst = lst.OrderBy(Sorts)


            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function GetAllowance(ByVal _filter As AllowanceDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " CREATED_DATE desc") As List(Of AllowanceDTO)
        Try
            Dim lst = (From p In Context.HU_ALLOWANCE
                       From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = p.EMPLOYEE_ID).DefaultIfEmpty()
                       From o In Context.HU_ALLOWANCE_LIST.Where(Function(o) o.ID = p.ALLOWANCE_TYPE).DefaultIfEmpty()
                       Select New AllowanceDTO With {
                                                    .ID = p.ID,
                                                    .ALLOWANCE_TYPE = p.ALLOWANCE_TYPE,
                                                    .ALLOWANCE_TYPE_NAME = o.NAME,
                                                    .AMOUNT = p.AMOUNT,
                                                    .EFFECT_DATE = p.EFFECT_DATE,
                                                    .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                    .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                    .FULLNAME_VN = e.FULLNAME_VN,
                                                    .EXP_DATE = p.EXP_DATE,
                                                    .CREATED_DATE = p.CREATED_DATE,
                                                    .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                    .REMARK = p.REMARK
                                                })

            If _filter.EMPLOYEE_ID <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_ID.ToUpper.Contains(_filter.EMPLOYEE_ID.ToUpper))
            End If
            If _filter.ALLOWANCE_TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.ALLOWANCE_TYPE_NAME.ToUpper.Contains(_filter.ALLOWANCE_TYPE_NAME.ToUpper))
            End If
            If _filter.ALLOWANCE_TYPE <> 0 Then
                lst = lst.Where(Function(p) p.ALLOWANCE_TYPE = _filter.ALLOWANCE_TYPE)
            End If

            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.AMOUNT <> 0 Then
                lst = lst.Where(Function(p) p.AMOUNT = _filter.AMOUNT)
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

    Public Function InsertAllowance(ByVal objTitle As AllowanceDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_ALLOWANCE
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.HU_ALLOWANCE.EntitySet.Name)
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.ALLOWANCE_TYPE = objTitle.ALLOWANCE_TYPE
            objTitleData.AMOUNT = objTitle.AMOUNT
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXP_DATE = objTitle.EXP_DATE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.REMARK = objTitle.REMARK

            Context.HU_ALLOWANCE.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyAllowance(ByVal objTitle As AllowanceDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New HU_ALLOWANCE With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.HU_ALLOWANCE Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.EMPLOYEE_ID = objTitle.EMPLOYEE_ID
            objTitleData.ALLOWANCE_TYPE = objTitle.ALLOWANCE_TYPE
            objTitleData.AMOUNT = objTitle.AMOUNT
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXP_DATE = objTitle.EXP_DATE
            objTitleData.ACTFLG = "A"
            objTitleData.REMARK = objTitle.REMARK
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveAllowance(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of HU_ALLOWANCE)
        Try
            lstData = (From p In Context.HU_ALLOWANCE Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteAllowance(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstAllowanceData As List(Of HU_ALLOWANCE)
        Try
            lstAllowanceData = (From p In Context.HU_ALLOWANCE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstAllowanceData.Count - 1
                Context.HU_ALLOWANCE.DeleteObject(lstAllowanceData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTaxation")
            Throw ex
        End Try
    End Function


#End Region

#Region "Taxation "
    Public Function GetTaxation(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS ASC") As List(Of PATaxationDTO)
        Try
            Dim query = From p In Context.PA_TAXATION
                        From o In Context.OT_OTHER_LIST.Where(Function(e) e.ID = p.RESIDENT_ID).DefaultIfEmpty()
            Dim lst = query.Select(Function(s) New PATaxationDTO With {
                                        .ID = s.p.ID,
                                        .RESIDENT_ID = s.p.RESIDENT_ID,
                                        .RESIDENT_NAMEVN = s.o.NAME_VN,
                                        .RESIDENT_NAMEEN = s.o.NAME_EN,
                                        .VALUE_FROM = s.p.VALUE_FROM,
                                        .VALUE_TO = s.p.VALUE_TO,
                                        .RATE = s.p.RATE,
                                        .EXCEPT_FAST = s.p.EXCEPT_FAST,
                                        .FROM_DATE = s.p.FROM_DATE,
                                        .TO_DATE = s.p.TO_DATE,
                                        .SDESC = s.p.SDESC,
                                        .ACTFLG = If(s.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                        .CREATED_DATE = s.p.CREATED_DATE,
                                        .ORDERS = s.p.ORDERS
                                    })
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertTaxation(ByVal objTitle As PATaxationDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_TAXATION
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_TAXATION.EntitySet.Name)
            objTitleData.RESIDENT_ID = objTitle.RESIDENT_ID
            objTitleData.VALUE_FROM = objTitle.VALUE_FROM
            objTitleData.VALUE_TO = objTitle.VALUE_TO
            objTitleData.RATE = objTitle.RATE
            objTitleData.EXCEPT_FAST = objTitle.EXCEPT_FAST
            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.ACTFLG = "A"
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            objTitleData.ORDERS = objTitle.ORDERS
            Context.PA_TAXATION.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyTaxation(ByVal objTitle As PATaxationDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_TAXATION With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.PA_TAXATION Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.RESIDENT_ID = objTitle.RESIDENT_ID
            objTitleData.VALUE_FROM = objTitle.VALUE_FROM
            objTitleData.VALUE_TO = objTitle.VALUE_TO
            objTitleData.RATE = objTitle.RATE
            objTitleData.EXCEPT_FAST = objTitle.EXCEPT_FAST
            objTitleData.FROM_DATE = objTitle.FROM_DATE
            objTitleData.TO_DATE = objTitle.TO_DATE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.ORDERS = objTitle.ORDERS
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveTaxation(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_TAXATION)
        Try
            lstData = (From p In Context.PA_TAXATION Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteTaxation(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstTaxationData As List(Of PA_TAXATION)
        Try
            lstTaxationData = (From p In Context.PA_TAXATION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTaxationData.Count - 1
                Context.PA_TAXATION.DeleteObject(lstTaxationData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteTaxation")
            Throw ex
        End Try
    End Function

#End Region

#Region "Payment list"
    Public Function GetPaymentList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        Try
            Dim query = From p In Context.PA_PAYMENT_LIST
                        From o In Context.PA_OBJECT_SALARY.Where(Function(e) e.ID = p.OBJ_PAYMENT_ID).DefaultIfEmpty()
            Dim lst = query.Select(Function(s) New PAPaymentListDTO With {
                                        .ID = s.p.ID,
                                        .CODE = s.p.CODE,
                                        .NAME = s.p.NAME,
                                        .OBJ_PAYMENT_ID = s.p.OBJ_PAYMENT_ID,
                                        .OBJ_PAYMENT_NAME_VN = s.o.NAME_EN,
                                        .OBJ_PAYMENT_NAME_EN = s.o.NAME_VN,
                                        .EFFECTIVE_DATE = s.p.EFFECTIVE_DATE,
                                        .VALUE = s.p.VALUE,
                                        .SDESC = s.p.SDESC,
                                        .ACTFLG = If(s.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                        .CREATED_DATE = s.p.CREATED_DATE,
                                        .ORDERS = s.p.ORDERS
                                     })
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetPaymentListAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAPaymentListDTO)
        Try

            Dim query = From p In Context.PA_PAYMENT_LIST
            Dim lst = query.Select(Function(p) New PAPaymentListDTO With {
                                        .ID = p.ID,
                                        .OBJ_PAYMENT_ID = p.OBJ_PAYMENT_ID,
                                        .CODE = p.CODE,
                                        .NAME = p.NAME,
                                        .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                        .VALUE = p.VALUE,
                                        .SDESC = p.SDESC,
                                        .ACTFLG = p.ACTFLG,
                                        .ORDERS = p.ORDERS
                                     })
            lst = lst.OrderBy(Sorts)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function InsertPaymentList(ByVal objTitle As PAPaymentListDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_PAYMENT_LIST
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_PAYMENT_LIST.EntitySet.Name)
            objTitleData.OBJ_PAYMENT_ID = objTitle.OBJ_PAYMENT_ID
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME = objTitle.NAME
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.VALUE = objTitle.VALUE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            objTitleData.ORDERS = objTitle.ORDERS
            Context.PA_PAYMENT_LIST.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyPaymentList(ByVal objTitle As PAPaymentListDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_PAYMENT_LIST With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.PA_PAYMENT_LIST Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.NAME = objTitle.NAME
            objTitleData.OBJ_PAYMENT_ID = objTitle.OBJ_PAYMENT_ID
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.VALUE = objTitle.VALUE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            objTitleData.ORDERS = objTitle.ORDERS
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActivePaymentList(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_PAYMENT_LIST)
        Try
            lstData = (From p In Context.PA_PAYMENT_LIST Where lstID.Contains(p.ID)).ToList
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

    Public Function ActiveSystemCriteria(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_SYSTEM_CRITERIA)
        Try
            lstData = (From p In Context.PA_SYSTEM_CRITERIA Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).STATUS = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeletePaymentList(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstPaymentListData As List(Of PA_PAYMENT_LIST)
        Try
            lstPaymentListData = (From p In Context.PA_PAYMENT_LIST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstPaymentListData.Count - 1
                Context.PA_PAYMENT_LIST.DeleteObject(lstPaymentListData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

#End Region

#Region "Object Salary"
    Public Function GetObjectSalary(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        Try

            Dim query = From p In Context.PA_OBJECT_SALARY
                        From o In Context.OT_OTHER_LIST.Where(Function(e) e.ID = p.GROUP_SAL).DefaultIfEmpty()

            Dim lst = query.Select(Function(p) New PAObjectSalaryDTO With {
                                        .ID = p.p.ID,
                                        .CODE = p.p.CODE,
                                        .NAME_VN = p.p.NAME_VN,
                                        .NAME_EN = p.p.NAME_EN,
                                        .EFFECTIVE_DATE = p.p.EFFECTIVE_DATE,
                                        .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                        .SDESC = p.p.SDESC,
                                        .CREATED_BY = p.p.CREATED_BY,
                                        .CREATED_DATE = p.p.CREATED_DATE,
                                        .CREATED_LOG = p.p.CREATED_LOG,
                                        .MODIFIED_BY = p.p.MODIFIED_BY,
                                        .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                        .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                        .GROUP_SAL = p.p.GROUP_SAL,
                                        .GROUP_SAL_NAME = p.o.NAME_VN
                                        })
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetObjectSalaryColumn(ByVal gID As Decimal) As DataTable
        Try
            Dim query = From p In Context.PA_OBJECT_SALARY
                        Where p.ACTFLG = "A" Order By p.NAME_VN Ascending
            Dim obj = query.Select(Function(f) New PAObjectSalaryDTO With
                        {.ID = f.ID,
                         .NAME_VN = f.NAME_VN,
                         .NAME_EN = f.NAME_EN
                        }).ToList
            Return obj.ToTable()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetObjectSalaryAll(Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PAObjectSalaryDTO)
        Try

            Dim query = From p In Context.PA_OBJECT_SALARY
            Dim lst = query.Select(Function(p) New PAObjectSalaryDTO With {
                                        .ID = p.ID,
                                        .CODE = p.CODE,
                                        .NAME_VN = p.NAME_VN,
                                        .NAME_EN = p.NAME_EN,
                                        .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                        .SDESC = p.SDESC,
                                        .CREATED_BY = p.CREATED_BY,
                                        .CREATED_DATE = p.CREATED_DATE,
                                        .CREATED_LOG = p.CREATED_LOG,
                                        .MODIFIED_BY = p.MODIFIED_BY,
                                        .MODIFIED_DATE = p.MODIFIED_DATE,
                                        .MODIFIED_LOG = p.MODIFIED_LOG
                                        })
            lst = lst.OrderBy(Sorts)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function InsertObjectSalary(ByVal objTitle As PAObjectSalaryDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_OBJECT_SALARY
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_OBJECT_SALARY.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            objTitleData.GROUP_SAL = objTitle.GROUP_SAL
            Context.PA_OBJECT_SALARY.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateObjectSalary(ByVal _validate As PAObjectSalaryDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_OBJECT_SALARY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_OBJECT_SALARY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyObjectSalary(ByVal objTitle As PAObjectSalaryDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_OBJECT_SALARY With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.PA_OBJECT_SALARY Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.SDESC = objTitle.SDESC
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            objTitleData.GROUP_SAL = objTitle.GROUP_SAL
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveObjectSalary(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_OBJECT_SALARY)
        Try
            lstData = (From p In Context.PA_OBJECT_SALARY Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteObjectSalary(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstObjectSalaryData As List(Of PA_OBJECT_SALARY)
        Try
            lstObjectSalaryData = (From p In Context.PA_OBJECT_SALARY Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstObjectSalaryData.Count - 1
                Context.PA_OBJECT_SALARY.DeleteObject(lstObjectSalaryData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

#End Region

#Region "Period list"

    Public Function GetPeriodList(ByVal _filter As ATPeriodDTO,
                                  ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "YEAR DESC,MONTH ASC") As List(Of ATPeriodDTO)

        Try
            Dim query = From p In Context.AT_PERIOD

            Dim lst = query.Select(Function(p) New ATPeriodDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .MONTH = p.MONTH,
                                       .PERIOD_NAME = p.PERIOD_NAME,
                                       .PERIOD_STANDARD = p.PERIOD_STANDARD,
                                       .PERIOD_STANDARD1 = p.PERIOD_STANDARD,
                                       .START_DATE = p.START_DATE,
                                       .END_DATE = p.END_DATE,
                                       .BONUS_DATE = p.BONUS_DATE,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .DATE_LOCK = p.DATE_LOCK,
                                       .DATE_NOTI = p.DATE_NOTI,
                                       .DATE_END_NOTI = p.DATE_END_NOTI
                                       })


            If _filter.PERIOD_NAME <> "" Then
                lst = lst.Where(Function(p) p.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If IsNumeric(_filter.YEAR) Then
                lst = lst.Where(Function(p) p.YEAR = _filter.YEAR)
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

    Public Function GetPeriodbyYear(ByVal year As Decimal) As List(Of ATPeriodDTO)
        Try
            Dim query = From p In Context.AT_PERIOD Where p.YEAR = year Order By p.MONTH Ascending
            Dim Period = query.Select(Function(p) New ATPeriodDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .MONTH = p.MONTH,
                                       .PERIOD_NAME = p.PERIOD_NAME,
                                       .START_DATE = p.START_DATE,
                                       .END_DATE = p.END_DATE,
                                       .BONUS_DATE = p.BONUS_DATE,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY,
                                       .REMARK = p.REMARK,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")})


            Return Period.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objPeriodData As New AT_PERIOD

        Try
            objPeriodData.ID = Utilities.GetNextSequence(Context, Context.AT_PERIOD.EntitySet.Name)
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.MONTH = objPeriod.MONTH
            objPeriodData.PERIOD_NAME = objPeriod.PERIOD_NAME
            objPeriodData.PERIOD_STANDARD = objPeriod.PERIOD_STANDARD
            objPeriodData.START_DATE = objPeriod.START_DATE
            objPeriodData.END_DATE = objPeriod.END_DATE
            objPeriodData.BONUS_DATE = objPeriod.BONUS_DATE
            objPeriodData.REMARK = objPeriod.REMARK
            objPeriodData.ACTFLG = objPeriod.ACTFLG
            objPeriodData.PERIOD_T = objPeriod.PERIOD_NAME & "/" & objPeriod.YEAR
            objPeriodData.DATE_LOCK = objPeriod.DATE_LOCK
            objPeriodData.DATE_NOTI = objPeriod.DATE_NOTI
            objPeriodData.DATE_END_NOTI = objPeriod.DATE_END_NOTI
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.ADD_ORG_PERIOD",
                                           New With {.P_PERIOD_ID = objPeriodData.ID,
                                                     .P_STATUSCOLEX = 1,
                                                     .P_STATUSPAROX = 1,
                                                     .P_CREATED_BY = log.Username,
                                                     .P_CREATED_DATE = Date.Now,
                                                     .P_CREATED_LOG = log.Ip & "-" & log.ComputerName
                                               })
            End Using
            Context.AT_PERIOD.AddObject(objPeriodData)
            Context.SaveChanges(log)
            'If objPeriodData.ID > 0 Then
            '    For Each obj As AT_ORG_PERIOD In objOrgPeriod
            '        objOrgPeriodData = New AT_ORG_PERIOD
            '        objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.AT_ORG_PERIOD.EntitySet.Name)
            '        objOrgPeriodData.ORG_ID = obj.ORG_ID
            '        objOrgPeriodData.PERIOD_ID = obj.ID
            '        objOrgPeriodData.STATUSCOLEX = 1
            '        objOrgPeriodData.STATUSPAROX = 1
            '        Context.AT_ORG_PERIOD.AddObject(objOrgPeriodData)
            '        Context.SaveChanges(log)
            '    Next
            'End If
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateATPeriod(ByVal _validate As ATPeriodDTO) As Boolean
        Try
            If _validate.ID <> 0 Then

                Dim query = (From p In Context.AT_ORG_PERIOD Where p.PERIOD_ID = _validate.ID).ToList
                If query.Count > 0 Then
                    Return False
                Else
                    Return True
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateATPeriodDay(ByVal _validate As ATPeriodDTO)
        Dim query
        Try
            If _validate.ID <> 0 Then
                query = (From p In Context.AT_PERIOD
                         Where p.ID <> _validate.ID _
                             And p.YEAR = _validate.YEAR _
                             And p.MONTH = _validate.MONTH).FirstOrDefault
            Else


                query = (From p In Context.AT_PERIOD
                         Where p.YEAR = _validate.YEAR _
                             And p.MONTH = _validate.MONTH).FirstOrDefault
            End If
            Return (query Is Nothing)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function checkDup(ByVal obj As NormDTO)
        Dim query
        Try
            If obj.ID <> 0 Then
                query = (From p In Context.PA_STANDARD_SETUP
                         Where p.ID <> obj.ID _
                             And (p.ORG_ID = obj.ORG_ID _
                              Or p.EMPLOYEE_ID = obj.EMPLOYEE_ID) _
                             And p.NORM_ID = obj.NORM_ID _
                             And p.EFFECT_DATE = obj.EFFECT_DATE).FirstOrDefault
            Else
                query = (From p In Context.PA_STANDARD_SETUP
                         Where (p.ORG_ID = obj.ORG_ID _
                              Or p.EMPLOYEE_ID = obj.EMPLOYEE_ID) _
                             And p.NORM_ID = obj.NORM_ID _
                             And p.EFFECT_DATE = obj.EFFECT_DATE).FirstOrDefault
            End If
            Return (query Is Nothing)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyPeriod(ByVal objPeriod As ATPeriodDTO, ByVal objOrgPeriod As List(Of AT_ORG_PERIOD), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New AT_PERIOD With {.ID = objPeriod.ID}
        'Dim objOrgPeriodData As AT_ORG_PERIOD
        Try
            Context.AT_PERIOD.Attach(objPeriodData)
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.MONTH = objPeriod.MONTH
            objPeriodData.PERIOD_STANDARD = objPeriod.PERIOD_STANDARD
            objPeriodData.PERIOD_NAME = objPeriod.PERIOD_NAME
            objPeriodData.START_DATE = objPeriod.START_DATE
            objPeriodData.END_DATE = objPeriod.END_DATE
            objPeriodData.BONUS_DATE = objPeriod.BONUS_DATE
            objPeriodData.REMARK = objPeriod.REMARK
            objPeriodData.PERIOD_T = objPeriod.PERIOD_NAME & "/" & objPeriod.YEAR
            objPeriodData.DATE_LOCK = objPeriod.DATE_LOCK
            objPeriodData.DATE_NOTI = objPeriod.DATE_NOTI
            objPeriodData.DATE_END_NOTI = objPeriod.DATE_END_NOTI
            'objPeriodData.ACTFLG = objPeriod.ACTFLG
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.ADD_ORG_PERIOD",
                                           New With {.P_PERIOD_ID = objPeriodData.ID,
                                                     .P_STATUSCOLEX = 1,
                                                     .P_STATUSPAROX = 1,
                                                     .P_CREATED_BY = log.Username,
                                                     .P_CREATED_DATE = Date.Now,
                                                     .P_CREATED_LOG = log.Ip & "-" & log.ComputerName
                                               })
            End Using
            'If objPeriodData.ID > 0 Then
            '    Dim objDelete As List(Of AT_ORG_PERIOD) = (From p In Context.AT_ORG_PERIOD Where p.PERIOD_ID = objPeriodData.ID).ToList
            '    For Each obj As AT_ORG_PERIOD In objDelete
            '        Context.AT_ORG_PERIOD.DeleteObject(obj)
            '    Next
            '    Context.SaveChanges(log)
            '    Dim i = 1
            '    For Each ObjIns As AT_ORG_PERIOD In objOrgPeriod
            '        objOrgPeriodData = New AT_ORG_PERIOD
            '        objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.AT_ORG_PERIOD.EntitySet.Name)
            '        objOrgPeriodData.ORG_ID = ObjIns.ORG_ID
            '        objOrgPeriodData.STATUSCOLEX = 1
            '        objOrgPeriodData.STATUSPAROX = 1
            '        objOrgPeriodData.PERIOD_ID = objPeriodData.ID
            '        Context.AT_ORG_PERIOD.AddObject(objOrgPeriodData)
            '        If i = objOrgPeriod.Count OrElse i Mod 40 = 0 Then
            '            Context.SaveChanges(log)
            '        End If
            '        i += 1
            '    Next
            'End If
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeletePeriod(ByVal lstPeriod As ATPeriodDTO) As Boolean
        Dim objOrgPeriod As List(Of AT_ORG_PERIOD) = (From p In Context.AT_ORG_PERIOD Where p.PERIOD_ID = lstPeriod.ID).ToList
        Dim objPeriod As List(Of AT_PERIOD) = (From p In Context.AT_PERIOD Where p.ID = lstPeriod.ID).ToList
        Try
            For Each item In objOrgPeriod
                Context.AT_ORG_PERIOD.DeleteObject(item)
            Next
            For Each item In objPeriod
                Context.AT_PERIOD.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function ActivePeriod(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of AT_PERIOD)
        Try
            lstData = (From p In Context.AT_PERIOD Where lstID.Contains(p.ID)).ToList
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

#End Region

#Region "SET UP COMPLETION KPI SHOP MANAGER"

    Public Function GetSetupCompletion_KPI_ShopManager(ByVal _filter As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO)

        Try

            Dim lst = (From p In Context.PA_SETUP_COMPLETION_KPI_SHOPMANAGER
                       From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND).DefaultIfEmpty
                       Select New PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO With {
                            .ID = p.ID,
                            .BRAND = p.BRAND,
                            .BRAND_NAME = o.NAME_VN,
                            .EFFECT_DATE = p.EFFECT_DATE,
                            .CREATED_BY = p.CREATED_BY,
                            .CREATED_DATE = p.CREATED_DATE,
                            .CREATED_LOG = p.CREATED_LOG,
                            .FACTOR = p.FACTOR,
                            .FROM_COMPLETION_RATE = p.FROM_COMPLETION_RATE,
                            .MODIFIED_BY = p.MODIFIED_BY,
                            .MODIFIED_DATE = p.MODIFIED_DATE,
                            .MODIFIED_LOG = p.MODIFIED_LOG,
                            .NOTE = p.NOTE,
                            .TO_COMPLETION_RATE = p.TO_COMPLETION_RATE})

            If _filter.BRAND IsNot Nothing Then
                lst = lst.Where(Function(f) f.BRAND = _filter.BRAND)
            End If
            If _filter.BRAND_NAME <> "" Then
                lst = lst.Where(Function(f) f.BRAND_NAME.ToUpper.Contains(_filter.BRAND_NAME.ToUpper))
            End If
            If _filter.FACTOR IsNot Nothing Then
                lst = lst.Where(Function(f) f.FACTOR = _filter.FACTOR)
            End If
            If _filter.FROM_COMPLETION_RATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROM_COMPLETION_RATE = _filter.FROM_COMPLETION_RATE)
            End If
            If _filter.TO_COMPLETION_RATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.TO_COMPLETION_RATE = _filter.TO_COMPLETION_RATE)
            End If
            If _filter.NOTE <> "" Then
                lst = lst.Where(Function(f) f.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
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

    Public Function GetSetupRate(ByVal _filter As PA_SETUP_RATE_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_RATE_DTO)

        Try

            Dim lst = (From p In Context.PA_SETUP_RATE
                       From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND).DefaultIfEmpty
                       Select New PA_SETUP_RATE_DTO With {
                            .ID = p.ID,
                            .BRAND = p.BRAND,
                            .BRAND_NAME = o.NAME_VN,
                            .EFFECT_DATE = p.EFFECT_DATE,
                            .CREATED_BY = p.CREATED_BY,
                            .CREATED_DATE = p.CREATED_DATE,
                            .CREATED_LOG = p.CREATED_LOG,
                            .COM_RATE = p.COMPANY_RATE,
                            .MODIFIED_BY = p.MODIFIED_BY,
                            .MODIFIED_DATE = p.MODIFIED_DATE,
                            .MODIFIED_LOG = p.MODIFIED_LOG,
                            .NOTE = p.NOTE,
                            .BRAND_RATE = p.BRAND_RATE})

            If _filter.BRAND IsNot Nothing Then
                lst = lst.Where(Function(f) f.BRAND = _filter.BRAND)
            End If
            If _filter.BRAND_NAME <> "" Then
                lst = lst.Where(Function(f) f.BRAND_NAME.ToUpper.Contains(_filter.BRAND_NAME.ToUpper))
            End If
            If _filter.COM_RATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.COM_RATE = _filter.COM_RATE)
            End If
            If _filter.BRAND_RATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.BRAND_RATE = _filter.BRAND_RATE)
            End If
            If _filter.NOTE <> "" Then
                lst = lst.Where(Function(f) f.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
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

    Public Function CheckBrandAndShopType(ByVal brandID As Integer, ByVal shopID As Integer) As Boolean
        Try
            Dim brand = (From p In Context.OT_OTHER_LIST Where p.ID = brandID).FirstOrDefault
            Dim shop = (From p In Context.OT_OTHER_LIST Where p.ID = shopID).FirstOrDefault
            If brand.CODE = "BnH" AndAlso shop.CODE = "KDAILY" Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetSetupBonusKpiProductType(ByVal _filter As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO)

        Try

            Dim lst = (From p In Context.PA_SETUP_BONUS_KPI_PRODUCTTYPE
                       From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND).DefaultIfEmpty
                       From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_SHOP).DefaultIfEmpty
                       From o2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.COMPLETION_LEVEL).DefaultIfEmpty
                       Select New PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO With {
                            .ID = p.ID,
                            .BRAND = p.BRAND,
                            .BRAND_NAME = o.NAME_VN,
                            .EFFECT_DATE = p.EFFECT_DATE,
                            .CREATED_BY = p.CREATED_BY,
                            .CREATED_DATE = p.CREATED_DATE,
                            .CREATED_LOG = p.CREATED_LOG,
                            .MODIFIED_BY = p.MODIFIED_BY,
                            .MODIFIED_DATE = p.MODIFIED_DATE,
                            .MODIFIED_LOG = p.MODIFIED_LOG,
                            .NOTE = p.NOTE,
                            .FROM_RATE = p.FROM_RATE,
                            .TO_RATE = p.TO_RATE,
                            .NG = p.BONUS_RATE_NG,
                            .NG1 = p.BONUS_RATE_NG1,
                            .NG2 = p.BONUS_RATE_NG2,
                            .TYPE_SHOP = p.TYPE_SHOP,
                            .TYPE_SHOP_NAME = o1.NAME_VN,
                            .COMPLETE_LV = p.COMPLETION_LEVEL,
                            .COMPLETE_LV_NAME = o2.NAME_VN,
                            .FROM_DT_TD = p.FROM_DT_TD,
                            .TO_DT_TD = p.TO_DT_TD,
                            .TLT_DT = p.TLT_DT})

            If _filter.BRAND IsNot Nothing Then
                lst = lst.Where(Function(f) f.BRAND = _filter.BRAND)
            End If
            If _filter.BRAND_NAME <> "" Then
                lst = lst.Where(Function(f) f.BRAND_NAME.ToUpper.Contains(_filter.BRAND_NAME.ToUpper))
            End If
            If _filter.COMPLETE_LV IsNot Nothing Then
                lst = lst.Where(Function(f) f.COMPLETE_LV = _filter.COMPLETE_LV)
            End If
            If _filter.COMPLETE_LV_NAME <> "" Then
                lst = lst.Where(Function(f) f.COMPLETE_LV_NAME.ToUpper.Contains(_filter.COMPLETE_LV_NAME.ToUpper))
            End If
            If _filter.TYPE_SHOP IsNot Nothing Then
                lst = lst.Where(Function(f) f.TYPE_SHOP = _filter.TYPE_SHOP)
            End If
            If _filter.TYPE_SHOP_NAME <> "" Then
                lst = lst.Where(Function(f) f.TYPE_SHOP_NAME.ToUpper.Contains(_filter.TYPE_SHOP_NAME.ToUpper))
            End If
            If _filter.FROM_RATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROM_RATE = _filter.FROM_RATE)
            End If
            If _filter.TO_RATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.TO_RATE = _filter.TO_RATE)
            End If
            If _filter.FROM_DT_TD IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROM_DT_TD = _filter.FROM_DT_TD)
            End If
            If _filter.TO_DT_TD IsNot Nothing Then
                lst = lst.Where(Function(f) f.TO_DT_TD = _filter.TO_DT_TD)
            End If
            If _filter.TLT_DT IsNot Nothing Then
                lst = lst.Where(Function(f) f.TLT_DT = _filter.TLT_DT)
            End If
            If _filter.NG IsNot Nothing Then
                lst = lst.Where(Function(f) f.NG = _filter.NG)
            End If
            If _filter.NG1 IsNot Nothing Then
                lst = lst.Where(Function(f) f.NG1 = _filter.NG1)
            End If
            If _filter.NG2 IsNot Nothing Then
                lst = lst.Where(Function(f) f.NG2 = _filter.NG2)
            End If
            If _filter.NOTE <> "" Then
                lst = lst.Where(Function(f) f.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
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

    Public Function InsertSetupCompletion_KPI_ShopManager(ByVal obj As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objData As New PA_SETUP_COMPLETION_KPI_SHOPMANAGER
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_COMPLETION_KPI_SHOPMANAGER.EntitySet.Name)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.FACTOR = obj.FACTOR
            objData.FROM_COMPLETION_RATE = obj.FROM_COMPLETION_RATE
            objData.TO_COMPLETION_RATE = obj.TO_COMPLETION_RATE
            objData.NOTE = obj.NOTE

            Context.PA_SETUP_COMPLETION_KPI_SHOPMANAGER.AddObject(objData)
            Context.SaveChanges(log)

            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifySetupCompletion_KPI_ShopManager(ByVal obj As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_SETUP_COMPLETION_KPI_SHOPMANAGER With {.ID = obj.ID}
        Try
            Context.PA_SETUP_COMPLETION_KPI_SHOPMANAGER.Attach(objData)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.FACTOR = obj.FACTOR
            objData.FROM_COMPLETION_RATE = obj.FROM_COMPLETION_RATE
            objData.TO_COMPLETION_RATE = obj.TO_COMPLETION_RATE
            objData.NOTE = obj.NOTE

            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertSetupRate(ByVal obj As PA_SETUP_RATE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objData As New PA_SETUP_RATE
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_RATE.EntitySet.Name)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.COMPANY_RATE = obj.COM_RATE
            objData.BRAND_RATE = obj.BRAND_RATE
            objData.NOTE = obj.NOTE

            Context.PA_SETUP_RATE.AddObject(objData)
            Context.SaveChanges(log)

            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifySetupRate(ByVal obj As PA_SETUP_RATE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_SETUP_RATE With {.ID = obj.ID}
        Try
            Context.PA_SETUP_RATE.Attach(objData)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.COMPANY_RATE = obj.COM_RATE
            objData.BRAND_RATE = obj.BRAND_RATE
            objData.NOTE = obj.NOTE

            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertSetupBonusKpiProductType(ByVal obj As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objData As New PA_SETUP_BONUS_KPI_PRODUCTTYPE
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_BONUS_KPI_PRODUCTTYPE.EntitySet.Name)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.TYPE_SHOP = obj.TYPE_SHOP
            objData.COMPLETION_LEVEL = obj.COMPLETE_LV
            objData.FROM_RATE = obj.FROM_RATE
            objData.TO_RATE = obj.TO_RATE
            objData.BONUS_RATE_NG = obj.NG
            objData.BONUS_RATE_NG1 = obj.NG1
            objData.BONUS_RATE_NG2 = obj.NG2
            objData.NOTE = obj.NOTE
            objData.FROM_DT_TD = obj.FROM_DT_TD
            objData.TO_DT_TD = obj.TO_DT_TD
            objData.TLT_DT = obj.TLT_DT

            Context.PA_SETUP_BONUS_KPI_PRODUCTTYPE.AddObject(objData)
            Context.SaveChanges(log)

            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifySetupBonusKpiProductType(ByVal obj As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_SETUP_BONUS_KPI_PRODUCTTYPE With {.ID = obj.ID}
        Try
            Context.PA_SETUP_BONUS_KPI_PRODUCTTYPE.Attach(objData)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.TYPE_SHOP = obj.TYPE_SHOP
            objData.COMPLETION_LEVEL = obj.COMPLETE_LV
            objData.FROM_RATE = obj.FROM_RATE
            objData.TO_RATE = obj.TO_RATE
            objData.BONUS_RATE_NG = obj.NG
            objData.BONUS_RATE_NG1 = obj.NG1
            objData.BONUS_RATE_NG2 = obj.NG2
            objData.NOTE = obj.NOTE
            objData.FROM_DT_TD = obj.FROM_DT_TD
            objData.TO_DT_TD = obj.TO_DT_TD
            objData.TLT_DT = obj.TLT_DT
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateSetupCompletion_KPI_ShopManager(ByVal _validate As PA_SETUP_COMPLETION_KPI_SHOPMANAGERDTO) As Boolean
        Try
            Dim query = (From p In Context.PA_SETUP_COMPLETION_KPI_SHOPMANAGER Where p.BRAND = _validate.BRAND _
                         AndAlso EntityFunctions.TruncateTime(p.EFFECT_DATE) = EntityFunctions.TruncateTime(_validate.EFFECT_DATE) _
                         AndAlso p.FROM_COMPLETION_RATE = _validate.FROM_COMPLETION_RATE AndAlso p.TO_COMPLETION_RATE = _validate.TO_COMPLETION_RATE _
                         AndAlso p.ID <> _validate.ID)
            Return query.Any
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateSetupRate(ByVal _validate As PA_SETUP_RATE_DTO) As Boolean
        Try
            If _validate.ID Is Nothing Then _validate.ID = 0
            Dim query = (From p In Context.PA_SETUP_RATE Where p.BRAND = _validate.BRAND And p.EFFECT_DATE = _validate.EFFECT_DATE And p.ID <> _validate.ID).ToList
            If query.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateSetupBonusKpiProductType(ByVal _validate As PA_SETUP_BONUS_KPI_PRODUCTTYPE_DTO) As Boolean
        Try
            If _validate.ID Is Nothing Then _validate.ID = 0
            Dim query = (From p In Context.PA_SETUP_BONUS_KPI_PRODUCTTYPE Where p.BRAND = _validate.BRAND And p.EFFECT_DATE = _validate.EFFECT_DATE And p.COMPLETION_LEVEL = _validate.COMPLETE_LV And p.TYPE_SHOP = _validate.TYPE_SHOP And p.ID <> _validate.ID).ToList
            If query.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSetupCompletion_KPI_ShopManager(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of PA_SETUP_COMPLETION_KPI_SHOPMANAGER)
        Try
            lstData = (From p In Context.PA_SETUP_COMPLETION_KPI_SHOPMANAGER Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_COMPLETION_KPI_SHOPMANAGER.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSetupRate(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of PA_SETUP_RATE)
        Try
            lstData = (From p In Context.PA_SETUP_RATE Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_RATE.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSetupBonusKpiProductType(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of PA_SETUP_BONUS_KPI_PRODUCTTYPE)
        Try
            lstData = (From p In Context.PA_SETUP_BONUS_KPI_PRODUCTTYPE Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_BONUS_KPI_PRODUCTTYPE.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function EXPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE(ByVal sLang As String) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dsData As DataSet = cls.ExecuteStore("PKG_PA_SETTING.EXPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE",
                                           New With {.P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False)
            Return dsData
        End Using
        Return Nothing
    End Function
    Public Function IMPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try

            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_SETTING.IMPORT_PA_SETUP_BONUS_KPI_PRODUCT_TYPE",
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

    Public Function GET_PAYROLL_SHEET_SUM_IMPORT(ByVal P_ORG_ID As Decimal, ByVal P_YEAR As Decimal, ByVal P_PERIOD_ID As Decimal, ByVal P_USERNAME As String) As DataSet
        Using cls As New DataAccess.QueryData
            Dim dsData As DataSet = cls.ExecuteStore("PKG_PA_BUSINESS.GET_PAYROLL_SHEET_SUM_IMPORT",
                                           New With {.P_ORG_ID = P_ORG_ID,
                                                     .P_YEAR = P_YEAR,
                                                     .P_PERIOD_ID = P_PERIOD_ID,
                                                     .P_USERNAME = P_USERNAME,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False)
            Return dsData
        End Using
        Return Nothing
    End Function

    Public Function IMPORT_PAYROLL_SHEET_SUM(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PA_BUSINESS.IMPORT_PAYROLL_SHEET_SUM",
                                            New With {.P_DOCXML = P_DOCXML,
                                                         .P_USER = P_USER})
            End Using

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function IMPORT_PAYROLL_ADVANCE(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PA_BUSINESS.IMPORT_PAYROLL_ADVANCE",
                                            New With {.P_DOCXML = P_DOCXML,
                                                         .P_USER = P_USER})
            End Using

            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
#End Region

#Region "SET UP SHOP GRADE"

    Public Function GetSetupShopGrade(ByVal _filter As PA_SETUP_SHOP_GRADEDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_SHOP_GRADEDTO)

        Try

            Dim lst = (From p In Context.PA_SETUP_SHOP_GRADE
                       From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND).DefaultIfEmpty
                       From t In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GRADE).DefaultIfEmpty
                       From ts In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_SHOP).DefaultIfEmpty
                       Select New PA_SETUP_SHOP_GRADEDTO With {
                            .ID = p.ID,
                            .BRAND = p.BRAND,
                            .BRAND_NAME = o.NAME_VN,
                            .TYPE_SHOP = p.TYPE_SHOP,
                            .TYPE_SHOP_NAME = ts.NAME_VN,
                            .GRADE = p.GRADE,
                            .GRADE_NAME = t.NAME_VN,
                            .EFFECT_DATE = p.EFFECT_DATE,
                            .CREATED_BY = p.CREATED_BY,
                            .CREATED_DATE = p.CREATED_DATE,
                            .CREATED_LOG = p.CREATED_LOG,
                            .FROM_REVENVUE = p.FROM_REVENVUE,
                            .TO_REVENVUE = p.TO_REVENVUE,
                            .MODIFIED_BY = p.MODIFIED_BY,
                            .MODIFIED_DATE = p.MODIFIED_DATE,
                            .MODIFIED_LOG = p.MODIFIED_LOG,
                            .LESS_DTTT = p.LESS_DTTT,
                            .THAN_DTTT = p.THAN_DTTT,
                            .BENEFIT_VALUE = p.BENEFIT_VALUE,
                            .NOTE = p.NOTE})

            If _filter.BRAND IsNot Nothing Then
                lst = lst.Where(Function(f) f.BRAND = _filter.BRAND)
            End If
            If _filter.BRAND_NAME <> "" Then
                lst = lst.Where(Function(f) f.BRAND_NAME.ToUpper.Contains(_filter.BRAND_NAME.ToUpper))
            End If

            If _filter.TYPE_SHOP IsNot Nothing Then
                lst = lst.Where(Function(f) f.TYPE_SHOP = _filter.TYPE_SHOP)
            End If
            If _filter.TYPE_SHOP_NAME <> "" Then
                lst = lst.Where(Function(f) f.TYPE_SHOP_NAME.ToUpper.Contains(_filter.TYPE_SHOP_NAME.ToUpper))
            End If
            If _filter.GRADE IsNot Nothing Then
                lst = lst.Where(Function(f) f.GRADE = _filter.GRADE)
            End If
            If _filter.GRADE_NAME <> "" Then
                lst = lst.Where(Function(f) f.GRADE_NAME.ToUpper.Contains(_filter.GRADE_NAME.ToUpper))
            End If
            If _filter.FROM_REVENVUE IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROM_REVENVUE = _filter.FROM_REVENVUE)
            End If
            If _filter.TO_REVENVUE IsNot Nothing Then
                lst = lst.Where(Function(f) f.TO_REVENVUE = _filter.TO_REVENVUE)
            End If
            If _filter.NOTE <> "" Then
                lst = lst.Where(Function(f) f.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If _filter.LESS_DTTT IsNot Nothing Then
                lst = lst.Where(Function(f) f.LESS_DTTT = _filter.LESS_DTTT)
            End If
            If _filter.THAN_DTTT IsNot Nothing Then
                lst = lst.Where(Function(f) f.THAN_DTTT = _filter.THAN_DTTT)
            End If
            If _filter.BENEFIT_VALUE IsNot Nothing Then
                lst = lst.Where(Function(f) f.BENEFIT_VALUE = _filter.BENEFIT_VALUE)
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

    Public Function InsertSetupShopGrade(ByVal obj As PA_SETUP_SHOP_GRADEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objData As New PA_SETUP_SHOP_GRADE
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_SHOP_GRADE.EntitySet.Name)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.TYPE_SHOP = obj.TYPE_SHOP
            objData.GRADE = obj.GRADE
            objData.TO_REVENVUE = obj.TO_REVENVUE
            objData.FROM_REVENVUE = obj.FROM_REVENVUE
            objData.NOTE = obj.NOTE
            objData.LESS_DTTT = obj.LESS_DTTT
            objData.THAN_DTTT = obj.THAN_DTTT
            objData.BENEFIT_VALUE = obj.BENEFIT_VALUE

            Context.PA_SETUP_SHOP_GRADE.AddObject(objData)
            Context.SaveChanges(log)

            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifySetupShopGrade(ByVal obj As PA_SETUP_SHOP_GRADEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_SETUP_SHOP_GRADE With {.ID = obj.ID}
        Try
            Context.PA_SETUP_SHOP_GRADE.Attach(objData)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.TYPE_SHOP = obj.TYPE_SHOP
            objData.GRADE = obj.GRADE
            objData.TO_REVENVUE = obj.TO_REVENVUE
            objData.FROM_REVENVUE = obj.FROM_REVENVUE
            objData.NOTE = obj.NOTE
            objData.LESS_DTTT = obj.LESS_DTTT
            objData.THAN_DTTT = obj.THAN_DTTT
            objData.BENEFIT_VALUE = obj.BENEFIT_VALUE

            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateSetupShopGrade(ByVal _validate As PA_SETUP_SHOP_GRADEDTO) As Boolean
        Try
            If _validate.ID IsNot Nothing Then
                Dim query = (From p In Context.PA_SETUP_SHOP_GRADE Where p.BRAND = _validate.BRAND And p.EFFECT_DATE = _validate.EFFECT_DATE And p.TYPE_SHOP = _validate.TYPE_SHOP And p.GRADE = _validate.GRADE And p.ID <> _validate.ID).ToList
                If query.Count > 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Dim query = (From p In Context.PA_SETUP_SHOP_GRADE Where p.BRAND = _validate.BRAND And p.EFFECT_DATE = _validate.EFFECT_DATE And p.TYPE_SHOP = _validate.TYPE_SHOP And p.GRADE = _validate.GRADE).ToList
                If query.Count > 0 Then
                    Return False
                Else
                    Return True
                End If
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSetupShopGrade(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of PA_SETUP_SHOP_GRADE)
        Try
            lstData = (From p In Context.PA_SETUP_SHOP_GRADE Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_SHOP_GRADE.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "SET UP FRAMEWORK OFFICE"
    Public Function GetGroup_Tilte() As DataTable
        Try
            Dim query = From p In Context.OT_OTHER_LIST Where p.TYPE_CODE = "HU_TITLE_GROUP" And p.ACTFLG = "A"
            Dim obj = query.Select(Function(f) New PAListSalariesDTO With
                        {.ID = f.ID,
                         .NAME_VN = f.NAME_VN
                        }).ToList
            Return obj.ToTable()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetHU_TITLE() As DataTable
        Try
            Dim query = From p In Context.HU_TITLE
            Dim obj = query.Select(Function(f) New PAListSalariesDTO With
                        {.ID = f.ID,
                         .NAME_VN = f.NAME_VN
                        }).ToList
            Return obj.ToTable()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetSetupFrameWorkOffice(ByVal _filter As PA_SETUP_FRAMEWORK_OFFICEDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_FRAMEWORK_OFFICEDTO)

        Try

            Dim lst = (From p In Context.PA_SETUP_FRAMEWORK_OFFICE
                       From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND).DefaultIfEmpty
                       From oo In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                       From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                       Select New PA_SETUP_FRAMEWORK_OFFICEDTO With {
                            .ID = p.ID,
                            .BRAND = p.BRAND,
                            .BRAND_NAME = o.NAME_VN,
                            .TITLE_ID = p.TITLE_ID,
                            .TITLE_NAME = oo.NAME_VN,
                            .EFFECT_DATE = p.EFFECT_DATE,
                            .CREATED_BY = p.CREATED_BY,
                            .CREATED_DATE = p.CREATED_DATE,
                            .CREATED_LOG = p.CREATED_LOG,
                            .FROM_RATE = p.FROM_RATE,
                            .FROM_TARGET = p.FROM_TARGET,
                            .MODIFIED_BY = p.MODIFIED_BY,
                            .MODIFIED_DATE = p.MODIFIED_DATE,
                            .MODIFIED_LOG = p.MODIFIED_LOG,
                            .NOTE = p.NOTE,
                            .STANDARD_SALES = p.STANDARD_SALES,
                            .TO_TARGET = p.TO_TARGET,
                            .TO_RATE = p.TO_RATE})

            If _filter.BRAND IsNot Nothing Then
                lst = lst.Where(Function(f) f.BRAND = _filter.BRAND)
            End If
            If _filter.BRAND_NAME <> "" Then
                lst = lst.Where(Function(f) f.BRAND_NAME.ToUpper.Contains(_filter.BRAND_NAME.ToUpper))
            End If
            If _filter.TO_RATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.TO_RATE = _filter.TO_RATE)
            End If
            If _filter.TO_TARGET IsNot Nothing Then
                lst = lst.Where(Function(f) f.TO_TARGET = _filter.TO_TARGET)
            End If
            If _filter.STANDARD_SALES IsNot Nothing Then
                lst = lst.Where(Function(f) f.STANDARD_SALES = _filter.STANDARD_SALES)
            End If
            If _filter.FROM_RATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROM_RATE = _filter.FROM_RATE)
            End If
            If _filter.FROM_TARGET IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROM_TARGET = _filter.FROM_TARGET)
            End If
            If _filter.NOTE <> "" Then
                lst = lst.Where(Function(f) f.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
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

    Public Function InsertSetupFrameWorkOffice(ByVal obj As PA_SETUP_FRAMEWORK_OFFICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objData As New PA_SETUP_FRAMEWORK_OFFICE
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_FRAMEWORK_OFFICE.EntitySet.Name)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.TITLE_ID = obj.TITLE_ID
            objData.TO_RATE = obj.TO_RATE
            objData.TO_TARGET = obj.TO_TARGET
            objData.FROM_RATE = obj.FROM_RATE
            objData.FROM_TARGET = obj.FROM_TARGET
            objData.STANDARD_SALES = obj.STANDARD_SALES
            objData.NOTE = obj.NOTE

            Context.PA_SETUP_FRAMEWORK_OFFICE.AddObject(objData)
            Context.SaveChanges(log)

            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifySetupFrameWorkOffice(ByVal obj As PA_SETUP_FRAMEWORK_OFFICEDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_SETUP_FRAMEWORK_OFFICE With {.ID = obj.ID}
        Try
            Context.PA_SETUP_FRAMEWORK_OFFICE.Attach(objData)
            objData.BRAND = obj.BRAND
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.TITLE_ID = obj.TITLE_ID
            objData.TO_RATE = obj.TO_RATE
            objData.TO_TARGET = obj.TO_TARGET
            objData.FROM_RATE = obj.FROM_RATE
            objData.FROM_TARGET = obj.FROM_TARGET
            objData.STANDARD_SALES = obj.STANDARD_SALES
            objData.NOTE = obj.NOTE

            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateSetupFrameWorkOffice(ByVal _validate As PA_SETUP_FRAMEWORK_OFFICEDTO) As Boolean
        Try
            Dim query = (From p In Context.PA_SETUP_FRAMEWORK_OFFICE Where p.BRAND = _validate.BRAND AndAlso _
                              EntityFunctions.TruncateTime(p.EFFECT_DATE) = EntityFunctions.TruncateTime(_validate.EFFECT_DATE) _
                              AndAlso p.TITLE_ID = _validate.TITLE_ID AndAlso p.ID <> _validate.ID AndAlso p.FROM_RATE = _validate.FROM_RATE AndAlso p.STANDARD_SALES = _validate.STANDARD_SALES _
                              AndAlso p.TO_RATE = _validate.TO_RATE AndAlso p.TO_TARGET = _validate.TO_TARGET AndAlso p.FROM_TARGET = _validate.FROM_TARGET).Any
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSetupFrameWorkOffice(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of PA_SETUP_FRAMEWORK_OFFICE)
        Try
            lstData = (From p In Context.PA_SETUP_FRAMEWORK_OFFICE Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_FRAMEWORK_OFFICE.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "SET UP FRAMEWORK KPI"
    Public Function GET_KPI_IMPORT() As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dsdata As DataSet = cls.ExecuteStore("PKG_PA_SETTING.GET_KPI_IMPORT",
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

    Public Function GetSetupFrameWorkKPI(ByVal _filter As PA_SETUP_FRAMEWORK_KPIDTO, ByVal PageIndex As Integer,
                                            ByVal PageSize As Integer,
                                            ByRef Total As Integer,
                                            Optional ByVal log As UserLog = Nothing,
                                            Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_FRAMEWORK_KPIDTO)

        Try

            Dim lst = (From p In Context.PA_SETUP_FRAMEWORK_KPI
                       From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.EMPLOYEE_OBJECT).DefaultIfEmpty
                       From b In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND_ID).DefaultIfEmpty
                       From i In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.INDEX_TYPE_ID).DefaultIfEmpty
                       Select New PA_SETUP_FRAMEWORK_KPIDTO With {
                            .ID = p.ID,
                            .FROM_RATE = p.FROM_RATE,
                            .TO_RATE = p.TO_RATE,
                            .KPI_FACTOR = p.KPI_FACTOR,
                            .EFFECT_DATE = p.EFFECT_DATE,
                            .EMPLOYEE_OBJECT_ID = p.EMPLOYEE_OBJECT,
                            .EMPLOYEE_OBJECT_NAME = o.NAME_VN,
                            .BRAND_ID = p.BRAND_ID,
                            .BRAND_NAME = b.NAME_VN,
                            .INDEX_TYPE_ID = p.INDEX_TYPE_ID,
                            .INDEX_TYPE_NAME = i.NAME_VN,
                            .CREATED_BY = p.CREATED_BY,
                            .CREATED_DATE = p.CREATED_DATE,
                            .CREATED_LOG = p.CREATED_LOG,
                            .MODIFIED_BY = p.MODIFIED_BY,
                            .MODIFIED_DATE = p.MODIFIED_DATE,
                            .MODIFIED_LOG = p.MODIFIED_LOG,
                            .NOTE = p.NOTE})

            If _filter.TO_RATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.TO_RATE = _filter.TO_RATE)
            End If
            If _filter.FROM_RATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROM_RATE = _filter.FROM_RATE)
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.KPI_FACTOR IsNot Nothing Then
                lst = lst.Where(Function(f) f.KPI_FACTOR = _filter.KPI_FACTOR)
            End If
            If _filter.EMPLOYEE_OBJECT_NAME IsNot Nothing Then
                lst = lst.Where(Function(f) f.EMPLOYEE_OBJECT_NAME = _filter.EMPLOYEE_OBJECT_NAME)
            End If
            If _filter.NOTE <> "" Then
                lst = lst.Where(Function(f) f.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
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

    Public Function InsertSetupFrameWorkKPI(ByVal obj As PA_SETUP_FRAMEWORK_KPIDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objData As New PA_SETUP_FRAMEWORK_KPI
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_FRAMEWORK_KPI.EntitySet.Name)
            objData.KPI_FACTOR = obj.KPI_FACTOR
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.TO_RATE = obj.TO_RATE
            objData.EMPLOYEE_OBJECT = obj.EMPLOYEE_OBJECT_ID
            objData.FROM_RATE = obj.FROM_RATE
            objData.NOTE = obj.NOTE
            objData.BRAND_ID = obj.BRAND_ID
            objData.INDEX_TYPE_ID = obj.INDEX_TYPE_ID

            Context.PA_SETUP_FRAMEWORK_KPI.AddObject(objData)
            Context.SaveChanges(log)

            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifySetupFrameWorkKPI(ByVal obj As PA_SETUP_FRAMEWORK_KPIDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_SETUP_FRAMEWORK_KPI With {.ID = obj.ID}
        Try
            Context.PA_SETUP_FRAMEWORK_KPI.Attach(objData)
            objData.KPI_FACTOR = obj.KPI_FACTOR
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.TO_RATE = obj.TO_RATE
            objData.EMPLOYEE_OBJECT = obj.EMPLOYEE_OBJECT_ID
            objData.FROM_RATE = obj.FROM_RATE
            objData.NOTE = obj.NOTE
            objData.BRAND_ID = obj.BRAND_ID
            objData.INDEX_TYPE_ID = obj.INDEX_TYPE_ID

            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function ValidateFrameWorkKPI(ByVal obj As PA_SETUP_FRAMEWORK_KPIDTO) As Boolean
        Try
            Dim query = (From f In Context.PA_SETUP_FRAMEWORK_KPI Where f.EMPLOYEE_OBJECT = obj.EMPLOYEE_OBJECT_ID _
                                                                           AndAlso EntityFunctions.TruncateTime(f.EFFECT_DATE) = EntityFunctions.TruncateTime(obj.EFFECT_DATE) _
                                                                           AndAlso f.BRAND_ID = obj.BRAND_ID _
                                                                           AndAlso f.INDEX_TYPE_ID = obj.INDEX_TYPE_ID _
                                                                           AndAlso f.FROM_RATE = obj.FROM_RATE _
                                                                           AndAlso f.TO_RATE = obj.TO_RATE _
                                                                           AndAlso f.KPI_FACTOR = obj.KPI_FACTOR _
                                                                           AndAlso f.ID <> obj.ID).Any
            Return query

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ValidateSetupFrameWorkKPI(ByVal _validate As PA_SETUP_FRAMEWORK_KPIDTO) As Boolean
        Try
            Dim count = 0
            If _validate.ID Is Nothing Then
                count = (From p In Context.PA_SETUP_FRAMEWORK_KPI Where p.EFFECT_DATE = _validate.EFFECT_DATE And p.EMPLOYEE_OBJECT = _validate.EMPLOYEE_OBJECT_ID).ToList.Count
            Else
                count = (From p In Context.PA_SETUP_FRAMEWORK_KPI Where p.EFFECT_DATE = _validate.EFFECT_DATE And p.EMPLOYEE_OBJECT = _validate.EMPLOYEE_OBJECT_ID And p.ID <> _validate.ID).ToList.Count

            End If
            If count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSetupFrameWorkKPI(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of PA_SETUP_FRAMEWORK_KPI)
        Try
            lstData = (From p In Context.PA_SETUP_FRAMEWORK_KPI Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_FRAMEWORK_KPI.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "SET UP FRAMEWORK ECD"

    Public Function GetSetupFrameWorkECD(ByVal _filter As PA_SETUP_FRAMEWORK_ECDDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SETUP_FRAMEWORK_ECDDTO)

        Try

            Dim lst = (From p In Context.PA_SETUP_FRAMEWORK_ECD
                       From b In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND).DefaultIfEmpty
                       From s In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GROUP_EMPLOYEE_ID).DefaultIfEmpty
                       Select New PA_SETUP_FRAMEWORK_ECDDTO With {
                            .ID = p.ID,
                            .FROM_RATE = p.FROM_RATE,
                            .TO_RATE = p.TO_RATE,
                            .EFFECT_DATE = p.EFFECT_DATE,
                            .CREATED_BY = p.CREATED_BY,
                            .CREATED_DATE = p.CREATED_DATE,
                            .CREATED_LOG = p.CREATED_LOG,
                            .FROM_AVG_SALE = p.FROM_AVG_SALE,
                            .TO_AVG_SALE = p.TO_AVG_SALE,
                            .MODIFIED_BY = p.MODIFIED_BY,
                            .MODIFIED_DATE = p.MODIFIED_DATE,
                            .MODIFIED_LOG = p.MODIFIED_LOG,
                            .BRAND = p.BRAND,
                            .BRAND_NAME = b.NAME_VN,
                            .GROUP_EMPLOYEE_ID = p.GROUP_EMPLOYEE_ID,
                            .GROUP_EMPLOYEE_NAME = s.NAME_VN,
                            .LDTC = p.LDTC,
                            .NOTE = p.NOTE})

            If _filter.TO_RATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.TO_RATE = _filter.TO_RATE)
            End If
            If _filter.FROM_RATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROM_RATE = _filter.FROM_RATE)
            End If
            If _filter.FROM_AVG_SALE IsNot Nothing Then
                lst = lst.Where(Function(f) f.FROM_AVG_SALE = _filter.FROM_AVG_SALE)
            End If
            If _filter.LDTC IsNot Nothing Then
                lst = lst.Where(Function(f) f.LDTC = _filter.LDTC)
            End If
            If _filter.TO_AVG_SALE IsNot Nothing Then
                lst = lst.Where(Function(f) f.TO_AVG_SALE = _filter.TO_AVG_SALE)
            End If
            If _filter.NOTE <> "" Then
                lst = lst.Where(Function(f) f.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If
            If _filter.BRAND_NAME <> "" Then
                lst = lst.Where(Function(f) f.BRAND_NAME.ToUpper.Contains(_filter.BRAND_NAME.ToUpper))
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

    Public Function InsertSetupFrameWorkECD(ByVal obj As PA_SETUP_FRAMEWORK_ECDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objData As New PA_SETUP_FRAMEWORK_ECD
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_SETUP_FRAMEWORK_ECD.EntitySet.Name)
            objData.FROM_AVG_SALE = obj.FROM_AVG_SALE
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.TO_RATE = obj.TO_RATE
            objData.TO_AVG_SALE = obj.TO_AVG_SALE
            objData.FROM_RATE = obj.FROM_RATE
            objData.NOTE = obj.NOTE
            objData.BRAND = obj.BRAND
            objData.GROUP_EMPLOYEE_ID = obj.GROUP_EMPLOYEE_ID
            objData.LDTC = obj.LDTC

            Context.PA_SETUP_FRAMEWORK_ECD.AddObject(objData)
            Context.SaveChanges(log)

            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifySetupFrameWorkECD(ByVal obj As PA_SETUP_FRAMEWORK_ECDDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_SETUP_FRAMEWORK_ECD With {.ID = obj.ID}
        Try
            Context.PA_SETUP_FRAMEWORK_ECD.Attach(objData)
            objData.FROM_AVG_SALE = obj.FROM_AVG_SALE
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.TO_RATE = obj.TO_RATE
            objData.TO_AVG_SALE = obj.TO_AVG_SALE
            objData.FROM_RATE = obj.FROM_RATE
            objData.NOTE = obj.NOTE
            objData.GROUP_EMPLOYEE_ID = obj.GROUP_EMPLOYEE_ID
            objData.BRAND = obj.BRAND
            objData.LDTC = obj.LDTC

            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateSetupFrameWorkECD(ByVal _validate As PA_SETUP_FRAMEWORK_ECDDTO) As Boolean
        Try
            Dim query = (From p In Context.PA_SETUP_FRAMEWORK_ECD Where EntityFunctions.TruncateTime(p.EFFECT_DATE) = EntityFunctions.TruncateTime(_validate.EFFECT_DATE) AndAlso _
                         p.ID <> _validate.ID).Any
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSetupFrameWorkECD(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of PA_SETUP_FRAMEWORK_ECD)
        Try
            lstData = (From p In Context.PA_SETUP_FRAMEWORK_ECD Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstData.Count - 1
                Context.PA_SETUP_FRAMEWORK_ECD.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GET_FRAMEWORK_ECD_IMPORT_DATA() As DataSet
        Try
            Using cls As New DataAccess.QueryData


                Dim dtData As DataSet = cls.ExecuteStore("PKG_PA_SETTING.GET_FRAMEWORK_ECD_IMPORT_DATA", New With {.P_CUR = cls.OUT_CURSOR,
                                                                                                                   .P_CUR1 = cls.OUT_CURSOR}, False)
                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function IMPORT_FRAMEWORK_ECD(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_PA_SETTING.IMPORT_FRAMEWORK_ECD",
                                               New With {.P_DOCXML = P_DOCXML,
                                                         .P_USER = P_USER,
                                                         .P_CUR = cls.OUT_CURSOR}, True)
                Return CBool(dtData(0)(0))
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "WORK STANDARD"
    Public Function IsCompanyLevel(ByVal org_id As Decimal) As Boolean
        Try
            Dim orgId2 As Decimal? = Context.HUV_ORGANIZATION.Where(Function(f) f.ID = org_id).Select(Function(f) f.ORG_ID2).FirstOrDefault()
            Return org_id = orgId2
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetWorkStandard(ByVal _filter As Work_StandardDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing,
                                        Optional ByVal Sorts As String = " YEAR, PERIOD_ID desc") As List(Of Work_StandardDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _filter.param.ORG_ID,
                                           .P_ISDISSOLVE = _filter.param.IS_DISSOLVE})
            End Using

            Dim lst = (From p In Context.PA_WORK_STANDARD
                       From OT In Context.OT_OTHER_LIST.Where(Function(OT) OT.ID = p.OBJECT_ID).DefaultIfEmpty()
                       From OTL In Context.OT_OTHER_LIST_TYPE.Where(Function(OTL) OTL.ID = OT.TYPE_ID And OTL.CODE = "OBJECT_LABOR").DefaultIfEmpty()
                       From AT In Context.AT_PERIOD.Where(Function(AT) p.PERIOD_ID = AT.ID).DefaultIfEmpty()
                       From ORG In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty()
                       From s In Context.SE_CHOSEN_ORG.Where(Function(se) se.ORG_ID = ORG.ID And
                                                                    se.USERNAME = log.Username.ToUpper)
                       Where p.ORG_ID = _filter.param.ORG_ID
                       Select New Work_StandardDTO With {
                                                  .ID = p.ID,
                                                  .YEAR = p.YEAR,
                                                  .ORG_ID = p.ORG_ID,
                                                  .ORG_NAME = ORG.NAME_VN,
                                                  .PERIOD_ID = p.PERIOD_ID,
                                                  .PERIOD_NAME = AT.PERIOD_NAME,
                                                  .OBJECT_ID = p.OBJECT_ID,
                                                  .OBJECT_NAME = OT.NAME_VN,
                                                  .Period_standard = p.PERIOD_STANDARD,
                                                  .CREATED_DATE = p.CREATED_DATE,
                                                  .REMARK = p.REMARK,
                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")
                                                  })
            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.PERIOD_NAME <> "" Then
                lst = lst.Where(Function(f) f.PERIOD_NAME.ToUpper.Contains(_filter.PERIOD_NAME.ToUpper))
            End If
            If _filter.OBJECT_NAME <> "" Then
                lst = lst.Where(Function(f) f.OBJECT_NAME.ToUpper.Contains(_filter.OBJECT_NAME.ToUpper))
            End If
            If _filter.ORG_ID <> 0 Then
                lst = lst.Where(Function(p) p.ORG_ID = _filter.ORG_ID)
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

    Public Function GetWorkStandardbyYear(ByVal year As Decimal) As List(Of Work_StandardDTO)
        Try
            Dim lst = (From p In Context.PA_WORK_STANDARD
                       From OT In Context.OT_OTHER_LIST.Where(Function(OT) OT.ID = p.OBJECT_ID And OT.TYPE_ID = 2071).DefaultIfEmpty()
                       From AT In Context.AT_PERIOD.Where(Function(AT) p.PERIOD_ID = AT.ID).DefaultIfEmpty()
                       From org In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty() Where p.YEAR = year
                       Select New Work_StandardDTO With {
                                                  .ID = p.ID,
                                                  .YEAR = p.YEAR,
                                                  .ORG_ID = p.ORG_ID,
                                                  .ORG_NAME = org.NAME_VN,
                                                  .PERIOD_ID = p.PERIOD_ID,
                                                  .PERIOD_NAME = AT.PERIOD_NAME,
                                                  .OBJECT_ID = p.OBJECT_ID,
                                                  .OBJECT_NAME = OT.NAME_VN,
                                                  .Period_standard = p.PERIOD_STANDARD,
                                                  .CREATED_DATE = p.CREATED_DATE,
                                                  .REMARK = p.REMARK,
                                                   .ACTFLG = p.ACTFLG
                                                  })
            'Dim query = From p In Context.PA_WORK_STANDARD Where p.YEAR = year Order By p.ID Ascending
            'Dim Period = query.Select(Function(p) New Work_StandardDTO With {
            '                           .ID = p.ID,
            '                           .YEAR = p.YEAR,
            '                           .PERIOD_ID = p.PERIOD_ID,
            '                           .OBJECT_ID = p.OBJECT_ID,
            '                           .Period_standard = p.PERIOD_STANDARD,
            '                           .CREATED_DATE = p.CREATED_DATE,
            '                           .REMARK = p.REMARK,
            '                           .ACTFLG = p.ACTFLG})


            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertWorkStandard(ByVal objPeriod As Work_StandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objPeriodData As New PA_WORK_STANDARD
        Try
            objPeriodData.ID = Utilities.GetNextSequence(Context, Context.PA_WORK_STANDARD.EntitySet.Name)
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.PERIOD_ID = objPeriod.PERIOD_ID
            objPeriodData.OBJECT_ID = objPeriod.OBJECT_ID
            objPeriodData.ORG_ID = objPeriod.ORG_ID
            objPeriodData.PERIOD_STANDARD = objPeriod.Period_standard
            objPeriodData.REMARK = objPeriod.REMARK
            objPeriodData.ACTFLG = objPeriod.ACTFLG
            Context.PA_WORK_STANDARD.AddObject(objPeriodData)
            Context.SaveChanges(log)

            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateWorkStandard(ByVal _validate As Work_StandardDTO) As Boolean
        Try
            Dim query = (From p In Context.PA_WORK_STANDARD Where p.ORG_ID = _validate.ORG_ID And p.PERIOD_ID = _validate.PERIOD_ID And p.OBJECT_ID = _validate.OBJECT_ID And p.ID <> _validate.ID).ToList
            If query.Count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyWORKSTANDARD(ByVal objPeriod As Work_StandardDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New PA_WORK_STANDARD With {.ID = objPeriod.ID}
        Try
            Context.PA_WORK_STANDARD.Attach(objPeriodData)
            objPeriodData.YEAR = objPeriod.YEAR
            objPeriodData.ORG_ID = objPeriod.ORG_ID
            objPeriodData.PERIOD_ID = objPeriod.PERIOD_ID
            objPeriodData.OBJECT_ID = objPeriod.OBJECT_ID
            objPeriodData.PERIOD_STANDARD = objPeriod.Period_standard
            objPeriodData.REMARK = objPeriod.REMARK
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeleteWorkStandard(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstWorkFactorData As List(Of PA_WORK_STANDARD)
        Try
            lstWorkFactorData = (From p In Context.PA_WORK_STANDARD Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstWorkFactorData.Count - 1
                Context.PA_WORK_STANDARD.DeleteObject(lstWorkFactorData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

    Public Function ActiveWorkStandard(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_WORK_STANDARD)
        Try
            lstData = (From p In Context.PA_WORK_STANDARD Where lstID.Contains(p.ID)).ToList
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
#End Region

#Region "List Salary Fomuler"
    Public Function GetAllFomulerGroup(ByVal _filter As PAFomulerGroup, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "IDX ASC") As List(Of PAFomulerGroup)

        Try
            Dim lst = (From q In Context.PA_FORMULER_GROUP
                       From s In Context.PA_OBJECT_SALARY.Where(Function(s) s.ID = q.OBJ_SAL_ID)
                       Select New PAFomulerGroup With {.ID = q.ID,
                                               .NAME_VN = q.NAME_VN,
                                               .NAME_EN = q.NAME_EN,
                                               .OBJ_SAL_ID = q.OBJ_SAL_ID,
                                               .OBJ_SAL_NAME = s.NAME_VN,
                                               .START_DATE = q.START_DATE,
                                               .END_DATE = q.END_DATE,
                                               .SDESC = q.SDESC,
                                               .STATUS = q.STATUS,
                                               .IDX = q.IDX,
                                                .AWARD_CODE = q.AWARD_CODE
                                               })
            If _filter.OBJ_SAL_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJ_SAL_NAME.ToUpper.Contains(_filter.OBJ_SAL_NAME.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
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

    Public Function EXPORT_CH(ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_PA_BUSINESS.EXPORT_CH",
                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False) ' FALSE : no datatable

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetAllNorm(ByVal _filter As NormDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ID") As List(Of NormDTO)

        Try
            Dim lst = (From q In Context.PA_STANDARD_SETUP
                       From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = q.EMPLOYEE_ID).DefaultIfEmpty
                       From oe In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                       From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = q.ORG_ID).DefaultIfEmpty
                       From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                       From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = q.NORM_ID).DefaultIfEmpty
                       Select New NormDTO With {.ID = q.ID,
                                                .EMPLOYEE_ID = q.EMPLOYEE_ID,
                                                .ORG_NAME = oe.NAME_VN,
                                                .EMP_NAME = e.FULLNAME_VN,
                                                .EMP_CODE = e.EMPLOYEE_CODE,
                                                .TITLE_NAME = t.NAME_VN,
                                                .ORG_ID = q.ORG_ID,
                                                .ORG = o.NAME_VN,
                                                .EFFECT_DATE = q.EFFECT_DATE,
                                                .EXPIRE_DATE = q.EXPIRE_DATE,
                                                .VALUE = q.VALUE,
                                                .NORM_ID = q.NORM_ID,
                                                .NORM = ot.NAME_VN,
                                                .NOTE = q.NOTE,
                                                .YEAR_SENIORITY = q.YEAR_SENIORITY,
                                                .WORK_STATUS = e.WORK_STATUS,
                                                .TER_EFFECT_DATE = e.TER_EFFECT_DATE
                                               })

            Dim dateNow = Date.Now.Date

            If Not _filter.IS_TER Then
                lst = lst.Where(Function(p) Not p.WORK_STATUS.HasValue Or
                                    (p.WORK_STATUS.HasValue And
                                     ((p.WORK_STATUS <> 257) Or (p.WORK_STATUS = 257 And p.TER_EFFECT_DATE > dateNow))))

            End If


            If _filter.EMP_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMP_NAME.ToUpper.Contains(_filter.EMP_NAME.ToUpper))
            End If
            If _filter.EMP_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMP_CODE.ToUpper.Contains(_filter.EMP_CODE.ToUpper))
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

    Public Function InsertFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_FORMULER_GROUP
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.AT_PERIOD.EntitySet.Name)
            objData.TYPE_PAYMENT = objPeriod.TYPE_PAYMENT
            objData.OBJ_SAL_ID = objPeriod.OBJ_SAL_ID
            objData.NAME_VN = objPeriod.NAME_VN
            objData.NAME_EN = objPeriod.NAME_EN
            objData.START_DATE = objPeriod.START_DATE
            objData.END_DATE = objPeriod.END_DATE
            objData.STATUS = objPeriod.STATUS
            objData.SDESC = objPeriod.SDESC
            objData.IDX = objPeriod.IDX
            objData.AWARD_CODE = objPeriod.AWARD_CODE
            Context.PA_FORMULER_GROUP.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyFomulerGroup(ByVal objPeriod As PAFomulerGroup, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_FORMULER_GROUP With {.ID = objPeriod.ID}
        Try
            Context.PA_FORMULER_GROUP.Attach(objData)
            objData.OBJ_SAL_ID = objPeriod.OBJ_SAL_ID
            objData.NAME_VN = objPeriod.NAME_VN
            objData.NAME_EN = objPeriod.NAME_EN
            objData.START_DATE = objPeriod.START_DATE
            objData.END_DATE = objPeriod.END_DATE
            objData.STATUS = objPeriod.STATUS
            objData.SDESC = objPeriod.SDESC
            objData.IDX = objPeriod.IDX
            objData.AWARD_CODE = objPeriod.AWARD_CODE
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function InsertNorm(ByVal obj As NormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_STANDARD_SETUP
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_STANDARD_SETUP.EntitySet.Name)
            objData.ORG_ID = obj.ORG_ID
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.EXPIRE_DATE = obj.EXPIRE_DATE
            objData.VALUE = obj.VALUE
            objData.YEAR_SENIORITY = obj.YEAR_SENIORITY
            objData.NORM_ID = obj.NORM_ID
            objData.NOTE = obj.NOTE
            Context.PA_STANDARD_SETUP.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyNorm(ByVal obj As NormDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_STANDARD_SETUP With {.ID = obj.ID}
        Try
            Context.PA_STANDARD_SETUP.Attach(objData)
            objData.ORG_ID = obj.ORG_ID
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.EXPIRE_DATE = obj.EXPIRE_DATE
            objData.VALUE = obj.VALUE
            objData.NORM_ID = obj.NORM_ID
            objData.NOTE = obj.NOTE
            objData.YEAR_SENIORITY = obj.YEAR_SENIORITY
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteFomulerGroup(ByVal lstDelete As PAFomulerGroup) As Boolean
        Dim objData As List(Of PA_FORMULER_GROUP) = (From p In Context.PA_FORMULER_GROUP Where p.ID = lstDelete.ID).ToList
        Try
            For Each item In objData
                Context.PA_FORMULER_GROUP.DeleteObject(item)
            Next
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteNorm(ByVal lstDelete As List(Of Decimal)) As Boolean
        Dim objData As List(Of PA_STANDARD_SETUP) = (From p In Context.PA_STANDARD_SETUP Where lstDelete.Contains(p.ID)).ToList
        Try
            For Each item In objData
                Context.PA_STANDARD_SETUP.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetListAllSalary(ByVal gID As Decimal) As List(Of PAFomuler)
        Try
            Dim query = From p In Context.PA_LISTSALARIES
                        From g In Context.PA_FORMULER_GROUP.Where(Function(g) g.OBJ_SAL_ID = p.OBJ_SAL_ID)
                        From f In Context.PA_FORMULER.Where(Function(f) f.GROUP_FML = g.ID And f.COL_NAME = p.COL_NAME).DefaultIfEmpty
                        Where g.ID = gID And p.IS_DELETED = 0 And (p.IS_SUMARISING = -1 Or p.IS_WORKARISING = -1) And p.IS_IMPORT = 0 Order By f.INDEX_FML Ascending, f.COL_NAME Ascending
            Dim obj = query.Select(Function(o) New PAFomuler With
                        {.ID = o.p.ID,
                         .COL_NAME = o.p.COL_NAME,
                         .NAME_VN = o.p.NAME_VN,
                         .NAME_EN = o.p.NAME_EN,
                         .COL_INDEX = o.f.INDEX_FML,
                         .FORMULER = o.f.FORMULER
                        }).ToList
            Return obj
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetListInputColumn(ByVal gID As Decimal) As DataTable
        Try
            Dim ObjSal As Decimal = (From p In Context.PA_FORMULER_GROUP Where p.ID = gID Select p.OBJ_SAL_ID).FirstOrDefault
            Dim query = From p In Context.PA_SYSTEM_CRITERIA Where p.OBJ_SAL_ID = ObjSal
            '_
            'And p.IS_SALARY = -1 And p.STATUS = "A"
            Dim obj = query.Select(Function(f) New PAListSalariesDTO With
                        {.ID = f.ID,
                         .COL_INDEX = f.UNIT,
                         .COL_NAME = f.CODE,
                         .NAME_VN = f.NAME & " - (" & f.CODE & ")",
                         .NAME_EN = f.NAME
                        }).ToList()
            Dim query1 = From p In Context.PA_LISTSALARIES
                         From g In Context.PA_FORMULER_GROUP.Where(Function(g) g.OBJ_SAL_ID = p.OBJ_SAL_ID)
                         Where p.STATUS = "A" And g.ID = gID Order By p.NAME_VN Ascending, p.COL_INDEX Ascending

            Dim obj1 = query1.Select(Function(f) New PAListSalariesDTO With
                        {.ID = f.p.ID,
                         .COL_INDEX = f.p.COL_INDEX,
                         .COL_NAME = f.p.COL_NAME,
                         .NAME_VN = f.p.NAME_VN & " - (" & f.p.COL_NAME & ")",
                         .NAME_EN = f.p.NAME_EN
                        }).ToList

            Dim list = obj.Union(obj1)
            Return list.ToList.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetListSalColunm(ByVal gID As Decimal) As DataTable
        Try
            Dim query = From p In Context.PA_LISTSAL
                        Where p.STATUS = "A" And p.GROUP_TYPE = gID Order By p.NAME_VN, p.COL_INDEX Ascending
            Dim obj = query.Select(Function(f) New PAListSalDTO With
                        {.ID = f.ID,
                         .COL_INDEX = f.COL_INDEX,
                         .COL_NAME = f.COL_NAME,
                         .NAME_VN = f.NAME_VN,
                         .NAME_EN = f.NAME_EN
                        }).ToList
            Return obj.ToTable()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function GetListCalculation() As List(Of OT_OTHERLIST_DTO)
        Try
            Dim query = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                         Where p.ACTFLG = "A" And t.CODE = "CALCULATION" Order By p.CREATED_DATE Descending
                         Select New OT_OTHERLIST_DTO With {
                             .ID = p.ID,
                             .CODE = p.CODE,
                             .NAME_EN = p.NAME_EN,
                             .NAME_VN = p.NAME_VN,
                .TYPE_ID = p.TYPE_ID
                         }).ToList
            Return query
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function CopyFomuler(ByRef F_ID As Decimal,
                                    ByVal log As UserLog, ByRef T_ID As Decimal) As Boolean


        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.COPY_FORMULER_SALARY",
                                           New With {.OBJ_SAL_FROM = F_ID,
                                                     .OBJ_SAL_TO = T_ID})
            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function SaveFomuler(ByVal objData As PAFomuler, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objInsert As PA_FORMULER
        Dim iCount As Integer = 0
        Try
            objInsert = (From p In Context.PA_FORMULER Where p.COL_NAME = objData.COL_NAME And p.GROUP_FML = objData.GROUP_FML).SingleOrDefault
            If objInsert Is Nothing Then
                objInsert = New PA_FORMULER
                objInsert.ID = Utilities.GetNextSequence(Context, Context.PA_FORMULER.EntitySet.Name)
                objInsert.COL_NAME = objData.COL_NAME
                objInsert.INDEX_FML = objData.INDEX_FML
                objInsert.GROUP_FML = objData.GROUP_FML
                objInsert.FORMULER = objData.FORMULER
                objInsert.CREATED_BY = objData.CREATED_BY
                objInsert.CREATED_DATE = objData.CREATED_DATE
                objInsert.CREATED_LOG = objData.CREATED_LOG
                objInsert.MODIFIED_BY = objData.MODIFIED_BY
                objInsert.MODIFIED_DATE = objData.MODIFIED_DATE
                objInsert.MODIFIED_LOG = objData.MODIFIED_LOG
                Context.PA_FORMULER.AddObject(objInsert)
            Else
                objInsert.COL_NAME = objData.COL_NAME
                objInsert.INDEX_FML = objData.INDEX_FML
                objInsert.GROUP_FML = objData.GROUP_FML
                objInsert.FORMULER = objData.FORMULER
                objInsert.MODIFIED_BY = objData.MODIFIED_BY
                objInsert.MODIFIED_DATE = objData.MODIFIED_DATE
                objInsert.MODIFIED_LOG = objData.MODIFIED_LOG
            End If
            Context.SaveChanges(log)
            gID = objInsert.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function CheckFomuler(ByVal sCol As String, ByVal sFormuler As String, ByVal objID As Decimal) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                Dim sql As String = ""
                Dim sql1 As String = ""
                Dim sql2 As String = ""
                Dim sql3 As String = ""
                sql = "UPDATE TEMP_CALCULATE T SET T." & sCol & " = NVL(" & sFormuler & ",0)"
                sql1 = "UPDATE TEMP_CALCULATE_SUM T SET T." & sCol & " = NVL(" & sFormuler & ",0)"
                sql2 = "UPDATE PA_INCOME_TAX_SUM T SET T." & sCol & " = NVL(" & sFormuler & ",0)"
                sql3 = "UPDATE PA_PAYROLL_ACHIEVEMENT_TEMP T SET T." & sCol & " = NVL(" & sFormuler & ",0)"
                sql &= " WHERE 1=0 "
                sql1 &= " WHERE 1=0 "
                sql2 &= " WHERE 1=0 "
                sql3 &= " WHERE 1=0 "
                If objID = 11 Then
                    cls.ExecuteSQL(sql2)
                ElseIf objID = 134 Or objID = 135 Or objID = 136 Or objID = 137 Or objID = 143 Or objID = 145 Or objID = 138 Or objID = 140 Or objID = 139 Or objID = 141 Or objID = 142 Then
                    cls.ExecuteSQL(sql3)
                Else
                    cls.ExecuteSQL(sql)
                    cls.ExecuteSQL(sql1)
                End If

            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ActiveFolmulerGroup(ByVal lstID As Decimal, ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        Dim lstData As PA_FORMULER_GROUP
        Try
            lstData = (From p In Context.PA_FORMULER_GROUP Where p.ID = lstID).SingleOrDefault
            lstData.STATUS = bActive
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "Salaries List"
    Public Function GetListSalaries(ByVal _filter As PAListSalariesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC") As List(Of PAListSalariesDTO)
        Try
            Dim lst = (From p In Context.PA_LISTSALARIES
                       From o In Context.OT_OTHER_LIST.Where(Function(o) o.ID = p.TYPE_PAYMENT).DefaultIfEmpty()
                       From ot In Context.OT_OTHER_LIST_TYPE.Where(Function(ot) ot.ID = o.TYPE_ID).DefaultIfEmpty()
                       From import In Context.OT_OTHER_LIST.Where(Function(e) e.ID = p.IMPORT_TYPE_ID).DefaultIfEmpty()
                       From ot_import In Context.OT_OTHER_LIST_TYPE.Where(Function(e) e.ID = import.TYPE_ID).DefaultIfEmpty()
                       From sal_type In Context.OT_OTHER_LIST.Where(Function(sty) sty.ID = p.GROUP_TYPE).DefaultIfEmpty()
                       From sal_obj In Context.PA_OBJECT_SALARY.Where(Function(sal) sal.ID = p.OBJ_SAL_ID).DefaultIfEmpty()
                       Where p.IS_DELETED = _filter.IS_DELETED
                       Select New PAListSalariesDTO With {
                                                    .ID = p.ID,
                                                    .TYPE_PAYMENT = p.TYPE_PAYMENT,
                                                    .COL_NAME = p.COL_NAME,
                                                    .NAME_VN = p.NAME_VN,
                                                    .NAME_EN = p.NAME_EN,
                                                    .DATA_TYPE = p.DATA_TYPE,
                                                    .COL_INDEX = p.COL_INDEX,
                                                    .STATUS = If(p.STATUS = "A", "Áp dụng", "Ngừng áp dụng"),
                                                    .IS_VISIBLE = p.IS_VISIBLE,
                                                    .IS_INPUT = p.IS_INPUT,
                                                    .IS_CALCULATE = p.IS_CALCULATE,
                                                    .IS_IMPORT = p.IS_IMPORT,
                                                    .INPUT_FORMULER = p.INPUT_FORMULER,
                                                    .CREATED_DATE = p.CREATED_DATE,
                                                    .IMPORT_TYPE_ID = p.IMPORT_TYPE_ID,
                                                    .IS_WORKDAY = p.IS_WORKDAY,
                                                    .IS_SUMDAY = p.IS_SUMDAY,
                                                    .IS_WORKARISING = p.IS_WORKARISING,
                                                    .IS_SUMARISING = p.IS_SUMARISING,
                                                    .IS_PAYBACK = p.IS_PAYBACK,
                                                    .IS_DELETED = p.IS_DELETED,
                                                    .EFFECTIVE_DATE = p.EFFECTIVE_DATE,
                                                    .EXPIRE_DATE = p.EXPIRE_DATE,
                                                    .REMARK = p.REMARK,
                                                    .IMPORT_TYPE_NAME = import.NAME_VN,
                                                    .OBJ_SAL_ID = p.OBJ_SAL_ID,
                                                    .OBJ_SAL_NAME = sal_obj.NAME_VN,
                                                    .GROUP_TYPE_ID = p.GROUP_TYPE,
                                                    .GROUP_TYPE_NAME = sal_type.NAME_VN
                                                })

            If _filter.COL_NAME <> "" Then
                lst = lst.Where(Function(p) p.COL_NAME.ToUpper.Contains(_filter.COL_NAME.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If

            If _filter.DATA_TYPE <> 0 Then
                lst = lst.Where(Function(p) p.DATA_TYPE = _filter.DATA_TYPE)
            End If
            If _filter.OBJ_SAL_ID <> 0 Then
                lst = lst.Where(Function(p) p.OBJ_SAL_ID = _filter.OBJ_SAL_ID)
            End If
            If _filter.GROUP_TYPE_ID <> 0 Then
                lst = lst.Where(Function(p) p.GROUP_TYPE_ID = _filter.GROUP_TYPE_ID)
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.OBJ_SAL_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJ_SAL_NAME.ToUpper.Contains(_filter.OBJ_SAL_NAME.ToUpper))
            End If
            If _filter.GROUP_TYPE_NAME <> "" Then
                lst = lst.Where(Function(p) p.GROUP_TYPE_NAME.ToUpper.Contains(_filter.GROUP_TYPE_NAME.ToUpper))
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
    Public Function InsertListSalaries(ByVal objTitle As PAListSalariesDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_LISTSALARIES
        Dim iCount As Integer = 0

        Try

            objTitleData.TYPE_PAYMENT = objTitle.TYPE_PAYMENT
            objTitleData.COL_NAME = objTitle.COL_NAME
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.COL_INDEX = objTitle.COL_INDEX
            objTitleData.STATUS = objTitle.STATUS
            objTitleData.IS_VISIBLE = objTitle.IS_VISIBLE
            objTitleData.IS_INPUT = objTitle.IS_INPUT
            objTitleData.IS_CALCULATE = objTitle.IS_CALCULATE
            objTitleData.IS_IMPORT = objTitle.IS_IMPORT
            objTitleData.INPUT_FORMULER = objTitle.INPUT_FORMULER
            objTitleData.IMPORT_TYPE_ID = objTitle.IMPORT_TYPE_ID
            objTitleData.COL_CODE = objTitle.COL_CODE
            objTitleData.IS_WORKDAY = objTitle.IS_WORKDAY
            objTitleData.IS_SUMDAY = objTitle.IS_SUMDAY
            objTitleData.IS_WORKARISING = objTitle.IS_WORKARISING
            objTitleData.IS_SUMARISING = objTitle.IS_SUMARISING
            objTitleData.IS_PAYBACK = objTitle.IS_PAYBACK
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.IS_DELETED = objTitle.IS_DELETED
            objTitleData.GROUP_TYPE = objTitle.GROUP_TYPE_ID
            objTitleData.OBJ_SAL_ID = objTitle.OBJ_SAL_ID
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_LISTSALARIES.EntitySet.Name)
            Context.PA_LISTSALARIES.AddObject(objTitleData)
            If objTitle.IS_IMPORT = -1 Then
                Using cls As New DataAccess.NonQueryData
                    cls.ExecuteStore("PKG_PA_SETTING.ADD_COL_SALARY_IMPORT_NEW",
                                               New With {.COL_NAME = objTitle.COL_NAME,
                                                         .COL_TYPE = objTitle.DATA_TYPE})
                End Using
            End If

            ' Kiểm tra khác đối tương lương tháng
            Dim count = (From p In Context.PA_OBJECT_SALARY
                         From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GROUP_SAL)
                         Where p.ID = objTitle.OBJ_SAL_ID And o.CODE <> "LUONGTHANG").Count

            If count > 0 Then
                Using cls As New DataAccess.NonQueryData
                    cls.ExecuteStore("PKG_PA_SETTING.ADD_COL_SALARY_ACHIEVEMENT",
                                               New With {.COL_NAME = objTitle.COL_NAME,
                                                         .COL_TYPE = objTitle.DATA_TYPE})
                End Using
            End If

            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function InsertPA_SAL_MAPPING(ByVal objTitle As PA_SALARY_FUND_MAPPINGDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean

        Try

            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.INSERT_PA_SAL_MAPPING",
                                           New With {.P_ID = objTitle.ID,
                                                     .P_YEAR = objTitle.YEAR,
                                                     .P_PERIOD_ID = objTitle.PERIOD_ID,
                                                     .P_SALARY_GROUP = objTitle.SALARY_GROUP,
                                                     .P_SALARY_FUND = objTitle.SALARY_FUND,
                                                     .P_SALARY_NAME = objTitle.SALARY_NAME,
                                                     .P_CREATED_BY = log.Username,
                                                     .P_CREATED_DATE = Date.Now,
                                                     .P_CREATED_LOG = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyListSalaries(ByVal objTitle As PAListSalariesDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_LISTSALARIES With {.ID = objTitle.ID}
        Dim old_Name As String
        Try
            objTitleData = (From p In Context.PA_LISTSALARIES Where p.ID = objTitleData.ID).SingleOrDefault
            old_Name = objTitleData.COL_NAME
            objTitleData.TYPE_PAYMENT = objTitle.TYPE_PAYMENT
            objTitleData.COL_NAME = objTitle.COL_NAME
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.COL_INDEX = objTitle.COL_INDEX
            objTitleData.IS_VISIBLE = objTitle.IS_VISIBLE
            objTitleData.IS_INPUT = objTitle.IS_INPUT
            objTitleData.IS_CALCULATE = objTitle.IS_CALCULATE
            objTitleData.IS_IMPORT = objTitle.IS_IMPORT
            objTitleData.INPUT_FORMULER = objTitle.INPUT_FORMULER
            objTitleData.IMPORT_TYPE_ID = objTitle.IMPORT_TYPE_ID
            objTitleData.COL_CODE = objTitle.COL_CODE
            objTitleData.IS_WORKDAY = objTitle.IS_WORKDAY
            objTitleData.IS_SUMDAY = objTitle.IS_SUMDAY
            objTitleData.IS_WORKARISING = objTitle.IS_WORKARISING
            objTitleData.IS_SUMARISING = objTitle.IS_SUMARISING
            objTitleData.IS_PAYBACK = objTitle.IS_PAYBACK
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.GROUP_TYPE = objTitle.GROUP_TYPE_ID
            objTitleData.OBJ_SAL_ID = objTitle.OBJ_SAL_ID
            If objTitle.IS_IMPORT = -1 Then
                Using cls As New DataAccess.NonQueryData
                    cls.ExecuteStore("PKG_PA_SETTING.ADD_COL_SALARY_IMPORT_NEW",
                                               New With {.COL_NAME = objTitle.COL_NAME,
                                                         .COL_TYPE = objTitle.DATA_TYPE})
                End Using
            End If

            ' Kiểm tra khác đối tương lương tháng
            Dim count = (From p In Context.PA_OBJECT_SALARY
                         From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.GROUP_SAL)
                         Where p.ID = objTitle.OBJ_SAL_ID And o.CODE <> "LUONGTHANG").Count

            If count > 0 Then
                Using cls As New DataAccess.NonQueryData
                    cls.ExecuteStore("PKG_PA_SETTING.ADD_COL_SALARY_ACHIEVEMENT",
                                               New With {.COL_NAME = objTitle.COL_NAME,
                                                         .COL_TYPE = objTitle.DATA_TYPE})
                End Using
            End If

            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveListSalaries(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_LISTSALARIES)
        Try
            lstData = (From p In Context.PA_LISTSALARIES Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).STATUS = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Update status deleted, khong xoa khoi he thong
    ''' </summary>
    ''' <param name="lstID"></param>
    ''' <param name="log"></param>
    ''' <param name="bActive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteListSalariesStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        Dim lstData As List(Of PA_LISTSALARIES)
        Try
            lstData = (From p In Context.PA_LISTSALARIES Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteListSalaries(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstListSalariesData As List(Of PA_LISTSALARIES)
        Try
            lstListSalariesData = (From p In Context.PA_LISTSALARIES Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstListSalariesData.Count - 1
                Context.PA_LISTSALARIES.DeleteObject(lstListSalariesData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
    Public Function GetListSal(ByVal _filter As PAListSalDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "COL_INDEX ASC, CREATED_DATE desc") As List(Of PAListSalDTO)
        Try
            Dim lst = (From p In Context.PA_LISTSAL
                       From o In Context.OT_OTHER_LIST.Where(Function(o) o.ID = p.DATA_TYPE).DefaultIfEmpty()
                       From sal_type In Context.OT_OTHER_LIST.Where(Function(sty) sty.ID = p.GROUP_TYPE).DefaultIfEmpty()
                       Select New PAListSalDTO With {
                                                    .ID = p.ID,
                                                    .COL_NAME = p.COL_NAME,
                                                    .NAME_VN = p.NAME_VN,
                                                    .NAME_EN = p.NAME_EN,
                                                    .DATA_TYPE = p.DATA_TYPE,
                                                    .COL_INDEX = p.COL_INDEX,
                                                    .STATUS = If(p.STATUS = "A", "Áp dụng", "Ngừng áp dụng"),
                                                    .CREATED_DATE = p.CREATED_DATE,
                                                    .REMARK = p.REMARK,
                                                    .DATA_TYPE_NAME = If(p.DATA_TYPE = "1", "Kiểu số", If(p.DATA_TYPE = "2", "Kiểu ngày", "Kiểu chữ")),
                                                    .GROUP_TYPE = p.GROUP_TYPE,
                                                    .GROUP_TYPE_NAME = sal_type.NAME_VN
                                                })

            If _filter.COL_NAME <> "" Then
                lst = lst.Where(Function(p) p.COL_NAME.ToUpper.Contains(_filter.COL_NAME.ToUpper))
            End If
            If _filter.NAME_VN <> "" Then
                lst = lst.Where(Function(p) p.NAME_VN.ToUpper.Contains(_filter.NAME_VN.ToUpper))
            End If
            If _filter.DATA_TYPE <> 0 Then
                lst = lst.Where(Function(p) p.DATA_TYPE = _filter.DATA_TYPE)
            End If
            If _filter.DATA_TYPE_NAME <> 0 Then
                lst = lst.Where(Function(p) p.DATA_TYPE_NAME = _filter.DATA_TYPE_NAME)
            End If
            If _filter.REMARK <> "" Then
                lst = lst.Where(Function(p) p.REMARK.ToUpper.Contains(_filter.REMARK.ToUpper))
            End If
            If _filter.GROUP_TYPE <> 0 Then
                lst = lst.Where(Function(p) p.GROUP_TYPE = _filter.GROUP_TYPE)
            End If
            If _filter.GROUP_TYPE_NAME <> 0 Then
                lst = lst.Where(Function(p) p.GROUP_TYPE_NAME = _filter.GROUP_TYPE_NAME)
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
    Public Function InsertListSal(ByVal objTitle As PAListSalDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_LISTSAL
        Dim iCount As Integer = 0

        Try

            objTitleData.COL_NAME = objTitle.COL_NAME
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.COL_INDEX = objTitle.COL_INDEX
            objTitleData.STATUS = objTitle.STATUS
            objTitleData.COL_CODE = objTitle.COL_CODE
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.GROUP_TYPE = objTitle.GROUP_TYPE
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.ADD_COL_SALARY_NEW",
                                           New With {.COL_NAME = objTitle.COL_NAME,
                                                     .COL_TYPE = objTitle.DATA_TYPE})
            End Using
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_LISTSAL.EntitySet.Name)
            Context.PA_LISTSAL.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyListSal(ByVal objTitle As PAListSalDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_LISTSAL With {.ID = objTitle.ID}
        Dim old_Name As String
        Dim old_Type As Decimal?
        Try
            objTitleData = (From p In Context.PA_LISTSAL Where p.ID = objTitleData.ID).SingleOrDefault
            old_Name = objTitleData.COL_NAME
            old_Type = objTitleData.DATA_TYPE
            objTitleData.COL_NAME = objTitle.COL_NAME
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.COL_INDEX = objTitle.COL_INDEX
            objTitleData.COL_CODE = objTitle.COL_CODE
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.GROUP_TYPE = objTitle.GROUP_TYPE
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_PA_SETTING.EDIT_COL_SALARY_NEW",
                                           New With {.COL_NAME = old_Name,
                                                     .COL_NAME_NEW = objTitleData.COL_NAME,
                                                     .DATA_TYPE = old_Type,
                                                     .DATA_TYPE_NEW = objTitleData.DATA_TYPE})
            End Using
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveListSal(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_LISTSAL)
        Try
            lstData = (From p In Context.PA_LISTSAL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).STATUS = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function


    Public Function DeleteListSal(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstListSalData As List(Of PA_LISTSAL)
        Try
            lstListSalData = (From p In Context.PA_LISTSAL Where lstID.Contains(p.ID)).ToList
            Using cls As New DataAccess.NonQueryData
                For index = 0 To lstListSalData.Count - 1
                    cls.ExecuteStore("PKG_PA_SETTING.DELETE_COL_SALARY",
                                            New With {.COL_NAME = lstListSalData(index).COL_NAME})
                Next
            End Using
            For index = 0 To lstListSalData.Count - 1
                Context.PA_LISTSAL.DeleteObject(lstListSalData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

#End Region

#Region "lunch list : Đơn giá tiền ăn trưa"

    Public Function GetPriceLunchList(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "EFFECT_DATE desc") As List(Of ATPriceLunchDTO)

        Try
            Dim query = From p In Context.PA_PRICE_LUNCH

            Dim lst = query.Select(Function(p) New ATPriceLunchDTO With {
                                       .ID = p.ID,
                                       .PRICE = p.PRICE,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .EXPIRE_DATE = p.EXPIRE_DATE,
                                       .REMARK = p.REMARK,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY})


            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function GetPriceLunch(ByVal year As Decimal) As List(Of ATPriceLunchDTO)
        Try
            Dim query = From p In Context.PA_PRICE_LUNCH Where p.ID = year Order By p.ID Ascending, p.EFFECT_DATE Ascending
            Dim Period = query.Select(Function(p) New ATPriceLunchDTO With {
                                       .ID = p.ID,
                                       .PRICE = p.PRICE,
                                       .EFFECT_DATE = p.EFFECT_DATE,
                                       .EXPIRE_DATE = p.EXPIRE_DATE,
                                       .REMARK = p.REMARK,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .CREATED_BY = p.CREATED_BY})


            Return Period.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objPeriodData As New PA_PRICE_LUNCH
        Dim objOrgPeriodData As PA_ORG_LUNCH
        Try
            objPeriodData.ID = Utilities.GetNextSequence(Context, Context.PA_PRICE_LUNCH.EntitySet.Name)
            objPeriodData.PRICE = objPeriod.PRICE
            objPeriodData.EFFECT_DATE = objPeriod.EFFECT_DATE
            objPeriodData.EXPIRE_DATE = objPeriod.EXPIRE_DATE
            objPeriodData.REMARK = objPeriod.REMARK
            Context.PA_PRICE_LUNCH.AddObject(objPeriodData)
            Context.SaveChanges(log)
            If objPeriodData.ID > 0 Then
                For Each obj As PA_ORG_LUNCH In objOrgPeriod
                    objOrgPeriodData = New PA_ORG_LUNCH
                    objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.PA_ORG_LUNCH.EntitySet.Name)
                    objOrgPeriodData.ORG_ID = obj.ORG_ID
                    objOrgPeriodData.LUNCH_ID = objPeriodData.ID
                    Context.PA_ORG_LUNCH.AddObject(objOrgPeriodData)
                    Context.SaveChanges(log)
                Next
            End If
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateATPriceLunch(ByVal _validate As ATPriceLunchDTO) As Boolean
        Try
            If _validate.ID <> 0 Then
                If _validate.ID <> 0 Then
                    Dim query = (From p In Context.PA_ORG_LUNCH Where p.LUNCH_ID = _validate.ID).ToList
                    If query.Count > 0 Then
                        Return False
                    Else
                        Return True
                    End If
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateATPriceLunchOrg(ByVal _validate As ATPriceLunchDTO)
        Dim query
        Try
            If _validate.EFFECT_DATE IsNot Nothing And _validate.EXPIRE_DATE IsNot Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_PRICE_LUNCH
                             From o In Context.PA_ORG_LUNCH.Where(Function(F) F.LUNCH_ID = p.ID).DefaultIfEmpty
                             Where (_validate.EFFECT_DATE <= p.EXPIRE_DATE And _validate.EXPIRE_DATE >= p.EFFECT_DATE) _
                             And o.ORG_ID = _validate.ORG_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_PRICE_LUNCH
                             From o In Context.PA_ORG_LUNCH.Where(Function(F) F.LUNCH_ID = p.ID).DefaultIfEmpty
                             Where (_validate.EFFECT_DATE <= p.EXPIRE_DATE And _validate.EXPIRE_DATE >= p.EFFECT_DATE) _
                             And o.ORG_ID = _validate.ORG_ID).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyPriceLunch(ByVal objPeriod As ATPriceLunchDTO, ByVal objOrgPeriod As List(Of PA_ORG_LUNCH), ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objPeriodData As New PA_PRICE_LUNCH With {.ID = objPeriod.ID}
        Dim objOrgPeriodData As PA_ORG_LUNCH
        Try
            Context.PA_PRICE_LUNCH.Attach(objPeriodData)
            objPeriodData.PRICE = objPeriod.PRICE
            objPeriodData.EFFECT_DATE = objPeriod.EFFECT_DATE
            objPeriodData.EXPIRE_DATE = objPeriod.EXPIRE_DATE
            objPeriodData.REMARK = objPeriod.REMARK
            If objPeriodData.ID > 0 Then

                If objOrgPeriod IsNot Nothing Then
                    Dim objDelete As List(Of PA_ORG_LUNCH) = (From p In Context.PA_ORG_LUNCH Where p.LUNCH_ID = objPeriodData.ID).ToList
                    For Each obj As PA_ORG_LUNCH In objDelete
                        Context.PA_ORG_LUNCH.DeleteObject(obj)
                    Next
                End If

                For Each ObjIns As PA_ORG_LUNCH In objOrgPeriod
                    objOrgPeriodData = New PA_ORG_LUNCH
                    objOrgPeriodData.ID = Utilities.GetNextSequence(Context, Context.PA_ORG_LUNCH.EntitySet.Name)
                    objOrgPeriodData.ORG_ID = ObjIns.ORG_ID
                    objOrgPeriodData.LUNCH_ID = objPeriodData.ID
                    Context.PA_ORG_LUNCH.AddObject(objOrgPeriodData)
                Next
            End If
            Context.SaveChanges(log)
            gID = objPeriodData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeletePriceLunch(ByVal lstPeriod As ATPriceLunchDTO) As Boolean
        Dim objOrgPeriod As List(Of PA_ORG_LUNCH) = (From p In Context.PA_ORG_LUNCH Where p.LUNCH_ID = lstPeriod.ID).ToList
        Dim objPeriod As List(Of PA_PRICE_LUNCH) = (From p In Context.PA_PRICE_LUNCH Where p.ID = lstPeriod.ID).ToList
        Try
            For Each item In objOrgPeriod
                Context.PA_ORG_LUNCH.DeleteObject(item)
            Next
            For Each item In objPeriod
                Context.PA_PRICE_LUNCH.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "CostCenter list"

    Public Function GetCostCenter(ByVal _filter As CostCenterDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS ASC, CREATED_DATE desc") As List(Of CostCenterDTO)

        Try
            Dim query = From p In Context.PA_COST_CENTER
                        Where p.IS_DELETED = 0

            If _filter.CODE <> "" Then
                query = query.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If

            If _filter.NAME <> "" Then
                query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If

            If _filter.ACTFLG IsNot Nothing Then
                query = query.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If

            Dim lst = query.Select(Function(p) New CostCenterDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
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

    ''' <summary>
    ''' Insert data cho Cost Center
    ''' </summary>
    ''' <param name="objCostCenter"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Function InsertCostCenter(ByVal objCostCenter As CostCenterDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objCostCenterData As New PA_COST_CENTER
        Try
            objCostCenterData.ID = Utilities.GetNextSequence(Context, Context.PA_COST_CENTER.EntitySet.Name)
            objCostCenterData.CODE = objCostCenter.CODE.Trim
            objCostCenterData.NAME = objCostCenter.NAME.Trim
            objCostCenterData.REMARK = objCostCenter.REMARK
            objCostCenterData.ORDERS = objCostCenter.ORDERS
            objCostCenterData.ACTFLG = objCostCenter.ACTFLG
            objCostCenterData.IS_DELETED = objCostCenter.IS_DELETED
            Context.PA_COST_CENTER.AddObject(objCostCenterData)
            Context.SaveChanges(log)


            Context.SaveChanges(log)
            gID = objCostCenterData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyCostCenter(ByVal objCostCenter As CostCenterDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objCostCenterData As New PA_COST_CENTER With {.ID = objCostCenter.ID}
        Try
            Context.PA_COST_CENTER.Attach(objCostCenterData)
            objCostCenterData.CODE = objCostCenter.CODE.Trim
            objCostCenterData.NAME = objCostCenter.NAME.Trim
            objCostCenterData.REMARK = objCostCenter.REMARK
            objCostCenterData.ORDERS = objCostCenter.ORDERS

            Context.SaveChanges(log)
            gID = objCostCenterData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateCostCenter(ByVal _validate As CostCenterDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_COST_CENTER
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.IS_DELETED = 0 _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.PA_COST_CENTER
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.IS_DELETED = 0).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ActiveCostCenter(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstCostCenterData As List(Of PA_COST_CENTER)
        Try
            lstCostCenterData = (From p In Context.PA_COST_CENTER Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstCostCenterData.Count - 1
                lstCostCenterData(idx).ACTFLG = bActive
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteCostCenterStatus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        Dim lstData As List(Of PA_COST_CENTER)
        Try
            lstData = (From p In Context.PA_COST_CENTER Where lstID.Contains(p.ID)).ToList
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

    Public Function DeleteCostCenter(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstCostCenterData As List(Of PA_COST_CENTER)
        Try
            lstCostCenterData = (From p In Context.PA_COST_CENTER Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstCostCenterData.Count - 1
                Context.PA_COST_CENTER.DeleteObject(lstCostCenterData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

#End Region

#Region "Org Bonus"
    Public Function GetOrgBonus(ByVal _filter As OrgBonusDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " ORDERS desc") As List(Of OrgBonusDTO)
        Try
            Dim lst = (From p In Context.PA_ORGBONUS
                       Select New OrgBonusDTO With {
                                                    .ID = p.ID,
                                                    .CODE = p.CODE,
                                                    .NAME = p.NAME,
                                                    .ORDERS = p.ORDERS,
                                                    .REMARK = p.REMARK,
                                                    .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                    .CREATED_DATE = p.CREATED_DATE
                                                })

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function InsertOrgBonus(ByVal objTitle As OrgBonusDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_ORGBONUS
        Dim iCount As Integer = 0

        Try


            objTitleData.NAME = objTitle.NAME
            objTitleData.CODE = objTitle.CODE
            objTitleData.ORDERS = objTitle.ORDERS
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_ORGBONUS.EntitySet.Name)
            Context.PA_ORGBONUS.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyOrgBonus(ByVal objTitle As OrgBonusDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_ORGBONUS With {.ID = objTitle.ID}
        Dim old_Name As String
        Try
            objTitleData = (From p In Context.PA_ORGBONUS Where p.ID = objTitleData.ID).SingleOrDefault
            old_Name = objTitleData.NAME
            objTitleData.NAME = objTitle.NAME
            objTitleData.ORDERS = objTitle.ORDERS
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.CODE = objTitle.CODE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveOrgBonus(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_ORGBONUS)
        Try
            lstData = (From p In Context.PA_ORGBONUS Where lstID.Contains(p.ID)).ToList
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


    Public Function DeleteOrgBonus(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstOrgBonusData As List(Of PA_ORGBONUS)
        Try
            lstOrgBonusData = (From p In Context.PA_ORGBONUS Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstOrgBonusData.Count - 1
                Context.PA_ORGBONUS.DeleteObject(lstOrgBonusData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
    Public Function ValidateOrgBonus(ByVal _validate As OrgBonusDTO) As Boolean
        Try
            If _validate.ID <> 0 Then
                If _validate.ID <> 0 Then
                    Dim query = (From p In Context.PA_ORGBONUS Where p.ID <> _validate.ID And p.CODE = _validate.CODE).ToList
                    If query.Count > 0 Then
                        Return False
                    Else
                        Return True
                    End If
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "Payment Sources"
    Public Function GetPaymentSources(ByVal _filter As PaymentSourcesDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " effect_date desc") As List(Of PaymentSourcesDTO)
        Try
            Dim lst = (From p In Context.PA_PAYMENTSOURCES
                       From OT In Context.OT_OTHER_LIST Where OT.ID = p.PAY_TYPE
                       From OT_TYPE In Context.OT_OTHER_LIST_TYPE Where OT_TYPE.ID = OT.TYPE_ID And OT_TYPE.CODE = "PAYMENTSOURCES"
                       Select New PaymentSourcesDTO With {
                                                    .ID = p.ID,
                                                    .YEAR = p.YEAR,
                                                    .NAME = p.NAME,
                                                    .ORDERS = p.ORDERS,
                                                    .PAY_TYPE = p.PAY_TYPE,
                                                    .PAY_TYPE_NAME = OT.NAME_VN,
                                                    .REMARK = p.REMARK,
                                                    .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                        .CREATED_DATE = p.CREATED_DATE
                                                })

            If _filter.YEAR.HasValue Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(f) f.NAME.ToLower().Contains(_filter.NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PAY_TYPE_NAME) Then
                lst = lst.Where(Function(f) f.PAY_TYPE_NAME.ToLower().Contains(_filter.PAY_TYPE_NAME.ToLower()))
            End If
            If _filter.ORDERS.HasValue Then
                lst = lst.Where(Function(f) f.ORDERS = _filter.ORDERS)
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REMARK) Then
                lst = lst.Where(Function(f) f.REMARK.ToLower().Contains(_filter.REMARK.ToLower()))
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
    Public Function InsertPaymentSources(ByVal objTitle As PaymentSourcesDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_PAYMENTSOURCES
        Dim iCount As Integer = 0

        Try


            objTitleData.NAME = objTitle.NAME
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.PAY_TYPE = objTitle.PAY_TYPE
            objTitleData.ORDERS = objTitle.ORDERS
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_PAYMENTSOURCES.EntitySet.Name)
            Context.PA_PAYMENTSOURCES.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyPaymentSources(ByVal objTitle As PaymentSourcesDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_PAYMENTSOURCES With {.ID = objTitle.ID}
        Dim old_Name As String
        Try
            objTitleData = (From p In Context.PA_PAYMENTSOURCES Where p.ID = objTitleData.ID).SingleOrDefault
            old_Name = objTitleData.NAME
            objTitleData.NAME = objTitle.NAME
            objTitleData.ORDERS = objTitle.ORDERS
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.PAY_TYPE = objTitle.PAY_TYPE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActivePaymentSources(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_PAYMENTSOURCES)
        Try
            lstData = (From p In Context.PA_PAYMENTSOURCES Where lstID.Contains(p.ID)).ToList
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


    Public Function DeletePaymentSources(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstPaymentSourcesData As List(Of PA_PAYMENTSOURCES)
        Try
            lstPaymentSourcesData = (From p In Context.PA_PAYMENTSOURCES Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstPaymentSourcesData.Count - 1
                Context.PA_PAYMENTSOURCES.DeleteObject(lstPaymentSourcesData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

#End Region

#Region "Work Factor"
    Public Function GetWorkFactor(ByVal _filter As WorkFactorDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = " EFFECT_DATE desc") As List(Of WorkFactorDTO)
        Try
            Dim lst = (From p In Context.PA_WORKFACTOR
                       Select New WorkFactorDTO With {
                                                    .ID = p.ID,
                                                    .CODE = p.CODE,
                                                    .XEPLOAI = p.XEPLOAI,
                                                    .FACTOR = p.FACTOR,
                                                    .BONUSE = p.BONUSE,
                                                    .effect_date = p.EFFECT_DATE,
                                                    .REMARK = p.REMARK,
                                                    .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                                    .CREATED_DATE = p.CREATED_DATE
                                                })

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function InsertWorkFactor(ByVal objTitle As WorkFactorDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_WORKFACTOR
        Dim iCount As Integer = 0

        Try


            objTitleData.XEPLOAI = objTitle.XEPLOAI
            objTitleData.CODE = objTitle.CODE
            objTitleData.FACTOR = objTitle.FACTOR
            objTitleData.BONUSE = objTitle.BONUSE
            objTitleData.EFFECT_DATE = objTitle.effect_date
            objTitleData.REMARK = objTitle.REMARK
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_WORKFACTOR.EntitySet.Name)
            Context.PA_WORKFACTOR.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyWorkFactor(ByVal objTitle As WorkFactorDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_WORKFACTOR With {.ID = objTitle.ID}
        Dim old_Name As String
        Try
            objTitleData = (From p In Context.PA_WORKFACTOR Where p.ID = objTitleData.ID).SingleOrDefault
            old_Name = objTitleData.XEPLOAI
            objTitleData.XEPLOAI = objTitle.XEPLOAI
            objTitleData.FACTOR = objTitle.FACTOR
            objTitleData.BONUSE = objTitle.BONUSE
            objTitleData.EFFECT_DATE = objTitle.effect_date
            objTitleData.REMARK = objTitle.REMARK
            'objTitleData.ACTFLG = "A"
            objTitleData.CODE = objTitle.CODE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveWorkFactor(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_WORKFACTOR)
        Try
            lstData = (From p In Context.PA_WORKFACTOR Where lstID.Contains(p.ID)).ToList
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


    Public Function DeleteWorkFactor(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstWorkFactorData As List(Of PA_WORKFACTOR)
        Try
            lstWorkFactorData = (From p In Context.PA_WORKFACTOR Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstWorkFactorData.Count - 1
                Context.PA_WORKFACTOR.DeleteObject(lstWorkFactorData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
    Public Function ValidateWorkFactor(ByVal _validate As WorkFactorDTO) As Boolean
        Try
            If _validate.ID <> 0 Then
                If _validate.ID <> 0 Then
                    Dim query = (From p In Context.PA_WORKFACTOR Where p.ID <> _validate.ID And p.XEPLOAI = _validate.XEPLOAI).ToList
                    If query.Count > 0 Then
                        Return False
                    Else
                        Return True
                    End If
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

#Region "SalaryFund "

    Public Function GetSalaryFundByID(ByVal _filter As PASalaryFundDTO) As PASalaryFundDTO
        Try
            Dim query = From p In Context.PA_SALARY_FUND
                        Select New PASalaryFundDTO With {
                            .ID = p.ID,
                            .ORG_ID = p.ORG_ID,
                            .YEAR = p.YEAR,
                            .MONTH = p.MONTH,
                            .SAL_ALLOWANCE = p.SAL_ALLOWANCE,
                            .SAL_HARD = p.SAL_HARD,
                            .SAL_OTHER = p.SAL_OTHER,
                            .SAL_SOFT = p.SAL_SOFT,
                            .SAL_TOTAL = p.SAL_TOTAL,
                            .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.YEAR.HasValue Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
            End If

            If _filter.MONTH.HasValue Then
                lst = lst.Where(Function(f) f.MONTH = _filter.MONTH)
            End If

            If _filter.ORG_ID.HasValue Then
                lst = lst.Where(Function(f) f.ORG_ID = _filter.ORG_ID)

            End If

            Return lst.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function UpdateSalaryFund(ByVal objSalaryFund As PASalaryFundDTO,
                                    ByVal log As UserLog) As Boolean
        Dim objSalaryFundData As New PA_SALARY_FUND
        Try
            objSalaryFundData = (From p In Context.PA_SALARY_FUND
                                 Where p.ORG_ID = objSalaryFund.ORG_ID And
                                 p.YEAR = objSalaryFund.YEAR And
                                 p.MONTH = objSalaryFund.MONTH).FirstOrDefault

            If objSalaryFundData Is Nothing Then
                objSalaryFundData = New PA_SALARY_FUND
                objSalaryFundData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_FUND.EntitySet.Name)
                objSalaryFundData.YEAR = objSalaryFund.YEAR
                objSalaryFundData.MONTH = objSalaryFund.MONTH
                objSalaryFundData.ORG_ID = objSalaryFund.ORG_ID
                objSalaryFundData.SAL_TOTAL = objSalaryFund.SAL_TOTAL
                objSalaryFundData.SAL_SOFT = objSalaryFund.SAL_SOFT
                objSalaryFundData.SAL_OTHER = objSalaryFund.SAL_OTHER
                objSalaryFundData.SAL_HARD = objSalaryFund.SAL_HARD
                objSalaryFundData.SAL_ALLOWANCE = objSalaryFund.SAL_ALLOWANCE
                Context.PA_SALARY_FUND.AddObject(objSalaryFundData)
            Else
                objSalaryFundData.SAL_TOTAL = objSalaryFund.SAL_TOTAL
                objSalaryFundData.SAL_SOFT = objSalaryFund.SAL_SOFT
                objSalaryFundData.SAL_OTHER = objSalaryFund.SAL_OTHER
                objSalaryFundData.SAL_HARD = objSalaryFund.SAL_HARD
                objSalaryFundData.SAL_ALLOWANCE = objSalaryFund.SAL_ALLOWANCE
            End If
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

#End Region

#Region "TitleCost "

    Public Function GetTitleCost(ByVal _filter As PATitleCostDTO,
                                   ByVal PageIndex As Integer,
                                    ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PATitleCostDTO)
        Try
            Dim query = From p In Context.PA_TITLE_COST
                        From t In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID)
                        Select New PATitleCostDTO With {
                                        .ID = p.ID,
                                        .TITLE_ID = p.TITLE_ID,
                                        .TITLE_NAME = t.NAME_VN,
                                        .SAL_BASIC = p.SAL_BASIC,
                                        .SAL_INS = p.SAL_INS,
                                        .SAL_MOBILE = p.SAL_MOBILE,
                                        .SAL_OTHER = p.SAL_OTHER,
                                        .SAL_RICE = p.SAL_RICE,
                                        .SAL_SOFT = p.SAL_SOFT,
                                        .SAL_TOTAL = p.SAL_TOTAL,
                                        .EFFECT_DATE = p.EFFECT_DATE,
                                        .EXPIRE_DATE = p.EXPIRE_DATE,
                                        .CREATED_DATE = p.CREATED_DATE}

            Dim lst = query

            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.EFFECT_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If _filter.EXPIRE_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If

            If _filter.SAL_BASIC.HasValue Then
                lst = lst.Where(Function(f) f.SAL_BASIC = _filter.SAL_BASIC)
            End If

            If _filter.SAL_INS.HasValue Then
                lst = lst.Where(Function(f) f.SAL_INS = _filter.SAL_INS)
            End If

            If _filter.SAL_MOBILE.HasValue Then
                lst = lst.Where(Function(f) f.SAL_MOBILE = _filter.SAL_MOBILE)
            End If

            If _filter.SAL_RICE.HasValue Then
                lst = lst.Where(Function(f) f.SAL_RICE = _filter.SAL_RICE)
            End If

            If _filter.SAL_OTHER.HasValue Then
                lst = lst.Where(Function(f) f.SAL_OTHER = _filter.SAL_OTHER)
            End If

            If _filter.SAL_SOFT.HasValue Then
                lst = lst.Where(Function(f) f.SAL_SOFT = _filter.SAL_SOFT)
            End If

            If _filter.SAL_TOTAL.HasValue Then
                lst = lst.Where(Function(f) f.SAL_TOTAL = _filter.SAL_TOTAL)
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

    Public Function InsertTitleCost(ByVal objTitle As PATitleCostDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_TITLE_COST
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_TITLE_COST.EntitySet.Name)
            objTitleData.TITLE_ID = objTitle.TITLE_ID
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.SAL_TOTAL = objTitle.SAL_TOTAL
            objTitleData.SAL_SOFT = objTitle.SAL_SOFT
            objTitleData.SAL_OTHER = objTitle.SAL_OTHER
            objTitleData.SAL_BASIC = objTitle.SAL_BASIC
            objTitleData.SAL_INS = objTitle.SAL_INS
            objTitleData.SAL_MOBILE = objTitle.SAL_MOBILE
            objTitleData.SAL_RICE = objTitle.SAL_RICE
            Context.PA_TITLE_COST.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyTitleCost(ByVal objTitle As PATitleCostDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_TITLE_COST With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.PA_TITLE_COST Where p.ID = objTitleData.ID).FirstOrDefault
            objTitleData.TITLE_ID = objTitle.TITLE_ID
            objTitleData.EFFECT_DATE = objTitle.EFFECT_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.SAL_TOTAL = objTitle.SAL_TOTAL
            objTitleData.SAL_SOFT = objTitle.SAL_SOFT
            objTitleData.SAL_OTHER = objTitle.SAL_OTHER
            objTitleData.SAL_BASIC = objTitle.SAL_BASIC
            objTitleData.SAL_INS = objTitle.SAL_INS
            objTitleData.SAL_MOBILE = objTitle.SAL_MOBILE
            objTitleData.SAL_RICE = objTitle.SAL_RICE
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeleteTitleCost(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstTitleCostData As List(Of PA_TITLE_COST)
        Try
            lstTitleCostData = (From p In Context.PA_TITLE_COST Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstTitleCostData.Count - 1
                Context.PA_TITLE_COST.DeleteObject(lstTitleCostData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#End Region

#Region "Get List Manning"
    Public Function LoadComboboxListMannName(ByVal org_id As Integer, ByVal year As Integer) As DataTable
        Try
            'Dim query = From p In Context.PA_SALARY_FUND
            '            Select New PASalaryFundDTO With {
            '                .ID = p.ID,
            '                .ORG_ID = p.ORG_ID,
            '                .YEAR = p.YEAR,
            '                .MONTH = p.MONTH,
            '                .SAL_ALLOWANCE = p.SAL_ALLOWANCE,
            '                .SAL_HARD = p.SAL_HARD,
            '                .SAL_OTHER = p.SAL_OTHER,
            '                .SAL_SOFT = p.SAL_SOFT,
            '                .SAL_TOTAL = p.SAL_TOTAL,
            '                .CREATED_DATE = p.CREATED_DATE}

            'Dim lst = query
            'Return lst
            Dim listMannName As DataTable
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_RECRUITMENT.LOAD_COMBOBOX_LISTMANNINGNAME",
                                                     New With {.P_ORG_ID = org_id,
                                                               .P_YEAR = year,
                                                                .P_CUR = cls.OUT_CURSOR})
                'If Not dtData Is Nothing Or Not dtData.Tables(0) Is Nothing Then
                '    listMannName = dtData.Tables(0)
                'End If
                listMannName = dtData
            End Using
            Return listMannName
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "LoadComboboxListMannName")
            Throw ex
        End Try
    End Function

#End Region

#Region "Validate Combobox"
    Public Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean
        Try
            'Danh sách nhóm ký hiệu lương
            If cbxData.GET_GROUP_TYPE Then
                Dim ID As Decimal = cbxData.LIST_GROUP_TYPE(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And t.CODE = "SAL_TYPE" And p.ID = ID
                                                         Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN,
                                                             .TYPE_ID = p.TYPE_ID
                                                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh loại bảng lương
            If cbxData.GET_TYPE_PAYMENT Then
                Dim ID As Decimal = cbxData.LIST_TYPE_PAYMENT(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And t.CODE = "TYPE_PAYMENT" And p.ID = ID
                                                         Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN,
                                                             .TYPE_ID = p.TYPE_ID
                                                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh sách các đối tượng cư trú
            If cbxData.GET_LIST_RESIDENT Then
                Dim ID As Decimal = cbxData.LIST_LIST_RESIDENT(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And t.CODE = "PA_RESIDENT" And p.ID = ID
                                                         Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN,
                                                             .TYPE_ID = p.TYPE_ID
                                                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            ' Danh sách các khoản tiền 
            If cbxData.GET_LIST_PAYMENT Then
                Dim ID As Decimal = cbxData.LIST_LIST_PAYMENT(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And t.CODE = "PA_LISTPAYMENT" And p.ID = ID
                                                         Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN,
                                                             .TYPE_ID = p.TYPE_ID
                                                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            ' Danh sách các khoản tiền trong bảng lương
            'TYPE_PAYMENT = 4123 là danh mục bảng lương tổng hợp
            If cbxData.GET_LIST_SALARY Then
                Dim ID As Decimal = cbxData.LIST_LIST_SALARY(0).ID
                Dim list As List(Of PAListSalariesDTO) = (From p In Context.PA_LISTSALARIES
                                                          Where p.STATUS = "A" And p.TYPE_PAYMENT = 4123 And p.ID = ID
                                                          Order By p.CREATED_DATE Descending
                                                          Select New PAListSalariesDTO With {
                                                              .ID = p.ID,
                                                              .NAME_EN = p.NAME_EN,
                                                              .NAME_VN = p.NAME_VN
                                                          }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh sách các đối tượng lương(bảng lương)
            If cbxData.GET_OBJECT_PAYMENT Then
                Dim ID As Decimal = cbxData.LIST_OBJECT_PAYMENT(0).ID
                Dim list As List(Of PAObjectSalaryDTO) = (From p In Context.PA_OBJECT_SALARY
                                                          Where p.ACTFLG = "A" And p.ID = ID
                                                          Order By p.CREATED_DATE Descending
                                                          Select New PAObjectSalaryDTO With {
                                                              .ID = p.ID,
                                                              .CODE = p.CODE,
                                                              .NAME_VN = p.NAME_VN,
                                                              .NAME_EN = p.NAME_EN
                                                          }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_SALARY_LEVEL Then
                Dim ID As Decimal = cbxData.LIST_SALARY_LEVEL(0).ID
                Dim list As List(Of SalaryLevelDTO) = (From p In Context.PA_SALARY_LEVEL
                                                       Join o In Context.PA_SALARY_GROUP On p.SAL_GROUP_ID Equals o.ID
                                                       Where p.ACTFLG = "A" And p.ID = ID
                                                       Order By p.NAME.ToUpper
                                                       Select New SalaryLevelDTO With {
                                                           .ID = p.ID,
                                                           .NAME = p.NAME,
                                                           .SAL_GROUP_ID = p.SAL_GROUP_ID
                                                       }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_SALARY_GROUP Then
                Dim ID As Decimal = cbxData.LIST_SALARY_GROUP(0).ID
                Dim list As List(Of SalaryGroupDTO) = (From p In Context.PA_SALARY_GROUP
                                                       Where p.ID = ID
                                                       Order By p.NAME.ToUpper
                                                       Select New SalaryGroupDTO With {
                                                           .ID = p.ID,
                                                           .NAME = p.NAME,
                                                           .EFFECT_DATE = p.EFFECT_DATE
                                                       }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh muc cap chuc danh
            If cbxData.GET_LIST_TITLE_LEVEL Then
                Dim ID As Decimal = cbxData.LIST_LIST_TITLE_LEVEL(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And t.CODE = "HU_TITLE_LEVEL" And p.ID = ID
                                                         Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN,
                                                             .TYPE_ID = p.TYPE_ID
                                                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh loại luong thuong
            If cbxData.GET_INCENTIVE_TYPE Then
                Dim ID As Decimal = cbxData.LIST_INCENTIVE_TYPE(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And t.CODE = "PA_INCENTIVE_TYPE" And p.ID - ID
                                                         Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN,
                                                             .TYPE_ID = p.TYPE_ID
                                                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh loại luong thuong
            If cbxData.GET_IMPORT_TYPE Then
                Dim ID As Decimal = cbxData.LIST_IMPORT_TYPE(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And t.CODE = "IMPORT_TYPE" And p.ID = ID
                                                         Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN,
                                                             .TYPE_ID = p.TYPE_ID
                                                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_SALARY_TYPE Then
                Dim ID As Decimal = cbxData.LIST_SALARY_TYPE(0).ID
                Dim list As List(Of SalaryTypeDTO) = (From p In Context.PA_SALARY_TYPE
                                                      Where p.ID = ID
                                                      Order By p.NAME.ToUpper
                                                      Select New SalaryTypeDTO With {
                                                          .ID = p.ID,
                                                          .NAME = p.NAME
                                                      }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            If cbxData.GET_PAY_TYPE Then
                Dim ID As Decimal = cbxData.LIST_PAY_TYPE(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And t.CODE = "PAYMENTSOURCES" And p.ID = ID
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .CODE = p.CODE,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN,
                                                             .TYPE_ID = p.TYPE_ID
                                                         }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return False
        End Try
    End Function

#End Region

#Region "SaleCommision"
    Public Function GetSaleCommision(ByVal _filter As SaleCommisionDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SaleCommisionDTO)

        Try
            Dim query = From p In Context.PA_SALE_COMMISION

            If _filter.CODE <> "" Then
                query = query.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If

            If _filter.NAME <> "" Then
                query = query.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If
            If _filter.ACTFLG <> "" Then
                Dim searchStr As String
                If _filter.ACTFLG.ToUpper = "ÁP DỤNG" Then
                    searchStr = "A"
                ElseIf _filter.ACTFLG.ToUpper = "NGỪNG ÁP DỤNG" Then
                    searchStr = "I"
                Else
                    searchStr = _filter.ACTFLG.ToUpper
                End If
                query = query.Where(Function(p) p.ACTFLG.ToUpper.Contains(searchStr))
            End If
            Dim lst = query.Select(Function(p) New SaleCommisionDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .ORDERS = p.ORDERS,
                                       .SALE_RATE = p.SALE_RATE,
                                       .SALE_RATE1 = p.SALE_RATE,
                                       .CREATED_DATE = p.CREATED_DATE,
            .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng")
                                       })
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function InsertSaleCommision(ByVal objSaleCommision As SaleCommisionDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objSaleCommisionData As New PA_SALE_COMMISION
        Try
            objSaleCommisionData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_TYPE.EntitySet.Name)
            objSaleCommisionData.CODE = objSaleCommision.CODE.Trim
            objSaleCommisionData.NAME = objSaleCommision.NAME.Trim
            objSaleCommisionData.REMARK = objSaleCommision.REMARK
            objSaleCommisionData.ORDERS = objSaleCommision.ORDERS
            objSaleCommisionData.ACTFLG = objSaleCommision.ACTFLG
            objSaleCommisionData.SALE_RATE = objSaleCommision.SALE_RATE
            Context.PA_SALE_COMMISION.AddObject(objSaleCommisionData)
            Context.SaveChanges(log)
            gID = objSaleCommision.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifySaleCommision(ByVal objSaleCommision As SaleCommisionDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSaleCommisionData As New PA_SALE_COMMISION With {.ID = objSaleCommision.ID}
        Try
            Context.PA_SALE_COMMISION.Attach(objSaleCommisionData)
            objSaleCommisionData.CODE = objSaleCommision.CODE.Trim
            objSaleCommisionData.NAME = objSaleCommision.NAME.Trim
            objSaleCommisionData.REMARK = objSaleCommision.REMARK
            objSaleCommisionData.ORDERS = objSaleCommision.ORDERS
            objSaleCommisionData.SALE_RATE = objSaleCommision.SALE_RATE
            Context.SaveChanges(log)
            gID = objSaleCommisionData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeleteSaleCommision(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstSaleCommsionData As List(Of PA_SALE_COMMISION)
        Try
            lstSaleCommsionData = (From p In Context.PA_SALE_COMMISION Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSaleCommsionData.Count - 1
                Context.PA_SALE_COMMISION.DeleteObject(lstSaleCommsionData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveSaleCommision(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_SALE_COMMISION)
        Try
            lstData = (From p In Context.PA_SALE_COMMISION Where lstID.Contains(p.ID)).ToList
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

#End Region

#Region "PA_SYSTEM_CRITERIA"
    Public Function GetPA_SYSTEM_CRITERIA(ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of PA_SYSTEM_CRITERIADTO)
        Try
            Dim query = From p In Context.PA_SYSTEM_CRITERIA
                        From o In Context.PA_OBJECT_SALARY.Where(Function(f) f.ID = p.OBJ_SAL_ID).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New PA_SYSTEM_CRITERIADTO With {
                                        .ID = p.p.ID,
                                        .CODE = p.p.CODE,
                                        .NAME = p.p.NAME,
                                        .FOMULER = p.p.FOMULER,
                                        .NOTE = p.p.NOTE,
                                        .UNIT = p.p.UNIT,
                                        .OBJ_SAL_ID = p.p.OBJ_SAL_ID,
                                        .OBJ_SAL_NAME = p.o.NAME_VN,
                                        .STATUS = p.p.STATUS,
                                        .DATA_TYPE = p.p.DATA_TYPE,
                                        .DATA_TYPE_NAME = If(p.p.DATA_TYPE = 0, "Kiểu chữ", If(p.p.DATA_TYPE = 1, "Kiểu số", "Kiểu ngày")),
                                        .IS_SALARY = p.p.IS_SALARY
                                     })
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
    Public Function InsertPA_SYSTEM_CRITERIA(ByVal objTitle As PA_SYSTEM_CRITERIADTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_SYSTEM_CRITERIA
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.PA_SYSTEM_CRITERIA.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME = objTitle.NAME
            objTitleData.FOMULER = objTitle.FOMULER
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.UNIT = objTitle.UNIT
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            objTitleData.IS_SALARY = objTitle.IS_SALARY
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.OBJ_SAL_ID = objTitle.OBJ_SAL_ID
            objTitleData.STATUS = "A"
            Context.PA_SYSTEM_CRITERIA.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID



            Dim PA_OBJECT_SALARY = (From p In Context.PA_OBJECT_SALARY
                                    Where p.ID = objTitle.OBJ_SAL_ID).FirstOrDefault

            Using cls As New DataAccess.QueryData

                If PA_OBJECT_SALARY.CODE = "MONTH" Then
                    Dim obj_Add_Cols = New With {.P_TABLE = "PA_PAYROLL_DETAIL", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)

                    'Them vao bang luong:
                    '   +TEMP_CALCULATE
                    obj_Add_Cols = New With {.P_TABLE = "TEMP_CALCULATE", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +TEMP_CALCULATE_SUM
                    obj_Add_Cols = New With {.P_TABLE = "TEMP_CALCULATE_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +PA_PAYROLLSHEET_TEMP
                    obj_Add_Cols = New With {.P_TABLE = "PA_PAYROLLSHEET_TEMP", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +PA_PAYROLLSHEET_SUM
                    obj_Add_Cols = New With {.P_TABLE = "PA_PAYROLLSHEET_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +PA_INCOME_TAX_SUM
                    'obj_Add_Cols = New With {.P_TABLE = "PA_INCOME_TAX_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    'store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)

                End If

                If PA_OBJECT_SALARY.CODE = "AWARD" Then
                    Dim obj_Add_Cols = New With {.P_TABLE = "PA_AWARD_DETAIL", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    'Them vao bang luong:
                    '   +TEMP_CALCULATE
                    obj_Add_Cols = New With {.P_TABLE = "TEMP_CALCULATE", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +TEMP_CALCULATE_SUM
                    obj_Add_Cols = New With {.P_TABLE = "TEMP_CALCULATE_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +PA_PAYROLLSHEET_TEMP
                    obj_Add_Cols = New With {.P_TABLE = "PA_PAYROLLSHEET_TEMP", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +PA_PAYROLLSHEET_SUM
                    obj_Add_Cols = New With {.P_TABLE = "PA_PAYROLLSHEET_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    ''   +PA_INCOME_TAX_SUM
                    'obj_Add_Cols = New With {.P_TABLE = "PA_INCOME_TAX_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    'store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                End If

                If PA_OBJECT_SALARY.CODE = "TAXINCOME" Then
                    Dim obj_Add_Cols = New With {.P_TABLE = "PA_TAXINCOME_DETAIL", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +PA_INCOME_TAX_SUM
                    obj_Add_Cols = New With {.P_TABLE = "PA_INCOME_TAX_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                End If

            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ModifyPA_SYSTEM_CRITERIA(ByVal objTitle As PA_SYSTEM_CRITERIADTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New PA_SYSTEM_CRITERIA With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.PA_SYSTEM_CRITERIA Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE
            objTitleData.NAME = objTitle.NAME
            objTitleData.FOMULER = objTitle.FOMULER
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.UNIT = objTitle.UNIT
            objTitleData.CREATED_BY = objTitle.CREATED_BY
            objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            objTitleData.IS_SALARY = objTitle.IS_SALARY
            objTitleData.DATA_TYPE = objTitle.DATA_TYPE
            objTitleData.OBJ_SAL_ID = objTitle.OBJ_SAL_ID
            Context.SaveChanges(log)
            gID = objTitleData.ID

            Dim PA_OBJECT_SALARY = (From p In Context.PA_OBJECT_SALARY
                                    Where p.ID = objTitle.OBJ_SAL_ID).FirstOrDefault

            Using cls As New DataAccess.QueryData

                If PA_OBJECT_SALARY.CODE = "MONTH" Then
                    Dim obj_Add_Cols = New With {.P_TABLE = "PA_PAYROLL_DETAIL", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    'Them vao bang luong:
                    '   +TEMP_CALCULATE
                    obj_Add_Cols = New With {.P_TABLE = "TEMP_CALCULATE", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +TEMP_CALCULATE_SUM
                    obj_Add_Cols = New With {.P_TABLE = "TEMP_CALCULATE_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +PA_PAYROLLSHEET_TEMP
                    obj_Add_Cols = New With {.P_TABLE = "PA_PAYROLLSHEET_TEMP", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +PA_PAYROLLSHEET_SUM
                    obj_Add_Cols = New With {.P_TABLE = "PA_PAYROLLSHEET_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +PA_INCOME_TAX_SUM
                    'obj_Add_Cols = New With {.P_TABLE = "PA_INCOME_TAX_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    'store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                End If

                If PA_OBJECT_SALARY.CODE = "AWARD" Then
                    Dim obj_Add_Cols = New With {.P_TABLE = "PA_AWARD_DETAIL", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    'Them vao bang luong:
                    '   +TEMP_CALCULATE
                    obj_Add_Cols = New With {.P_TABLE = "TEMP_CALCULATE", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +TEMP_CALCULATE_SUM
                    obj_Add_Cols = New With {.P_TABLE = "TEMP_CALCULATE_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +PA_PAYROLLSHEET_TEMP
                    obj_Add_Cols = New With {.P_TABLE = "PA_PAYROLLSHEET_TEMP", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +PA_PAYROLLSHEET_SUM
                    obj_Add_Cols = New With {.P_TABLE = "PA_PAYROLLSHEET_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +PA_INCOME_TAX_SUM
                    'obj_Add_Cols = New With {.P_TABLE = "PA_INCOME_TAX_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    'store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                End If

                If PA_OBJECT_SALARY.CODE = "TAXINCOME" Then
                    Dim obj_Add_Cols = New With {.P_TABLE = "PA_TAXINCOME_DETAIL", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    Dim store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                    '   +PA_INCOME_TAX_SUM
                    obj_Add_Cols = New With {.P_TABLE = "PA_INCOME_TAX_SUM", .P_COL = objTitle.CODE.Trim.ToUpper, .P_TYPE_COL_ID = objTitle.DATA_TYPE, .P_OUT = cls.OUT_NUMBER}
                    store = cls.ExecuteStore("PKG_ATTENDANCE_LIST.ADD_COLS_TABLE", obj_Add_Cols)
                End If

            End Using

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function


    Public Function DeletePA_SYSTEM_CRITERIA(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstPaymentListData As List(Of PA_SYSTEM_CRITERIA)
        Try
            lstPaymentListData = (From p In Context.PA_SYSTEM_CRITERIA Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstPaymentListData.Count - 1
                Context.PA_SYSTEM_CRITERIA.DeleteObject(lstPaymentListData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ValidateList_PA_SYSTEM_CRITERIA(ByVal _validate As PA_SYSTEM_CRITERIADTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SYSTEM_CRITERIA
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_SYSTEM_CRITERIA
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SYSTEM_CRITERIA
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If

            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function
#End Region

    Public Function GetBrandRate(ByVal _filter As PA_BrandRate_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_BrandRate_DTO)

        Try

            Dim lst = (From p In Context.PA_BRAND_RATE
                       From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.BRAND_ID And f.TYPE_ID = 16).DefaultIfEmpty
                       Select New PA_BrandRate_DTO With {
                           .ID = p.ID,
                           .BRAND_ID = p.BRAND_ID,
                           .BRAND_NAME = o.NAME_VN,
                           .RATE = p.RATE,
                           .EFFECT_DATE = p.EFFECT_DATE,
                           .NOTE = p.NOTE,
                           .IS_DOANHTHU = p.IS_DOANHTHU
                            })

            If _filter.BRAND_NAME <> "" Then
                lst = lst.Where(Function(f) f.BRAND_NAME.ToUpper.Contains(_filter.BRAND_NAME.ToUpper))
            End If
            If _filter.RATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.RATE = _filter.RATE)
            End If
            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(f) f.EFFECT_DATE = _filter.EFFECT_DATE)
            End If
            If _filter.NOTE <> "" Then
                lst = lst.Where(Function(f) f.NOTE.ToUpper.Contains(_filter.NOTE.ToUpper))
            End If

            lst = lst.OrderBy(Function(f) f.ID)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function InsertBrandRate(ByVal obj As PA_BrandRate_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objData As New PA_BRAND_RATE
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_BRAND_RATE.EntitySet.Name)
            objData.BRAND_ID = obj.BRAND_ID
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.RATE = obj.RATE
            objData.NOTE = obj.NOTE
            objData.IS_DOANHTHU = obj.IS_DOANHTHU

            Context.PA_BRAND_RATE.AddObject(objData)
            Context.SaveChanges(log)

            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyBrandRate(ByVal obj As PA_BrandRate_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_BRAND_RATE With {.ID = obj.ID}
        Try
            Context.PA_BRAND_RATE.Attach(objData)
            objData.BRAND_ID = obj.BRAND_ID
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.RATE = obj.RATE
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

    Public Function ValidateBrandRate(ByVal _validate As PA_BrandRate_DTO) As Boolean
        Try
            If _validate.ID IsNot Nothing Then
                Dim query = (From p In Context.PA_BRAND_RATE Where p.BRAND_ID = _validate.BRAND_ID And p.EFFECT_DATE = _validate.EFFECT_DATE And p.ID <> _validate.ID).ToList
                If query.Count > 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Dim query = (From p In Context.PA_BRAND_RATE Where p.BRAND_ID = _validate.BRAND_ID And p.EFFECT_DATE = _validate.EFFECT_DATE).ToList
                If query.Count > 0 Then
                    Return False
                Else
                    Return True
                End If
            End If

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteBrandRate(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of PA_BRAND_RATE)
        Try
            lstData = (From p In Context.PA_BRAND_RATE Where lstID.Contains(p.ID)).ToList

            For index = 0 To lstData.Count - 1
                Context.PA_BRAND_RATE.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

#Region "Thiết lập cửa hàng do AOMs, TOM quản lý"

    Public Function GetPA_AOMS_TOM_MNG(ByVal _filter As PA_AOMS_TOM_MNG_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ID",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_AOMS_TOM_MNG_DTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = 0})
            End Using


            Dim lst = (From q In Context.PA_AOMS_TOM_MNG
                       From s In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = q.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                       From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = q.EMPLOYEE_ID).DefaultIfEmpty
                       From oe In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID).DefaultIfEmpty
                       From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = q.ORG_ID).DefaultIfEmpty
                       From t In Context.HU_TITLE.Where(Function(f) f.ID = e.TITLE_ID).DefaultIfEmpty
                       Select New PA_AOMS_TOM_MNG_DTO With {.ID = q.ID,
                                                .EMPLOYEE_ID = q.EMPLOYEE_ID,
                                                .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                .ORG_ID = e.ORG_ID,
                                                .ORG_NAME = oe.NAME_VN,
                                                .ORG_DESC = oe.DESCRIPTION_PATH,
                                                .ORG_DV_ID = q.ORG_ID,
                                                .ORG_DV_NAME = o.NAME_VN,
                                                .TITLE_ID = e.TITLE_ID,
                                                .TITLE_NAME = t.NAME_VN,
                                                .EFFECT_DATE = q.EFFECT_DATE,
                                                .CREATED_DATE = q.CREATED_DATE,
                                                .EXPIRE_DATE = q.EXPIRE_DATE})
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.ORG_DV_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_DV_NAME.ToUpper.Contains(_filter.ORG_DV_NAME.ToUpper))
            End If

            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
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

    Public Function InsertPA_AOMS_TOM_MNG(ByVal obj As PA_AOMS_TOM_MNG_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_AOMS_TOM_MNG
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_AOMS_TOM_MNG.EntitySet.Name)
            objData.ORG_ID = obj.ORG_DV_ID
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.EXPIRE_DATE = obj.EXPIRE_DATE
            Context.PA_AOMS_TOM_MNG.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyPA_AOMS_TOM_MNG(ByVal obj As PA_AOMS_TOM_MNG_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_AOMS_TOM_MNG With {.ID = obj.ID}
        Try
            Context.PA_AOMS_TOM_MNG.Attach(objData)
            objData.ORG_ID = obj.ORG_DV_ID
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.EXPIRE_DATE = obj.EXPIRE_DATE
            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeletePA_AOMS_TOM_MNG(ByVal lstDelete As List(Of Decimal)) As Boolean
        Dim objData As List(Of PA_AOMS_TOM_MNG) = (From p In Context.PA_AOMS_TOM_MNG Where lstDelete.Contains(p.ID)).ToList
        Try
            For Each item In objData
                Context.PA_AOMS_TOM_MNG.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CheckPA_AOMS_TOMExits(ByVal empID As Decimal, ByVal orgID As Decimal, ByVal pDate As Date, ByVal pID As Decimal?) As Boolean
        'Dim count = (From p In Context.PA_AOMS_TOM_MNG Where p.EMPLOYEE_ID = empID AndAlso p.ORG_ID = orgID AndAlso p.EFFECT_DATE.Value.Year = pDate.Year AndAlso p.EFFECT_DATE.Value.Month = pDate.Month AndAlso p.ID <> pID ).FirstOrDefault
        Dim count = (From p In Context.PA_AOMS_TOM_MNG Where p.EMPLOYEE_ID = empID AndAlso p.ORG_ID = orgID AndAlso p.EFFECT_DATE <= pDate AndAlso p.ID <> pID AndAlso p.EXPIRE_DATE Is Nothing).FirstOrDefault
        If count Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function CheckPA_AOMS_TOMExits_EF_EX(ByVal empID As Decimal, ByVal orgID As Decimal, ByVal pDate As Date, ByVal pID As Decimal?) As Boolean
        'Dim count = (From p In Context.PA_AOMS_TOM_MNG Where p.EMPLOYEE_ID = empID AndAlso p.ORG_ID = orgID AndAlso p.EFFECT_DATE.Value.Year = pDate.Year AndAlso p.EFFECT_DATE.Value.Month = pDate.Month AndAlso p.ID <> pID ).FirstOrDefault
        Dim count = (From p In Context.PA_AOMS_TOM_MNG Where p.EMPLOYEE_ID = empID AndAlso p.ORG_ID = orgID AndAlso p.EFFECT_DATE <= pDate AndAlso p.ID <> pID AndAlso p.EXPIRE_DATE >= pDate).FirstOrDefault
        If count Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function GetExportAomsTom() As DataTable
        Try
            Dim query = From p In Context.HU_ORGANIZATION
                        Where p.ACTFLG = "A" Order By p.NAME_VN Ascending
            Dim obj = query.Select(Function(f) New PA_AOMS_TOM_MNG_DTO With
                        {.ID = f.ID,
                         .ORG_NAME = f.NAME_VN,
                         .ORG_DV_CODE = f.CODE
                        }).ToList
            Return obj.ToTable()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function IMPORT_PA_AOMS_TOM_MNG(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PA_BUSINESS.IMPORT_PA_AOMS_TOM_MNG",
                                 New With {.P_DOCXML = P_DOCXML, .P_USER = P_USER})
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function IMPORT_PA_EMP_FORMULER(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PA_SETTING.IMPORT_PA_EMP_FORMULER",
                                 New With {.P_DOCXML = P_DOCXML, .P_USER = P_USER})
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function IMPORT_PA_SETUP_FRAMEWORK_OFFICE(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PA_SETTING.IMPORT_PA_SETUP_FRAMEWORK_OFFICE",
                                 New With {.P_DOCXML = P_DOCXML, .P_USER = P_USER})
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function IMPORT_PA_SETUP_HESOMR_NV_QLCH(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_PA_SETTING.IMPORT_PA_SETUP_HESOMR_NV_QLCH",
                                 New With {.P_DOCXML = P_DOCXML, .P_USER = P_USER})
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

#Region "Thiết lập nhân viên theo công thức lương"
    Public Function GetPA_EMP_FORMULER(ByVal _filter As PA_EMP_FORMULER_DTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ID",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_EMP_FORMULER_DTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = 0})
            End Using


            Dim lst = (From q In Context.PA_EMP_FORMULER
                       From o In Context.PA_OBJECT_SALARY.Where(Function(f) f.ID = q.OBJ_SAL_ID).DefaultIfEmpty
                       From s In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = q.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                       From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = q.EMPLOYEE_ID).DefaultIfEmpty
                       From oe In Context.HU_ORGANIZATION.Where(Function(f) f.ID = q.ORG_ID).DefaultIfEmpty
                       From t In Context.HU_TITLE.Where(Function(f) f.ID = q.TITLE_ID).DefaultIfEmpty
                       From g In Context.PA_FORMULER_GROUP.Where(Function(f) f.ID = q.FOR_GROUP_ID).DefaultIfEmpty
                       From og In Context.OT_OTHER_LIST.Where(Function(f) f.ID = q.GROUP_EMPLOYEE_ID).DefaultIfEmpty
                       From obj In Context.OT_OTHER_LIST.Where(Function(f) f.ID = q.OBJECT_EMPLOYEE).DefaultIfEmpty
                       Select New PA_EMP_FORMULER_DTO With {.ID = q.ID,
                                                .EMPLOYEE_ID = q.EMPLOYEE_ID,
                                                .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                .ORG_ID = q.ORG_ID,
                                                .ORG_NAME = oe.NAME_VN,
                                                .ORG_DESC = oe.DESCRIPTION_PATH,
                                                .OBJ_SAL_ID = q.OBJ_SAL_ID,
                                                .OBJ_SAL_NAME = o.NAME_VN,
                                                .TITLE_ID = q.TITLE_ID,
                                                .TITLE_NAME = t.NAME_VN,
                                                .EFFECT_DATE = q.EFFECT_DATE,
                                                .EXPIRE_DATE = q.EXPIRE_DATE,
                                                .CREATED_DATE = q.CREATED_DATE,
                                                .FOR_GROUP_ID = q.FOR_GROUP_ID,
                                                .FOR_GROUP_NAME = g.NAME_VN,
                                                .GROUP_EMPLOYEE_ID = q.GROUP_EMPLOYEE_ID,
                                                .GROUP_EMPLOYEE_NAME = og.NAME_VN,
                                                .OBJECT_EMPLOYEE = q.OBJECT_EMPLOYEE,
                                                .OBJECT_EMPLOYEE_NAME = obj.NAME_VN})
            If _filter.EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_NAME.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                lst = lst.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If

            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.OBJECT_EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJECT_EMPLOYEE_NAME.ToUpper.Contains(_filter.OBJECT_EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.FOR_GROUP_NAME <> "" Then
                lst = lst.Where(Function(p) p.FOR_GROUP_NAME.ToUpper.Contains(_filter.FOR_GROUP_NAME.ToUpper))
            End If

            If _filter.GROUP_EMPLOYEE_NAME <> "" Then
                lst = lst.Where(Function(p) p.GROUP_EMPLOYEE_NAME.ToUpper.Contains(_filter.GROUP_EMPLOYEE_NAME.ToUpper))
            End If

            If _filter.OBJ_SAL_NAME <> "" Then
                lst = lst.Where(Function(p) p.OBJ_SAL_NAME.ToUpper.Contains(_filter.OBJ_SAL_NAME.ToUpper))
            End If

            If _filter.TITLE_NAME <> "" Then
                lst = lst.Where(Function(p) p.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If

            If _filter.EFFECT_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EFFECT_DATE = _filter.EFFECT_DATE)
            End If

            If _filter.EXPIRE_DATE IsNot Nothing Then
                lst = lst.Where(Function(p) p.EXPIRE_DATE = _filter.EXPIRE_DATE)
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

    Public Function GetObj_Sal() As List(Of PAObjectSalaryDTO)
        Dim lst = (From q In Context.PA_OBJECT_SALARY
                   From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = q.GROUP_SAL)
                   Where o.CODE <> "LUONGTHANG" And q.ACTFLG = "A"
                   Select New PAObjectSalaryDTO With {.ID = q.ID,
                                                      .NAME_VN = q.NAME_VN})

        Return lst.ToList
    End Function

    Public Function GetFORMULER_GROUP(ByVal objSalID As Decimal) As List(Of PAObjectSalaryDTO)
        Dim lst = (From q In Context.PA_FORMULER_GROUP
                   Where q.OBJ_SAL_ID = objSalID
                   Select New PAObjectSalaryDTO With {.ID = q.ID,
                                                      .NAME_VN = q.NAME_VN})

        Return lst.ToList
    End Function

    Public Function GETGROUP_EMPLOYEE_ID(ByVal titleID As Decimal) As Decimal?
        Dim lst = (From q In Context.HU_TITLE Where q.ID = titleID Select q.GROUP_EMPLOYEE_ID).SingleOrDefault

        Return lst
    End Function

    Public Function InsertPA_EMP_FORMULER(ByVal obj As PA_EMP_FORMULER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_EMP_FORMULER
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_EMP_FORMULER.EntitySet.Name)
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.ORG_ID = obj.ORG_ID
            objData.TITLE_ID = obj.TITLE_ID
            objData.OBJ_SAL_ID = obj.OBJ_SAL_ID
            objData.FOR_GROUP_ID = obj.FOR_GROUP_ID
            objData.GROUP_EMPLOYEE_ID = obj.GROUP_EMPLOYEE_ID
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.EXPIRE_DATE = obj.EXPIRE_DATE
            objData.OBJECT_EMPLOYEE = obj.OBJECT_EMPLOYEE

            Context.PA_EMP_FORMULER.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function
    Public Function ModifyPA_EMP_FORMULER(ByVal obj As PA_EMP_FORMULER_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_EMP_FORMULER With {.ID = obj.ID}
        Try
            Context.PA_EMP_FORMULER.Attach(objData)
            objData.EMPLOYEE_ID = obj.EMPLOYEE_ID
            objData.ORG_ID = obj.ORG_ID
            objData.TITLE_ID = obj.TITLE_ID
            objData.OBJ_SAL_ID = obj.OBJ_SAL_ID
            objData.FOR_GROUP_ID = obj.FOR_GROUP_ID
            objData.GROUP_EMPLOYEE_ID = obj.GROUP_EMPLOYEE_ID
            objData.EFFECT_DATE = obj.EFFECT_DATE
            objData.EXPIRE_DATE = obj.EXPIRE_DATE
            objData.OBJECT_EMPLOYEE = obj.OBJECT_EMPLOYEE
            Context.SaveChanges(log)

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeletePA_EMP_FORMULER(ByVal lstDelete As List(Of Decimal)) As Boolean
        Dim objData As List(Of PA_EMP_FORMULER) = (From p In Context.PA_EMP_FORMULER Where lstDelete.Contains(p.ID)).ToList
        Try
            For Each item In objData
                Context.PA_EMP_FORMULER.DeleteObject(item)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function CheckPA_EMP_FORMULERExits(ByVal empID As Decimal, ByVal orgID As Decimal, ByVal TitleID As Decimal, ByVal groupTitleID As Decimal, ByVal formulerID As Decimal, ByVal pDate As Date, ByVal pID As Decimal?) As Boolean
        Dim count = (From p In Context.PA_EMP_FORMULER Where p.EMPLOYEE_ID = empID AndAlso p.ORG_ID = orgID AndAlso p.EFFECT_DATE = pDate AndAlso p.ID <> pID AndAlso p.TITLE_ID = TitleID AndAlso p.GROUP_EMPLOYEE_ID = groupTitleID AndAlso p.FOR_GROUP_ID = formulerID).FirstOrDefault

        If count Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region



#End Region


    Public Function GetPA_SUM_CH_TOM(ByVal _filter As PA_SumCHTomDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ID",
                                        Optional ByVal log As UserLog = Nothing) As List(Of PA_SumCHTomDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username.ToUpper,
                                           .P_ORGID = _filter.ORG_ID,
                                           .P_ISDISSOLVE = 0})
            End Using


            Dim lst = (From q In Context.PA_SUM_CH_TOM
                       From s In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = q.ORG_ID And f.USERNAME.ToUpper = log.Username.ToUpper)
                       From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = q.ORG_ID).DefaultIfEmpty
                       Where q.PERIOD_ID = _filter.PERIOD_ID
                       Select New PA_SumCHTomDTO With {.ID = q.ID,
                                                .ORG_ID = q.ORG_ID,
                                                .ORG_NAME = o.NAME_VN,
                                                .TARGET_DT = q.TARGET_DT,
                                                .DTTD_CH = q.DTTD_CH,
                                                .TLDT_CH = q.TLDT_CH,
                                                .ORG_DESC = o.DESCRIPTION_PATH,
                                                .RATE_90D = q.RATE_90D,
                                                .RATE_SPSG = q.RATE_SPSG,
                                                .SLBILL_NONMEMBER = q.SLBILL_NONMEMBER,
                                                .SLBILL_NEWMEMBER = q.SLBILL_NEWMEMBER,
                                                .SLBILL_TARGET = q.SLBILL_TARGET,
                                                .SLBILL_TD = q.SLBILL_TD,
                                                .CREATED_DATE = q.CREATED_DATE,
                                                .RR6_TARGET = q.RR6_TARGET,
                                                .SLTV = q.SLTV,
                                                .SLTV_6MONTH = q.SLTV_6MONTH,
                                                .RR6_SLTV = q.RR6_SLTV,
                                                .TILE_SLTV = q.TILE_SLTV,
                                                .MBS_TARGET = q.MBS_TARGET,
                                                .MBS_TD = q.MBS_TD,
                                                .PERIOD_ID = q.PERIOD_ID,
                                                .SLKH_MEMBER_YEAR = q.SLKH_MEMBER_YEAR,
                                                .SLKH_RETURN_YEAR = q.SLKH_RETURN_YEAR
                                                      })
            If _filter.ORG_NAME <> "" Then
                lst = lst.Where(Function(p) p.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If

            If _filter.TARGET_DT IsNot Nothing Then
                lst = lst.Where(Function(p) p.TARGET_DT = _filter.TARGET_DT)
            End If

            If _filter.SLKH_MEMBER_YEAR IsNot Nothing Then
                lst = lst.Where(Function(p) p.SLKH_MEMBER_YEAR = _filter.SLKH_MEMBER_YEAR)
            End If

            If _filter.SLKH_RETURN_YEAR IsNot Nothing Then
                lst = lst.Where(Function(p) p.SLKH_RETURN_YEAR = _filter.SLKH_RETURN_YEAR)
            End If

            If _filter.DTTD_CH IsNot Nothing Then
                lst = lst.Where(Function(p) p.DTTD_CH = _filter.DTTD_CH)
            End If

            If _filter.TLDT_CH IsNot Nothing Then
                lst = lst.Where(Function(p) p.TLDT_CH = _filter.TLDT_CH)
            End If

            If _filter.RATE_90D IsNot Nothing Then
                lst = lst.Where(Function(p) p.RATE_90D = _filter.RATE_90D)
            End If
            If _filter.RATE_SPSG IsNot Nothing Then
                lst = lst.Where(Function(p) p.RATE_SPSG = _filter.RATE_SPSG)
            End If

            If _filter.SLBILL_NONMEMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.SLBILL_NONMEMBER = _filter.SLBILL_NONMEMBER)
            End If
            If _filter.SLBILL_NEWMEMBER IsNot Nothing Then
                lst = lst.Where(Function(p) p.SLBILL_NEWMEMBER = _filter.SLBILL_NEWMEMBER)
            End If

            If _filter.SLBILL_TARGET IsNot Nothing Then
                lst = lst.Where(Function(p) p.SLBILL_TARGET = _filter.SLBILL_TARGET)
            End If

            If _filter.SLBILL_TD IsNot Nothing Then
                lst = lst.Where(Function(p) p.SLBILL_TD = _filter.SLBILL_TD)
            End If

            If _filter.RR6_TARGET IsNot Nothing Then
                lst = lst.Where(Function(p) p.RR6_TARGET = _filter.RR6_TARGET)
            End If
            If _filter.SLTV IsNot Nothing Then
                lst = lst.Where(Function(p) p.SLTV = _filter.SLTV)
            End If

            If _filter.SLTV_6MONTH IsNot Nothing Then
                lst = lst.Where(Function(p) p.SLTV_6MONTH = _filter.SLTV_6MONTH)
            End If

            If _filter.RR6_SLTV IsNot Nothing Then
                lst = lst.Where(Function(p) p.RR6_SLTV = _filter.RR6_SLTV)
            End If

            If _filter.TILE_SLTV IsNot Nothing Then
                lst = lst.Where(Function(p) p.TILE_SLTV = _filter.TILE_SLTV)
            End If


            If _filter.MBS_TARGET IsNot Nothing Then
                lst = lst.Where(Function(p) p.MBS_TARGET = _filter.MBS_TARGET)
            End If

            If _filter.MBS_TD IsNot Nothing Then
                lst = lst.Where(Function(p) p.MBS_TD = _filter.MBS_TD)
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

    Public Function CAL_PA_SUM_CH_TOM(ByVal P_PERIOD As Decimal, ByVal P_ORG As Decimal, ByVal P_ISDISSOLVE As Decimal, ByVal _log As UserLog) As Boolean
        Try
            Dim cls As New DataAccess.QueryData
            Dim obj_data As DataTable = cls.ExecuteStore("PKG_PA_BUSINESS.CAL_PA_SUM_CH_TOM",
                                 New With {.P_PERIOD_ID = P_PERIOD,
                                           .P_ORG_ID = P_ORG,
                                           .P_USERNAME = _log.Username.ToUpper(),
                                           .P_ISDISSOLVE = P_ISDISSOLVE,
                                           .P_OUT = cls.OUT_CURSOR})
            Return CBool(obj_data(0)(0))
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
            Return False
        End Try
    End Function
#Region "PA_FRAME_SALARY - Khung hệ số lương chức danh"
    Public Function GetFrameSalary(ByVal sACT As String) As List(Of PA_FRAME_SALARYDTO)
        Dim query As ObjectQuery(Of PA_FRAME_SALARYDTO)
        Try

            query = (From p In Context.PA_FRAME_SALARY
                     From parent In Context.PA_FRAME_SALARY.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
                     Order By p.NAME_VN.ToUpper
                     Select New PA_FRAME_SALARYDTO With {.ID = p.ID,
                                                          .CODE = p.CODE,
                                                          .NAME_VN = p.NAME_VN,
                                                          .NAME_EN = p.NAME_EN,
                                                          .PARENT_ID = p.PARENT_ID,
                                                          .PARENT_NAME = parent.NAME_VN,
                                                         .ACTFLG = p.ACTFLG,
                                                         .COEFFICIENT = p.COEFFICIENT,
                                                         .CREATED_BY = p.CREATED_BY,
                                                         .CREATED_DATE = p.CREATED_DATE,
                                                         .CREATED_LOG = p.CREATED_LOG,
                                                         .MODIFIED_BY = p.MODIFIED_BY,
                                                         .MODIFIED_DATE = p.MODIFIED_DATE,
                                                         .MODIFIED_LOG = p.MODIFIED_LOG,
                                                         .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                                                         .HIERARCHICAL_PATH = p.HIERARCHICAL_PATH,
                                                         .IS_LEVEL1 = p.IS_LEVEL1,
                                                         .IS_LEVEL2 = p.IS_LEVEL2,
                                                         .IS_LEVEL3 = p.IS_LEVEL3,
                                                         .PROMOTE_MONTH = p.PROMOTE_MONTH,
                                                         .NEXT_CODE = p.NEXT_CODE,
                                                         .REMARK = p.REMARK})

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Payroll")
            Throw ex
        End Try
    End Function
    Public Function InsertFrameSalary(ByVal objOrganization As PA_FRAME_SALARYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_FRAME_SALARY
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_FRAME_SALARY.EntitySet.Name)
            objData.CODE = objOrganization.CODE
            objData.NAME_VN = objOrganization.NAME_VN.Trim
            objData.NAME_EN = objOrganization.NAME_EN.Trim
            objData.PARENT_ID = objOrganization.PARENT_ID
            objData.DESCRIPTION_PATH = objOrganization.DESCRIPTION_PATH
            objData.HIERARCHICAL_PATH = objOrganization.HIERARCHICAL_PATH
            objData.ACTFLG = objOrganization.ACTFLG
            objData.REMARK = objOrganization.REMARK

            objData.COEFFICIENT = objOrganization.COEFFICIENT
            objData.PROMOTE_MONTH = objOrganization.PROMOTE_MONTH
            objData.NEXT_CODE = objOrganization.NEXT_CODE
            objData.IS_LEVEL1 = objOrganization.IS_LEVEL1
            objData.IS_LEVEL2 = objOrganization.IS_LEVEL2
            objData.IS_LEVEL3 = objOrganization.IS_LEVEL3
            Context.PA_FRAME_SALARY.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Payroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyFrameSalary(ByVal objOrganization As PA_FRAME_SALARYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_FRAME_SALARY With {.ID = objOrganization.ID}

        Try
            objData = (From p In Context.PA_FRAME_SALARY Where p.ID = objOrganization.ID).FirstOrDefault
            objData.ID = objOrganization.ID
            objData.CODE = objOrganization.CODE
            objData.NAME_VN = objOrganization.NAME_VN.Trim
            objData.NAME_EN = objOrganization.NAME_EN.Trim
            'objData.PARENT_ID = objOrganization.PARENT_ID
            objData.DESCRIPTION_PATH = objOrganization.DESCRIPTION_PATH
            objData.HIERARCHICAL_PATH = objOrganization.HIERARCHICAL_PATH
            'objData.ACTFLG = objOrganization.ACTFLG
            objData.REMARK = objOrganization.REMARK
            objData.COEFFICIENT = objOrganization.COEFFICIENT
            objData.PROMOTE_MONTH = objOrganization.PROMOTE_MONTH
            objData.NEXT_CODE = objOrganization.NEXT_CODE
            objData.IS_LEVEL1 = objOrganization.IS_LEVEL1
            objData.IS_LEVEL2 = objOrganization.IS_LEVEL2
            objData.IS_LEVEL3 = objOrganization.IS_LEVEL3
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Payroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyFrameSalaryPath(ByVal lstPath As List(Of PA_FRAME_SALARY_PATHDTO)) As Boolean
        Try

            For Each item As PA_FRAME_SALARY_PATHDTO In lstPath
                Dim objOrganizationData As New PA_FRAME_SALARY With {.ID = item.ID}
                Context.PA_FRAME_SALARY.Attach(objOrganizationData)
                objOrganizationData.DESCRIPTION_PATH = item.DESCRIPTION_PATH
                objOrganizationData.HIERARCHICAL_PATH = item.HIERARCHICAL_PATH
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Payroll")
            Throw ex
        End Try
    End Function

    Public Function ActiveFrameSalary(ByVal lstOrganization() As PA_FRAME_SALARYDTO, ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstOrganizationData As List(Of PA_FRAME_SALARY)
        Dim lstIDOrganization As List(Of Decimal) = (From p In lstOrganization.ToList Select p.ID).ToList
        lstOrganizationData = (From p In Context.PA_FRAME_SALARY Where lstIDOrganization.Contains(p.ID)).ToList
        For index = 0 To lstOrganizationData.Count - 1
            lstOrganizationData(index).ACTFLG = sActive
            lstOrganizationData(index).MODIFIED_DATE = DateTime.Now
            lstOrganizationData(index).MODIFIED_BY = log.Username
            lstOrganizationData(index).MODIFIED_LOG = log.ComputerName
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function ValidateFrameSalary(ByVal _validate As PA_FRAME_SALARYDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_FRAME_SALARY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_FRAME_SALARY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            Else
                If (_validate.NAME_VN IsNot Nothing And _validate.NAME_EN IsNot Nothing) Then
                    query = (From p In Context.PA_FRAME_SALARY
                             Where (p.NAME_VN.ToUpper = _validate.NAME_VN.ToUpper _
                             Or p.NAME_EN.ToUpper = _validate.NAME_VN.ToUpper) _
                         And p.ACTFLG = _validate.ACTFLG).FirstOrDefault
                End If
                Return (query IsNot Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetMaxId() As Decimal
        Try
            Dim chuoi As Decimal = (From p In Context.PA_FRAME_SALARY
                                    Select p.ID Order By ID Descending).FirstOrDefault
            Return chuoi
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function GetNameFrameSalary(ByVal org_id As Decimal) As String
        Dim str As String = ""
        Try
            Dim chuoi = Context.PA_FRAME_SALARY.Where(Function(f) f.ID = org_id).FirstOrDefault
            str = chuoi.NAME_VN
            Return str
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region
#Region "PA_FRAME_PRODUCTIVITY - Khung hệ số năng suất"
    Public Function GetFrame_Productivity(ByVal sACT As String) As List(Of PA_FRAME_PRODUCTIVITYDTO)
        Dim query As ObjectQuery(Of PA_FRAME_PRODUCTIVITYDTO)
        Try

            query = (From p In Context.PA_FRAME_PRODUCTIVITY
                     From parent In Context.PA_FRAME_PRODUCTIVITY.Where(Function(f) f.ID = p.PARENT_ID).DefaultIfEmpty
                     Order By p.NAME_VN.ToUpper
                     Select New PA_FRAME_PRODUCTIVITYDTO With {.ID = p.ID,
                                                          .CODE = p.CODE,
                                                          .NAME_VN = p.NAME_VN,
                                                          .NAME_EN = p.NAME_EN,
                                                          .PARENT_ID = p.PARENT_ID,
                                                          .PARENT_NAME = parent.NAME_VN,
                                                         .ACTFLG = p.ACTFLG,
                                                         .COEFFICIENT = p.COEFFICIENT,
                                                         .CREATED_BY = p.CREATED_BY,
                                                         .CREATED_DATE = p.CREATED_DATE,
                                                         .CREATED_LOG = p.CREATED_LOG,
                                                         .MODIFIED_BY = p.MODIFIED_BY,
                                                         .MODIFIED_DATE = p.MODIFIED_DATE,
                                                         .MODIFIED_LOG = p.MODIFIED_LOG,
                                                         .DESCRIPTION_PATH = p.DESCRIPTION_PATH,
                                                         .HIERARCHICAL_PATH = p.HIERARCHICAL_PATH,
                                                         .IS_LEVEL1 = p.IS_LEVEL1,
                                                         .IS_LEVEL2 = p.IS_LEVEL2,
                                                         .IS_LEVEL3 = p.IS_LEVEL3,
                                                         .REMARK = p.REMARK})

            Return query.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Payroll")
            Throw ex
        End Try
    End Function
    Public Function InsertFrame_Productivity(ByVal objOrganization As PA_FRAME_PRODUCTIVITYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_FRAME_PRODUCTIVITY
        Try
            objData.ID = Utilities.GetNextSequence(Context, Context.PA_FRAME_PRODUCTIVITY.EntitySet.Name)
            objData.CODE = objOrganization.CODE
            objData.NAME_VN = objOrganization.NAME_VN.Trim
            objData.NAME_EN = objOrganization.NAME_EN.Trim
            objData.PARENT_ID = objOrganization.PARENT_ID
            objData.DESCRIPTION_PATH = objOrganization.DESCRIPTION_PATH
            objData.HIERARCHICAL_PATH = objOrganization.HIERARCHICAL_PATH
            objData.ACTFLG = objOrganization.ACTFLG
            objData.REMARK = objOrganization.REMARK

            objData.COEFFICIENT = objOrganization.COEFFICIENT

            objData.IS_LEVEL1 = objOrganization.IS_LEVEL1
            objData.IS_LEVEL2 = objOrganization.IS_LEVEL2
            objData.IS_LEVEL3 = objOrganization.IS_LEVEL3
            Context.PA_FRAME_PRODUCTIVITY.AddObject(objData)
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Payroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyFrame_Productivity(ByVal objOrganization As PA_FRAME_PRODUCTIVITYDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objData As New PA_FRAME_PRODUCTIVITY With {.ID = objOrganization.ID}

        Try
            objData = (From p In Context.PA_FRAME_PRODUCTIVITY Where p.ID = objOrganization.ID).FirstOrDefault
            objData.ID = objOrganization.ID
            objData.CODE = objOrganization.CODE
            objData.NAME_VN = objOrganization.NAME_VN.Trim
            objData.NAME_EN = objOrganization.NAME_EN.Trim
            'objData.PARENT_ID = objOrganization.PARENT_ID
            objData.DESCRIPTION_PATH = objOrganization.DESCRIPTION_PATH
            objData.HIERARCHICAL_PATH = objOrganization.HIERARCHICAL_PATH
            'objData.ACTFLG = objOrganization.ACTFLG
            objData.REMARK = objOrganization.REMARK
            objData.COEFFICIENT = objOrganization.COEFFICIENT
            objData.IS_LEVEL1 = objOrganization.IS_LEVEL1
            objData.IS_LEVEL2 = objOrganization.IS_LEVEL2
            objData.IS_LEVEL3 = objOrganization.IS_LEVEL3
            Context.SaveChanges(log)
            gID = objData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Payroll")
            Throw ex
        End Try
    End Function

    Public Function ModifyFrame_ProductivityPath(ByVal lstPath As List(Of PA_FRAME_PRODUCTIVITY_PATHDTO)) As Boolean
        Try

            For Each item As PA_FRAME_PRODUCTIVITY_PATHDTO In lstPath
                Dim objOrganizationData As New PA_FRAME_PRODUCTIVITY With {.ID = item.ID}
                Context.PA_FRAME_PRODUCTIVITY.Attach(objOrganizationData)
                objOrganizationData.DESCRIPTION_PATH = item.DESCRIPTION_PATH
                objOrganizationData.HIERARCHICAL_PATH = item.HIERARCHICAL_PATH
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "Payroll")
            Throw ex
        End Try
    End Function

    Public Function ActiveFrame_Productivity(ByVal lstOrganization() As PA_FRAME_PRODUCTIVITYDTO, ByVal sActive As String,
                                   ByVal log As UserLog) As Boolean
        Dim lstOrganizationData As List(Of PA_FRAME_PRODUCTIVITY)
        Dim lstIDOrganization As List(Of Decimal) = (From p In lstOrganization.ToList Select p.ID).ToList
        lstOrganizationData = (From p In Context.PA_FRAME_PRODUCTIVITY Where lstIDOrganization.Contains(p.ID)).ToList
        For index = 0 To lstOrganizationData.Count - 1
            lstOrganizationData(index).ACTFLG = sActive
            lstOrganizationData(index).MODIFIED_DATE = DateTime.Now
            lstOrganizationData(index).MODIFIED_BY = log.Username
            lstOrganizationData(index).MODIFIED_LOG = log.ComputerName
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    Public Function ValidateFrame_Productivity(ByVal _validate As PA_FRAME_PRODUCTIVITYDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_FRAME_PRODUCTIVITY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_FRAME_PRODUCTIVITY
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If

                Return (query Is Nothing)
            Else
                If (_validate.NAME_VN IsNot Nothing And _validate.NAME_EN IsNot Nothing) Then
                    query = (From p In Context.PA_FRAME_PRODUCTIVITY
                             Where (p.NAME_VN.ToUpper = _validate.NAME_VN.ToUpper _
                             Or p.NAME_EN.ToUpper = _validate.NAME_VN.ToUpper) _
                         And p.ACTFLG = _validate.ACTFLG).FirstOrDefault
                End If
                Return (query IsNot Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetMaxIdFrame_Productivity() As Decimal
        Try
            Dim chuoi As Decimal = (From p In Context.PA_FRAME_PRODUCTIVITY
                                    Select p.ID Order By ID Descending).FirstOrDefault
            Return chuoi
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function GetNameFrame_Productivity(ByVal org_id As Decimal) As String
        Dim str As String = ""
        Try
            Dim chuoi = Context.PA_FRAME_PRODUCTIVITY.Where(Function(f) f.ID = org_id).FirstOrDefault
            str = chuoi.NAME_VN
            Return str
        Catch ex As Exception
            Throw ex
        End Try

    End Function
#End Region

End Class

