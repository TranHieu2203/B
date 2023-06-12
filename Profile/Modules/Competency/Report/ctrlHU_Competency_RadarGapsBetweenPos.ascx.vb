Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports Profile.ProfileBusiness

Public Class ctrlHU_Competency_RadarGapsBetweenPos

    Inherits Common.CommonView
    Protected WithEvents ctrlFindEmployeePopup As ctrlFindEmployeePopup

#Region "Property"

    '0 - normal
    '1 - Employee
    '2 - Signer
    '3 - Salary
    Property isLoadPopup As Integer
        Get
            Return ViewState(Me.ID & "_isLoadPopup")
        End Get
        Set(ByVal value As Integer)
            ViewState(Me.ID & "_isLoadPopup") = value
        End Set
    End Property


#End Region
    Public title As String = "Năng lực chức danh hiện tại"
    Public title2 As String = "Năng lực chức danh mong muốn"
    Public name1 As String = "Năng lực tối đa"
    Public name3 As String = "Năng lực chuẩn chức danh"
    Public categories As String = ""
    Public categories2 As String = ""
    Public dataset1 As String = ""
    Public dataset3 As String = ""
    Public dataset2_1 As String = ""
    Public dataset2_3 As String = ""
#Region "Page"
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Refresh()
            UpdateControlState()
            If Not IsPostBack Then
                CreateDataFilter()
            End If
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        CreateDataFilter()
    End Sub
    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub
    Protected Function CreateDataFilter()
        If Not checkData() Then
            Exit Function
        End If
        title = "Năng lực chức danh hiện tại " & cboTitleNameCur.Text
        title2 = "Năng lực chức danh mong muốn " & cboTitleName.Text
        Dim rep As New ProfileRepository
        Dim _filter As New CompetencyStandardDTO
        Try
            If cboTitleNameCur.SelectedValue <> "" Then
                _filter.TITLE_ID = cboTitleNameCur.SelectedValue
            End If
            Dim lst = rep.GetCompetencyStandard(_filter)
            If lst.Count > 0 Then
                For Each item As CompetencyStandardDTO In lst
                    categories &= "'" & item.COMPETENCY_NAME & "',"
                    dataset1 &= "4,"
                    dataset3 &= item.LEVEL_NUMBER & ","
                Next
                categories = categories.Remove(categories.Length - 1, 1)
                dataset1 = dataset1.Remove(dataset1.Length - 1, 1)
                dataset3 = dataset3.Remove(dataset3.Length - 1, 1)
            Else
                dataset1 = ""
                dataset3 = ""
                categories = ""
            End If

            _filter.TITLE_ID = cboTitleName.SelectedValue
            lst = rep.GetCompetencyStandard(_filter)

            If lst.Count > 0 Then
                For Each item As CompetencyStandardDTO In lst
                    categories2 &= "'" & item.COMPETENCY_NAME & "',"
                    dataset2_1 &= "4,"
                    dataset2_3 &= item.LEVEL_NUMBER & ","
                Next
                categories2 = categories2.Remove(categories2.Length - 1, 1)
                dataset2_1 = dataset2_1.Remove(dataset2_1.Length - 1, 1)
                dataset2_3 = dataset2_3.Remove(dataset2_3.Length - 1, 1)
            Else
                dataset2_1 = ""
                dataset2_3 = ""
                categories2 = ""
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function checkData() As Boolean
        If cboTitleNameCur.SelectedValue = Nothing Then
            If IsPostBack Then
                ShowMessage(Translate("Bạn phải chọn chức danh hiện tại."), Utilities.NotifyType.Error)
            End If
            Return False
        End If
        If cboTitleName.SelectedValue = Nothing Then
            If IsPostBack Then
                ShowMessage(Translate("Bạn phải chọn chức danh mong muốn."), Utilities.NotifyType.Error)
            End If
            Return False
        End If
        Return True
    End Function

    Public Overrides Sub UpdateControlState()
        Dim rep As New ProfileRepository
        Try

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try

    End Sub

    Public Overrides Sub BindData()
        Dim rep As New ProfileRepository
        Dim _filter As New CompetencyAppendixDTO
        Dim tableAppendix As New DataTable
        Try
            tableAppendix = rep.GetCompetencyAppendix(_filter).ToTable()
            FillRadCombobox(cboTitleName, tableAppendix, "TITLE_NAME", "TITLE_ID")
            FillRadCombobox(cboTitleNameCur, tableAppendix, "TITLE_NAME", "TITLE_ID")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

#Region "Event"

    Private Sub ctrlFind_CancelClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles ctrlFindEmployeePopup.CancelClicked
        isLoadPopup = 0
    End Sub

#End Region

#Region "Custom"

    Private Sub UpdateToolbarState()
        Try
            ChangeToolbarState()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class