Imports Common.CommonBusiness
Imports Framework.UI
Imports Telerik.Web.UI
Imports WebAppLog

<ValidationProperty("SelectedValue")>
Public Class ctrlOrganization
    Inherits CommonView

    Public Delegate Sub trvOrganization_SelectedNodeChangedDelegate(ByVal sender As Object, ByVal e As EventArgs)

    Public Event SelectedNodeChanged As trvOrganization_SelectedNodeChangedDelegate
    Dim trvOrganization As New RadTreeView
    Public Overrides Property MustAuthorize As Boolean = False
    'ChienNV added when checked value in organization then autopostback -> return list(of decimal)
    Public Delegate Sub trvOrganization_CheckedNodeChangedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Public Event CheckedNodeChanged As trvOrganization_CheckedNodeChangedDelegate

    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile/Module/Profile/Setting/" + Me.GetType().Name.ToString()

#Region "Property"
    Public Property is_UYBAN As Boolean = False
    Public Property build_UYBAN As Boolean = False
    Public Property IsOM As Boolean
        Get
            If ViewState(Me.ID & "_IsOM") Is Nothing Then
                ViewState(Me.ID & "_IsOM") = False
            End If
            Return ViewState(Me.ID & "_IsOM")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_IsOM") = value
        End Set
    End Property
    ''' <summary>
    ''' hiển thị all tổ chức
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' PeriodID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property PeriodID As Decimal
        Get
            If ViewState(Me.ID & "_PeriodID") Is Nothing Then
                ViewState(Me.ID & "_PeriodID") = 0
            End If
            Return ViewState(Me.ID & "_PeriodID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_PeriodID") = value
        End Set
    End Property

    Public Property strOrgs As String
        Get
            If ViewState(Me.ID & "_strOrgs") Is Nothing Then
                ViewState(Me.ID & "_strOrgs") = False
            End If
            Return ViewState(Me.ID & "_strOrgs")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_strOrgs") = value
        End Set
    End Property

    ''' <summary>
    ''' ColorType
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property ColorType As PeriodType
        Get
            Return ViewState(Me.ID & "_ColorType")
        End Get
        Set(ByVal value As PeriodType)
            ViewState(Me.ID & "_ColorType") = value
        End Set
    End Property
    ''' <summary>
    ''' HadLoad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property HadLoad As Boolean
        Get
            If ViewState(Me.ID & "_HadLoad") = Nothing Then
                ViewState(Me.ID & "_HadLoad") = False
            End If
            Return ViewState(Me.ID & "_HadLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_HadLoad") = value
        End Set
    End Property
    Public Property IS_HadLoad As Boolean?
        Get

            Return ViewState(Me.ID & "_IS_HadLoad")
        End Get
        Set(ByVal value As Boolean?)
            ViewState(Me.ID & "_IS_HadLoad") = value
        End Set
    End Property
    ''' <summary>
    ''' Enable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Enabled As Boolean
        Get
            Return trvOrg.Enabled
        End Get
        Set(ByVal value As Boolean)
            trvOrgPostback.Enabled = value
            trvOrg.Enabled = value
        End Set
    End Property
    ''' <summary>
    ''' Danh sách tổ chức
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property OrganizationList As List(Of OrganizationDTO)
        Get
            Using rep As New CommonRepository
                If LoadAllOrganization Then
                    Dim lstOrg = rep.GetOrganizationAll(IsOM)
                    If is_UYBAN And chkViewCommitee.Visible = False Then
                        Dim lstUyBan = lstOrg.AsQueryable
                        lstUyBan = lstUyBan.Where(Function(f) f.ID = 1 OrElse (f.UY_BAN IsNot Nothing AndAlso f.UY_BAN = -1))
                        lstOrg = lstUyBan.ToList
                    ElseIf is_UYBAN And chkViewCommitee.Visible = True Then
                        Dim lstUyBan = lstOrg.AsQueryable
                        If chkViewCommitee.Checked = True Then
                            lstUyBan = lstUyBan.Where(Function(f) f.ID = 1 OrElse (f.UY_BAN IsNot Nothing AndAlso f.UY_BAN = -1))
                        Else
                            lstUyBan = lstUyBan.Where(Function(f) f.UY_BAN Is Nothing Or f.UY_BAN = 0)
                        End If
                        lstOrg = lstUyBan.ToList
                    Else
                        Dim lstUyBan = lstOrg.AsQueryable
                        lstUyBan = lstUyBan.Where(Function(f) f.UY_BAN Is Nothing Or f.UY_BAN = 0)
                        Return lstUyBan.ToList
                    End If

                    If ViewState(Me.ID & "_OrganizationListAll") Is Nothing Then
                        ViewState(Me.ID & "_OrganizationListAll") = lstOrg
                    End If
                    Return ViewState(Me.ID & "_OrganizationListAll")
                Else
                    Dim lstOrg = rep.GetOrganizationLocationTreeView()
                    If is_UYBAN And chkViewCommitee.Visible = False Then
                        Dim lstUyBan = lstOrg.AsQueryable
                        lstUyBan = lstUyBan.Where(Function(f) f.ID = 1 OrElse (f.UY_BAN IsNot Nothing AndAlso f.UY_BAN = -1))
                        Return lstUyBan.ToList
                    ElseIf is_UYBAN And chkViewCommitee.Visible = True Or ShowCommitee_static_check = 1 Then
                        Dim lstUyBan = lstOrg.AsQueryable

                        If chkViewCommitee.Checked = True Then
                            lstUyBan = lstUyBan.Where(Function(f) f.ID = 1 OrElse (f.UY_BAN IsNot Nothing AndAlso f.UY_BAN = -1))
                        Else
                            lstUyBan = lstUyBan.Where(Function(f) f.UY_BAN Is Nothing Or f.UY_BAN = 0)
                        End If
                        Return lstUyBan.ToList
                    Else
                        Dim lstUyBan = lstOrg.AsQueryable
                        lstUyBan = lstUyBan.Where(Function(f) f.UY_BAN Is Nothing Or f.UY_BAN = 0)
                        Return lstUyBan.ToList
                    End If
                End If
            End Using
        End Get
    End Property
    ''' <summary>
    ''' Check để lấy toàn bộ danh sách các Org đã chọn
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListOrg_Checked As List(Of OrganizationDTO)
        Get
            Dim listOrg As New List(Of OrganizationDTO)
            Dim Organization_List = OrganizationList
            'Lấy toàn bộ danh sách các Org đã select
            For Each item As RadTreeNode In trvOrganization.CheckedNodes
                Dim org = (From p In Organization_List Where p.ID = Decimal.Parse(item.Value) And p.IS_NOT_PER = False).FirstOrDefault
                If org IsNot Nothing Then
                    listOrg.Add(org)
                End If
            Next
            ViewState(Me.ID & "_ListOrg_Checked") = listOrg
            Return ViewState(Me.ID & "_ListOrg_Checked")
        End Get
        Set(ByVal value As List(Of OrganizationDTO))
            ViewState(Me.ID & "_ListOrg_Checked") = value
        End Set
    End Property
    ''' <summary>
    ''' TreeClientID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TreeClientID As String
        Get
            Return If(trvOrg.Visible = True, trvOrg.ClientID, trvOrgPostback.ClientID)
        End Get
    End Property
    ''' <summary>
    ''' IsDissolve
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsDissolve As Boolean
        Get
            Return cbDissolve.Checked
        End Get
    End Property
    ''' <summary>
    ''' ShowDissolve
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ShowDissolve As Boolean
        Set(ByVal value As Boolean)
            cbDissolve.Visible = value
        End Set
    End Property
    ''' <summary>
    ''' ShowCommitee
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ShowCommitee As Boolean
        Set(ByVal value As Boolean)
            chkViewCommitee.Visible = value
        End Set
    End Property
    ''' <summary>
    ''' ShowCommitee_static_check dùng để thiết lập cứng hiển thị cây sơ đồ tổ chức ủy ban
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property ShowCommitee_static_check As Integer
        Get
            Return ViewState(Me.ID & "_ShowCommitee_static_check")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_ShowCommitee_static_check") = value
            If value = 1 Then
                is_UYBAN = True
                build_UYBAN = True
                chkViewCommitee.Checked = True
            End If
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
    ''' Hiển thị theo cấp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShowLevel As Integer
        Get
            If ViewState(Me.ID & "_ShowLevel") Is Nothing Then
                Return 0
            End If
            Return ViewState(Me.ID & "_ShowLevel")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_ShowLevel") = value
        End Set
    End Property

    ''' <summary>
    ''' Tự động Select First Node
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelectFirstNode As Boolean
        Get
            If ViewState(Me.ID & "_SelectFirstNode") Is Nothing Then
                Return False
            End If
            Return ViewState(Me.ID & "_SelectFirstNode")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_SelectFirstNode") = value
        End Set
    End Property

    ''' <summary>
    ''' Tự động Load cây sơ đồ tổ chức sau khi Load View
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
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

    ''' <summary>
    ''' Postback selectnodechanged
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NodeClickPostback As Boolean
        Get
            If ViewState(Me.ID & "_NodeClickPostback") Is Nothing Then
                ViewState(Me.ID & "_NodeClickPostback") = True
            End If
            Return ViewState(Me.ID & "_NodeClickPostback")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_NodeClickPostback") = value
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
    Public Property PARENT_ID_VALUE As String
        Get
            If trvOrganization.SelectedValue <> Nothing Then
                ViewState(Me.ID & "_PARENT_ID_VALUE") = trvOrganization.SelectedNode.Level
            End If
            Return ViewState(Me.ID & "_PARENT_ID_VALUE")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_PARENT_ID_VALUE") = value
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
    ''' Lấy hoặc trả về Danh sách ID sơ đồ tổ chức đang chọn hiện tại dưới dạng String
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckedValues As List(Of String)
        Get
            Dim lst As New List(Of String)
            'Lấy toàn bộ danh sách các Org đã select
            Dim Organization_List = OrganizationList
            For Each item As RadTreeNode In trvOrganization.CheckedNodes
                Dim orgID = (From p In Organization_List Where p.ID = Decimal.Parse(item.Value) And p.IS_NOT_PER = False).FirstOrDefault
                If orgID IsNot Nothing Then
                    lst.Add(item.Value)
                End If
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
    ''' Lấy hoặc trả về Danh sách ID sơ đồ tổ chức đang chọn hiện tại dưới dạng String ( dùng riêng cho phần Phân quyền )
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckedValueGroups As List(Of String)
        Get
            Dim lst As New List(Of String)
            'Lấy toàn bộ danh sách các Org đã select
            Dim Organization_List = OrganizationList
            For Each item As RadTreeNode In trvOrganization.CheckedNodes
                Dim orgID = (From p In Organization_List Where p.ID = Decimal.Parse(item.Value) And p.IS_NOT_PER = False).FirstOrDefault
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
            Dim Organization_List = OrganizationList
            For Each item As RadTreeNode In trvOrganization.CheckedNodes
                Dim orgID = (From p In Organization_List Where p.ID = Decimal.Parse(item.Value)).FirstOrDefault
                If orgID IsNot Nothing Then
                    lstDecimal.Add(Decimal.Parse(item.Value))
                End If
            Next
            Return lstDecimal
        End Get
        Set(ByVal value As List(Of Decimal))
            trvOrganization.UncheckAllNodes()
            trvOrganization.CollapseAllNodes()
            For Each orgID In value
                Dim node = trvOrganization.FindNodeByValue(orgID)
                If node IsNot Nothing Then
                    node.Checked = True
                    node.ExpandParentNodes()
                End If
            Next
        End Set
    End Property

    Public Property Bind_CheckedValueKeys As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_Bind_CheckedValueKeys")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_Bind_CheckedValueKeys") = value
        End Set
    End Property
    Public Property Bind_Find_ValueKeys As List(Of Decimal)
        Get
            Return ViewState(Me.ID & "_Bind_Find_ValueKeys")
        End Get
        Set(ByVal value As List(Of Decimal))
            ViewState(Me.ID & "_Bind_Find_ValueKeys") = value
        End Set
    End Property
    ''' <summary>
    ''' SetColorPeriod (màu vàng) cho tổ chức giải thể, bỏ màu vàng khi tổ chức hoạt động trở lại
    ''' </summary>
    ''' <param name="periodID"></param>
    ''' <param name="type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetColorPeriod(ByVal periodID As String, ByVal type As PeriodType) As Boolean
        Dim dtData As DataTable
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim nodes = trvOrganization.GetAllNodes()
            If nodes.Count > 0 Then
                For Each node In nodes
                    If node.Level <> 0 Then
                        node.ForeColor = trvOrganization.Nodes(0).ForeColor
                    End If
                Next
            End If
            If periodID = "" Then
                Return True
            End If
            Me.PeriodID = periodID
            Me.ColorType = type
            Using rep As New CommonRepository
                dtData = rep.GetATOrgPeriod(periodID)
                If dtData IsNot Nothing AndAlso dtData.Rows.Count > 0 Then
                    For Each row In dtData.Rows
                        If row("ORG_ID").ToString <> "" Then
                            Dim node = trvOrganization.FindNodeByValue(row("ORG_ID").ToString)
                            If node IsNot Nothing Then
                                If node.Level = 0 Then
                                    Continue For
                                End If
                                Select Case type
                                    Case PeriodType.AT
                                        If row("STATUSCOLEX") = 0 Then
                                            node.ForeColor = Drawing.Color.DodgerBlue
                                        Else
                                            'node.ForeColor = Drawing.Color.Blue
                                        End If
                                    Case PeriodType.PA
                                        If row("STATUSPAROX") = 0 Then
                                            node.ForeColor = Drawing.Color.DodgerBlue
                                        Else
                                            'node.ForeColor = Drawing.Color.Blue
                                        End If
                                End Select
                            End If
                        End If
                    Next
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

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
    Public ReadOnly Property GetCheckedTexts As List(Of String)
        Get
            Dim lst As New List(Of String)
            'Lấy toàn bộ danh sách các Org đã select
            For Each item As RadTreeNode In trvOrganization.CheckedNodes
                lst.Add(item.Text)
            Next
            ViewState(Me.ID & "_GetCheckedTexts") = lst
            Return ViewState(Me.ID & "_GetCheckedTexts")
        End Get
    End Property

    ''' <summary>
    ''' Mở toàn bộ cây sau khi Load
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExpandOnLoad As Boolean
        Get
            If ViewState(Me.ID & "_ExpandOnLoad") Is Nothing Then
                ViewState(Me.ID & "_ExpandOnLoad") = True
            End If
            Return ViewState(Me.ID & "_ExpandOnLoad")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_ExpandOnLoad") = value
        End Set
    End Property
    Public Sub SetCheckedValue(ByVal value As List(Of Decimal))
        For Each orgID In value
            Dim node = trvOrganization.FindNodeByValue(orgID)
            If node IsNot Nothing Then
                node.Checked = True
                node.ExpandParentNodes()
            End If
        Next
    End Sub

