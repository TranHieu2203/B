Imports Common.CommonBusiness
Imports Framework.UI
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlApproveProcessNewEdit
    Inherits CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/ApproveProcess/" + Me.GetType().Name.ToString()
#Region "Property"
    Property ApproveProcess As ApproveProcessDTO
        Get
            Return ViewState(Me.ID & "_ApproveProcess")
        End Get
        Set(ByVal value As ApproveProcessDTO)
            ViewState(Me.ID & "_ApproveProcess") = value
        End Set
    End Property

    Property ApproveProcessID As String = ""
    Dim lstApproveProcesss As New List(Of ApproveProcessDTO)
    Dim lstAllowanceByGroup As New List(Of ApproveProcessDTO)
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            LoadAllControlDefault()
            Refresh()

            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                 CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub

    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow

            Me.MainToolBar = tbarApproveProcess
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Seperator,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New CommonRepository
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Not IsPostBack Then
            Else
                Select Case Message
                    Case "UpdateView"
                        If ApproveProcess IsNot Nothing Then
                            txtName.Text = ApproveProcess.NAME
                            hidID.Value = ApproveProcess.ID.ToString
                            rntxtRequestDate.Value = ApproveProcess.NUMREQUEST
                            txtEmail.Text = ApproveProcess.EMAIL
                        End If
                    Case "InsertView"
                        txtName.Text = ""
                End Select

            End If
            txtName.Focus()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
        MyBase.Refresh(Message)
    End Sub
#End Region

#Region "Event"

    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objApproveProcess As New ApproveProcessDTO
        Dim rep As New CommonRepository
        Dim gID As Decimal
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objApproveProcess.ACTFLG = "A"
                                objApproveProcess.NAME = txtName.Text
                                objApproveProcess.NUMREQUEST = rntxtRequestDate.Value
                                objApproveProcess.EMAIL = txtEmail.Text

                                If rep.InsertApproveProcess(objApproveProcess) Then
                                    Me.Send(New MessageDTO With {.ID = gID, .Mess = CommonMessage.ACTION_INSERTED})
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                            Case CommonMessage.STATE_EDIT

                                objApproveProcess = rep.GetApproveProcess(Decimal.Parse(hidID.Value))

                                objApproveProcess.NAME = txtName.Text
                                objApproveProcess.NUMREQUEST = rntxtRequestDate.Value
                                objApproveProcess.EMAIL = txtEmail.Text

                                'objApproveProcess.ID = Decimal.Parse(hidID.Value)
                                If rep.UpdateApproveProcess(objApproveProcess) Then
                                    Me.Send(New MessageDTO With {.ID = gID, .Mess = CommonMessage.ACTION_UPDATED})
                                Else
                                    ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
                                End If
                        End Select
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    Me.Send(New MessageDTO With {.ID = gID, .Mess = CommonMessage.ACTION_CANCEL})
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub


#End Region

#Region "Custom"
    Protected Sub LoadAllControlDefault()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CurrentState
                Case CommonMessage.STATE_NEW
                    'lblViewApproveProcess.Text = Translate(CommonMessage.VIEW_TITLE_NEW)
                Case CommonMessage.STATE_EDIT
                    'lblViewApproveProcess.Text = Translate(CommonMessage.VIEW_TITLE_EDIT)
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

End Class