Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Public Class PayrollRepository

#Region "SalaryLevel list"

    Public Function GetSalaryLevel(ByVal _filter As SalaryLevelDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryLevelDTO)

        Try
            Dim query = From p In Context.PA_SALARY_LEVEL
                        Join o In Context.PA_SALARY_GROUP On p.SAL_GROUP_ID Equals o.ID
                        Where Not p.OTHER_USE.Equals("U")

            Dim lst = query.Select(Function(e) New SalaryLevelDTO With {
                                       .ID = e.p.ID,
                                       .SAL_GROUP_ID = e.p.SAL_GROUP_ID,
                                       .SAL_GROUP_NAME = e.o.NAME,
                                       .CODE = e.p.CODE,
                                       .NAME = e.p.NAME,
                                       .REMARK = e.p.REMARK,
                                       .ACTFLG = If(e.p.ACTFLG = "A", "Áp dụng", "Ngừng áp dụng"),
                                       .ACTFLG_DB = e.p.ACTFLG,
                                       .ORDERS = e.p.ORDERS,
                                       .CREATED_DATE = e.p.CREATED_DATE})
            If _filter.SAL_GROUP_ID <> 0 Then
                lst = lst.Where(Function(p) p.SAL_GROUP_ID = _filter.SAL_GROUP_ID)
            End If

            If _filter.CODE <> "" Then
                lst = lst.Where(Function(p) p.CODE.ToUpper.Contains(_filter.CODE.ToUpper))
            End If

            If _filter.NAME <> "" Then
                lst = lst.Where(Function(p) p.NAME.ToUpper.Contains(_filter.NAME.ToUpper))
            End If

            If _filter.ACTFLG <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG.ToUpper.Contains(_filter.ACTFLG.ToUpper))
            End If
            If _filter.ACTFLG_DB <> "" Then
                lst = lst.Where(Function(p) p.ACTFLG_DB.ToUpper.Contains(_filter.ACTFLG_DB.ToUpper))
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

    Public Function InsertSalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objSalaryLevelData As New PA_SALARY_LEVEL
        Try
            objSalaryLevelData.ID = Utilities.GetNextSequence(Context, Context.PA_SALARY_LEVEL.EntitySet.Name)
            objSalaryLevelData.SAL_GROUP_ID = objSalaryLevel.SAL_GROUP_ID
            objSalaryLevelData.CODE = objSalaryLevel.CODE.Trim
            objSalaryLevelData.NAME = objSalaryLevel.NAME.Trim
            objSalaryLevelData.REMARK = objSalaryLevel.REMARK
            objSalaryLevelData.ACTFLG = objSalaryLevel.ACTFLG
            objSalaryLevelData.ORDERS = objSalaryLevel.ORDERS
            objSalaryLevelData.OTHER_USE = "ALL"
            Context.PA_SALARY_LEVEL.AddObject(objSalaryLevelData)
            Context.SaveChanges(log)
            gID = objSalaryLevelData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    Public Function ValidateSalaryLevel(ByVal _validate As SalaryLevelDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_LEVEL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_SALARY_LEVEL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_LEVEL
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.PA_SALARY_LEVEL
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

    Public Function ModifySalaryLevel(ByVal objSalaryLevel As SalaryLevelDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryLevelData As New PA_SALARY_LEVEL With {.ID = objSalaryLevel.ID}
        Try
            Context.PA_SALARY_LEVEL.Attach(objSalaryLevelData)
            objSalaryLevelData.SAL_GROUP_ID = objSalaryLevel.SAL_GROUP_ID
            objSalaryLevelData.CODE = objSalaryLevel.CODE.Trim
            objSalaryLevelData.NAME = objSalaryLevel.NAME.Trim
            objSalaryLevelData.REMARK = objSalaryLevel.REMARK
            objSalaryLevelData.ORDERS = objSalaryLevel.ORDERS
            objSalaryLevelData.OTHER_USE = "ALL"
            Context.SaveChanges(log)
            gID = objSalaryLevelData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function


    Public Function ActiveSalaryLevel(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of PA_SALARY_LEVEL)
        Try
            lstData = (From p In Context.PA_SALARY_LEVEL Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                For Each item In lstData(index).PA_SALARY_RANK
                    item.ACTFLG = bActive
                Next
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function DeleteSalaryLevel(ByVal lstSalaryLevel As List(Of Decimal)) As Boolean
        Dim lstSalaryLevelData As List(Of PA_SALARY_LEVEL)
        Try
            lstSalaryLevelData = (From p In Context.PA_SALARY_LEVEL Where lstSalaryLevel.Contains(p.ID)).ToList
            For idx = 0 To lstSalaryLevelData.Count - 1
                For Each item In lstSalaryLevelData(idx).PA_SALARY_RANK
                    Context.PA_SALARY_RANK.DeleteObject(item)
                Next
                Context.PA_SALARY_LEVEL.DeleteObject(lstSalaryLevelData(idx))
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

