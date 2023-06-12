Imports Framework.UI
Imports Telerik.Web.UI
Imports Common.CommonBusiness

<ValidationProperty("SelectedValue")>
Public Class ctrlOrganizationLoadOnDemand
    Inherits CommonView

    Public Delegate Sub trvOrganization_SelectedNodeChangedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Public Event SelectedNodeChanged As trvOrganization_SelectedNodeChangedDelegate
    Public Delegate Sub trvOrganization_CheckedNodeChangedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Public Event CheckedNodeChanged As trvOrganization_SelectedNodeChangedDelegate
    Dim trvOrganization As New RadTreeView
    Public Overrides Property MustAuthorize As Boolean = False
    Public Property is_UYBAN As Boolean = False

#Region "Property"
    Public Property LoadDataAfterLoaded As Boolean
        Get
            If ViewState(Me.ID & "_LoadDataAfterLoaded") = Nothing Then
                ViewState(Me.ID & "_LoadDataAfterLoaded") = True
            End If
            Return ViewState(Me.ID & "_LoadDataAfterLoaded")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_LoadDataAfterLoaded") = value
        End Set
    End Property
    Public Property LoadAllOrganization As Boolean
        Get
            If ViewState(Me.ID & "_LoadAllOrganization") Is Nothing Then
                ViewState(Me.ID & "_LoadAllOrganization") = False
            End If
            Return ViewState(Me.ID & "_LoadAllOrganization")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_LoadAllOrganization") = value
        End Set
    End Property

    ''' <summary>
    ''' Loại cây sơ đồ tổ chức, theo Location hay Function
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OrganizationType As OrganizationType
        Get
            If ViewState(Me.ID & "_OrganizationType") Is Nothing Then
                ViewState(Me.ID & "_OrganizationType") = OrganizationType.OrganizationLocation
            End If
            Return ViewState(Me.ID & "_OrganizationType")
        End Get
        Set(ByVal value As OrganizationType)
            ViewState(Me.ID & "_OrganizationType") = value
        End Set
    End Property

    Private Property HadLoad As Boolean
        Get
            If ViewState(Me.ID & "_HadLoad") Is Nothing Then
                ViewState(Me.ID & "_HadLoad") = False
            End If
            Return ViewState(Me.ID & "_HadLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_HadLoad") = value
        End Set
    End Property

    Public Property Enabled As Boolean
        Get
            Return trvOrg.Enabled
        End Get
        Set(ByVal value As Boolean)
            trvOrgPostback.Enabled = value
            trvOrg.Enabled = value
        End Set
    End Property

    Public ReadOnly Property OrganizationList As List(Of OrganizationDTO)
        Get
            Dim isDIs = If(cbDissolve.Checked, 0, 1)
            Dim lstOrg As List(Of OrganizationDTO)
            Using rep As New CommonRepository
                If LoadAllOrganization Then
                    lstOrg = rep.GetOrganizationAll(False)
                    Dim lstUyBan = lstOrg.AsQueryable
                    If is_UYBAN Then
                        lstOrg = lstUyBan.AsEnumerable.Where(Function(f) f.ID = 1 OrElse (f.UY_BAN IsNot Nothing AndAlso f.UY_BAN = -1)).ToList
                    Else
                        lstOrg = lstUyBan.AsEnumerable.Where(Function(f) f.UY_BAN Is Nothing Or f.UY_BAN = 0).ToList
                    End If
                    If ViewState(Me.ID & "_OrganizationListAll") Is Nothing Then
                        ViewState(Me.ID & "_OrganizationListAll") = lstOrg
                    End If
                    lstOrg = ViewState(Me.ID & "_OrganizationListAll")
                Else
                    lstOrg = rep.GetOrganizationLocationTreeView_New()
                    Dim lstUyBan = lstOrg.AsQueryable
                    If is_UYBAN Then
                        lstOrg = lstUyBan.AsEnumerable.Where(Function(f) f.ID = 1 OrElse (f.UY_BAN IsNot Nothing AndAlso f.UY_BAN = -1)).ToList
                    Else
                        lstOrg = lstUyBan.AsEnumerable.Where(Function(f) f.UY_BAN Is Nothing Or f.UY_BAN = 0).ToList
                    End If
                End If

            End Using

            Return (From p In lstOrg
                    Where p.ACTFLG = "A").ToList
        End Get
    End Property

    Public ReadOnly Property TreeClientID As String
        Get
            Return If(trvOrg.Visible = True, trvOrg.ClientID, trvOrgPostback.ClientID)
        End Get
    End Property

    Public ReadOnly Property IsDissolve As Boolean
        Get
            Return cbDissolve.Checked
        End Get
    End Property

    Public WriteOnly Property ShowDissolve As Boolean
        Set(ByVal value As Boolean)
            cbDissolve.Visible = value
        End Set
    End Property


    ''' <summary>
    ''' Check cha khi check con
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckParentNodes As Boolean
        Get
            Return trvOrg.TriStateCheckBoxes = True
        End Get
        Set(ByVal value As Boolean)
            trvOrg.TriStateCheckBoxes = value
            trvOrgPostback.TriStateCheckBoxes = value
        End Set
    End Property


    ''' <summary>
    ''' Check con khi check cha
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckChildNodes As Boolean
        Get
            Return trvOrg.CheckChildNodes
        End Get
        Set(ByVal value As Boolean)
            trvOrg.CheckChildNodes = value
            trvOrgPostback.CheckChildNodes = value
        End Set
    End Property

    ''' <summary>
    ''' Loại checkbox (nếu đặt CheckChildNodes=true)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckBoxes As TreeNodeTypes
        Get
            Return trvOrg.CheckBoxes
        End Get
        Set(ByVal value As TreeNodeTypes)
            trvOrg.CheckBoxes = value
            trvOrgPostback.CheckBoxes = value
        End Set
    End Property

    ''' <summary>
    ''' AutoPostBack for TreeView
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AutoPostBack As Boolean
        Get
            If ViewState(Me.ID & "_AutoPostBack") Is Nothing Then
                ViewState(Me.ID & "_AutoPostBack") = True
            End If
            Return ViewState(Me.ID & "_AutoPostBack")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_AutoPostBack") = value
        End Set
    End Property

    ''' <summary>
    ''' Lấy hoặc trả về ID sơ đồ tổ chức đang chọn hiện tại dưới dạng String
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CurrentValue As String
        Get
            If trvOrganization.SelectedValue <> Nothing Then
                ViewState(Me.ID & "_CurrentValue") = trvOrganization.SelectedValue
            End If
            Return ViewState(Me.ID & "_CurrentValue")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_CurrentValue") = value
        End Set
    End Property

    ''' <summary>
    ''' Lấy hoặc trả về code sơ đồ tổ chức đang chọn hiện tại dưới dạng String
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CurrentCode As String
        Get
            If trvOrganization.SelectedNode IsNot Nothing Then
                If trvOrganization.SelectedNode.Text.IndexOf("-") > 0 Then
                    ViewState(Me.ID & "_CurrentCode") = trvOrganization.SelectedNode.Text.Substring(0, trvOrganization.SelectedNode.Text.IndexOf("-") - 1)
                Else
                    ViewState(Me.ID & "_CurrentCode") = ""
                End If
            End If
            Return ViewState(Me.ID & "_CurrentCode")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_CurrentCode") = value
        End Set
    End Property

    ''' <summary>
    ''' Lấy hoặc trả về tên sơ đồ tổ chức đang chọn hiện tại dưới dạng String
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CurrentText As String
        Get
            If trvOrganization.SelectedNode IsNot Nothing Then
                If trvOrganization.SelectedNode.Text.IndexOf("-") > 0 Then
                    ViewState(Me.ID & "_CurrentText") = trvOrganization.SelectedNode.Text.Substring(trvOrganization.SelectedNode.Text.IndexOf("-") + 2)
                Else
                    ViewState(Me.ID & "_CurrentText") = trvOrganization.SelectedNode.Text
                End If
            Else
                ViewState(Me.ID & "_CurrentText") = ""
            End If
            Return ViewState(Me.ID & "_CurrentText")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_CurrentText") = value
        End Set
    End Property

    ''' <summary>
    ''' ''' <summary>
    ''' Lấy hoặc trả về Danh sách ID sơ đồ tổ chức đang chọn hiện tại dưới dạng Decimal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckedValueKeys As List(Of Decimal)
        Get
            Dim lstDecimal As New List(Of Decimal)
            Dim lstChild As New List(Of Decimal)
            Dim lstPermission As New List(Of Decimal)
            Dim lstOrg As New List(Of Decimal)
            Dim lstExistChild As New List(Of String)
            For Each item As RadTreeNode In trvOrganization.CheckedNodes
                If item.ExpandMode = TreeNodeExpandMode.ClientSide Or item.Expanded Then
                    lstDecimal.Add(Decimal.Parse(item.Value))
                Else
                    lstExistChild.Add(item.Value)
                End If
            Next

            If lstExistChild.Count > 0 Then

                Using rep As New CommonRepository
                    lstChild = rep.GetOrganizationChildByList(If(cbDissolve.Checked, 0, 1), lstExistChild)
                End Using
                lstDecimal.AddRange(lstChild)
            End If

            'Check phân quyền
            If lstDecimal.Count > 0 Then
                Using rep As New CommonRepository
                    lstPermission = rep.GetOrganizationPermission(lstDecimal)
                End Using
                lstOrg.AddRange(lstPermission)
            End If

            Return lstOrg

        End Get
        Set(ByVal value As List(Of Decimal))
            Dim lst As New List(Of String)
            For Each item As Decimal In value
                lst.Add(item.ToString())
            Next
            ViewState(Me.ID & "_CheckedValueKeys") = lst
            If trvOrganization.Nodes.Count > 0 Then
                CheckNode(trvOrganization.Nodes(0))
            End If
        End Set
    End Property


    'Public Property CheckedValueKeys As List(Of Decimal)
    '    Get
    '        Dim lstDecimal As New List(Of Decimal)
    '        Dim lst As List(Of String)
    '        'Lấy toàn bộ danh sách các Org đã select
    '        lst = New List(Of String)
    '        For Each item As RadTreeNode In trvOrganization.CheckedNodes
    '            Dim orgID = (From p In OrganizationList Where p.ID = Decimal.Parse(item.Value) And p.IS_NOT_PER = False).FirstOrDefault
    '            If orgID IsNot Nothing Then
    '                lst.Add(item.Value)
    '                lstDecimal.Add(Decimal.Parse(item.Value))
    '            End If
    '        Next
    '        ViewState(Me.ID & "_CheckedValueKeys") = lst
    '        Return lstDecimal
    '    End Get
    '    Set(ByVal value As List(Of Decimal))
    '        Dim lst As New List(Of String)
    '        For Each item As Decimal In value
    '            lst.Add(item.ToString())
    '        Next
    '        ViewState(Me.ID & "_CheckedValueKeys") = lst
    '        If trvOrganization.Nodes.Count > 0 Then
    '            CheckNode(trvOrganization.Nodes(0))
    '        End If
    '    End Set
    'End Property
    Private Sub CheckNode(ByVal Node As RadTreeNode)
        If ViewState(Me.ID & "_CheckedValueKeys").Contains(Node.Value) Then
            Node.Checked = True
        Else
            Node.Checked = False
        End If
        For Each item As RadTreeNode In Node.Nodes
            If ViewState(Me.ID & "_CheckedValueKeys").Contains(item.Value) Then
                item.Checked = True
            Else
                item.Checked = False
            End If
            If item.Nodes.Count > 0 Then CheckNode(item)
        Next
    End Sub
    ''' <summary>
    ''' Lấy hoặc trả về Danh sách ID sơ đồ tổ chức đang chọn hiện tại dưới dạng String ( dùng riêng cho phần Phân quyền )
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckedValueGroups As List(Of String)
        Get
            Dim lst As New List(Of String)
            'Lấy toàn bộ danh sách các Org đã select
            For Each item As RadTreeNode In trvOrganization.CheckedNodes
                Dim orgID = (From p In OrganizationList Where p.ID = Decimal.Parse(item.Value) And p.IS_NOT_PER = False).FirstOrDefault
                If orgID IsNot Nothing Then
                    lst.Add(item.Value)
                End If
                'GetParentByNode(item, lst)
            Next
            ViewState(Me.ID & "_CheckedValues") = lst
            Return ViewState(Me.ID & "_CheckedValues")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_CheckedValues") = value
            For Each item As RadTreeNode In trvOrganization.Nodes
                If value.Contains(item.Value) Then
                    item.Checked = True
                Else
                    item.Checked = False
                End If
            Next
        End Set
    End Property



    ''' <summary>
    ''' Lấy hoặc trả về Danh sách ID sơ đồ tổ chức đang chọn hiện tại dưới dạng String
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckedValues As List(Of String)
        Get
            Dim lstDecimal As New List(Of Decimal)
            lstDecimal = CheckedValueKeys
            Dim result = lstDecimal.ConvertAll(Of String)(Function(f) f.ToString)

            Return result
        End Get
        Set(ByVal value As List(Of String))
            For Each item As RadTreeNode In trvOrganization.Nodes
                If value.Contains(item.Value) Then
                    item.Checked = True
                Else
                    item.Checked = False
                End If
            Next
        End Set
    End Property


    ''' <summary>
    ''' ''' <summary>
    ''' Lấy hoặc trả về Danh sách tên sơ đồ tổ chức đang chọn hiện tại dưới dạng String
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckedTexts As List(Of String)
        Get
            Dim lstString As New List(Of String)
            Dim lstChild As New List(Of String)
            Dim lstExistChild As New List(Of String)
            For Each item As RadTreeNode In trvOrganization.CheckedNodes
                If item.ExpandMode = TreeNodeExpandMode.ClientSide Or item.Expanded Then
                    lstString.Add(item.Text)
                Else
                    lstExistChild.Add(item.Value)
                End If
            Next

            If lstExistChild.Count > 0 Then

                Using rep As New CommonRepository
                    lstChild = rep.GetOrganizationChildTextByList(If(cbDissolve.Checked, 0, 1), lstExistChild)
                End Using
                lstString.AddRange(lstChild)
            End If

            Return lstString
            Dim lst As New List(Of String)
            'Lấy toàn bộ danh sách các Org đã select
            For Each item As RadTreeNode In trvOrganization.CheckedNodes
                lst.Add(item.Text)
            Next
            ViewState(Me.ID & "_CheckedTexts") = lst
            Return ViewState(Me.ID & "_CheckedTexts")
        End Get
        Set(ByVal value As List(Of String))
            ViewState(Me.ID & "_CheckedTexts") = value
        End Set
    End Property

    ''' <summary>
    ''' Trả về danh sách các phòng ban con của 1 đơn vị
    ''' </summary>
    ''' <param name="_orgId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllChild(ByVal _orgId As String) As List(Of Decimal)
        Try
            Dim lstChild As List(Of Decimal)
            Dim lstExistChild As New List(Of String)
            lstExistChild.Add(trvOrganization.SelectedValue)
            Using rep As New CommonRepository
                lstChild = rep.GetOrganizationChildByList(If(cbDissolve.Checked, 0, 1), lstExistChild)
            End Using
            Return lstChild
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public ReadOnly Property IsChecked As Boolean
        Get
            Return trvOrganization.CheckedNodes.Count > 0
        End Get
    End Property

    Public ReadOnly Property Nodes As RadTreeNodeCollection
        Get
            Return trvOrganization.Nodes
        End Get
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startDate As DateTime = DateTime.Now
        Try
            If AutoPostBack Then
                trvOrgPostback.Visible = True
                trvOrg.Visible = False
                trvOrganization = trvOrgPostback
            Else
                trvOrgPostback.Visible = False
                trvOrg.Visible = True
                trvOrganization = trvOrg
            End If
            If Not HadLoad Then
                Refresh()
                HadLoad = True
            End If
            If Session("isDissolve") IsNot Nothing Then
                If (Session("isDissolve") = True) Then
                    cbDissolve.Checked = True
                End If
            End If
            'Framework.UI.Utilities.logger.Info(String.Format("Total time Function ctrlOrganizationLoadOnDemand.ViewLoad:{0}", (DateTime.Now - startDate).TotalSeconds))
        Catch ex As Exception
            'Framework.UI.Utilities.logger.Error("ctrlOrganizationLoadOnDemand.ViewLoad", ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startDate As DateTime = DateTime.Now
        Try
            Try
                trvOrganization.Nodes.Clear()
                BuildOrganization(trvOrganization)
                'Framework.UI.Utilities.logger.Info(String.Format("Total time Function ctrlOrganizationLoadOnDemand.Refresh:{0}", (DateTime.Now - startDate).TotalSeconds))
            Catch ex As Exception
                DisplayException(Me.ViewName, Me.ID, ex)
                'Framework.UI.Utilities.logger.Error("ctrlOrganizationLoadOnDemand.Refresh", ex)
            End Try
        Catch ex As Exception
            'Framework.UI.Utilities.logger.Error("ctrlOrganizationLoadOnDemand.Refresh", ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub trvOrgPostback_SelectedNodeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles trvOrgPostback.NodeClick
        Dim startDate As DateTime = DateTime.Now
        Try
            If trvOrganization.SelectedNode IsNot Nothing Then
                CurrentValue = trvOrganization.SelectedValue
                CurrentText = trvOrganization.SelectedNode.Text.Substring(trvOrganization.SelectedNode.Text.IndexOf("-") + 1)
                If trvOrganization.SelectedNode Is trvOrganization.Nodes(0) AndAlso trvOrganization.SelectedNode.Nodes.Count > 0 Then
                    trvOrganization.SelectedNode.Nodes(0).ExpandParentNodes()
                Else
                    trvOrganization.SelectedNode.ExpandParentNodes()
                End If

            Else
                If trvOrganization.Nodes.Count > 0 Then
                    If trvOrganization.Nodes(0).Nodes.Count > 0 Then
                        trvOrganization.Nodes(0).Nodes(0).ExpandParentNodes()
                    End If
                End If

            End If
            RaiseEvent SelectedNodeChanged(sender, e)
            'Framework.UI.Utilities.logger.Info(String.Format("Total time Function ctrlOrganizationLoadOnDemand.trvOrgPostback_SelectedNodeChanged:{0}", (DateTime.Now - startDate).TotalSeconds))
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            'Framework.UI.Utilities.logger.Error("ctrlOrganizationLoadOnDemand.trvOrgPostback_SelectedNodeChanged", ex)
        End Try

    End Sub

    Protected Sub cbDissolve_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbDissolve.CheckedChanged
        Dim startDate As DateTime = DateTime.Now
        Try
            trvOrganization.Nodes.Clear()
            BuildOrganization(trvOrganization)
            'Framework.UI.Utilities.logger.Info(String.Format("Total time Function ctrlOrganizationLoadOnDemand.cbDissolve_CheckedChanged:{0}", (DateTime.Now - startDate).TotalSeconds))
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            'Framework.UI.Utilities.logger.Error("ctrlOrganizationLoadOnDemand.cbDissolve_CheckedChanged", ex)
        End Try

    End Sub

    Private Sub trvOrg_NodeDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles trvOrg.NodeDataBound, trvOrgPostback.NodeDataBound
        Try
            Dim node As Object
            node = CType(e.Node.DataItem, OrganizationDTO)
            If node.STATUS = 0 Then
                e.Node.BackColor = Drawing.Color.Yellow
            End If
            If CType(CommonConfig.dicConfig("IS_ORG_EXPAND"), Boolean) AndAlso CType(CommonConfig.dicConfig("ORG_EXPAND_LEVEL"), Decimal) > 1 Then
                Dim level = CType(CommonConfig.dicConfig("ORG_EXPAND_LEVEL"), Decimal)
                If e.Node.Level < level - 1 Then
                    e.Node.Expanded = True
                End If
            End If
        Catch ex As Exception
            'Framework.UI.Utilities.logger.Error("ctrlOrganizationLoadOnDemand.trvOrg_NodeDataBound", ex)
        End Try
    End Sub

    Private Sub trvOrgExpand_PopulateNodeOnDemand(ByVal sender As Object, ByVal e As RadTreeNodeEventArgs) Handles trvOrg.NodeExpand, trvOrgPostback.NodeExpand
        Dim isCheck As Boolean
        Dim strorg As String = ""
        Dim startDate As DateTime = DateTime.Now
        Try
            isCheck = e.Node.Checked
            Dim lstOrg As List(Of OrganizationDTO)

            lstOrg = (From p In OrganizationList
                      Where p.PARENT_ID = e.Node.Value
                      Order By p.ORD_NO, p.NAME_VN).ToList

            For Each obj As OrganizationDTO In lstOrg
                Dim node As New RadTreeNode()
                node.Text = obj.NAME_VN
                node.Value = obj.ID
                node.DataItem = obj
                If obj.CHILD_COUNT > 0 Then
                    node.ExpandMode = TreeNodeExpandMode.ServerSideCallBack
                End If
                node.Checked = isCheck
                e.Node.Nodes.Add(node)
            Next
            e.Node.Checked = isCheck
            CheckNode(trvOrganization.Nodes(0))
            e.Node.Expanded = True
            'Framework.UI.Utilities.logger.Info(String.Format("Total time Function ctrlOrganizationLoadOnDemand.trvOrgExpand_PopulateNodeOnDemand:{0}", (DateTime.Now - startDate).TotalSeconds))
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            'Framework.UI.Utilities.logger.Error("ctrlOrganizationLoadOnDemand.trvOrgExpand_PopulateNodeOnDemand", ex)
        End Try

    End Sub

