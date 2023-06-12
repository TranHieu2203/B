Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlSE_System_Maintain
    Inherits CommonView
    Protected WithEvents UserView As ViewBase
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

    Property lstFunctions As List(Of SE_SYSTEM_MAINTAINDTO)
        Get
            Return ViewState(Me.ID & "_lstFunctions")
        End Get
        Set(ByVal value As List(Of SE_SYSTEM_MAINTAINDTO))
            ViewState(Me.ID & "_lstFunctions") = value
        End Set
    End Property

    Property ActiveFunctions As List(Of SE_SYSTEM_MAINTAINDTO)
        Get
            Return ViewState(Me.ID & "_ActiveFunctions")
        End Get
        Set(ByVal value As List(Of SE_SYSTEM_MAINTAINDTO))
            ViewState(Me.ID & "_ActiveFunctions") = value
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

    Property lstDeleteDecimals As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_lstDeleteDecimals")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_lstDeleteDecimals") = value
        End Set
    End Property

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgListFunctions.SetFilter()
            rgListFunctions.AllowCustomPaging = True
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = rtbMainToolBar
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, _
                 ToolbarItem.Edit, _
                 ToolbarItem.Seperator, _
                 ToolbarItem.Save, _
                 ToolbarItem.Cancel, _
                 ToolbarItem.Seperator, _
                 ToolbarItem.Export, _
                 ToolbarItem.Delete)
            CType(Me.MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                rgListFunctions.Rebind()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim rep As New CommonRepository


            Dim dic As New Dictionary(Of String, Control)
            dic.Add("CONTRACT_MAINTAIN", rtCONTRACT_MAINTAIN)
            dic.Add("EFFECT_DATE", rdEFFECT_DATE)
            dic.Add("EXPIRE_TDATE", rdEXPIRE_TDATE)
            dic.Add("SIGN_DATE", rdSIGN_DATE)
            dic.Add("NOTE", txtNote)
            Utilities.OnClientRowSelectedChanged(rgListFunctions, dic)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objFunction As New SE_SYSTEM_MAINTAINDTO
        Dim rep As New CommonRepository

        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW 'Thiet lap trang thai là them moi
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgListFunctions.SelectedItems.Count = 0 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgListFunctions.SelectedItems.Count > 1 Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgListFunctions.SelectedItems.Count > 0 Then
                        CurrentState = CommonMessage.STATE_EDIT
                        UpdateControlState()
                    End If

                Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE
                    Dim lstActiveFunctions As New List(Of SE_SYSTEM_MAINTAINDTO)
                    Dim sActive As String = ""
                    Dim bCheck As Boolean = True
                    For intIndex As Int16 = 0 To rgListFunctions.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgListFunctions.SelectedItems(intIndex)
                        If sActive = "" Then
                            sActive = item("ACTFLG").Text
                        ElseIf sActive <> item("ACTFLG").Text Then
                            bCheck = False
                            Exit For
                        End If
                        'lstActiveFunctions.Add(New RegisterUserDTO With {.ID = Decimal.Parse(item("ID").Text),
                        '                                         .ACTFLG = sActive})
                    Next
                    ActiveFunctions = lstActiveFunctions 'Đưa danh sách vào property để thực hiện Active/DeActive trong  UpdateControlState
                    If lstActiveFunctions.Count > 0 Then
                        If Not bCheck Then
                            Me.ShowMessage("Các bản ghi không cùng 1 trạng thái, kiểm tra lại!", NotifyType.Warning)
                            Exit Sub
                        End If
                        If sActive = "A" Then
                            If CType(e.Item, RadToolBarButton).CommandName = CommonMessage.TOOLBARITEM_ACTIVE Then
                                Me.ShowMessage("Các bản ghi đã ở trạng thái áp dụng trước đó, kiểm tra lại!", NotifyType.Warning)
                                Exit Sub
                            End If
                        Else
                            If CType(e.Item, RadToolBarButton).CommandName = CommonMessage.TOOLBARITEM_DEACTIVE Then
                                Me.ShowMessage("Các bản ghi đã ở trạng thái ngưng áp dụng trước đó, kiểm tra lại!", NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        If sActive = "A" Then
                            ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)

                        Else
                            ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)

                        End If
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_ACTIVE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    Dim lstDeletes As New List(Of Decimal)
                    For idx = 0 To rgListFunctions.SelectedItems.Count - 1
                        Dim item As GridDataItem = rgListFunctions.SelectedItems(idx)
                        lstDeletes.Add(Decimal.Parse(item("ID").Text))
                    Next

                    lstDeleteDecimals = lstDeletes
                    If lstDeleteDecimals.Count > 0 Then
                        ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                    End If
                Case CommonMessage.TOOLBARITEM_EXPORT
                    GridExportExcel(rgListFunctions, "RegisterUser")
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objFunction.CONTRACT_MAINTAIN = rtCONTRACT_MAINTAIN.Text
                        objFunction.EFFECT_DATE = rdEFFECT_DATE.SelectedDate
                        objFunction.EXPIRE_TDATE = rdEXPIRE_TDATE.SelectedDate
                        objFunction.SIGN_DATE = rdSIGN_DATE.SelectedDate
                        objFunction.NOTE = txtNote.Text
                        If Session("ConfirmCodeAccuracy") = 2 Then
                            rwPopup.VisibleOnPageLoad = False
                        End If
                        If CurrentState = CommonMessage.STATE_NEW Then ' Trường hợp thêm mới
                            If rep.CHECK_DUP_SYSTEM_MAINTAIN(objFunction.EFFECT_DATE) > 0 Then
                                ShowMessage("Trùng ngày hiệu lực", Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                            If Session("ConfirmCodeAccuracy") = 0 Then
                                rwPopup.VisibleOnPageLoad = True
                                rwPopup.NavigateUrl = "/Dialog.aspx?mid=Common&fid=ctrlConfirmCodeAccuracy&group=Secure"
                            End If
                            If Session("ConfirmCodeAccuracy") = 1 Then
                                rwPopup.VisibleOnPageLoad = False
                                If rep.InsertSystem_Maintain(objFunction) Then


                                    Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&()"
                                    Dim sb As New StringBuilder
                                    Dim r As New Random
                                    Dim cnt As Integer = r.Next(24, 24)
                                    For i As Integer = 1 To cnt
                                        Dim idx As Integer = r.Next(0, s.Length)
                                        sb.Append(s.Substring(idx, 1))
                                    Next
                                    Dim s1 = sb.ToString
                                    CommonConfig.dicConfig_save("CODE_ACCURACY") = sb.ToString
                                    CommonConfig.SetGeneralConfig()


                                    'Show message success
                                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                    'IDSelect = gID
                                    rgListFunctions.CurrentPageIndex = 0
                                    rgListFunctions.MasterTableView.SortExpressions.Clear()
                                    rgListFunctions.Rebind()
                                    'SelectedItemDataGridByKey(rgListFunctions, IDSelect, )
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            End If

                        Else ' Trường hợp sửa
                            If rep.CHECK_DUP_SYSTEM_MAINTAIN(objFunction.EFFECT_DATE, rgListFunctions.SelectedValue) > 0 Then
                                ShowMessage("Trùng ngày hiệu lực", Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                            If Session("ConfirmCodeAccuracy") = 0 Then
                                rwPopup.VisibleOnPageLoad = True
                                rwPopup.NavigateUrl = "/Dialog.aspx?mid=Common&fid=ctrlConfirmCodeAccuracy&group=Secure"
                            End If
                            If Session("ConfirmCodeAccuracy") = 1 Then
                                rwPopup.VisibleOnPageLoad = False

                                Dim s As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&()"
                                Dim sb As New StringBuilder
                                Dim r As New Random
                                Dim cnt As Integer = r.Next(24, 24)
                                For i As Integer = 1 To cnt
                                    Dim idx As Integer = r.Next(0, s.Length)
                                    sb.Append(s.Substring(idx, 1))
                                Next
                                Dim s1 = sb.ToString
                                CommonConfig.dicConfig_save("CODE_ACCURACY") = sb.ToString
                                CommonConfig.SetGeneralConfig()
                                objFunction.ID = rgListFunctions.SelectedValue
                                If rep.ModifySystem_Maintain(objFunction) Then
                                    'Show message success
                                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                                    rgListFunctions.Rebind()
                                    SelectedItemDataGridByKey(rgListFunctions, IDSelect, , rgListFunctions.CurrentPageIndex)
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            End If
                        End If
                    Else
                        ExcuteScript("Resize", "ResizeSplitter()")
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    rwPopup.VisibleOnPageLoad = False
                    CurrentState = CommonMessage.STATE_NORMAL
                    UpdateControlState()
                    ExcuteScript("Resize", "ResizeSplitter()")

            End Select
            Session.Remove("ConfirmCodeAccuracy")
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.ActionName = CommonMessage.TOOLBARITEM_ACTIVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New CommonRepository
                'If ActiveFunctions(0).ACTFLG = "A" Then
                '    If rep.ActiveFunctions(ActiveFunctions, "I") Then
                '        ActiveFunctions = Nothing
                '        Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                '    Else
                '        Me.ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                '    End If
                'Else
                '    If rep.ActiveFunctions(ActiveFunctions, "A") Then
                '        ActiveFunctions = Nothing
                '        Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                '    Else
                '        Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                '    End If
                'End If
                rgListFunctions.Rebind()
                SelectedItemDataGridByKey(rgListFunctions, IDSelect, , rgListFunctions.CurrentPageIndex)
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState()
            ElseIf e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Dim rep As New CommonRepository
                Dim strError As String = ""
                If rep.DeleteRegisterUser(lstDeleteDecimals) Then
                    lstDeleteDecimals = Nothing
                    Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                Else
                    If strError = "EXIST" Then
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_IS_USING), NotifyType.Error)
                    Else
                        Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                End If
                rgListFunctions.Rebind()
                CurrentState = CommonMessage.STATE_NORMAL
                UpdateControlState()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgListFunctions.NeedDataSource
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


