Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlCriteriaTitlegroupNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = True

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Performance/Module/Performance/List/" + Me.GetType().Name.ToString()

#Region "Property"

    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property
    Property isEdit As Boolean
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property

    Property _Value As Decimal?
        Get
            Return ViewState(Me.ID & "_Value")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_Value") = value
        End Set
    End Property

    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property

    Property lstCriteria As List(Of CriteriaTitleGroupDTO)
        Get
            Return ViewState(Me.ID & "_lstCriteria")
        End Get
        Set(ByVal value As List(Of CriteriaTitleGroupDTO))
            ViewState(Me.ID & "_lstCriteria") = value
        End Set
    End Property
    Property dtAllowUpdate As DataTable
        Get
            Return ViewState(Me.ID & "_dtAllowUpdate")
        End Get
        Set(ByVal value As DataTable)
            ViewState(Me.ID & "_dtAllowUpdate") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad, hiển thị dữ liệu trên trang
    ''' Làm mới trang
    ''' Cập nhật các trạng thái của các control trên page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            UpdateControlState()
            '_mylog.WriteLog(_mylog._info, _classPath, method,
            '                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ' _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' Gọi phương thức khởi tạo 
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarOT
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True

            Me.MainToolBar.OnClientButtonClicking = "clientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            ' _mylog.WriteLog(_mylog._info, _classPath, method,
            '                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex.ToString)
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetDataCombo()
            Refresh()
            ' _mylog.WriteLog(_mylog._info, _classPath, method,
            '                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham khoi taso toolbar voi cac button them moi, sua, xuat file, xoa, in hop dong
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            ' _mylog.WriteLog(_mylog._info, _classPath, method,
            '                                   CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ' _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message, 
    ''' Message co gia tri default la ""
    ''' Xet cac tuy chon gia tri cua Message la UpdateView, InsertView
    ''' Bind lai du lieu cho grid rgRegisterLeave
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New PerformanceRepository
        Dim rep_client As New PerformanceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Message = Request.Params("VIEW")
            Select Case Message
                Case "TRUE"
                    Dim obj As New CriteriaTitleGroupDTO
                    Dim objDetail As New CriteriaTitleGroupDTO

                    Dim objHTCH = rep.GetPE_Criteria_HTCH(New PE_Criteria_HTCHDTO)

                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetCriteriaTitleGroupbyID(obj)

                    If obj IsNot Nothing Then
                        hidID.Value = obj.ID
                        cboTitleGroup.Enabled = False
                        If IsNumeric(obj.GROUPTITLE_ID) Then
                            cboTitleGroup.SelectedValue = obj.GROUPTITLE_ID
                        End If
                        If IsNumeric(obj.BRAND_ID) Then
                            cboBrandID.SelectedValue = obj.BRAND_ID
                        End If
                        If IsDate(obj.EFFECT_DATE) Then
                            rdEffectDate.SelectedDate = obj.EFFECT_DATE
                        End If
                        txtNote.Text = obj.NOTE
                        lstCriteria = New List(Of CriteriaTitleGroupDTO)
                        If obj.lstObj.Count > 0 Then
                            For Each item In obj.lstObj
                                objDetail = New CriteriaTitleGroupDTO
                                objDetail.ID = item.ID
                                objDetail.CRITERIA_ID = item.CRITERIA_ID
                                Dim criteriaHTCH = (From p In objHTCH Where p.ID = objDetail.CRITERIA_ID).FirstOrDefault
                                objDetail.CRITERIA_NAME = criteriaHTCH.NAME
                                objDetail.IS_CHECK = criteriaHTCH.IS_CHECK
                                objDetail.RATIO = item.RATIO
                                lstCriteria.Add(objDetail)
                            Next
                        End If
                    End If

            End Select
            '_mylog.WriteLog(_mylog._info, _classPath, method,
            '                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: them moi, sua, xoa, xuat excel, huy, Ap dung, Ngung Ap dung
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objData As New CriteriaTitleGroupDTO
        Dim rep As New PerformanceRepository
        Dim rep_client As New PerformanceBusinessClient
        Dim gID As Integer = 0
        Dim gstatus As Integer = 0
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        If rgCriteria.Items.Count = 0 Then
                            ShowMessage(Translate("Chưa chọn tiêu chí"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If cboBrandID.SelectedValue <> "" Then
                            objData.BRAND_ID = cboBrandID.SelectedValue
                        End If
                        objData.GROUPTITLE_ID = cboTitleGroup.SelectedValue
                        objData.EFFECT_DATE = rdEffectDate.SelectedDate
                        objData.NOTE = txtNote.Text
                        objData.lstObj = New List(Of CriteriaTitleGroupDTO)
                        Dim sumRatio As Decimal = 0
                        If rgCriteria.Items.Count > 0 Then
                            For Each item As GridDataItem In rgCriteria.Items
                                Dim objDataDetail As New CriteriaTitleGroupDTO
                                objDataDetail.ID = item.GetDataKeyValue("ID")
                                objDataDetail.CRITERIA_ID = item.GetDataKeyValue("CRITERIA_ID")
                                objDataDetail.RATIO = item.GetDataKeyValue("RATIO")
                                sumRatio += objDataDetail.RATIO
                                objData.lstObj.Add(objDataDetail)
                            Next
                        End If
                        If sumRatio <> 100 Then
                            ShowMessage(Translate("Tổng tỷ trọng các tiêu chí phải là 100%"), NotifyType.Warning)
                            Exit Sub
                        End If
                        If IsNumeric(hidID.Value) AndAlso hidID.Value > 0 Then
                            'Modify
                            objData.ID = hidID.Value
                            Dim check = rep.ValidateCriteriaTitleGroup(objData)
                            If check = False Then
                                ShowMessage(Translate("Đã tồn tại thiết lập"), NotifyType.Warning)
                                Exit Sub
                            End If

                            'Dim checkDetail = rep.ValidateCriteriaTitleGroup_Detail(objData)
                            'If checkDetail = False Then
                            '    ShowMessage(Translate("Tiêu chí của thiết lập đã tồn tại"), NotifyType.Warning)
                            '    Exit Sub
                            'End If

                            If rep.ModifyCriteriaTitleGroup(objData) Then
                                Dim str As String = "getRadWindow().close('1');"
                                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                Exit Sub
                            End If
                        Else
                            'Insert

                            Dim check = rep.ValidateCriteriaTitleGroup(objData)
                            If check = False Then
                                ShowMessage(Translate("Đã tồn tại thiết lập"), NotifyType.Warning)
                                Exit Sub
                            End If

                            If rep.InsertCriteriaTitleGroup(objData) Then
                                Dim str As String = "getRadWindow().close('1');"
                                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                Exit Sub
                            End If
                        End If
                        'POPUPTOLINK()
                        'Response.Redirect("/Default.aspx?mid=Performance&fid=ctrlInsListContract&group=List")
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Dim str As String = "getRadWindow().close('1');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
            '_mylog.WriteLog(_mylog._info, _classPath, method,
            '                                  CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            '_mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub



    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load du lieu cho cac combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Try
            Dim rep As New PerformanceRepository

            Dim dtData = rep.GetOtherList("HU_TITLE_GROUP", Common.Common.SystemLanguage.Name, True)
            FillRadCombobox(cboTitleGroup, dtData, "NAME", "ID")

            dtData = rep.GetOtherList("BRAND", Common.Common.SystemLanguage.Name, True)
            dtData = dtData.AsEnumerable().Where(Function(f) IsDBNull(f("ID")) OrElse (Not IsDBNull(f("ATTRIBUTE1")) AndAlso f("ATTRIBUTE1") = 1)).CopyToDataTable()
            FillRadCombobox(cboBrandID, dtData, "NAME", "ID")

            Dim objHTCH = rep.GetPE_Criteria_HTCH(New PE_Criteria_HTCHDTO).ToTable
            FillRadCombobox(cbCriteria, objHTCH, "NAME", "ID")

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgCriteria_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCriteria.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow.Selected = True
        End If
    End Sub
    Private Sub rgCriteria_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgCriteria.NeedDataSource
        Try
            If lstCriteria IsNot Nothing AndAlso lstCriteria.Count > 0 Then
                rgCriteria.DataSource = lstCriteria
            Else
                lstCriteria = New List(Of CriteriaTitleGroupDTO)
                rgCriteria.DataSource = New List(Of CriteriaTitleGroupDTO)
            End If

        Catch ex As Exception

            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgCriteria_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgCriteria.ItemCommand
        Try
            Select Case e.CommandName
                Case "InsertCriteria"
                    If Not IsNumeric(cbCriteria.SelectedValue) Then
                        ShowMessage(Translate("Mời chọn tiêu chí"), NotifyType.Warning)
                        cbCriteria.Focus()
                        Exit Sub
                    End If
                    Dim lstCriteriaList As New List(Of Decimal)
                    For Each item As GridDataItem In rgCriteria.Items
                        lstCriteriaList.Add(item.GetDataKeyValue("CRITERIA_ID"))
                    Next
                    If lstCriteriaList.Contains(cbCriteria.SelectedValue) Then
                        ShowMessage(Translate("Chương trình bảo hiểm đã tồn tại dưới lưới"), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim allow1 As New CriteriaTitleGroupDTO

                    allow1.CRITERIA_ID = cbCriteria.SelectedValue
                    allow1.CRITERIA_NAME = cbCriteria.Text
                    allow1.IS_CHECK = chkCriteria.Checked
                    allow1.RATIO = rdRatio.Value
                    lstCriteria.Add(allow1)
                    ClearControlValue(cbCriteria, chkCriteria, rdRatio)
                    rgCriteria.Rebind()

                Case "DeleteCriteria"
                    If rgCriteria.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    'ctrlMessageBox.MessageText = Translate(CommonMessage.MESSAGE_CONFIRM_DELETE)
                    'ctrlMessageBox.ActionName = "REMOVECRITERIA"
                    'ctrlMessageBox.DataBind()
                    'ctrlMessageBox.Show()
                    For Each selected As GridDataItem In rgCriteria.SelectedItems
                        lstCriteria.RemoveAll(Function(x) x.CRITERIA_ID = selected.GetDataKeyValue("CRITERIA_ID"))
                    Next
                    rgCriteria.Rebind()
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 06/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Update trang thai cua cac control theo state
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub UpdateControlState()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'Select Case isLoadPopup
            '    Case 1
            '        If Not phFindEmployee.Controls.Contains(ctrlFindEmployeePopup) Then
            '            ctrlFindEmployeePopup = Me.Register("ctrlFindEmployeePopup", "Common", "ctrlFindEmployeePopup")
            '            phFindEmployee.Controls.Add(ctrlFindEmployeePopup)
            '            ctrlFindEmployeePopup.MultiSelect = False
            '        End If
            'End Select
            ' _mylog.WriteLog(_mylog._info, _classPath, method,
            '                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ' _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub cbCriteria_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cbCriteria.SelectedIndexChanged
        Dim rep As New PerformanceRepository
        Try
            If IsNumeric(e.Value) Then
                Dim objHTCH = rep.GetPE_Criteria_HTCH(New PE_Criteria_HTCHDTO)
                Dim dtData = (From p In objHTCH Where p.ID = e.Value).FirstOrDefault
                chkCriteria.Checked = dtData.IS_CHECK
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region


End Class

