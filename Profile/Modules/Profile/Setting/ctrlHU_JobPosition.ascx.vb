Imports Common
Imports Framework.UI
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_JobPosition
    Inherits Common.CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Setting/" + Me.GetType().Name.ToString()

#Region "Property"
    Public Property EmployeeID As Decimal
    ''' <summary>
    ''' Obj Organizations
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property JobPositinTree As List(Of JobPositinTreeDTO)
        Get
            Return ViewState(Me.ID & "_JobPositinTree")
        End Get
        Set(ByVal value As List(Of JobPositinTreeDTO))
            ViewState(Me.ID & "_JobPositinTree") = value
        End Set
    End Property

    Public Property JobChildTree As List(Of JobChildTreeDTO)
        Get
            Return ViewState(Me.ID & "_JobChildTree")
        End Get
        Set(ByVal value As List(Of JobChildTreeDTO))
            ViewState(Me.ID & "_JobChildTree") = value
        End Set
    End Property

    Public Property IDSELECT As Decimal
        Get
            Return ViewState(Me.ID & "_IDSELECT")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSELECT") = value
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
        If Not Page.IsPostBack Then
            jobPosTreeList.ExpandToLevel(1)
        End If
    End Sub
#End Region

#Region "Event"

    Private Sub jobPosTreeList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.TreeListItemDataBoundEventArgs) Handles jobPosTreeList.ItemDataBound
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

    Private Sub jobPosTreeList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.TreeListCommandEventArgs) Handles jobPosTreeList.ItemCommand
        Dim tab As New Hashtable()
        Dim item As Object
        Select Case e.CommandName.ToUpper
            Case "EXPORTTOEXCEL"
                jobPosTreeList.ExportSettings.ExportMode = DirectCast(3, TreeListExportMode)
                jobPosTreeList.ExpandAllItems()
            Case "EXPORTTOPDF"
                jobPosTreeList.ExportSettings.ExportMode = DirectCast(3, TreeListExportMode)
                jobPosTreeList.ExpandAllItems()
            Case "EXPORTTOWORD"
                jobPosTreeList.ExportSettings.ExportMode = DirectCast(3, TreeListExportMode)
                jobPosTreeList.ExpandAllItems()
            Case "SELECT"
                jobPosTreeList.ClearSelectedItems()
                e.ExecuteCommand(e.Item)
                item = e.Item
                If (jobPosTreeList.SelectedItems.Count > 0) Then
                    If item("ID").Text <> "" Then
                        Dim jobId = Decimal.Parse(item("ID").Text)
                        IDSELECT = jobId + 1000000
                        jobChildTreeList_NeedDataSource(Nothing, Nothing)
                    End If
                End If
        End Select
    End Sub

    Protected Sub jobPosTreeList_NeedDataSource(ByVal sender As Object, ByVal e As TreeListNeedDataSourceEventArgs) Handles jobPosTreeList.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim lst As New List(Of JobPositinTreeDTO)
            Using rep As New ProfileRepository
                JobPositinTree = rep.GetJobPosTree()
            End Using

            jobPosTreeList.DataSource = JobPositinTree 'Me.JobPositinTree
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Protected Sub jobPosTreeList_UpdateCommand(ByVal sender As Object, ByVal e As TreeListCommandEventArgs)
        Dim tab As New Hashtable()
        Dim item As TreeListEditableItem = TryCast(e.Item, TreeListEditableItem)
        Dim objJobPosTree As New JobPositinTreeDTO
        Dim rep As New ProfileRepository
        item.ExtractValues(tab)

        ConvertEmptyValuesToDBNull(tab)
        objJobPosTree.ID = Decimal.Parse(tab("ID"))
        objJobPosTree.LY_FTE = Decimal.Parse(tab("LY_FTE"))
        objJobPosTree.PARENT_ID = Decimal.Parse(IIf(IsDBNull(tab("PARENT_ID")), 0, tab("PARENT_ID")))
        objJobPosTree.ORG_NAME = tab("ORG_NAME")
        If rep.ModifyJobPosTreeList(objJobPosTree) Then

        Else
            ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Error)
        End If

    End Sub

    Private Sub jobChildTreeList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.TreeListItemDataBoundEventArgs) Handles jobChildTreeList.ItemDataBound
        If TypeOf e.Item Is TreeListDataItem Then
            Dim dataItem = CType(e.Item, TreeListDataItem)
            If (dataItem.DataItem.FUNCTION_NAME Is Nothing) Then
                dataItem("NAME").CssClass = "NodeFolderContent"
                e.Item.CssClass = "NodeFolderContent1"
            Else
                If dataItem.DataItem.FUNCTION_NAME.Replace("&nbsp;", "").Trim() <> "" Then
                    dataItem("NAME").CssClass = "NodeFileContent"
                    e.Item.CssClass = "NodeFileContent1"
                Else
                    dataItem("NAME").CssClass = "NodeFolderContent"
                    e.Item.CssClass = "NodeFolderContent1"
                End If
            End If

        End If
    End Sub

    'Private Sub jobPosTreeList_PreRender(sender As Object, e As System.EventArgs) Handles jobPosTreeList.PreRender
    '    Dim tab As New Hashtable()
    '    ConvertEmptyValuesToDBNull(tab)
    '    If tab("ID") <> "" Then
    '        Dim jobId As Decimal = Decimal.Parse(tab("ID"))
    '        IDSELECT = jobId - 111
    '    End If
    'End Sub

    Protected Sub jobChildTreeList_NeedDataSource(ByVal sender As Object, ByVal e As TreeListNeedDataSourceEventArgs) Handles jobChildTreeList.NeedDataSource
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim startTime As DateTime = DateTime.UtcNow
        Try
            Dim rep As New ProfileRepository
            Dim lst As New List(Of JobChildTreeDTO)
            lst = rep.GetJobChileTree(IDSELECT)
            Me.JobChildTree = lst

            jobChildTreeList.DataSource = rep.GetJobChileTree(IDSELECT) 'Me.JobPositinTree
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    Private Sub ConvertEmptyValuesToDBNull(ByVal values As Hashtable)
        Dim keysToDbNull As New List(Of Object)()

        For Each entry As DictionaryEntry In values
            If entry.Value Is Nothing OrElse (TypeOf entry.Value Is [String] AndAlso [String].IsNullOrEmpty(DirectCast(entry.Value, [String]))) Then
                keysToDbNull.Add(entry.Key)
            End If
        Next

        For Each key As Object In keysToDbNull
            values(key) = DBNull.Value
        Next
    End Sub

    'Protected Sub BindRowData(ByVal sender As Object, ByVal e As Telerik.Web.UI.TreeListItemDataBoundEventArgs) Handles jobPosTreeList.DataBinding
    '    Try
    '        Dim item As TreeListItem = TryCast(e.Item, TreeListItem)
    '        If item.ItemType = TreeListItemType.AlternatingItem Or item.ItemType = TreeListItemType.Item Or item.ItemType = TreeListItemType.SelectedItem Then
    '            Dim dt = TryCast(e.Item, TreeListDataItem)
    '            Dim img As Image = item.FindControl("img")
    '            Dim NhomDuAn = dt("NHOMDUAN").Text
    '            Dim imgUrl As String = ""
    '            If NhomDuAn = -1 Then
    '                imgUrl = "~/tree2/icons/"
    '            End If
    '            img.ImageUrl = imgUrl
    '        End If

    '    Catch ex As Exception

    '    End Try
    'End Sub

#End Region


End Class