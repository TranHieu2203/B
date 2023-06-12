Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Recruitment.RecruitmentBusiness
Imports Telerik.Web.UI

Public Class ctrlRC_HRYearPlaningNewEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = True

#Region "Property"

    Property IDSelect As Decimal?
        Get
            Return PageViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal?)
            PageViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

    Property dtHRPlaning As DataTable
        Get
            Return PageViewState(Me.ID & "_dtHRPlaning")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtHRPlaning") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            If Not IsPostBack Then
                GetParams()
                Refresh()
                UpdateControlState()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub BindData()
        Try
            GetDataCombo()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New RecruitmentRepository
        Try
            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetHrYearPlaningByID(IDSelect)
                    If IsNumeric(obj.YEAR) Then
                        rnYear.Value = obj.YEAR
                    End If
                    txtVersion.Text = obj.VERSION
                    If IsDate(obj.EFFECT_DATE) Then
                        rdEffectDate.SelectedDate = obj.EFFECT_DATE
                    End If
                    txtNote.Text = obj.NOTE
                    If Not rep.CheckExistData(IDSelect) Then
                        tbarMain.Enabled = False
                        EnableControlAll(False, rnYear, txtNote, txtVersion, cboLstDefault, rdEffectDate)
                        ShowMessage(Translate("Đã khởi tạo dữ liệu định biên chi tiết, không thể chỉnh sửa"), Utilities.NotifyType.Warning)
                    End If
                Case "InsertView"
                    CurrentState = CommonMessage.STATE_NEW
            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim gID As Decimal
        Dim rep As New RecruitmentRepository
        Dim store As New RecruitmentStoreProcedure
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName

                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Dim obj As New HRYearPlaningDTO
                        obj.ID = If(IsNumeric(IDSelect), IDSelect, 0)
                        obj.YEAR = rnYear.Value
                        obj.VERSION = txtVersion.Text.Trim
                        obj.NOTE = txtNote.Text.Trim
                        obj.EFFECT_DATE = rdEffectDate.SelectedDate
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                If rep.CheckExistEffectDate_HRYearPlaning(0, obj.EFFECT_DATE) Then
                                    ShowMessage(Translate("Ngày hiệu lực định biên đã tồn tại"), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.InsertHRYearPlaning(obj, gID) Then
                                    If IsNumeric(cboLstDefault.SelectedValue) Then
                                        store.COPY_HR_DETAIL(cboLstDefault.SelectedValue, gID, LogHelper.CurrentUser.USERNAME.ToUpper)
                                    End If
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT
                                If rep.CheckExistEffectDate_HRYearPlaning(IDSelect, obj.EFFECT_DATE) Then
                                    ShowMessage(Translate("Ngày hiệu lực định biên đã tồn tại"), Utilities.NotifyType.Warning)
                                    Exit Sub
                                End If
                                If rep.ModifyHRYearPlaning(obj, gID) Then
                                    If IsNumeric(cboLstDefault.SelectedValue) Then
                                        store.COPY_HR_DETAIL(cboLstDefault.SelectedValue, gID, LogHelper.CurrentUser.USERNAME.ToUpper)
                                    End If
                                    Dim str As String = "getRadWindow().close('1');"
                                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    Dim str As String = "getRadWindow().close('0');"
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
        UpdateControlState()
    End Sub

    Private Sub cboLstDefault_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cboLstDefault.SelectedIndexChanged
        Try
            ClearControlValue(rnYear, txtNote, txtVersion, rdEffectDate, hidCopyID)
            If cboLstDefault.SelectedValue <> "" Then
                Dim copyer = (From p In dtHRPlaning Where p("ID") = cboLstDefault.SelectedValue).FirstOrDefault
                hidCopyID.Value = cboLstDefault.SelectedValue
                If IsNumeric(copyer("YEAR")) Then
                    rnYear.Value = CDec(copyer("YEAR"))
                End If
                If IsDate(copyer("EFFECT_DATE")) Then
                    rdEffectDate.SelectedDate = CDate(copyer("EFFECT_DATE"))
                End If
                If copyer("NOTE") IsNot Nothing AndAlso Not IsDBNull(copyer("NOTE")) Then
                    txtNote.Text = copyer("NOTE")
                End If
                If copyer("VERSION") IsNot Nothing AndAlso Not IsDBNull(copyer("VERSION")) Then
                    txtVersion.Text = copyer("VERSION")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Custom"

    Public Overrides Sub UpdateControlState()
        Try
        Catch ex As Exception
            Throw ex
        End Try
        'ChangeToolbarState()
    End Sub

    Private Sub GetDataCombo()
        Dim store As New RecruitmentStoreProcedure
        Try
            dtHRPlaning = store.GET_HR_PLANINGS_DEFAULT()
            FillRadCombobox(cboLstDefault, dtHRPlaning, "NAME", "ID")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetParams()
        Try
            If CurrentState Is Nothing Then
                If Request.Params("ID") IsNot Nothing Then
                    IDSelect = Decimal.Parse(Request.Params("ID"))
                End If
                If IDSelect IsNot Nothing Then
                    Refresh("UpdateView")
                Else
                    Refresh("InsertView")
                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region


End Class