Imports Common
Imports Framework.UI
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI

Public Class ctrlHU_CommendCollectDetail
    Inherits Common.CommonView
    Dim psp As New ProfileStoreProcedure
#Region "Property"

    Public Property Commends As List(Of CommendDTO)
        Get
            Return ViewState(Me.ID & "_Commends")
        End Get
        Set(ByVal value As List(Of CommendDTO))
            ViewState(Me.ID & "_Commends") = value
        End Set
    End Property

    Public Property empCode As String
        Get
            Return ViewState(Me.ID & "_empCode")
        End Get
        Set(ByVal value As String)
            ViewState(Me.ID & "_empCode") = value
        End Set
    End Property

    Public Property orgID As Decimal
        Get
            Return ViewState(Me.ID & "_orgID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_orgID") = value
        End Set
    End Property

#End Region

#Region "Page"

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Try
            rgCommend.AllowCustomPaging = True
            rgCommend.PageSize = Common.Common.DefaultPageSize
            rgCommend.SetFilter()
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            InitControl()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub BindData()
        Try
            getParams()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Protected Sub RadGrid_NeedDataSource(ByVal source As Object, ByVal e As GridNeedDataSourceEventArgs) Handles rgCommend.NeedDataSource
        Try
            CreateDataFilter()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Private Sub RadGrid_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles rgCommend.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim datarow As GridDataItem = DirectCast(e.Item, GridDataItem)
            datarow("ORG_NAME").ToolTip = Utilities.DrawTreeByString(datarow("ORG_DESC").Text)
        End If
    End Sub

#End Region

#Region "Custom"

    Protected Function CreateDataFilter(Optional ByVal isFull As Boolean = False) As DataTable
        Dim _filter As New CommendDTO
        _filter.param = New ParamDTO
        Dim rep As New ProfileBusinessRepository
        Dim bCheck As Boolean = False
        Try
            _filter.param.ORG_ID = Utilities.ObjToDecima(orgID)
            _filter.param.IS_DISSOLVE = False
            _filter.EMPLOYEE_CODE = empCode
            _filter.EFFECT_DATE = Date.Now.Date.AddYears(-5)
            _filter.EXPIRE_DATE = Date.Now.Date
            _filter.COMMEND_OBJ = 388 'Hash code : khen thưởng cá nhân. CHơi lầy lun

            Dim MaximumRows As Integer
            Dim Sorts As String = "EFFECT_DATE DESC, ORDER"
            If isFull Then
                If Sorts IsNot Nothing Then
                    Return rep.GetCommend(_filter, Sorts).ToTable()
                Else
                    Return rep.GetCommend(_filter).ToTable()
                End If
            Else
                If Sorts IsNot Nothing Then
                    Me.Commends = rep.GetCommend(_filter, rgCommend.CurrentPageIndex, rgCommend.PageSize, MaximumRows, Sorts)
                Else
                    Me.Commends = rep.GetCommend(_filter, rgCommend.CurrentPageIndex, rgCommend.PageSize, MaximumRows)
                End If
                rgCommend.VirtualItemCount = MaximumRows
                rgCommend.DataSource = Me.Commends
            End If
            rep.Dispose()
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Sub getParams()
        Try
            If Request.Params("EmpCode") IsNot Nothing Then
                empCode = Request.Params("EmpCode")
                orgID = Decimal.Parse(Request.Params("OrgID").ToString())
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class