Imports Common
Imports Framework.UI
Imports Framework.UI.Utilities
Imports HistaffFrameworkPublic
Imports Profile.ProfileBusiness
Imports Telerik.Web.UI
Imports WebAppLog

Public Class ctrlHU_CommendCollect
    Inherits Common.CommonView
    'author: hongdx
    'EditDate: 21-06-2017
    'Content: Write log time and error
    'asign: hdx
    Dim _mylog As New MyLog()
    Dim _pathLog As String = _mylog._pathLog
    Dim _classPath As String = "Profile\Modules\Profile\Business" + Me.GetType().Name.ToString()
#Region "Property"

    ''' <summary>
    ''' Obj ProfileStoreProcedure
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property rep As New ProfileStoreProcedure

    ''' <summary>
    ''' Obj SelectedItemList
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SelectedItemList As List(Of Decimal)
        Get
            Return PageViewState(Me.ID & "_SelectedItemList")
        End Get
        Set(ByVal value As List(Of Decimal))
            PageViewState(Me.ID & "_SelectedItemList") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj dtSource
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property dtSource As DataTable
        Get
            Return PageViewState(Me.ID & "_dtSource")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtSource") = value
        End Set
    End Property

    ''' <summary>
    ''' Obj dtCommendDate
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property dtCommendDate As DataTable
        Get
            Return PageViewState(Me.ID & "_dtCommendDate")
        End Get
        Set(ByVal value As DataTable)
            PageViewState(Me.ID & "_dtCommendDate") = value
        End Set
    End Property

#End Region

#Region "Page"

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Khởi tạo, load control
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try

            Me.MainToolBar = rtbMain
            Common.Common.BuildToolbar(Me.MainToolBar,
                                       ToolbarItem.Import,
                                       ToolbarItem.Calculate)
            rtbMain.Items(0).Text = Translate("Import khen thưởng")
            rtbMain.Items(1).Text = Translate("Xử lý Xét duyệt khen thưởng")
            ctrlOrg.AutoPostBack = True
            ctrlOrg.LoadDataAfterLoaded = True
            ctrlOrg.OrganizationType = OrganizationType.OrganizationLocation

            Me.MainToolBar.OnClientButtonClicking = "OnClientButtonClicking"
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Khởi tạo, load page
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)

        Try
            'Call LoadDataGrid()
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức BindData
    ''' Gọi phương thức load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub BindData()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'txtYear.Value = Date.Now.Year
            GetDataCombo()
            'BuildColumnGroup()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phuong thuc lam moi cac control theo tuy chon Message
    ''' </summary>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")

        Try
            'Call LoadDataGrid(True)
            'ShowMessage(Translate(CommonMessage.MESSAGE_TRANSACTION_SUCCESS), NotifyType.Success)

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo colum của datagrid
    ''' Tạo ra cột Thông tin nhân viên để hiển thị
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub BuildColumnGroup()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgLoadData.MasterTableView.Columns.Clear()

            Dim columnGroup As GridColumnGroup
            columnGroup = New GridColumnGroup()
            columnGroup.HeaderText = "Thông tin nhân viên"
            columnGroup.Name = "EmployeeInfor"
            rgLoadData.MasterTableView.ColumnGroups.Add(columnGroup)
            Using rep As New ProfileRepository
                Dim commendList As List(Of CommendListDTO) = rep.GetListCommendList("A")
                For i As Integer = 0 To commendList.Count - 1
                    Dim columnGroupDtl As New GridColumnGroup
                    columnGroupDtl.HeaderText = commendList(i).NAME
                    columnGroupDtl.Name = commendList(i).CODE
                    rgLoadData.MasterTableView.ColumnGroups.Add(columnGroupDtl)
                Next
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Event"

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Event click button tìm kiếm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnFIND_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFind.Click
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Call LoadDataGrid()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Event click item trên menu toolbar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub OnToolbar_Command(ByVal sender As Object, ByVal e As RadToolBarEventArgs) Handles Me.OnMainToolbarClick
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Select Case CType(e.Item, RadToolBarButton).CommandName
                Case CommonMessage.TOOLBARITEM_IMPORT
                    Response.Redirect("Default.aspx?mid=Profile&fid=ctrlHU_ImportCommend&group=Business")
                Case CommonMessage.TOOLBARTIEM_CALCULATE
                    Response.Redirect("Default.aspx?mid=Common&fid=ctrlPROCESS&programID=COMMEND_CALCULATE&moduleID=3")
            End Select
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Kiểm tra xem khen thưởng đã được TCNS xét duyệt chưa
    ''' nếu chưa thì bôi màu đỏ 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgLoadData_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles rgLoadData.ItemDataBound
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            If TypeOf e.Item Is GridDataItem Then
                If dtSource IsNot Nothing Then
                    Using rep As New ProfileRepository
                        Dim commendList As List(Of CommendListDTO) = rep.GetListCommendList("A")
                        For i As Integer = 0 To commendList.Count - 1
                            If dtSource.Columns.Contains(commendList(i).CODE) Then
                                Dim dataItem As GridDataItem = e.Item
                                Dim myCell As TableCell = dataItem(commendList(i).CODE)
                                Dim myCellTCNS As TableCell = dataItem(commendList(i).CODE & "_TCNS")
                                If myCell.Text <> myCellTCNS.Text Then
                                    myCell.BackColor = Drawing.Color.Red
                                    myCell.ForeColor = Drawing.Color.White
                                    myCellTCNS.BackColor = Drawing.Color.Red
                                    myCellTCNS.ForeColor = Drawing.Color.White
                                End If
                            End If
                        Next
                    End Using
                End If
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Reload, Load datasource cho grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rgLoadData_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles rgLoadData.NeedDataSource
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Call LoadDataGrid(False)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

