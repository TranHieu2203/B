Imports Telerik.Web.UI

Public Class ctrlCheckActionChange
    Inherits CommonView
    Public Overrides Property MustAuthorize As Boolean = False
    Dim _classPath As String = "Common/Secure/" + Me.GetType().Name.ToString()

#Region "Property"

    Property IDSelect As String
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property

#End Region

#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            GetParams()
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try

            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.MainToolBar = tbarCheckAction
            Common.BuildToolbar(MainToolBar, ToolbarItem.Cancel)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Dim rep As New CommonRepository
        Try
            If Not IsPostBack Then
                rgActiocLog.Rebind()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CreateDataFilter()
        Dim rep As New CommonRepository
        Try
            ''todo
            Dim lst = rep.GetActionLogByID(Decimal.Parse(IDSelect))
            rgActiocLog.DataSource = lst
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick

    End Sub

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgActiocLog.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgActiocLog.ItemDataBound
        Try
            If (TypeOf e.Item Is GridDataItem) Then
                Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
                For i As Integer = 0 To dataItem.Cells.Count - 1
                    Dim txt As String = dataItem.Cells(i).Text
                    If Regex.IsMatch(txt, "[0-9]{4}-[0-9]{2}-[0-9]{2}") Then
                        Dim value As DateTime
                        If DateTime.TryParse(txt, value) Then
                            dataItem.Cells(i).Text = value.ToString("dd/MM/yyyy hh:mm:ss")
                        End If
                    End If

                Next
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

#End Region

#Region "Custom"

    Private Sub GetParams()
        Try
            If Request.Params("ID") IsNot Nothing Then
                IDSelect = Request.Params("ID")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
