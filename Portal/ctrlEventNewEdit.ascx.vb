Imports Common
Imports Portal.PortalBusiness
Imports Telerik.Web.UI

Public Class ctrlEventNewEdit
    Inherits CommonView
#Region "Property"
    Property EventInfo As EventDTO
        Get
            Return PageViewState(Me.ID & "_EventInfo")
        End Get
        Set(ByVal value As EventDTO)
            PageViewState(Me.ID & "_EventInfo") = value
        End Set
    End Property
#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        InitControl()
    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar, ToolbarItem.Save,
                                       ToolbarItem.Cancel)
            CType(Me.MainToolBar.Items(0), RadToolBarButton).CausesValidation = True
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try

            Select Case CurrentState
                Case CommonMessage.STATE_EDIT
                    If EventInfo IsNot Nothing Then
                        rtTITLE.Text = EventInfo.TITLE
                        rEditor.Content = EventInfo.DETAIL
                        hidID.Value = EventInfo.ID.ToString
                    End If
                Case CommonMessage.STATE_NEW, ""
                    rtTITLE.Text = ""
                    rEditor.Content = ""
            End Select
            rtTITLE.Focus()
        Catch ex As Exception
            Throw ex
        End Try
        MyBase.Refresh(Message)
    End Sub
#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim objUser As New EventDTO
        Dim rep As New PortalRepository
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_SAVE
                    If Page.IsValid Then
                        objUser.TITLE = rtTITLE.Text.Trim
                        objUser.DETAIL = rEditor.Content
                        objUser.ADD_TIME = DateTime.Now
                        Select Case CurrentState
                            Case CommonMessage.STATE_NEW
                                objUser.IS_SHOW = False
                                If rep.InsertEventInformation(objUser) Then
                                    Me.Send(CommonMessage.ACTION_SUCCESS)
                                Else
                                    Me.Send(CommonMessage.ACTION_UNSUCCESS)
                                End If
                            Case CommonMessage.STATE_EDIT
                                objUser.ID = Decimal.Parse(hidID.Value)
                                objUser.IS_SHOW = EventInfo.IS_SHOW
                                If rep.UpdateEventInformation(objUser) Then
                                    Me.Send(CommonMessage.ACTION_SUCCESS)
                                Else
                                    Me.Send(CommonMessage.ACTION_UNSUCCESS)
                                End If
                        End Select
                    End If

                Case CommonMessage.TOOLBARITEM_CANCEL
                    Me.Send(CommonMessage.ACTION_CANCEL)
            End Select
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub
    Private Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        Try
            args.IsValid = True

        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region "Custom"

#End Region

End Class