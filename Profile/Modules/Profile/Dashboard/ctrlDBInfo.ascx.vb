Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlDBInfo
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _orgID As Decimal
#Region "Property"
    Public Property InfoList As List(Of StatisticDTO)
        Get
            Return ViewState(Me.ID & "_InfoList")
        End Get
        Set(value As List(Of StatisticDTO))
            ViewState(Me.ID & "_InfoList") = value
        End Set
    End Property

    Public Property RemindList As List(Of ReminderLogDTO)
        Get
            Return PageViewState(Me.ID & "_RemindContractList")
        End Get
        Set(ByVal value As List(Of ReminderLogDTO))
            PageViewState(Me.ID & "_RemindContractList") = value
        End Set
    End Property

    Public Property _filter As ReminderLogDTO
        Get
            If PageViewState(Me.ID & "_filter") Is Nothing Then
                PageViewState(Me.ID & "_filter") = New ReminderLogDTO
            End If
            Return PageViewState(Me.ID & "_filter")
        End Get
        Set(ByVal value As ReminderLogDTO)
            PageViewState(Me.ID & "_filter") = value
        End Set
    End Property

    Dim birthdayRemind As Integer
    Dim contractRemind As Integer


    Dim workingRemind As Integer
    Dim terminateRemind As Integer
    Dim terminateDebtRemind As Integer
    Dim noPaperRemind As Integer
    Dim visaRemind As Integer
    Dim worPermitRemind As Integer
    Dim certificateRemind As Integer

#End Region

#Region "Page"
    Private width As Integer
    Private height As Integer

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Dim resize As Integer = 0
            If Request("resize") IsNot Nothing Then
                resize = CInt(Request("resize"))
            End If
            _orgID = 1
            If Request("orgid") IsNot Nothing Then
                _orgID = CInt(Request("orgid"))
            End If
            LoadConfig()
            If Not IsPostBack Or resize = 0 Then
                Refresh(CommonMessage.ACTION_UPDATED)
            Else
                Refresh()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileDashboardRepository
        Try
            If InfoList Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
                InfoList = rep.GetCompanyNewInfo(_orgID)
            End If
            rep.Dispose()
            lbtnEmpCount.Text = (From p In InfoList Where p.NAME = "EMP_COUNT" Select p.VALUE).FirstOrDefault
            lbtnEmpNew.Text = (From p In InfoList Where p.NAME = "EMP_NEW" Select p.VALUE).FirstOrDefault
            lbtnEmpTer.Text = (From p In InfoList Where p.NAME = "EMP_TER" Select p.VALUE).FirstOrDefault
            lbtnContractNew.Text = (From p In InfoList Where p.NAME = "CONTRACT_NEW" Select p.VALUE).FirstOrDefault
            lbtnTransferNew.Text = (From p In InfoList Where p.NAME = "TRANSFER_NEW" Select p.VALUE).FirstOrDefault
            lbtnTransferMove.Text = (From p In InfoList Where p.NAME = "TRANSFER_MOVE" Select p.VALUE).FirstOrDefault
            lbtnAgeAvg.Text = (From p In InfoList Where p.NAME = "AGE_AVG" Select p.VALUE).FirstOrDefault
            lbtnSeniority.Text = (From p In InfoList Where p.NAME = "SENIORITY_AVG" Select p.VALUE).FirstOrDefault
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(e As System.EventArgs)
        Try
            rgContract.SetFilter()
            rgContract.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub


#End Region

