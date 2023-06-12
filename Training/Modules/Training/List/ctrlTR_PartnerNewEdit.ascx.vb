Imports Framework.UI
Imports Common
Imports Telerik.Web.UI
Imports Framework.UI.Utilities
Imports Training.TrainingRepository
Imports Training.TrainingBusiness
Imports Common.CommonBusiness

Public Class ctrlTR_PartnerNewEdit
    Inherits CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup
    Protected WithEvents ctrlFindSigner As ctrlFindEmployeePopup
    Public Overrides Property MustAuthorize As Boolean = False
    Dim lstLecture As New List(Of LectureDTO)
#Region "Property"
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
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
    Property isEdit As Boolean
        Get
            Return ViewState(Me.ID & "_isEdit")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_isEdit") = value
        End Set
    End Property
    Property Employee_id As Integer
        Get
            Return ViewState(Me.ID & "_Employee_id")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_Employee_id") = value
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
    Property Partner As TR_PARTNERDTO
        Get
            Return ViewState(Me.ID & "_objPartner")
        End Get
        Set(ByVal value As TR_PARTNERDTO)
            ViewState(Me.ID & "_objPartner") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            UpdateControlState()
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
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            Using rep As New TrainingRepository
                Message = Request.Params("VIEW")
                Select Case Message
                    Case "TRUE"
                        Dim obj As New TR_PARTNERDTO
                        Dim objLecture As New List(Of LectureDTO)
                        obj.ID = Decimal.Parse(Request.Params("ID"))
                        CurrentState = CommonMessage.STATE_EDIT
                        obj = rep.GetTR_PartnerById(obj.ID)
                        objLecture = rep.GetLectureByIdPartner(obj.ID)
                        If obj IsNot Nothing Then
                            txtCode.Text = obj.CODE
                            txtName.Text = obj.NAME
                            txtField.Text = obj.FIELD
                            txtPIT_NO.Text = obj.PIT_NO
                            txtBUSINESS_LICENSE_NO.Text = obj.BUSINESS_LICENSE_NO
                            txtWebsite.Text = obj.WEBSITE
                            txtFax.Text = obj.FAX
                            txtPHONE_ORG.Text = obj.PHONE_ORG
                            txtAddress.Text = obj.PHONE_ORG
                            txtFULLNAME_SURROGATE.Text = obj.FULLNAME_SURROGATE
                            txtTITLE_SURROGATE.Text = obj.TITLE_SURROGATE
                            txtPHONE_SURROGATE.Text = obj.PHONE_SURROGATE
                            txtEMAIL_SURROGATE.Text = obj.EMAIL_SURROGATE
                            txtFULLNAME_CONTACT.Text = obj.FULLNAME_CONTACT
                            txtTITLE_CONTACT.Text = obj.TITLE_CONTACT
                            txtPHONE_CONTACT.Text = obj.PHONE_CONTACT
                            txtEMAIL_CONTACT.Text = obj.EMAIL_CONTACT
                            ' lấy dữ liệu lên lưới danh mục giảng viên
                            lstLecture = objLecture

                            _Value = obj.ID

                        End If
                End Select
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"
    Private Sub ctrlFindPopup_CancelClicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

        Dim obj As TR_PARTNERDTO
        Dim rep As New TrainingRepository
        Dim gCODE As String = ""
        Dim gstatus As Integer = 0
        Dim sAction As String
        Dim lstEmp As New List(Of EmployeeDTO)
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    For Each item As GridDataItem In rgLecture.Items
                        Dim allow = New LectureDTO
                        allow.ID = item.GetDataKeyValue("STT")
                        allow.NAME = item.GetDataKeyValue("NAME")
                        allow.BIRDTH_DAY = item.GetDataKeyValue("BIRDTH_DAY")
                        allow.EXPERIENCE_TEACHER = item.GetDataKeyValue("EXPERIENCE_TEACHER")
                        allow.FIELDS_TEACHER = item.GetDataKeyValue("FIELDS_TEACHER")
                        allow.REMARK = txtREMARK.Text
                        lstLecture.Add(allow)
                    Next
                    If lstLecture.Count <= 0 Then
                        ShowMessage(Translate("Bạn chưa nhập giảng viên đào tạo!"), NotifyType.Error)
                        Exit Sub
                    End If
                    If _Value.HasValue Then
                        obj = New TR_PARTNERDTO
                        obj.ID = _Value.Value
                        obj.NAME = txtName.Text
                        obj.CODE = txtCode.Text
                        obj.FIELD = txtField.Text
                        obj.PIT_NO = txtPIT_NO.Text
                        obj.BUSINESS_LICENSE_NO = txtBUSINESS_LICENSE_NO.Text
                        obj.WEBSITE = txtWebsite.Text
                        obj.FAX = txtFax.Text
                        obj.PHONE_ORG = txtPHONE_ORG.Text
                        obj.ADDRESS = txtAddress.Text
                        obj.FULLNAME_SURROGATE = txtFULLNAME_SURROGATE.Text
                        obj.TITLE_SURROGATE = txtTITLE_SURROGATE.Text
                        obj.PHONE_SURROGATE = txtPHONE_SURROGATE.Text
                        obj.EMAIL_SURROGATE = txtEMAIL_SURROGATE.Text
                        obj.FULLNAME_CONTACT = txtFULLNAME_CONTACT.Text
                        obj.TITLE_CONTACT = txtTITLE_CONTACT.Text
                        obj.PHONE_CONTACT = txtPHONE_CONTACT.Text
                        obj.EMAIL_CONTACT = txtEMAIL_CONTACT.Text
                        If rep.ModifyTR_PARTNER(obj, lstLecture, gstatus) Then
                            Dim str As String = "getRadWindow().Close('1');"
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                        End If
                    Else
                        obj = New TR_PARTNERDTO
                        obj.NAME = txtName.Text
                        obj.CODE = txtCode.Text
                        obj.FIELD = txtField.Text
                        obj.PIT_NO = txtPIT_NO.Text
                        obj.BUSINESS_LICENSE_NO = txtBUSINESS_LICENSE_NO.Text
                        obj.WEBSITE = txtWebsite.Text
                        obj.FAX = txtFax.Text
                        obj.PHONE_ORG = txtPHONE_ORG.Text
                        obj.ADDRESS = txtAddress.Text
                        obj.FULLNAME_SURROGATE = txtFULLNAME_SURROGATE.Text
                        obj.TITLE_SURROGATE = txtTITLE_SURROGATE.Text
                        obj.PHONE_SURROGATE = txtPHONE_SURROGATE.Text
                        obj.EMAIL_SURROGATE = txtEMAIL_SURROGATE.Text
                        obj.FULLNAME_CONTACT = txtFULLNAME_CONTACT.Text
                        obj.TITLE_CONTACT = txtTITLE_CONTACT.Text
                        obj.PHONE_CONTACT = txtPHONE_CONTACT.Text
                        obj.EMAIL_CONTACT = txtEMAIL_CONTACT.Text
                        If rep.InsertTR_PARTNER(obj, lstLecture, gstatus) Then
                            Dim str As String = "getRadWindow().Close('1');"
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "clientButtonClicking", str, True)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Success)
                        End If
                    End If
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand

    End Sub

    Private Sub rgLecture_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles rgLecture.ItemCommand
        Try
            Select Case e.CommandName
                Case "InsertAllow"
                    If txtNameGV.Text = "" Then
                        ShowMessage(Translate("Bạn phải nhập tên nhân viên"), NotifyType.Warning)
                        Exit Sub
                    End If
                    Dim lstLectureList As New List(Of Decimal)
                    For Each item As GridDataItem In rgLecture.Items
                        lstLectureList.Add(item.GetDataKeyValue("STT"))
                    Next
                    For Each item As GridDataItem In rgLecture.Items
                        Dim allow = New LectureDTO
                        allow.ID = item.GetDataKeyValue("STT")
                        allow.NAME = item.GetDataKeyValue("NAME")
                        allow.BIRDTH_DAY = item.GetDataKeyValue("BIRDTH_DAY")
                        allow.EXPERIENCE_TEACHER = item.GetDataKeyValue("EXPERIENCE_TEACHER")
                        allow.FIELDS_TEACHER = item.GetDataKeyValue("FIELDS_TEACHER")
                        allow.REMARK = txtREMARK.Text
                        lstLecture.Add(allow)
                    Next
                    Dim allow1 As LectureDTO
                    allow1 = New LectureDTO
                    allow1.STT = lstLecture.Count + 1
                    allow1.NAME = txtNameGV.Text
                    allow1.BIRDTH_DAY = rdBirth_date.SelectedDate
                    allow1.EXPERIENCE_TEACHER = txtEXPERIENCE_TEACHER.Text
                    allow1.FIELDS_TEACHER = txtFIELDS_TEACHER.Text
                    allow1.REMARK = txtREMARK.Text
                    lstLecture.Add(allow1)

                    ClearControlValue(txtNameGV, rdBirth_date, txtEXPERIENCE_TEACHER, txtFIELDS_TEACHER, txtREMARK)
                    rgLecture.Rebind()
                Case "DeleteAllow"
                    For Each item As GridDataItem In rgLecture.Items
                        Dim isExist As Boolean = False
                        For Each selected As GridDataItem In rgLecture.SelectedItems
                            If item.GetDataKeyValue("STT") = selected.GetDataKeyValue("STT") Then
                                isExist = True
                                Exit For
                            End If
                        Next
                        If Not isExist Then
                            Dim allow As New LectureDTO
                            allow.STT = item.GetDataKeyValue("STT")
                            allow.NAME = item.GetDataKeyValue("NAME")
                            allow.BIRDTH_DAY = item.GetDataKeyValue("BIRDTH_DAY")
                            allow.EXPERIENCE_TEACHER = item.GetDataKeyValue("EXPERIENCE_TEACHER")
                            allow.FIELDS_TEACHER = item.GetDataKeyValue("FIELDS_TEACHER")
                            lstLecture.Add(allow)
                        End If
                    Next
                    rgLecture.Rebind()
            End Select

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Private Sub rgLecture_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgLecture.NeedDataSource
        CreateDataLecture(lstLecture)
    End Sub

#End Region

#Region "Custom"
    Private Sub CreateDataLecture(Optional ByVal lstLecture As List(Of LectureDTO) = Nothing)
        If lstLecture Is Nothing Then
            lstLecture = New List(Of LectureDTO)
        End If
        rgLecture.DataSource = lstLecture
    End Sub

    Private Sub GetDataCombo()
        'Dim rep As New AttendanceRepository
        'Try
        '    If ListComboData Is Nothing Then
        '        ListComboData = New Attendance.AttendanceBusiness.ComboBoxDataDTO
        '        ListComboData.GET_LIST_HS_OT = True
        '        rep.GetComboboxData(ListComboData)
        '    End If
        '    FillRadCombobox(cbohs_ot, ListComboData.LIST_LIST_HS_OT, "CODE", "ID", True)
        '    If ListComboData.LIST_LIST_HS_OT.Count > 0 Then
        '        cbohs_ot.SelectedIndex = 0
        '    End If
        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub
#End Region


    
End Class

