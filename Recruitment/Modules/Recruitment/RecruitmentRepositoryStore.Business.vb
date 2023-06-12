Imports HistaffFrameworkPublic.FrameworkUtilities


Partial Class RecruitmentRepository
    'Private rep As New HistaffFrameworkRepository

#Region "Chuyển sang hồ sơ nhân viên"

    'Lấy danh sách trang thái ứng viên
    Public Function GET_STATUS_CANDIDATE() As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.GET_STATUS_CANDIDATE", New List(Of Object)(New Object() {}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)

        End If
        Return dt
    End Function


    'Lấy danh sách nhân viên xác nhận trúng tuyển
    Public Function GET_LIST_EMPLOYEE_ELECT(ByVal P_ID As Int32, ByVal P_STATUS As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.GET_LIST_EMPLOYEE_ELECT", New List(Of Object)(New Object() {P_ID, P_STATUS}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Lấy danh sách đợt tuyển dụng theo: đơn vị & year
    Public Function GET_LIST_EMPLOYEE_EXAMS(ByVal P_ID As Int32, ByVal P_EMPLOYEE_CODE As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.GET_LIST_EMPLOYEE_EXAMS", New List(Of Object)(New Object() {P_ID, P_EMPLOYEE_CODE}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Lấy danh sách nguyện vọng của nhân viên
    Public Function GET_LIST_EMPLOYEE_ASPIRATION(ByVal P_ID As Int32, ByVal P_STATUS As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.GET_LIST_EMP_ASPIRATION", New List(Of Object)(New Object() {P_ID, P_STATUS}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Cập nhật nguyện vọng
    Public Function UPDATE_ASPIRATION(ByVal P_CADIDATE_ID As Integer, ByVal P_PLACE_WORK As String, ByVal P_RECEIVE_FROM As Date?, ByVal P_RECEIVE_TO As Date?, _
                                      ByVal P_PROBATION_FROM As Date?, ByVal P_PROBATION_TO As Date?) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.UPDATE_ASPIRATION", New List(Of Object)(New Object() {P_CADIDATE_ID, _
                                      P_PLACE_WORK, P_RECEIVE_FROM, P_RECEIVE_TO, P_PROBATION_FROM, P_PROBATION_TO, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    'Cập nhật trạng thái ứng viên
    Public Function UPDATE_CANDIDATE_STATUS(ByVal P_CADIDATE_LST As String, ByVal P_STATUS_CODE As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.UPDATE_CANDIDATE_STATUS", New List(Of Object)(New Object() {P_CADIDATE_LST, P_STATUS_CODE, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function UPDATE_PROGRAM_CANDIDATE_OFFER_ID(ByVal P_CADIDATE_ID As Int32, ByVal P_PRO_ID As Int32, ByVal P_OFFER_ID As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.UPDATE_PROGRAM_CANDIDATE_OFFER_ID", New List(Of Object)(New Object() {P_CADIDATE_ID, P_PRO_ID, P_OFFER_ID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function UPDATE_PROGRAM_CANDIDATE_STATUS(ByVal P_CADIDATE_LST As String, ByVal P_STATUS_CODE As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.UPDATE_PROGRAM_CANDIDATE_STATUS", New List(Of Object)(New Object() {P_CADIDATE_LST, P_STATUS_CODE, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
    'Đếm số lượng ứng viên đã thành nhân viên
    Public Function COUNT_NUMBER_RC(ByVal P_PROGRAMID As Integer) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.COUNT_NUMBER_RC", New List(Of Object)(New Object() {P_PROGRAMID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
    'kiểm tra ứng viên đã có kết quả ở vòng cuối chưa
    Public Function CHECK_EXIST_PROGRAM_SCHEDULE_CAN(ByVal P_PROGRAMID As Integer) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.CHECK_EXIST_PROGRAM_SCHEDULE_CAN", New List(Of Object)(New Object() {P_PROGRAMID, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
    'Thư mời nhận việc
    Public Function LETTER_RECIEVE(ByVal P_ID As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.LETTER_RECIEVE2", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Xuất tờ trình ký HĐLĐ thử việc
    Public Function CONTRACT_RECIEVE(ByVal P_ID As String) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.CONTRACT_RECIEVE", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    ' Gửi mail tiếp nhân nhận LĐ
    Public Function EMAIL_RECIEVE(ByVal P_ID As Integer) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_NEW.EMAIL_CT_RECIEVE", New List(Of Object)(New Object() {P_ID}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function

    'Đẩy dữ liệu từ table ứng viên sang nhân viên
    Public Function INSERT_CADIDATE_EMPLOYEE(ByVal P_CADIDATE_LST As String, ByVal P_USER As String, ByVal P_LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.INSERT_CADIDATE_EMPLOYEE", New List(Of Object)(New Object() {P_CADIDATE_LST, P_USER, P_LOG, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function
    Public Function INSERT_CADIDATE_EMPLOYEE1(ByVal P_CADIDATE_LST As String, ByVal P_PROGRAM As Decimal, ByVal P_TRAN_CAN As Decimal, ByVal P_IS_COPY_ALLOWANCE As Decimal, ByVal P_IS_COPY_SALARY_CONTRACT As Decimal, ByVal P_USER As String, ByVal P_LOG As String) As Int32
        Dim obj As Object = rep.ExecuteStoreScalar("PKG_RECRUITMENT_NEW.INSERT_CADIDATE_EMPLOYEE1", New List(Of Object)(New Object() {P_CADIDATE_LST, P_PROGRAM, P_TRAN_CAN, P_IS_COPY_ALLOWANCE, P_IS_COPY_SALARY_CONTRACT, P_USER, P_LOG, OUT_NUMBER}))
        Return Int32.Parse(obj(0).ToString())
    End Function

    Public Function GET_SIGNER_BY_FUNC(ByVal P_FUNC_NAME As String, ByVal P_DATE As Date, Optional ByVal P_SETUP_TYPE As Decimal? = Nothing) As DataTable
        Dim dt As New DataTable
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_PROFILE_BUSINESS.GET_SIGNER_BY_FUNC", New List(Of Object)(New Object() {P_FUNC_NAME, P_DATE, P_SETUP_TYPE}))
        If Not ds Is Nothing Or Not ds.Tables(0) Is Nothing Then
            dt = ds.Tables(0)
        End If
        Return dt
    End Function
    Public Function GET_OFFERLETTER_IMPORT(ByVal P_PROGRAMID As Decimal?) As DataSet
        Try
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_EXPORT.GET_OFFERLETTER_IMPORT", New List(Of Object)(New Object() {P_PROGRAMID, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR}))
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GET_HSNV_OFFERLETTER_IMPORT(ByVal P_CANID As String) As DataSet
        Try
            Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_EXPORT.GET_HSNV_OFFERLETTER_IMPORT", New List(Of Object)(New Object() {P_CANID, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR, OUT_CURSOR}))
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function IMPORT_OFFERLETTER(ByVal P_DOCXML As String, ByVal P_USER As String) As Boolean
        Dim ds As DataSet = rep.ExecuteToDataSet("PKG_RECRUITMENT_EXPORT.IMPORT_OFFERLETTER", New List(Of Object)(New Object() {P_DOCXML, P_USER}))
        If ds IsNot Nothing AndAlso ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            Return CBool(ds.Tables(0)(0)(0))
        End If
    End Function
#End Region


End Class
