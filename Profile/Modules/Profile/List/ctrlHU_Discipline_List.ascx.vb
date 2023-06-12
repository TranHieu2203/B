Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_Discipline_List
    Inherits Common.CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/List/" + Me.GetType().Name.ToString()
    Dim psp As New ProfileStoreProcedure

#Region "Properties"
    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
    Public Property lstDisciplineList As List(Of DisciplineListDTO)
        Get
            Return ViewState(Me.ID & "_lstDisciplineList")
        End Get
        Set(value As List(Of DisciplineListDTO))
            ViewState(Me.ID & "_lstDisciplineList") = value
        End Set
    End Property
#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Hien thi cac control tren page theo trang thai cac control da duoc thiet lap
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            rgMain.SetFilter()
            rgMain.AllowCustomPaging = True
            'rgMain.PageSize = Common.Common.DefaultPageSize
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Goi ham khoi tao gia tri ban dau cho cac control tren page
    ''' khoi tao trang thai cho grid rgAssets voi cac thiet lap filter
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            'If Not IsPostBack Then
            '    ViewConfig(RadPane1)
            '    GirdConfig(rgMain)
            'End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Khoi tao gia tri cho cac control tren page
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create,
                                       ToolbarItem.Edit, ToolbarItem.Seperator,
                                       ToolbarItem.Save, ToolbarItem.Cancel,
                                       ToolbarItem.Delete,
                                       ToolbarItem.Active, ToolbarItem.Deactive, ToolbarItem.Export)
            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
            CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Rebind du lieu cho rgAssets theo case message
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        ' Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
            Else
                'BindData()
                Select Case Message
                    Case "UpdateView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.Rebind()
                        ClearControlValue(txtCode, txtNameVN, txtRemark, cbDatatype, cbObject, nmNumberOrder)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgMain.CurrentPageIndex = 0
                        rgMain.MasterTableView.SortExpressions.Clear()
                        rgMain.Rebind()
                        CurrentState = CommonMessage.STATE_NORMAL
                        ClearControlValue(txtCode, txtNameVN, txtRemark, cbDatatype, cbObject, nmNumberOrder)
                    Case "Cancel"
                        rgMain.MasterTableView.ClearSelectedItems()
                End Select
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 30/06/2017 11:18
    ''' </lastupdate>
    ''' <summary>
    ''' Fill du lieu cho control combobox, thiet lap ngon ngu hien thi cho cac control
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Using rep As New ProfileRepository
                'load kieu du lieu
                Dim dataType As New DataTable
                dataType = rep.GetOtherList("DATA_TYPE", True)
                FillRadCombobox(cbDatatype, dataType, "NAME", "ID", False)

                'load loai danh muc
                Dim DisciplineType As New DataTable
                DisciplineType = rep.GetOtherList("DISCIPLINE_TYPE", True)
                FillRadCombobox(cbTYPE, DisciplineType, "NAME", "ID", False)
                cbTYPE.Enabled = False
                cbTYPE.SelectedValue = DisciplineType.Select("CODE = '6'")(0)("ID")
                'load cấp Kỷ luật
                'Dim DisciplineLevel As New DataTable
                'DisciplineLevel = psp.Get_Discipline_Level(True)
                'FillRadCombobox(cbLevel, DisciplineLevel, "NAME", "ID", False)

                'Load doi tuong Ky luat
                Dim DisciplineObject As New DataTable
                DisciplineObject = rep.GetOtherList("DISCIPLINE_OBJECT", True)
                FillRadCombobox(cbObject, DisciplineObject, "NAME", "ID", False)
            End Using

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CODE", txtCode)
            dic.Add("NAME", txtNameVN)
            dic.Add("DATATYPE_ID", cbDatatype)
            dic.Add("OBJECT_ID", cbObject)
            dic.Add("TYPE_ID", cbTYPE)
            'dic.Add("LEVEL_ID", cbLevel)
            dic.Add("NUMBER_ORDER", nmNumberOrder)
            dic.Add("REMARK", txtRemark)
            ' dic.Add("EXCEL_BOOL", chkExcel)
            Utilities.OnClientRowSelectedChanged(rgMain, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw
        End Try

    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 30/06/2017 15:33
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien command cho control OnMainToolbar
    ''' Cac command la them moi, sua, kich hoat, huy kich hoat, xoa, xuat file, luu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objDisciplineList As New DisciplineListDTO
        Dim rep As New ProfileRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, txtNameVN, txtRemark, cbDatatype, cbObject, nmNumberOrder)
                    rgMain.Rebind()
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgMain.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT

                    UpdateControlState()

                Case CommonMessage.TOOLBARITEM_DELETE


                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item("ID").Text))
                    Next

                    'If Not rep.CheckExistInDatabase(lstDeletes, ProfileCommonTABLE_NAME.HU_STAFF_RANK) Then
                    '    ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Warning)
                    '    Return
                    'End If
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count > 0 Then
                            rgMain.ExportExcel(Server, Response, dtData, "Ky luat")
                        End If
                    End Using
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgMain.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Page.Validate()
                    If Page.IsValid Then
                        objDisciplineList.CODE = txtCode.Text.Trim
                        objDisciplineList.NAME = txtNameVN.Text.Trim
                        objDisciplineList.REMARK = txtRemark.Text.Trim
                        If cbDatatype.SelectedValue <> "" Then
                            objDisciplineList.DATATYPE_ID = cbDatatype.SelectedValue
                        End If


                        If cbObject.SelectedValue <> "" Then
                            objDisciplineList.OBJECT_ID = cbObject.SelectedValue
                        End If

                        If cbTYPE.SelectedValue <> "" Then
                            objDisciplineList.TYPE_ID = cbTYPE.SelectedValue
                        End If



                        objDisciplineList.NUMBER_ORDER = nmNumberOrder.Value
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objDisciplineList.ACTFLG = "A"
                                If rep.InsertDisciplineList(objDisciplineList, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = gID
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objDisciplineList.ID = rgMain.SelectedValue
                                'check exist item asset
                                Dim _validate As New DisciplineListDTO
                                _validate.ID = rgMain.SelectedValue
                                If rep.ValidateDisciplineList(_validate) Then
                                    ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXIST_DATABASE), NotifyType.Error)
                                    ClearControlValue(txtCode, txtNameVN, txtRemark, cbDatatype, cbObject)
                                    rgMain.Rebind()
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                    Exit Sub
                                End If
                                If rep.ModifyDisciplineList(objDisciplineList, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    IDSelect = objDisciplineList.ID
                                    Refresh("UpdateView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    Else
                        ExcuteScript("Resize", "ResizeSplitter(splitterID, pane1ID, pane2ID, validateID, oldSize, 'rgMain')")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ClearControlValue(txtCode, txtNameVN, txtRemark, cbDatatype, cbObject, nmNumberOrder)
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
            End Select
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:37
    ''' </lastupdate>
    ''' <summary>
    ''' Xu lu su kien button command cho control ctrlMessageBox
    ''' Xu ly cho cac command kich hoat, huy kich hoat, xoa
    ''' Cap nhat lai trang thai cac control theo tung command
    ''' Rebind lai du lieu tren grid rgAssets
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_ACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DEACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DEACTIVE
                UpdateControlState()
            End If
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                CurrentState = CommonMessage.STATE_DELETE
                UpdateControlState()
            End If
            ClearControlValue(txtCode, txtNameVN, txtRemark, cbDatatype, cbObject, nmNumberOrder)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:44
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc dung de ket noi du lieu truoc khi bind du lieu va grid
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgMain.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                            CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:46
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly server validate cho control cvalCode
    ''' Call ham AutogenCode(firstChar As String, tableName As String, colName As String)
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cvalCode_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvalCode.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New DisciplineListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If CurrentState = CommonMessage.STATE_EDIT Then
                _validate.CODE = txtCode.Text
                _validate.ID = rgMain.SelectedValue
                args.IsValid = rep.ValidateDisciplineList(_validate)
            Else
                _validate.CODE = txtCode.Text
                args.IsValid = rep.ValidateDisciplineList(_validate)
            End If
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:46
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly server validate cho control cusObject
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusObject_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusObject.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New OtherListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If (cbObject.SelectedValue IsNot Nothing And cbObject.SelectedValue <> "") Then
                _validate.ID = cbObject.SelectedValue
            End If
            _validate.ACTFLG = "A"
            If (cbObject.Text <> "") Then
                _validate.NAME_VN = cbObject.Text
            End If
            _validate.TYPE_CODE = "Discipline_OBJECT"
            args.IsValid = rep.ValidateOtherList(_validate)
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:46
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly server validate cho control cusObject
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    'Private Sub cusLevel_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusLevel.ServerValidate
    '    Dim rep As New ProfileRepository
    '    Dim _validate As New OtherListDTO
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        Dim DisciplineLevel As New DataTable
    '        DisciplineLevel = psp.Get_Discipline_Level(True)
    '        If (DisciplineLevel IsNot Nothing) Then
    '            If (DisciplineLevel.Rows.Count > 0) Then
    '                If CurrentState = CommonMessage.STATE_EDIT Then
    '                    _validate.ID = cbLevel.SelectedValue
    '                    If (cbLevel.Text <> "") Then
    '                        _validate.NAME_VN = cbLevel.Text
    '                    End If
    '                    'check tồn tại cấp Kỷ luật - DisciplineLevel.AsEnumerable()(1).Field(Of String)("NAME")- DisciplineLevel.AsEnumerable()(1).Field(Of Decimal)("ID")
    '                    Dim item = (From u In DisciplineLevel.AsEnumerable()
    '                                Where u.Field(Of Decimal?)("ID") = _validate.ID _
    '                                         And u.Field(Of String)("NAME") = _validate.NAME_VN
    '                                Select u).FirstOrDefault()
    '                    args.IsValid = (Not item Is Nothing)
    '                Else
    '                    If (cbLevel.Text <> "") Then
    '                        _validate.NAME_VN = cbLevel.Text
    '                    Else
    '                        _validate.NAME_VN = ""
    '                    End If
    '                    Dim item = (From u In DisciplineLevel.AsEnumerable()
    '                                Where u.Field(Of String)("NAME") = _validate.NAME_VN
    '                                Select u).FirstOrDefault()
    '                    args.IsValid = (Not item Is Nothing)
    '                End If
    '            Else
    '                args.IsValid = True
    '            End If
    '        Else
    '            args.IsValid = True
    '        End If
    '        rep.Dispose()
    '        _myLog.WriteLog(_myLog._info, _classPath, method,
    '                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '    End Try
    'End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:46
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly server validate cho control cusObject
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusTYPE_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusTYPE.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New OtherListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If (cbTYPE.SelectedValue IsNot Nothing And cbTYPE.SelectedValue <> "") Then
                _validate.ID = cbTYPE.SelectedValue
            End If
            _validate.ACTFLG = "A"
            If (cbTYPE.Text <> "") Then
                _validate.NAME_VN = cbTYPE.Text
            End If
            _validate.TYPE_CODE = "Discipline_TYPE"
            args.IsValid = rep.ValidateOtherList(_validate)
            rep.Dispose()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 30/06/2017 15:46
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly server validate cho control cusObject
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="args"></param>
    ''' <remarks></remarks>
    Private Sub cusDatatype_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cusDatatype.ServerValidate
        Dim rep As New ProfileRepository
        Dim _validate As New OtherListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If cbDatatype.SelectedValue IsNot Nothing And cbDatatype.SelectedValue <> "" Then
                _validate.ID = cbDatatype.SelectedValue
            End If
            _validate.ACTFLG = "A"
            If (cbDatatype.Text <> "") Then
                _validate.NAME_VN = cbDatatype.Text
            End If
            _validate.TYPE_CODE = "DATA_TYPE"
            args.IsValid = rep.ValidateOtherList(_validate)
            rep.Dispose()

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Common"
    '''<lastupdate>
    ''' 30/06/2017 15:54
    ''' </lastupdate>
    ''' <summary>
    ''' Ham tao du lieu filter theo cac tham so dau vao
    ''' mac dinh isFull  load full man hinh hay ko?
    ''' </summary>
    ''' <param name="isFull"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim rep As New ProfileRepository
        Dim _filter As New DisciplineListDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            'SetValueObjectByRadGrid(rgMain, _filter)
            Dim Sorts As String = rgMain.MasterTableView.SortExpressions.GetSortString()
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetDisciplineList(_filter, Sorts).ToTable()
                Else
                    Return rep.GetDisciplineList(_filter).ToTable()
                End If
            Else
                Dim DisciplineLists As List(Of DisciplineListDTO)
                If Sorts IsNot Nothing Then
                    DisciplineLists = rep.GetDisciplineList(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows, Sorts)
                Else
                    DisciplineLists = rep.GetDisciplineList(_filter, rgMain.CurrentPageIndex, rgMain.PageSize, MaximumRows)
                End If
                rep.Dispose()
                lstDisciplineList = DisciplineLists
                rgMain.VirtualItemCount = MaximumRows
                rgMain.DataSource = DisciplineLists
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    ''' <lastupdate>
    ''' 30/06/2017 15:52
    ''' </lastupdate>
    ''' <summary>
    ''' Cap nhat trang thai control tren page theo trang thai them moi, sua hay default, kich hoat
    ''' huy kich hoat, update trang thai cac item tren toolbar theo tung truong hop 
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW

                    EnabledGridNotPostback(rgMain, False)
                    Utilities.EnableRadCombo(cbDatatype, True)

                    Utilities.EnableRadCombo(cbObject, True)
                    'Utilities.EnableRadCombo(cbTYPE, True)
                    txtNameVN.ReadOnly = False
                    txtCode.ReadOnly = False
                    txtRemark.ReadOnly = False
                    nmNumberOrder.ReadOnly = False
                Case CommonMessage.STATE_NORMAL
                    EnabledGridNotPostback(rgMain, True)

                    txtNameVN.ReadOnly = True
                    txtCode.ReadOnly = True
                    txtRemark.ReadOnly = True
                    nmNumberOrder.ReadOnly = True
                    Utilities.EnableRadCombo(cbDatatype, False)

                    Utilities.EnableRadCombo(cbObject, False)
                    'Utilities.EnableRadCombo(cbTYPE, False)

                Case CommonMessage.STATE_EDIT

                    EnabledGridNotPostback(rgMain, False)

                    txtNameVN.ReadOnly = False
                    txtCode.ReadOnly = True
                    txtRemark.ReadOnly = False
                    nmNumberOrder.ReadOnly = False
                    Utilities.EnableRadCombo(cbDatatype, True)

                    Utilities.EnableRadCombo(cbObject, True)
                   ' Utilities.EnableRadCombo(cbTYPE, True)


                Case CommonMessage.STATE_DELETE
                    Dim str As String
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.DeleteDisciplineList(lstDeletes, str) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                        UpdateControlState()
                    End If

                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveDisciplineList(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgMain.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgMain.SelectedItems(idx)
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next
                    If rep.ActiveDisciplineList(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgMain.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
            End Select
            rep.Dispose()
            txtCode.Focus()
            UpdateToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    '''<lastupdate>
    ''' 30/06/2017 15:54
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc cap nhat trang thai cho control toolbar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateToolbarState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ChangeToolbarState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
#End Region
End Class