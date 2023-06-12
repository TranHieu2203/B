Imports System.IO
Imports Aspose.Cells
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
'Imports HistaffFrameworkPublic

Public Class ctrlHU_BHLD
    Inherits Common.CommonView

    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"

    ''' <summary>
    ''' IDSelect
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get

        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property dtData As DataTable
        Get
            If ViewState(Me.ID & "_dtData") Is Nothing Then
                Dim dt As New DataTable("DATA")
                dt.Columns.Add("STT", GetType(String))
                dt.Columns.Add("EMPLOYEE_ID", GetType(String))
                dt.Columns.Add("EMPLOYEE_CODE", GetType(String))
                dt.Columns.Add("EMPLOYEE_NAME", GetType(String))
                dt.Columns.Add("MATHE", GetType(String))
                dt.Columns.Add("FROM_DATE", GetType(String))
                dt.Columns.Add("TO_DATE", GetType(String))
                dt.Columns.Add("DAY_NUM", GetType(String))
                dt.Columns.Add("PLACE_NAME", GetType(String))
                dt.Columns.Add("MONEY", GetType(String))
                dt.Columns.Add("REMARK", GetType(String))
                ViewState(Me.ID & "_dtData") = dt
            End If

            Return ViewState(Me.ID & "_dtData")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtData") = value
        End Set
    End Property

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load page, trang thai page, trang thai control</summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Refresh()
            UpdateControlState()
            rgMain.SetFilter()
            rgMain.AllowCustomPaging = True
            rgMain.PageSize = Common.Common.DefaultPageSize
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Khoi tao control, set thuoc tinh cho grid </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            InitControl()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain

            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Calculate)

            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE,
                                                                  ToolbarIcons.Export,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Xuất excel"))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_EXPORT,
                                                                  ToolbarIcons.Export,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Xuất file mẫu"))
            Me.MainToolBar.Items.Add(Common.Common.CreateToolbarItem(CommonMessage.TOOLBARITEM_IMPORT,
                                                                  ToolbarIcons.Import,
                                                                  ToolbarAuthorize.Special1,
                                                                  "Nhập file mẫu"))
            CType(MainToolBar.Items(1), RadToolBarButton).CausesValidation = True
            CType(MainToolBar.Items(3), RadToolBarButton).Text = "Tính BHLĐ"
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Khoi tao, load menu toolbar</summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository

        Try
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>
    ''' Load control, Khoi tao popup tim Ma Nhan Vien, Tim Don Vi To Chuc
    ''' Set Trang thai control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case isLoadPopup
                Case 1
                    If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.MustHaveContract = False
                    End If

            End Select
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    ' EnabledGridNotPostback(rgMain, False)
                Case CommonMessage.STATE_NORMAL
                    ' EnabledGridNotPostback(rgMain, True)
                Case CommonMessage.STATE_EDIT
                    'EnabledGridNotPostback(rgMain, False)
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DeleteTravel(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            rep.Dispose()
            ChangeToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Fill du lieu len combobox</summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If rnYear.Value Is Nothing Then
                rnYear.Value = System.DateTime.Now.Year
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> 
    ''' Event click item tren menu toolbar
    ''' Set trang thai control sau khi process xong 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim commonRes As New CommonRepository

        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT


                    CreateDataFilter()
                    If rgMain.SelectedItems.Count = 0 Then
                        For Each item As GridItem In rgMain.MasterTableView.Items
                            item.Edit = True
                        Next
                    Else
                        For Each _item As GridDataItem In rgMain.SelectedItems
                            _item.Edit = True
                        Next
                    End If

                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                    rgMain.Rebind()
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Template_ImportBHLD()

                Case CommonMessage.TOOLBARITEM_EXPORT_TEMPLATE
                    excel()
                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    CreateDataFilter()
                    If rnYear.Value Is Nothing Then
                        ShowMessage("Vui lòng nhập năm", NotifyType.Warning)
                        Exit Sub
                    End If
                    If GetDataFromGrid() = 0 Then
                        ShowMessage("Thao tác thực hiện không thành công", NotifyType.Error)
                        Exit Sub
                    Else
                        For Each item As GridItem In rgMain.MasterTableView.Items
                            item.Edit = False
                        Next
                        rgMain.Rebind()
                        UpdateControlState()
                        ShowMessage("Thao tác thực hiện thành công", NotifyType.Success)
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    CreateDataFilter()

                    For Each item As GridItem In rgMain.MasterTableView.Items
                        item.Edit = False
                    Next

                    rgMain.Rebind()
                    UpdateControlState()

                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    If Not IsNumeric(rnYear.Value) Then
                        ShowMessage(Translate("Chưa nhập năm"), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}
                    If rep.CalculateBHLD(rnYear.Value, _param) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If

            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub excel()
        Dim rep As New ProfileRepository

        Dim _filter As New HUTravelDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgMain, _filter)
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()


            Dim dtData As DataTable
            If Sorts IsNot Nothing Then
                dtData = rep.GetBHLD1(CDec(Val(rnYear.Value)), txtempcode.Text, 0, Integer.MaxValue, MaximumRows, _param, Sorts)
            Else
                dtData = rep.GetBHLD1(CDec(Val(rnYear.Value)), txtempcode.Text, 0, Integer.MaxValue, MaximumRows, _param)
            End If

            Using xls As New ExcelCommon
                If dtData.Rows.Count = 0 Then
                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                    Exit Sub
                ElseIf dtData.Rows.Count > 0 Then
                    rgMain.ExportExcel(Server, Response, dtData, "BHLD")
                    Exit Sub
                End If
            End Using
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub Template_ImportBHLD()
        Dim rep As New ProfileRepository

        Dim _filter As New HUTravelDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgMain, _filter)
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()


            Dim Travels As DataTable
            If Sorts IsNot Nothing Then
                Travels = rep.GetBHLD1(CDec(Val(rnYear.Value)), txtempcode.Text, rgMain.CurrentPageIndex, 4000, MaximumRows, _param, Sorts)
            Else
                Travels = rep.GetBHLD1(CDec(Val(rnYear.Value)), txtempcode.Text, rgMain.CurrentPageIndex, 4000, MaximumRows, _param)
            End If

            Dim tb2 As New DataTable
            tb2.Columns.Add("ORG_NAME", GetType(String))
            tb2.Columns.Add("YEAR", GetType(String))
            tb2.Rows.Add(rep.getOrgName(ctrlOrg.CurrentValue), rnYear.Value.ToString)

            Dim configPath As String = ConfigurationManager.AppSettings("PathImportFolder")
            Dim dsData As New DataSet

            dsData.Tables.Add(Travels)
            dsData.Tables.Add(tb2)

            dsData.Tables(0).TableName = "Table"
            dsData.Tables(1).TableName = "Table1"
            rep.Dispose()


            ExportTemplate("Profile\Import\BHLD.xls",
                                  dsData, Nothing, "BHLD" & Format(Date.Now, "yyyymmdd"))


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function GetDataFromGrid() As Decimal
        Dim rep As New ProfileRepository

        Try
            Dim vData As New DataTable
            Dim lst_col = rep.getListCol()
            Dim ListKey() = New String(lst_col.Count + 1) {}
            Dim col_lst = New List(Of String)
            'Dim ListKeyRoot() = New String(4) {}
            Dim i = 1


            ListKey(0) = "EMP_ID"
            'ListKey(1) = "EMP_CODE"
            'ListKey(2) = "EMP_NAME"
            'ListKey(3) = "ORG_NAME"
            'ListKey(4) = "TITLE_NAME"
            'ListKeyRoot = ListKey

            For Each item In lst_col
                ListKey(i) = item.CODE.ToUpper
                vData.Columns.Add(item.CODE.ToUpper, GetType(String))
                i += 1
            Next
            ListKey(i) = "REMARK"

            Dim year = rnYear.Value
            Dim code = txtempcode.Text
            rnYear.Value = Nothing
            txtempcode.Text = ""

            vData = CreateDataFilter()

            rnYear.Value = year
            txtempcode.Text = code


            For Each item As GridDataItem In rgMain.EditItems
                Dim ID = CType(item("EMP_ID").Controls(0), TextBox).Text
                For Each row As DataRow In vData.Rows
                    Dim isExist As Boolean = False
                    If ID = row("EMP_ID").ToString Then
                        For Each key In ListKey
                            'f Not String.IsNullOrEmpty(item(key).ToString) AndAlso CType(item(key).Controls(0), Object).Text <> "" Then
                            If key = "EMP_ID" Then
                                Dim numOut As Decimal = 0D
                                Decimal.TryParse(CType(item(key).Controls(0), Object).Text, numOut)
                                row(key) = numOut
                            ElseIf key = "REMARK" Then
                                row(key) = CType(item(key).Controls(0), Object).Text
                            Else
                                'Dim numOut As Decimal = 0D
                                'Decimal.TryParse(CType(item(key).Controls(0), Object).Checked, numOut)
                                row(key) = CType(item(key).Controls(0), Object).Checked
                            End If

                            'End If
                        Next
                        isExist = True
                    End If
                    If isExist Then
                        Exit For
                    End If
                Next
            Next
            For Each key In ListKey
                col_lst.Add(key)
            Next
            Dim re = rep.saveBHLD(CDec(Val(year)), col_lst, vData, False)
            Return re
        Catch ex As Exception

        End Try
    End Function
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary> Event xử lý sự kiện khi ấn button trên MsgBox </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgMain_ColumnCreated(sender As Object, e As GridColumnCreatedEventArgs) Handles rgMain.ColumnCreated
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateCol()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


    ''' <summary>
    ''' Ham xu ly tạo cột cho Grid 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub CreateCol()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim rep As New ProfileRepository
        Try
            Dim lst_col = rep.getListCol()
            Dim strTest() = New String(7 + lst_col.Count) {}

            Dim i0 As Integer = 0

            While (i0 < rgMain.Columns.Count)
                Dim c As GridColumn = rgMain.Columns(i0)
                If c.UniqueName <> "cbStatus" And c.UniqueName <> "YEAR" Then
                    rgMain.Columns.Remove(c)
                    Continue While
                End If
                i0 = i0 + 1
            End While
            'rgMain.MasterTableView.Columns.Clear()
            '1. Column cố định   
            'Dim rCol1 As GridClientSelectColumn
            'rCol1 = New GridClientSelectColumn()
            'rgMain.MasterTableView.Columns.Add(rCol1)
            'rCol.DataField = "EMP_ID"
            'rCol1.UniqueName = "cbStatus"
            'rCol.HeaderText = Translate("EMP_ID")
            'rCol.ReadOnly = True
            'rCol1.Visible = True
            'rCol1.EmptyDataText = String.Empty
            'strTest(0) = "EMP_ID"

            Dim rCol As GridBoundColumn
            Dim rNCol As GridNumericColumn
            rCol = New GridBoundColumn()
            rgMain.MasterTableView.Columns.Add(rCol)
            rCol.DataField = "EMP_ID"
            rCol.UniqueName = "EMP_ID"
            rCol.HeaderText = Translate("EMP_ID")
            rCol.ReadOnly = True
            rCol.Visible = False
            rCol.EmptyDataText = String.Empty
            strTest(0) = rCol.DataField

            rCol = New GridBoundColumn()
            rgMain.MasterTableView.Columns.Add(rCol)
            rCol.DataField = "EMP_CODE"
            rCol.UniqueName = "EMP_CODE"
            rCol.HeaderText = Translate("Mã NV")
            rCol.HeaderStyle.Width = 100
            rCol.ReadOnly = True
            rCol.Visible = True
            rCol.SortExpression = "EMP_CODE"
            rCol.EmptyDataText = String.Empty
            strTest(1) = rCol.DataField

            rCol = New GridBoundColumn()
            rgMain.MasterTableView.Columns.Add(rCol)
            rCol.DataField = "MA_THE"
            rCol.UniqueName = "MA_THE"
            rCol.HeaderText = Translate("Mã thẻ")
            rCol.HeaderStyle.Width = 100
            rCol.ReadOnly = True
            rCol.Visible = True
            rCol.SortExpression = "MA_THE"
            rCol.EmptyDataText = String.Empty
            strTest(2) = rCol.DataField

            rCol = New GridBoundColumn()
            rgMain.MasterTableView.Columns.Add(rCol)
            rCol.DataField = "EMP_NAME"
            rCol.UniqueName = "EMP_NAME"
            rCol.HeaderText = Translate("Tên NV")
            rCol.HeaderStyle.Width = 100
            rCol.ReadOnly = True
            rCol.Visible = True
            rCol.SortExpression = "EMP_NAME"
            rCol.EmptyDataText = String.Empty
            strTest(3) = rCol.DataField

            rCol = New GridBoundColumn()
            rgMain.MasterTableView.Columns.Add(rCol)
            rCol.DataField = "ORG_NAME"
            rCol.UniqueName = "ORG_NAME"
            rCol.HeaderText = Translate("Đơn vị")
            rCol.HeaderStyle.Width = 100
            rCol.ReadOnly = True
            rCol.Visible = True
            rCol.SortExpression = "ORG_NAME"
            rCol.EmptyDataText = String.Empty
            strTest(4) = rCol.DataField

            rCol = New GridBoundColumn()
            rgMain.MasterTableView.Columns.Add(rCol)
            rCol.DataField = "TITLE_NAME"
            rCol.UniqueName = "TITLE_NAME"
            rCol.HeaderText = Translate("Chức danh")
            rCol.HeaderStyle.Width = 100
            rCol.ReadOnly = True
            rCol.Visible = True
            rCol.SortExpression = "TITLE_NAME"
            rCol.EmptyDataText = String.Empty
            strTest(5) = rCol.DataField


            Dim i = 6
            For Each Item In lst_col
                Dim col As New GridCheckBoxColumn 'GridBoundColumn '
                col.HeaderText = Item.NAME
                col.DataField = Item.CODE.ToUpper
                col.UniqueName = Item.CODE.ToUpper
                'col.AllowSorting = False
                'col.AllowFiltering = False
                strTest(i) = Item.CODE.ToUpper
                col.HeaderStyle.Width = 80
                rgMain.MasterTableView.Columns.Add(col)
                'stringKey.Add(col.DataField)
                i = i + 1
            Next

            rCol = New GridBoundColumn()
            rgMain.MasterTableView.Columns.Add(rCol)
            rCol.DataField = "REMARK"
            rCol.UniqueName = "REMARK"
            rCol.HeaderText = Translate("Ghi chú màu nón")
            rCol.HeaderStyle.Width = 100
            'rCol.ReadOnly = True
            rCol.Visible = True
            rCol.SortExpression = "REMARK"
            rCol.EmptyDataText = String.Empty
            strTest(i) = rCol.DataField

            rNCol = New GridNumericColumn()
            rgMain.MasterTableView.Columns.Add(rNCol)
            rNCol.DataField = "ITEM_MONEY"
            rNCol.UniqueName = "ITEM_MONEY"
            rNCol.HeaderText = Translate("Tổng tiền BHLĐ")
            rNCol.HeaderStyle.Width = 130
            rNCol.ReadOnly = True
            rNCol.DataFormatString = "{0:N1}"
            rNCol.Visible = True
            rNCol.SortExpression = "ITEM_MONEY"
            rNCol.EmptyDataText = String.Empty
            strTest(i + 1) = rNCol.DataField

            If lst_col.Count > 0 Then
                rgMain.MasterTableView.ClientDataKeyNames = strTest
                rgMain.MasterTableView.DataKeyNames = strTest
            End If
            'rgMain.MasterTableView.ClientDataKeyNames = stringKey.ToArray

            rgMain.MasterTableView.EditMode = GridEditMode.InPlace


            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>Load datasource cho grid</summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <summary>Xử lý sự kiện khi click [OK] xác nhận sẽ Upload file</summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlUpload1_OkClicked(sender As Object, e As EventArgs) Handles ctrlUpload1.OkClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim fileName As String
        Dim rep As New Profile.ProfileBusinessRepository
        Dim rep1 As New ProfileRepository
        Try
            Dim tempPath As String = ConfigurationManager.AppSettings("ExcelFileFolder")
            Dim savepath = Context.Server.MapPath(tempPath)
            Dim countFile As Integer = ctrlUpload1.UploadedFiles.Count
            Dim ds As New DataSet
            If countFile > 0 Then
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(countFile - 1)
                fileName = System.IO.Path.Combine(savepath, file.FileName)
                file.SaveAs(fileName, True)
                Dim ep As New HistaffFrameworkPublic.ExcelPackage
                ds = HistaffFrameworkPublic.ExcelPackage.ReadExcelToDataSet(fileName, False)

            End If
            If ds Is Nothing Then
                Exit Sub
            End If
            TableMapping(ds.Tables(0))


            Dim lst_col = rep1.getListCol()
            Dim col_lst = New List(Of String)
            Dim i = 1
            col_lst.Add("EMP_ID")
            For Each item In lst_col
                col_lst.Add(item.CODE.ToUpper)
                i += 1
            Next
            col_lst.Add("REMARK")

            If rep1.saveBHLD(CDec(Val(rnYear.Value)), col_lst, ds.Tables(0), True) = 1 Then
                ShowMessage(Translate("Import thành công"), NotifyType.Success)
                rgMain.Rebind()
            Else
                ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
                Exit Sub
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi. Kiểm tra lại biểu mẫu Import"), NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>24/07/2017</lastupdate>
    ''' <summary>  Load data len grid </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim rep As New ProfileRepository
        Dim _filter As New HUTravelDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgMain, _filter)
            Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}

            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()


            Dim Travels As DataTable

            If isFull Then
                If Sorts IsNot Nothing Then
                    Travels = rep.GetBHLD(CDec(Val(rnYear.Value)), txtempcode.Text, 0, Integer.MaxValue, 0, _param, Sorts)
                Else
                    Travels = rep.GetBHLD(CDec(Val(rnYear.Value)), txtempcode.Text, 0, Integer.MaxValue, 0, _param)
                End If
            Else
                If Sorts IsNot Nothing Then
                    Travels = rep.GetBHLD(CDec(Val(rnYear.Value)), txtempcode.Text, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, _param, Sorts)
                Else
                    Travels = rep.GetBHLD(CDec(Val(rnYear.Value)), txtempcode.Text, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, _param)
                End If

            End If

            rgMain.VirtualItemCount = MaximumRows
            rgMain.DataSource = Travels


            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
            Return Travels
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function


    ''' <lastupdate>
    ''' 06/07/2017 17:48
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien khi click ctrlFind_CancelClick
    ''' Cap nhat trang thai isLoadPopup = 0
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles ctrlFindEmployeePopup.CancelClicked
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, 0, Nothing, "ctrlFind_CancelClick")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub


    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgMain.CurrentPageIndex = 0
            rgMain.MasterTableView.SortExpressions.Clear()
            rgMain.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub


