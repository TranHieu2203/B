Imports Common.CommonBusiness


Partial Public Class CommonRepository
    Inherits CommonRepositoryBase

#Region "Approve Process"


#Region "Process Setup"
    Public Function GetLeavePlanList() As List(Of OtherListDTO)
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetLeavePlanList
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetSignList() As List(Of ATTimeManualDTO)
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetSignList
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTitleList() As List(Of OtherListDTO)
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetTitleList
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetApproveProcessList() As List(Of ApproveProcessDTO)
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetApproveProcessList
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveProcess(ByVal processId As Decimal) As ApproveProcessDTO
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetApproveProcess(processId)

                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertApproveProcess(ByVal item As ApproveProcessDTO) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.InsertApproveProcess(item, Me.Log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveProcess(ByVal item As ApproveProcessDTO) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.UpdateApproveProcess(item, Me.Log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveProcessStatus(ByVal itemUpdates As List(Of Decimal), ByVal status As String) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.UpdateApproveProcessStatus(itemUpdates, status)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using


        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "Approve Setup"

    Public Function GetApproveSetup(ByVal employeeId As Decimal) As ApproveSetupDTO
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetApproveSetup(employeeId)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveSetupByEmployee(ByVal employeeId As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupDTO)
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetApproveSetupByEmployee(employeeId, Sorts)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveSetupByOrg(ByVal orgId As Decimal,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupDTO)
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetApproveSetupByOrg(orgId, Sorts)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function InsertApproveSetup(ByVal item As ApproveSetupDTO) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.InsertApproveSetup(item, Me.Log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveSetup(ByVal item As ApproveSetupDTO) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.UpdateApproveSetup(item, Me.Log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteApproveSetup(ByVal item As List(Of Decimal)) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.DeleteApproveSetup(item)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsExistApproveSetupByDate(ByVal itemCheck As ApproveSetupDTO, ByVal idExclude As Decimal?) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.IsExistSetupByDate(itemCheck, idExclude)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Approve Template"
    Public Function InsertApproveTemplate(ByVal item As ApproveTemplateDTO) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.InsertApproveTemplate(item, Me.Log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveTemplate(ByVal item As ApproveTemplateDTO) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.UpdateApproveTemplate(item, Me.Log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveTemplate(ByVal id As Decimal) As ApproveTemplateDTO
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetApproveTemplate(id)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveTemplateList() As List(Of ApproveTemplateDTO)
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetApproveTemplateList()
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteApproveTemplate(ByVal itemDeletes As List(Of Decimal)) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.DeleteApproveTemplate(itemDeletes)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsApproveTemplateUsed(ByVal itemDeletes As Decimal) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.IsApproveTemplateUsed(itemDeletes)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Approve Template Detail"

    Public Function InsertApproveTemplateDetail(ByVal item As ApproveTemplateDetailDTO) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.InsertApproveTemplateDetail(item, Me.Log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveTemplateDetail(ByVal item As ApproveTemplateDetailDTO) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.UpdateApproveTemplateDetail(item, Me.Log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveTemplateDetail(ByVal id As Decimal) As ApproveTemplateDetailDTO
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetApproveTemplateDetail(id)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveTemplateDetailList(ByVal templateId As Decimal) As List(Of ApproveTemplateDetailDTO)
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetApproveTemplateDetailList(templateId)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteApproveTemplateDetail(ByVal itemDeletes As List(Of Decimal)) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.DeleteApproveTemplateDetail(itemDeletes)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CheckLevelInsert(ByVal level As Decimal, ByVal idExclude As Decimal, ByVal idTemplate As Decimal) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.CheckLevelInsert(level, idExclude, idTemplate)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "Setup Approve Ext - Thiết lập người thay thế"
    Public Function InsertApproveSetupExt(ByVal item As ApproveSetupExtDTO) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.InsertApproveSetupExt(item, Me.Log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateApproveSetupExt(ByVal item As ApproveSetupExtDTO) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.UpdateApproveSetupExt(item, Me.Log)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveSetupExt(ByVal id As Decimal) As ApproveSetupExtDTO
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetApproveSetupExt(id)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetApproveSetupExtList(ByVal employeeId As Decimal,
                                           Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of ApproveSetupExtDTO)
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetApproveSetupExtList(employeeId, Sorts)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function DeleteApproveSetupExt(ByVal itemDeletes As List(Of Decimal)) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.DeleteApproveSetupExt(itemDeletes)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function IsExistApproveSetupExtByDate(ByVal itemCheck As ApproveSetupExtDTO, ByVal idExclude As Decimal?) As Boolean
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.IsExistSetupExtByDate(itemCheck, idExclude)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

    Public Function GetListEmployee(ByVal _orgId As List(Of Decimal), Optional ByVal is_ter As Decimal = 0) As List(Of EmployeeDTO)
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetListEmployee(_orgId, is_ter)
                Catch ex As Exception
                    rep.Abort()
                    Throw ex
                End Try
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTitleByRank_ID(ByVal lst_Rank As List(Of OtherListDTO)) As List(Of TitleDTO)
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetTitleByRank_ID(lst_Rank)
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTitleAll() As List(Of TitleDTO)
        Try
            Using rep As New CommonBusinessClient
                Try
                    Return rep.GetTitleAll()
                Catch ex As Exception
                    Throw ex
                End Try
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