#End Region

#Region "Page"
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức ViewLoad trang
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
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
            If LoadDataAfterLoaded And Not HadLoad Then
                Refresh()
            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức làm mới trạng thái các control trên trang
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            trvOrganization.Nodes.Clear()
            If OrganizationType = Global.Common.OrganizationType.OrganizationLocation Then
                BuildOrganization(trvOrganization, OrganizationList, cbDissolve.Checked, chkViewCommitee.Checked)
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedNodeChanged của control trvOrgPostBack
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub trvOrgPostback_SelectedNodeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles trvOrgPostback.NodeClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If trvOrganization.SelectedNode IsNot Nothing Then
                CurrentValue = trvOrganization.SelectedValue
                CurrentText = trvOrganization.SelectedNode.Text.Substring(trvOrganization.SelectedNode.Text.IndexOf("-") + 1)
                PARENT_ID_VALUE = trvOrganization.SelectedNode.Level
                trvOrganization.SelectedNode.ExpandParentNodes()
            End If
            RaiseEvent SelectedNodeChanged(sender, e)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện CheckedChanged cúa control cbDissolve
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cbDissolve_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cbDissolve.CheckedChanged
        Dim rep As New CommonRepository
        Dim lstOrgs As List(Of CommonBusiness.OrganizationDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If OrganizationType = Global.Common.OrganizationType.OrganizationLocation Then
                BuildOrganization(trvOrganization, OrganizationList, cbDissolve.Checked, chkViewCommitee.Checked)
            End If
            trvOrgPostback_SelectedNodeChanged(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub trvOrgPostback_CheckedNodeChanged(ByVal sender As Object, ByVal e As EventArgs) Handles trvOrgPostback.NodeCheck
        Try
            If trvOrganization.CheckedNodes IsNot Nothing Then
                strOrgs = String.Join(",", From p In trvOrgPostback.CheckedNodes.AsEnumerable Select p.Value)
            End If
            RaiseEvent CheckedNodeChanged(sender, e)
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện NodeDataBound cho control trvOrgm, trvOrgPostback
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub trvOrg_NodeDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles trvOrg.NodeDataBound, trvOrgPostback.NodeDataBound
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim node As OrganizationDTO
            If OrganizationType = Global.Common.OrganizationType.OrganizationLocation Then
                node = CType(e.Node.DataItem, OrganizationDTO)
            End If

            If ShowLevel <> 0 Then
                If e.Node.Level + 1 > ShowLevel Then
                    e.Node.Visible = False
                End If
            End If


            If e.Node.Level < 4 Then
                e.Node.Expanded = True
            End If
            'e.Node.Text = node.CODE & " - " & node.NAME_VN
            If node.DISSOLVE_DATE IsNot Nothing Then
                If node.DISSOLVE_DATE < Date.Now Then
                    e.Node.BackColor = Drawing.Color.Yellow
                End If
            End If
            If node.ACTFLG = "I" Then
                e.Node.BackColor = Drawing.Color.Yellow
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Check all node trvOrg
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChkAll() As Boolean
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            trvOrg.CheckAllNodes()
            Return True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Return False
        End Try
    End Function
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' clear check box
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UncheckAllNodes() As Boolean
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            trvOrgPostback.UncheckAllNodes()
            Return True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Return False
        End Try
    End Function
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy toàn bộ danh sách ID sơ đồ tổ chức
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllOrgID() As List(Of Decimal)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim lst As List(Of Decimal)
            If OrganizationType = Global.Common.OrganizationType.OrganizationLocation Then
                lst = (From p In OrganizationList Where p.IS_NOT_PER = False Select p.ID).ToList
            End If
            Return lst
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
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
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New CommonRepository
            If OrganizationType = Global.Common.OrganizationType.OrganizationLocation Then
                If CurrentValue <> "" Then
                    Return (From p In OrganizationList Where p.ID = Decimal.Parse(CurrentValue)).FirstOrDefault
                End If
                Return Nothing
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
            Return Nothing
        End Try
    End Function
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Trả về cấu trúc sơ đồ tổ chức đang chọn hiện tại (chỉ có nhánh đơn vị đang chọn)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CurrentStructureInfo() As List(Of OrganizationStructureDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim rep As New CommonRepository
            If CurrentValue <> "" Then
                Return rep.GetOrganizationStructureInfo(Decimal.Parse(CurrentValue))
            End If
            Return Nothing
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Trả về danh sách các phòng ban con của 1 đơn vị
    ''' </summary>
    ''' <param name="_orgId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllChild(ByVal _orgId As String) As List(Of Decimal)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If _orgId IsNot Nothing AndAlso _orgId.Trim <> "" Then
                Dim id = Decimal.Parse(_orgId)
                Return GetAllChild(id)
            End If
            Return New List(Of Decimal)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Trả về danh sách các phòng ban con của 1 đơn vị
    ''' </summary>
    ''' <param name="_orgId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Function GetAllChild(ByVal _orgId As Decimal) As List(Of Decimal)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Dim FindNode As RadTreeNode = Nothing
        Dim lst As New List(Of Decimal)
        Try
            If OrganizationType = Global.Common.OrganizationType.OrganizationLocation Then
                If _orgId <> 0 Then
                    FindNode = trvOrganization.FindNodeByValue(_orgId.ToString)
                ElseIf trvOrganization.Nodes.Count > 0 Then
                    FindNode = trvOrganization.Nodes(0)
                End If
                If FindNode IsNot Nothing Then
                    lst.Add(Decimal.Parse(FindNode.Value))
                    Dim childNodes = FindNode.GetAllNodes
                    Dim Organization_List = OrganizationList
                    For Each Node In childNodes
                        Dim orgID = (From p In Organization_List Where p.ID = Decimal.Parse(Node.Value) And p.IS_NOT_PER = False).FirstOrDefault
                        If orgID IsNot Nothing Then
                            lst.Add(Decimal.Parse(Node.Value))
                        End If
                    Next
                End If
            Else
                If _orgId <> 0 Then
                    FindNode = trvOrg.FindNodeByValue(_orgId.ToString)
                ElseIf trvOrg.Nodes.Count > 0 Then
                    FindNode = trvOrg.Nodes(0)
                End If

                If FindNode IsNot Nothing Then
                    lst.Add(Decimal.Parse(FindNode.Value))
                    Dim childNodes = FindNode.GetAllNodes
                    For Each Node In childNodes
                        lst.Add(Decimal.Parse(Node.Value))
                    Next
                End If

            End If

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
        Return lst
    End Function
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy all node child
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllChild() As List(Of Decimal)
        Dim lst As New List(Of Decimal)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If OrganizationType = Global.Common.OrganizationType.OrganizationLocation Then
                If trvOrganization.SelectedValue IsNot Nothing AndAlso trvOrganization.SelectedValue.Trim <> "" Then
                    lst = GetAllChild(trvOrganization.SelectedValue)
                ElseIf trvOrganization.Nodes.Count > 0 Then
                    lst = GetAllChild(trvOrganization.Nodes(0).Value)
                End If
            Else
                If trvOrg.SelectedValue IsNot Nothing AndAlso trvOrg.SelectedValue.Trim <> "" Then
                    lst = GetAllChild(trvOrg.SelectedValue)
                ElseIf trvOrg.Nodes.Count > 0 Then
                    lst = GetAllChild(trvOrg.Nodes(0).Value)
                End If
            End If
            Return lst
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Function
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy all danh sách tổ chức cha fill vào cây thư mục
    ''' </summary>
    ''' <param name="tree"></param>
    ''' <param name="list"></param>
    ''' <param name="bCheck"></param>
    ''' <remarks></remarks>
    Protected Sub BuildOrganization(ByVal tree As RadTreeView,
                                ByVal list As List(Of OrganizationDTO),
                                Optional ByVal bCheck As Boolean = False,
                                Optional ByVal cCheck As Boolean = False)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim listTemp As List(Of OrganizationDTO)
            If bCheck And cCheck Then
                listTemp = (From t In list).ToList
            ElseIf bCheck And cCheck = False Then
                If build_UYBAN = False And is_UYBAN Then
                    listTemp = (From t In list
                                Where t.ID = 1 Or (t.UY_BAN = -1 And t.UY_BAN IsNot Nothing)
                                Order By t.ORD_NO, t.NAME_VN).ToList
                Else
                    listTemp = (From t In list
                                Where t.ID = 1 Or (t.UY_BAN Is Nothing Or t.UY_BAN = 0)
                                Order By t.ORD_NO, t.NAME_VN).ToList
                End If
            ElseIf cCheck And bCheck = False Then
                listTemp = (From t In list
                            Where t.ID = 1 Or (t.DISSOLVE_DATE Is Nothing Or (t.DISSOLVE_DATE IsNot Nothing And t.DISSOLVE_DATE >= Date.Now)) _
                            And t.ACTFLG = "A" And (t.UY_BAN = -1 And t.UY_BAN IsNot Nothing)
                            Order By t.ORD_NO, t.NAME_VN).ToList
            Else
                listTemp = (From t In list
                            Where t.ID = 1 Or (t.DISSOLVE_DATE Is Nothing Or (t.DISSOLVE_DATE IsNot Nothing And t.DISSOLVE_DATE >= Date.Now)) _
                            And t.ACTFLG = "A"
                            Order By t.ORD_NO, t.NAME_VN).ToList
            End If
            If listTemp.Count = 0 Then
                Exit Sub
            End If

            'If Not bCheck Then
            '    listTemp = (From t In list
            '                Where (t.DISSOLVE_DATE Is Nothing OrElse
            '                       (t.DISSOLVE_DATE IsNot Nothing And t.DISSOLVE_DATE >= Date.Now)) _
            '                And t.ACTFLG = "A"
            '                Order By t.ORD_NO, t.NAME_VN.ToUpper).ToList
            'Else
            '    listTemp = list
            'End If
            BuildTreeNode(tree, listTemp)

            If PeriodID <> 0 Then
                SetColorPeriod(PeriodID, ColorType)
            End If
            If Bind_CheckedValueKeys IsNot Nothing Then
                If Bind_CheckedValueKeys.Count > 0 Then
                    For Each orgID In Bind_CheckedValueKeys
                        Dim node = trvOrganization.FindNodeByValue(orgID)
                        If node IsNot Nothing Then
                            node.Checked = True
                            node.ExpandParentNodes()
                        End If
                    Next
                    If Enabled = False Then
                        HadLoad = False
                    End If

                End If
            End If
            If Bind_Find_ValueKeys IsNot Nothing Then
                If Bind_Find_ValueKeys.Count > 0 Then
                    For Each orgID In Bind_Find_ValueKeys
                        Dim node = trvOrganization.FindNodeByValue(orgID)
                        If node IsNot Nothing Then
                            node.ForeColor = Drawing.Color.Brown
                            node.ExpandParentNodes()
                        End If
                    Next
                    HadLoad = False
                End If
            End If
            If IS_HadLoad = False Then
                HadLoad = IS_HadLoad
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy danh sách tổ chức con fill vào cây thư mục
    ''' </summary>
    ''' <param name="tree"></param>
    ''' <param name="list"></param>
    ''' <remarks></remarks>
    Protected Sub BuildTreeNode(ByVal tree As RadTreeView,
                                ByVal list As Object)
        Dim node As New RadTreeNode
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            tree.DataFieldID = "ID"
            tree.DataFieldParentID = "PARENT_ID"
            Select Case Common.SystemLanguage.Name
                Case "vi-VN"
                    tree.DataTextField = "NAME_VN"
                Case Else
                    tree.DataTextField = "NAME_EN"
            End Select

            tree.DataValueField = "ID"
            tree.DataSource = list
            tree.DataBind()

            If CurrentValue Is Nothing OrElse (tree.Nodes.Count > 0 AndAlso CurrentValue = tree.Nodes(0).Value) Then
                If tree.Nodes.Count > 0 Then
                    tree.Nodes(0).Selected = True
                    trvOrganization.SelectedNode.Expanded = True
                    trvOrgPostback_SelectedNodeChanged(Nothing, Nothing)
                    'If tree.Nodes(0).Nodes.Count > 0 Then
                    '    tree.Nodes(0).Nodes(0).Selected = True
                    '    trvOrgPostback_SelectedNodeChanged(Nothing, Nothing)
                    'End If
                End If
            Else
                Dim treeNode As RadTreeNode = Nothing
                For Each n As RadTreeNode In tree.Nodes
                    treeNode = getNodeSelected(n)
                    If treeNode IsNot Nothing Then
                        Exit For
                    End If
                Next
                If treeNode IsNot Nothing Then
                    treeNode.Selected = True
                    treeNode.ExpandParentNodes()

                End If
            End If

            HadLoad = True
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub
    '''<lastupdate>
    ''' 24/07/2017 10:48
    ''' </lastupdate>
    ''' <summary>
    ''' Lấy danh sách các node trên cây thư mục đã được chọn
    ''' </summary>
    ''' <param name="node"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getNodeSelected(ByVal node As RadTreeNode) As RadTreeNode
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")

        End Try
        If node.Value = CurrentCode Then
            Return node
        End If
        For Each n As RadTreeNode In node.Nodes
            If n.Value = CurrentValue Then
                Return n
            End If
            Dim _node As RadTreeNode = getNodeSelected(n)
            If _node IsNot Nothing Then
                Return _node
            End If
        Next
        Return Nothing
    End Function
    ''' <summary>
    ''' Lấy node cho của danh sách các node con
    ''' </summary>
    ''' <param name="node"></param>
    ''' <param name="lst"></param>
    ''' <remarks></remarks>
    Private Sub GetParentByNode(ByVal node As RadTreeNode, ByRef lst As List(Of String))
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If node.ParentNode IsNot Nothing Then
                Dim nodeParent = node.ParentNode
                If nodeParent.Checked = False Then
                    Dim orgID = (From p In OrganizationList Where p.ID = Decimal.Parse(nodeParent.Value) And p.IS_NOT_PER = False).FirstOrDefault
                    If orgID IsNot Nothing Then
                        lst.Add(nodeParent.Value)
                    End If

                    nodeParent.Checked = True
                    GetParentByNode(nodeParent, lst)
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            Throw ex
        End Try
    End Sub

    Private Sub chkViewCommitee_CheckedChanged(sender As Object, e As EventArgs) Handles chkViewCommitee.CheckedChanged

        Dim rep As New CommonRepository
        Dim lstOrgs As List(Of CommonBusiness.OrganizationDTO)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If OrganizationType = Global.Common.OrganizationType.OrganizationLocation Then
                BuildOrganization(trvOrganization, OrganizationList, cbDissolve.Checked, chkViewCommitee.Checked)
            End If
            trvOrgPostback_SelectedNodeChanged(Nothing, Nothing)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

End Class

Public Enum OrganizationType
    OrganizationLocation
End Enum

Public Enum PeriodType
    AT
    PA
End Enum
