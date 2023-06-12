Imports Common
Imports Training.TrainingBusiness

Public Class ctrlFindPlanPopup
    Inherits CommonView
    ''' <summary>
    ''' Khai bao SalarySelected, CancelClick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Delegate Sub PlanSelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event PlanSelected As PlanSelectedDelegate
    Delegate Sub CancelClick(ByVal sender As Object, ByVal e As EventArgs)
    Event CancelClicked As CancelClick

#Region "Property"
    ''' <summary>
    ''' MustAuthorize
    ''' </summary>
    ''' <value>False</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Property MustAuthorize As Boolean = False
    Public Property PlanIDPopup As Decimal
    Public Property Year As Decimal
    Public Property OrgId As Decimal
    ''' <summary>
    ''' Trang thai Opened
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property Opened As Boolean
        Get
            Return ViewState(Me.ID & "_Opened")
        End Get
        Set(ByVal value As Boolean)
            ViewState(Me.ID & "_Opened") = value
        End Set
    End Property
    Public Property EFFECT_DATE As Date?
        Get
            Return ViewState(Me.ID & "_EFFECT_DATE")
        End Get
        Set(ByVal value As Date?)
            ViewState(Me.ID & "_EFFECT_DATE") = value
        End Set
    End Property
    ''' <summary>
    ''' EmployeeID
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EmployeeID As Decimal
        Get
            Return ViewState(Me.ID & "_EmployeeID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_EmployeeID") = value
        End Set
    End Property
    'Public Property EmployeeID As Decimal
    ' ''' <summary>
    ' ''' EmployeeIDpopup
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property EmployeeIDPopup As Decimal
    '    Get
    '        Return ViewState(Me.ID & "_EmployeeIDPopup")
    '    End Get
    '    Set(ByVal value As Decimal)
    '        ViewState(Me.ID & "_EmployeeIDPopup") = value
    '    End Set
    'End Property
    ''' <summary>
    ''' MultiSelect()
    ''' Thiet lap trang thai khong cho phep cho rad grid rgPlan chon nhieu ban ghi
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MultiSelect() As Boolean
        Get
            Return False
        End Get
        Set(ByVal value As Boolean)
            rgPlan.AllowMultiRowSelection = False
        End Set
    End Property
    ''' <summary>
    ''' Lay danh sach WorkingDTO lay tu cac column cua rad grid rgPlan duoc chon
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedPlan() As List(Of PlanDTO)
        Get
            Dim lst As New List(Of PlanDTO)
            Dim lstID As New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgPlan.SelectedItems
                Dim _new As New PlanDTO
                _new.ID = dr.GetDataKeyValue("ID")
                _new.ORG_ID = dr.GetDataKeyValue("ORG_ID")
                _new.ORG_NAME = dr.GetDataKeyValue("ORG_NAME")
                _new.TR_COURSE_ID = dr.GetDataKeyValue("TR_COURSE_ID")
                _new.TR_COURSE_NAME = dr.GetDataKeyValue("TR_COURSE_NAME")
                _new.VENUE = dr.GetDataKeyValue("VENUE")
                _new.EXPECT_TR_FROM = dr.GetDataKeyValue("EXPECT_TR_FROM")
                _new.EXPECT_TR_TO = dr.GetDataKeyValue("EXPECT_TR_TO")
                _new.STUDENT_NUMBER = dr.GetDataKeyValue("STUDENT_NUMBER")
                _new.COST_TOTAL_USD = dr.GetDataKeyValue("COST_TOTAL_USD")
                lst.Add(_new)
            Next

            Return lst
        End Get
    End Property

#End Region

#Region "Page"
    ''' <lastupdate>
    ''' 07/07/2017 9:35
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc khoi tao de loading panel
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        Dim rep As New TrainingStoreProcedure
        Dim dtData As New DataTable
        Try
            If Page.Master IsNot Nothing Then
                Page.Master.EnableViewState = True
            End If
            'dtData = rep.GET_YEARS_IN_COURSE()
            'FillRadCombobox(cboYear, dtData, "YEAR", "YEAR")
            RadAjaxPanel1.LoadingPanelID = CType(Me.Page, AjaxPage).AjaxLoading.ID

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 9:35
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi de phuong thuc hien thi cac control tren page
    ''' Thiet lap visible cho page load
    ''' Lam moi lai cac control tren page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        If IsPostBack Then
            Exit Sub
        End If

        Try
            If Opened Then
                rwMessage.VisibleOnPageLoad = True
                Refresh()
            End If
        Catch ex As Exception
            DisplayException(ViewName, ID, ex)
        End Try
    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 9:35
    ''' </lastupdate>
    ''' <summary>
    ''' Hien thi thiet lap trang thai cho rwMessage
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Show()
        Try
            'Refresh()
            Opened = True
            rwMessage.VisibleOnPageLoad = True
            rwMessage.Visible = True
        Catch ex As Exception
        End Try

    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 9:35
    ''' </lastupdate>
    ''' <summary>
    ''' An thiet lap trang thai cho rwMessage
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Hide()
        Try
            Opened = False
            rwMessage.VisibleOnPageLoad = False
            rwMessage.Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try
            rgPlan.Rebind()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "Event"
    ''' <lastupdate>
    ''' 07/07/2017 9:35
    ''' </lastupdate>
    ''' <summary>
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnYES_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYES.Click
        RaiseEvent PlanSelected(sender, e)
        Try
            Hide()
        Catch ex As Exception

        End Try

    End Sub
    ''' <lastupdate>
    ''' 07/07/2017 9:35
    ''' </lastupdate>
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNO.Click
        RaiseEvent CancelClicked(sender, e)
        Try
            Hide()
        Catch ex As Exception

        End Try
    End Sub
    'Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
    '    rgPlan.CurrentPageIndex = 0
    '    rgPlan.Rebind()
    'End Sub
    ''' <lastupdate>
    ''' 07/07/2017 9:35
    ''' </lastupdate>
    ''' <summary>
    ''' Do du lieu cho rad grid rgPlan
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgPlan_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgPlan.NeedDataSource
        Try
            Using rep As New TrainingRepository
                Dim _filter As New PlanDTO
                Dim _param = New ParamDTO With {.ORG_ID = Decimal.Parse(OrgId), _
                                                               .IS_DISSOLVE = 0}
                _filter.YEAR = Year
                Dim MaximumRows As Integer
                Dim Sorts As String = rgPlan.MasterTableView.SortExpressions.GetSortString()
                Dim lstData As List(Of PlanDTO)
                If Sorts IsNot Nothing Then
                    lstData = rep.GetPlans(_filter, rgPlan.CurrentPageIndex, rgPlan.PageSize, MaximumRows, _param, Sorts)
                Else
                    lstData = rep.GetPlans(_filter, rgPlan.CurrentPageIndex, rgPlan.PageSize, MaximumRows, _param)
                End If
                rgPlan.DataSource = lstData
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class