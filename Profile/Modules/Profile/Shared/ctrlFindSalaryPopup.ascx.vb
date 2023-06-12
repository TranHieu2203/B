Imports Common
Imports HistaffWebAppResources.My.Resources
Imports Profile.ProfileBusiness
Public Class ctrlFindSalaryPopup
    Inherits CommonView
    ''' <summary>
    ''' Khai bao SalarySelected, CancelClick
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Delegate Sub SalarySelectedDelegate(ByVal sender As Object, ByVal e As EventArgs)
    Event SalarySelected As SalarySelectedDelegate
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
    Public Property EmployeeIDPopup As Decimal
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
    ''' Thiet lap trang thai khong cho phep cho rad grid rgSalary chon nhieu ban ghi
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MultiSelect() As Boolean
        Get
            Return False
        End Get
        Set(ByVal value As Boolean)
            rgSalary.AllowMultiRowSelection = False
        End Set
    End Property
    ''' <summary>
    ''' Lay danh sach WorkingDTO lay tu cac column cua rad grid rgSalary duoc chon
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedSalary() As List(Of WorkingDTO)
        Get
            Dim lst As New List(Of WorkingDTO)
            Dim lstID As New List(Of Decimal)
            For Each dr As Telerik.Web.UI.GridDataItem In rgSalary.SelectedItems
                Dim _new As New WorkingDTO
                _new.ID = dr.GetDataKeyValue("ID")
                _new.EFFECT_DATE = dr.GetDataKeyValue("EFFECT_DATE")
                _new.DECISION_NO = dr.GetDataKeyValue("DECISION_NO")
                _new.EXPIRE_DATE = dr.GetDataKeyValue("EXPIRE_DATE")
                _new.SAL_GROUP_NAME = dr.GetDataKeyValue("SAL_GROUP_NAME")
                _new.SAL_LEVEL_NAME = dr.GetDataKeyValue("SAL_LEVEL_NAME")
                _new.SAL_RANK_NAME = dr.GetDataKeyValue("SAL_RANK_NAME")
                _new.SAL_BASIC = dr.GetDataKeyValue("SAL_BASIC")
                _new.PERCENT_SALARY = dr.GetDataKeyValue("PERCENT_SALARY")
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
        Try
            If Page.Master IsNot Nothing Then
                Page.Master.EnableViewState = True
            End If
            RadAjaxPanel1.LoadingPanelID = CType(Me.Page, AjaxPage).AjaxLoading.ID
            rgSalary.MasterTableView.GetColumn("SAL_TYPE_NAME").HeaderText = UI.Wage_WageGRoup
            'rgSalary.MasterTableView.GetColumn("SAL_BASIC").HeaderText = UI.Wage_BasicSalary
            rgSalary.MasterTableView.GetColumn("TAX_TABLE_Name").HeaderText = UI.Wage_TaxTable
            'rgSalary.MasterTableView.GetColumn("SAL_INS").HeaderText = UI.Wage_Sal_Ins

            rgSalary.MasterTableView.GetColumn("GAS_SAL").HeaderText = "Xăng xe"
            rgSalary.MasterTableView.GetColumn("PHONE_SAL").HeaderText = "Điện thoại"
            rgSalary.MasterTableView.GetColumn("ADDITIONAL_SAL").HeaderText = "Khoản bổ sung"
            rgSalary.MasterTableView.GetColumn("OTHERSALARY1").HeaderText = "Thưởng HQCLCV"

            rgSalary.MasterTableView.GetColumn("PC1").HeaderText = "PC cơm"
            rgSalary.MasterTableView.GetColumn("PC2").HeaderText = "PC hỗ trợ nhà ở"
            rgSalary.MasterTableView.GetColumn("PC3").HeaderText = "PC đồng phục"
            rgSalary.MasterTableView.GetColumn("PC4").HeaderText = "PC đi lại"
            rgSalary.MasterTableView.GetColumn("PC5").HeaderText = "Thưởng chuyên cần"

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
            rgSalary.Rebind()
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
        RaiseEvent SalarySelected(sender, e)
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
    ''' <lastupdate>
    ''' 07/07/2017 9:35
    ''' </lastupdate>
    ''' <summary>
    ''' Do du lieu cho rad grid rgSalary
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgSalary_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgSalary.NeedDataSource
        Try
            Using rep As New ProfileBusinessRepository
                Dim _param = New ParamDTO With {.ORG_ID = 1,
                                                .IS_DISSOLVE = True}
                If (EmployeeID <> 0) Then
                    rgSalary.DataSource = rep.GetWorking_1(New WorkingDTO With {.EMPLOYEE_ID = EmployeeID,
                                                                                .EFFECT_DATE = EFFECT_DATE,
                                                                          .IS_WAGE = -1}, _param, "EFFECT_DATE desc")
                End If
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class