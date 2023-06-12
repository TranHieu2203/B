Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_OrgChart
    Inherits Common.CommonView

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Setting/" + Me.GetType().Name.ToString()

#Region "Property"
    Public Property Organizations As List(Of OrgChartRptDTO)
        Get
            Return ViewState(Me.ID & "_Organizations")
        End Get

        Set(ByVal value As List(Of OrgChartRptDTO))
            ViewState(Me.ID & "_Organizations") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'Refresh()
            UpdateControlState()
            'FillDataByTree()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                orgTreeList.ExpandToLevel(1)
            End If
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Event"
    Private Sub orgTreeList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.TreeListCommandEventArgs) Handles orgTreeList.ItemCommand
        Dim tab As New Hashtable()
        Select Case e.CommandName.ToUpper
            Case "EXPORTTOEXCEL"
                orgTreeList.ExportSettings.ExportMode = DirectCast(3, TreeListExportMode)
                orgTreeList.ExpandAllItems()
            Case "EXPORTTOPDF"
                orgTreeList.ExportSettings.ExportMode = DirectCast(3, TreeListExportMode)
                orgTreeList.ExpandAllItems()
            Case "EXPORTTOWORD"
                orgTreeList.ExportSettings.ExportMode = DirectCast(3, TreeListExportMode)
                orgTreeList.ExpandAllItems()
        End Select
    End Sub

    Private Sub orgTreeList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.TreeListItemDataBoundEventArgs) Handles orgTreeList.ItemDataBound
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If TypeOf e.Item Is TreeListDataItem Then
                Dim item As TreeListDataItem = CType(e.Item, TreeListDataItem)

                If (CType(item("IS_JOB").Text, Boolean)) Then
                    item("ORG_NAME").CssClass = "NodeJOB"
                    e.Item.CssClass = "NodeJOB1"
                    'e.Item.b
                    'e.Node.HoveredCssClass = "OpenningTree"
                    'e.Node.HoveredImageUrl = "/Static/Images/folder-open.gif"
                    'e.Node.SelectedImageUrl = "/Static/Images/folder-open.gif"
                    'e.Node.SelectedCssClass = "OpenmedTree"
                    'e.Node.CssClass = "ClassTree"
                    'e.Node.ImageUrl = "/Static/Images/folder.gif"
                Else
                    If (CType(item("NHOMDUAN").Text, Boolean)) Then
                        item("ORG_NAME").CssClass = "NodeNhomDuAn"
                        e.Item.CssClass = "NodeNhomDuAn1"
                        'e.Node.HoveredCssClass = "OpenningTreeNhomDuAn"
                        'e.Node.HoveredImageUrl = "/Static/Images/group.png"
                        'e.Node.SelectedImageUrl = "/Static/Images/group.png"
                        'e.Node.SelectedCssClass = "OpenmedTreeNhomDuAn"
                        'e.Node.CssClass = "ClassTreeNhomDuAn"
                        'e.Node.ImageUrl = "/Static/Images/group.png"
                    Else
                        item("ORG_NAME").CssClass = "NodeTree"
                        e.Item.CssClass = "NodeTree1"
                        'e.Node.HoveredCssClass = "OpenningTree"
                        'e.Node.HoveredImageUrl = "/Static/Images/folder-open.gif"
                        'e.Node.SelectedImageUrl = "/Static/Images/folder-open.gif"
                        'e.Node.SelectedCssClass = "OpenmedTree"
                        'e.Node.CssClass = "ClassTree"
                        'e.Node.ImageUrl = "/Static/Images/folder.gif"
                    End If
                End If
            End If
            'node = CType(e.DataItem, OrganizationDTO)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub orgTreeList_NeedDataSource(ByVal sender As Object, ByVal e As TreeListNeedDataSourceEventArgs) Handles orgTreeList.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New ProfileRepository
            Dim lst1 As New List(Of OrgChartRptDTO)
            lst1 = rep.OrgChartRpt(Common.Common.SystemLanguage.Name)
            Me.Organizations = lst1

            orgTreeList.DataSource = Me.Organizations
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub
#End Region

End Class