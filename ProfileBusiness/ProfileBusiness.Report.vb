Imports Aspose.Cells
Imports Framework.Data
Imports ProfileBusiness.ServiceContracts
Imports ProfileDAL

Namespace ProfileBusiness.ServiceImplementations
    Partial Class ProfileBusiness
        '

#Region "Reports"

#Region "Dynamic"
        Function GetConditionColumn(ByVal _ConditionID As Decimal) As List(Of RptDynamicColumnDTO) Implements IProfileBusiness.GetConditionColumn
            Using rep As New ProfileRepository
                Try

                    Return rep.GetConditionColumn(_ConditionID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Function GetListReportName(ByVal _ViewId As Decimal) As List(Of HuDynamicConditionDTO) Implements IProfileBusiness.GetListReportName
            Using rep As New ProfileRepository
                Try

                    Return rep.GetListReportName(_ViewId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function DeleteDynamicReport(ByVal ID As Decimal, ByVal log As UserLog) As Boolean Implements IProfileBusiness.DeleteDynamicReport
            Using rep As New ProfileRepository
                Try

                    Return rep.DeleteDynamicReport(ID, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function SaveDynamicReport(ByVal _report As HuDynamicConditionDTO, ByVal _col As List(Of HuConditionColDTO), ByVal log As UserLog) As Boolean Implements IProfileBusiness.SaveDynamicReport
            Using rep As New ProfileRepository
                Try

                    Return rep.SaveDynamicReport(_report, _col, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetDynamicReportList() As Dictionary(Of Decimal, String) Implements IProfileBusiness.GetDynamicReportList
            Using rep As New ProfileRepository
                Try

                    Return rep.GetDynamicReportList()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        ''' <summary>
        ''' Lấy danh sách các cột của DynamicReport
        ''' </summary>
        ''' <param name="_reportID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetDynamicReportColumn(ByVal _reportID As Decimal) As List(Of RptDynamicColumnDTO) Implements IProfileBusiness.GetDynamicReportColumn

            Using rep As New ProfileRepository
                Try

                    Return rep.GetDynamicReportColumn(_reportID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        ''' <summary>
        ''' Lấy dữ liệu báo cáo động
        ''' </summary>
        ''' <param name="column">Danh sách các cột cần lấy</param>
        ''' <param name="condition">Expression điều kiện</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetDynamicReport(ByVal _reportID As Decimal,
                                         ByVal orgID As Decimal,
                                         ByVal isDissolve As Decimal,
                                         ByVal chkTerminate As Decimal,
                                         ByVal chkHasTerminate As Decimal,
                                         ByVal column As List(Of String),
                                         ByVal condition As String,
                                         ByVal log As UserLog) As DataTable _
                                     Implements ServiceContracts.IProfileBusiness.GetDynamicReport
            Using rep As New ProfileRepository
                Try
                    Return rep.GetDynamicReport(_reportID, orgID, isDissolve, chkTerminate, chkHasTerminate, column, condition, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region
#Region "Chart"
        Public Function Chart_HDLD(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable Implements IProfileBusiness.Chart_HDLD
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_HDLD(param, _lstOrg, _strFilter, log, isExport)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Chart_Age(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable Implements IProfileBusiness.Chart_Age
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_Age(param, _lstOrg, _strFilter, log, isExport)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Chart_TRINHDO_HOCVAN(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable Implements IProfileBusiness.Chart_TRINHDO_HOCVAN
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_TRINHDO_HOCVAN(param, _lstOrg, _strFilter, log, isExport)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Chart_GENDER(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable Implements IProfileBusiness.Chart_GENDER
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_GENDER(param, _lstOrg, _strFilter, log, isExport)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function Chart_TNCT(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable Implements IProfileBusiness.Chart_TNCT
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_TNCT(param, _lstOrg, _strFilter, log, isExport)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Chart_HANHCHINH(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable Implements IProfileBusiness.Chart_HANHCHINH
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_HANHCHINH(param, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Chart_TRINHDO_NGOAINGU(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable Implements IProfileBusiness.Chart_TRINHDO_NGOAINGU
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_TRINHDO_NGOAINGU(param, _lstOrg, _strFilter, log, isExport)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Chart_EmpObj(ByVal param As ParamDTO, ByVal log As UserLog) As DataTable Implements IProfileBusiness.Chart_EmpObj
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_EmpObj(param, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Chart_BAC_LAO_DONG(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable Implements IProfileBusiness.Chart_BAC_LAO_DONG
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_BAC_LAO_DONG(param, _lstOrg, _strFilter, log, isExport)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Chart_NEW_EMPLOYEE(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable Implements IProfileBusiness.Chart_NEW_EMPLOYEE
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_NEW_EMPLOYEE(param, _lstOrg, _strFilter, log, isExport)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Chart_TER_EMPLOYEE(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable Implements IProfileBusiness.Chart_TER_EMPLOYEE
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_TER_EMPLOYEE(param, _lstOrg, _strFilter, log, isExport)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Chart_BO_NHIEM(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable Implements IProfileBusiness.Chart_BO_NHIEM
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_BO_NHIEM(param, _lstOrg, _strFilter, log, isExport)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Chart_Employee_Num(ByVal param As ParamDTO, ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable Implements IProfileBusiness.Chart_Employee_Num
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_Employee_Num(param, _strFilter, log, isExport)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function Chart_WorkPlace(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, Optional isExport As Decimal = 0) As DataTable Implements IProfileBusiness.Chart_WorkPlace
            Using rep As New ProfileRepository
                Try

                    Return rep.Chart_WorkPlace(param, _lstOrg, _strFilter, log, isExport)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function ExportChartReportlist_Data(ByVal param As ParamDTO, ByVal _lstOrg As List(Of Decimal), ByVal _strFilter As String, ByVal log As UserLog, ByVal Kind_report_func As String) As Byte() Implements IProfileBusiness.ExportChartReportlist_Data
            Using rep As New ProfileRepository
                Try
                    Dim mStream As New System.IO.MemoryStream
                    Dim dsData As New DataTable
                    If Kind_report_func.Trim.ToUpper = "CHART_EMPLOYEE_NUM" Then
                        dsData = Chart_Employee_Num(param, _strFilter, log, 1)
                    ElseIf Kind_report_func.Trim().ToUpper() = "CHART_AGE" Then

                        dsData = Chart_Age(param, _lstOrg, _strFilter, log, 1)

                    ElseIf Kind_report_func.Trim().ToUpper() = "CHART_BAC_LAO_DONG" Then

                        dsData = Chart_BAC_LAO_DONG(param, _lstOrg, _strFilter, log, 1)

                    ElseIf Kind_report_func.Trim().ToUpper() = "CHART_TNCT" Then

                        dsData = Chart_TNCT(param, _lstOrg, _strFilter, log, 1)

                    ElseIf Kind_report_func.Trim().ToUpper() = "CHART_WORKPLACE" Then

                        dsData = Chart_WorkPlace(param, _lstOrg, _strFilter, log, 1)

                    ElseIf Kind_report_func.Trim().ToUpper() = "CHART_GENDER" Then

                        dsData = Chart_GENDER(param, _lstOrg, _strFilter, log, 1)

                    ElseIf Kind_report_func.Trim().ToUpper() = "CHART_TRINHDO_HOCVAN" Then

                        dsData = Chart_TRINHDO_HOCVAN(param, _lstOrg, _strFilter, log, 1)

                    ElseIf Kind_report_func.Trim().ToUpper() = "CHART_TRINHDO_NGOAINGU" Then

                        dsData = Chart_TRINHDO_NGOAINGU(param, _lstOrg, _strFilter, log, 1)

                    ElseIf Kind_report_func.Trim().ToUpper() = "CHART_NEW_EMPLOYEE" Then

                        dsData = Chart_NEW_EMPLOYEE(param, _lstOrg, _strFilter, log, 1)

                    ElseIf Kind_report_func.Trim().ToUpper() = "CHART_TER_EMPLOYEE" Then

                        dsData = Chart_TER_EMPLOYEE(param, _lstOrg, _strFilter, log, 1)

                    ElseIf Kind_report_func.Trim().ToUpper() = "CHART_HDLD" Then

                        dsData = Chart_HDLD(param, _lstOrg, _strFilter, log, 1)

                    ElseIf Kind_report_func.Trim().ToUpper() = "CHART12" Then

                        'dạng multi xài ké các hàm khác
                        'CHART_HDLD,CHART_AGE,CHART_TRINHDO_HOCVAN

                    ElseIf Kind_report_func.Trim().ToUpper() = "CHART_BO_NHIEM" Then

                        dsData = Chart_BO_NHIEM(param, _lstOrg, _strFilter, log, 1)

                    ElseIf Kind_report_func.Trim().ToUpper() = "CHART_TOTAL" Then

                        'dạng multi xài ké các hàm khác
                        'CHART_EMPLOYEE_NUM,CHART_BAC_LAO_DONG,CHART_TNCT,CHART_WORKPLACE,CHART_AGE
                    End If

                    Dim rootPath As String = AppDomain.CurrentDomain.BaseDirectory & "\ReportTemplates\Profile\ExportData.xlsx"

                    Dim designer As WorkbookDesigner = New WorkbookDesigner

                    designer.Open(rootPath)
                    designer.SetDataSource(dsData)
                    designer.Process()
                    designer.Workbook.CalculateFormula()

                    designer.Workbook.Save(mStream, SaveFormat.Xlsx)
                    mStream.Close()
                    Return mStream.ToArray()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
#End Region

    End Class
End Namespace