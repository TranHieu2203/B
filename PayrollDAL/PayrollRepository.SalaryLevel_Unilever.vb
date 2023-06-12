Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Public Class PayrollRepository

#Region "SalaryLevel list"

    Public Function GetSalaryLevel_Unilever(ByVal _filter As SalaryLevelDTO, ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "ORDERS, CREATED_DATE desc") As List(Of SalaryLevelDTO)

        Try
            Dim query = From p In Context.PA_SALARY_LEVEL
                        Join o In Context.PA_SALARY_GROUP On p.SAL_GROUP_ID Equals o.ID
                        Join l In Context.OT_OTHER_LIST On p.CODE Equals l.ID
                        Join t In Context.OT_OTHER_LIST_TYPE On l.TYPE_ID Equals t.ID
                        Where t.CODE = "HU_TITLE_LEVEL" And p.OTHER_USE = "U"


            Dim lst = query.Select(Function(e) New SalaryLevelDTO With {
                                       .ID = e.p.ID,
                                       .SAL_GROUP_ID = e.p.SAL_GROUP_ID,
                                       .SAL_GROUP_NAME = e.o.NAME,
                                       .CODE = e.p.CODE,
                                       .NAME = e.l.NAME_VN,
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

    Public Function InsertSalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO,
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
            objSalaryLevelData.OTHER_USE = "U"
            Context.PA_SALARY_LEVEL.AddObject(objSalaryLevelData)
            Context.SaveChanges(log)
            gID = objSalaryLevelData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' Include get data for U and All
    ''' </summary>
    ''' <param name="salGroupID">Ma bang luong</param>
    ''' <param name="isBlank">Co lay them dong data empty cho combo khong. 0: không; 1: co</param>
    ''' <param name="other_Use">U: Chi lay data cho U; ALL: Lay data cho Acc con lai</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSalaryLevelCombo(salGroupID As Decimal, ByVal isBlank As Boolean, Optional ByVal other_Use As String = "ALL") As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_PA_SAL_LEVEL_NEW",
                                           New With {.P_ISBLANK = isBlank,
                                                     .P_SAL_GROUP_ID = salGroupID,
                                                     .P_OTHER_USE = other_Use,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ValidateSalaryLevel_Unilever(ByVal _validate As SalaryLevelDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.PA_SALARY_LEVEL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.SAL_GROUP_ID = _validate.SAL_GROUP_ID _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.PA_SALARY_LEVEL
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                            And p.SAL_GROUP_ID = _validate.SAL_GROUP_ID
                    ).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try
    End Function

    Public Function ModifySalaryLevel_Unilever(ByVal objSalaryLevel As SalaryLevelDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objSalaryLevelData As New PA_SALARY_LEVEL With {.ID = objSalaryLevel.ID}
        Try
            Context.PA_SALARY_LEVEL.Attach(objSalaryLevelData)
            objSalaryLevelData.SAL_GROUP_ID = objSalaryLevel.SAL_GROUP_ID
            objSalaryLevelData.CODE = objSalaryLevel.CODE.Trim
            objSalaryLevelData.NAME = objSalaryLevel.NAME.Trim
            objSalaryLevelData.REMARK = objSalaryLevel.REMARK
            objSalaryLevelData.ORDERS = objSalaryLevel.ORDERS
            objSalaryLevelData.OTHER_USE = "U"
            Context.SaveChanges(log)
            gID = objSalaryLevelData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iPayroll")
            Throw ex
        End Try

    End Function


    Public Function ActiveSalaryLevel_Unilever(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
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

    Public Function DeleteSalaryLevel_Unilever(ByVal lstSalaryLevel() As SalaryLevelDTO) As Boolean
        Dim lstSalaryLevelData As List(Of PA_SALARY_LEVEL)
        Dim lstIDSalaryLevel As List(Of Decimal) = (From p In lstSalaryLevel.ToList Select p.ID).ToList
        Try
            lstSalaryLevelData = (From p In Context.PA_SALARY_LEVEL Where lstIDSalaryLevel.Contains(p.ID)).ToList
            For idx = 0 To lstSalaryLevelData.Count - 1
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

