Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports Training.TrainingBusiness

Public Class ctrlTR_ProgramEnrollRecord
    Inherits Common.CommonView

#Region "Property"

    Property dtProgramClassRollcard As List(Of ProgramClassRollcardDTO)
        Get
            Return ViewState(Me.ID & "dtProgramClassRollcard")
        End Get
        Set(ByVal value As List(Of ProgramClassRollcardDTO))
            ViewState(Me.ID & "dtProgramClassRollcard") = value
        End Set
    End Property
    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("TR_CLASS_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("FULLNAME", GetType(String))
                dt.Columns.Add("CLASS_DATE", GetType(String))
                dt.Columns.Add("ATTEND", GetType(String))
                dt.Columns.Add("REMARK", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If
            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property
    Property ProFromDate As Date
        Get
            Return ViewState(Me.ID & "_ProFromDate")
        End Get
        Set(ByVal value As Date)
            ViewState(Me.ID & "_ProFromDate") = value
        End Set
    End Property

    Property ProToDate As Date
        Get
            Return ViewState(Me.ID & "_ProToDate")
        End Get
        Set(ByVal value As Date)
            ViewState(Me.ID & "_ProToDate") = value
        End Set
    End Property

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property Program As String
        Get
            Return ViewState(Me.ID & "_Program")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Program") = value
        End Set
    End Property

    Property SelectedID As Decimal
        Get
            Return ViewState(Me.ID & "_SelectedID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_SelectedID") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        SetGridFilter(rgMain)
        rgMain.AllowCustomPaging = True
        rgMain.PageSize = Common.Common.DefaultPageSize
        InitControl()
    End Sub
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                                       ToolbarItem.Cancel)
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
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
        Dim reps As New TrainingStoreProcedure
        Dim dtData As New DataTable
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                hidClassID.Value = Request.Params("CLASS_ID")
                dtData = reps.GET_EMPLOYEES_IN_CLASS(Convert.ToDecimal(hidClassID.Value))
                Dim obj As ProgramClassDTO
                Using rep As New TrainingRepository
                    obj = rep.GetClassByID(New ProgramClassDTO With {.ID = hidClassID.Value})
                End Using
                If obj.TR_PROGRAM_ID IsNot Nothing Then
                    Dim prog As ProgramDTO
                    Using rep As New TrainingRepository
                        prog = rep.GetProgramById(obj.TR_PROGRAM_ID)
                        Program = prog.NAME
                        If prog.START_DATE IsNot Nothing Then
                            ProFromDate = prog.START_DATE
                        End If
                        If prog.END_DATE IsNot Nothing Then
                            ProToDate = prog.END_DATE
                        End If
                    End Using
                End If
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Overrides Sub UpdateControlState()
        Dim rep As New TrainingRepository
        Try
            UpdateToolbarState()

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
    End Sub

    Public Overrides Sub BindData()
        Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As ProgramClassRollcardDTO
        Dim lstObj As New List(Of ProgramClassRollcardDTO)
        Dim rep As New TrainingRepository
        Dim reps As New TrainingStoreProcedure
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    Page.Validate()
                    If Page.IsValid Then
                        For Each row As GridDataItem In rgMain.Items
                            obj = New ProgramClassRollcardDTO
                            obj.ATTEND = row.GetDataKeyValue("ATTEND")
                            obj.TR_CLASS_ID = hidClassID.Value
                            obj.EMPLOYEE_ID = row.GetDataKeyValue("EMPLOYEE_ID")
                            obj.REMARK = row.GetDataKeyValue("REMARK")
                            obj.CLASS_DATE = rdClassDate.SelectedDate
                            lstObj.Add(obj)
                        Next
                        If rep.UpdateProgramClassRollcard(lstObj) Then
                            CurrentState = CommonMessage.STATE_NORMAL
                            Refresh("UpdateView")
                            UpdateControlState()
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New TrainingRepository
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub RadGrid_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles rgMain.PreRender
        Try
            For Each items As GridDataItem In rgMain.MasterTableView.Items
                items.Edit = True
            Next
            rgMain.MasterTableView.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgMain.ItemDataBound
        Try
            If e.Item.Edit Then
                Dim edit = CType(e.Item, GridEditableItem)
                SetDataToGrid(edit)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub SetDataToGrid(ByVal EditItem As GridEditableItem)
        Dim chkAttend As New CheckBox
        Dim txtRemark As New RadTextBox
        Try
            chkAttend = CType(EditItem.FindControl("chkAttend"), CheckBox)
            txtRemark = CType(EditItem.FindControl("txtRemark"), RadTextBox)
            Dim id = EditItem.GetDataKeyValue("EMPLOYEE_ID")
            For Each item As ProgramClassRollcardDTO In dtProgramClassRollcard
                If item.EMPLOYEE_ID = id Then
                    chkAttend.Checked = item.ATTEND
                    txtRemark.Text = item.REMARK
                End If
            Next
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim rep As New TrainingRepository
        Dim _filter As New ProgramClassRollcardDTO
        Dim _param As New ParamDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgMain, _filter)

            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            _filter.TR_CLASS_ID = Convert.ToDecimal(Request.Params("CLASS_ID"))
            If rdClassDate.SelectedDate IsNot Nothing Then
                _filter.CLASS_DATE = rdClassDate.SelectedDate
            End If
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetStudentInClass(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.GetStudentInClass(_filter, _param).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    If dtProgramClassRollcard Is Nothing Then
                        dtProgramClassRollcard = rep.GetStudentInClass(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, _param, MaximumRows, Sorts)
                    End If
                Else
                    If dtProgramClassRollcard Is Nothing Then
                        dtProgramClassRollcard = rep.GetStudentInClass(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, _param, MaximumRows)
                    End If
                End If
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = dtProgramClassRollcard
            End If
            rep.Dispose()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Function

    Protected Sub txtRemark_TextChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim edit = CType(sender, RadTextBox)
            Dim row = CType(edit.NamingContainer, GridEditableItem)
            Dim remark = edit.Text
            Dim empId = row.GetDataKeyValue("EMPLOYEE_ID")
            For Each item As ProgramClassRollcardDTO In dtProgramClassRollcard
                If item.EMPLOYEE_ID = empId Then
                    item.REMARK = remark
                End If
            Next
            rgMain.Rebind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Sub chkAttend_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Dim edit = CType(sender, CheckBox)
            Dim row = CType(edit.NamingContainer, GridEditableItem)
            Dim check = edit.Checked
            Dim empId = row.GetDataKeyValue("EMPLOYEE_ID")
            Dim id = row.GetDataKeyValue("ID")
            For Each item As ProgramClassRollcardDTO In dtProgramClassRollcard
                If item.EMPLOYEE_ID = empId Then
                    item.ATTEND = check
                End If
            Next
            rgMain.Rebind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rdClassDate_SelectedDateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdClassDate.SelectedDateChanged
        Try
            Dim rep As New TrainingRepository
            Dim _param As New ParamDTO
            Dim _filter As New ProgramClassRollcardDTO
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            Dim MaximumRows As Integer
            _filter.TR_CLASS_ID = Convert.ToDecimal(Request.Params("CLASS_ID"))
            If rdClassDate.SelectedDate IsNot Nothing Then
                _filter.CLASS_DATE = rdClassDate.SelectedDate
            End If
            If Sorts IsNot Nothing Then
                dtProgramClassRollcard = rep.GetStudentInClass(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, _param, MaximumRows, Sorts)
            Else
                dtProgramClassRollcard = rep.GetStudentInClass(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, _param, MaximumRows)
            End If
            rgMain.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
#End Region
End Class