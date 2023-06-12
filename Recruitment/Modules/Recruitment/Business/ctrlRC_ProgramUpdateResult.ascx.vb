Imports Common
Imports HistaffFrameworkPublic
Imports Recruitment.RecruitmentBusiness


Public Class ctrlRC_ProgramUpdateResult
    Inherits Common.CommonView
    Private rep As New HistaffFrameworkRepository

#Region "Property"
    Public Property EmployeeID As Decimal
        Get
            Return ViewState(Me.ID & "_EmployeeID")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_EmployeeID") = value
        End Set
    End Property

    Property IDSelect As Decimal
        Get
            Return ViewState(Me.ID & "_IDSelect")
        End Get
        Set(ByVal value As Decimal)
            ViewState(Me.ID & "_IDSelect") = value
        End Set
    End Property
#End Region

#Region "Page"

    Public Overrides Sub ViewInit(ByVal e As System.EventArgs)
        InitControl()
    End Sub

    Public Overrides Sub ViewLoad(ByVal e As System.EventArgs)
        Try
            Dim rep As New RecruitmentRepository
            hidProgramID.Value = Request.Params("PROGRAM_ID")

            Dim obj = rep.GetProgramByID(New ProgramDTO With {.ID = Decimal.Parse(hidProgramID.Value)})
            lblSendDate.Text = obj.SEND_DATE.Value.ToString("dd/MM/yyyy")
            lblCode.Text = obj.CODE
            lblJobName.Text = obj.JOB_NAME
            lblOrgName.Text = obj.ORG_NAME
            txtTitleName.Text = obj.TITLE_NAME
            lblRequestNumber.Text = obj.REQUEST_NUMBER
            lblNumberHaveRecruit.Text = obj.CANDIDATE_RECEIVED
            lblRecordreceived.Text = obj.CANDIDATE_COUNT

            CurrentState = CommonMessage.STATE_NORMAL

        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

    Protected Sub InitControl()
        Try
            Me.ctrlMessageBox.Listener = Me
            CType(Me.Page, AjaxPage).AjaxManager.ClientEvents.OnRequestStart = "onRequestStart"
        Catch ex As Exception
            DisplayException(Me.ViewName, Me.ID, ex)
        End Try
    End Sub

#End Region

    Private Sub cmdExportExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExportExcel.Click
        Dim template_URL As String = String.Format("~/ReportTemplates/Recruitment/Report/Danh sach de nghi ky HĐLĐ thu viec.xls")
        Dim fileName As String = String.Format("Danh sách đề nghị ký HĐLĐ thử việc - {0}", lblJobName.Text)
        Dim _error As String = ""
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT.EXPORT_DE_NGHI_THU_VIEC", New List(Of Object)(New Object() {hidProgramID.Value}))

        Using xls As New AsposeExcelCommon
            xls.ExportExcelTemplateReport(Server.MapPath(template_URL), Server.MapPath(template_URL), fileName, ds, Response)

        End Using
    End Sub

End Class