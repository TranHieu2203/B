Imports System.Data.EntityClient
Imports System.Reflection
Imports System.Web
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class ProfileRepository

#Region "ThanhLCM"

    Public Function GetDynamicReportList() As Dictionary(Of Decimal, String)
        Try
            Dim r = Context.HU_DYNAMIC_REPORT.OrderBy(Function(f) f.ORD_NO).Select(Function(p) p).ToDictionary(Of Decimal, String)(Function(x) x.ID, Function(x) x.VIEW_DESCRIPTION)
            Return r
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetDynamicReportColumn(ByVal _reportID As Decimal) As List(Of RptDynamicColumnDTO)
        Try
            Dim result = (From p In Context.HU_DYNAMIC_REPORT_DTL
                          Where p.VIEW_ID = _reportID
                          Order By p.COLUMN_ORDER
                          Select New RptDynamicColumnDTO With
                           {.ID = p.ID,
                            .COLUMN_NAME = p.COLUMN_NAME,
                            .COLUMN_TYPE = p.COLUMN_TYPE,
                            .TRANSLATE = p.TRANSLATE}).ToList
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetConditionColumn(ByVal _ConditionID As Decimal) As List(Of RptDynamicColumnDTO)
        Try
            Dim result = (From c In Context.HU_CONDITION_COL
                          From d In Context.HU_DYNAMIC_REPORT_DTL.Where(Function(d) d.ID = c.COL_ID)
                          Where c.CONDITION_ID = _ConditionID
                          Order By d.COLUMN_ORDER Ascending
                          Select New RptDynamicColumnDTO With
                           {.ID = d.ID,
                            .COLUMN_NAME = d.COLUMN_NAME,
                            .COLUMN_TYPE = d.COLUMN_TYPE,
                            .TRANSLATE = d.TRANSLATE}).ToList
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetListReportName(ByVal _ViewId As Decimal) As List(Of HuDynamicConditionDTO)
        Try
            Dim result = (From p In Context.HU_DYNAMIC_CONDITION
                          Where p.VIEW_ID = _ViewId
                          Order By p.REPORT_NAME
                          Select New HuDynamicConditionDTO With
                           {.ID = p.ID,
                            .REPORT_NAME = p.REPORT_NAME,
                            .VIEW_ID = p.VIEW_ID,
                            .CONDITION = p.CONDITION}).ToList
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function SaveDynamicReport(ByVal _report As HuDynamicConditionDTO, ByVal _col As List(Of HuConditionColDTO), ByVal log As UserLog) As Boolean
        Try
            Dim objData As New HU_DYNAMIC_CONDITION
            objData.ID = Utilities.GetNextSequence(Context, Context.HU_DYNAMIC_CONDITION.EntitySet.Name)
            objData.VIEW_ID = _report.VIEW_ID
            objData.REPORT_NAME = _report.REPORT_NAME
            objData.CONDITION = _report.CONDITION
            objData.CREATED_BY = log.Username
            objData.CREATED_DATE = Date.Now
            objData.CREATED_LOG = log.Ip
            Context.HU_DYNAMIC_CONDITION.AddObject(objData)
            For Each obj As HuConditionColDTO In _col
                Dim objcol As New HU_CONDITION_COL
                objcol.ID = Utilities.GetNextSequence(Context, Context.HU_CONDITION_COL.EntitySet.Name)
                objcol.CONDITION_ID = objData.ID
                objcol.COL_ID = obj.COL_ID
                objcol.CREATED_BY = log.Username
                objcol.CREATED_DATE = Date.Now
                objcol.CREATED_LOG = log.Ip
                Context.HU_CONDITION_COL.AddObject(objcol)
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function
    Public Function DeleteDynamicReport(ByVal ID As Decimal, ByVal log As UserLog) As Boolean
        Try
            Dim lstDynamicCondition As List(Of HU_DYNAMIC_CONDITION)
            Dim lstConditionCol As List(Of HU_CONDITION_COL)
            Try
                lstDynamicCondition = (From p In Context.HU_DYNAMIC_CONDITION Where p.ID = ID).ToList
                lstConditionCol = (From p In Context.HU_CONDITION_COL Where p.CONDITION_ID = ID).ToList
                For Each o As HU_DYNAMIC_CONDITION In lstDynamicCondition
                    Context.HU_DYNAMIC_CONDITION.DeleteObject(o)
                Next
                For Each c As HU_CONDITION_COL In lstConditionCol
                    Context.HU_CONDITION_COL.DeleteObject(c)
                Next
                Context.SaveChanges(log)
                Return True
            Catch ex As Exception
                WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
                Throw ex
            End Try
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try
    End Function
    Public Function GetDynamicReport(ByVal _reportID As Decimal,
                                     ByVal orgID As Decimal,
                                     ByVal isDissolve As Decimal,
                                     ByVal chkTerminate As Decimal,
                                     ByVal chkHasTerminate As Decimal,
                                     ByVal column As List(Of String),
                                     ByVal condition As String,
                                     ByVal log As UserLog) As DataTable
        Dim con As EntityConnection
        Try
            Dim ReportViewName As String = (From p In Context.HU_DYNAMIC_REPORT Where p.ID = _reportID Select p.VIEW_NAME).SingleOrDefault
            Dim lstColumn = (From p In Context.HU_DYNAMIC_REPORT_DTL
                             Where p.VIEW_ID = _reportID
                             Select New RptDynamicColumnDTO With
                              {.COLUMN_NAME = p.COLUMN_NAME,
                               .COLUMN_TYPE = p.COLUMN_TYPE,
                               .TRANSLATE = p.TRANSLATE}).ToList

            Dim sql As String
            Dim strColumn As String = ""
            Dim strOrgCondition As String = ""
            Dim result As New DataTable
            Dim strJoin As String = ""
            sql = "SELECT {0} from {1} WHERE {2}"
            For Each s As String In column
                strColumn &= "," & s
            Next
            If strColumn <> "" Then
                strColumn = strColumn.Substring(1)
            End If
            If condition = "" Then condition = "1 = 1"
            For Each p In lstColumn
                If condition.Contains("[" & p.COLUMN_NAME & "]") Then
                    If p.COLUMN_TYPE.ToUpper.Contains("DATE") Then
                        condition = condition.Replace("[" & p.COLUMN_NAME & "]", "TO_CHAR(" & p.COLUMN_NAME & ",'yyyyMMdd')")
                    Else
                        condition = condition.Replace("[" & p.COLUMN_NAME & "]", "UPPER(" & p.COLUMN_NAME & ")")
                    End If
                End If
            Next

            strJoin &= " E INNER JOIN SE_CHOSEN_ORG CHOSEN " & _
                " ON E.ORG_ID = CHOSEN.ORG_ID AND USERNAME ='" & log.Username.ToUpper & "' "

            condition &= strOrgCondition
            If chkHasTerminate = 0 And ReportViewName <> "HUV_TERMINATE" Then
                If chkTerminate <> 0 Then
                    condition &= " AND E.WORK_STATUS = 257"
                Else
                    condition &= " AND (E.WORK_STATUS <> 257 OR E.WORK_STATUS IS NULL)"
                End If
            End If

            sql = String.Format(sql, strColumn, ReportViewName & strJoin, condition)

            Using cls As New DataAccess.QueryData

                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = orgID,
                                           .P_ISDISSOLVE = isDissolve})

                result = cls.ExecuteSQL(sql)
                Return result
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            If con IsNot Nothing Then con.Close()
            Throw ex
        End Try
    End Function

#End Region

    Function REPORT_GET_LIST_COMMEND_SINGLE(record As Decimal) As DataTable
        Throw New NotImplementedException
    End Function
End Class
