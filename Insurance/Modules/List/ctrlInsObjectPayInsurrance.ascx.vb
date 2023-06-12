Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Insurance.InsuranceBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlInsObjectPayInsurrance

    Inherits Common.CommonView
    Protected WithEvents TerminateView As ViewBase
    Public Overrides Property MustAuthorize As Boolean = True

    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Insurance/Module/Insurance/List/" + Me.GetType().Name.ToString()

#Region "Property"
    Property ListComboData As ComboBoxDataDTO
        Get
            Return ViewState(Me.ID & "_ListComboData")
        End Get
        Set(ByVal value As ComboBoxDataDTO)
            ViewState(Me.ID & "_ListComboData") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 05/09/2017 14:00
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
            If Not IsPostBack Then
                CurrentState = CommonMessage.STATE_NORMAL
                Me.MainToolBar = tbarOtherLists
                'Me.ctrlMessageBox.Listener = Me
                'Me.rgGridData.SetFilter()
                'rgGridData.Rebind()
                UpdateControlState()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 05/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham duoc ke thua tu commonView va goi phuong thuc InitControl
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.MainToolBar = tbarOtherLists
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel,
                                       ToolbarItem.Seperator)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            Me.MainToolBar.Items(0).Enabled = True
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 05/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Get data va bind lên form, tren grid
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            GetListContact()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 05/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Load cac control, menubar
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 05/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly them the <br /> vao chuoi de hien thi len view
    ''' </summary>
    ''' <param name="listBox"></param>
    ''' <remarks></remarks>
    Protected Sub ShowCheckedItems(ByVal listBox As RadListBox)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim sb As New StringBuilder()
            Dim collection As IList(Of RadListBoxItem) = listBox.CheckedItems
            For Each item As RadListBoxItem In collection
                sb.Append(item.Value + "<br />")
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                       CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 05/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly su kien SelectedIndexChanged cho control radLstInsurrance
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub radLstInsurrance_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles radLstInsurrance.SelectedIndexChanged
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Dim listCheck As New List(Of ContractTypeDTO)
            Select Case radLstInsurrance.SelectedValue
                Case "BHXH"
                    listCheck = (From s In ListComboData.LIST_CONTRACTTYPE
                                 Where s.BHXH = 1).ToList
                Case "BHYT"
                    listCheck = (From s In ListComboData.LIST_CONTRACTTYPE
                                 Where s.BHYT = 1).ToList
                Case "BHTN"
                    listCheck = (From s In ListComboData.LIST_CONTRACTTYPE
                                 Where s.BHTN = 1).ToList
                Case "BHTNLD_BNN"
                    listCheck = (From s In ListComboData.LIST_CONTRACTTYPE
                                 Where s.BHTNLD_BNN = 1).ToList
            End Select
            radLstContact.ClearChecked()
            For Each chk As RadListBoxItem In radLstContact.Items
                Dim q = (From l In listCheck
                         Where l.ID = chk.Value).Count
                If q > 0 Then
                    chk.Checked = True
                End If
            Next
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 05/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly cac event click menu tren menu toolbar: luu, huy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New InsuranceBusiness.InsuranceBusinessClient
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_CANCEL
                    GetListContact()
                    radLstInsurrance_SelectedIndexChanged(Nothing, Nothing)
                Case CommonMessage.TOOLBARITEM_SAVE
                    If radLstInsurrance.SelectedValue Is Nothing Or radLstInsurrance.SelectedValue = "" Then
                        ShowMessage("Bạn chưa chọn đối tượng bảo hiểm", NotifyType.Warning)
                        radLstInsurrance.Focus()
                        Exit Sub
                    End If
                    Dim list As New List(Of String)
                    For Each item As RadListBoxItem In radLstContact.CheckedItems
                        list.Add(Utilities.ObjToString(item.Value))
                    Next
                    If rep.ObjectPayInsurrance(list, radLstInsurrance.SelectedValue) Then
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)
                    Else
                        ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                    End If
                    GetListContact()
                    radLstInsurrance_SelectedIndexChanged(Nothing, Nothing)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Customs"

    ''' <lastupdate>
    ''' 05/09/2017 14:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ham xu ly load du lieu cho cac radLstContact
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetListContact()
        Dim rep As New InsuranceRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            ListComboData = Nothing
            ListComboData = New ComboBoxDataDTO
            ListComboData.GET_CONTRACTTYPE = True
            rep.GetComboboxData(ListComboData)
            radLstContact.DataTextField = "NAME"
            radLstContact.DataValueField = "ID"
            radLstContact.DataSource = ListComboData.LIST_CONTRACTTYPE
            radLstContact.DataBind()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                           CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class