Imports Common
Imports Telerik.Web.UI

Public Class ctrlRegisterLeave
    Inherits CommonView

    Public WithEvents AjaxManager As RadAjaxManager
    Public Property AjaxManagerId As String
    '#Region "Properties"
    '    Private Property ENTITLEMENT As List(Of EmployeeDTO)
    '        Get
    '            Return ViewState(Me.ID & "_ENTITLEMENT")
    '        End Get
    '        Set(ByVal value As List(Of EmployeeDTO))
    '            ViewState(Me.ID & "_ENTITLEMENT") = value
    '        End Set
    '    End Property
    '    Private Property sender As String
    '        Get
    '            Return ViewState(Me.ID & "_sender")
    '        End Get
    '        Set(ByVal value As String)
    '            ViewState(Me.ID & "_sender") = value
    '        End Set
    '    End Property


    '#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try


            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try

            AjaxManager = CType(Me.Page, AjaxPage).AjaxManager
            AjaxManagerId = AjaxManager.ClientID

            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Calculate,
                                       ToolbarItem.Print)
            MainToolBar.Items(0).Text = Translate("Tính")
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            'Dim rep As New ProfileRepository
            'Dim comboBoxDataDTO As New ComboBoxDataDTO
            'comboBoxDataDTO.GET_WORK_STATUS = True
            'comboBoxDataDTO.GET_TITLE = True
            'rep.GetComboList(comboBoxDataDTO)

            ''Lay trang thai nhan vien
            'FillDropDownList(cboWorkStatus, comboBoxDataDTO.LIST_WORK_STATUS, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
            'FillDropDownList(cboTitle, comboBoxDataDTO.LIST_TITLE, "NAME_VN", "ID", Common.Common.SystemLanguage, True)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            'If ENTITLEMENT Is Nothing Or Message = CommonMessage.ACTION_UPDATED Then
            '    rgENTITLEMENT.Rebind()
            'End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "EventHandle"
    'Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
    '    Try
    '        Me.sender = "btnSearch"
    '        rgENTITLEMENT.CurrentPageIndex = 0
    '        rgENTITLEMENT.Rebind()
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub






