Imports Common
Imports Common.CommonBusiness
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Performance.PerformanceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlPE_CriteriaTitlegroupRankNewEdit
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

    Property lstCriteria As List(Of CriteriaTitleGroupRankDTO)
        Get
            Return ViewState(Me.ID & "_lstCriteria")
        End Get
        Set(ByVal value As List(Of CriteriaTitleGroupRankDTO))
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
                    Dim obj As New CriteriaTitleGroupRankDTO
                    Dim objDetail As New CriteriaTitleGroupRankDTO

                    obj.ID = Decimal.Parse(Request.Params("ID"))
                    CurrentState = CommonMessage.STATE_EDIT
                    obj = rep.GetCriteriaTitleGroupRankbyID(obj)

                    If obj IsNot Nothing Then
                        hidID.Value = obj.ID
                        cboTitleGroup.Enabled = False
                        If IsNumeric(obj.GROUPTITLE_ID) Then
                            cboTitleGroup.SelectedValue = obj.GROUPTITLE_ID
                        End If

                        If IsNumeric(obj.CRITERIA_ID) Then
                            cbCriteria.SelectedValue = obj.CRITERIA_ID
                        End If

                        If IsDate(obj.EFFECT_DATE) Then
                            rdEffectDate.SelectedDate = obj.EFFECT_DATE
                        End If
                        txtNote.Text = obj.NOTE
                        lstCriteria = New List(Of CriteriaTitleGroupRankDTO)
                        If obj.lstObj.Count > 0 Then
                            For Each item In obj.lstObj
                                objDetail = New CriteriaTitleGroupRankDTO
                                objDetail.ID = item.ID
                                objDetail.CRITERIA_TITLEGROUP_ID = item.CRITERIA_TITLEGROUP_ID
                                objDetail.RANK_FROM = item.RANK_FROM
                                objDetail.RANK_TO = item.RANK_TO
                                objDetail.POINT = item.POINT
                                objDetail.DESCRIPTION = item.DESCRIPTION
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
        Dim objData As New CriteriaTitleGroupRankDTO
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

                        objData.GROUPTITLE_ID = cboTitleGroup.SelectedValue
                        objData.CRITERIA_ID = cbCriteria.SelectedValue
                        objData.EFFECT_DATE = rdEffectDate.SelectedDate
                        objData.NOTE = txtNote.Text

                        objData.lstObj = New List(Of CriteriaTitleGroupRankDTO)
                        Dim sumRatio As Decimal = 0
                        If rgCriteria.Items.Count > 0 Then
                            For Each item As GridDataItem In rgCriteria.Items
                                Dim objDataDetail As New CriteriaTitleGroupRankDTO
                                objDataDetail.ID = item.GetDataKeyValue("ID")
                                objDataDetail.CRITERIA_TITLEGROUP_ID = item.GetDataKeyValue("CRITERIA_TITLEGROUP_ID")
                                objDataDetail.RANK_FROM = item.GetDataKeyValue("RANK_FROM")
                                objDataDetail.RANK_TO = item.GetDataKeyValue("RANK_TO")
                                objDataDetail.POINT = item.GetDataKeyValue("POINT")
                                objDataDetail.DESCRIPTION = item.GetDataKeyValue("DESCRIPTION")
                                objData.lstObj.Add(objDataDetail)
                            Next
                        End If

                        If IsNumeric(hidID.Value) AndAlso hidID.Value > 0 Then
                            'Modify
                            objData.ID = hidID.Value
                            'Dim check = rep.ValidateCriteriaTitleGroup(objData)
                            'If check = False Then
                            '    ShowMessage(Translate("Đã tồn tại thiết lập"), NotifyType.Warning)
                            '    Exit Sub
                            'End If

                            If rep.ModifyCriteriaTitleGroupRank(objData) Then
                                Dim str As String = "getRadWindow().close('1');"
                                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                                Exit Sub
                            End If
                        Else
                            'Insert

                            'Dim check = rep.ValidateCriteriaTitleGroup(objData)
                            'If check = False Then
                            '    ShowMessage(Translate("Đã tồn tại thiết lập"), NotifyType.Warning)
                            '    Exit Sub
                            'End If

                            If rep.InsertCriteriaTitleGroupRank(objData) Then
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
                lstCriteria = New List(Of CriteriaTitleGroupRankDTO)
                rgCriteria.DataSource = New List(Of CriteriaTitleGroupRankDTO)
            End If

        Catch ex As Exception

            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Private Sub rgCriteria_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles rgCriteria.ItemCommand
        Try
            Select Case e.CommandName
                Case "InsertCriteria"

                    If Not IsNumeric(ntxtRankFrom.Value) Then
                        ShowMessage(Translate("Chưa nhập Giá trị từ"), NotifyType.Warning)
                        Exit Sub
                    End If

                    If Not IsNumeric(ntxtRankTo.Value) Then
                        ShowMessage(Translate("Chưa nhập Giá trị đến"), NotifyType.Warning)
                        Exit Sub
                    End If

                    If Not IsNumeric(ntxtPoint.Value) Then
                        ShowMessage(Translate("Chưa nhập Mức tiền"), NotifyType.Warning)
                        Exit Sub
                    End If

                    Dim allow1 As New CriteriaTitleGroupRankDTO
                    allow1.POINT = ntxtPoint.Value
                    allow1.RANK_FROM = ntxtRankFrom.Value
                    allow1.RANK_TO = ntxtRankTo.Value
                    allow1.DESCRIPTION = txtDESCRIPTION.Text

                    lstCriteria.Add(allow1)

                    rgCriteria.Rebind()

                Case "DeleteCriteria"
                    If rgCriteria.SelectedItems.Count = 0 Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), NotifyType.Warning)
                        Exit Sub
                    End If

                    For Each selected As GridDataItem In rgCriteria.SelectedItems
                        lstCriteria.RemoveAll(Function(x) x.RANK_FROM = selected.GetDataKeyValue("RANK_FROM"))
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
            End If

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region


End Class

