Imports Common
Imports Profile.ProfileBusiness
Public Class ctrlEmpBasicInfo
    Inherits CommonView
    Dim employeeCode As String
    Public Overrides Property MustAuthorize As Boolean = False
#Region "Properties"
    Property EmployeeInfo As EmployeeDTO
        Get
            Return PageViewState(Me.ID & "_EmployeeInfo")
        End Get
        Set(ByVal value As EmployeeDTO)
            PageViewState(Me.ID & "_EmployeeInfo") = value
        End Set
    End Property

#End Region

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        LoadEmployeeInfo()
        UpdateControlState()
    End Sub

    ''' <summary>
    ''' Lấy thông tin chi tiết của nhân viên từ EmployeeCode
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadEmployeeInfo()
        Try
            If EmployeeInfo IsNot Nothing Then
                txtEmployeeCODE1.Text = EmployeeInfo.EMPLOYEE_CODE
                txtFullName.Text = EmployeeInfo.FULLNAME_VN
                hidID.Value = EmployeeInfo.ID.ToString()
                If EmployeeInfo.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID Then
                    hidIsTer.Value = -1
                End If
                GetParams()
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub GetParams()
        Dim method As String = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString()
        Try
            Dim startTime As DateTime = DateTime.UtcNow
            If Request.Params("Place") IsNot Nothing Then
                Dim ctrl = ""
                Dim fid = ""
                btnAdd.Visible = True
                Dim user = LogHelper.CurrentUser
                Using rep As New CommonRepository
                    Dim GroupAdmin As Boolean = rep.CheckGroupAdmin(Common.Common.GetUsername)
                    Dim permissions As List(Of CommonBusiness.PermissionDTO) = rep.GetUserPermissions(Common.Common.GetUsername)
                    If Request.Params("Place").Trim = "ctrlHU_EmpDtlWorking" Then
                        ctrl = "ctrlHU_ChangeInfoNewEdit"
                        fid = "ctrlHU_EmpDtlWorking"
                    ElseIf Request.Params("Place").Trim = "ctrlHU_EmpDtlWorkingBefore" Then
                        ctrl = "ctrlHU_WorkInfoNewEdit"
                        fid = "ctrlHU_EmpDtlWorkingBefore"
                    ElseIf Request.Params("Place").Trim = "ctrlHU_EmpDtlSalary" Then
                        ctrl = "ctrlHU_WageNewEdit"
                        fid = "ctrlHU_EmpDtlSalary"
                    ElseIf Request.Params("Place").Trim = "ctrlHU_EmpDtlContract" Then
                        ctrl = "ctrlHU_ContractNewEdit"
                        fid = "ctrlHU_EmpDtlContract"
                    ElseIf Request.Params("Place").Trim = "ctrlHU_EmpDtlTrainingOutCompany" Then
                        ctrl = "ctrlHU_CertificateNewEdit"
                        fid = "ctrlHU_EmpDtlTrainingOutCompany"
                    ElseIf Request.Params("Place").Trim = "ctrlHU_EmpDtlCommend" Then
                        ctrl = ""
                        btnAdd.Visible = False
                    ElseIf Request.Params("Place").Trim = "ctrlHU_EmpDtlDiscipline" Then
                        ctrl = ""
                        btnAdd.Visible = False
                    ElseIf Request.Params("Place").Trim = "ctrlHU_EmpDtlFamily" Then
                        ctrl = "ctrlHU_FamilytNewEdit"
                        fid = "ctrlHU_EmpDtlFamily"
                    ElseIf Request.Params("Place").Trim = "ctrlHU_EmpDtlConcurrently" Then
                        ctrl = "ctrlHU_ConcurrentlyNewEdit"
                        fid = "ctrlHU_EmpDtlConcurrently"
                        If EmployeeInfo.EMP_STATUS_NAME = "Kiêm nhiệm" Then
                            btnAdd.Enabled = False
                        End If
                    ElseIf Request.Params("Place").Trim = "ctrlHU_EmpDtl_HU_Allowance" Then
                        ctrl = "ctrlHU_Allowance"
                        fid = "ctrlHU_EmpDtl_HU_Allowance"
                    ElseIf Request.Params("Place").Trim = "ctrlHU_EmpDtlAppendix" Then
                        ctrl = "ctrlHU_ContractTemplete"
                        fid = "ctrlHU_EmpDtlAppendix"
                    ElseIf Request.Params("Place").Trim = "ctrlHU_EmpDtlComitee" Then
                        ctrl = "ctrlHU_Commitee"
                        fid = "ctrlHU_EmpDtlComitee"
                    Else
                        ctrl = ""
                    End If
                    hid_ctrl.Value = "/Dialog.aspx?mid=Profile&fid=" + ctrl + "&group=Business&empID=" + hidID.Value + "&Is_dis=dis_emp&kind=isNewEdit"

                    If GroupAdmin = False Then
                        If permissions IsNot Nothing Then
                            Dim isPermissions = (From p In permissions Where p.FID = fid And p.IS_REPORT = False And p.AllowCreate <> 0).Any
                            If Not isPermissions Then
                                btnAdd.Visible = False
                            End If
                        Else
                            btnAdd.Visible = False
                        End If
                    Else
                        If user.MODULE_ADMIN = "*" OrElse (user.MODULE_ADMIN IsNot Nothing AndAlso user.MODULE_ADMIN.Contains(Me.ModuleName)) Then
                        Else
                            btnAdd.Visible = False
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Overrides Sub Refresh(Optional ByVal Message As String = "")
        UpdateControlState()
    End Sub
End Class