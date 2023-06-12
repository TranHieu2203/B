Imports System.Configuration
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Aspose.Words
Imports Telerik.Web.UI

Public Class Utilities
    'Public Shared logger As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
    Public Enum NotifyType
        Alert = 0
        Success = 1
        [Error] = 2
        Warning = 3
        Information = 4
        Multi_Login = 5
    End Enum
    Public Enum ConfirmResult
        Yes = 1
        No = 0
    End Enum

    Public Shared Sub ExcuteScript(ByVal control As System.Web.UI.Control, ByVal Key As String, ByVal Script As String)
        Try
            ScriptManager.RegisterStartupScript(control.Page, control.Page.GetType, Key, Script, True)
        Catch ex As Exception

        End Try

    End Sub

    Public Shared Sub ShowMessage(ByVal control As System.Web.UI.Control, ByVal Text As String,
                                  Optional ByVal type As NotifyType = NotifyType.Alert, Optional ByVal autohidesecond As Integer = 5)
        Dim id As String
        Dim g As String = Guid.NewGuid().ToString.Replace("-", "")
        id = "notify" & g
        Text = Text.Replace("'", """")
        'If Text.ToString.Contains(".") Then
        '    Text = Text.Replace(".", ".<br/>")
        'End If
        'If Text.ToString.Contains("!") Then
        '    Text = Text.Replace("!", "!<br/>")
        'End If
        Text = Text.Replace(vbNewLine, "<br/>")
        Dim textForMessage As String = "var " & id & "=noty({text: '" + Text + "', dismissQueue: true"
        Select Case type
            Case NotifyType.Alert
                textForMessage &= ", type: 'alert'"
            Case NotifyType.Success
                textForMessage &= ", type: 'success'"
            Case NotifyType.Error
                textForMessage &= ", type: 'error'"
            Case NotifyType.Warning
                textForMessage &= ", type: 'warning'"
            Case NotifyType.Information
                textForMessage &= ", type: 'information'"
            Case NotifyType.Multi_Login
                textForMessage &= ",layout: 'center', type: 'information',buttons:[{addClass:'cancel',text:'Bỏ qua',onClick:function(){$.noty.close(" & id & ".options.id);}},{addClass:'accept',text:'Xác nhận',onClick:function(){$('#hidAccept').val('True');$('#LoginUser_LoginButton').click();}}]"

        End Select
        textForMessage &= "});"
        If autohidesecond > 0 Then
            textForMessage &= "setTimeout(function() {$.noty.close(" & id & ".options.id);}, " & autohidesecond * 1000 & ");"
        End If
        ScriptManager.RegisterStartupScript(control.Page, control.Page.GetType, "UserPopup", textForMessage, True)
    End Sub

    Public Shared Sub DisplayException(ByVal control As Control,
                                       ByVal ex As System.Exception,
                                       Optional ByVal ExtraInfo As String = "", Optional ByVal autohidesecond As Integer = 5)
        Try
            Dim st As New StackTrace(ex, True)
            Dim frame = st.GetFrame(0)
            Dim strMessage As String = "Message: " & ex.Message & "<br/>"
            If ex.InnerException IsNot Nothing Then
                strMessage = strMessage & "Inner Message: " & ex.InnerException.Message & "<br/>"
            End If
            If ex.StackTrace IsNot Nothing Then
                strMessage = strMessage & "Stack Trace: " & ex.StackTrace & "<br/>"
            End If
            strMessage = strMessage & "Class: " & ex.TargetSite.DeclaringType.Name & "<br/>"
            strMessage = strMessage & "Method: " & ex.TargetSite.Name & "<br/>"
            strMessage = strMessage & "Line: " & frame.GetFileLineNumber & "<br/>"
            strMessage = strMessage & "On: " & DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") & "<br/>"
            If ExtraInfo.Trim <> "" Then
                strMessage = strMessage & "Extra: " & ExtraInfo
            End If
            Utilities.ShowMessage(control, strMessage, Utilities.NotifyType.Error, 0)
        Catch eg As Exception

        End Try

    End Sub



    Public Shared Function ShowConfirm(ByVal usercontrol As System.Web.UI.UserControl, ByVal Text As String) As ConfirmResult
        Dim textForMessage As String = "var confirm_value = document.createElement('INPUT');confirm_value.type = 'hidden';confirm_value.name = 'confirm_value';"
        textForMessage &= "if (confirm('" & Text & "')) {confirm_value.value = 'Yes';} else {confirm_value.value = 'No';}document.forms[0].appendChild(confirm_value);"
        RadScriptManager.RegisterStartupScript(usercontrol, usercontrol.GetType, "UserPopup", textForMessage, True)
        Dim confirmValue As String = usercontrol.Request.Form("confirm_value")
        If confirmValue = "Yes" Then
            Return ConfirmResult.Yes
        Else
            Return ConfirmResult.No
        End If
    End Function

    Public Shared Function ShowModelWindow(ByVal control As System.Web.UI.Control,
                                           ByVal _radWindowid As String,
                                           ByVal _mid As String,
                                           ByVal _fid As String,
                                           Optional ByVal _group As String = "",
                                           Optional ByVal _parameter As String = "",
                                           Optional ByVal OnWindowCloseEventFunction As String = "",
                                           Optional ByVal _maximize As Boolean = False) As Boolean
        Try
            Dim script As String
            script = "var oWnd = $find('" & _radWindowid & "');"
            If OnWindowCloseEventFunction <> "" Then
                script &= "oWnd.add_close(" & OnWindowCloseEventFunction & ");"
            End If
            Dim url As String = "Dialog.aspx?mid=" & _mid & "&fid=" & _fid
            If _group <> "" Then
                url &= "&group=" & _group
            End If
            If _parameter <> "" Then
                url &= "&" & _parameter
            End If
            script &= "oWnd.setUrl('" & url & "');"
            script &= "oWnd.show();"
            If _maximize Then
                script &= "oWnd.maximize();"
            End If
            ScriptManager.RegisterStartupScript(control.Page, control.Page.GetType, "UserPopup", script, True)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function CloseModelWindow(ByVal control As System.Web.UI.Control,
                                           ByVal _radWindowid As String) As Boolean
        Try
            Dim script As String
            script = "$find('" & _radWindowid & "').close();"
            ScriptManager.RegisterStartupScript(control.Page, control.Page.GetType, "UserPopup", script, True)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetUsername() As String
        Try
            Return System.Web.HttpContext.Current.User.Identity.Name
        Catch ex As Exception

        End Try
        Return ""
    End Function

    Public Shared Function IsAuthenticated() As Boolean
        Try
            Return System.Web.HttpContext.Current.User.Identity.IsAuthenticated

        Catch ex As Exception

        End Try
        Return False
    End Function

    Public Shared Function SetProperty(ByVal Name As String, ByVal Value As Object, ByRef Entity As Object) As Boolean

        Try
            Dim infos As PropertyInfo() = Entity.GetType.GetProperties()
            Dim item = (From p In infos Where p.CanWrite And p.Name = Name Select p).SingleOrDefault
            If item IsNot Nothing Then
                item.SetValue(Entity, Value, Nothing)
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return False

    End Function

    Public Shared Function GetProperty(ByVal Entity As Object, ByVal Name As String) As Object
        Try
            Dim infos As PropertyInfo() = Entity.GetType.GetProperties()
            Dim item = (From p In infos Where p.CanRead And p.Name.ToUpper = Name.ToUpper Select p).SingleOrDefault
            If item IsNot Nothing Then
                Return item.GetValue(Entity, Nothing)
            End If
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function

    Public Shared Function Copy(Of T1, T2 As New)(ByVal Entity As T1) As T2
        Try
            If Entity Is Nothing Then
                Return Nothing
            End If

            Dim Target As New T2
            Dim infos As PropertyInfo() = Entity.GetType.GetProperties()
            For Each p In infos
                Dim value = p.GetValue(Entity, Nothing)
                Dim p1 As PropertyInfo = Target.GetType.GetProperty(p.Name)
                If p1 IsNot Nothing AndAlso p1.CanWrite Then
                    p1.SetValue(Target, value, Nothing)
                End If
            Next
            Return Target
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function


    Public Shared Function Copy(Of T1, T2 As New)(ByVal Entities As IEnumerable(Of T1)) As IEnumerable(Of T2)
        Try
            If Entities Is Nothing Then
                Return Nothing
            End If
            Dim Rets = New List(Of T2)
            For Each item In Entities
                Dim target = Copy(Of T1, T2)(item)
                Rets.Add(target)
            Next
            Return Rets
        Catch ex As Exception
            Throw ex
        End Try
        Return Nothing
    End Function


    Public Shared Sub FillDropDownList(ByVal ddl As RadComboBox,
                                       ByVal lstData As IEnumerable,
                                       ByVal sFieldText As String,
                                       ByVal sFieldValue As String,
                                        Optional ByVal lang As CultureInfo = Nothing,
                                       Optional ByVal bFieldBlank As Boolean = True,
                                       Optional ByVal idSelect As String = "")
        Try
            ddl.ClearSelection()
            ddl.Items.Clear()
            If lang IsNot Nothing Then
                Select Case lang.Name
                    Case "vi-VN"
                    Case Else
                        If sFieldText = "NAME_VN" Then
                            sFieldText = "NAME_EN"
                        End If
                End Select
            End If

            ddl.DataTextField = sFieldText
            ddl.DataValueField = sFieldValue
            If bFieldBlank Then

                ddl.Items.Add(New RadComboBoxItem("", ""))
            End If
            If lstData IsNot Nothing Then
                For Each item In lstData
                    Dim value = Utilities.GetProperty(item, sFieldValue).ToString
                    Dim text = Utilities.GetProperty(item, sFieldText).ToString
                    ddl.Items.Add(New RadComboBoxItem(text, value))
                Next
            End If
            Try
                ddl.SelectedValue = idSelect
            Catch ex As Exception

            End Try
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    ''' <summary>
    ''' Fill dữ liệu vào combobox
    ''' </summary>
    ''' <param name="cbo"></param>
    ''' <param name="lstObj"></param>
    ''' <param name="sFieldValue"></param>
    ''' <param name="sFieldText"></param>
    ''' <remarks></remarks>
    Public Shared Sub FillRadCombobox(Of T)(ByVal cbo As RadComboBox,
                                   ByVal lstObj As List(Of T),
                                   ByVal sFieldText As String,
                                   ByVal sFieldValue As String,
                                   Optional ByVal isFirstSelect As Boolean = False)
        Try

            cbo.DataValueField = sFieldValue
            cbo.DataTextField = sFieldText
            cbo.DataSource = lstObj
            If lstObj.Count > 0 And isFirstSelect Then
                cbo.SelectedIndex = 0
            End If
            cbo.DataBind()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Fill dữ liệu vào combobox
    ''' </summary>
    ''' <param name="cbo"></param>
    ''' <param name="dtData"></param>
    ''' <param name="sFieldValue"></param>
    ''' <param name="sFieldText"></param>
    ''' <remarks></remarks>
    Public Shared Sub FillRadCombobox(ByVal cbo As RadComboBox,
                                   ByVal dtData As DataTable,
                                   ByVal sFieldText As String,
                                   ByVal sFieldValue As String,
                                   Optional ByVal isFirstSelect As Boolean = False)
        Try

            cbo.DataValueField = sFieldValue
            cbo.DataTextField = sFieldText
            cbo.DataSource = dtData
            If dtData.Rows.Count > 0 And isFirstSelect Then
                cbo.SelectedIndex = 0
            End If
            cbo.DataBind()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Public Shared Sub FillCheckBoxList(Of T)(ByVal ddl As RadListBox,
                                   ByVal lstObj As List(Of T),
                                   ByVal sFieldText As String,
                                   ByVal sFieldValue As String,
                                       Optional ByVal lang As CultureInfo = Nothing)
        Try
            If lang IsNot Nothing Then
                Select Case lang.Name
                    Case "vi-VN"
                    Case Else
                        If sFieldText = "NAME_VN" Then
                            sFieldText = "NAME_EN"
                        End If
                End Select
            End If

            ddl.Items.Clear()
            ddl.DataTextField = sFieldText
            ddl.DataValueField = sFieldValue
            ddl.DataSource = lstObj
            ddl.DataBind()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub FillCheckBoxList(ByVal ddl As RadListBox,
                                   ByVal dtData As DataTable,
                                   ByVal sFieldText As String,
                                   ByVal sFieldValue As String,
                                       Optional ByVal lang As CultureInfo = Nothing)
        Try
            If lang IsNot Nothing Then
                Select Case lang.Name
                    Case "vi-VN"
                    Case Else
                        If sFieldText = "NAME_VN" Then
                            sFieldText = "NAME_EN"
                        End If
                End Select
            End If

            ddl.Items.Clear()
            ddl.DataTextField = sFieldText
            ddl.DataValueField = sFieldValue
            ddl.DataSource = dtData
            ddl.DataBind()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Shared Sub SelectedItemDataGridByKey(ByVal rgData As RadGrid,
                                                 ByRef gID As Decimal,
                                                 Optional ByVal isBlank As Boolean = True,
                                                 Optional ByVal isLoadAll As Boolean = False)
        Try
            If rgData.MasterTableView.Items.Count > 0 Then

                If gID = 0 Then
                    Exit Sub
                End If
                If isLoadAll Then
                    Dim gdiItem As GridDataItem = rgData.MasterTableView.FindItemByKeyValue("ID", gID)
                    If gdiItem IsNot Nothing Then
                        gdiItem.Selected = True
                        If isBlank Then
                            gID = 0
                        End If
                    End If
                Else
                    For idx = 0 To rgData.PageCount - 1
                        rgData.CurrentPageIndex = idx
                        rgData.Rebind()
                        Dim gdiItem As GridDataItem = rgData.MasterTableView.FindItemByKeyValue("ID", gID)
                        If gdiItem IsNot Nothing Then
                            gdiItem.Selected = True
                            If isBlank Then
                                gID = 0
                            End If
                            Exit For
                        End If
                    Next
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub SelectedItemDataGridByKey(ByVal rgData As RadGrid,
                                             ByRef gID As Guid,
                                             Optional ByVal isBlank As Boolean = True,
                                             Optional ByVal isLoadAll As Boolean = False)
        Try
            If rgData.MasterTableView.Items.Count > 0 Then

                If gID = Guid.Empty Then
                    Exit Sub
                End If
                If isLoadAll Then
                    Dim gdiItem As GridDataItem = rgData.MasterTableView.FindItemByKeyValue("ID", gID)
                    If gdiItem IsNot Nothing Then
                        gdiItem.Selected = True
                        If isBlank Then
                            gID = Guid.Empty
                        End If
                    End If
                Else
                    For idx = 0 To rgData.PageCount - 1
                        rgData.CurrentPageIndex = idx
                        rgData.Rebind()
                        Dim gdiItem As GridDataItem = rgData.MasterTableView.FindItemByKeyValue("ID", gID)
                        If gdiItem IsNot Nothing Then
                            gdiItem.Selected = True
                            If isBlank Then
                                gID = Guid.Empty
                            End If
                            Exit For
                        End If
                    Next
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ' Vẽ cây sơ đồ từ string truyền vào
    Public Shared Function DrawTreeByString(ByVal strDes As String, Optional ByVal level As Integer = 0) As String
        Dim strText As String = ""
        Dim iLevel As Integer
        Dim iLv As Integer
        Try
            If strDes = "&nbsp;" OrElse strDes Is Nothing Then 'ThanhNT added 27052016 - Nếu nothing thì return
                Return ""
            End If

            iLevel = strDes.Split(";").Count

            If level <= 0 Then
                iLv = 0
            Else
                iLv = strDes.Split(";").Count - level
                If iLv < 0 Then
                    iLv = 0
                End If
            End If

            For idx = iLv To iLevel - 1
                Dim strSpace As String = ""
                For idx1 = iLv To idx - 1
                    strSpace = strSpace + "&nbsp;&nbsp;&nbsp;&nbsp;"
                Next
                If strSpace = "" Then
                    strText = strText + strDes.Split(";")(idx)
                Else
                    strText = strText + "<br/>" + strSpace + "|__" + strDes.Split(";")(idx)
                End If
            Next
            Return strText
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared ReadOnly Property DefaultPage As String
        Get
            Return ConfigurationManager.AppSettings("DefaultPage")
        End Get
    End Property

    Public Shared ReadOnly Property ModulePath As String
        Get
            Return ConfigurationManager.AppSettings("ModulePath")
        End Get
    End Property

    'ThanhNT them cac duong` dan~ cho cac folder trong he thong khi release product
    Public Shared ReadOnly Property PathArchiveFolder As String
        Get
            Dim path As Object = ConfigurationManager.AppSettings("PathArchiveFolder")
            Return If(path IsNot Nothing, path, "")
        End Get
    End Property

    Public Shared ReadOnly Property PathOutBoxEmailFolder As String
        Get
            Dim path As Object = ConfigurationManager.AppSettings("PathOutBoxEmailFolder")
            Return If(path IsNot Nothing, path, "")
        End Get
    End Property

    Public Shared ReadOnly Property PathImportFolder As String
        Get
            Dim path As Object = ConfigurationManager.AppSettings("PathImportFolder")
            Return If(path IsNot Nothing, path, "")
        End Get
    End Property
    Public Shared ReadOnly Property PathCetificateFolder As String
        Get
            Dim path As Object = ConfigurationManager.AppSettings("PathCetificateFolder")
            Return If(path IsNot Nothing, path, "")
        End Get
    End Property

    Public Shared ReadOnly Property PathImportTempFolder As String
        Get
            Dim path As Object = ConfigurationManager.AppSettings("PathImportTempFolder")
            Return If(path IsNot Nothing, path, "")
        End Get
    End Property

    Public Shared ReadOnly Property PathControlFolder As String
        Get
            Dim path As Object = ConfigurationManager.AppSettings("PathControlFolder")
            Return If(path IsNot Nothing, path, "")
        End Get
    End Property

    Public Shared ReadOnly Property PathControlAttachFolder As String
        Get
            Dim path As Object = ConfigurationManager.AppSettings("PathControlAttachFolder")
            Return If(path IsNot Nothing, path, "")
        End Get
    End Property

    Public Shared ReadOnly Property PathTemplateInFolder As String
        Get
            Dim path As Object = ConfigurationManager.AppSettings("PathTemplateInFolder")
            Return If(path IsNot Nothing, path, "")
        End Get
    End Property

    Public Shared ReadOnly Property PathTemplateOutFolder As String
        Get
            Dim path As Object = ConfigurationManager.AppSettings("PathTemplateOutFolder")
            Return If(path IsNot Nothing, path, "")
        End Get
    End Property


    'Cau' hinh` co hien thi cay chuc' nang khi click vao menu report, Import, Process hay khong
    Public Shared ReadOnly Property IsVisibleMenu As Boolean
        Get
            Return False
        End Get
    End Property
    '--------------------------Ket thuc ThanhNT them-------------------------------

    Public Shared Sub Redirect(ByVal mid As String, ByVal fid As String,
                             Optional ByVal group As String = "",
                             Optional ByVal params As Dictionary(Of String, Object) = Nothing)
        Try
            Dim sUrl As String = DefaultPage & "?mid=" & mid & "&fid=" & fid
            If group.Trim <> "" Then
                sUrl = sUrl & "&group=" & group
            End If
            If params IsNot Nothing Then
                For Each item In params
                    sUrl = sUrl & "&" & item.Key & "=" & HttpContext.Current.Server.UrlEncode(item.Value.ToString)
                Next
            End If
            HttpContext.Current.Response.Redirect(sUrl)
        Catch ex As Exception
            Return
        End Try
    End Sub

    Public Shared Sub OnClientRowSelectedChanged(ByVal grid As RadGrid, ByVal lstBind As Dictionary(Of String, Control), Optional ByVal exScript As String = "")
        Dim script As New StringBuilder
        Dim strDebug As String = String.Empty
        Dim strFormat As String
        Dim FormatDate As String
        Dim func As String = grid.ID & "OnClientRowSelected"
        If lstBind.Count > 0 AndAlso Not String.IsNullOrEmpty(Trim(func)) Then

#If DEBUG Then
            strDebug = vbCrLf
#End If
            strFormat = ConfigurationManager.AppSettings("FDATECLIENT")
            script.Append("function " & func & "(sender, eventArgs){try{")
            script.Append("for (i = 0; i < Page_Validators.length; i++) { ")
            script.Append("ValidatorEnable(Page_Validators[i], false); ")
            script.Append("} ")
            script.Append("var count = sender.get_masterTableView().get_selectedItems().length; ")
            For Each item As KeyValuePair(Of String, Control) In lstBind
                Select Case TypeName(item.Value)
                    Case "Label"
                        script.Append("var ctr" & item.Key & "=$get(""" & item.Value.ClientID & """);" & strDebug)
                        script.Append("ctr" & item.Key & ".innerHTML  = (eventArgs.getDataKeyValue(""" & item.Key & """));" & strDebug)
                        script.Append("if(count > 1){ ")
                        script.Append("ctr" & item.Key & ".innerHTML= """";" & strDebug)
                        script.Append(" } ")

                    Case "HiddenField"
                        script.Append("var ctr" & item.Key & "=$get(""" & item.Value.ClientID & """);" & strDebug)
                        script.Append("ctr" & item.Key & ".value  = (eventArgs.getDataKeyValue(""" & item.Key & """));" & strDebug)
                        script.Append("if(count > 1){ ")
                        script.Append("ctr" & item.Key & ".value = """";" & strDebug)
                        script.Append(" } ")

                    Case "CheckBox"
                        script.Append("var ctr" & item.Key & "=$get(""" & item.Value.ClientID & """);" & strDebug)
                        script.Append("if(eventArgs.getDataKeyValue(""" & item.Key & """)==""True"" || eventArgs.getDataKeyValue(""" & item.Key & """)==""1"" || eventArgs.getDataKeyValue(""" & item.Key & """)==""-1"")" & strDebug)
                        script.Append("{ctr" & item.Key & ".checked  = true;}" & strDebug)
                        script.Append("else {ctr" & item.Key & ".checked =false;}" & strDebug)
                        script.Append("if(count > 1){ ")
                        script.Append("ctr" & item.Key & ".checked =false;" & strDebug)
                        script.Append(" } ")

                    Case "RadNumericTextBox"
                        script.Append("var ctr" & item.Key & "=$find(""" & item.Value.ClientID & """);" & strDebug)
                        script.Append("ctr" & item.Key & ".set_value(eventArgs.getDataKeyValue(""" & item.Key & """));" & strDebug)
                        script.Append("if(count > 1){ ")
                        script.Append("ctr" & item.Key & ".set_value("""");" & strDebug)
                        script.Append(" } ")

                    Case "RadButton"
                        If CType(item.Value, RadButton).ButtonType = RadButtonType.ToggleButton AndAlso
                           CType(item.Value, RadButton).ToggleType = ButtonToggleType.CheckBox Then
                            script.Append("var ctr" & item.Key & "=$find(""" & item.Value.ClientID & """);" & strDebug)
                            script.Append("if(eventArgs.getDataKeyValue(""" & item.Key & """)==""True"" || eventArgs.getDataKeyValue(""" & item.Key & """)==""1"")" & strDebug)
                            'script.Append("if(eventArgs.getDataKeyValue(""" & item.Key & """)==""True"")" & strDebug)
                            script.Append("{ctr" & item.Key & ".set_checked(true);}" & strDebug)
                            script.Append("else {ctr" & item.Key & ".set_checked(false);}" & strDebug)
                            script.Append("if(count > 1){ ")
                            script.Append("ctr" & item.Key & ".set_checked(false);" & strDebug)
                            script.Append(" } ")
                        End If

                    Case "RadComboBox"
                        If item.Key.Split(";").Length = 1 Then
                            script.Append("var ctr" & item.Key & "=$find(""" & item.Value.ClientID & """);" & strDebug)
                            script.Append("ctr" & item.Key & ".enable();" & strDebug)
                            script.Append("var items = ctr" & item.Key & ".get_items();" & strDebug)
                            script.Append("var itemsCount = items.get_count(); " & strDebug)
                            script.Append("for (var i = 0; i < itemsCount; i++) " & strDebug)
                            script.Append("{ " & strDebug)
                            script.Append("var item1 = items.getItem(i).get_text(); " & strDebug)
                            script.Append("} " & strDebug)
                            script.Append("var item" & item.Key & "=ctr" & item.Key & ".findItemByValue(eventArgs.getDataKeyValue(""" & item.Key & """));" & strDebug)
                            script.Append("if(item" & item.Key & "!=null)" & strDebug)
                            script.Append("{item" & item.Key & ".select();}" & strDebug)

                            script.Append("if(item" & item.Key & "==null)" & strDebug)
                            script.Append("{item" & item.Key & "=ctr" & item.Key & ".findItemByText("""");" & strDebug)
                            script.Append("if(item" & item.Key & "!=null)" & strDebug)
                            script.Append("{item" & item.Key & ".select();} else{$find(""" & item.Value.ClientID & """).clearSelection();}}" & strDebug)
                            script.Append("ctr" & item.Key & ".disable();" & strDebug)
                            script.Append("if(count > 1){ ")
                            script.Append("$find(""" & item.Value.ClientID & """).clearSelection();" & strDebug)
                            script.Append(" } ")
                        ElseIf item.Key.Split(";").Length = 2 Then
                            Dim itmName As String = item.Key.Split(";")(1)
                            Dim itmKey As String = item.Key.Split(";")(0)
                            script.Append("var ctr" & itmKey & "=$find(""" & item.Value.ClientID & """);" & strDebug)
                            script.Append("ctr" & itmKey & ".clearItems();" & strDebug)
                            script.Append("if(eventArgs.getDataKeyValue(""" & itmName & """)!=null && eventArgs.getDataKeyValue(""" & itmKey & """)!=null){" & strDebug)
                            script.Append("ctr" & itmKey & ".enable();" & strDebug)
                            script.Append("ctr" & itmKey & ".trackChanges();" & strDebug)
                            script.Append("var cboItem" & itmKey & "  = new Telerik.Web.UI.RadComboBoxItem();" & strDebug)
                            script.Append("cboItem" & itmKey & ".set_text(eventArgs.getDataKeyValue(""" & itmName & """));" & strDebug)
                            script.Append("cboItem" & itmKey & ".set_value(eventArgs.getDataKeyValue(""" & itmKey & """));" & strDebug)
                            script.Append("ctr" & itmKey & ".get_items().add(cboItem" & itmKey & ");" & strDebug)
                            script.Append("cboItem" & itmKey & ".select();" & strDebug)
                            script.Append("ctr" & itmKey & ".commitChanges();" & strDebug)
                            script.Append("ctr" & itmKey & ".disable();" & strDebug)
                            script.Append("}" & strDebug)
                            script.Append("else{ctr" & itmKey & ".clearSelection();ctr" & itmKey & ".set_text('')}" & strDebug)
                            script.Append("if(count > 1){ ")
                            script.Append("ctr" & itmKey & ".clearSelection();ctr" & itmKey & ".set_text('');" & strDebug)
                            script.Append(" } ")
                        End If

                    Case "RadTextBox"
                        script.Append("var ctr" & item.Key & "=$find(""" & item.Value.ClientID & """);" & strDebug)
                        script.Append("ctr" & item.Key & ".set_value(eventArgs.getDataKeyValue(""" & item.Key & """));" & strDebug)
                        script.Append("if(count > 1){ ")
                        script.Append("ctr" & item.Key & ".set_value("""");" & strDebug)
                        script.Append(" } ")

                    Case "RadDatePicker"
                        FormatDate = "dd/MM/yyyy"
                        script.Append("var date = eventArgs.getDataKeyValue(""" & item.Key & """);" & strDebug)
                        script.Append("if(date!=null){" & strDebug)
                        'script.Append("var ctr_date = new Date(date.split(' ')[0]);" & strDebug) 'new Date(date)
                        'script.Append("var dd = ctr_date.getDate();" & strDebug)
                        'script.Append("var mm = ctr_date.getMonth() + 1;" & strDebug)
                        'script.Append("var yyyy = ctr_date.getFullYear();" & strDebug)
                        'script.Append("if (dd < 10) {dd = '0' + dd}" & strDebug)
                        'script.Append("if (mm < 10) {mm = '0' + mm}" & strDebug)
                        script.Append("var arrPartDate = (date.split(' ')[0]).split('/');" & strDebug)
                        script.Append("var dd = arrPartDate[0];" & strDebug)
                        script.Append("var mm = arrPartDate[1];" & strDebug)
                        script.Append("var yyyy = arrPartDate[2];" & strDebug)
                        script.Append("result = dd + '/' + mm + '/' + yyyy;" & strDebug)
                        script.Append("$find(""" & item.Value.ClientID & """).set_selectedDate(ConvertToDate(result,""" & FormatDate & """));" & strDebug)
                        script.Append("}else{" & strDebug)
                        'script.Append("$find(""" & item.Value.ClientID & """).set_selectedDate('empty');}" & strDebug)
                        script.Append("$find(""" & item.Value.ClientID & """).clear();}" & strDebug)
                        script.Append("if(count > 1){ ")
                        script.Append("$find(""" & item.Value.ClientID & """).clear();" & strDebug)
                        script.Append(" } ")

                    Case "RadTimePicker", "RadDateTimePicker"
                        If String.Equals(String.Empty, strFormat) Then
                            strFormat = "dd/MM/yyyy HH:mm:ss"
                        End If
                        'script.Append("var date = eventArgs.getDataKeyValue(""" & item.Key & """);" & strDebug)
                        'script.Append("if(date!=null){" & strDebug)
                        'script.Append("var ctr_date = new Date(date.split(' ')[0]);" & strDebug) 'new Date(date)
                        'script.Append("var dd = ctr_date.getDate();" & strDebug)
                        'script.Append("var mm = ctr_date.getMonth() + 1;" & strDebug)
                        'script.Append("var yyyy = ctr_date.getFullYear();" & strDebug)
                        'script.Append("if (dd < 10) {dd = '0' + dd}" & strDebug)
                        'script.Append("if (mm < 10) {mm = '0' + mm}" & strDebug)
                        'script.Append("result = dd + '/' + mm + '/' + yyyy;" & strDebug)
                        'script.Append("$find(""" & item.Value.ClientID & """).set_selectedDate(ConvertToDate(result,""" & strFormat & """));" & strDebug) '.get_dateInput().set_emptyMessage(result);" & strDebug)
                        'script.Append("}else{" & strDebug)
                        'script.Append("$find(""" & item.Value.ClientID & """).set_selectedDate('empty');}" & strDebug)
                        script.Append("var ctr" & item.Key & "=$find(""" & item.Value.ClientID & """);" & strDebug)
                        script.Append("if(eventArgs.getDataKeyValue(""" & item.Key & """)!=null){" & strDebug)
                        script.Append("ctr" & item.Key & ".set_selectedDate(ConvertToDate(eventArgs.getDataKeyValue(""" & item.Key & """),""" & strFormat & """));}" & strDebug)
                        script.Append("else{ctr" & item.Key & ".set_selectedDate(null)}" & strDebug)
                        script.Append("if(count > 1){ ")
                        script.Append("ctr" & item.Key & ".clear();" & strDebug)
                        script.Append(" } ")

                    Case "RadMonthYearPicker"
                        If String.Equals(String.Empty, strFormat) Then
                            strFormat = "MM/yyyy HH:mm:ss"
                        End If
                        script.Append("var ctr" & item.Key & "=$find(""" & item.Value.ClientID & """);" & strDebug)
                        script.Append("if(eventArgs.getDataKeyValue(""" & item.Key & """)!=null){" & strDebug)
                        script.Append("ctr" & item.Key & ".set_selectedDate(ConvertToDate(eventArgs.getDataKeyValue(""" & item.Key & """),""" & strFormat & """));}" & strDebug)
                        script.Append("else{ctr" & item.Key & ".set_selectedDate(null)}" & strDebug)
                        script.Append("if(count > 1){ ")
                        script.Append("ctr" & item.Key & ".clear();" & strDebug)
                        script.Append(" } ")

                    Case "RadListBox"
                        script.Append("var selected" & item.Key & "=eventArgs.getDataKeyValue(""" & item.Key & """);" & strDebug)
                        script.Append("var lstitem" & item.Key & "=$find(""" & item.Value.ClientID & """).get_items();" & strDebug)
                        script.Append("for (var i = 0; i < lstitem" & item.Key & ".get_count(); i++){lstitem" & item.Key & ".getItem(i).uncheck();}" & strDebug)
                        script.Append("if(selected" & item.Key & "){" & strDebug)
                        script.Append("for (i = 0; i < selected" & item.Key & ".split("","").length; i++) {" & strDebug)
                        script.Append("var item = $find(""" & item.Value.ClientID & """).findItemByValue(selected" & item.Key & ".split("","")[i]);if (item) {item.check();}" & strDebug)
                        script.Append("}}" & strDebug)
                        script.Append("if(count > 1){ ")
                        script.Append("for (var i = 0; i < lstitem" & item.Key & ".get_count(); i++){lstitem" & item.Key & ".getItem(i).uncheck();}" & strDebug)
                        script.Append(" } ")
                End Select
            Next
            script.Append(exScript)
            script.Append("}catch(err){}}")
            RadScriptManager.RegisterClientScriptBlock(grid.Page, grid.Page.GetType(), func, script.ToString(), True)

            script = New StringBuilder
            func = grid.ID & "RadGridSelecting"
            script.Append("function " & func & "(sender, args) {" & strDebug)
            script.Append("var _en = sender.ClientSettings.AllowKeyboardNavigation;" & strDebug)
            script.Append("args.set_cancel(!_en);}" & strDebug)
            RadScriptManager.RegisterClientScriptBlock(grid.Page, grid.Page.GetType(), func, script.ToString(), True)

            script = New StringBuilder
            func = grid.ID & "RadGridDeSelecting"
            script.Append("function " & func & "(sender, args) {" & strDebug)
            script.Append("var _en = sender.ClientSettings.AllowKeyboardNavigation;" & strDebug)
            script.Append("args.set_cancel(!_en);}" & strDebug)
            RadScriptManager.RegisterClientScriptBlock(grid.Page, grid.Page.GetType(), func, script.ToString(), True)
        End If
    End Sub

    Public Shared Sub EnabledGridNotPostback(ByVal _radGrid As RadGrid, ByVal _enabled As Boolean)
        Dim func As String = _radGrid.ID & "OnClientRowSelected"
        Dim funcSelecting As String = _radGrid.ID & "RadGridSelecting"
        Dim funcDeSelecting As String = _radGrid.ID & "RadGridDeSelecting"

        _radGrid.Enabled = _enabled
        _radGrid.MasterTableView.Enabled = _enabled
        _radGrid.ClientSettings.EnableRowHoverStyle = _enabled
        _radGrid.ClientSettings.ClientEvents.OnRowSelected = func
        _radGrid.ClientSettings.ClientEvents.OnRowSelecting = funcSelecting
        _radGrid.ClientSettings.ClientEvents.OnRowDeselecting = funcDeSelecting
        _radGrid.ClientSettings.AllowKeyboardNavigation = _enabled
    End Sub

    Public Shared Sub EnabledGrid(ByVal _radGrid As RadGrid, ByVal _enabled As Boolean, Optional ByVal _postback As Boolean = True)
        _radGrid.Enabled = _enabled
        _radGrid.ClientSettings.EnableRowHoverStyle = _enabled
        If _postback Then
            _radGrid.ClientSettings.EnablePostBackOnRowClick = _enabled
        End If
        _radGrid.ClientSettings.Resizing.AllowColumnResize = _enabled
        '_radGrid.ClientSettings.Selecting.AllowRowSelect = _enabled
        _radGrid.ClientSettings.AllowKeyboardNavigation = _enabled
    End Sub

    Public Shared Sub EnableRadCombo(ByVal _radCbx As RadComboBox, ByVal _enable As Boolean)
        _radCbx.ShowDropDownOnTextboxClick = _enable
        _radCbx.ShowToggleImage = _enable
        _radCbx.EnableTextSelection = _enable
        _radCbx.ChangeTextOnKeyBoardNavigation = _enable
        _radCbx.Enabled = _enable
    End Sub

    Public Shared Sub EnableRadDatePicker(ByVal _radDatePicker As RadDatePicker, ByVal _enable As Boolean)
        _radDatePicker.Calendar.Enabled = _enable
        _radDatePicker.DateInput.ReadOnly = Not _enable
        _radDatePicker.Enabled = _enable
    End Sub

    Public Shared Sub EnableRadDateTimePicker(ByVal _radDatePicker As RadDateTimePicker, ByVal _enable As Boolean)
        _radDatePicker.Calendar.Enabled = _enable
        _radDatePicker.DateInput.ReadOnly = Not _enable
        _radDatePicker.Enabled = _enable
        _radDatePicker.TimePopupButton.Enabled = _enable
    End Sub

    Public Shared Sub EnableRadTimePicker(ByVal _radTimePicker As RadTimePicker, ByVal _enable As Boolean)
        _radTimePicker.TimePopupButton.Enabled = _enable
        _radTimePicker.DateInput.ReadOnly = Not _enable
        _radTimePicker.Enabled = _enable
    End Sub

    Public Shared Sub ReadOnlyRadComBo(ByVal _radCombo As RadComboBox, ByVal _readonly As Boolean)
        _radCombo.ShowDropDownOnTextboxClick = Not _readonly
        _radCombo.ShowToggleImage = Not _readonly
        _radCombo.Enabled = Not _readonly
    End Sub

    Public Shared Sub ClearRadGridFilter(ByVal _radGrid As RadGrid)
        _radGrid.MasterTableView.FilterExpression = ""
        For Each col As GridColumn In _radGrid.MasterTableView.Columns
            col.CurrentFilterValue = ""
        Next
    End Sub

#Region "Export Word - MailMerge"
    Public Shared Sub ExportWordMailMerge(ByVal link As String, ByVal filename As String, ByVal dtData As DataTable,
                                          ByVal response As System.Web.HttpResponse, ByVal sourcePath As String, Optional ByVal format As Integer = 0)
        Try
            'THanhNT added variable "format" (0 = doc, 1 = pdf)
            ' Open an existing document. 
            Dim doc As New Document(link)
            ' Fill the fields in the document with user data.
            doc.MailMerge.Execute(dtData)
            ' Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.

            'doc.Save(filename, SaveFormat.Doc, SaveType.OpenInApplication, response)
            Dim tempDoc As Document = WordCommon.InsertHeaderLogo(doc, dtData, sourcePath)
            tempDoc.Save(response, filename, Aspose.Words.ContentDisposition.Attachment, If(format = 0, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Doc), Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Pdf)))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Shared Sub ExportWordMailMerge(ByVal link As String, ByVal filename As String, ByVal dtData As DataTable,
                                          ByVal response As System.Web.HttpResponse, Optional ByVal format As Integer = 0)
        Try
            'THanhNT added variable "format" (0 = doc, 1 = pdf)
            ' Open an existing document. 
            Dim doc As New Document(link)
            ' Fill the fields in the document with user data.
            doc.MailMerge.Execute(dtData)
            ' Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.

            'doc.Save(filename, SaveFormat.Doc, SaveType.OpenInApplication, response)

            doc.Save(response, filename, Aspose.Words.ContentDisposition.Attachment, If(format = 0, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Doc), Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Pdf)))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub ExportWordMailMergeDS(ByVal link As String, ByVal filename As String, ByVal dsData As DataSet, ByVal response As System.Web.HttpResponse)
        Try
            ' Open an existing document. 
            Dim doc As New Document(link)
            ' Fill the fields in the document with user data.
            doc.MailMerge.ExecuteWithRegions(dsData)
            ' Send the document in Word format to the client browser with an option to save to disk or open inside the current browser.

            'doc.Save(filename, SaveFormat.Doc, SaveType.OpenInApplication, response)

            doc.Save(response, filename, Aspose.Words.ContentDisposition.Attachment, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Doc))
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function GetDataTableByList(Of T)(ByVal varlist As IEnumerable(Of T)) As DataTable
        Dim dtReturn As New DataTable()

        ' column names 
        Dim oProps() As PropertyInfo = Nothing

        If varlist Is Nothing Then
            Return dtReturn
        End If

        For Each rec As T In varlist

            If oProps Is Nothing Then
                oProps = (CType(rec.GetType(), Type)).GetProperties()
                For Each pi As PropertyInfo In oProps
                    Dim colType As Type = pi.PropertyType

                    If (colType.IsGenericType) AndAlso (colType.GetGenericTypeDefinition() Is GetType(Nullable(Of ))) Then
                        colType = colType.GetGenericArguments()(0)
                    End If

                    dtReturn.Columns.Add(New DataColumn(pi.Name, colType))
                Next pi
            End If

            Dim dr As DataRow = dtReturn.NewRow()

            For Each pi As PropertyInfo In oProps
                dr(pi.Name) = If(pi.GetValue(rec, Nothing) Is Nothing, DBNull.Value, pi.GetValue(rec, Nothing))
            Next pi

            dtReturn.Rows.Add(dr)
        Next rec
        Return dtReturn
    End Function

#End Region

    ''' <summary>
    ''' Lấy giá trị filter trên radgrid gán vào object
    ''' </summary>
    ''' <param name="radgrid"></param>
    ''' <param name="var"></param>
    ''' <remarks></remarks>
    Public Shared Sub SetValueObjectByRadGrid(ByVal radgrid As RadGrid, ByRef var As Object)
        Dim oProps() As PropertyInfo = Nothing
        Try
            oProps = var.GetType().GetProperties()
            For Each pi As PropertyInfo In oProps
                Dim item = radgrid.MasterTableView.Columns.FindByUniqueNameSafe(pi.Name)
                If item IsNot Nothing AndAlso item.Visible = True AndAlso item.CurrentFilterValue <> "" AndAlso pi.CanWrite Then
                    Dim value As Object = Nothing
                    Dim colType As Type = pi.PropertyType
                    Select Case pi.PropertyType
                        Case GetType(DateTime), GetType(DateTime?)
                            If item.ColumnType = "GridBoundColumn" AndAlso CType(item, GridBoundColumn).DataFormatString = "{0:MM/yyyy}" Then
                                DateTime.TryParseExact("01/" & item.CurrentFilterValue, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, value)
                                If value IsNot Nothing AndAlso value <> DateTime.MinValue Then
                                    item.CurrentFilterValue = Format(value, "MM/yyyy")
                                End If
                            ElseIf item.ColumnType = "GridBoundColumn" AndAlso CType(item, GridBoundColumn).DataFormatString = "{0:dd/MM/yyyy}" Then
                                DateTime.TryParseExact(item.CurrentFilterValue, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, value)
                                If value IsNot Nothing AndAlso value <> DateTime.MinValue Then
                                    item.CurrentFilterValue = Format(value, "dd/MM/yyyy")
                                End If
                            Else
                                DateTime.TryParseExact(item.CurrentFilterValue.Replace("SA", "AM"), ConfigurationManager.AppSettings("FDATECLIENT"), CultureInfo.InvariantCulture, DateTimeStyles.None, value)
                                If value IsNot Nothing AndAlso value <> DateTime.MinValue Then
                                    item.CurrentFilterValue = Format(value, "dd/MM/yyyy")
                                End If
                            End If
                        Case GetType(Double), GetType(Double?)
                            Double.TryParse(item.CurrentFilterValue, value)
                        Case GetType(Decimal), GetType(Decimal?)
                            Decimal.TryParse(item.CurrentFilterValue, value)
                        Case GetType(Integer), GetType(Integer?)
                            Integer.TryParse(item.CurrentFilterValue, value)
                        Case GetType(Boolean), GetType(Boolean?)

                            Boolean.TryParse(item.CurrentFilterValue, value)
                        Case Else
                            value = item.CurrentFilterValue
                    End Select
                    pi.SetValue(var, value, Nothing)
                End If

            Next pi
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Đặt thuộc tính để tạo control filter trên radgrid
    ''' </summary>
    ''' <param name="rad"></param>
    ''' <remarks></remarks>
    Public Shared Sub SetGridFilter(ByVal rad As RadGrid)
        Dim kt As Boolean = rad.AllowFilteringByColumn
        Try
            For Each item As GridColumn In rad.MasterTableView.Columns
                If item.UniqueName = "CheckBoxTemplateColumn" Then
                    item.ShowFilterIcon = False
                    item.AutoPostBackOnFilter = False
                    item.FilterControlToolTip = String.Empty
                    Continue For
                End If

                item.ShowFilterIcon = False
                item.AutoPostBackOnFilter = True
                Select Case item.ColumnType
                    Case "GridBoundColumn", "GridHyperLinkColumn"
                        If item.DataType = GetType(DateTime) Or item.DataType = GetType(DateTime?) Or CType(item, GridBoundColumn).DataFormatString = "{0:dd/MM/yyyy}" Or CType(item, GridBoundColumn).DataFormatString = "{0:MM/yyyy}" Then
                            item.CurrentFilterFunction = GridKnownFunction.EqualTo
                        Else
                            item.CurrentFilterFunction = GridKnownFunction.Contains
                        End If
                        item.ItemStyle.HorizontalAlign = HorizontalAlign.Left
                        item.FilterControlWidth = New Unit("99%")
                    Case "GridDateTimeColumn"
                        item.CurrentFilterFunction = GridKnownFunction.EqualTo
                        item.FilterControlWidth = New Unit("99%")
                        item.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                        If CType(item, GridDateTimeColumn).DataFormatString = "" Then
                            CType(item, GridDateTimeColumn).DataFormatString = "{0:dd/MM/yyyy}"
                        End If
                    Case ("GridCheckBoxColumn")
                        item.CurrentFilterFunction = GridKnownFunction.EqualTo
                        item.FilterControlWidth = New Unit("99%")
                        item.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                        item.ItemStyle.HorizontalAlign = HorizontalAlign.Center
                    Case "GridNumericColumn"
                        item.CurrentFilterFunction = GridKnownFunction.EqualTo
                        item.FilterControlWidth = New Unit("99%")
                        item.ItemStyle.HorizontalAlign = HorizontalAlign.Right
                        If CType(item, GridNumericColumn).DataFormatString = "" Then
                            CType(item, GridNumericColumn).DataFormatString = "{0:#,##0.##}"
                        End If
                    Case "GridClientSelectColumn"
                        item.ShowFilterIcon = False
                        item.AutoPostBackOnFilter = False
                        item.FilterControlToolTip = String.Empty
                        item.FilterControlWidth = New Unit("0%")
                        Continue For
                End Select
                item.HeaderStyle.HorizontalAlign = HorizontalAlign.Center
                item.FilterListOptions = GridFilterListOptions.VaryByDataType
                item.FilterControlToolTip = item.HeaderText
            Next
            If Not kt Then
                rad.AllowFilteringByColumn = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub GridExportExcel(ByVal grid As RadGrid, Optional ByVal FileNameDefault As String = "ExportFile")
        Try
            grid.ExportSettings.ExportOnlyData = True
            grid.ExportSettings.OpenInNewWindow = True
            grid.ExportSettings.FileName = FileNameDefault
            grid.ExportSettings.IgnorePaging = False
            For Each filter As GridFilteringItem In grid.MasterTableView.GetItems(GridItemType.FilteringItem)
                filter.Visible = False
            Next
            For i As Integer = 0 To grid.MasterTableView.Columns.Count - 1
                If TypeOf grid.MasterTableView.Columns(i) Is GridClientSelectColumn Then
                    grid.MasterTableView.Columns(i).Visible = False

                End If
            Next
            grid.MasterTableView.UseAllDataFields = True
            grid.MasterTableView.ExportToExcel()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Function CheckDecimalNothing(ByVal _value As Decimal?, Optional ByVal _defaultValue As Decimal = 0) As Decimal
        If _value Is Nothing Then
            Return _defaultValue
        Else
            Return Convert.ToDecimal(_value)
        End If
    End Function

    Public Shared Function CheckObjectNothing(ByVal _value As Object, Optional ByVal _defaultValue As Decimal = 0) As Object
        If _value Is Nothing Then
            Return _defaultValue
        Else
            Return _value
        End If
    End Function

#Region "ConvertDataType"
    Public Shared Function ObjToString(ByVal _value As Object, Optional ByVal _defualvalue As String = "") As String
        Try
            If _value Is Nothing Then
                Return _defualvalue
            Else
                Return Convert.ToString(_value)
            End If
        Catch ex As Exception
            Return _defualvalue
        End Try
    End Function

    Public Shared Function ObjToInt(ByVal _value As Object, Optional ByVal _defualvalue As Integer = 0) As Integer
        Try
            If _value Is Nothing Or _value Is String.Empty Then
                Return _defualvalue
            Else
                Return Convert.ToInt32(_value)
            End If
        Catch ex As Exception
            Return _defualvalue
        End Try
    End Function

    Public Shared Function ObjToDecima(ByVal _value As Object, Optional ByVal _defualvalue As Decimal = 0) As Decimal
        Try
            If _value Is Nothing Then
                Return _defualvalue
            Else
                Return Convert.ToDecimal(_value)
            End If
        Catch ex As Exception
            Return _defualvalue
        End Try
    End Function
#End Region

#Region "Control"
    Public Shared Function ValidateJSHTML(ByVal ParamArray ctrls() As Object) As Boolean

        Try
            Dim _fontJIAdieuxRegEx As String = "<(" & Chr(34) & "[^\" & Chr(34) & "]*\" & Chr(34) & "|'[^']*'|[^'\" & Chr(34) & ">])*>"
            Dim r = New Regex(_fontJIAdieuxRegEx)
            For Each ctrl In ctrls
                If TypeOf (ctrl) Is RadTextBox Then
                    Dim c As RadTextBox = ctrl
                    If r.IsMatch(c.Text) Then
                        Return True
                    End If
                End If

            Next
        Catch ex As Exception

        End Try
        Return False
    End Function

    Public Shared Function ValidateSQL(ByVal ParamArray ctrls() As Object) As Boolean
        Try
            Dim _fontJIAdieuxRegEx As String = "(SELECT\s[\w\*\)\(\,\s]+\sFROM\s[\w]+)| (UPDATE\s[\w]+\sSET\s[\w\,\'\=]+)| (INSERT\sINTO\s[\d\w]+[\s\w\d\)\(\,]*\sVALUES\s\([\d\w\'\,\)]+)| (DELETE\sFROM\s[\d\w\'\=]+)"
            Dim r = New Regex(_fontJIAdieuxRegEx)
            For Each ctrl In ctrls
                If TypeOf (ctrl) Is RadTextBox Then
                    Dim c As RadTextBox = ctrl
                    If r.IsMatch(c.Text) Then
                        Return True
                    End If
                End If

            Next
        Catch ex As Exception

        End Try
        Return False
    End Function
    Public Shared Sub ClearControlValue(ByVal ParamArray ctrls() As Object)
        Try
            For Each ctrl In ctrls
                If TypeOf (ctrl) Is RadComboBox Then
                    Dim c As RadComboBox = ctrl
                    c.ClearValue()
                ElseIf TypeOf (ctrl) Is RadTextBox Then
                    Dim c As RadTextBox = ctrl
                    c.ClearValue()
                ElseIf TypeOf (ctrl) Is RadNumericTextBox Then
                    Dim c As RadNumericTextBox = ctrl
                    c.ClearValue()
                ElseIf TypeOf (ctrl) Is RadDatePicker Then
                    Dim c As RadDatePicker = ctrl
                    c.ClearValue()
                    c.Clear()
                    c.DateInput.Clear()
                    c.DateInput.EmptyMessage = ""
                ElseIf TypeOf (ctrl) Is RadDateTimePicker Then
                    Dim c As RadDateTimePicker = ctrl
                    c.ClearValue()
                    c.Clear()
                    c.DateInput.Clear()
                    c.DateInput.EmptyMessage = ""
                ElseIf TypeOf (ctrl) Is RadMonthYearPicker Then
                    Dim c As RadMonthYearPicker = ctrl
                    c.Clear()
                    c.DateInput.Clear()
                    c.DateInput.EmptyMessage = ""
                ElseIf TypeOf (ctrl) Is RadTimePicker Then
                    Dim c As RadTimePicker = ctrl
                    c.ClearValue()
                    c.Clear()
                    c.DateInput.Clear()
                    c.DateInput.EmptyMessage = ""
                ElseIf TypeOf (ctrl) Is HiddenField Then
                    Dim c As HiddenField = ctrl
                    c.ClearValue()
                ElseIf TypeOf (ctrl) Is TextBox Then
                    Dim c As TextBox = ctrl
                    c.ClearValue()
                ElseIf TypeOf (ctrl) Is CheckBox Then
                    Dim c As CheckBox = ctrl
                    c.ClearValue()
                ElseIf TypeOf (ctrl) Is Label Then
                    Dim c As Label = ctrl
                    c.Text = ""
                ElseIf TypeOf (ctrl) Is RadListBox Then
                    Dim c As RadListBox = ctrl
                    c.ClearChecked()
                End If
                If TypeOf (ctrl) Is RadListBox Then
                    Dim c As RadListBox = ctrl
                    c.ClearChecked()
                    c.ClearSelection()
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub EnableControlAll(ByVal isEnable As Boolean, ByVal ParamArray ctrls() As Object)
        Try
            For Each ctrl In ctrls
                If TypeOf (ctrl) Is RadComboBox Then
                    Dim c As RadComboBox = ctrl
                    EnableRadCombo(c, isEnable)
                End If
                If TypeOf (ctrl) Is RadTextBox Then
                    Dim c As RadTextBox = ctrl
                    c.ReadOnly = Not isEnable
                End If
                If TypeOf (ctrl) Is RadNumericTextBox Then
                    Dim c As RadNumericTextBox = ctrl
                    c.ReadOnly = Not isEnable
                End If
                If TypeOf (ctrl) Is RadDatePicker Then
                    Dim c As RadDatePicker = ctrl
                    EnableRadDatePicker(c, isEnable)
                End If
                If TypeOf (ctrl) Is RadDateTimePicker Then
                    Dim c As RadDateTimePicker = ctrl
                    EnableRadDateTimePicker(c, isEnable)
                End If
                If TypeOf (ctrl) Is RadTimePicker Then
                    Dim c As RadTimePicker = ctrl
                    EnableRadTimePicker(c, isEnable)
                End If
                If TypeOf (ctrl) Is TextBox Then
                    Dim c As TextBox = ctrl
                    c.ReadOnly = Not isEnable
                End If
                If TypeOf (ctrl) Is CheckBox Then
                    Dim c As CheckBox = ctrl
                    c.Enabled = isEnable
                End If
                If TypeOf (ctrl) Is RadButton Then
                    Dim c As RadButton = ctrl
                    c.Enabled = isEnable
                End If
                If TypeOf (ctrl) Is Button Then
                    Dim c As Button = ctrl
                    c.Enabled = isEnable
                End If
                If TypeOf (ctrl) Is RadListBox Then
                    Dim c As RadListBox = ctrl
                    c.Enabled = isEnable
                End If
                If TypeOf (ctrl) Is RadMonthYearPicker Then
                    Dim c As RadMonthYearPicker = ctrl
                    c.Enabled = isEnable
                End If
                If TypeOf (ctrl) Is RequiredFieldValidator Then
                    Dim c As RequiredFieldValidator = ctrl
                    c.Enabled = isEnable
                End If

            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub EnableControlAll_Cus(ByVal isEnable As Boolean, ByVal ctrPane As RadPane)
        Try
            For Each ctrl As Control In ctrPane.Controls

                If TypeOf (ctrl) Is RadComboBox Then
                    Dim c As RadComboBox = ctrl
                    EnableRadCombo(c, isEnable)
                End If
                If TypeOf (ctrl) Is RadTextBox Then
                    Dim c As RadTextBox = ctrl
                    c.ReadOnly = Not isEnable
                End If
                If TypeOf (ctrl) Is RadNumericTextBox Then
                    Dim c As RadNumericTextBox = ctrl
                    c.ReadOnly = Not isEnable
                End If
                If TypeOf (ctrl) Is RadDatePicker Then
                    Dim c As RadDatePicker = ctrl
                    EnableRadDatePicker(c, isEnable)
                End If
                If TypeOf (ctrl) Is RadDateTimePicker Then
                    Dim c As RadDateTimePicker = ctrl
                    EnableRadDateTimePicker(c, isEnable)
                End If
                If TypeOf (ctrl) Is RadTimePicker Then
                    Dim c As RadTimePicker = ctrl
                    EnableRadTimePicker(c, isEnable)
                End If
                If TypeOf (ctrl) Is TextBox Then
                    Dim c As TextBox = ctrl
                    c.ReadOnly = Not isEnable
                End If
                If TypeOf (ctrl) Is CheckBox Then
                    Dim c As CheckBox = ctrl
                    c.Enabled = isEnable
                End If
                If TypeOf (ctrl) Is RadButton Then
                    Dim c As RadButton = ctrl
                    c.Enabled = isEnable
                End If
                If TypeOf (ctrl) Is Button Then
                    Dim c As Button = ctrl
                    c.Enabled = isEnable
                End If
                If TypeOf (ctrl) Is RadListBox Then
                    Dim c As RadListBox = ctrl
                    c.Enabled = isEnable
                End If
                If TypeOf (ctrl) Is RadMonthYearPicker Then
                    Dim c As RadMonthYearPicker = ctrl
                    c.Enabled = isEnable
                End If
                If TypeOf (ctrl) Is RadGrid Then
                    Dim c As RadGrid = ctrl
                    c.Enabled = isEnable
                End If
            Next
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub SetValueComboBox(ByRef radComboBox As RadComboBox, ByVal value As Object, ByVal text As String)
        If value IsNot Nothing Then
            radComboBox.SelectedValue = value
            If Not String.IsNullOrWhiteSpace(text) Then
                radComboBox.Text = text
            End If
        Else
            ClearControlValue(radComboBox)
        End If
    End Sub
    Public Shared Function GetValueFromComboBox(ByRef radComboBox As RadComboBox) As Decimal?
        If radComboBox.SelectedItem IsNot Nothing Then
            Return ConvertTo(radComboBox.SelectedValue)
        End If
        If radComboBox.SelectedValue IsNot Nothing Then
            Return ConvertTo(radComboBox.SelectedValue)
        End If
        Return Nothing
    End Function
    Public Shared Function ConvertTo(ByVal value As Object) As Decimal?
        If value Is Nothing OrElse String.IsNullOrWhiteSpace(value.ToString) Then
            Return Nothing
        End If
        Dim result As Decimal
        If Decimal.TryParse(value.ToString, result) Then
            Return result
        End If
        Return Nothing
    End Function
    Public Shared Function Status() As List(Of RadComboBoxItemData)
        Dim result = New List(Of RadComboBoxItemData)
        result.Add(New RadComboBoxItemData() With {.Value = 446, .Text = HistaffWebAppResources.My.Resources.UI.Status_New})
        result.Add(New RadComboBoxItemData() With {.Value = 447, .Text = HistaffWebAppResources.My.Resources.UI.Status_Approved})
        result.Add(New RadComboBoxItemData() With {.Value = 445, .Text = HistaffWebAppResources.My.Resources.UI.Status_Reject})
        Return result
    End Function
    Public Shared Function AssetStatus() As List(Of RadComboBoxItemData)
        Dim assetStatusList = New List(Of RadComboBoxItemData)
        assetStatusList.Add(New RadComboBoxItemData() With {.Value = 483, .Text = HistaffWebAppResources.My.Resources.UI.AssetStatus_Granted})
        assetStatusList.Add(New RadComboBoxItemData() With {.Value = 484, .Text = HistaffWebAppResources.My.Resources.UI.AssetStatus_HandOvered})
        assetStatusList.Add(New RadComboBoxItemData() With {.Value = 485, .Text = HistaffWebAppResources.My.Resources.UI.AssetStatus_WaitingGrant})
        Return assetStatusList
    End Function
#End Region

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="fileId"></param>
    ''' <param name="folderName"></param>
    ''' <param name="fileName"></param>
    ''' <param name="iError">
    ''' 1. Không tồn tại file
    ''' </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTemplateLinkFile(ByVal fileId As Decimal,
                                               ByVal folderName As String,
                                               ByRef fileName As String,
                                               ByRef extension As String,
                                               ByRef iError As Integer) As Boolean
        Try
            Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "TemplateDynamic\" & folderName
            If (Not System.IO.Directory.Exists(filePath)) Then
                iError = 1
                Return False
            End If
            Dim dirs As String() = Directory.GetFiles(filePath, fileId.ToString & ".*")
            If dirs.Length > 0 Then
                Dim fInfo = New FileInfo(dirs(0))
                fileName = fInfo.FullName
                extension = fInfo.Extension
                Return True
            Else
                iError = 1
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="fileId"></param>
    ''' <param name="folderName"></param>
    ''' <param name="fileName"></param>
    ''' <param name="iError">
    ''' 1. Không tồn tại file
    ''' </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetTemplateLinkFile(ByVal fileId As String,
                                               ByVal folderName As String,
                                               ByRef fileName As String,
                                               ByRef extension As String,
                                               ByRef iError As Integer) As Boolean
        Try
            Dim filePath = AppDomain.CurrentDomain.BaseDirectory & "TemplateDynamic\" & folderName
            If (Not System.IO.Directory.Exists(filePath)) Then
                iError = 1
                Return False
            End If
            Dim dirs As String() = Directory.GetFiles(filePath, fileId.ToString & ".*")
            If dirs.Length > 0 Then
                Dim fInfo = New FileInfo(dirs(0))
                fileName = fInfo.FullName
                extension = fInfo.Extension
                Return True
            Else
                iError = 1
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Shared Function SaveMultyFile(ByRef dtData As DataTable, ByVal filePath As String, ByVal fileName As String) As List(Of String)

        Try
            Dim lstReturn As New List(Of String)
            Dim dtDataMerge As DataTable
            Dim path As String = AppDomain.CurrentDomain.BaseDirectory & "TemplateDynamic\Attachment"
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If
            fileName = RemoveUnicode(fileName)
            For Each row As DataRow In dtData.Rows
                Dim doc As Document = New Document(filePath)
                Dim pathFile = path & "\" & fileName & "_" & row("EMPLOYEE_CODE").ToString & "_" & Format(Date.Now, "yyyyMMddHHmmss") & ".doc"
                dtDataMerge = dtData.Clone
                dtDataMerge.ImportRow(row)
                doc.MailMerge.Execute(dtDataMerge)
                doc.Save(pathFile, Aspose.Words.Saving.SaveOptions.CreateSaveOptions(SaveFormat.Doc))
                lstReturn.Add(pathFile)
            Next
            Return lstReturn
        Catch ex As Exception
            Return Nothing
            Throw ex
        End Try
    End Function

    Public Shared Function RemoveUnicode(ByVal s As String)
        Dim stFormD As String = s.Normalize(System.Text.NormalizationForm.FormD)
        Dim sb As New System.Text.StringBuilder
        For ich As Integer = 0 To stFormD.Length - 1
            Dim uc As UnicodeCategory = CharUnicodeInfo.GetUnicodeCategory(stFormD(ich))
            If uc = UnicodeCategory.NonSpacingMark = False Then
                sb.Append(stFormD(ich))
            End If
        Next
        Return sb.ToString().Normalize(System.Text.NormalizationForm.FormD)
    End Function

    Public Shared ReadOnly Property Account As String
        Get
            Dim acc As Object = ConfigurationManager.AppSettings("ACC")
            Return If(acc IsNot Nothing, acc.ToString(), String.Empty)
        End Get
    End Property

    Public Shared Function ParseBoolean(ByVal dValue As String) As Boolean
        If String.IsNullOrEmpty(dValue) Then
            Return False
        Else
            Return If(dValue = "1", True, False)
        End If
    End Function

    Public Shared ReadOnly Property APPLY_ORG_PERMISSION As Boolean
        Get
            Dim apply As Object = ConfigurationManager.AppSettings("APPLY_ORG_PERMISSION")
            Return If(apply IsNot Nothing, If(apply.ToString() = "1", True, False), False)
        End Get
    End Property

    Public Shared ReadOnly Property CONTROL_LOAD_AFTER_LOGGED_IN_PERMISSION As String 'THanhNT added 29/09/2016
        Get
            Dim apply As Object = ConfigurationManager.AppSettings("URLRedirectAfterLoggedIn")
            Return If(apply IsNot Nothing, apply, "")
        End Get
    End Property

    Public Shared ReadOnly Property DefaultView_AfterLoggedIn As String 'THanhNT added 29/09/2016
        Get
            Dim apply As Object = ConfigurationManager.AppSettings("DefaultView")
            Return If(apply IsNot Nothing, apply, "")
        End Get
    End Property

End Class
