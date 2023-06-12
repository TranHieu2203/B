Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Ionic.Crc
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_SalItemsPercent
    Inherits Common.CommonView

    Dim log As UserLog = LogHelper.GetUserLog
    Public Overrides Property MustAuthorize As Boolean = False

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Setting" + Me.GetType().Name.ToString()

#Region "Property"

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            rgData.AllowCustomPaging = True
            rgData.SetFilter()
            InitControl()
            If Not IsPostBack Then
                ViewConfig(RadPane1)
                GirdConfig(rgData)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow

        Try
            Me.ctrlMessageBox.Listener = Me
            Me.MainToolBar = tbarMain

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Delete,
                                       ToolbarItem.Active, ToolbarItem.Deactive, ToolbarItem.Export)

            CType(MainToolBar.Items(2), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

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
                        rgData.Rebind()
                        ClearControlValue(hiOrgdID, txtOrgName, rdEffectDate, rnLuongCB, rnXangXe, rnDienThoai, rnLuongBS, rnYTCLCV, rnOther1, rnOther2, rnOther3, rnOther4, rnOther5, txtNote, cboActflg,
                          chkLuongCB, chkOther1, chkOther2, chkOther3, chkOther4, chkOther5, chkXangXe, chkYTCLCV, chkLuongBS, chkDienThoai, chkUnuseRatio, chkViolate, chkUnionPercent, chkUnionPermanent, rnUnionMoney)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        ClearControlValue(hiOrgdID, txtOrgName, rdEffectDate, rnLuongCB, rnXangXe, rnDienThoai, rnLuongBS, rnYTCLCV, rnOther1, rnOther2, rnOther3, rnOther4, rnOther5, txtNote, cboActflg,
                          chkLuongCB, chkOther1, chkOther2, chkOther3, chkOther4, chkOther5, chkXangXe, chkYTCLCV, chkLuongBS, chkDienThoai, chkUnuseRatio, chkViolate, chkUnionPercent, chkUnionPermanent, rnUnionMoney)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                        ClearControlValue(hiOrgdID, txtOrgName, rdEffectDate, rnLuongCB, rnXangXe, rnDienThoai, rnLuongBS, rnYTCLCV, rnOther1, rnOther2, rnOther3, rnOther4, rnOther5, txtNote, cboActflg,
                          chkLuongCB, chkOther1, chkOther2, chkOther3, chkOther4, chkOther5, chkXangXe, chkYTCLCV, chkLuongBS, chkDienThoai, chkUnuseRatio, chkViolate, chkUnionPercent, chkUnionPermanent, rnUnionMoney)
                End Select
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    ctrlOrg.Enabled = False
                    rgData.Enabled = False
                    EnableControlAll(True, rdEffectDate, rnLuongCB, rnXangXe, rnDienThoai, rnLuongBS, rnYTCLCV, rnOther1, rnOther2, rnOther3, rnOther4, rnOther5, txtNote, cboActflg,
                          chkLuongCB, chkOther1, chkOther2, chkOther3, chkOther4, chkOther5, chkXangXe, chkYTCLCV, chkLuongBS, chkDienThoai, chkUnuseRatio, chkViolate, chkUnionPercent, chkUnionPermanent, rnUnionMoney)
                Case CommonMessage.STATE_NORMAL
                    ctrlOrg.Enabled = True
                    rgData.Enabled = True
                    EnableControlAll(False, rdEffectDate, rnLuongCB, rnXangXe, rnDienThoai, rnLuongBS, rnYTCLCV, rnOther1, rnOther2, rnOther3, rnOther4, rnOther5, txtNote, cboActflg,
                          chkLuongCB, chkOther1, chkOther2, chkOther3, chkOther4, chkOther5, chkXangXe, chkYTCLCV, chkLuongBS, chkDienThoai, chkUnuseRatio, chkViolate, chkUnionPercent, chkUnionPermanent, rnUnionMoney)
                Case CommonMessage.STATE_EDIT
                    ctrlOrg.Enabled = False
                    rgData.Enabled = False
                    EnableControlAll(True, rdEffectDate, rnLuongCB, rnXangXe, rnDienThoai, rnLuongBS, rnYTCLCV, rnOther1, rnOther2, rnOther3, rnOther4, rnOther5, txtNote, cboActflg,
                          chkLuongCB, chkOther1, chkOther2, chkOther3, chkOther4, chkOther5, chkXangXe, chkYTCLCV, chkLuongBS, chkDienThoai, chkUnuseRatio, chkViolate, chkUnionPercent, chkUnionPermanent, rnUnionMoney)

                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For Each item In rgData.SelectedItems
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DeleteSalItemsPercent(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                        UpdateControlState()
                    End If

                Case CommonMessage.STATE_ACTIVE
                    Dim lstDeletes As New List(Of Decimal)

                    For Each item In rgData.SelectedItems
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.ActiveSalItemsPercent(lstDeletes, "A") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If

                Case CommonMessage.STATE_DEACTIVE
                    Dim lstDeletes As New List(Of Decimal)

                    For Each item In rgData.SelectedItems
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.ActiveSalItemsPercent(lstDeletes, "I") Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        CurrentState = CommonMessage.STATE_NORMAL
                        rgData.Rebind()
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Warning)
                    End If
            End Select
            rep.Dispose()

            ChangeToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objData As New SalaryItemsPercentDTO
        Dim gID As Decimal
        Dim result As Int32 = 0
        Dim rep As New ProfileRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(hiOrgdID, txtOrgName, rdEffectDate, rnLuongCB, rnXangXe, rnDienThoai, rnLuongBS, rnYTCLCV, rnOther1, rnOther2, rnOther3, rnOther4, rnOther5, txtNote, cboActflg,
                          chkLuongCB, chkOther1, chkOther2, chkOther3, chkOther4, chkOther5, chkXangXe, chkYTCLCV, chkLuongBS, chkDienThoai)
                    txtOrgName.Text = ctrlOrg.CurrentText
                    hiOrgdID.Value = ctrlOrg.CurrentValue
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim item As GridDataItem = rgData.SelectedItems(0)
                    'If item.GetDataKeyValue("ACTFLG") = "A" Then
                    '    ShowMessage("Bản ghi đang áp dụng", NotifyType.Warning)
                    '    Exit Sub
                    'End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_ACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    For Each item In rgData.SelectedItems
                        If item.GetDataKeyValue("ACTFLG") = "A" Then
                            ShowMessage("Tồn tại bản ghi đang áp dụng", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_ACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_ACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_DEACTIVE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each item In rgData.SelectedItems
                        If item.GetDataKeyValue("ACTFLG") = "I" Then
                            ShowMessage("Tồn tại bản ghi đang ngừng áp dụng", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next

                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DEACTIVE)
                    ctrlMessageBox.ActionName = CommonMessage.ACTION_DEACTIVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If Not chkUnionPermanent.Checked AndAlso Not chkUnionPercent.Checked Then
                            ShowMessage("Chưa chọn Phí công đoàn Theo tỷ lệ hoặc Cố định", NotifyType.Warning)
                            Exit Sub
                        End If
                        If chkUnionPermanent.Checked AndAlso rnUnionMoney.Value Is Nothing Then
                            ShowMessage("Chưa nhập tiền phí công đoàn", NotifyType.Warning)
                            Exit Sub
                        End If
                        Dim luongCB = If(IsNumeric(rnLuongCB.Value), rnLuongCB.Value, 0)
                        Dim luongBS = If(IsNumeric(rnLuongBS.Value), rnLuongBS.Value, 0)
                        Dim xangxe = If(IsNumeric(rnXangXe.Value), rnXangXe.Value, 0)
                        Dim dienthoai = If(IsNumeric(rnDienThoai.Value), rnDienThoai.Value, 0)
                        Dim ytclcv = If(IsNumeric(rnYTCLCV.Value), rnYTCLCV.Value, 0)
                        Dim other1 = If(IsNumeric(rnOther1.Value), rnOther1.Value, 0)
                        Dim other2 = If(IsNumeric(rnOther2.Value), rnOther2.Value, 0)
                        Dim other3 = If(IsNumeric(rnOther3.Value), rnOther3.Value, 0)
                        Dim other4 = If(IsNumeric(rnOther4.Value), rnOther4.Value, 0)
                        Dim other5 = If(IsNumeric(rnOther5.Value), rnOther5.Value, 0)
                        Dim total = luongCB + luongBS + xangxe + dienthoai + ytclcv + other1 + other2 + other3 + other4 + other5
                        If total >= 100 Then
                            ShowMessage("Tổng các khoản không được > hoặc = 100", NotifyType.Warning)
                            Exit Sub
                        End If
                        If rnUnionMoney.Value IsNot Nothing Then
                            Dim lstPayment = rep.GetPaymentListAll()
                            Dim objPayment = (From p In lstPayment Where p.CODE = "MLTTC" AndAlso p.EFFECTIVE_DATE <= rdEffectDate.SelectedDate).FirstOrDefault
                            If objPayment Is Nothing Then
                                ShowMessage("Chưa thiết lập mức lương cơ sở", NotifyType.Warning)
                                Exit Sub
                            Else
                                If rnUnionMoney.Value > ((objPayment.VALUE / 100) * 10) Then
                                    ShowMessage("Phí công đoàn không được lơn hơn 10% mức lương cơ sở!", NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rnUnionMoney.Value <= 0 Then
                                    ShowMessage("Phí công đoàn phải lơn hơn 0", NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                        End If
                        If hiOrgdID.Value = "" Then
                            ShowMessage("Chưa chọn phòng ban", NotifyType.Warning)
                            Exit Sub
                        End If
                        If Not chkUnuseRatio.Checked AndAlso Not chkDienThoai.Checked AndAlso Not chkLuongBS.Checked AndAlso Not chkLuongCB.Checked AndAlso Not chkOther1.Checked _
                            AndAlso Not chkOther2.Checked AndAlso Not chkOther3.Checked AndAlso Not chkOther4.Checked AndAlso Not chkOther5.Checked AndAlso Not chkXangXe.Checked AndAlso Not chkYTCLCV.Checked Then
                            ShowMessage("Phải có ít nhất một trường check LEFT", NotifyType.Warning)
                            Exit Sub
                        End If
                        If chkUnuseRatio.Checked AndAlso rnLuongCB.Value Is Nothing AndAlso rnXangXe.Value Is Nothing AndAlso rnOther5.Value Is Nothing AndAlso rnDienThoai.Value Is Nothing _
                             AndAlso rnLuongBS.Value Is Nothing AndAlso rnYTCLCV.Value Is Nothing AndAlso rnOther1.Value Is Nothing AndAlso rnOther2.Value Is Nothing AndAlso rnOther3.Value Is Nothing _
                             AndAlso rnOther4.Value Is Nothing Then
                            ShowMessage("Điền 0 vào những khoản có sử dụng", NotifyType.Warning)
                            Exit Sub
                        ElseIf chkUnuseRatio.Checked Then
                            If rnLuongCB.Value <> 0 Then
                                ShowMessage("Chỉ được điền 0 vào những khoản có sử dụng", NotifyType.Warning)
                                Exit Sub
                            End If
                            If rnXangXe.Value <> 0 Then
                                ShowMessage("Chỉ được điền 0 vào những khoản có sử dụng", NotifyType.Warning)
                                Exit Sub
                            End If
                            If rnOther5.Value <> 0 Then
                                ShowMessage("Chỉ được điền 0 vào những khoản có sử dụng", NotifyType.Warning)
                                Exit Sub
                            End If
                            If rnDienThoai.Value <> 0 Then
                                ShowMessage("Chỉ được điền 0 vào những khoản có sử dụng", NotifyType.Warning)
                                Exit Sub
                            End If
                            If rnLuongBS.Value <> 0 Then
                                ShowMessage("Chỉ được điền 0 vào những khoản có sử dụng", NotifyType.Warning)
                                Exit Sub
                            End If
                            If rnYTCLCV.Value <> 0 Then
                                ShowMessage("Chỉ được điền 0 vào những khoản có sử dụng", NotifyType.Warning)
                                Exit Sub
                            End If
                            If rnOther1.Value <> 0 Then
                                ShowMessage("Chỉ được điền 0 vào những khoản có sử dụng", NotifyType.Warning)
                                Exit Sub
                            End If
                            If rnOther2.Value <> 0 Then
                                ShowMessage("Chỉ được điền 0 vào những khoản có sử dụng", NotifyType.Warning)
                                Exit Sub
                            End If
                            If rnOther3.Value <> 0 Then
                                ShowMessage("Chỉ được điền 0 vào những khoản có sử dụng", NotifyType.Warning)
                                Exit Sub
                            End If
                            If rnOther4.Value <> 0 Then
                                ShowMessage("Chỉ được điền 0 vào những khoản có sử dụng", NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        objData.ORG_ID = hiOrgdID.Value
                        objData.EFFECT_DATE = rdEffectDate.SelectedDate
                        If chkLuongCB.Checked Then
                            objData.LUONGCB = "LEFT"
                        Else
                            If IsNumeric(rnLuongCB.Value) Then
                                objData.LUONGCB = rnLuongCB.Value.ToString.Replace(",", ".")
                            End If
                        End If

                        If chkXangXe.Checked Then
                            objData.XANGXE = "LEFT"
                        Else
                            If IsNumeric(rnXangXe.Value) Then
                                objData.XANGXE = rnXangXe.Value.ToString.Replace(",", ".")
                            End If
                        End If

                        If chkDienThoai.Checked Then
                            objData.DIENTHOAI = "LEFT"
                        Else
                            If IsNumeric(rnDienThoai.Value) Then
                                objData.DIENTHOAI = rnDienThoai.Value.ToString.Replace(",", ".")
                            End If
                        End If

                        If chkLuongBS.Checked Then
                            objData.LUONGBS = "LEFT"
                        Else
                            If IsNumeric(rnLuongBS.Value) Then
                                objData.LUONGBS = rnLuongBS.Value.ToString.Replace(",", ".")
                            End If
                        End If

                        If chkYTCLCV.Checked Then
                            objData.THUONGYTCLCV = "LEFT"
                        Else
                            If IsNumeric(rnYTCLCV.Value) Then
                                objData.THUONGYTCLCV = rnYTCLCV.Value.ToString.Replace(",", ".")
                            End If
                        End If

                        If chkOther1.Checked Then
                            objData.OTHER1 = "LEFT"
                        Else
                            If IsNumeric(rnOther1.Value) Then
                                objData.OTHER1 = rnOther1.Value.ToString.Replace(",", ".")
                            End If
                        End If

                        If chkOther2.Checked Then
                            objData.OTHER2 = "LEFT"
                        Else
                            If IsNumeric(rnOther2.Value) Then
                                objData.OTHER2 = rnOther2.Value.ToString.Replace(",", ".")
                            End If
                        End If

                        If chkOther3.Checked Then
                            objData.OTHER3 = "LEFT"
                        Else
                            If IsNumeric(rnOther3.Value) Then
                                objData.OTHER3 = rnOther3.Value.ToString.Replace(",", ".")
                            End If
                        End If

                        If chkOther4.Checked Then
                            objData.OTHER4 = "LEFT"
                        Else
                            If IsNumeric(rnOther4.Value) Then
                                objData.OTHER4 = rnOther4.Value.ToString.Replace(",", ".")
                            End If
                        End If

                        If chkOther5.Checked Then
                            objData.OTHER5 = "LEFT"
                        Else
                            If IsNumeric(rnOther5.Value) Then
                                objData.OTHER5 = rnOther5.Value.ToString.Replace(",", ".")
                            End If
                        End If
                        If cboActflg.SelectedValue <> "" Then
                            If cboActflg.SelectedValue = 1 Then
                                objData.ACTFLG = "A"
                            Else
                                objData.ACTFLG = "I"
                            End If
                        End If

                        objData.NOTE = txtNote.Text.Trim
                        objData.UNUSE_RATIO = chkUnuseRatio.Checked
                        objData.VIOLATE = chkViolate.Checked
                        If rnUnionMoney.Value IsNot Nothing Then
                            objData.UNION_MONEY = rnUnionMoney.Value
                        End If
                        objData.UNION_PERCENT = chkUnionPercent.Checked
                        objData.UNION_PERMANENT = chkUnionPermanent.Checked

                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.ValidateSalItemsPercent(objData) Then
                                    ShowMessage("Dữ liệu đã tồn tại", NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.InsertSalItemsPercent(objData, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                    Refresh("InsertView")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objData.ID = rgData.SelectedValue
                                If rep.ValidateSalItemsPercent(objData) Then
                                    ShowMessage("Dữ liệu đã tồn tại", NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.ModifySalItemsPercent(objData, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    Refresh("InsertView")
                                    UpdateControlState()
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each item In rgData.SelectedItems
                        If item.GetDataKeyValue("ACTFLG") = "A" Then
                            ShowMessage("Tồn tại bản ghi đang áp dụng", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    Refresh("Cancel")
                    UpdateControlState()
                    'Exit Sub
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "SalItemsPercent")
                            Exit Sub
                        End If
                    End Using
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If e.ButtonID = MessageBoxButtonType.ButtonYes Then

                Select Case e.ActionName
                    Case CommonMessage.ACTION_ACTIVE
                        CurrentState = CommonMessage.STATE_ACTIVE
                    Case CommonMessage.ACTION_DEACTIVE
                        CurrentState = CommonMessage.STATE_DEACTIVE
                    Case CommonMessage.TOOLBARITEM_DELETE
                        CurrentState = CommonMessage.STATE_DELETE
                End Select
                UpdateControlState()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgData.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            CreateDataFilter()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub rgData_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rgData.SelectedIndexChanged
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ClearControlValue(hiOrgdID, txtOrgName, rdEffectDate, rnLuongCB, rnXangXe, rnDienThoai, rnLuongBS, rnYTCLCV, rnOther1, rnOther2, rnOther3, rnOther4, rnOther5, txtNote, cboActflg,
                          chkLuongCB, chkOther1, chkOther2, chkOther3, chkOther4, chkOther5, chkXangXe, chkYTCLCV, chkLuongBS, chkDienThoai, chkUnuseRatio, chkViolate, chkUnionPercent, chkUnionPermanent, rnUnionMoney)
            If rgData.SelectedItems.Count = 1 Then
                Dim item As GridDataItem = rgData.SelectedItems(0)

                txtOrgName.Text = item.GetDataKeyValue("ORG_NAME")
                hiOrgdID.Value = item.GetDataKeyValue("ORG_ID")
                rdEffectDate.SelectedDate = item.GetDataKeyValue("EFFECT_DATE")
                If item.GetDataKeyValue("LUONGCB") = "LEFT" Then
                    chkLuongCB.Checked = True
                    chkLuongCB_CheckedChanged(Nothing, Nothing)
                Else
                    rnLuongCB.Text = If(item.GetDataKeyValue("LUONGCB") IsNot Nothing, item.GetDataKeyValue("LUONGCB").ToString.Replace(",", "."), "")
                End If

                If item.GetDataKeyValue("XANGXE") = "LEFT" Then
                    chkXangXe.Checked = True
                    chkXangXe_CheckedChanged(Nothing, Nothing)
                Else
                    rnXangXe.Text = If(item.GetDataKeyValue("XANGXE") IsNot Nothing, item.GetDataKeyValue("XANGXE").ToString.Replace(",", "."), "")
                End If

                If item.GetDataKeyValue("DIENTHOAI") = "LEFT" Then
                    chkDienThoai.Checked = True
                    chkDienThoai_CheckedChanged(Nothing, Nothing)
                Else
                    rnDienThoai.Text = If(item.GetDataKeyValue("DIENTHOAI") IsNot Nothing, item.GetDataKeyValue("DIENTHOAI").ToString.Replace(",", "."), "")
                End If

                If item.GetDataKeyValue("LUONGBS") = "LEFT" Then
                    chkLuongBS.Checked = True
                    chkLuongBS_CheckedChanged(Nothing, Nothing)
                Else
                    rnLuongBS.Text = If(item.GetDataKeyValue("LUONGBS") IsNot Nothing, item.GetDataKeyValue("LUONGBS").ToString.Replace(",", "."), "")
                End If

                If item.GetDataKeyValue("THUONGYTCLCV") = "LEFT" Then
                    chkYTCLCV.Checked = True
                    chkYTCLCV_CheckedChanged(Nothing, Nothing)
                Else
                    rnYTCLCV.Text = If(item.GetDataKeyValue("THUONGYTCLCV") IsNot Nothing, item.GetDataKeyValue("THUONGYTCLCV").ToString.Replace(",", "."), "")
                End If

                If item.GetDataKeyValue("OTHER1") = "LEFT" Then
                    chkOther1.Checked = True
                    chkOther1_CheckedChanged(Nothing, Nothing)
                Else
                    rnOther1.Text = If(item.GetDataKeyValue("OTHER1") IsNot Nothing, item.GetDataKeyValue("OTHER1").ToString.Replace(",", "."), "")
                End If

                If item.GetDataKeyValue("OTHER2") = "LEFT" Then
                    chkOther2.Checked = True
                    chkOther2_CheckedChanged(Nothing, Nothing)
                Else
                    rnOther2.Text = If(item.GetDataKeyValue("OTHER2") IsNot Nothing, item.GetDataKeyValue("OTHER2").ToString.Replace(",", "."), "")
                End If

                If item.GetDataKeyValue("OTHER3") = "LEFT" Then
                    chkOther3.Checked = True
                    chkOther3_CheckedChanged(Nothing, Nothing)
                Else
                    rnOther3.Text = If(item.GetDataKeyValue("OTHER3") IsNot Nothing, item.GetDataKeyValue("OTHER3").ToString.Replace(",", "."), "")
                End If

                If item.GetDataKeyValue("OTHER4") = "LEFT" Then
                    chkOther4.Checked = True
                    chkOther4_CheckedChanged(Nothing, Nothing)
                Else
                    rnOther4.Text = If(item.GetDataKeyValue("OTHER4") IsNot Nothing, item.GetDataKeyValue("OTHER4").ToString.Replace(",", "."), "")
                End If

                If item.GetDataKeyValue("OTHER5") = "LEFT" Then
                    chkOther5.Checked = True
                    chkOther5_CheckedChanged(Nothing, Nothing)
                Else
                    rnOther5.Text = If(item.GetDataKeyValue("OTHER5") IsNot Nothing, item.GetDataKeyValue("OTHER5").ToString.Replace(",", "."), "")
                End If
                If item.GetDataKeyValue("UNUSE_RATIO") = "-1" Then
                    chkUnuseRatio.Checked = True
                End If
                If item.GetDataKeyValue("VIOLATE") = "-1" Then
                    chkViolate.Checked = True
                End If
                If item.GetDataKeyValue("UNION_PERCENT") Then
                    chkUnionPercent.Checked = True
                End If
                If item.GetDataKeyValue("UNION_PERMANENT") Then
                    chkUnionPermanent.Checked = True
                End If
                If item.GetDataKeyValue("UNION_MONEY") IsNot Nothing Then
                    rnUnionMoney.Value = CDec(item.GetDataKeyValue("UNION_MONEY"))
                End If
                If item.GetDataKeyValue("ACTFLG") = "A" Then
                    cboActflg.SelectedValue = 1
                ElseIf item.GetDataKeyValue("ACTFLG") = "I" Then
                    cboActflg.SelectedValue = 2
                End If
                txtNote.Text = item.GetDataKeyValue("NOTE")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            hiOrgdID.Value = ctrlOrg.CurrentValue
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    'Private Sub textbox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rnLuongCB.TextChanged, rnLuongBS.TextChanged, rnXangXe.TextChanged, rnDienThoai.TextChanged,
    '                                                                                                rnYTCLCV.TextChanged, rnOther1.TextChanged, rnOther2.TextChanged, rnOther3.TextChanged, rnOther4.TextChanged, rnOther5.TextChanged
    '    Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
    '    Try
    '        Dim startTime As DateTime = DateTime.UtcNow
    '        Dim luongCB = If(IsNumeric(rnLuongCB.Value), rnLuongCB.Value, 0)
    '        Dim luongBS = If(IsNumeric(rnLuongBS.Value), rnLuongBS.Value, 0)
    '        Dim xangxe = If(IsNumeric(rnXangXe.Value), rnXangXe.Value, 0)
    '        Dim dienthoai = If(IsNumeric(rnDienThoai.Value), rnDienThoai.Value, 0)
    '        Dim ytclcv = If(IsNumeric(rnYTCLCV.Value), rnYTCLCV.Value, 0)
    '        Dim other1 = If(IsNumeric(rnOther1.Value), rnOther1.Value, 0)
    '        Dim other2 = If(IsNumeric(rnOther2.Value), rnOther2.Value, 0)
    '        Dim other3 = If(IsNumeric(rnOther3.Value), rnOther3.Value, 0)
    '        Dim other4 = If(IsNumeric(rnOther4.Value), rnOther4.Value, 0)
    '        Dim other5 = If(IsNumeric(rnOther5.Value), rnOther5.Value, 0)
    '        Dim total = luongCB + luongBS + xangxe + dienthoai + ytclcv + other1 + other2 + other3 + other4 + other5
    '        If total > 100 Then
    '            ShowMessage("Tổng tỷ lệ phải nhỏ hơn hoặc bằng 100", NotifyType.Warning)
    '            Exit Sub
    '        End If
    '        _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
    '    Catch ex As Exception
    '        DisplayException(Me.ViewName, Me.ID, ex)
    '        _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
    '    End Try
    'End Sub

    Private Sub chkLuongBS_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLuongBS.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(rnLuongBS)
            rnDienThoai.ReadOnly = chkDienThoai.Checked
            rnLuongBS.ReadOnly = False
            rnLuongCB.ReadOnly = False
            rnOther1.ReadOnly = False
            rnOther2.ReadOnly = False
            rnOther3.ReadOnly = False
            rnOther4.ReadOnly = False
            rnOther5.ReadOnly = False
            rnXangXe.ReadOnly = False
            rnYTCLCV.ReadOnly = False
            'textbox_CheckedChanged(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub chkDienThoai_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDienThoai.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(rnDienThoai)
            rnDienThoai.ReadOnly = chkDienThoai.Checked
            rnLuongBS.ReadOnly = False
            rnLuongCB.ReadOnly = False
            rnOther1.ReadOnly = False
            rnOther2.ReadOnly = False
            rnOther3.ReadOnly = False
            rnOther4.ReadOnly = False
            rnOther5.ReadOnly = False
            rnXangXe.ReadOnly = False
            rnYTCLCV.ReadOnly = False
            'textbox_CheckedChanged(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub chkLuongCB_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkLuongCB.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(rnLuongCB)
            rnLuongCB.ReadOnly = chkLuongCB.Checked
            rnDienThoai.ReadOnly = False
            rnLuongBS.ReadOnly = False
            rnOther1.ReadOnly = False
            rnOther2.ReadOnly = False
            rnOther3.ReadOnly = False
            rnOther4.ReadOnly = False
            rnOther5.ReadOnly = False
            rnXangXe.ReadOnly = False
            rnYTCLCV.ReadOnly = False
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub chkOther1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOther1.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(rnOther1)
            rnOther1.ReadOnly = chkOther1.Checked
            rnDienThoai.ReadOnly = False
            rnLuongBS.ReadOnly = False
            rnLuongCB.ReadOnly = False
            rnOther2.ReadOnly = False
            rnOther3.ReadOnly = False
            rnOther4.ReadOnly = False
            rnOther5.ReadOnly = False
            rnXangXe.ReadOnly = False
            rnYTCLCV.ReadOnly = False
            'textbox_CheckedChanged(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub chkOther2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOther2.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(rnOther2)
            rnOther2.ReadOnly = chkOther2.Checked
            rnDienThoai.ReadOnly = False
            rnLuongBS.ReadOnly = False
            rnLuongCB.ReadOnly = False
            rnOther1.ReadOnly = False
            rnOther3.ReadOnly = False
            rnOther4.ReadOnly = False
            rnOther5.ReadOnly = False
            rnXangXe.ReadOnly = False
            rnYTCLCV.ReadOnly = False
            'textbox_CheckedChanged(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub chkOther3_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOther3.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            ClearControlValue(rnOther3)
            rnOther3.ReadOnly = chkOther3.Checked
            rnDienThoai.ReadOnly = False
            rnLuongBS.ReadOnly = False
            rnLuongCB.ReadOnly = False
            rnOther1.ReadOnly = False
            rnOther2.ReadOnly = False
            rnOther4.ReadOnly = False
            rnOther5.ReadOnly = False
            rnXangXe.ReadOnly = False
            rnYTCLCV.ReadOnly = False
            'textbox_CheckedChanged(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub chkOther4_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOther4.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(rnOther4)
            rnOther4.ReadOnly = chkOther4.Checked
            rnDienThoai.ReadOnly = False
            rnLuongBS.ReadOnly = False
            rnLuongCB.ReadOnly = False
            rnOther1.ReadOnly = False
            rnOther2.ReadOnly = False
            rnOther3.ReadOnly = False
            rnOther5.ReadOnly = False
            rnXangXe.ReadOnly = False
            rnYTCLCV.ReadOnly = False
            'textbox_CheckedChanged(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub chkOther5_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkOther5.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(rnOther5)
            rnOther5.ReadOnly = chkOther5.Checked
            rnDienThoai.ReadOnly = False
            rnLuongBS.ReadOnly = False
            rnLuongCB.ReadOnly = False
            rnOther1.ReadOnly = False
            rnOther2.ReadOnly = False
            rnOther3.ReadOnly = False
            rnOther4.ReadOnly = False
            rnXangXe.ReadOnly = False
            rnYTCLCV.ReadOnly = False
            'textbox_CheckedChanged(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub chkXangXe_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkXangXe.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(rnXangXe)
            rnXangXe.ReadOnly = chkXangXe.Checked
            rnDienThoai.ReadOnly = False
            rnLuongBS.ReadOnly = False
            rnLuongCB.ReadOnly = False
            rnOther1.ReadOnly = False
            rnOther2.ReadOnly = False
            rnOther3.ReadOnly = False
            rnOther4.ReadOnly = False
            rnOther5.ReadOnly = False
            rnYTCLCV.ReadOnly = False
            'textbox_CheckedChanged(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub chkYTCLCV_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkYTCLCV.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ClearControlValue(rnYTCLCV)
            rnYTCLCV.ReadOnly = chkYTCLCV.Checked
            rnDienThoai.ReadOnly = False
            rnLuongBS.ReadOnly = False
            rnLuongCB.ReadOnly = False
            rnOther1.ReadOnly = False
            rnOther2.ReadOnly = False
            rnOther3.ReadOnly = False
            rnOther4.ReadOnly = False
            rnOther5.ReadOnly = False
            rnXangXe.ReadOnly = False
            'textbox_CheckedChanged(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub chkUnuseRatio_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkUnuseRatio.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If chkUnuseRatio.Checked Then
                Label14.Visible = True
                Label13.Visible = False
                chkDienThoai.Visible = False
                chkLuongBS.Visible = False
                chkLuongCB.Visible = False
                chkOther1.Visible = False
                chkOther2.Visible = False
                chkOther3.Visible = False
                chkOther4.Visible = False
                chkOther5.Visible = False
                chkXangXe.Visible = False
                chkYTCLCV.Visible = False
                chkLuongCB.Checked = False
                chkOther1.Checked = False
                chkOther2.Checked = False
                chkOther3.Checked = False
                chkOther4.Checked = False
                chkOther5.Checked = False
                chkXangXe.Checked = False
                chkYTCLCV.Checked = False
                chkLuongBS.Checked = False
                chkDienThoai.Checked = False
                ClearControlValue(rnYTCLCV, rnDienThoai, rnLuongBS, rnLuongCB, rnOther1, rnOther2, rnOther3, rnOther4, rnOther5, rnXangXe)
            Else
                Label14.Visible = False
                Label13.Visible = True
                chkDienThoai.Visible = True
                chkLuongBS.Visible = True
                chkLuongCB.Visible = True
                chkOther1.Visible = True
                chkOther2.Visible = True
                chkOther3.Visible = True
                chkOther4.Visible = True
                chkOther5.Visible = True
                chkXangXe.Visible = True
                chkYTCLCV.Visible = True
                chkLuongCB.Checked = False
                chkOther1.Checked = False
                chkOther2.Checked = False
                chkOther3.Checked = False
                chkOther4.Checked = False
                chkOther5.Checked = False
                chkXangXe.Checked = False
                chkYTCLCV.Checked = False
                chkLuongBS.Checked = False
                chkDienThoai.Checked = False
                ClearControlValue(rnYTCLCV, rnDienThoai, rnLuongBS, rnLuongCB, rnOther1, rnOther2, rnOther3, rnOther4, rnOther5, rnXangXe)
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub chkUnionPermanent_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkUnionPermanent.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            EnableControlAll(True, rnUnionMoney)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub chkUnionPercent_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles chkUnionPercent.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ClearControlValue(rnUnionMoney)
            EnableControlAll(False, rnUnionMoney)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim rep As New ProfileRepository
        Dim _filter As New SalaryItemsPercentDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgData, _filter)

            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim _param = New ProfileBusiness.ParamDTO With {.ORG_ID = Decimal.Parse(ctrlOrg.CurrentValue),
                                            .IS_DISSOLVE = ctrlOrg.IsDissolve}
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetSalItemsPercent(_filter, 0, Integer.MaxValue, 0, _param, Sorts).ToTable()
                Else
                    Return rep.GetSalItemsPercent(_filter, 0, Integer.MaxValue, 0, _param).ToTable()
                End If
            Else
                Dim lstData As List(Of SalaryItemsPercentDTO)
                If Sorts IsNot Nothing Then
                    lstData = rep.GetSalItemsPercent(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                Else
                    lstData = rep.GetSalItemsPercent(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
                End If
                rgData.VirtualItemCount = MaximumRows
                rgData.DataSource = lstData
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Function
#End Region
End Class