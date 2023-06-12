Public Class ctrlTR_ProgramNotify
    Inherits Common.CommonView

#Region "Property"

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        'SetGridFilter(rgMain)
        'rgMain.AllowCustomPaging = True
        'rgMain.PageSize = Common.Common.DefaultPageSize
        'rgMain.ClientSettings.EnablePostBackOnRowClick = True
        InitControl()
    End Sub
    Protected Sub InitControl()
        Try
            'Me.ctrlMessageBox.Listener = Me
            'Me.MainToolBar = tbarMain
            'Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
            '                           ToolbarItem.Seperator, ToolbarItem.Save,
            '                           ToolbarItem.Cancel, ToolbarItem.Delete)
            'CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            'CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = False
            'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        'Try
        '    If Not IsPostBack Then
        '        CurrentState = CommonMessage.STATE_NORMAL
        '        hidClassID.Value = Request.Params("CLASS_ID")
        '        Dim obj As ProgramClassDTO
        '        Using rep As New TrainingRepository
        '            obj = rep.GetClassByID(New ProgramClassDTO With {.ID = hidClassID.Value})
        '        End Using
        '        txtClassName.Text = obj.NAME
        '        rdClassEnd.SelectedDate = obj.END_DATE
        '        rdClassStart.SelectedDate = obj.START_DATE
        '        If obj.TR_PROGRAM_ID IsNot Nothing Then
        '            Dim prog As ProgramDTO
        '            Using rep As New TrainingRepository
        '                prog = rep.GetProgramById(obj.TR_PROGRAM_ID)
        '                Program = prog.NAME
        '                ProFromDate = prog.START_DATE
        '                ProToDate = prog.END_DATE
        '            End Using
        '        End If

        '        GetSchedule()
        '    Else
        '        Select Case Message
        '            Case "UpdateView"
        '                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
        '                'rgMain.Rebind()
        '                CurrentState = CommonMessage.STATE_NORMAL

        '            Case "InsertView"
        '                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
        '                'rgMain.CurrentPageIndex = 0
        '                'rgMain.MasterTableView.SortExpressions.Clear()
        '                'rgMain.Rebind()

        '            Case "Cancel"
        '                'rgMain.MasterTableView.ClearSelectedItems()
        '        End Select
        '    End If

        'Catch ex As Exception
        '    DisplayException(Me.ViewName, Me.ID, ex)
        'End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        'Dim rep As New TrainingRepository
        'Try
        '    Select Case CurrentState
        '        Case CommonMessage.STATE_NEW
        '            UpdateControlStatus(True)

        '        Case CommonMessage.STATE_NORMAL
        '            UpdateControlStatus(False)
        '            ClearControlValue()

        '        Case CommonMessage.STATE_DELETE
        '            Dim lstDeletes As New List(Of ProgramClassScheduleDTO)
        '            'For idx = 0 To rgMain.SelectedItems.Count - 1
        '            '    Dim item As GridDataItem = rgMain.SelectedItems(idx)
        '            '    lstDeletes.Add(New ProgramClassScheduleDTO With {.ID = item.GetDataKeyValue("ID")})
        '            'Next
        '            If rep.DeleteClassSchedule(lstDeletes) Then
        '                Refresh("UpdateView")
        '                UpdateControlState()
        '            Else
        '                CurrentState = CommonMessage.STATE_NORMAL
        '                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
        '                UpdateControlState()
        '            End If
        '    End Select
        '    UpdateToolbarState()
        'Catch ex As Exception
        '    DisplayException(Me.ViewName, Me.ID, ex)
        'End Try
    End Sub

    Public Overrides Sub BindData()
        'Try
        '    Dim dic As New Dictionary(Of String, Control)
        '    'dic.Add("IS_HC", chkIsHalf)
        '    'dic.Add("IS_HALF", chkIsHC)
        '    dic.Add("START_DATE", dtpFromTime)
        '    dic.Add("END_DATE", dtpToTime)
        '    dic.Add("CONTENT", txtContent)
        '    'Utilities.OnClientRowSelectedChanged(rgMain, dic)
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

#End Region

#Region "Event"
#End Region

#Region "Custom"
#End Region

End Class