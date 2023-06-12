Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Public Class PayrollRepository

#Region "SalaryType list"

    Public Function GetSalaryType(ByVal _filter As SalaryTypeDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of SalaryTypeDTO)

        Try
            Dim query = From p In Context.PA_SALARY_TYPE

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
            Dim lst = query.Select(Function(p) New SalaryTypeDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .IS_INCENTIVE = p.IS_INCENTIVE,
                                       .ORDERS = p.ORDERS,
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
    Public Function GetSalaryTypebyIncentive(ByVal incentive As Integer) As List(Of SalaryTypeDTO)

        Try
            Dim query = From p In Context.PA_SALARY_TYPE

            Dim lst = query.Select(Function(p) New SalaryTypeDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .IS_INCENTIVE = p.IS_INCENTIVE,
                                       .ORDERS = p.ORDERS,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .ACTFLG = p.ACTFLG
                                       }).Where(Function(f) f.IS_INCENTIVE = incentive And f.ACTFLG = "A")
            lst = lst.OrderBy("CREATED_DATE desc")
            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try


    End Function

    Public Function GetPaymentSourcesbyYear(ByVal year As Integer) As List(Of PaymentSourcesDTO)

        Try
            Dim query = From p In Context.PA_PAYMENTSOURCES

            Dim lst = query.Select(Function(p) New PaymentSourcesDTO With {
                                       .ID = p.ID,
                                       .YEAR = p.YEAR,
                                       .NAME = p.NAME,
                                       .REMARK = p.REMARK,
                                       .PAY_TYPE = p.PAY_TYPE,
                                       .ORDERS = p.ORDERS,
                                       .CREATED_DATE = p.CREATED_DATE,
                                       .ACTFLG = p.ACTFLG
                                       }).Where(Function(f) f.YEAR = year)
            lst = lst.OrderBy("CREATED_DATE desc")
            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function GetListOrgBonus() As List(Of OrgBonusDTO)

        Try
            Dim query = From p In Context.PA_ORGBONUS

            Dim lst = query.Select(Function(p) New OrgBonusDTO With {
                                       .ID = p.ID,
                                       .NAME = p.NAME,
                                       .CREATED_DATE = p.CREATED_DATE})
            lst = lst.OrderBy("CREATED_DATE desc")
            Return lst.ToList

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Insert data cho Salary Type
    ''' </summary>
    ''' <param name="objSalaryGroup"></param>
    ''' <param name="log"></param>
    ''' <param name="gID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertSalaryType(ByVal objSalaryType As SalaryTypeDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objSalaryTypeData As New PA_SALARY_TYPE
        Try
            objSalaryTypeData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_TYPE.EntitySet.Name)
            objSalaryTypeData.CODE = objSalaryType.CODE.Trim
            objSalaryTypeData.NAME = objSalaryType.NAME.Trim
            objSalaryTypeData.REMARK = objSalaryType.REMARK
            objSalaryTypeData.IS_INCENTIVE = objSalaryType.IS_INCENTIVE
            objSalaryTypeData.ORDERS = objSalaryType.ORDERS
            objSalaryTypeData.ACTFLG = objSalaryType.ACTFLG
            Context.PA_SALARY_TYPE.AddObject(objSalaryTypeData)
            Context.SaveChanges(log)
            gID = objSalaryTypeData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateSalaryType(ByVal _validate As SalaryTypeDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_TYPE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).SingleOrDefault
                Else
                    query = (From p In Context.PA_SALARY_TYPE
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_TYPE
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.PA_SALARY_TYPE
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifySalaryType(ByVal objSalaryType As SalaryTypeDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryTypeData As New PA_SALARY_TYPE With {.ID = objSalaryType.ID}
        Try
            Context.PA_SALARY_TYPE.Attach(objSalaryTypeData)
            objSalaryTypeData.CODE = objSalaryType.CODE.Trim
            objSalaryTypeData.NAME = objSalaryType.NAME.Trim
            objSalaryTypeData.REMARK = objSalaryType.REMARK
            objSalaryTypeData.IS_INCENTIVE = objSalaryType.IS_INCENTIVE
            objSalaryTypeData.ORDERS = objSalaryType.ORDERS

            Context.SaveChanges(log)
            gID = objSalaryTypeData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function DeleteSalaryType(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstSalaryTypeData As List(Of PA_SALARY_TYPE)
        Try
            lstSalaryTypeData = (From p In Context.PA_SALARY_TYPE Where lstID.Contains(p.ID)).ToList

            For idx = 0 To lstSalaryTypeData.Count - 1
                Context.PA_SALARY_TYPE.DeleteObject(lstSalaryTypeData(idx))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ActiveSalaryType(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_SALARY_TYPE)
        Try
            lstData = (From p In Context.PA_SALARY_TYPE Where lstID.Contains(p.ID)).ToList
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


End Class

