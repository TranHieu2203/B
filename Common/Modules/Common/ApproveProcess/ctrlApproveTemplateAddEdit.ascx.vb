Imports Common.CommonBusiness
Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlApproveTemplateAddEdit
    Inherits CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Common/Modules/Common/ApproveProcess/" + Me.GetType().Name.ToString()
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Property"
    ''' <lastupdate>
    ''' 24/07/2017 14:09
    ''' </lastupdate>
    ''' <summary>
    ''' IDSelectư
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IDSelect As String
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 24/07/2017 14:09
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad cho trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Me.IDSelect = Request.QueryString("id")
            If Not IsPostBack Then
                LoadData()
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                              CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 14:09
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo các thiết lập giá trị cho các control trên trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            'rgApproveProcess.AllowCustomPaging = True
            'rgApproveProcess.PageSize = Common.Common.DefaultPageSize
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")

        End Try

    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 14:09
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện button click khi click control tbarEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbarEdit_ButtonClick(sender As Object, e As Telerik.Web.UI.RadToolBarEventArgs) Handles tbarEdit.ButtonClick
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case "SAVE"
                    Dim db As New CommonRepository
                    If IDSelect = "" Then
                        Dim itemInsert As New CommonBusiness.ApproveTemplateDTO With {
                            .TEMPLATE_NAME = txtTemplateName.Text.ToString.Trim,
                            .TEMPLATE_ORDER = txtTemplateOrder.Value.Value.ToString.Trim,
                            .TEMPLATE_TYPE = Integer.Parse(cboTemplateType.SelectedValue)
                        }
                        If db.InsertApproveTemplate(itemInsert) Then
                            'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

                            radAjaxManager.ResponseScripts.Add("getRadWindow().close('1');")
                            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseWindow", "getRadWindow().close('1');", True)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    Else
                        Dim itemEdit As ApproveTemplateDTO = db.GetApproveTemplate(Decimal.Parse(IDSelect))
                        With itemEdit
                            .TEMPLATE_NAME = txtTemplateName.Text.ToString.Trim
                            .TEMPLATE_ORDER = txtTemplateOrder.Value.Value.ToString.Trim
                            .TEMPLATE_TYPE = Integer.Parse(cboTemplateType.SelectedValue)
                        End With
                        If db.UpdateApproveTemplate(itemEdit) Then
                            'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

                            radAjaxManager.ResponseScripts.Add("getRadWindow().close('1');")
                            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "CloseWindow", "getRadWindow().close('1');", True)
                        Else
                            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), NotifyType.Error)
                        End If
                    End If
            End Select

            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Customs"
    ''' <lastupdate>
    ''' 24/07/2017 14:09
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức khởi tạo các giá trị cho các control trên trang
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            Common.BuildToolbar(tbarEdit,
                                       ToolbarItem.Save,
                                       ToolbarItem.Cancel)

            cboTemplateType.AllowCustomText = False
            CType(tbarEdit.Items(0), RadToolBarButton).CausesValidation = True
            txtTemplateName.Focus()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                             CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 24/07/2017 14:09
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức load dữ liệu cho các control khi 1 thiết lập biểu mẫu được chọn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadData()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            txtTemplateName.Text = ""
            txtTemplateOrder.Value = 1
            cboTemplateType.SelectedIndex = 0
            If IDSelect <> "" Then
                Dim db As New CommonRepository
                Dim item As ApproveTemplateDTO = db.GetApproveTemplate(Decimal.Parse(IDSelect))
                If item IsNot Nothing Then
                    txtTemplateName.Text = item.TEMPLATE_NAME
                    txtTemplateOrder.Value = item.TEMPLATE_ORDER
                    cboTemplateType.SelectedValue = item.TEMPLATE_TYPE.ToString()
                End If
            End If
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                     CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

End Class