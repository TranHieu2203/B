Imports System.IO
Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Telerik.Web.UI


Public Class ctrlInsHealthImport
    Inherits Common.CommonView
    'Private WithEvents viewRegister As ctrlShiftPlanningRegister
    'Public isPhysical As Decimal = Decimal.Parse(ConfigurationManager.AppSettings("PHYSICAL_PATH"))

#Region "Property & Variable"
    Public Property popup As RadWindow
    Public Property popupId As String
    Private Property LastStartDate As Nullable(Of Date)
        Get
            Return PageViewState("LastStartDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastStartDate") = value
        End Set
    End Property

    Private Property LastEndDate As Nullable(Of Date)
        Get
            Return PageViewState("LastEndDate")
        End Get
        Set(ByVal value As Nullable(Of Date))
            PageViewState("LastEndDate") = value
        End Set
    End Property

    'Private Property ListSign As List(Of SIGN_DTO)
    '    Get
    '        Return PageViewState(Me.ID & "_ListSign")
    '    End Get
    '    Set(ByVal value As List(Of SIGN_DTO))
    '        PageViewState(Me.ID & "_ListSign") = value
    '    End Set
    'End Property

    Public ReadOnly Property PageId As String
        Get
            Return Me.ID
        End Get
    End Property

    Private Property IsCheckOrg As Boolean
        Get
            Return PageViewState("IsCheckOrg")
        End Get
        Set(ByVal value As Boolean)
            PageViewState("IsCheckOrg") = value
        End Set
    End Property


    'Private Property ListPeriod As List(Of PERIOD_DTO)
    '    Get
    '        Return ViewState(Me.ID & "_ListPeriod")
    '    End Get
    '    Set(ByVal value As List(Of PERIOD_DTO))
    '        ViewState(Me.ID & "_ListPeriod") = value
    '    End Set
    'End Property


    Public Property InsCompList As DataTable
        Get
            Return ViewState(Me.ID & "_InsCompList")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_InsCompList") = value
        End Set
    End Property

    Public Property SelectedItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_SelectedItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedItemList") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            GetDataCombo()

            rgGridData.AllowCustomPaging = True
            rgGridData.PageSize = Common.Common.DefaultPageSize
            popup = CType(Me.Page, AjaxPage).PopupWindow
            popup.Behaviors = WindowBehaviors.Close
            popupId = popup.ClientID

            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Import, ToolbarItem.Export)
            'Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            'CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation
            'ctrlOrg.CheckChildNodes = True
            'ctrlOrg.CheckBoxes = TreeNodeTypes.All

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            'ShowPopupEmployee()
            Me.rgGridData.SetFilter()
            If Not IsPostBack Then
                UpdateControlState(CommonMessage.STATE_NORMAL)
                UpdateToolbarState(CommonMessage.STATE_NORMAL)
                txtID.Text = "0"
                ' Refresh()
                ' UpdateControlState()
            End If

            'rgGridData.Culture = Common.Common.SystemLanguage
        Catch ex As Exception
            'DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            Dim dic As New Dictionary(Of String, Control)
            IsCheckOrg = False 'Chon org
            dic.Add("ID", txtID)
            'dic.Add("EMPLOYEE_CODE", txtEMPLOYEE_ID)

            Utilities.OnClientRowSelectedChanged(rgGridData, dic)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Call LoadDataGrid()
            CurrentState = CommonMessage.STATE_NORMAL
            UpdateToolbarState(CurrentState)
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub UpdateControlState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)

                Case CommonMessage.STATE_NEW, CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)


                Case CommonMessage.STATE_DELETE

                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridData, True)
            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub UpdateToolbarState(ByVal sState As String)
        Try
            Select Case sState
                Case CommonMessage.STATE_NORMAL
                    Utilities.EnabledGridNotPostback(rgGridData, True)
                    CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = True
                    ' CType(Me.MainToolBar.Items(2), RadToolBarButton).Enabled = True

                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = False

                    'CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = True

                Case CommonMessage.STATE_NEW
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    'CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    'CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = False


                Case CommonMessage.STATE_EDIT
                    Utilities.EnabledGridNotPostback(rgGridData, False)
                    'CType(Me.MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    'CType(Me.MainToolBar.Items(1), RadToolBarButton).Enabled = False

                    'CType(Me.MainToolBar.Items(3), RadToolBarButton).Enabled = True
                    'CType(Me.MainToolBar.Items(4), RadToolBarButton).Enabled = True

                    'CType(Me.MainToolBar.Items(6), RadToolBarButton).Enabled = False

                Case CommonMessage.STATE_DELETE

                Case "Nothing"
                    Utilities.EnabledGridNotPostback(rgGridData, True)

            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Protected Sub btnFIND_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFind.Click
        IsCheckOrg = False
        Call LoadDataGrid()
    End Sub

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        'Dim objOrgFunction As New OrganizationDTO
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CREATE
                    Call ResetForm()
                    CurrentState = CommonMessage.STATE_NEW
                    'txtCODE.Focus()
                Case CommonMessage.TOOLBARITEM_EDIT
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần sửa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    CurrentState = CommonMessage.STATE_EDIT
                    'txtCODE.Focus()
                    'Case CommonMessage.TOOLBARITEM_ACTIVE, CommonMessage.TOOLBARITEM_DEACTIVE

                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    'FillDropDownList(cboLevel_ID, ListComboData.LIST_ORG_LEVEL, "NAME_VN", "ID", Common.Common.SystemLanguage, True, cboLevel_ID.SelectedValue)
                    'FillDataByTree()
                    Call ResetForm()
                Case CommonMessage.TOOLBARITEM_SAVE
                    Call SaveData()
                    CurrentState = CommonMessage.STATE_NORMAL

                Case CommonMessage.TOOLBARITEM_DELETE
                    If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                        ShowMessage(Translate("Bạn chưa chọn nội dung cần xóa."), NotifyType.Warning)
                        Exit Sub
                    End If
                    If CheckData_Delete() Then
                        ctrlMessageBox.MessageText = Translate("Bạn có muốn xóa dữ liệu ?")
                        ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_DELETE
                        ctrlMessageBox.DataBind()
                        ctrlMessageBox.Show()
                    Else
                        ShowMessage(Translate("Dữ liệu đang dùng không được xóa"), NotifyType.Warning)
                    End If

                Case CommonMessage.TOOLBARITEM_IMPORT
                    ctrlUpload1.Show()


                Case CommonMessage.TOOLBARITEM_EXPORT
                    Call Export()

            End Select
            UpdateControlState(CurrentState)
            UpdateToolbarState(CurrentState)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgGridData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgGridData.NeedDataSource
        Call LoadDataGrid(False)
    End Sub

    Protected Function RepareDataForAction() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        For Each dr As Telerik.Web.UI.GridDataItem In rgGridData.SelectedItems
            lst.Add(dr.GetDataKeyValue("ID"))
        Next
        Return lst
    End Function

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_DELETE And e.ButtonID = MessageBoxButtonType.ButtonYes Then
                Call DeleteData()
                Call ResetForm()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Function CheckData_Delete() As Boolean
        Try
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

