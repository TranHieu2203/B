Imports Framework.UI.Utilities
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlSkinAppConfig
    Inherits CommonView
    Dim _myLog As New MyLog()
    Dim _pathLog As String = _myLog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/List/" + Me.GetType().Name.ToString()

#Region "Property"

    Property SkinName As String
        Get
            Return PageViewState(Me.ID & "_SkinName")
        End Get
        Set(value As String)
            PageViewState(Me.ID & "_SkinName") = value
        End Set
    End Property

    Property ColorMenu As String
        Get
            Return PageViewState(Me.ID & "_ColorMenu")
        End Get
        Set(value As String)
            PageViewState(Me.ID & "_ColorMenu") = value
        End Set
    End Property
#End Region

#Region "Page"

    ''' <summary>
    ''' ViewLoad
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            countImg.Value = 0
            Dim configName As String = System.Configuration.ConfigurationManager.AppSettings("Telerik.Skin")
            rcbSkin.SelectedValue = configName
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            InitControl()
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            'Khoi tao ToolBar
            Me.MainToolBar = tbarMainToolBar
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save)
            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' BindData
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Try
            Dim lstSkin As List(Of String) = Telerik.Web.SkinRegistrar.GetEmbeddedSkinNames(GetType(RadGrid))
            Dim dt As New DataTable
            dt.Columns.Add("ID", GetType(String))

            For i As Integer = 0 To lstSkin.Count - 1
                Dim row As DataRow = dt.NewRow
                row("ID") = lstSkin(i).ToString
                dt.Rows.Add(row)
            Next

            FillRadCombobox(rcbSkin, dt, "ID", "ID")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

#Region "Event"

    ''' <summary>
    ''' Button Event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    ctrlMessageBox.MessageText = Translate("Bạn chắc chắn thay đổi Giao diện của hệ thống ?")
                    ctrlMessageBox.ActionName = CommonMessage.TOOLBARITEM_SAVE
                    ctrlMessageBox.DataBind()
                    ctrlMessageBox.Show()
            End Select
            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ctrlMessageBox_ButtonCommand(ByVal sender As Object, ByVal e As MessageBoxEventArgs) Handles ctrlMessageBox.ButtonCommand
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim ckChange As Decimal = 0
        Dim strFilepath = Server.MapPath("\App_Data\RadUploadTemp\")
        Dim strFileSave = Server.MapPath("\Static\Images\")


        Dim directory As New System.IO.DirectoryInfo(strFilepath)
        Dim File As System.IO.FileInfo() = directory.GetFiles()
        Dim FilePart As System.IO.FileInfo
        Try
            If e.ActionName = CommonMessage.TOOLBARITEM_SAVE And e.ButtonID = MessageBoxButtonType.ButtonYes Then

                'Set Color for Menu
                If ColorMenu IsNot Nothing AndAlso ColorMenu <> "" AndAlso ColorMenu <> "#ffffff" Then
                    ColorMenu = "#" + ColorMenu
                    Dim line As String = ".mlddm li a:hover, .mlddm li a#buttonhover, .mlddm li a.selected {background: " + ColorMenu + ";}" + vbCrLf
                    Dim line2 As String = ".mlddm ul li a:hover {background-color: " + ColorMenu + ";}" + vbCrLf
                    Dim FilePathColor As String = Server.MapPath("~\Styles\Config.jquery.css")
                    Dim objColor As New System.IO.StreamWriter(FilePathColor)
                    Dim lineGroup As String
                    lineGroup = line + line2
                    objColor.Write(lineGroup)
                    objColor.Close()
                    ckChange = 1
                End If

                'Set Skin
                If SkinName IsNot Nothing AndAlso SkinName <> System.Configuration.ConfigurationManager.AppSettings("Telerik.Skin") Then
                    Dim configFile = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~")
                    Dim settings = configFile.AppSettings.Settings
                    settings("Telerik.Skin").Value = SkinName
                    configFile.Save(ConfigurationSaveMode.Modified)
                    ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name)
                    ckChange = 1
                End If

                'Set Image Background
                If countImg.Value > 0 Then
                    Dim dt As DataTable = New DataTable
                    dt.Columns.AddRange({New DataColumn("Name"), New DataColumn("CreationTime", GetType(Date))})
                    For Each FilePart In File
                        Dim dr As DataRow = dt.NewRow
                        dr(0) = FilePart.Name
                        dr(1) = FilePart.CreationTime
                        dt.Rows.Add(dr)
                    Next
                    Dim rowCk = (From dr As DataRow In dt.AsEnumerable Order By dr("CreationTime") Descending).First
                    If rowCk IsNot Nothing Then
                        Dim FilePathImg As String = Server.MapPath("~\Styles\ImgConfig.jquery.css")
                        Dim objImg As New System.IO.StreamWriter(FilePathImg)
                        My.Computer.FileSystem.MoveFile(strFilepath + rowCk(0).ToString, strFileSave + rowCk(0).ToString)
                        Dim strBackground_Img As String = ".mlddm {background-image: url(""" + "/Static/Images/" + rowCk(0).ToString + """);}" + vbCrLf
                        Dim lineGroup As String
                        lineGroup = strBackground_Img
                        objImg.Write(lineGroup)
                        objImg.Close()
                    End If
                    ckChange = 1
                End If

                'Check to change
                If ckChange <> 0 Then
                    Page.Response.Redirect("Default.aspx?mid=Dashboard&fid=ctrlDashboard", False)
                    Exit Sub
                Else
                    ShowMessage(Translate("Không có thay đổi khi lưu. Vui lòng kiểm tra lại !"), NotifyType.Warning)
                    Exit Sub
                End If
            End If

            _myLog.WriteLog(_myLog._info, _classPath, method,
                                        CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rcbSkin_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbSkin.SelectedIndexChanged
        Try
            If rcbSkin.SelectedValue <> "" Then
                SkinName = rcbSkin.SelectedValue
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rcpColor_ColorChanged(sender As Object, e As System.EventArgs) Handles rcpColor.ColorChanged
        Try
            If rcpColor.SelectedColor.IsEmpty = False Then
                ColorMenu = rcpColor.SelectedColor.Name.ToString.Substring(2).ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Custom"
#End Region


End Class