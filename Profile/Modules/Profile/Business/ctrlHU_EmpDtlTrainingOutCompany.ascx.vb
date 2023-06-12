Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_EmpDtlTrainingOutCompany
    Inherits CommonView
    ' Public Overrides Property MustAuthorize As Boolean = False
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\List" + Me.GetType().Name.ToString()
    Dim checkCRUD As Integer = 0 '0-chua thao tac 1-Insert 2-Edit 3-Save 4-Delete


#Region "Property"
    Public Property popupId As String
    Public Property AjaxManagerId As String

    Property checkClickUpload As Integer
        Get
            Return ViewState(Me.ID & "_checkClickUpload")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_checkClickUpload") = value
        End Set
    End Property

    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property

    Public Property EmployeeTrain As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO)
        Get
            Return ViewState(Me.ID & "_EmployeeTrain")
        End Get
        Set(ByVal value As List(Of HU_PRO_TRAIN_OUT_COMPANYDTO))
            ViewState(Me.ID & "_EmployeeTrain") = value
        End Set
    End Property

    Property DeleteEmployeeTrain As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_DeleteEmployeeTrain")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_DeleteEmployeeTrain") = value
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

    Property Down_File As String
        Get
            Return ViewState(Me.ID & "_Down_File")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_Down_File") = value
        End Set
    End Property

    Property FileOldName As String
        Get
            Return ViewState(Me.ID & "_FileOldName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_FileOldName") = value
        End Set
    End Property
#End Region

#Region "Page"

    Private Property ListComboData As ComboBoxDataDTO

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            ctrlEmpBasicInfo.SetProperty("EmployeeInfo", EmployeeInfo)
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgEmployeeTrain.SetFilter()
            rgEmployeeTrain.AllowCustomPaging = True
            rgEmployeeTrain.ClientSettings.EnablePostBackOnRowClick = True
            InitControl()
            popupId = CType(Me.Page, AjaxPage).PopupWindow.ClientID
            'If Not IsPostBack Then
            '    ViewConfig(RadPane1)
            '    GirdConfig(rgEmployeeTrain)
            'End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rgEmployeeTrain.Rebind()
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgEmployeeTrain.Rebind()
                        'SelectedItemDataGridByKey(rgEmployeeTrain, IDSelect, , rgEmployeeTrain.CurrentPageIndex)

                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgEmployeeTrain.CurrentPageIndex = 0
                        rgEmployeeTrain.MasterTableView.SortExpressions.Clear()
                        rgEmployeeTrain.Rebind()
                        'SelectedItemDataGridByKey(rgEmployeeTrain, IDSelect, )
                    Case "Cancel"
                        FileOldName = ""
                        rgEmployeeTrain.MasterTableView.ClearSelectedItems()
                End Select
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileBusinessRepository
        Dim _filter As New HU_PRO_TRAIN_OUT_COMPANYDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            _filter.EMPLOYEE_ID = EmployeeInfo.ID
            SetValueObjectByRadGrid(rgEmployeeTrain, _filter)
            Dim MaximumRows As Integer
            Dim Sorts As String = rgEmployeeTrain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetProcessTraining(_filter, Sorts).ToTable()
                Else
                    Return rep.GetProcessTraining(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.EmployeeTrain = rep.GetProcessTraining(_filter, rgEmployeeTrain.CurrentPageIndex, rgEmployeeTrain.PageSize, MaximumRows, Sorts)
                Else
                    Me.EmployeeTrain = rep.GetProcessTraining(_filter, rgEmployeeTrain.CurrentPageIndex, rgEmployeeTrain.PageSize, MaximumRows)
                End If

                rgEmployeeTrain.VirtualItemCount = MaximumRows
                rgEmployeeTrain.DataSource = Me.EmployeeTrain
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Private Sub rgEmployeeTrain_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgEmployeeTrain.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim strName = If(datarow.GetDataKeyValue("FILE_NAME") IsNot Nothing, datarow.GetDataKeyValue("FILE_NAME").ToString, "")
                If strName <> "" Then
                    datarow("DowloadCommandColumn").Enabled = True
                Else
                    datarow("DowloadCommandColumn").Enabled = False
                    datarow("DowloadCommandColumn").CssClass = "hide-button"
                End If
                If strName.ToUpper.Contains(".JPG") Or strName.ToUpper.Contains(".GIF") Or strName.ToUpper.Contains(".PNG") Then
                    datarow("ViewCommandColumn").Enabled = True
                Else
                    datarow("ViewCommandColumn").Enabled = False
                    datarow("ViewCommandColumn").CssClass = "hide-button"
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgEmployeeTrain_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgEmployeeTrain.ItemCommand
        Try
            If e.CommandName = "Dowload" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link = file.LINK
                Dim name = file.FILE_NAME
                ' ZipFiles(path, FileName)
                link = link.Replace("\", "(slash)")
                Dim url As String = "Download.aspx?" & "ctrlHU_ApproveWorkingBefore_Edit," & link & "," & name
                Dim str As String = "window.open('" & url + "');"
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End If
            If e.CommandName = "View" Then
                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fileName As String = item.GetDataKeyValue("UPLOAD_FILE").ToString()
                Dim file As New FileUploadDTO
                Using rep As New ProfileBusinessRepository
                    file = rep.GetFileForView(fileName)
                End Using
                Dim link As String = file.LINK & "\" & file.FILE_NAME
                Dim strName As String = IO.Path.GetExtension(link).ToUpper()

                link = link.Replace("\", "(slash)")
                If strName.Contains(".JPG") Or strName.Contains(".GIF") Or strName.Contains(".PNG") Then
                    Show(link)
                Else
                    ShowMessage(Translate("Chỉ có thể xem file hình ảnh."), NotifyType.Warning)
                End If

            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Sub Show(strfile As Object)
        Dim script As String
        script = "var oWnd = $find('" & popupId & "');"
        script &= "oWnd.add_close(" & Me.ClientID & "_OnClientClose);"
        script &= "oWnd.setUrl('Dialog.aspx?mid=Profile&fid=ctrlViewImage&group=Business&state=Normal&imgUrl=" & strfile & "');"
        script &= "oWnd.show();"
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "UserPopup", script, True)

    End Sub

    Public Overrides Sub UpdateControlState()

        Try
            Me.Send(CurrentState)
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Overrides Sub BindData()

        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        If e.ButtonID = MessageBoxButtonType.ButtonYes Then
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE Then
                CurrentState = CommonMessage.STATE_DELETE
            End If
            UpdateControlState()
        End If
    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgEmployeeTrain.NeedDataSource
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