#End Region

#Region "Function & Sub"

    Private Sub ResetForm()
        Try
            txtID.Text = "0"
            'txtCODE.Text = ""
            'txtNAME.Text = ""
            'txtADDRESS.Text = ""
            'txtPHONE_NUMBER.Text = ""
            'txtTREATMENTCODE.Text = ""
            'chkSTATUS.Checked = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub SaveData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient

            If rgGridData.MasterTableView.GetSelectedItems().Length = 0 Then
                ShowMessage(Translate("Bạn chưa chọn nội dung cần lưu."), NotifyType.Warning)
                Exit Sub
            End If

            Dim isFail As Integer = 0
            Dim isResult As Integer = 0
            For i As Integer = 0 To rgGridData.SelectedItems.Count - 1
                'Dim item As GridDataItem = rgGridData.SelectedItems(i)
                'Dim id As Integer = Integer.Parse(item("ID").Text)
                'Dim empid As Integer = Integer.Parse(item("EMPID").Text)
                'isResult = rep.UpdateInsHealthImport(Common.Common.GetUserName(), 0, empid, 0, "" _
                '                                  , txtEFFECTIVE_FROM_DATE.SelectedDate, txtEFFECTIVE_TO_DATE.SelectedDate _
                '                                  , txtEFFECTIVE_FROM_DATE.SelectedDate, txtEFFECTIVE_TO_DATE.SelectedDate)
                'If isResult = 0 Then
                '    isFail = 1
                'End If
            Next
            If isFail = 0 Then
                Refresh("UpdateView")
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Sub DeleteData()
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            'objOrgFunction.ID = Decimal.Parse(hidID.Value)
            If rep.DeleteInsHealthImport(Common.Common.GetUserName(), InsCommon.getNumber(txtID.Text)) Then
                Refresh("UpdateView")
                'Common.Common.OrganizationLocationDataSession = Nothing
            Else
                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub GetDataCombo()
        Try
            Dim lstSource = (New InsuranceBusiness.InsuranceBusinessClient).GetInsListInsurance(False)
            FillRadCombobox(ddlINSORG, lstSource, "NAME", "ID", False)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Try
            IsCheckOrg = False
            Call LoadDataGrid(True)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub LoadDataGrid(Optional ByVal IsDataBind As Boolean = True)
        Try
            Dim lstSource As DataTable
            Dim lstOrg As New List(Of Decimal)
            Dim lstOrgStr = ""
            If IsCheckOrg Then
                lstOrg = ctrlOrg.CheckedValueKeys

                For i As Integer = 0 To lstOrg.Count - 1
                    If i = lstOrg.Count - 1 Then
                        lstOrgStr = lstOrgStr & lstOrg(i).ToString
                    Else
                        lstOrgStr = lstOrgStr & lstOrg(i).ToString & ","
                    End If
                Next
                lstSource = (New InsuranceBusiness.InsuranceBusinessClient).GetInsHealthImportCheckOrg(Common.Common.GetUserName(), InsCommon.getNumber(0) _
                                                                                                        , txtEMPLOYEEID_SEARCH.Text, lstOrgStr, InsCommon.getNumber(IIf(ddlINSORG.SelectedValue = "", -1, ddlINSORG.SelectedValue)), Nothing, Nothing)
            Else
                lstSource = (New InsuranceBusiness.InsuranceBusinessClient).GetInsHealthImport(Common.Common.GetUserName(), InsCommon.getNumber(0) _
                                                                                                        , txtEMPLOYEEID_SEARCH.Text, InsCommon.getNumber(ctrlOrg.CurrentValue), InsCommon.getNumber(IIf(ddlINSORG.SelectedValue = "", -1, ddlINSORG.SelectedValue)), Nothing, Nothing)

            End If
            rgGridData.DataSource = lstSource
            rgGridData.MasterTableView.VirtualItemCount = lstSource.Rows.Count
            rgGridData.CurrentPageIndex = rgGridData.MasterTableView.CurrentPageIndex

            If IsDataBind Then
                rgGridData.DataBind()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub Export()
        Try
            Dim _error As String = ""

            Dim dtVariable = New DataTable
            dtVariable.Columns.Add(New DataColumn("FROM_DATE"))
            dtVariable.Columns.Add(New DataColumn("TO_DATE"))
            Dim drVariable = dtVariable.NewRow()
            drVariable("FROM_DATE") = Now.ToString("dd/MM/yyyy")
            drVariable("TO_DATE") = Now.ToString("dd/MM/yyyy")
            dtVariable.Rows.Add(drVariable)

            Using xls As New ExcelCommon
                Dim lstSource As DataTable = (New InsuranceBusiness.InsuranceBusinessClient).GetInsHealthImport(Common.Common.GetUserName(), InsCommon.getNumber(0) _
                                     , txtEMPLOYEEID_SEARCH.Text, InsCommon.getNumber(ctrlOrg.CurrentValue), InsCommon.getNumber(IIf(ddlINSORG.SelectedValue = "", -1, ddlINSORG.SelectedValue)), Nothing, Nothing)
                lstSource.TableName = "DATA"
                Dim lstDS As New DataSet
                lstDS = (New InsuranceBusiness.InsuranceBusinessClient).GetListDataImportHelth(InsCommon.getNumber(0))

                lstDS.Tables.Add(lstSource)

                'Rename name of datatable in dataset
                lstDS.Tables(0).TableName = "CITYHOPITAL"
                lstDS.Tables(1).TableName = "INSORG"
                lstDS.Tables(2).TableName = "CITY"
                lstDS.Tables(3).TableName = "SISTATUS"
                lstDS.Tables(4).TableName = "HISTATUS"
                lstDS.Tables(5).TableName = "DATA"


                If lstDS.Tables.Count = 0 OrElse lstDS Is Nothing Then
                    ShowMessage(Translate("Không có dữ liệu để xuất file! Vui lòng kiểm tra lại"), Framework.UI.Utilities.NotifyType.Warning)
                    Exit Sub
                End If
                If Not File.Exists(Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/ImportHealth.xls")) Then
                    ShowMessage(Translate("Mẫu báo cáo không tồn tại"), Framework.UI.Utilities.NotifyType.Warning)
                    Exit Sub
                End If
                Dim bCheck = xls.ExportExcelTemplate(
                Server.MapPath("~/ReportTemplates/" & Request.Params("mid") & "/" & Request.Params("group") & "/ImportHealth.xls"), "ImportHealthBHXH", lstDS, Response)
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ctrlUpload1_OkClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlUpload1.OkClicked
        Dim fileName As String
        Try

            'Dim rep As New HistaffFrameworkRepository
            'Dim obj As Object = rep.ExecuteStoreScalar("PKG_HCM_SYSTEM.READ_URL_CONTROL_FOLDER",
            '                                           New List(Of Object)(New Object() {"PATH_CONTROL_FOLDER", OUT_STRING}))

            Dim tempPath = "UploadFile\Insurance"


            'If isPhysical = 1 Then 'Nếu là 1 : lấy đường dẫn theo cấu hình ||    0: lấy đường dẫn theo web server
            '    If ctrlUpload1.UploadedFiles.Count > 1 Then
            '        ShowMessage("Chỉ được chọn 1 file control (.txt hoặc .ctr)", NotifyType.Error)
            '        Exit Sub
            '    Else
            '        Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)
            '        If file.GetExtension = ".txt" Or file.GetExtension = ".ctr" Then
            '            fileName = System.IO.Path.Combine(tempPath, file.FileName)
            '            file.SaveAs(fileName, True)
            '            'rtbSQLLoaderFile.Text = file.FileName
            '        Else
            '            ShowMessage(Translate("Vui lòng chọn file text hoặc file control chứa script SQLLOADER !!! Hệ thống chỉ nhận file .txt hoặc .ctr"), NotifyType.Error)
            '            Exit Sub
            '        End If
            '    End If
            'Else '0: lấy đường dẫn theo web server
            If ctrlUpload1.UploadedFiles.Count > 1 Then
                ShowMessage("Chỉ được chọn 1 file control (.xls hoặc .xlsx)", NotifyType.Error)
                Exit Sub
            Else
                Dim file As UploadedFile = ctrlUpload1.UploadedFiles(0)
                If file.GetExtension = ".xls" Or file.GetExtension = ".xlsx" Then
                    If Not Directory.Exists(Server.MapPath(tempPath)) Then
                        Directory.CreateDirectory(Server.MapPath(tempPath))
                    End If
                    fileName = System.IO.Path.Combine(Server.MapPath(tempPath), file.FileName)
                    file.SaveAs(fileName, True)
                    'rtbSQLLoaderFile.Text = file.FileName
                    Dim cls As New InsCommon.ReadExel
                    Dim strErr As String = ""
                    Dim dt As DataTable = cls.ReadExcelToDT(fileName, 2, 0, strErr, 0, 0)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            Call ImportData(dr)
                        Next
                        Call Refresh()
                    Else
                        ShowMessage(Translate("Không có dữ liệu để nhập hoặc không đúng tập tin mẫu. Vui lòng kiểm tra lại"), NotifyType.Error)
                        Exit Sub
                    End If
                Else
                    ShowMessage(Translate("Vui lòng chọn file excel !!! Hệ thống chỉ nhận file .xls hoặc .xlsx"), NotifyType.Error)
                    Exit Sub
                End If
            End If
            'End If
        Catch ex As Exception
            ShowMessage(Translate("Import bị lỗi : ") + ex.ToString(), NotifyType.Error)
        End Try
    End Sub

    Private Sub ImportData(ByVal dr As DataRow)
        Try
            Dim rep As New InsuranceBusiness.InsuranceBusinessClient
            rep.UpdateHealthImport(Common.Common.GetUserName, InsCommon.getString(dr("EMPLOYEE_ID")) _
                , InsCommon.getString(dr("INS_ORG_NAME")) _
                , InsCommon.getString(dr("SENIORITY_INSURANCE")) _
                , InsCommon.getString(dr("SOCIAL_NUMBER")) _
                , InsCommon.getString(dr("SOCIAL_STATUS")) _
                , IIf(InsCommon.getString(dr("SOCIAL_GRANT_DATE")).Length < 10, "", InsCommon.getString(dr("SOCIAL_GRANT_DATE"))) _
                , InsCommon.getString(dr("SOCIAL_SAVE_NUMBER")) _
                , InsCommon.getString(dr("HEALTH_NUMBER")) _
                , InsCommon.getString(dr("HEALTH_STATUS")) _
                , IIf(InsCommon.getString(dr("HEALTH_EFFECT_FROM_DATE")).Length < 10, "", InsCommon.getString(dr("HEALTH_EFFECT_FROM_DATE"))) _
                , IIf(InsCommon.getString(dr("HEALTH_EFFECT_TO_DATE")).Length < 10, "", InsCommon.getString(dr("HEALTH_EFFECT_TO_DATE"))) _
                , IIf(InsCommon.getString(dr("HEALTH_RECEIVE_DATE")).Length < 10, "", InsCommon.getString(dr("HEALTH_RECEIVE_DATE"))) _
                , InsCommon.getString(dr("HEALTH_RECEIVER")) _
                , InsCommon.getString(dr("HEALTH_AREA_INS"))
                )

        Catch ex As Exception

        End Try
    End Sub

#End Region

End Class