#Region "Event"

    Private Sub rgContract_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgContract.ItemCommand
        If e.CommandName = Telerik.Web.UI.RadGrid.ExportToExcelCommandName Then
            ExportToExcel(rgContract)
            e.Canceled = True
        End If
    End Sub

    Private Sub rgContract_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgContract.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim item = CType(e.Item, GridDataItem)
                Dim link As LinkButton
                link = CType(item("LINK_POPUP").FindControl("lbtnLink"), LinkButton)
                If item.GetDataKeyValue("LINK_POPUP") Is Nothing Then
                    link.Visible = False
                Else
                    link.OnClientClick = item.GetDataKeyValue("LINK_POPUP")
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgContract.NeedDataSource
        Using rep As New ProfileDashboardRepository
            Try
                Utilities.SetValueObjectByRadGrid(rgContract, _filter)
                If RemindList Is Nothing OrElse (RemindList IsNot Nothing AndAlso RemindList.Count = 0) Then
                    RemindList = rep.GetRemind(contractRemind.ToString & "," & _
                                               birthdayRemind.ToString & "," & _
                                               visaRemind.ToString & "," & _
                                               workingRemind.ToString & "," & _
                                               terminateRemind.ToString & "," & _
                                               terminateDebtRemind.ToString & "," & _
                                               noPaperRemind.ToString & "," & _
                                               "0," & _
                                               "0," & _
                                               worPermitRemind.ToString & "," & _
                                               certificateRemind.ToString
                                               )
                    For Each item In RemindList
                        item.REMIND_NAME = Translate(item.REMIND_NAME)
                    Next
                End If
                lbReminder1.Text = (From p In RemindList Where p.REMIND_TYPE = 1).Count
                lbReminder2.Text = (From p In RemindList Where p.REMIND_TYPE = 2).Count
                lbReminder13.Text = (From p In RemindList Where p.REMIND_TYPE = 13).Count
                lbReminder14.Text = (From p In RemindList Where p.REMIND_TYPE = 14).Count
                lbReminder16.Text = (From p In RemindList Where p.REMIND_TYPE = 16).Count
                lbReminder19.Text = (From p In RemindList Where p.REMIND_TYPE = 19).Count
                lbReminder5.Text = (From p In RemindList Where p.REMIND_TYPE = 5).Count
                lbReminder20.Text = (From p In RemindList Where p.REMIND_TYPE = 20).Count
                rgContract.DataSource = RemindList
            Catch ex As Exception
                rep.Dispose()
                DisplayException(Me.ViewName, Me.ID, ex)
            End Try
        End Using
    End Sub



#End Region

#Region "Custom"


    Private Sub LoadConfig()
        Try
            contractRemind = CommonConfig.ReminderContractDays
            birthdayRemind = CommonConfig.ReminderBirthdayDays

            workingRemind = CommonConfig.ReminderWorking
            terminateRemind = CommonConfig.ReminderTerminate
            terminateDebtRemind = CommonConfig.ReminderTerminateDebt
            noPaperRemind = CommonConfig.ReminderNoPaper
            visaRemind = CommonConfig.ReminderVisa

            worPermitRemind = CommonConfig.ReminderLabor
            certificateRemind = CommonConfig.ReminderCertificate
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ExportToExcel(ByVal grid As RadGrid)
        Dim lstData As List(Of ReminderLogDTO)

        Dim _error As Integer = 0
        Using xls As New ExcelCommon

            Dim query = From p In RemindList
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(f) f.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.FULLNAME <> "" Then
                query = query.Where(Function(f) f.FULLNAME.ToUpper.Contains(_filter.FULLNAME.ToUpper))
            End If
            If _filter.REMIND_NAME <> "" Then
                query = query.Where(Function(f) f.REMIND_NAME.ToUpper.Contains(_filter.REMIND_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(f) f.TITLE_NAME.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(f) f.ORG_NAME.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.JOIN_DATE IsNot Nothing Then
                query = query.Where(Function(f) f.JOIN_DATE = _filter.JOIN_DATE)
            End If
            If _filter.REMIND_DATE IsNot Nothing Then
                query = query.Where(Function(f) f.REMIND_DATE = _filter.REMIND_DATE)
            End If
            lstData = query.ToList
            Dim dtData = lstData.ToTable
            Dim bCheck = xls.ExportExcelTemplate(
                Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("fid") & ".xls"),
                "Reminder", dtData, Response, _error)
            If Not bCheck Then
                Select Case _error
                    Case 1
                        ShowMessage(Translate("Mẫu báo cáo không tồn tại"), NotifyType.Warning)
                    Case 2
                        ShowMessage(Translate("Dữ liệu không tồn tại"), NotifyType.Warning)
                End Select
                Exit Sub
            End If
        End Using
    End Sub

#End Region

End Class