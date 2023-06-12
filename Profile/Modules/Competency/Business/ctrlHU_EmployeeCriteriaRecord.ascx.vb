Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlHU_EmployeeCriteriaRecord
    Inherits Common.CommonView
    Protected WithEvents RequestView As ViewBase

    Dim rep As New ProfileRepository

#Region "Property"

    Property IsLoad As Boolean
        Get
            Return ViewState(Me.ID & "_IsLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsLoad") = value
        End Set
    End Property

    Public Property tabSource As List(Of EmployeeCriteriaRecordDTO)
        Get
            Return tabSource
        End Get
        Set(ByVal value As List(Of EmployeeCriteriaRecordDTO))
            PageViewState(Me.ID & "_tabSource") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New ProfileRepository
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                End Select
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_DELETE
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            'rgData.SetFilter()
            rgData.AllowCustomPaging = True
            rgData.PageSize = Common.Common.DefaultPageSize
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Export)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub BindData()
        MyBase.BindData()
        GetDataCombo()
    End Sub
    Private Sub GetDataCombo()
        Try
            Dim dtData As DataTable
            Using rep As New ProfileRepository
                'get competency group
                dtData = rep.GetHU_CompetencyGroupList(True)
                FillRadCombobox(cboCompetencyGroup, dtData, "NAME", "ID")
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "TranningRecord")
                        End If
                    End Using
            End Select
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        rgData.Rebind()
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New EmployeeCriteriaRecordDTO
        Dim rep As New ProfileRepository
        Try
            SetValueObjectByRadGrid(rgData, _filter)
            If ctrlOrg.CurrentValue Is Nothing Then
                rgData.DataSource = New List(Of EmployeeCriteriaRecordDTO)
                Exit Function
            End If

            _filter.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue)
            If cboCompetencyGroup.SelectedValue <> "" Then
                _filter.COMPETENCY_GROUP_ID = cboCompetencyGroup.SelectedValue
            End If
            'If cboCompetency.SelectedValue <> "" Then
            '    _filter.COMPETENCY_ID = cboCompetency.SelectedValue
            'End If
            Dim lstCompetency As New List(Of Decimal?)
            Dim collection As IList(Of RadComboBoxItem) = cboCompetency.CheckedItems
            If (collection.Count <> 0) Then
                For Each item As RadComboBoxItem In collection
                    lstCompetency.Add(item.Value)
                Next
            End If
            _filter.LST_COMPETENCY_ID = lstCompetency

            If cboLevelNumber.SelectedValue <> "" Then
                _filter.LEVEL_NUMBER = cboLevelNumber.SelectedValue
            End If
            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue), .IS_DISSOLVE = ctrlOrg.IsDissolve}

            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.EmployeeCriteriaRecord(_filter, _param, Sorts).ToTable()
                Else
                    Return rep.EmployeeCriteriaRecord(_filter, _param).ToTable()
                End If
            Else
                Dim MaximumRows As Integer
                Dim lstData As List(Of EmployeeCriteriaRecordDTO)
                If Sorts IsNot Nothing Then
                    lstData = rep.EmployeeCriteriaRecord(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                Else
                    lstData = rep.EmployeeCriteriaRecord(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                End If
                rgData.VirtualItemCount = MaximumRows

                rgData.DataSource = lstData
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub cboCompetencyGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCompetencyGroup.SelectedIndexChanged
        Dim groupID As Decimal = 0
        Dim dtData
        Try
            If cboCompetencyGroup.SelectedValue <> "" Then
                groupID = cboCompetencyGroup.SelectedValue
            End If
            Using rep As New ProfileRepository
                dtData = rep.GetHU_CompetencyList(groupID)
                FillRadCombobox(cboCompetency, dtData, "NAME", "ID")
                For Each itm As RadComboBoxItem In cboCompetency.Items
                    itm.Checked = True
                Next
            End Using
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Custom"


#End Region

End Class