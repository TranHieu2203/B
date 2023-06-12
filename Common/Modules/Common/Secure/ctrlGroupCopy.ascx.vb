Imports Framework.UI
Imports Framework.UI.Utilities
Imports WebAppLog

Public Class ctrlGroupCopy
    Inherits CommonView

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/Secure/" + Me.GetType().Name.ToString()
    Public Overrides Property MustAuthorize As Boolean = False
    Delegate Sub GroupCopySelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event GroupCopySelected As GroupCopySelectedDelegate

    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick

#Region "Property"

    ''' <summary>
    ''' Obj lstGroups
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    'Property lstGroups As List(Of GroupDTO)
    '    Get
    '        Return ViewState(Me.ID & "_lstGroups")
    '    End Get
    '    Set(ByVal value As List(Of GroupDTO))
    '        ViewState(Me.ID & "_lstGroups") = value
    '    End Set
    'End Property

    ''' <summary>
    ''' Obj GroupID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property GroupID As Decimal?
        Get
            Return ViewState(Me.ID & "_GroupID")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_GroupID") = value
        End Set
    End Property
    Property LabelName As String
        Get
            Return ViewState(Me.ID & "_LabelName")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_LabelName") = value
        End Set
    End Property
    ''' <summary>
    ''' Obj GroupID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property UserID As Decimal?
        Get
            Return ViewState(Me.ID & "_UserID")
        End Get
        Set(ByVal value As Decimal?)
            ViewState(Me.ID & "_UserID") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj Opened
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property Opened As Boolean
        Get
            Return ViewState(Me.ID & "_Opened")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_Opened") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj SelectGroup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedID As Decimal?
        Get
            If cboGroup.SelectedValue <> "" Then
                Return Decimal.Parse(cboGroup.SelectedValue)
                'Return (From p In lstGroups Where p.ID = Decimal.Parse(cboGroup.SelectedValue)).FirstOrDefault
            End If
            Return Nothing
        End Get
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 26/07/2017 10:00
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
            If LabelName <> "" Then
                lblName.Text = LabelName
            End If
            If Opened Then
                rwMessage.VisibleOnPageLoad = True

                Refresh()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo dữ liệu cho các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            RadAjaxPanel1.LoadingPanelID = CType(Me.Page, AjaxPage).AjaxLoading.ID
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 10:00
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
            'Load Combobox
            Dim data = (New CommonProgramsRepository).GetGroupOrUserList(If(UserID IsNot Nothing, 0, 1), If(UserID IsNot Nothing, UserID, GroupID))
            FillRadCombobox(cboGroup, data, "NAME", "ID", False)
            'Dim rep As New CommonRepository
            'lstGroups = rep.GetGroupListToComboListBox()
            'If GroupID IsNot Nothing AndAlso GroupID <> 0 Then
            '    Dim lstTemp As New List(Of GroupDTO)
            '    For Each itm In lstGroups
            '        If itm.ID <> GroupID Then
            '            lstTemp.Add(itm)
            '        End If
            '    Next
            '    lstGroups = lstTemp
            'End If
            'FillDropDownList(cboGroup, lstGroups, "NAME", "ID", Common.SystemLanguage, True)
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 26/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cua control btnYES
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnYES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYES.Click
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If cboGroup.CheckedItems.Count = 0 Then
                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ROW), Utilities.NotifyType.Warning)
                Exit Sub
            Else
                Dim rep As New CommonRepository
                For Each item In cboGroup.CheckedItems
                    If rep.CopyGroupFunction(GroupID, item.Value) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    End If
                Next
            End If



            RaiseEvent GroupCopySelected(sender, e)
            Hide()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xu ly su kien click cua control btnNO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNO.Click
        RaiseEvent CancelClicked(sender, e)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Hide()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 26/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện show của popup copy
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Show()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Opened = True
            rwMessage.VisibleOnPageLoad = True
            rwMessage.Visible = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

    ''' <lastupdate>
    ''' 26/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện hide của popup copy
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Hide()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Opened = False
            rwMessage.VisibleOnPageLoad = False
            rwMessage.Visible = False
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                    CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try
    End Sub

#End Region

End Class
