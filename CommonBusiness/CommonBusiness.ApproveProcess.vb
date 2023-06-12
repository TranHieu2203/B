Imports System.Drawing
Imports System.IO
Imports CommonBusiness.ServiceContracts
Imports CommonDAL
Imports Framework.Data

Namespace CommonBusiness.ServiceImplementations
    Partial Public Class CommonBusiness
        Implements ICommonBusiness

#Region "Process Setup"
        Public Function GetLeavePlanList() As List(Of OtherListDTO) _
            Implements ICommonBusiness.GetLeavePlanList
            Using rep As New CommonRepository
                Try
                    Return rep.GetLeavePlanList
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetSignList() As List(Of ATTimeManualDTO) _
            Implements ICommonBusiness.GetSignList
            Using rep As New CommonRepository
                Try
                    Return rep.GetSignList
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTitleList() As List(Of OtherListDTO) _
            Implements ICommonBusiness.GetTitleList
            Using rep As New CommonRepository
                Try
                    Return rep.GetTitleList
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveProcessList() As List(Of ApproveProcessDTO) _
            Implements ICommonBusiness.GetApproveProcessList
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveProcess
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveProcess(ByVal processId As Decimal) As ApproveProcessDTO _
            Implements ICommonBusiness.GetApproveProcess
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveProcess(processId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertApproveProcess(ByVal item As ApproveProcessDTO, ByVal log As UserLog) As Boolean _
            Implements ICommonBusiness.InsertApproveProcess
            Using rep As New CommonRepository
                Try
                    Return rep.InsertApproveProcess(item)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateProcess(ByVal item As ApproveProcessDTO, ByVal log As UserLog) As Boolean _
            Implements ICommonBusiness.UpdateApproveProcess
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateApproveProcess(item)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateProcessStatus(ByVal itemUpdates As List(Of Decimal), ByVal status As String) As Boolean _
            Implements ICommonBusiness.UpdateApproveProcessStatus
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateApproveProcessStatus(itemUpdates, status)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Approve Setup"

        Public Function GetApproveSetup(ByVal id As Decimal) As ApproveSetupDTO _
            Implements ICommonBusiness.GetApproveSetup
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveSetup(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveSetupByEmployee(ByVal employeeId As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupDTO) _
                                    Implements ICommonBusiness.GetApproveSetupByEmployee
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveSetupByEmployee(employeeId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveSetupByOrg(ByVal orgId As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupDTO) _
                                    Implements ICommonBusiness.GetApproveSetupByOrg
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveSetupByOrg(orgId, Sorts)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertApproveSetup(ByVal item As ApproveSetupDTO, ByVal log As UserLog) As Boolean _
            Implements ICommonBusiness.InsertApproveSetup
            Using rep As New CommonRepository
                Try
                    Return rep.InsertApproveSetup(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateApproveSetup(ByVal item As ApproveSetupDTO, ByVal log As UserLog) As Boolean _
            Implements ICommonBusiness.UpdateApproveSetup
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateApproveSetup(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteApproveSetup(ByVal itemDeletes As List(Of Decimal)) As Boolean _
            Implements ICommonBusiness.DeleteApproveSetup
            Using rep As New CommonRepository
                Try
                    Return rep.DeleteApproveSetup(itemDeletes)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IsExistSetupByDate(ByVal itemCheck As ApproveSetupDTO,
                                           Optional ByVal idExclude As Decimal? = Nothing) As Boolean _
                                       Implements ICommonBusiness.IsExistSetupByDate
            Using rep As New CommonRepository
                Try
                    Return rep.IsExistSetupByDate(itemCheck, idExclude)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region

#Region "Approve Template"

        Public Function InsertApproveTemplate(ByVal item As ApproveTemplateDTO,
                                              ByVal log As UserLog) As Boolean _
            Implements ICommonBusiness.InsertApproveTemplate
            Using rep As New CommonRepository
                Try
                    Return rep.InsertApproveTemplate(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateApproveTemplate(ByVal item As ApproveTemplateDTO,
                                              ByVal log As UserLog) As Boolean _
                                          Implements ICommonBusiness.UpdateApproveTemplate
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateApproveTemplate(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveTemplate(ByVal id As Decimal) As ApproveTemplateDTO _
            Implements ICommonBusiness.GetApproveTemplate
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveTemplate(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveTemplateList() As List(Of ApproveTemplateDTO) _
            Implements ICommonBusiness.GetApproveTemplateList
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveTemplate
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteApproveTemplate(ByVal itemDeletes As List(Of Decimal)) As Boolean _
            Implements ICommonBusiness.DeleteApproveTemplate
            Using rep As New CommonRepository
                Try
                    Return rep.DeleteApproveTemplate(itemDeletes)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IsApproveTemplateUsed(ByVal templateID As Decimal) As Boolean _
            Implements ICommonBusiness.IsApproveTemplateUsed
            Using rep As New CommonRepository
                Try
                    Return rep.IsApproveTemplateUsed(templateID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Approve Template Detail"

        Public Function InsertApproveTemplateDetail(ByVal item As ApproveTemplateDetailDTO,
                                                    ByVal log As UserLog) As Boolean _
                                                Implements ICommonBusiness.InsertApproveTemplateDetail
            Using rep As New CommonRepository
                Try
                    Return rep.InsertApproveTemplateDetail(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateApproveTemplateDetail(ByVal item As ApproveTemplateDetailDTO,
                                                    ByVal log As UserLog) As Boolean _
                                                Implements ICommonBusiness.UpdateApproveTemplateDetail
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateApproveTemplateDetail(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveTemplateDetail(ByVal id As Decimal) As ApproveTemplateDetailDTO _
            Implements ICommonBusiness.GetApproveTemplateDetail
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveTemplateDetail(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveTemplateDetailList(ByVal templateId As Decimal) As List(Of ApproveTemplateDetailDTO) _
            Implements ICommonBusiness.GetApproveTemplateDetailList
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveTemplateDetailList(templateId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteApproveTemplateDetail(ByVal itemDeeltes As List(Of Decimal)) As Boolean _
            Implements ICommonBusiness.DeleteApproveTemplateDetail
            Using rep As New CommonRepository
                Try
                    Return rep.DeleteApproveTemplateDetail(itemDeeltes)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function CheckLevelInsert(ByVal level As Decimal,
                                         ByVal idExclude As Decimal,
                                         ByVal idTemplate As Decimal) As Boolean _
                                     Implements ICommonBusiness.CheckLevelInsert
            Using rep As New CommonRepository
                Try
                    Return rep.CheckLevelInsert(level, idExclude, idTemplate)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Setup Approve Ext - Thiết lập người thay thế"

        Public Function InsertApproveSetupExt(ByVal item As ApproveSetupExtDTO,
                                              ByVal log As UserLog) As Boolean _
                                          Implements ICommonBusiness.InsertApproveSetupExt
            Using rep As New CommonRepository
                Try
                    Return rep.InsertApproveSetupExt(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function UpdateApproveSetupExt(ByVal item As ApproveSetupExtDTO,
                                              ByVal log As UserLog) As Boolean _
                                          Implements ICommonBusiness.UpdateApproveSetupExt
            Using rep As New CommonRepository
                Try
                    Return rep.UpdateApproveSetupExt(item, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveSetupExt(ByVal id As Decimal) As ApproveSetupExtDTO _
            Implements ICommonBusiness.GetApproveSetupExt
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveSetupExt(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetApproveSetupExtList(ByVal employeeId As Decimal,
                                               Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupExtDTO) _
                                           Implements ICommonBusiness.GetApproveSetupExtList
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveSetupExtList(employeeId)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteApproveSetupExt(ByVal itemDeletes As List(Of Decimal)) As Boolean _
            Implements ICommonBusiness.DeleteApproveSetupExt
            Using rep As New CommonRepository
                Try
                    Return rep.DeleteApproveSetupExt(itemDeletes)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function IsExistSetupExtByDate(ByVal itemCheck As ApproveSetupExtDTO,
                                              Optional ByVal idExclude As Decimal? = Nothing) As Boolean _
                                          Implements ICommonBusiness.IsExistSetupExtByDate
            Using rep As New CommonRepository
                Try
                    Return rep.IsExistSetupExtByDate(itemCheck, idExclude)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

        Public Function GetApproveUsers(ByVal employeeId As Decimal,
                                        ByVal processCode As String) As List(Of ApproveUserDTO) _
                                    Implements ICommonBusiness.GetApproveUsers
            Using rep As New CommonRepository
                Try
                    Return rep.GetApproveUsers(employeeId, processCode)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTitleByRank_ID(ByVal lst_Rank As List(Of OtherListDTO)) As List(Of TitleDTO) _
            Implements ICommonBusiness.GetTitleByRank_ID
            Using rep As New CommonRepository
                Try
                    Return rep.GetTitleByRank_ID(lst_Rank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function GetTitleAll() As List(Of TitleDTO) _
            Implements ICommonBusiness.GetTitleAll
            Using rep As New CommonRepository
                Try
                    Return rep.GetTitleAll()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetListEmployee(ByVal _orgIds As List(Of Decimal), Optional ByVal is_ter As Decimal = 0) As List(Of EmployeeDTO) _
            Implements ICommonBusiness.GetListEmployee
            Using rep As New CommonRepository
                Try
                    Return rep.GetListEmployee(_orgIds, is_ter)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#Region "Work Process"

        Public Function GetHRProcessList(ByVal _filter As HR_PROCESS_DTO,
                                      ByVal _param As ParamDTO,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByVal Sorts As String = "CREATED_DATE DESC", Optional ByVal log As UserLog = Nothing) As DataTable Implements ServiceContracts.ICommonBusiness.GetHRProcessList
            Using rep As New CommonRepository
                Try
                    Return rep.GetHRProcessList(_filter, _param, Total, PageIndex, PageSize, Sorts, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPeWorkProcessByID(ByVal id As Decimal) As PE_WORK_PROCESSDTO Implements ServiceContracts.ICommonBusiness.GetPeWorkProcessByID
            Using rep As New CommonRepository
                Try
                    Return rep.GetPeWorkProcessByID(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPeWorkEmployeeByProcess(ByVal id As Decimal) As List(Of PE_WORK_EMPLOYEEDTO) Implements ServiceContracts.ICommonBusiness.GetPeWorkEmployeeByProcess
            Using rep As New CommonRepository
                Try
                    Return rep.GetPeWorkEmployeeByProcess(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPeWorkManByProcess(ByVal id As Decimal) As List(Of PE_WORK_MANAGERDTO) Implements ServiceContracts.ICommonBusiness.GetPeWorkManByProcess
            Using rep As New CommonRepository
                Try
                    Return rep.GetPeWorkManByProcess(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetPeWorkManConcludeByProcess(ByVal id As Decimal) As PE_WORK_MANAGER_CONCLUDEDTO Implements ServiceContracts.ICommonBusiness.GetPeWorkManConcludeByProcess
            Using rep As New CommonRepository
                Try
                    Return rep.GetPeWorkManConcludeByProcess(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertPeWorkProcess(ByVal obj As PE_WORK_PROCESSDTO, ByVal log As UserLog) As Integer Implements ServiceContracts.ICommonBusiness.InsertPeWorkProcess
            Using rep As New CommonRepository
                Try
                    Return rep.InsertPeWorkProcess(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyPeWorkProcess(ByVal obj As PE_WORK_PROCESSDTO, ByVal log As UserLog) As Integer Implements ServiceContracts.ICommonBusiness.ModifyPeWorkProcess
            Using rep As New CommonRepository
                Try
                    Return rep.ModifyPeWorkProcess(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteWorkProcess(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.ICommonBusiness.DeleteWorkProcess
            Using rep As New CommonRepository
                Try
                    Return rep.DeleteWorkProcess(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateWorkProcess(ByVal obj As PE_WORK_PROCESSDTO) As Boolean Implements ServiceContracts.ICommonBusiness.ValidateWorkProcess
            Using rep As New CommonRepository
                Try
                    Return rep.ValidateWorkProcess(obj)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertManagerConclude(ByVal obj As PE_WORK_MANAGER_CONCLUDEDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.InsertManagerConclude
            Using rep As New CommonRepository
                Try
                    Return rep.InsertManagerConclude(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyManagerConclude(ByVal obj As PE_WORK_MANAGER_CONCLUDEDTO, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.ModifyManagerConclude
            Using rep As New CommonRepository
                Try
                    Return rep.ModifyManagerConclude(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function HRProcessCancel(ByVal id As Decimal, ByVal process As String, ByVal log As UserLog) As Boolean Implements ServiceContracts.ICommonBusiness.HRProcessCancel
            Using rep As New CommonRepository
                Try
                    Return rep.HRProcessCancel(id, process, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function HRProcessChangeAssignee(ByVal id As Decimal, ByVal _empID As Decimal) As Boolean Implements ServiceContracts.ICommonBusiness.HRProcessChangeAssignee
            Using rep As New CommonRepository
                Try
                    Return rep.HRProcessChangeAssignee(id, _empID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function PrintWorkProcess(ByVal _Id_reggroup As Decimal) As Byte() Implements ServiceContracts.ICommonBusiness.PrintWorkProcess
            Using rep As New CommonRepository
                Try
                    Dim mStream As New System.IO.MemoryStream
                    Dim dsData = rep.GetDataPrintWorkProcess(_Id_reggroup)
                    Dim rootPath As String
                    If dsData IsNot Nothing AndAlso dsData.Tables(0) IsNot Nothing AndAlso dsData.Tables(0).Rows.Count > 0 Then
                        If dsData.Tables(2) IsNot Nothing AndAlso dsData.Tables(2).Rows.Count > 0 Then
                            rootPath = AppDomain.CurrentDomain.BaseDirectory & "\TemplateDynamic\HRProcess\WORK_PROCESS.doc"
                        Else
                            rootPath = AppDomain.CurrentDomain.BaseDirectory & "\TemplateDynamic\HRProcess\WORK_PROCESS_DEFAULT.doc"
                        End If

                        For Each row In dsData.Tables(0).Rows
                            If row("END_EXTEND") = "1" Then
                                row("END_EXTEND") = "☑"
                            Else
                                row("END_EXTEND") = "☐"
                            End If

                            If row("END_CONTRACT") = "1" Then
                                row("END_CONTRACT") = "☑"
                            Else
                                row("END_CONTRACT") = "☐"
                            End If
                            If row("IS_BONHIEM") = "1" Then
                                row("IS_BONHIEM") = "☑"
                            Else
                                row("IS_BONHIEM") = "☐"
                            End If

                            If row("IS_THOINHIEM") = "1" Then
                                row("IS_THOINHIEM") = "☑"
                            Else
                                row("IS_THOINHIEM") = "☐"
                            End If

                            If row("IS_CHANGE_SAL") = "1" Then
                                row("IS_CHANGE_SAL") = "☑"
                            Else
                                row("IS_CHANGE_SAL") = "☐"
                            End If

                            If row("IS_CHANGE_TITLE") = "1" Then
                                row("IS_CHANGE_TITLE") = "☑"
                            Else
                                row("IS_CHANGE_TITLE") = "☐"
                            End If
                            If row("IMAGE") IsNot Nothing And row("IMAGE").ToString <> "" Then
                                Try
                                    Dim logoTempPath = AppDomain.CurrentDomain.BaseDirectory & "\LogoImageTemp"
                                    Dim uploadTmpPath = AppDomain.CurrentDomain.BaseDirectory & "\RadUploadTemp"
                                    Dim Pcheck = logoTempPath & "\" & row("IMAGE")
                                    Dim pathLogo = AppDomain.CurrentDomain.BaseDirectory & "\ReportTemplates\Profile\LocationInfo\" & row("IMAGE")
                                    If File.Exists(Pcheck) Then
                                        row("IMAGE") = Pcheck
                                    Else
                                        If File.Exists(pathLogo) Then
                                            'Delete file trong thu muc tam
                                            If Directory.Exists(logoTempPath) Then
                                                For Each fileT As String In Directory.GetFiles(logoTempPath)
                                                    Try
                                                        System.IO.File.Delete(fileT)
                                                    Catch ex As Exception
                                                        Continue For
                                                    End Try
                                                Next
                                            End If

                                            If Directory.Exists(uploadTmpPath) Then
                                                For Each fileT As String In Directory.GetFiles(uploadTmpPath)
                                                    Try
                                                        System.IO.File.Delete(fileT)
                                                    Catch ex As Exception
                                                        Continue For
                                                    End Try
                                                Next
                                            End If

                                            Dim fileName = row("IMAGE").ToString.Substring(row("IMAGE").ToString.IndexOf("\") + 1)
                                            If Not Directory.Exists(uploadTmpPath) Then
                                                Directory.CreateDirectory(uploadTmpPath)
                                            End If
                                            Dim file = New FileInfo(pathLogo)

                                            Try
                                                file.CopyTo(Path.Combine(uploadTmpPath + "\" + fileName), True)
                                            Catch ex As Exception
                                                Throw ex
                                            End Try

                                            file.IsReadOnly = False

                                            Dim originalImage = System.Drawing.Image.FromFile(Path.Combine(uploadTmpPath, fileName))
                                            Dim thumbnail As New Bitmap(110, 60)
                                            Using g As Graphics = Graphics.FromImage(DirectCast(thumbnail, System.Drawing.Image))
                                                g.DrawImage(originalImage, 0, 0, 110, 60)
                                            End Using
                                            Dim tempPath = System.IO.Path.Combine(logoTempPath, fileName)
                                            If Not Directory.Exists(logoTempPath) Then
                                                Directory.CreateDirectory(logoTempPath)
                                            End If
                                            thumbnail.Save(tempPath)
                                            thumbnail.Dispose()
                                            originalImage.Dispose()
                                            row("IMAGE") = logoTempPath & "\" & fileName
                                        Else
                                            row("IMAGE") = Nothing
                                        End If
                                    End If
                                Catch ex As Exception
                                    row("IMAGE") = ""
                                End Try
                            End If
                        Next

                        For Each row In dsData.Tables(3).Rows
                            If row("END_CONTRACT") = "1" Then
                                row("END_CONTRACT") = "☑"
                            Else
                                row("END_CONTRACT") = "☐"
                            End If

                            If row("EXTEND") = "1" Then
                                row("EXTEND") = "☑"
                            Else
                                row("EXTEND") = "☐"
                            End If

                            If row("ASIGN_PLHD") = "1" Then
                                row("ASIGN_PLHD") = "☑"
                            Else
                                row("ASIGN_PLHD") = "☐"
                            End If

                            If row("RE_CONTRACT") = "1" Then
                                row("RE_CONTRACT") = "☑"
                            Else
                                row("RE_CONTRACT") = "☐"
                            End If

                            If row("CONTRACT1") = "1" Then
                                row("CONTRACT1") = "☑"
                            Else
                                row("CONTRACT1") = "☐"
                            End If

                            If row("CONTRACT2") = "1" Then
                                row("CONTRACT2") = "☑"
                            Else
                                row("CONTRACT2") = "☐"
                            End If

                            If row("CONTRACT3") = "1" Then
                                row("CONTRACT3") = "☑"
                            Else
                                row("CONTRACT3") = "☐"
                            End If

                            If row("CONTRACT4") = "1" Then
                                row("CONTRACT4") = "☑"
                            Else
                                row("CONTRACT4") = "☐"
                            End If

                            If row("CONTRACT5") = "1" Then
                                row("CONTRACT5") = "☑"
                            Else
                                row("CONTRACT5") = "☐"
                            End If

                            If row("BO_NHIEM") = "1" Then
                                row("BO_NHIEM") = "☑"
                            Else
                                row("BO_NHIEM") = "☐"
                            End If

                            If row("THOI_NHIEM") = "1" Then
                                row("THOI_NHIEM") = "☑"
                            Else
                                row("THOI_NHIEM") = "☐"
                            End If

                            If row("IS_CHANGE_SAL") = "1" Then
                                row("IS_CHANGE_SAL") = "☑"
                            Else
                                row("IS_CHANGE_SAL") = "☐"
                            End If

                            If row("IS_CHANGE_JOBBAND") = "1" Then
                                row("IS_CHANGE_JOBBAND") = "☑"
                            Else
                                row("IS_CHANGE_JOBBAND") = "☐"
                            End If

                            If row("IS_CHANGE_TITLE") = "1" Then
                                row("IS_CHANGE_TITLE") = "☑"
                            Else
                                row("IS_CHANGE_TITLE") = "☐"
                            End If
                        Next

                        dsData.Tables(0).TableName = "DT"
                        dsData.Tables(1).TableName = "DT2"
                        dsData.Tables(2).TableName = "DT3"
                        dsData.Tables(3).TableName = "DT4"
                    Else
                        Return Nothing
                    End If

                    Dim doc As Aspose.Words.Document
                    doc = New Aspose.Words.Document(rootPath)
                    doc.MailMerge.ExecuteWithRegions(dsData)
                    doc.Save(mStream, Aspose.Words.SaveFormat.Docx)

                    Return mStream.ToArray()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Ter Process"

        Public Function GetTerProcessByID(ByVal id As Decimal) As HU_TERMINATION_PROCESSDTO Implements ServiceContracts.ICommonBusiness.GetTerProcessByID
            Using rep As New CommonRepository
                Try
                    Return rep.GetTerProcessByID(id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function InsertTerProcess(ByVal obj As HU_TERMINATION_PROCESSDTO, ByVal log As UserLog) As Integer Implements ServiceContracts.ICommonBusiness.InsertTerProcess
            Using rep As New CommonRepository
                Try
                    Return rep.InsertTerProcess(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyTerProcess(ByVal obj As HU_TERMINATION_PROCESSDTO, ByVal log As UserLog) As Integer Implements ServiceContracts.ICommonBusiness.ModifyTerProcess
            Using rep As New CommonRepository
                Try
                    Return rep.ModifyTerProcess(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteTerProcess(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.ICommonBusiness.DeleteTerProcess
            Using rep As New CommonRepository
                Try
                    Return rep.DeleteTerProcess(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ValidateTerProcess(ByVal obj As HU_TERMINATION_PROCESSDTO) As Boolean Implements ServiceContracts.ICommonBusiness.ValidateTerProcess
            Using rep As New CommonRepository
                Try
                    Return rep.ValidateTerProcess(obj)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function PrintTerProcess(ByVal _Id_reggroup As Decimal) As Byte() Implements ServiceContracts.ICommonBusiness.PrintTerProcess
            Using rep As New CommonRepository
                Try
                    Dim mStream As New System.IO.MemoryStream
                    Dim dtData = rep.GetDataPrintTerProcess(_Id_reggroup)
                    Dim rootPath As String
                    If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                        rootPath = AppDomain.CurrentDomain.BaseDirectory & "\TemplateDynamic\HRProcess\TER_PROCESS.doc"

                        For Each row In dtData.Rows
                            If row("TIME_OVER1") = "1" Then
                                row("TIME_OVER1") = "☑"
                            Else
                                row("TIME_OVER1") = "☐"
                            End If

                            If row("TIME_OVER2") = "1" Then
                                row("TIME_OVER2") = "☑"
                            Else
                                row("TIME_OVER2") = "☐"
                            End If

                            If row("IS_TIME_OVER_OTHER") = "1" Then
                                row("IS_TIME_OVER_OTHER") = "☑"
                            Else
                                row("IS_TIME_OVER_OTHER") = "☐"
                            End If

                            If row("IMAGE") IsNot Nothing And row("IMAGE").ToString <> "" Then
                                Try
                                    Dim logoTempPath = AppDomain.CurrentDomain.BaseDirectory & "\LogoImageTemp"
                                    Dim uploadTmpPath = AppDomain.CurrentDomain.BaseDirectory & "\RadUploadTemp"
                                    Dim Pcheck = logoTempPath & "\" & row("IMAGE")
                                    Dim pathLogo = AppDomain.CurrentDomain.BaseDirectory & "\ReportTemplates\Profile\LocationInfo\" & row("IMAGE")
                                    If File.Exists(Pcheck) Then
                                        row("IMAGE") = Pcheck
                                    Else
                                        If File.Exists(pathLogo) Then
                                            'Delete file trong thu muc tam
                                            If Directory.Exists(logoTempPath) Then
                                                For Each fileT As String In Directory.GetFiles(logoTempPath)
                                                    Try
                                                        System.IO.File.Delete(fileT)
                                                    Catch ex As Exception
                                                        Continue For
                                                    End Try
                                                Next
                                            End If

                                            If Directory.Exists(uploadTmpPath) Then
                                                For Each fileT As String In Directory.GetFiles(uploadTmpPath)
                                                    Try
                                                        System.IO.File.Delete(fileT)
                                                    Catch ex As Exception
                                                        Continue For
                                                    End Try
                                                Next
                                            End If

                                            Dim fileName = row("IMAGE").ToString.Substring(row("IMAGE").ToString.IndexOf("\") + 1)
                                            If Not Directory.Exists(uploadTmpPath) Then
                                                Directory.CreateDirectory(uploadTmpPath)
                                            End If
                                            Dim file = New FileInfo(pathLogo)

                                            Try
                                                file.CopyTo(Path.Combine(uploadTmpPath + "\" + fileName), True)
                                            Catch ex As Exception
                                                Throw ex
                                            End Try

                                            file.IsReadOnly = False

                                            Dim originalImage = System.Drawing.Image.FromFile(Path.Combine(uploadTmpPath, fileName))
                                            Dim thumbnail As New Bitmap(110, 60)
                                            Using g As Graphics = Graphics.FromImage(DirectCast(thumbnail, System.Drawing.Image))
                                                g.DrawImage(originalImage, 0, 0, 110, 60)
                                            End Using
                                            Dim tempPath = System.IO.Path.Combine(logoTempPath, fileName)
                                            If Not Directory.Exists(logoTempPath) Then
                                                Directory.CreateDirectory(logoTempPath)
                                            End If
                                            thumbnail.Save(tempPath)
                                            thumbnail.Dispose()
                                            originalImage.Dispose()
                                            row("IMAGE") = logoTempPath & "\" & fileName
                                        Else
                                            row("IMAGE") = Nothing
                                        End If
                                    End If
                                Catch ex As Exception
                                    row("IMAGE") = ""
                                End Try
                            End If
                        Next
                    Else
                        Return Nothing
                    End If

                    Dim doc As Aspose.Words.Document
                    doc = New Aspose.Words.Document(rootPath)
                    doc.MailMerge.Execute(dtData)
                    doc.Save(mStream, Aspose.Words.SaveFormat.Docx)

                    Return mStream.ToArray()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function PrintTerHandover(ByVal _Id_reggroup As Decimal) As Byte() Implements ServiceContracts.ICommonBusiness.PrintTerHandover
            Using rep As New CommonRepository
                Try
                    Dim mStream As New System.IO.MemoryStream
                    Dim item = rep.GetTerProcessByID(_Id_reggroup)
                    Dim rootPath As String
                    If item IsNot Nothing Then
                        rootPath = AppDomain.CurrentDomain.BaseDirectory & "\TemplateDynamic\Termination\TER_HANDOVER.doc"
                    Else
                        Return Nothing
                    End If

                    Dim dtData As New DataTable
                    dtData.Columns.Add("EMPLOYEE_NAME")
                    dtData.Columns.Add("ORG_NAME")
                    dtData.Columns.Add("TER_LASTDATE")
                    Dim newRow As DataRow = dtData.NewRow
                    '',EMPLOYEE_NAME,ORG_NAME,LAST_DATE
                    newRow("EMPLOYEE_NAME") = item.EMPLOYEE_NAME
                    newRow("ORG_NAME") = item.ORG_NAME
                    newRow("TER_LASTDATE") = item.TER_LASTDATE.Value.ToString("dd/MM/yyyy")
                    dtData.Rows.Add(newRow)

                    Dim doc As Aspose.Words.Document
                    doc = New Aspose.Words.Document(rootPath)
                    doc.MailMerge.Execute(dtData)
                    doc.Save(mStream, Aspose.Words.SaveFormat.Doc)

                    Return mStream.ToArray()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetTitlesByOrg2(ByVal _emp As Decimal, Optional ByVal _Is_Blank As Boolean = False) As DataTable Implements ServiceContracts.ICommonBusiness.GetTitlesByOrg2
            Using rep As New CommonRepository
                Try
                    Return rep.GetTitlesByOrg2(_emp, _Is_Blank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmpHrProcessNoty(ByVal empID As Decimal, ByVal processType As Decimal) As List(Of HR_PROCESS_DTO) Implements ServiceContracts.ICommonBusiness.GetEmpHrProcessNoty
            Using rep As New CommonRepository
                Try
                    Return rep.GetEmpHrProcessNoty(empID, processType)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ReadHRNoty(ByVal id As Decimal, ByVal processType As String) As Boolean Implements ServiceContracts.ICommonBusiness.ReadHRNoty
            Using rep As New CommonRepository
                Try
                    Return rep.ReadHRNoty(id, processType)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmpInfoNoty(ByVal empID As Decimal, ByVal processType As Decimal) As List(Of HR_PROCESS_DTO) Implements ServiceContracts.ICommonBusiness.GetEmpInfoNoty
            Using rep As New CommonRepository
                Try
                    Return rep.GetEmpInfoNoty(empID, processType)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetAppProcessNoty(ByVal empID As Decimal, ByVal processType As Decimal) As List(Of HR_PROCESS_DTO) Implements ServiceContracts.ICommonBusiness.GetAppProcessNoty
            Using rep As New CommonRepository
                Try
                    Return rep.GetAppProcessNoty(empID, processType)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

#End Region

#Region "Man Process"

        Public Function InsertManProcess(ByVal obj As PE_MANAGER_OFFER_DTO, ByVal log As UserLog) As Integer Implements ServiceContracts.ICommonBusiness.InsertManProcess
            Using rep As New CommonRepository
                Try
                    Return rep.InsertManProcess(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function ModifyManProcess(ByVal obj As PE_MANAGER_OFFER_DTO, ByVal log As UserLog) As Integer Implements ServiceContracts.ICommonBusiness.ModifyManProcess
            Using rep As New CommonRepository
                Try
                    Return rep.ModifyManProcess(obj, log)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function DeleteManProcess(ByVal lstID As List(Of Decimal)) As Boolean Implements ServiceContracts.ICommonBusiness.DeleteManProcess
            Using rep As New CommonRepository
                Try
                    Return rep.DeleteManProcess(lstID)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function

        Public Function GetEmployeeByManager(ByVal man_id As Decimal) As List(Of EmployeeDTO) Implements ServiceContracts.ICommonBusiness.GetEmployeeByManager
            Using rep As New CommonRepository
                Try
                    Return rep.GetEmployeeByManager(man_id)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
        Public Function PrintManProcess(ByVal _Id_reggroup As Decimal) As Byte() Implements ServiceContracts.ICommonBusiness.PrintManProcess
            Using rep As New CommonRepository
                Try
                    Dim mStream As New System.IO.MemoryStream
                    Dim dsData = rep.GetDataPrintManProcess(_Id_reggroup)
                    Dim rootPath As String
                    If dsData IsNot Nothing AndAlso dsData.Tables(0) IsNot Nothing AndAlso dsData.Tables(0).Rows.Count > 0 Then
                        If dsData.Tables(2) IsNot Nothing AndAlso dsData.Tables(2).Rows.Count > 0 Then
                            rootPath = AppDomain.CurrentDomain.BaseDirectory & "\TemplateDynamic\HRProcess\MAN_PROCESS.doc"
                        Else
                            rootPath = AppDomain.CurrentDomain.BaseDirectory & "\TemplateDynamic\HRProcess\WORK_PROCESS_DEFAULT.doc"
                        End If

                        For Each row In dsData.Tables(0).Rows
                            If row("END_EXTEND") = "1" Then
                                row("END_EXTEND") = "☑"
                            Else
                                row("END_EXTEND") = "☐"
                            End If

                            If row("END_CONTRACT") = "1" Then
                                row("END_CONTRACT") = "☑"
                            Else
                                row("END_CONTRACT") = "☐"
                            End If

                            If row("IS_BONHIEM") = "1" Then
                                row("IS_BONHIEM") = "☑"
                            Else
                                row("IS_BONHIEM") = "☐"
                            End If

                            If row("IS_THOINHIEM") = "1" Then
                                row("IS_THOINHIEM") = "☑"
                            Else
                                row("IS_THOINHIEM") = "☐"
                            End If

                            If row("IS_CHANGE_SAL") = "1" Then
                                row("IS_CHANGE_SAL") = "☑"
                            Else
                                row("IS_CHANGE_SAL") = "☐"
                            End If

                            If row("IS_CHANGE_TITLE") = "1" Then
                                row("IS_CHANGE_TITLE") = "☑"
                            Else
                                row("IS_CHANGE_TITLE") = "☐"
                            End If

                            If row("IMAGE") IsNot Nothing And row("IMAGE").ToString <> "" Then
                                Try
                                    Dim logoTempPath = AppDomain.CurrentDomain.BaseDirectory & "\LogoImageTemp"
                                    Dim uploadTmpPath = AppDomain.CurrentDomain.BaseDirectory & "\RadUploadTemp"
                                    Dim Pcheck = logoTempPath & "\" & row("IMAGE")
                                    Dim pathLogo = AppDomain.CurrentDomain.BaseDirectory & "\ReportTemplates\Profile\LocationInfo\" & row("IMAGE")
                                    If File.Exists(Pcheck) Then
                                        row("IMAGE") = Pcheck
                                    Else
                                        If File.Exists(pathLogo) Then
                                            'Delete file trong thu muc tam
                                            If Directory.Exists(logoTempPath) Then
                                                For Each fileT As String In Directory.GetFiles(logoTempPath)
                                                    Try
                                                        System.IO.File.Delete(fileT)
                                                    Catch ex As Exception
                                                        Continue For
                                                    End Try
                                                Next
                                            End If

                                            If Directory.Exists(uploadTmpPath) Then
                                                For Each fileT As String In Directory.GetFiles(uploadTmpPath)
                                                    Try
                                                        System.IO.File.Delete(fileT)
                                                    Catch ex As Exception
                                                        Continue For
                                                    End Try
                                                Next
                                            End If

                                            Dim fileName = row("IMAGE").ToString.Substring(row("IMAGE").ToString.IndexOf("\") + 1)
                                            If Not Directory.Exists(uploadTmpPath) Then
                                                Directory.CreateDirectory(uploadTmpPath)
                                            End If
                                            Dim file = New FileInfo(pathLogo)

                                            Try
                                                file.CopyTo(Path.Combine(uploadTmpPath + "\" + fileName), True)
                                            Catch ex As Exception
                                                Throw ex
                                            End Try

                                            file.IsReadOnly = False

                                            Dim originalImage = System.Drawing.Image.FromFile(Path.Combine(uploadTmpPath, fileName))
                                            Dim thumbnail As New Bitmap(110, 60)
                                            Using g As Graphics = Graphics.FromImage(DirectCast(thumbnail, System.Drawing.Image))
                                                g.DrawImage(originalImage, 0, 0, 110, 60)
                                            End Using
                                            Dim tempPath = System.IO.Path.Combine(logoTempPath, fileName)
                                            If Not Directory.Exists(logoTempPath) Then
                                                Directory.CreateDirectory(logoTempPath)
                                            End If
                                            thumbnail.Save(tempPath)
                                            thumbnail.Dispose()
                                            originalImage.Dispose()
                                            row("IMAGE") = logoTempPath & "\" & fileName
                                        Else
                                            row("IMAGE") = Nothing
                                        End If
                                    End If
                                Catch ex As Exception
                                    row("IMAGE") = ""
                                End Try
                            End If
                        Next

                        For Each row In dsData.Tables(3).Rows
                            If row("END_CONTRACT") = "1" Then
                                row("END_CONTRACT") = "☑"
                            Else
                                row("END_CONTRACT") = "☐"
                            End If

                            If row("EXTEND") = "1" Then
                                row("EXTEND") = "☑"
                            Else
                                row("EXTEND") = "☐"
                            End If

                            If row("ASIGN_PLHD") = "1" Then
                                row("ASIGN_PLHD") = "☑"
                            Else
                                row("ASIGN_PLHD") = "☐"
                            End If

                            If row("RE_CONTRACT") = "1" Then
                                row("RE_CONTRACT") = "☑"
                            Else
                                row("RE_CONTRACT") = "☐"
                            End If

                            If row("CONTRACT1") = "1" Then
                                row("CONTRACT1") = "☑"
                            Else
                                row("CONTRACT1") = "☐"
                            End If

                            If row("CONTRACT2") = "1" Then
                                row("CONTRACT2") = "☑"
                            Else
                                row("CONTRACT2") = "☐"
                            End If

                            If row("CONTRACT3") = "1" Then
                                row("CONTRACT3") = "☑"
                            Else
                                row("CONTRACT3") = "☐"
                            End If

                            If row("CONTRACT4") = "1" Then
                                row("CONTRACT4") = "☑"
                            Else
                                row("CONTRACT4") = "☐"
                            End If

                            If row("CONTRACT5") = "1" Then
                                row("CONTRACT5") = "☑"
                            Else
                                row("CONTRACT5") = "☐"
                            End If


                            If row("BO_NHIEM") = "1" Then
                                row("BO_NHIEM") = "☑"
                            Else
                                row("BO_NHIEM") = "☐"
                            End If

                            If row("THOI_NHIEM") = "1" Then
                                row("THOI_NHIEM") = "☑"
                            Else
                                row("THOI_NHIEM") = "☐"
                            End If

                            If row("IS_CHANGE_SAL") = "1" Then
                                row("IS_CHANGE_SAL") = "☑"
                            Else
                                row("IS_CHANGE_SAL") = "☐"
                            End If

                            If row("IS_CHANGE_JOBBAND") = "1" Then
                                row("IS_CHANGE_JOBBAND") = "☑"
                            Else
                                row("IS_CHANGE_JOBBAND") = "☐"
                            End If


                            If row("IS_CHANGE_TITLE") = "1" Then
                                row("IS_CHANGE_TITLE") = "☑"
                            Else
                                row("IS_CHANGE_TITLE") = "☐"
                            End If
                        Next

                        dsData.Tables(0).TableName = "DT"
                        dsData.Tables(1).TableName = "DT2"
                        dsData.Tables(2).TableName = "DT3"
                        dsData.Tables(3).TableName = "DT4"
                    Else
                        Return Nothing
                    End If

                    Dim doc As Aspose.Words.Document
                    doc = New Aspose.Words.Document(rootPath)
                    doc.MailMerge.ExecuteWithRegions(dsData)
                    doc.Save(mStream, Aspose.Words.SaveFormat.Docx)

                    Return mStream.ToArray()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        End Function
#End Region
    End Class
End Namespace