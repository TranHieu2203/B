Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Ionic.Crc
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_Stocks
    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

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
                dtData = rep.GetOtherList("STOCK_TYPE", True)
                FillRadCombobox(cboStockType, dtData, "NAME", "ID")
                dtData = rep.GetOtherList("PAY_TYPE", True)
                FillRadCombobox(cboPayType, dtData, "NAME", "ID")
            End Using

            Dim dic As New Dictionary(Of String, Control)
            dic.Add("ID", hidID)
            dic.Add("EMPLOYEE_ID", hidEmployeeID)
            dic.Add("TITLE_NAME", txtTileName)
            dic.Add("CODE", txtCode)
            dic.Add("PERCENT", rnPercent)
            dic.Add("EMPLOYEE_CODE", txtEmployeeCode)
            dic.Add("STATE_DATE", rdDateState)
            dic.Add("MONTH", rnMonth)
            dic.Add("NOTE", txtNote)
            dic.Add("EMPLOYEE_NAME", txtEmployeeName)
            dic.Add("EFFECTED_DATE", rdEffectedDate)
            dic.Add("TIME", rnTime)
            dic.Add("LOCATION_NAME", txtLocation)
            dic.Add("STOCKS_TYPE", cboStockType)
            dic.Add("ORG_NAME", txtOrgName)
            dic.Add("PAY_TYPE", cboPayType)
            dic.Add("MONEY_DEAL", rnMoneyDeal)
            dic.Add("STOCK_DEAL", rnStockDeal)
            dic.Add("FILE_NAME", txtUpload)
            Utilities.OnClientRowSelectedChanged(rgData, dic)
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
                        ClearControlValue(hidEmployeeID, txtCode, txtTileName, rnPercent, txtEmployeeCode, rdDateState, rnMonth, txtNote, txtEmployeeName, rdEffectedDate, rnTime, txtLocation, cboStockType, rnStockDeal, txtOrgName,
                          cboPayType, rnMoneyDeal, txtUpload, txtUploadFile)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "InsertView"
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                        rgData.CurrentPageIndex = 0
                        rgData.MasterTableView.SortExpressions.Clear()
                        rgData.Rebind()
                        ClearControlValue(hidEmployeeID, txtCode, txtTileName, rnPercent, txtEmployeeCode, rdDateState, rnMonth, txtNote, txtEmployeeName, rdEffectedDate, rnTime, txtLocation, cboStockType, rnStockDeal, txtOrgName,
                          cboPayType, rnMoneyDeal, txtUpload, txtUploadFile)
                        CurrentState = CommonMessage.STATE_NORMAL
                    Case "Cancel"
                        rgData.MasterTableView.ClearSelectedItems()
                        ClearControlValue(hidEmployeeID, txtCode, txtTileName, rnPercent, txtEmployeeCode, rdDateState, rnMonth, txtNote, txtEmployeeName, rdEffectedDate, rnTime, txtLocation, cboStockType, rnStockDeal, txtOrgName,
                          cboPayType, rnMoneyDeal, txtUpload, txtUploadFile)
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
                    ctrlOrg.Enabled = False
                    rgData.Enabled = False
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, rnPercent, rnMonth, txtNote, rdEffectedDate, rnTime, cboStockType, cboPayType, btnUpload, btnEmployee)
                    EnableControlls()
                Case CommonMessage.STATE_NORMAL
                    ctrlOrg.Enabled = True
                    rgData.Enabled = True
                    EnabledGridNotPostback(rgData, True)
                    EnableControlAll(False, rnPercent, rnMonth, txtNote, rdEffectedDate, rnTime, cboStockType, rnStockDeal, cboPayType, rnMoneyDeal, btnUpload, btnEmployee)
                Case CommonMessage.STATE_EDIT
                    ctrlOrg.Enabled = False
                    rgData.Enabled = False
                    EnabledGridNotPostback(rgData, False)
                    EnableControlAll(True, rnPercent, rnMonth, txtNote, rdEffectedDate, rnTime, cboStockType, cboPayType, btnUpload, btnEmployee)
                    EnableControlls()
                Case CommonMessage.STATE_DELETE
                    Dim lstDeletes As New List(Of Decimal)

                    For Each item In rgData.SelectedItems
                        lstDeletes.Add(item.GetDataKeyValue("ID"))
                    Next

                    If rep.DeleteStocks(lstDeletes) Then
                        Refresh("UpdateView")
                        UpdateControlState()
                    Else
                        CurrentState = CommonMessage.STATE_NORMAL
                        ShowMessage(Translate(CommonMessage.MESSAGE_IS_EFFECT_NOT_DELETE), NotifyType.Error)
                        UpdateControlState()
                    End If
            End Select
            rep.Dispose()

            Select Case isLoadPopup
                Case 1
                    If Not FindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
                        ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
                        FindEmployee.Controls.Add(ctrlFindEmployeePopup)
                        ctrlFindEmployeePopup.LoadAllOrganization = False
                        ctrlFindEmployeePopup.MultiSelect = False
                        ctrlFindEmployeePopup.MustHaveContract = True
                    End If
            End Select
            ChangeToolbarState()

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objData As New StocksDTO
        Dim gID As Decimal
        Dim rep As New ProfileBusinessRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    CurrentState = CommonMessage.STATE_NEW
                    ClearControlValue(hidEmployeeID, txtCode, txtTileName, rnPercent, txtEmployeeCode, rdDateState, rnMonth, txtNote, txtEmployeeName, rdEffectedDate, rnTime, txtLocation, cboStockType, rnStockDeal, txtOrgName,
                          cboPayType, rnMoneyDeal, txtUpload, txtUploadFile)
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
                    CurrentState = CommonMessage.STATE_EDIT
                    UpdateControlState()
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If hidEmployeeID.Value <> "" Then
                            objData.EMPLOYEE_ID = hidEmployeeID.Value
                        End If
                        If rdEffectedDate.SelectedDate IsNot Nothing Then
                            objData.EFFECTED_DATE = rdEffectedDate.SelectedDate
                        End If
                        objData.UPLOAD_FILE_NAME = txtUploadFile.Text
                        objData.FILE_NAME = txtUpload.Text
                        If rnMoneyDeal.Value IsNot Nothing Then
                            objData.MONEY_DEAL = rnMoneyDeal.Value
                        End If
                        If rnMonth.Value IsNot Nothing Then
                            objData.MONTH = rnMonth.Value
                        End If
                        objData.NOTE = txtNote.Text
                        If cboPayType.SelectedValue <> "" Then
                            objData.PAY_TYPE = cboPayType.SelectedValue
                        End If
                        If rnPercent.Value IsNot Nothing Then
                            objData.PERCENT = rnPercent.Value
                        End If
                        If cboStockType.SelectedValue <> "" Then
                            objData.STOCKS_TYPE = cboStockType.SelectedValue
                        End If
                        If rnStockDeal.Value IsNot Nothing Then
                            objData.STOCK_DEAL = rnStockDeal.Value
                        End If
                        If rnTime.Value IsNot Nothing Then
                            objData.TIME = rnTime.Value
                        End If
                        objData.CODE = txtCode.Text
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.ValidateStocks(objData) Then
                                    ShowMessage("Dữ liệu đã tồn tại", NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.InsertStocks(objData, gID) Then
                                    CurrentState = CommonMessage.STATE_NORMAL
                                    UpdateControlState()
                                    Refresh("InsertView")
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objData.ID = rgData.SelectedValue
                                If rep.ValidateStocks(objData) Then
                                    ShowMessage("Dữ liệu đã tồn tại", NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.ValidateStocksGenerate(objData) Then
                                    ShowMessage("Giao dịch đã phát sinh không thể chỉnh sửa", NotifyType.Warning)
                                    Exit Sub
                                End If
                                If hidID.Value <> "" Then
                                    objData.ID = hidID.Value
                                End If
                                If rep.ModifyStocks(objData, gID) Then
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
                        If item.GetDataKeyValue("ID") IsNot Nothing Then
                            objData.ID = CDec(item.GetDataKeyValue("ID"))
                        End If
                        If rep.ValidateStocksGenerate(objData) Then
                            ShowMessage("Giao dịch đã phát sinh không thể chỉnh sửa", NotifyType.Warning)
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
    Protected Sub btnEmployee_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEmployee.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            isLoadPopup = 1
            UpdateControlState()
            ctrlFindEmployeePopup.Show()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    Private Sub ctrlFindEmployeePopup_EmployeeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.EmployeeSelected
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim lstEmpID As New List(Of Decimal)
        Try
            lstEmpID = ctrlFindEmployeePopup.SelectedEmployeeID
            If lstEmpID.Count <> 0 Then
                FillData(lstEmpID(0))
            End If
            isLoadPopup = 0
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
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

            Dim strPath As String = Server.MapPath("~/ReportTemplates/Profile/StockInfo/")
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
                    finfo.CONTROL_NAME = "ctrlHU_Stocks"
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
                Dim strPath_Down As String = Server.MapPath("~/ReportTemplates/Profile/StockInfo/" + txtUpload.Text)
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
        Dim rep As New ProfileRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            If txtUpload.Text <> "" Then
                btnDownload.Enabled = True
            Else
                btnDownload.Enabled = False
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub cboPayType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboPayType.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim store As New ProfileStoreProcedure
        Try
            If CurrentState <> CommonMessage.STATE_NORMAL Then
                EnableControlls()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    Protected Sub rdEffectedDate_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles rdEffectedDate.SelectedDateChanged
        Dim item As New ContractTypeDTO
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            AutoCreate_StockNo()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            rgData.CurrentPageIndex = 0
            rgData.MasterTableView.SortExpressions.Clear()
            rgData.Rebind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

#Region "Custom"
    Private Sub EnableControlls()
        Dim store As New ProfileStoreProcedure
        Try
            If cboPayType.SelectedValue <> "" Then
                Dim code = store.GET_CODE_OT_OTHER_LIST(cboPayType.SelectedValue)
                If code = "COPHIEU" Then
                    ClearControlValue(rnMoneyDeal)
                    rnMoneyDeal.ReadOnly = True
                    reqStockDeal.Enabled = True
                    rnStockDeal.ReadOnly = False
                    reqMoneyDeal.Enabled = False
                ElseIf code = "TIENQUYCP" Then
                    ClearControlValue(rnStockDeal)
                    rnStockDeal.ReadOnly = True
                    reqMoneyDeal.Enabled = True
                    rnMoneyDeal.ReadOnly = False
                    reqStockDeal.Enabled = False
                End If
            Else
                rnMoneyDeal.ReadOnly = True
                reqStockDeal.Enabled = True
                rnStockDeal.ReadOnly = True
                reqMoneyDeal.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillData(ByVal empID As Decimal)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim store As New ProfileStoreProcedure
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Using rep As New ProfileBusinessRepository
                Dim item = rep.GetEmployeeByID(empID)
                hidEmployeeID.Value = item.ID.ToString
                txtEmployeeCode.Text = item.EMPLOYEE_CODE
                txtEmployeeName.Text = item.FULLNAME_VN
                txtTileName.Text = item.TITLE_NAME_VN
                txtOrgName.Text = item.ORG_NAME
                txtLocation.Text = item.CONTRACTED_UNIT_NAME
                If item.JOIN_DATE_STATE IsNot Nothing Then
                    rdDateState.SelectedDate = item.JOIN_DATE_STATE
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
            If CurrentState = CommonMessage.STATE_NEW Or CurrentState = CommonMessage.STATE_EDIT Then
                If rdEffectedDate.SelectedDate Is Nothing Then
                    Exit Sub
                End If
                ClearControlValue(txtCode)
                Dim stockNo = store.AUTOCREATE_STOCKNO(rdEffectedDate.SelectedDate)

                txtCode.Text = stockNo
            End If
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
            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
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
        Dim _filter As New StocksDTO
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
                    Return rep.GetStocks(_filter, 0, Integer.MaxValue, 0, _param, Sorts).ToTable()
                Else
                    Return rep.GetStocks(_filter, 0, Integer.MaxValue, 0, _param).ToTable()
                End If
            Else
                Dim lstData As List(Of StocksDTO)
                If Sorts IsNot Nothing Then
                    lstData = rep.GetStocks(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param, Sorts)
                Else
                    lstData = rep.GetStocks(_filter, rgData.CurrentPageIndex, rgData.PageSize, MaximumRows, _param)
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