#End Region

#Region "Custom"

    Private Sub TableMapping(ByVal dtTemp As System.Data.DataTable)
        Try
            ' lấy dữ liệu thô từ excel vào và tinh chỉnh dữ liệu
            dtTemp.Columns(0).ColumnName = "EMP_ID"
            dtTemp.Columns(1).ColumnName = "STT"
            dtTemp.Columns(2).ColumnName = "EMP_CODE"
            dtTemp.Columns(3).ColumnName = "MA_THE"
            dtTemp.Columns(4).ColumnName = "EMP_NAME"
            dtTemp.Columns(5).ColumnName = "NAM"
            dtTemp.Columns(6).ColumnName = "NU"
            dtTemp.Columns(7).ColumnName = "BV48"
            dtTemp.Columns(8).ColumnName = "BV52"

            dtTemp.Columns(9).ColumnName = "T_TRANG"
            dtTemp.Columns(10).ColumnName = "T_XDUONG"
            dtTemp.Columns(11).ColumnName = "T_LACAY"
            dtTemp.Columns(12).ColumnName = "T_XAM"
            dtTemp.Columns(13).ColumnName = "Q_XDCN_NAM33"
            dtTemp.Columns(14).ColumnName = "Q_XDCN_NAM36"
            dtTemp.Columns(15).ColumnName = "Q_XDCN_NU33"
            dtTemp.Columns(16).ColumnName = "Q_XDCN_NU36"

            dtTemp.Columns(17).ColumnName = "GIAY_NAM"
            dtTemp.Columns(18).ColumnName = "GIAY_NU"
            dtTemp.Columns(19).ColumnName = "GIAY300"
            dtTemp.Columns(20).ColumnName = "GIAY500"
            dtTemp.Columns(21).ColumnName = "GIAY550"
            dtTemp.Columns(22).ColumnName = "NON_NAM1"
            dtTemp.Columns(23).ColumnName = "NON_NAM2"
            dtTemp.Columns(24).ColumnName = "NON_NU1"
            dtTemp.Columns(25).ColumnName = "NON_NU2"

            dtTemp.Columns(26).ColumnName = "TAPDE"
            dtTemp.Columns(27).ColumnName = "SM1"
            dtTemp.Columns(28).ColumnName = "SM2"
            dtTemp.Columns(29).ColumnName = "SM3"
            dtTemp.Columns(30).ColumnName = "VEST1"
            dtTemp.Columns(31).ColumnName = "VEST2"
            dtTemp.Columns(32).ColumnName = "BLOUSE"
            dtTemp.Columns(33).ColumnName = "QTVP1"
            dtTemp.Columns(34).ColumnName = "QTVP2"
            dtTemp.Columns(35).ColumnName = "REMARK"
            'XOA DONG TIEU DE VA HEADER
            dtTemp.Rows(0).Delete()
            dtTemp.Rows(1).Delete()
            dtTemp.Rows(3).Delete()
            dtTemp.Rows(4).Delete()
            dtTemp.Rows(5).Delete()
            dtTemp.Rows(6).Delete()
            dtTemp.Rows(7).Delete()
            dtTemp.Rows(8).Delete()
            dtTemp.Rows(9).Delete()

            'XOA NHUNG DONG DU LIEU NULL STT
            Dim rowDel As DataRow
            For i As Integer = 0 To dtTemp.Rows.Count - 1
                If dtTemp.Rows(i).RowState = DataRowState.Deleted OrElse dtTemp.Rows(i).RowState = DataRowState.Detached Then Continue For
                rowDel = dtTemp.Rows(i)
                If rowDel("EMP_CODE").ToString.Trim = "" Then
                    dtTemp.Rows(i).Delete()
                End If
            Next

            dtTemp.AcceptChanges()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function loadToGrid() As Boolean
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim dtError As New DataTable("ERROR")
        Try
            If dtData.Rows.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_ROW), NotifyType.Warning)
                Return False
            End If
            Dim rowError As DataRow
            Dim isError As Boolean = False
            Dim sError As String = String.Empty
            dtError = dtData.Clone
            Dim iRow = 1
            Dim _filter As New WorkingDTO
            Dim rep As New ProfileBusinessRepository
            Dim sp As New ProfileStoreProcedure()
            Dim IBusiness As IProfileBusiness = New ProfileBusinessClient()
            Dim dsData As DataSet = rep.GetHopdongImport()
            'Dim dt_work As New DataTable
            'dt_work = dsData.Tables(4)
            For Each row As DataRow In dtData.Rows
                rowError = dtError.NewRow
                isError = False
                sError = "Chưa nhập mã nhân viên"
                ImportValidate.EmptyValue("EMPLOYEE_CODE", row, rowError, isError, sError)

                Dim empId = rep.CheckEmployee_Exits(row("EMPLOYEE_CODE"))

                If empId = 0 Then
                    sError = "Mã nhân viên không tồn tại"
                    ImportValidate.IsValidTime("EMPLOYEE_NAME", row, rowError, isError, sError)
                Else
                    row("EMPLOYEE_ID") = empId
                End If

                If row("FROM_DATE") Is DBNull.Value OrElse row("FROM_DATE") = "" Then
                    sError = "Chưa nhập ngày bắt đầu"
                    ImportValidate.IsValidTime("FROM_DATE", row, rowError, isError, sError)
                Else
                    If IsDate(row("FROM_DATE")) = False Then
                        sError = "Ngày bắt đầu không đúng định dạng"
                        ImportValidate.IsValidTime("FROM_DATE", row, rowError, isError, sError)
                    End If
                End If

                If row("TO_DATE") Is DBNull.Value OrElse row("TO_DATE") = "" Then
                    sError = "Chưa nhập ngày kết thúc"
                    ImportValidate.IsValidTime("TO_DATE", row, rowError, isError, sError)
                Else
                    If IsDate(row("TO_DATE")) = False Then
                        sError = "Ngày kết thúc không đúng định dạng"
                        ImportValidate.IsValidTime("TO_DATE", row, rowError, isError, sError)
                    End If
                End If

                If row("DAY_NUM") Is DBNull.Value OrElse row("DAY_NUM") = "" Then
                    sError = "Chưa nhập Số ngày đi đường"
                    ImportValidate.IsValidTime("DAY_NUM", row, rowError, isError, sError)
                Else
                    If Not IsNumeric(row("DAY_NUM")) Then
                        rowError("DAY_NUM") = "Chỉ được nhập số"
                        isError = True
                    Else
                        row("DAY_NUM") = row("DAY_NUM").ToString().Replace(",", ".")
                    End If
                End If

                If row("MONEY") IsNot DBNull.Value OrElse row("MONEY") <> "" Then
                    If Not IsNumeric(row("MONEY")) Then
                        rowError("MONEY") = "Chỉ được nhập số"
                        isError = True
                    Else
                        row("MONEY") = row("MONEY").ToString().Replace(",", ".")
                    End If
                End If

                If isError Then
                    rowError("STT") = row("STT").ToString
                    rowError("MATHE") = row("MATHE").ToString
                    If rowError("EMPLOYEE_CODE").ToString = "" Then
                        rowError("EMPLOYEE_CODE") = row("EMPLOYEE_CODE").ToString
                    End If
                    If rowError("EMPLOYEE_NAME").ToString = "" Then
                        rowError("EMPLOYEE_NAME") = row("EMPLOYEE_NAME").ToString
                    End If
                    dtError.Rows.Add(rowError)
                End If
                iRow = iRow + 1
            Next
            If dtError.Rows.Count > 0 Then
                Dim dsErr = New DataSet
                dsErr.Tables.Add(dtError)
                Session("TRAVEL_ERR") = dsErr
                rep.Dispose()
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "javascriptfunction", "ExportReport('Template_Import_Travel_Error');", True)
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
            If isError OrElse (dtError IsNot Nothing AndAlso dtError.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function

    Public Function ExportTemplate(ByVal sReportFileName As String,
                                                    ByVal dsData As DataSet,
                                                    ByVal dtVariable As DataTable,
                                                    ByVal filename As String) As Boolean

        Dim filePath As String
        Dim templatefolder As String

        Dim designer As WorkbookDesigner
        Try

            templatefolder = ConfigurationManager.AppSettings("ReportTemplatesFolder")
            filePath = AppDomain.CurrentDomain.BaseDirectory & templatefolder & "\" & sReportFileName

            'cau hinh lai duong dan tren server
            'filePath = sReportFileName

            If Not File.Exists(filePath) Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "javascriptfunction", "goBack()", True)
                Return False
            End If

            designer = New WorkbookDesigner
            designer.Open(filePath)
            designer.SetDataSource(dsData)

            If dtVariable IsNot Nothing Then
                Dim intCols As Integer = dtVariable.Columns.Count
                For i As Integer = 0 To intCols - 1
                    designer.SetDataSource(dtVariable.Columns(i).ColumnName.ToString(), dtVariable.Rows(0).ItemArray(i).ToString())
                Next
            End If
            designer.Process()
            designer.Workbook.CalculateFormula()
            designer.Workbook.Save(HttpContext.Current.Response, filename & ".xls", Aspose.Words.ContentDisposition.Attachment, New XlsSaveOptions())

        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

#End Region

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSeach.Click

        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgMain.CurrentPageIndex = 0
            rgMain.MasterTableView.SortExpressions.Clear()
            rgMain.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
End Class