#End Region

#Region "Custom"
    Protected Sub CreateDataFilter()
        Dim rep As New CommonRepository
        Dim obj As New SE_SYSTEM_MAINTAINDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim MaximumRows As Integer
            SetValueObjectByRadGrid(rgListFunctions, obj)
            Dim Sorts As String = rgListFunctions.MasterTableView.SortExpressions.GetSortString()
            If Sorts IsNot Nothing Then
                Me.lstFunctions = rep.GetSystem_Maintain(obj, rgListFunctions.CurrentPageIndex, rgListFunctions.PageSize, MaximumRows, Sorts)
            Else
                Me.lstFunctions = rep.GetSystem_Maintain(obj, rgListFunctions.CurrentPageIndex, rgListFunctions.PageSize, MaximumRows)
            End If
            rgListFunctions.VirtualItemCount = MaximumRows
            rgListFunctions.DataSource = Me.lstFunctions
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Select Case CurrentState
                ''-----------Tab Function----------------'
                Case CommonMessage.STATE_NORMAL
                    rtCONTRACT_MAINTAIN.Text = ""
                    rdEXPIRE_TDATE.SelectedDate = Nothing
                    txtNote.Text = ""
                    rdSIGN_DATE.SelectedDate = Nothing
                    rdEFFECT_DATE.SelectedDate = Nothing

                    rtCONTRACT_MAINTAIN.ReadOnly = True
                    rdEXPIRE_TDATE.Enabled = False
                    rdSIGN_DATE.Enabled = False
                    rdEFFECT_DATE.Enabled = False
                    txtNote.ReadOnly = True
                    EnabledGridNotPostback(rgListFunctions, True)
                Case CommonMessage.STATE_NEW
                    rtCONTRACT_MAINTAIN.Text = ""
                    rdEXPIRE_TDATE.SelectedDate = Nothing
                    txtNote.Text = ""
                    rdSIGN_DATE.SelectedDate = Nothing
                    rdEFFECT_DATE.SelectedDate = Nothing
                    rtCONTRACT_MAINTAIN.ReadOnly = False
                    rdEXPIRE_TDATE.Enabled = True
                    rdSIGN_DATE.Enabled = True
                    rdEFFECT_DATE.Enabled = True
                    txtNote.ReadOnly = False
                    EnabledGridNotPostback(rgListFunctions, True)
                Case CommonMessage.STATE_EDIT
                    EnabledGridNotPostback(rgListFunctions, False)
                    rtCONTRACT_MAINTAIN.ReadOnly = False
                    rdEXPIRE_TDATE.Enabled = True
                    rdSIGN_DATE.Enabled = True
                    rdEFFECT_DATE.Enabled = True
                    txtNote.ReadOnly = False
                    rdEFFECT_DATE.Enabled = True
            End Select
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