#End Region

#Region "Custom"

    ''' <summary>
    ''' ''' <summary>
    ''' Trả về DTO sơ đồ tổ chức đang chọn hiện tại
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CurrentItemDataObject() As Object
        Try
            Dim rep As New CommonRepository
            If CurrentValue <> "" Then
                Return (From p In OrganizationList Where p.ID = CurrentValue).FirstOrDefault
            End If
            Return Nothing
        Catch ex As Exception
            'Framework.UI.Utilities.logger.Error("ctrlOrganizationLoadOnDemand.CurrentItemDataObject", ex)
            Return Nothing
        End Try
    End Function

    Protected Sub BuildOrganization(ByVal tree As RadTreeView)
        Dim startDate As DateTime = DateTime.Now
        Try
            Dim lstOrg As New List(Of OrganizationDTO)
            Dim lstChild As New List(Of OrganizationDTO)
            lstOrg = (From p In OrganizationList
                      Where p.ID = 1).ToList

            lstChild = (From p In OrganizationList
                        Where p.PARENT_ID = 1
                        Order By p.ORD_NO, p.NAME_VN).ToList

            For Each obj As OrganizationDTO In lstOrg
                Dim node As New RadTreeNode()
                node.Text = obj.NAME_VN
                node.Value = obj.ID
                node.DataItem = obj
                node.Selected = True
                tree.Nodes.Add(node)
                For Each child As OrganizationDTO In lstChild
                    Dim isShowByLevel = CType(CommonConfig.dicConfig("IS_ORG_EXPAND"), Boolean) AndAlso CType(CommonConfig.dicConfig("ORG_EXPAND_LEVEL"), Decimal) > 1
                    Dim nodeChild As New RadTreeNode()
                    nodeChild.Text = child.NAME_VN
                    nodeChild.Value = child.ID
                    nodeChild.DataItem = child
                    node.Expanded = True
                    node.Nodes.Add(nodeChild)
                    If isShowByLevel Then
                        Dim level = CType(CommonConfig.dicConfig("ORG_EXPAND_LEVEL"), Decimal)
                        If level > 2 Then
                            Dim lstChild2 = (From p In OrganizationList
                                             Where p.PARENT_ID = child.ID
                                             Order By p.ORD_NO, p.NAME_VN).ToList
                            For Each item In lstChild2
                                Dim nodeChild2 As New RadTreeNode()
                                nodeChild2.Text = item.NAME_VN
                                nodeChild2.Value = item.ID
                                nodeChild2.DataItem = item
                                nodeChild.Expanded = True
                                nodeChild.Nodes.Add(nodeChild2)
                                If level > 3 Then
                                    Dim lstChild3 = (From p In OrganizationList
                                                     Where p.PARENT_ID = item.ID
                                                     Order By p.ORD_NO, p.NAME_VN).ToList
                                    For Each item2 In lstChild3
                                        Dim nodeChild3 As New RadTreeNode()
                                        nodeChild3.Text = item2.NAME_VN
                                        nodeChild3.Value = item2.ID
                                        nodeChild3.DataItem = item2
                                        nodeChild2.Expanded = True
                                        nodeChild2.Nodes.Add(nodeChild3)
                                        If level > 4 Then
                                            Dim lstChild4 = (From p In OrganizationList
                                                             Where p.PARENT_ID = item2.ID
                                                             Order By p.ORD_NO, p.NAME_VN).ToList
                                            For Each item3 In lstChild4
                                                Dim nodeChild4 As New RadTreeNode()
                                                nodeChild4.Text = item3.NAME_VN
                                                nodeChild4.Value = item3.ID
                                                nodeChild4.DataItem = item3
                                                nodeChild3.Expanded = True
                                                nodeChild3.Nodes.Add(nodeChild4)
                                                If level > 5 Then
                                                    Dim lstChild5 = (From p In OrganizationList
                                                                     Where p.PARENT_ID = item3.ID
                                                                     Order By p.ORD_NO, p.NAME_VN).ToList
                                                    For Each item4 In lstChild5
                                                        Dim nodeChild5 As New RadTreeNode()
                                                        nodeChild5.Text = item4.NAME_VN
                                                        nodeChild5.Value = item4.ID
                                                        nodeChild5.DataItem = item4
                                                        nodeChild4.Expanded = True
                                                        nodeChild4.Nodes.Add(nodeChild5)
                                                        If level > 6 Then
                                                            Dim lstChild6 = (From p In OrganizationList
                                                                             Where p.PARENT_ID = item4.ID
                                                                             Order By p.ORD_NO, p.NAME_VN).ToList
                                                            For Each item5 In lstChild6
                                                                Dim nodeChild6 As New RadTreeNode()
                                                                nodeChild6.Text = item5.NAME_VN
                                                                nodeChild6.Value = item5.ID
                                                                nodeChild6.DataItem = item5
                                                                nodeChild5.Expanded = True
                                                                nodeChild5.Nodes.Add(nodeChild6)
                                                                If level > 7 Then
                                                                    Dim lstChild7 = (From p In OrganizationList
                                                                                     Where p.PARENT_ID = item5.ID
                                                                                     Order By p.ORD_NO, p.NAME_VN).ToList
                                                                    For Each item6 In lstChild7
                                                                        Dim nodeChild7 As New RadTreeNode()
                                                                        nodeChild7.Text = item6.NAME_VN
                                                                        nodeChild7.Value = item6.ID
                                                                        nodeChild7.DataItem = item6
                                                                        nodeChild6.Expanded = True
                                                                        nodeChild6.Nodes.Add(nodeChild7)
                                                                        If level > 8 Then
                                                                            Dim lstChild8 = (From p In OrganizationList
                                                                                             Where p.PARENT_ID = item6.ID
                                                                                             Order By p.ORD_NO, p.NAME_VN).ToList
                                                                            For Each item7 In lstChild8
                                                                                Dim nodeChild8 As New RadTreeNode()
                                                                                nodeChild8.Text = item7.NAME_VN
                                                                                nodeChild8.Value = item7.ID
                                                                                nodeChild8.DataItem = item7
                                                                                nodeChild7.Expanded = True
                                                                                nodeChild7.Nodes.Add(nodeChild8)
                                                                                If level > 9 Then
                                                                                    Dim lstChild9 = (From p In OrganizationList
                                                                                                     Where p.PARENT_ID = item7.ID
                                                                                                     Order By p.ORD_NO, p.NAME_VN).ToList
                                                                                    For Each item8 In lstChild9
                                                                                        Dim nodeChild9 As New RadTreeNode()
                                                                                        nodeChild9.Text = item8.NAME_VN
                                                                                        nodeChild9.Value = item8.ID
                                                                                        nodeChild9.DataItem = item7
                                                                                        nodeChild8.Expanded = True
                                                                                        nodeChild8.Nodes.Add(nodeChild9)
                                                                                        If level > 10 Then
                                                                                            Dim lstChild10 = (From p In OrganizationList
                                                                                                              Where p.PARENT_ID = item8.ID
                                                                                                              Order By p.ORD_NO, p.NAME_VN).ToList
                                                                                            For Each item9 In lstChild10
                                                                                                Dim nodeChild10 As New RadTreeNode()
                                                                                                nodeChild10.Text = item9.NAME_VN
                                                                                                nodeChild10.Value = item9.ID
                                                                                                nodeChild10.DataItem = item7
                                                                                                nodeChild9.Expanded = True
                                                                                                nodeChild9.Nodes.Add(nodeChild10)
                                                                                            Next
                                                                                        Else
                                                                                            If item8.CHILD_COUNT > 0 Then
                                                                                                nodeChild9.ExpandMode = TreeNodeExpandMode.ServerSideCallBack
                                                                                            End If
                                                                                        End If
                                                                                    Next
                                                                                Else
                                                                                    If item7.CHILD_COUNT > 0 Then
                                                                                        nodeChild8.ExpandMode = TreeNodeExpandMode.ServerSideCallBack
                                                                                    End If
                                                                                End If
                                                                            Next
                                                                        Else
                                                                            If item6.CHILD_COUNT > 0 Then
                                                                                nodeChild7.ExpandMode = TreeNodeExpandMode.ServerSideCallBack
                                                                            End If
                                                                        End If
                                                                    Next
                                                                Else
                                                                    If item5.CHILD_COUNT > 0 Then
                                                                        nodeChild6.ExpandMode = TreeNodeExpandMode.ServerSideCallBack
                                                                    End If
                                                                End If
                                                            Next
                                                        Else
                                                            If item4.CHILD_COUNT > 0 Then
                                                                nodeChild5.ExpandMode = TreeNodeExpandMode.ServerSideCallBack
                                                            End If
                                                        End If
                                                    Next
                                                Else
                                                    If item3.CHILD_COUNT > 0 Then
                                                        nodeChild4.ExpandMode = TreeNodeExpandMode.ServerSideCallBack
                                                    End If
                                                End If
                                            Next
                                        Else
                                            If item2.CHILD_COUNT > 0 Then
                                                nodeChild3.ExpandMode = TreeNodeExpandMode.ServerSideCallBack
                                            End If
                                        End If
                                    Next
                                Else
                                    If item.CHILD_COUNT > 0 Then
                                        nodeChild2.ExpandMode = TreeNodeExpandMode.ServerSideCallBack
                                    End If
                                End If
                            Next
                        Else
                            If child.CHILD_COUNT > 0 Then
                                nodeChild.ExpandMode = TreeNodeExpandMode.ServerSideCallBack
                            End If
                        End If
                    End If
                Next
            Next
            trvOrgPostback_SelectedNodeChanged(Nothing, Nothing)
            'Framework.UI.Utilities.logger.Info(String.Format("Total time Function ctrlOrganizationLoadOnDemand.BuildOrganization:{0}", (DateTime.Now - startDate).TotalSeconds))
        Catch ex As Exception
            'Framework.UI.Utilities.logger.Error("ctrlOrganizationLoadOnDemand.BuildOrganization", ex)
        End Try
    End Sub

    Public Function GenOrgByLevel(ByVal obj As OrganizationDTO) As RadTreeNode
        Dim nodeChild As New RadTreeNode()
        nodeChild.Text = obj.NAME_VN
        nodeChild.Value = obj.ID
        nodeChild.DataItem = obj
        If obj.CHILD_COUNT > 0 Then
            nodeChild.ExpandMode = TreeNodeExpandMode.ServerSideCallBack
        End If
        Return nodeChild
        Try
        Catch ex As Exception
            'Framework.UI.Utilities.logger.Error("ctrlOrganizationLoadOnDemand.trvOrg_NodeDataBound", ex)
        End Try
    End Function


    ''' <summary>
    ''' Trả về cấu trúc sơ đồ tổ chức đang chọn hiện tại (chỉ có nhánh đơn vị đang chọn)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CurrentStructureInfo() As List(Of OrganizationStructureDTO)
        Try
            Dim rep As New CommonRepository
            If CurrentValue <> "" Then
                Return rep.GetOrganizationStructureInfo(Decimal.Parse(CurrentValue))
            End If
            Return Nothing
        Catch ex As Exception
            'Framework.UI.Utilities.logger.Error("ctrlOrganizationLoadOnDemand.CurrentStructureInfo", ex)
            Return Nothing
        End Try
    End Function


#End Region

End Class
