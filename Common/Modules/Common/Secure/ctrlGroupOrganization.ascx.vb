Imports Common.CommonBusiness
Imports Framework.UI
Imports Telerik.Web.UI

Public Class ctrlGroupOrganization
    Inherits CommonView
    Public Property GroupInfo As GroupDTO

#Region "Property"

    Public Property GroupOganization As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_GroupOganization") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_GroupOganization")
        End Get
    End Property

    Public Property GroupOganizationFunction As List(Of Decimal)
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_GroupOganizationFunction") = value
        End Set
        Get
            Return PageViewState(Me.ID & "_GroupOganizationFunction")
        End Get
    End Property

    Public Property GroupID_Old As Decimal
        Get
            Return PageViewState(Me.ID & "_GroupID_Old")
        End Get
        Set(ByVal value As Decimal)
            PageViewState(Me.ID & "_GroupID_Old") = value
        End Set
    End Property
    ''' <summary>
    ''' lstOrgID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstOrgID As List(Of CommonBusiness.GroupOrgAccessDTO)
        Get
            Return PageViewState(Me.ID & "_lstOrgID")
        End Get
        Set(ByVal value As List(Of CommonBusiness.GroupOrgAccessDTO))
            PageViewState(Me.ID & "_lstOrgID") = value
        End Set
    End Property
    ''' <summary>
    ''' lstOrgIDFirst
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstOrgIDFirst As List(Of CommonBusiness.GroupOrgAccessDTO)
        Get
            Return PageViewState(Me.ID & "_lstOrgIDFirst")
        End Get
        Set(ByVal value As List(Of CommonBusiness.GroupOrgAccessDTO))
            PageViewState(Me.ID & "_lstOrgIDFirst") = value
        End Set
    End Property
    ''' <summary>
    ''' lstOrgIDFirst
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property lstOrgIDExpect As List(Of CommonBusiness.GroupOrgAccessDTO)
        Get
            Return PageViewState(Me.ID & "_lstOrgIDExpect")
        End Get
        Set(ByVal value As List(Of CommonBusiness.GroupOrgAccessDTO))
            PageViewState(Me.ID & "_lstOrgIDExpect") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            Me.MainToolBar = tbarMain
            Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Edit,
                                        ToolbarItem.Seperator,
                                        ToolbarItem.Save,
                                        ToolbarItem.Cancel)
            orgLoca.AutoPostBack = False
            orgLoca.CheckBoxes = TreeNodeTypes.All
            orgLoca.CheckChildNodes = True
            orgLoca.CheckParentNodes = False
            orgLoca.ShowCommitee = True
            orgLoca.is_UYBAN = True
            orgLoca.build_UYBAN = True
            Refresh("ViewFirst")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            orgLoca.SetProperty("GroupOf", "1")
            UpdateControlStatus()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub UpdateControlStatus()
        If CurrentState = CommonMessage.STATE_EDIT Then
            Me.MainToolBar.Items(0).Enabled = False

            Me.MainToolBar.Items(2).Enabled = True
            Me.MainToolBar.Items(3).Enabled = True

            orgLoca.Enabled = True
        Else
            If GroupInfo IsNot Nothing Then
                Me.MainToolBar.Items(0).Enabled = True
            Else
                Me.MainToolBar.Items(0).Enabled = False
            End If
            Me.MainToolBar.Items(2).Enabled = False
            Me.MainToolBar.Items(3).Enabled = False

            orgLoca.Enabled = False
        End If
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            If GroupInfo Is Nothing Then Exit Sub

            If GroupID_Old = Nothing Or GroupID_Old <> GroupInfo.ID Or
                Message = CommonMessage.ACTION_SAVED Or GroupOganization Is Nothing Then
                Dim rep As New CommonRepository
                GroupOganization = rep.GetGroupOrganization(GroupInfo.ID)
            End If
            If Message = "ViewFirst" Then
                lstOrgIDFirst = New List(Of GroupOrgAccessDTO)
                For i = 0 To GroupOganization.Count - 1
                    If lstOrgIDFirst IsNot Nothing AndAlso lstOrgIDFirst.Where(Function(f) f.ORG_ID = Decimal.Parse(GroupOganization(i))).Count > 0 Then
                    Else
                        lstOrgIDFirst.Add(New CommonBusiness.GroupOrgAccessDTO() With {.GROUP_ID = GroupInfo.ID,
                                                                                 .ORG_ID = Decimal.Parse(GroupOganization(i))})
                    End If
                Next
            End If
            If GroupOganization IsNot Nothing Then
                lstOrgID = New List(Of GroupOrgAccessDTO)
                For i = 0 To GroupOganization.Count - 1
                    If lstOrgID IsNot Nothing AndAlso lstOrgID.Where(Function(f) f.ORG_ID = Decimal.Parse(GroupOganization(i))).Count > 0 Then
                    Else
                        lstOrgID.Add(New CommonBusiness.GroupOrgAccessDTO() With {.GROUP_ID = GroupInfo.ID,
                                                                                 .ORG_ID = Decimal.Parse(GroupOganization(i))})
                    End If
                Next
            End If
            'Đưa dữ liệu vào Grid
            orgLoca.CheckedValueKeys = GroupOganization
            orgLoca.Bind_CheckedValueKeys = GroupOganization
            'Thay đổi trạng thái các control
            UpdateControlStatus()

            GroupID_Old = GroupInfo.ID
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Event"

    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim rep As New CommonRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_EDIT
                    CurrentState = CommonMessage.STATE_EDIT
                    'Thay đổi trạng thái các control
                    UpdateControlStatus()
                    'Gửi thông điệp cho Parent View
                    Me.Send(CommonMessage.ACTION_UPDATING)
                Case CommonMessage.TOOLBARITEM_SAVE
                    If CurrentState = CommonMessage.STATE_EDIT Then
                        Dim lst As List(Of CommonBusiness.GroupOrgAccessDTO)
                        'Dim lstFunc As List(Of CommonBusiness.SE_GROUP_ORG_FUN_ACCESS)
                        lst = GetListOrgID()
                        'lstFunc = GetListOrgFuncID()                        
                        If lst.Count = 0 Then
                            If lstOrgID Is Nothing Then
                                ShowMessage(Translate(CommonMessage.MESSAGE_NOT_SELECT_ORG), Utilities.NotifyType.Warning)
                                Exit Sub
                            End If
                        End If
                        rep.DeleteGroupOrganization(GroupInfo.ID)
                        If rep.UpdateGroupOrganization(lst) Then
                            Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), Utilities.NotifyType.Success)
                            Refresh(CommonMessage.ACTION_SAVED)

                        Else
                            Me.ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_FAIL), Utilities.NotifyType.Warning)
                        End If
                    End If
                    CurrentState = CommonMessage.STATE_NORMAL
                    'Thay đổi trạng thái các control
                    UpdateControlStatus()
                    'Gửi thông điệp cho Parent View
                    Me.Send(CommonMessage.ACTION_UPDATED)
                Case CommonMessage.TOOLBARITEM_CANCEL
                    CurrentState = CommonMessage.STATE_NORMAL
                    'Thay đổi trạng thái các control
                    UpdateControlStatus()
                    Refresh()
                    'Gửi thông điệp cho Parent View
                    Me.Send(CommonMessage.ACTION_UPDATED)
            End Select
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
#End Region