#End Region

    '#Region "Custom"

    '    Private Sub SearchData()
    '        Try
    '            Dim rep As New ProfileBusinessRepository
    '            Dim lstOrgIDs As List(Of Decimal)
    '            'Danh sách Org.
    '            If ctrlOrganization.CurrentValue = "" Then
    '                Exit Sub
    '            Else
    '                If chkOnlyOrgSelected.Checked = False Then
    '                    lstOrgIDs = ctrlOrganization.GetAllChild(Decimal.Parse(ctrlOrganization.CurrentValue))
    '                Else
    '                    lstOrgIDs = New List(Of Decimal)
    '                    lstOrgIDs.Add(Decimal.Parse(ctrlOrganization.CurrentValue))
    '                End If
    '            End If
    '            'lstOrgIDs = ctrlOrganization.GetAllChild(Decimal.Parse(ctrlOrganization.CurrentValue))

    '            Dim _filter As New EmployeeDTO
    '            'Mặc định chỉ load nhân viên đang làm việc
    '            _filter.WORK_STATUS_TEMP = 2
    '            'If Me.sender IsNot Nothing AndAlso Me.sender = "btnSearch" Then 'Chỉ khi click nút tìm kiếm thì mới kết hợp các điều kiện tìm kiếm trên form
    '            If rdpJoinDate.SelectedDate IsNot Nothing Then
    '                _filter.JOIN_DATE = rdpJoinDate.SelectedDate.Value
    '            End If

    '            If cboTitle.SelectedValue <> "" Then
    '                _filter.TITLE_ID = Decimal.Parse(cboTitle.SelectedValue)
    '            End If

    '            If cboWorkStatus.SelectedValue <> "" Then
    '                _filter.WORK_STATUS = Decimal.Parse(cboWorkStatus.SelectedValue)
    '            End If
    '            _filter.EMPLOYEE_CODE = txtEmployeeCode.Text.Trim()
    '            'If chkOnlyResigned.Checked Then 'Chỉ liệt kê nhân viên nghỉ việc.
    '            '    _filter.WORK_STATUS_TEMP = 0
    '            'End If
    '            'If chkWithResigned.Checked Then 'Liệt kê tất cả nhân viên gồm cả nhân viên nghỉ việc
    '            '    _filter.WORK_STATUS_TEMP = 1
    '            'End If
    '            'If Not chkOnlyResigned.Checked Then 'Liệt kê nhân viên đang làm việc
    '            '    _filter.WORK_STATUS_TEMP = 2
    '            'End If
    '            'If chkWithResigned.Checked AndAlso chkOnlyResigned.Checked Then 'Liệt kê tất cả nhân viên
    '            '    _filter.WORK_STATUS_TEMP = 3
    '            'End If
    '            'Me.sender = Nothing
    '            'End If
    '            Dim MaximumRows As Integer

    '            Dim Sorts As String = rgENTITLEMENT.MasterTableView.SortExpressions.GetSortString()
    '            'ENTITLEMENT = rep.GetListEmployee(lstOrgIDs, _filter)
    '            If Sorts IsNot Nothing Then
    '                Me.ENTITLEMENT = rep.GetListEmployeePaging(rgENTITLEMENT.CurrentPageIndex, rgENTITLEMENT.PageSize, MaximumRows, lstOrgIDs, _filter, Sorts)
    '            Else
    '                Me.ENTITLEMENT = rep.GetListEmployeePaging(rgENTITLEMENT.CurrentPageIndex, rgENTITLEMENT.PageSize, MaximumRows, lstOrgIDs, _filter, "EMPLOYEE_CODE asc")
    '            End If

    '            rgENTITLEMENT.VirtualItemCount = MaximumRows

    'LoadGrid:
    '            If ENTITLEMENT IsNot Nothing Then
    '                rgENTITLEMENT.DataSource = ENTITLEMENT
    '            Else
    '                rgENTITLEMENT.DataSource = New List(Of EmployeeDTO)
    '            End If
    '        Catch ex As Exception
    '            DisplayException(Me.ViewName, Me.ID, ex)
    '        End Try
    '    End Sub

    '    Private Sub DeleteEmployee(ByRef strError As String)
    '        Try
    '            Dim rep As New ProfileBusinessRepository

    '            'Kiểm tra các điều kiện trước khi xóa
    '            Dim lstEmpID As New List(Of Decimal)
    '            For Each dr As Telerik.Web.UI.GridDataItem In rgENTITLEMENT.SelectedItems
    '                lstEmpID.Add(Decimal.Parse(dr("ID").Text))
    '            Next
    '            'Xóa nhân viên.
    '            rep.DeleteEmployee(lstEmpID, strError)
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Sub
    '#End Region

    Private Sub AjaxManager_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs) Handles AjaxManager.AjaxRequest
        Try
            Dim url = e.Argument
            '********************** Chọn select bản ghi hiện tại khi không ORDER BY theo MODIFIED_DATE***********************'
            'Dim url As String = e.Argument
            ''eventArg : Employee Code
            'If rgENTITLEMENT.MasterTableView.Items.Count > 0 Then

            '    If url = "" Then
            '        Exit Sub
            '    End If

            '    Dim reg As New System.Text.RegularExpressions.Regex("emp=([^&]+)")
            '    Dim empCode = reg.Match(url).Value.Replace("emp=", "")

            '    If empCode.Length > 0 Then
            '        For idx = 0 To rgENTITLEMENT.PageCount - 1
            '            rgENTITLEMENT.CurrentPageIndex = idx
            '            rgENTITLEMENT.Rebind()

            '            Dim gdiItem As GridDataItem = rgENTITLEMENT.MasterTableView.FindItemByKeyValue("EMPLOYEE_CODE", empCode)
            '            If gdiItem IsNot Nothing Then
            '                rgENTITLEMENT.SelectedIndexes.Clear()
            '                gdiItem.Selected = True
            '                Exit Sub
            '            End If
            '        Next
            '    End If
            'End If
            '*************************END*****************************************
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
End Class