#Region "Custom"

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Phương thức xử lý việc load dữ liệu cho các combobox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetDataCombo()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Using rep1 As New ProfileRepository
                Dim lstSource = rep1.GetOtherList("COMMEND_OBJECT", False)
                FillRadCombobox(cboCommendType, lstSource, "NAME", "ID", True)
                cboCommendType.SelectedIndex = 0
            End Using
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Throw ex
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' xu ly su kien SelectedNodeChanged cua control ctrlOrg
    ''' Xet lai cac thiet lap trang thai cho grid rgLoadData
    ''' Bind lai du lieu cho rgWelfareMng
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ctrlOrg_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ctrlOrg.SelectedNodeChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            LoadDataGrid()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            ShowMessage(ex.Message, Utilities.NotifyType.Error)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện load data và hiển thị cho datagrid 
    ''' </summary>
    ''' <param name="IsDataBind"></param>
    ''' <remarks></remarks>
    Private Sub FillGridViewReview(Optional ByVal isDataBind As Boolean = True)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            rgLoadData.DataSource = dtSource
            If isDataBind Then
                rgLoadData.DataBind()
            End If
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện load data cho datagrid và gọi hàm tạo cột cho datagrid
    ''' </summary>
    ''' <param name="IsDataBind"></param>
    ''' <remarks></remarks>
    Private Sub LoadDataGrid(Optional ByVal IsDataBind As Boolean = True)
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim userName As String
            userName = UserLogHelper.GetUsername()
            If ctrlOrg.CurrentValue IsNot Nothing Then
                dtSource = rep.ReadDataCommendImported(If(ctrlOrg.CurrentValue Is Nothing, 1, ctrlOrg.CurrentValue), userName, If(cboCommendType.SelectedValue = "", 0, cboCommendType.SelectedValue), rcbbCommendDate.SelectedValue, IIf(chkIsAll.Checked = True, 1, 0))
            Else
                dtSource = Nothing
            End If

            DesginGridViewReview()
            FillGridViewReview(IsDataBind)
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            Me.DisplayException(Me.ViewName, Me.ID, ex)
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Ghi đè phương thức khởi tạo colum của datagrid
    ''' Tạo ra cột Thông tin nhân viên để hiển thị
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DesginGridViewReview()
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            'BuildColumnGroup()
            rgLoadData.MasterTableView.Columns.Clear()
            rgLoadData.MasterTableView.ColumnGroups.Clear()

            Dim columnGroup As GridColumnGroup
            columnGroup = New GridColumnGroup()
            columnGroup.HeaderText = "Thông tin nhân viên"
            columnGroup.Name = "EmployeeInfor"
            rgLoadData.MasterTableView.ColumnGroups.Add(columnGroup)

            Dim rCol As GridBoundColumn
            If cboCommendType.SelectedValue <> "" Then
                If cboCommendType.SelectedValue = 388 Then 'khen thuong? ca nhan
                    rCol = New GridBoundColumn()
                    rCol.DataField = "ID"
                    rCol.Visible = False
                    rCol.ColumnGroupName = "EmployeeInfor"
                    rgLoadData.MasterTableView.Columns.Add(rCol)


                    rCol = New GridBoundColumn()
                    rCol.DataField = "EMPLOYEE_ID"
                    rCol.HeaderText = Translate("EMPLOYEE_ID")
                    rCol.Visible = False
                    rCol.ColumnGroupName = "EmployeeInfor"
                    rgLoadData.MasterTableView.Columns.Add(rCol)

                    rCol = New GridBoundColumn()
                    rCol.DataField = "EMPLOYEE_CODE"
                    rCol.Visible = True
                    rCol.HeaderText = Translate("Mã NV")
                    rCol.HeaderStyle.Width = 50
                    rCol.EmptyDataText = "0"
                    rCol.ColumnGroupName = "EmployeeInfor"
                    rgLoadData.MasterTableView.Columns.Add(rCol)

                    rCol = New GridBoundColumn()
                    rCol.DataField = "FULLNAME_VN"
                    rCol.Visible = True
                    rCol.HeaderText = Translate("Họ tên nhân viên")
                    rCol.HeaderStyle.Width = 150
                    rCol.EmptyDataText = String.Empty
                    rCol.ColumnGroupName = "EmployeeInfor"
                    rgLoadData.MasterTableView.Columns.Add(rCol)
                End If
            End If


            rCol = New GridBoundColumn()
            rCol.DataField = "ORG_ID"
            rCol.HeaderText = Translate("ORG_ID")
            rCol.Visible = False
            rCol.HeaderStyle.Width = 80
            rCol.EmptyDataText = "0"
            rCol.ColumnGroupName = "EmployeeInfor"
            rgLoadData.MasterTableView.Columns.Add(rCol)

            rCol = New GridBoundColumn()
            rCol.DataField = "ORG_NAME"
            rCol.Visible = True
            rCol.HeaderText = Translate("Phòng ban")
            rCol.HeaderStyle.Width = 150
            rCol.EmptyDataText = String.Empty
            rCol.ColumnGroupName = "EmployeeInfor"
            rgLoadData.MasterTableView.Columns.Add(rCol)

            rCol = New GridBoundColumn()
            rCol.DataField = "DEPARMENT_NAME"
            rCol.Visible = True
            rCol.HeaderText = Translate("Bộ phận")
            rCol.HeaderStyle.Width = 150
            rCol.EmptyDataText = String.Empty
            rCol.ColumnGroupName = "EmployeeInfor"
            rgLoadData.MasterTableView.Columns.Add(rCol)

            '-----------------------------------------------------
            '-----------GROUP theo loại khen thưởng---------------
            If dtSource IsNot Nothing Then
                Using rep As New ProfileRepository
                    Dim commendList As List(Of CommendListDTO) = rep.GetListCommendList("A")
                    For i As Integer = 0 To commendList.Count - 1
                        If dtSource.Columns.Contains(commendList(i).CODE) Then
                            Dim columnGroupDtl As New GridColumnGroup
                            columnGroupDtl.HeaderText = commendList(i).NAME
                            columnGroupDtl.Name = commendList(i).CODE
                            rgLoadData.MasterTableView.ColumnGroups.Add(columnGroupDtl)

                            'TCNS sẽ thực hiện design cột TCNS theo từng loại danh hiệu
                            rCol = New GridBoundColumn()
                            rgLoadData.MasterTableView.Columns.Add(rCol)
                            rCol.DataField = commendList(i).CODE & "_TCNS"
                            rCol.Visible = True
                            rCol.HeaderText = Translate("TCNS")
                            rCol.HeaderStyle.Width = 80
                            rCol.EmptyDataText = String.Empty
                            rCol.ColumnGroupName = commendList(i).CODE

                            'Phòng ban đánh giá (đã được import trước đó)
                            rCol = New GridBoundColumn()
                            rgLoadData.MasterTableView.Columns.Add(rCol)
                            rCol.DataField = commendList(i).CODE
                            rCol.Visible = True
                            rCol.HeaderText = Translate("Phòng ban")
                            rCol.HeaderStyle.Width = 80
                            rCol.EmptyDataText = String.Empty
                            rCol.ColumnGroupName = commendList(i).CODE
                        End If
                    Next

                End Using
            End If


            'Điều kiện xét thưởng
            'Dim columnGroupDK As New GridColumnGroup()
            'rgLoadData.MasterTableView.ColumnGroups.Add(columnGroupDK)
            'columnGroupDK.HeaderText = "Điều kiện xét thưởng"
            'columnGroupDK.Name = "CONDITION"

            'rCol = New GridBoundColumn()
            'rgLoadData.MasterTableView.Columns.Add(rCol)
            'rCol.DataField = "DISCIPLINE_TIMES"
            'rCol.HeaderText = Translate("Bị kỷ luật")
            'rCol.Visible = True
            'rCol.HeaderStyle.Width = 80
            'rCol.EmptyDataText = ""
            'rCol.ColumnGroupName = "CONDITION"

            'rCol = New GridBoundColumn()
            'rgLoadData.MasterTableView.Columns.Add(rCol)
            'rCol.DataField = "WORK_DAYS"
            'rCol.HeaderText = Translate("Số ngày làm việc trong năm")
            'rCol.Visible = True
            'rCol.HeaderStyle.Width = 130
            'rCol.EmptyDataText = ""
            'rCol.ColumnGroupName = "CONDITION"

            rgLoadData.SetFilter()
            'rgLoadData.DataBind()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try

    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged cho rcbbCommendDate
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub rcbbCommendDate_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles rcbbCommendDate.SelectedIndexChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Call LoadDataGrid()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _mylog.WriteLog(_mylog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

    ''' <lastupdate>
    ''' 12/07/2017 10:00
    ''' </lastupdate>
    ''' <summary>
    ''' Xử lý sự kiện SelectedIndexChanged cho cboCommendType
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cboCommendType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cboCommendType.SelectedIndexChanged
        Dim startTime As DateTime = DateTime.UtcNow
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim userName As String
            userName = UserLogHelper.GetUsername()
            dtCommendDate = rep.ReadListCommendDate(If(ctrlOrg.CurrentValue Is Nothing, 1, ctrlOrg.CurrentValue), userName, cboCommendType.SelectedValue)
            If dtCommendDate IsNot Nothing Then
                FillRadCombobox(rcbbCommendDate, dtCommendDate, "COMMEND_DATE", "COMMEND_DATE", False)
                rcbbCommendDate.SelectedIndex = -1
                rcbbCommendDate.Text = ""
            End If
            LoadDataGrid()
            _mylog.WriteLog(_mylog._info, _classPath, method, CLng(DateTime.UtcNow.Subtract(startTime).TotalSeconds).ToString(), Nothing, "")
        Catch ex As Exception
            _myLog.WriteLog(_myLog._error, _classPath, method, 0, ex, "")
        End Try
    End Sub

#End Region

End Class