#Region "Custom"

    Protected Function GetListOrgID() As List(Of CommonBusiness.GroupOrgAccessDTO)
        Dim lst As New List(Of CommonBusiness.GroupOrgAccessDTO)
        If GroupID_Old <> GroupInfo.ID Then
            lstOrgID = New List(Of CommonBusiness.GroupOrgAccessDTO)
        End If
        Dim lstOrg = orgLoca.CheckedValueGroups
        Dim orgList As List(Of Decimal) = orgLoca.GetAllOrgID()
        For i = 0 To lstOrg.Count - 1
            lst.Add(New CommonBusiness.GroupOrgAccessDTO() With {.GROUP_ID = GroupInfo.ID,
                                                                        .ORG_ID = Decimal.Parse(lstOrg(i))})
        Next
        Dim query = orgList.Except(lstOrg.ConvertAll(Of Decimal)(Function(i As Integer) i)).ToList
        If lstOrgID Is Nothing Then
            lstOrgID = lstOrgIDFirst
        End If
        Dim query2 = lstOrgID.Union(lst).ToList.Select(Function(o) o.ORG_ID).ToList.Except(query).ToList
        lstOrgID = (From p In query2 Select New CommonBusiness.GroupOrgAccessDTO() With {.GROUP_ID = GroupInfo.ID,
                                                                                           .ORG_ID = p}).Distinct.ToList
        Return lstOrgID
    End Function

#End Region

End Class