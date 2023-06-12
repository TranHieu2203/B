Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Ionic.Crc
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_StocksTransactionNewEdit
    Inherits Common.CommonView

    Dim log As UserLog = LogHelper.GetUserLog
    Public Overrides Property MustAuthorize As Boolean = True

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()

#Region "Property"
    Property ListAttachFile As List(Of AttachFilesDTO)
        Get
            Return ViewState(Me.ID & "_ListAttachFile")
        End Get
        Set(ByVal value As List(Of AttachFilesDTO))
            ViewState(Me.ID & "_ListAttachFile") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetParams()
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

            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Create, ToolbarItem.Edit, ToolbarItem.Delete, ToolbarItem.Save, ToolbarItem.Cancel, ToolbarItem.Export)

            CType(MainToolBar.Items(3), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dtData As DataTable
            Using rep As New ProfileRepository
                dtData = rep.GetOtherList("TRANSACTION_STATUS", True)
                FillRadCombobox(cboStatus, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("STOCK_PAY_TIME", True)
                FillRadCombobox(cboPayType2, dtData, "NAME", "ID")
            End Using

            Using rep As New ProfileBusinessRepository
                Dim _filter = New StocksDTO
                _filter.CODE = ""
                Dim _param = New ProfileBusiness.ParamDTO With {.ORG_ID = 1,
                                                                .IS_DISSOLVE = 0}
                Dim lstCode = rep.GetStocks(_filter, 0, Integer.MaxValue, 0, _param).ToTable()
                FillRadCombobox(cboCode, lstCode, "EMPLOYEE_STOCK_CODE", "ID")
            End Using

            Dim item As New RadComboBoxItem
            Dim item2 As New RadComboBoxItem
            Dim item3 As New RadComboBoxItem
            Dim item4 As New RadComboBoxItem
            item.Text = "85"
            item.Value = "85"
            cboProbationPercent.Items.Add(item)
            item2.Text = "90"
            item2.Value = "90"
            cboProbationPercent.Items.Add(item2)
            item3.Text = "95"
            item3.Value = "95"
            cboProbationPercent.Items.Add(item3)
            item4.Text = "100"
            item4.Value = "100"
            cboProbationPercent.Items.Add(item4)
        Catch ex As Exception
            Throw ex
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
                        ClearControlValue(hidEmployeeID, cboCode, txtCode, txtTileName, rnPercent, txtEmployeeCode, rdDateState, rnMonth, txtNote, txtEmployeeName, rdEffectedDate, rnTime, txtLocation, txtStockType, rnStockDeal, txtOrgName,
                          txtPayType, rnMoneyDeal, txtUpload, txtUploadFile, hidPayType, rnTradeMonth, rnStockPrice, rnStockLeft, txtUpload, txtUploadFile, rdTradeDate, chkIsProbation, rnProbationMonth, rnStockFinalPrice,
                            rnStockTotal, cboPayType2, cboProbationPercent, rnProbationStock, rnStockTotalRound, cboStatus, rnStockPay, rdPayDate, txtNote2)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        ClearControlValue(hidEmployeeID, cboCode, txtCode, txtTileName, rnPercent, txtEmployeeCode, rdDateState, rnMonth, txtNote, txtEmployeeName, rdEffectedDate, rnTime, txtLocation, txtStockType, rnStockDeal, txtOrgName,
                          txtPayType, rnMoneyDeal, txtUpload, txtUploadFile, hidPayType, rnTradeMonth, rnStockPrice, rnStockLeft, txtUpload, txtUploadFile, rdTradeDate, chkIsProbation, rnProbationMonth, rnStockFinalPrice,
                            rnStockTotal, cboPayType2, cboProbationPercent, rnProbationStock, rnStockTotalRound, cboStatus, rnStockPay, rdPayDate, txtNote2)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                        ClearControlValue(hidEmployeeID, cboCode, txtCode, txtTileName, rnPercent, txtEmployeeCode, rdDateState, rnMonth, txtNote, txtEmployeeName, rdEffectedDate, rnTime, txtLocation, txtStockType, rnStockDeal, txtOrgName,
                          txtPayType, rnMoneyDeal, txtUpload, txtUploadFile, hidPayType, rnTradeMonth, rnStockPrice, rnStockLeft, txtUpload, txtUploadFile, rdTradeDate, chkIsProbation, rnProbationMonth, rnStockFinalPrice,
                            rnStockTotal, cboPayType2, cboProbationPercent, rnProbationStock, rnStockTotalRound, cboStatus, rnStockPay, rdPayDate, txtNote2)
                End Select
            End If
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileBusinessRepository
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    EnabledGrid(rgData, False, True)
                    EnableControlAll(True, btnUpload, chkIsProbation, btnUpload, cboStatus, txtNote2, rdTradeDate, rdPayDate, cboProbationPercent, rnStockPay, cboPayType2, rnProbationMonth)
                    EnableControlls()
                    Calculate()
                Case CommonMessage.STATE_NORMAL
                    EnabledGrid(rgData, True, True)
                    EnableControlAll(False, btnUpload, chkIsProbation, btnUpload, cboStatus, txtNote2, rdTradeDate, rdPayDate, cboProbationPercent, rnStockPay, cboPayType2, rnProbationMonth)
                Case CommonMessage.STATE_EDIT
                    EnabledGrid(rgData, False, True)
                    EnableControlAll(True, btnUpload, chkIsProbation, btnUpload, cboStatus, txtNote2, rdTradeDate, rdPayDate, cboProbationPercent, rnStockPay, cboPayType2, rnProbationMonth)
                    EnableControlls()
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For Each item In rgData.SelectedItems
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DeleteStocksTransaction(lstDeletes) Then
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

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objData As New StocksTransactionDTO
        Dim gID As Decimal
        Dim rep As New ProfileBusinessRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(txtCode, txtUpload, txtUploadFile, rnTradeMonth, rnStockPrice, rnStockLeft, txtUpload, txtUploadFile, rdTradeDate, chkIsProbation, rnProbationMonth, rnStockFinalPrice,
                            rnStockTotal, cboPayType2, cboProbationPercent, rnProbationStock, rnStockTotalRound, cboStatus, rnStockPay, rdPayDate, txtNote2)
                    UpdateControlState()
                    AutoCreate_StockNo()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgData.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    If rgData.SelectedItems.Count > 1 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_MULTI_ROW), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If hidEmployeeID.Value <> "" Then
                            objData.EMPLOYEE_ID = hidEmployeeID.Value
                        End If
                        If cboCode.SelectedValue <> "" Then
                            objData.STOCK_ID = cboCode.SelectedValue
                        End If
                        If rnTradeMonth.Value IsNot Nothing Then
                            objData.TRADE_MONTH = rnTradeMonth.Value
                        End If
                        If rnStockPrice.Value IsNot Nothing Then
                            objData.STOCK_PRICE = rnStockPrice.Value
                        End If
                        If rnStockLeft.Value IsNot Nothing Then
                            objData.STOCK_LEFT = rnStockLeft.Value
                        End If
                        objData.UPLOAD_FILE_NAME = txtUploadFile.Text
                        objData.FILE_NAME = txtUpload.Text
                        If rdTradeDate.SelectedDate IsNot Nothing Then
                            objData.TRADE_DATE = rdTradeDate.SelectedDate
                        End If
                        If rnProbationMonth.Value IsNot Nothing Then
                            objData.PROBATION_MONTHS = rnProbationMonth.Value
                        End If
                        If rnStockFinalPrice.Value IsNot Nothing Then
                            objData.STOCK_FINAL_PRICE = rnStockFinalPrice.Value
                        End If
                        If rnStockTotal.Value IsNot Nothing Then
                            objData.STOCK_TOTAL = rnStockTotal.Value
                        End If
                        If cboPayType2.SelectedValue <> "" Then
                            objData.PAY_TYPE = cboPayType2.SelectedValue
                        End If
                        If cboProbationPercent.SelectedValue <> "" Then
                            objData.PROBATION_PERCENT = cboProbationPercent.SelectedValue
                        End If
                        If rnProbationStock.Value IsNot Nothing Then
                            objData.PROBATION_STOCK = rnProbationStock.Value
                        End If
                        If rnStockTotalRound.Value IsNot Nothing Then
                            objData.STOCK_TOTAL_ROUND = rnStockTotalRound.Value
                        End If
                        If cboStatus.SelectedValue <> "" Then
                            objData.STATUS = cboStatus.SelectedValue
                        End If
                        If rnStockPay.Value IsNot Nothing Then
                            objData.STOCK_PAY = rnStockPay.Value
                        End If
                        If rdPayDate.SelectedDate IsNot Nothing Then
                            objData.PAY_DATE = rdPayDate.SelectedDate
                        End If
                        objData.NOTE = txtNote2.Text
                        objData.CODE = txtCode.Text

                        If cboStatus.SelectedValue <> "" Then
                            Dim store As New ProfileStoreProcedure
                            Dim code = store.GET_CODE_OT_OTHER_LIST(cboStatus.SelectedValue)
                            If code = "COMPLETE" Then
                                If rep.ValidateStocksTransactionBefore(objData) Then
                                    ShowMessage("Giao dịch liền kề chưa được thanh toán, vui lòng kiểm tra lại", NotifyType.Warning)
                                    Exit Sub
                                End If
                            End If
                        End If
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.ValidateStocksTransaction(objData) Then
                                    ShowMessage("Ngày giao dịch phải lớn hơn Ngày GD của GD liền kề Hồ sơ đang chọn", NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.InsertStocksTransaction(objData, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                    Refresh("InsertView")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objData.ID = rgData.SelectedValue
                                If rep.ValidateStocksTransactionStatus(objData) Then
                                    ShowMessage("Không thể chỉnh sửa khi đã phát sinh giao dịch mới hơn liền kề ở trạng thái hoàn thành", NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.ModifyStocksTransaction(objData, gID) Then
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
                        If item.GetDataKeyValue("STOCK_ID") IsNot Nothing Then
                            objData.STOCK_ID = CDec(item.GetDataKeyValue("STOCK_ID"))
                        End If
                        If item.GetDataKeyValue("ID") IsNot Nothing Then
                            objData.ID = CDec(item.GetDataKeyValue("ID"))
                        End If
                        If item.GetDataKeyValue("TRADE_DATE") IsNot Nothing Then
                            objData.TRADE_DATE = CDate(item.GetDataKeyValue("TRADE_DATE"))
                        End If
                        If rep.ValidateStocksTransactionStatus(objData) Then
                            ShowMessage("Không thể chỉnh sửa khi đã phát sinh giao dịch mới hơn liền kề ở trạng thái hoàn thành", NotifyType.Warning)
                            Exit Sub
                        End If
                    Next
                    ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    btnDownload.Enabled = False
                    Refresh("Cancel")
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_EXPORT
                    Dim dtData As DataTable
                    Using xls As New ExcelCommon
                        dtData = CreateDataFilter(True)
                        If dtData.Rows.Count = 0 Then
                            ShowMessage(Translate(CommonMessage.MESSAGE_WARNING_EXPORT_EMPTY), NotifyType.Warning)
                            Exit Sub
                        ElseIf dtData.Rows.Count > 0 Then
                            rgData.ExportExcel(Server, Response, dtData, "Stocks")
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

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            ctrlUpload1.AllowedExtensions = "xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"
            ctrlUpload1.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            txtUpload.Text = ""
            Dim listExtension = New List(Of String)
            listExtension.Add(".xls")
            listExtension.Add(".xlsx")
            listExtension.Add(".txt")
            listExtension.Add(".ctr")
            listExtension.Add(".doc")
            listExtension.Add(".docx")
            listExtension.Add(".xml")
            listExtension.Add(".png")
            listExtension.Add(".jpg")
            listExtension.Add(".bitmap")
            listExtension.Add(".jpeg")
            listExtension.Add(".gif")
            listExtension.Add(".pdf")
            listExtension.Add(".rar")
            listExtension.Add(".zip")
            listExtension.Add(".ppt")
            listExtension.Add(".pptx")
            Dim fileName As String

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/StockTransactionInfo/")
            If ctrlUpload1.UploadedFiles.Count >= 1 Then
                Dim finfo As New AttachFilesDTO
                ListAttachFile = New List(Of AttachFilesDTO)
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(ctrlUpload1.UploadedFiles.Count - 1)
                If listExtension.Any(Function(x) x.ToUpper().Trim() = file.GetExtension.ToUpper().Trim()) Then
                    System.IO.Directory.CreateDirectory(strPath)
                    strPath = strPath
                    fileName = System.IO.Path.Combine(strPath, file.FileName)
                    file.SaveAs(fileName, True)
                    txtUpload.Text = file.FileName
                    finfo.FILE_PATH = strPath + file.FileName
                    finfo.ATTACHFILE_NAME = file.FileName
                    finfo.CONTROL_NAME = "ctrlHU_StocksTransactionNewEdit"
                    finfo.FILE_TYPE = file.GetExtension
                    ListAttachFile.Add(finfo)
                Else
                    ShowMessage(Translate("Vui lòng chọn file đúng định dạng. !!! Hệ thống chỉ nhận file xls,xlsx,txt,ctr,doc,docx,xml,png,jpg,bitmap,jpeg,gif,pdf,rar,zip,ppt,pptx"), NotifyType.Warning)
                    Exit Sub
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub btnDownload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If txtUpload.Text <> "" Then
                Dim strPath_Down As String = Server.MapPath("~/ReportTemplates/Profile/StockTransactionInfo/" + txtUpload.Text)
                ZipFiles(strPath_Down, 2)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim store As New ProfileStoreProcedure
        Try
            If rgData.SelectedItems.Count > 0 Then
                Dim item As GridDataItem
                item = rgData.SelectedItems(0)
                ClearControlValue(cboCode, txtCode, txtUpload, txtUploadFile, hidPayType, rnTradeMonth, rnStockPrice, rnStockLeft, rdTradeDate, chkIsProbation, rnProbationMonth, rnStockFinalPrice,
                            rnStockTotal, cboPayType2, cboProbationPercent, rnProbationStock, rnStockTotalRound, cboStatus, rnStockPay, rdPayDate, txtNote2)
                If item.GetDataKeyValue("STOCK_ID") IsNot Nothing Then
                    cboCode.SelectedValue = item.GetDataKeyValue("STOCK_ID")
                    FillData(cboCode.SelectedValue)
                End If
                If item.GetDataKeyValue("CODE") IsNot Nothing Then
                    txtCode.Text = item.GetDataKeyValue("CODE")
                End If
                If item.GetDataKeyValue("TRADE_MONTH") IsNot Nothing Then
                    rnTradeMonth.Value = CDec(item.GetDataKeyValue("TRADE_MONTH"))
                End If
                If item.GetDataKeyValue("STOCK_PRICE") IsNot Nothing Then
                    rnStockPrice.Value = CDec(item.GetDataKeyValue("STOCK_PRICE"))
                End If
                If item.GetDataKeyValue("STOCK_LEFT") IsNot Nothing Then
                    rnStockLeft.Value = CDec(item.GetDataKeyValue("STOCK_LEFT"))
                End If
                If item.GetDataKeyValue("FILE_NAME") IsNot Nothing Then
                    txtUpload.Text = item.GetDataKeyValue("FILE_NAME")
                End If
                If item.GetDataKeyValue("UPLOAD_FILE_NAME") IsNot Nothing Then
                    txtUploadFile.Text = item.GetDataKeyValue("UPLOAD_FILE_NAME")
                End If
                If item.GetDataKeyValue("TRADE_DATE") IsNot Nothing Then
                    rdTradeDate.SelectedDate = CDate(item.GetDataKeyValue("TRADE_DATE"))
                End If
                If item.GetDataKeyValue("PROBATION_MONTHS") IsNot Nothing Then
                    rnProbationMonth.Value = CDec(item.GetDataKeyValue("PROBATION_MONTHS"))
                End If
                If item.GetDataKeyValue("STOCK_FINAL_PRICE") IsNot Nothing Then
                    rnStockFinalPrice.Value = CDec(item.GetDataKeyValue("STOCK_FINAL_PRICE"))
                End If
                If item.GetDataKeyValue("STOCK_TOTAL") IsNot Nothing Then
                    rnStockTotal.Value = CDec(item.GetDataKeyValue("STOCK_TOTAL"))
                End If
                If item.GetDataKeyValue("PAY_TYPE") IsNot Nothing Then
                    cboPayType2.SelectedValue = item.GetDataKeyValue("PAY_TYPE")
                End If
                If item.GetDataKeyValue("PROBATION_PERCENT") IsNot Nothing Then
                    cboProbationPercent.SelectedValue = item.GetDataKeyValue("PROBATION_PERCENT")
                End If
                If item.GetDataKeyValue("PROBATION_STOCK") IsNot Nothing Then
                    rnProbationStock.Value = CDec(item.GetDataKeyValue("PROBATION_STOCK"))
                End If
                If item.GetDataKeyValue("STOCK_TOTAL_ROUND") IsNot Nothing Then
                    rnStockTotalRound.Value = CDec(item.GetDataKeyValue("STOCK_TOTAL_ROUND"))
                End If
                If item.GetDataKeyValue("STATUS") IsNot Nothing Then
                    cboStatus.SelectedValue = item.GetDataKeyValue("STATUS")
                End If
                If item.GetDataKeyValue("STOCK_PAY") IsNot Nothing Then
                    rnStockPay.Value = CDec(item.GetDataKeyValue("STOCK_PAY"))
                End If
                If item.GetDataKeyValue("PAY_DATE") IsNot Nothing Then
                    rdPayDate.SelectedDate = CDate(item.GetDataKeyValue("PAY_DATE"))
                End If
                If item.GetDataKeyValue("NOTE") IsNot Nothing Then
                    txtNote2.Text = item.GetDataKeyValue("NOTE")
                End If

                If cboStatus.SelectedValue = "" Then
                    Exit Sub
                End If
                Dim code = store.GET_CODE_OT_OTHER_LIST(cboStatus.SelectedValue)
                If code = "INCOMPLETE" Then
                    reqStockPay.Enabled = False
                    reqPayDate.Enabled = False
                Else
                    reqStockPay.Enabled = True
                    reqPayDate.Enabled = True
                End If
                If rnProbationMonth.Value IsNot Nothing Then
                    chkIsProbation.Checked = True
                Else
                    chkIsProbation.Checked = False
                End If
                If txtUpload.Text <> "" Then
                    btnDownload.Enabled = True
                Else
                    btnDownload.Enabled = False
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Public Sub chkIsProbation_CheckChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkIsProbation.CheckedChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            EnableControlls()
            Calculate()
            If Not chkIsProbation.Checked Then
                ClearControlValue(cboProbationPercent, rnProbationMonth)
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub rnProbationMonth_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rnProbationMonth.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Calculate()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rnStockPrice_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rnStockPrice.TextChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Calculate()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub cboCode_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCode.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim store As New ProfileStoreProcedure
        Try
            If cboCode.SelectedValue = "" Then
                Exit Sub
            End If
            FillData(cboCode.SelectedValue)
            EnableControlls()
            AutoCreate_StockNo()
            Calculate()
            rgData.Rebind()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub cboStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboStatus.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim store As New ProfileStoreProcedure
        Try
            If cboStatus.SelectedValue = "" Then
                Exit Sub
            End If
            Dim code = store.GET_CODE_OT_OTHER_LIST(cboStatus.SelectedValue)
            If code = "INCOMPLETE" Then
                reqStockPay.Enabled = False
                reqPayDate.Enabled = False
            Else
                reqStockPay.Enabled = True
                reqPayDate.Enabled = True
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Protected Sub cboProbationPercent_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboProbationPercent.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim store As New ProfileStoreProcedure
        Try
            Calculate()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    hidID.Value = Request.Params("ID")
                    rgData.Rebind()
                    For Each item As GridDataItem In rgData.Items
                        If item.GetDataKeyValue("ID").ToString = hidID.Value Then
                            item.Selected = True
                            rgData_SelectedIndexChanged(Nothing, Nothing)
                            Exit For
                        End If
                    Next
                End If
                Refresh("NormalView")
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub EnableControlls()
        Dim store As New ProfileStoreProcedure
        Try
            If hidPayType.value <> "" Then
                Dim code = store.GET_CODE_OT_OTHER_LIST(hidPayType.Value)
                If code = "COPHIEU" Then
                    rnStockPrice.ReadOnly = True
                    reqStockPrice.Enabled = False
                ElseIf code = "TIENQUYCP" Then
                    rnStockPrice.ReadOnly = False
                    reqStockPrice.Enabled = True
                End If
            End If
            If chkIsProbation.Checked Then
                rnProbationMonth.ReadOnly = False
                reqProbationMonth.Enabled = True
                cboProbationPercent.Enabled = True
                reqProbationPercent.Enabled = True
            Else
                rnProbationMonth.ReadOnly = True
                reqProbationMonth.Enabled = False
                cboProbationPercent.Enabled = False
                reqProbationPercent.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Calculate()
        Try
            Dim store As New ProfileStoreProcedure

            ClearControlValue(rnProbationStock, rnStockLeft, rnTradeMonth, rnStockFinalPrice, rnStockTotal, rnStockTotalRound)
            If rnMonth.Value IsNot Nothing AndAlso rnTime.Value IsNot Nothing Then
                rnTradeMonth.Value = rnMonth.Value / rnTime.Value
            End If

            If hidPayType.Value <> "" Then
                Dim code = store.GET_CODE_OT_OTHER_LIST(hidPayType.Value)
                If code = "TIENQUYCP" Then
                    If rnStockPrice.Value IsNot Nothing AndAlso rnPercent.Value IsNot Nothing Then
                        rnStockFinalPrice.Value = rnStockPrice.Value - (rnStockPrice.Value * rnPercent.Value / 100)
                    End If
                    If chkIsProbation.Checked Then
                        If rnMoneyDeal.Value IsNot Nothing AndAlso rnTime.Value IsNot Nothing AndAlso cboProbationPercent.SelectedValue <> "" AndAlso rnStockFinalPrice.Value IsNot Nothing AndAlso rnProbationMonth.Value IsNot Nothing AndAlso rnTradeMonth.Value Then
                            rnProbationStock.Value = (((rnMoneyDeal.Value / rnTime.Value) / rnStockFinalPrice.Value) / rnTradeMonth.Value) * (CDec(cboProbationPercent.SelectedValue) / 100) * rnProbationMonth.Value
                        End If
                        If rnMoneyDeal.Value IsNot Nothing AndAlso rnTime.Value IsNot Nothing AndAlso rnStockFinalPrice.Value IsNot Nothing AndAlso rnTradeMonth.Value IsNot Nothing AndAlso rnProbationMonth.Value IsNot Nothing Then
                            rnStockLeft.Value = (((rnMoneyDeal.Value / rnTime.Value) / rnStockFinalPrice.Value) / rnTradeMonth.Value) * (rnTradeMonth.Value - rnProbationMonth.Value)
                        End If
                    Else
                        If rnMoneyDeal.Value IsNot Nothing AndAlso rnTime.Value IsNot Nothing AndAlso rnStockFinalPrice.Value IsNot Nothing AndAlso rnTradeMonth.Value IsNot Nothing Then
                            rnStockLeft.Value = (((rnMoneyDeal.Value / rnTime.Value) / rnStockFinalPrice.Value) / rnTradeMonth.Value) * rnTradeMonth.Value
                        End If
                        rnProbationStock.Value = 0
                    End If
                ElseIf code = "COPHIEU" Then
                    ClearControlValue(rnStockPrice)
                    If chkIsProbation.Checked Then
                        If rnStockDeal.Value IsNot Nothing AndAlso rnMonth.Value IsNot Nothing AndAlso cboProbationPercent.SelectedValue <> "" AndAlso rnProbationMonth.Value IsNot Nothing Then
                            rnProbationStock.Value = ((rnStockDeal.Value / rnMonth.Value) * (CDec(cboProbationPercent.SelectedValue) / 100)) * rnProbationMonth.Value
                        End If
                        If rnStockDeal.Value IsNot Nothing AndAlso rnMonth.Value IsNot Nothing AndAlso rnTradeMonth.Value IsNot Nothing AndAlso rnProbationMonth.Value IsNot Nothing Then
                            rnStockLeft.Value = (rnStockDeal.Value / rnMonth.Value) * (rnTradeMonth.Value - rnProbationMonth.Value)
                        End If
                    Else
                        If rnStockDeal.Value IsNot Nothing AndAlso rnMonth.Value IsNot Nothing AndAlso rnTradeMonth.Value IsNot Nothing Then
                            rnStockLeft.Value = (rnStockDeal.Value / rnMonth.Value) * rnTradeMonth.Value
                        End If
                        rnProbationStock.Value = 0
                    End If
                End If

                rnStockTotal.Value = If(rnProbationStock.Value Is Nothing, 0, rnProbationStock.Value) + If(rnStockLeft.Value Is Nothing, 0, rnStockLeft.Value)

                'If chkIsProbation.Checked Then
                '    If rnProbationStock.Value IsNot Nothing AndAlso rnStockLeft.Value IsNot Nothing Then
                '        rnStockTotal.Value = rnProbationStock.Value + rnStockLeft.Value
                '    End If
                'Else
                '    If code = "COPHIEU" Then
                '        If rnStockDeal.Value IsNot Nothing AndAlso rnMonth.Value IsNot Nothing AndAlso rnTradeMonth.Value IsNot Nothing Then
                '            rnStockTotal.Value = rnStockDeal.Value / rnMonth.Value * rnTradeMonth.Value
                '        End If
                '    ElseIf code = "TIENQUYCP" Then
                '        If rnMoneyDeal.Value IsNot Nothing AndAlso rnTime.Value IsNot Nothing AndAlso rnStockFinalPrice.Value IsNot Nothing AndAlso rnTradeMonth.Value IsNot Nothing Then
                '            rnStockTotal.Value = ((rnMoneyDeal.Value / rnTime.Value) / rnStockFinalPrice.Value) * rnTradeMonth.Value
                '        End If
                '    End If
                'End If
                If rnStockTotal.Value IsNot Nothing Then
                    rnStockTotalRound.Value = Math.Ceiling(CDbl(rnStockTotal.Value / 10)) * 10
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub rgData_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgData.ItemDataBound
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
                If datarow.GetDataKeyValue("STATUS_CODE") IsNot Nothing AndAlso datarow.GetDataKeyValue("STATUS_CODE").ToString = "COMPLETE" Then
                    datarow.CssClass = "complete-status"
                ElseIf datarow.GetDataKeyValue("STATUS_CODE") IsNot Nothing AndAlso datarow.GetDataKeyValue("STATUS_CODE") = "INCOMPLETE" Then
                    datarow.CssClass = "incomplete-status"
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub FillData(ByVal empID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim objStock = New StocksDTO
            objStock.ID = cboCode.SelectedValue
            Using rep As New ProfileBusinessRepository
                objStock = rep.GetStocksByID(objStock)
                ClearControlValue(txtTileName, rnPercent, txtEmployeeCode, rdDateState, rnMonth, txtEmployeeName, rdEffectedDate, rnTime, txtLocation, txtStockType, rnStockDeal,
                            txtOrgName, txtPayType, rnMoneyDeal, hidEmployeeID, hidPayType, txtNote)
                If objStock IsNot Nothing Then
                    cboCode.SelectedValue = objStock.ID
                    txtTileName.Text = objStock.TITLE_NAME
                    If objStock.PERCENT IsNot Nothing Then
                        rnPercent.Value = objStock.PERCENT
                    End If
                    txtEmployeeCode.Text = objStock.EMPLOYEE_CODE
                    If objStock.STATE_DATE IsNot Nothing Then
                        rdDateState.SelectedDate = objStock.STATE_DATE
                    End If
                    If objStock.MONTH IsNot Nothing Then
                        rnMonth.Value = objStock.MONTH
                    End If
                    txtEmployeeName.Text = objStock.EMPLOYEE_NAME
                    If objStock.EFFECTED_DATE IsNot Nothing Then
                        rdEffectedDate.SelectedDate = objStock.EFFECTED_DATE
                    End If
                    If objStock.TIME IsNot Nothing Then
                        rnTime.Value = objStock.TIME
                    End If
                    txtLocation.Text = objStock.LOCATION_NAME
                    txtStockType.Text = objStock.STOCKS_TYPE_NAME
                    If objStock.STOCK_DEAL IsNot Nothing Then
                        rnStockDeal.Value = objStock.STOCK_DEAL
                    End If
                    txtOrgName.Text = objStock.ORG_NAME
                    txtPayType.Text = objStock.PAY_TYPE_NAME
                    If objStock.MONEY_DEAL IsNot Nothing Then
                        rnMoneyDeal.Value = objStock.MONEY_DEAL
                    End If
                    If objStock.EMPLOYEE_ID IsNot Nothing Then
                        hidEmployeeID.Value = objStock.EMPLOYEE_ID
                    End If
                    If objStock.PAY_TYPE IsNot Nothing Then
                        hidPayType.Value = objStock.PAY_TYPE
                    End If
                    txtNote.Text = objStock.NOTE
                End If
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Private Sub AutoCreate_StockNo()
        Dim store As New ProfileStoreProcedure
        Try
            If cboCode.SelectedValue = "" Then
                Exit Sub
            End If
            ClearControlValue(txtCode)
            Dim stockNo = store.AUTOCREATE_STOCK_TRANS_NO(cboCode.SelectedValue)

            txtCode.Text = stockNo
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ZipFiles(ByVal path As String, ByVal _ID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim fileNameZip As String
            If _ID = 1 Then
                fileNameZip = txtUpload.Text.Trim
            Else
                fileNameZip = txtUpload.Text.Trim
            End If
            Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" & HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8))
            Response.AddHeader("Content-Length", file.Length.ToString())
            'Response.ContentType = "application/octet-stream"
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document "
            Response.WriteFile(file.FullName)
            Response.End()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            HttpContext.Current.Trace.Warn(ex.ToString())
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        CreateDataFilter = Nothing
        Dim rep As New ProfileBusinessRepository
        Dim _filter As New StocksTransactionDTO
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()

        Try
            Dim MaximumRows As Integer

            SetValueObjectByRadGrid(rgData, _filter)

            Dim Sorts As String = rgData.MasterTableView.SortExpressions.GetSortString()
            Dim _param = New ProfileBusiness.ParamDTO With {.ORG_ID = 1,
                                                            .IS_DISSOLVE = 0}
            If IsPostBack Then
                If cboCode.SelectedValue <> "" Then
                    _filter.STOCK_ID = cboCode.SelectedValue
                Else
                    _filter.STOCK_ID = 0
                End If
            Else
                If (hidID.Value IsNot Nothing AndAlso hidID.Value <> "0") Then
                    If cboCode.SelectedValue <> "" Then
                        _filter.STOCK_ID = cboCode.SelectedValue
                    End If
                    _filter.ID = hidID.Value
                End If
            End If
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetStocksTransaction(_filter, 0, Integer.MaxValue, 0, _param, Sorts).ToTable()
                Else
                    Return rep.GetStocksTransaction(_filter, 0, Integer.MaxValue, 0, _param).ToTable()
                End If
            Else
                Dim lstData As List(Of StocksTransactionDTO)
                If Sorts IsNot Nothing Then
                    lstData = rep.GetStocksTransaction(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                Else
                    lstData = rep.GetStocksTransaction(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
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