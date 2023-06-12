Imports Common
Imports Framework.UI
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog
Public Class ctrlHU_DisciplineSalaryNewEdit
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"

#End Region

#Region "Page"
    ''' <summary>
    ''' Khoi tao page, control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            GetParams()
            Refresh()
            UpdateControlState()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Khoi tao control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    ''' <summary>
    ''' Khoi tao, load menu toolbar
    ''' </summary>
    ''' <remarks></remarks>

    Protected Sub InitControl()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Me.MainToolBar = tbarDiscipline
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save, ToolbarItem.Seperator,
                                       ToolbarItem.Cancel)
            CType(MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
    ''' <summary>
    ''' Reset page khi process xong: update
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        'Dim objOther As OtherListDTO
        Dim rep As New ProfileBusinessRepository
        Try

            Select Case Message
                Case "UpdateView"
                    CurrentState = CommonMessage.STATE_EDIT
                    Dim obj = rep.GetDisciplineSalaryByID(New DisciplineSalaryDTO With {.ID = hidID.Value})
                    rntxtMoney.Value = 0
                    If obj.MONEY IsNot Nothing Then
                        rntxtMoney.Value = obj.MONEY
                    End If
                    rntxtMonth.Value = obj.MONTH
                    rntxtYear.Value = obj.YEAR

                    txtEmployeeCode.Text = obj.EMPLOYEE_CODE
                    txtEmployeeName.Text = obj.EMPLOYEE_NAME
                    txtOrgName.Text = obj.ORG_NAME
                    txtTitleName.Text = obj.TITLE_NAME

                    hidEmployeeID.Value = obj.EMPLOYEE_ID
                    hidDisciplineID.Value = obj.DISCIPLINE_ID

                    If obj.APPROVE_STATUS Then
                        RadPane2.Enabled = False
                        CurrentState = CommonMessage.STATE_NORMAL
                        CType(MainToolBar.Items(0), RadToolBarButton).Enabled = False
                    End If
            End Select
            rep.Dispose()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "Event"
    ''' <summary>
    ''' Event click item menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim obj As New DisciplineSalaryDTO
        Dim sError As String = ""
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then


                        Using rep As New ProfileBusinessRepository
                            obj.DISCIPLINE_ID = hidDisciplineID.Value
                            obj.EMPLOYEE_ID = hidEmployeeID.Value
                            obj.ID = hidID.Value
                            obj.MONEY = rntxtMoney.Value
                            obj.MONTH = rntxtMonth.Value
                            obj.YEAR = rntxtYear.Value

                            If Not rep.ValidateDisciplineSalary(obj, sError) Then
                                ShowMessage(sError, Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                            If rep.EditDisciplineSalary(obj) Then
                                'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                                ''POPUPTOLINK
                                Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_DisciplineSalary&group=Business")
                            Else
                                ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                            End If
                        End Using

                    End If
                Case CommonMessage.TOOLBARITEM_CANCEL
                    ''POPUPTOLINK_CANCEL
                    Response.Redirect("/Default.aspx?mid=Profile&fid=ctrlHU_DisciplineSalary&group=Business")
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

#End Region

#Region "Custom"
    ''' <summary>
    ''' Load Data theo ID Giam tru thu nhap
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetParams()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If CurrentState Is Nothing Then
                hidID.Value = Decimal.Parse(Request.Params("ID"))
                Refresh("UpdateView")

            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region


End Class