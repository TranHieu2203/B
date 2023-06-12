Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Public Class InsuranceRepository
    Implements IDisposable

    Private _ctx As InsuranceContext
    Private _isAvailable As Boolean
    Public ReadOnly Property Context As InsuranceContext
        Get
            If _ctx Is Nothing Then
                _ctx = New InsuranceContext
                _ctx.ContextOptions.LazyLoadingEnabled = True
            End If
            Return _ctx
        End Get
    End Property
    Public Function GetComboboxData(ByRef cbxData As ComboBoxDataDTO) As Boolean
        Try
            'Danh mục loại hợp đồng
            If cbxData.GET_CONTRACTTYPE Then
                'cbxData.LIST_CONTRACTTYPE = (From p In Context.HU_CONTRACT_TYPE
                '         Where p.ACTFLG = "A"
                '         Order By p.NAME
                '        Select New ContractTypeDTO With {
                '            .ID = p.ID,
                '            .CODE = p.CODE,
                '            .NAME = p.NAME,
                '            .BHXH = p.BHXH,
                '            .BHYT = p.BHYT,
                '            .BHTN = p.BHTN,
                '            .BHTNLD_BNN = p.BHTNLD_BNN,
                '            .PERIOD = p.PERIOD}).ToList
                Using cls As New DataAccess.QueryData
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_CONTRACTTYPE",
                                               New With {.P_CUR = cls.OUT_CURSOR})

                    cbxData.LIST_CONTRACTTYPE = (From r As DataRow In dtData.Rows
                                                 Select New ContractTypeDTO With {
                                                    .ID = Utilities.Obj2Decima(r("ID").ToString),
                                                    .CODE = r("CODE").ToString,
                                                    .NAME = r("NAME").ToString,
                                                    .BHXH = Utilities.Obj2Decima(r("BHXH").ToString),
                                                    .BHYT = Utilities.Obj2Decima(r("BHYT").ToString),
                                                    .BHTN = Utilities.Obj2Decima(r("BHTN").ToString),
                                                    .BHTNLD_BNN = Utilities.Obj2Decima(r("BHTNLD_BNN").ToString),
                                                    .PERIOD = Utilities.Obj2Decima(r("PERIOD").ToString)}).ToList
                End Using
            End If
            'Danh sách loại chế độ
            If cbxData.GET_REMIGE_TYPE Then
                cbxData.LIST_REMIGE_TYPE = (From s In (From p In Context.INS_ENTITLED_TYPE
                                                       Where p.STATUS = "A" Order By p.CREATED_DATE Descending)
                                            Select New INS_ENTITLED_TYPE_DTO With {
                                                .ID = s.ID,
                                                .NAME_VN = s.NAME_VN,
                                                .NAME_EN = s.NAME_EN}).ToList
            End If
            'Danh sách vùng
            If cbxData.GET_LOCALTION Then
                'cbxData.LIST_LOCALTION = (From p In Context.INS_REGION
                '                             Where p.ACTFLG = "A" Order By p.CREATED_DATE Descending
                '         Select New INS_REGION_DTO With {
                '             .ID = p.ID,
                '             .REGION_NAME = p.REGION_NAME}).ToList
            End If
            'Danh sách nhóm hưởng bảo hiểm
            If cbxData.GET_LIST_COST_LEVER Then
                cbxData.LIST_LIST_COST_LEVER = (From p In Context.INS_COST_FOLLOW_LEVER
                                                Where p.ACTFLG = "A" Order By p.COST_NAME Descending
                                                Select New INS_COST_FOLLOW_LEVERDTO With {
                                                    .ID = p.ID,
                                                    .COST_NAME = p.COST_NAME}).ToList
            End If
            'DANH MỤC CHỨC DANH
            If cbxData.GET_LIST_TITLE Then
                cbxData.LIST_LIST_TITLE = (From p In Context.HU_TITLE
                                           Where p.ACTFLG = "A" Order By p.NAME_VN Descending
                                           Select New HU_TitleDTO With {
                                               .ID = p.ID,
                                               .NAME_VN = p.NAME_VN}).ToList
            End If
            'DANH MỤC CẤP NHÂN SỰ
            If cbxData.GET_LIST_STAFF_RANK Then
                cbxData.LIST_LIST_STAFF_RANK = (From p In Context.HU_STAFF_RANK
                                                Where p.ACTFLG = "A" Order By p.NAME Descending
                                                Select New HU_STAFF_RANKDTO With {
                                                    .ID = p.ID,
                                                    .NAME = p.NAME}).ToList
            End If

            'Danh sách loại biến động
            If cbxData.GET_LIST_CHANGETYPE Then
                cbxData.LIST_LIST_CHANGETYPE = (From p In Context.INS_CHANGE_TYPE
                                                Where p.ACTFLG = "A" _
                                                And p.DISPLAY_COMBO = 1 _
                                                Order By p.CREATED_DATE Descending
                                                Select New INS_CHANGE_TYPEDTO With {
                                                    .ID = p.ID,
                                                    .ARISING_NAME = p.ARISING_NAME,
                                                    .ARISING_TYPE = p.ARISING_TYPE
                                                    }).ToList

            End If
            ' danh sách nhóm chế độ
            If cbxData.GET_LIST_GROUP_REGIMES Then
                cbxData.LIST_LIST_GROUP_REGIMES = (From p In Context.INS_GROUP_REGIMES
                                                   Where p.ACTFLG = "A" Order By p.REGIMES_NAME Descending
                                                   Select New INS_GROUP_REGIMESDTO With {
                                                       .ID = p.ID,
                                                       .REGIMES_NAME = p.REGIMES_NAME}).ToList

            End If

            'Danh sách nhóm biến động 
            If cbxData.GET_LIST_ARISING_TYPE Then
                cbxData.LIST_LIST_ARISING_TYPE = (From s In (From p In Context.OT_OTHER_LIST
                                                             From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID)
                                                             Where p.ACTFLG = "A" And t.CODE = "GROUP_ARISING_TYPE" Order By p.CREATED_DATE Descending)
                                                  Select New OT_OTHERLIST_DTO With {
                                                      .ID = s.p.ID,
                                                      .CODE = s.p.CODE,
                                                      .NAME_EN = s.p.NAME_EN,
                                                      .NAME_VN = s.p.NAME_VN}).ToList

            End If
            'Danh sách thành phố
            If cbxData.GET_LIST_PROVINCE Then

                Using cls As New DataAccess.QueryData
                    Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_HU_PROVINCE",
                                               New With {.P_ISBLANK = 1,
                                                         .P_CUR = cls.OUT_CURSOR})

                    cbxData.LIST_LIST_PROVINCE = (From r As DataRow In dtData.Rows
                                                  Select New HU_PROVINCEDTO With {
                                                        .id = Utilities.Obj2Decima(r("ID").ToString),
                                                        .name_vn = r("NAME").ToString
                                                  }).ToList

                    'For Each r As DataRow In dtData.Rows
                    '    cbxData.LIST_LIST_PROVINCE.Add(New HU_PROVINCEDTO With {
                    '                                        .id = Convert.ToDecimal(r("ID").ToString),
                    '                                        .name_vn = r("NAME_VN").ToString
                    '                                   })
                    'Next
                End Using

                'cbxData.LIST_LIST_PROVINCE = (From p In Context.HU_PROVINCE
                '                             Where p.ACTFLG = "A" Order By p.NAME_VN, p.CREATED_DATE Descending
                '         Select New HU_PROVINCEDTO With {
                '             .id = p.ID,
                '             .code = p.CODE,
                '             .name_en = p.NAME_EN,
                '             .name_vn = p.NAME_VN}).ToList
            End If
            ' danh sách quân huyện.
            If cbxData.GET_LIST_DISTRICT Then
                cbxData.LIST_LIST_DISTRICT = (From p In Context.HU_DISTRICT Join t In Context.HU_PROVINCE On p.PROVINCE_ID Equals t.ID
                                              Where p.ACTFLG = "A" Order By p.NAME_VN, p.CREATED_DATE Descending
                                              Select New HU_DISTRICTDTO With {
                                                  .ID = p.ID,
                                                  .name_en = p.NAME_EN,
                                                  .name_vn = p.NAME_VN,
                                                  .province_id = p.PROVINCE_ID}).ToList
            End If
            ' Danh muc noi kham chua ben
            If cbxData.GET_LIST_INS_WHEREHEALTH Then
                cbxData.LIST_LIST_INS_WHEREHEALTH = (From p In Context.INS_WHEREHEALTH
                                                     Where p.ACTFLG = "A" Order By p.CREATED_DATE Descending
                                                     Select New INS_WHEREHEALTHDTO With {
                                                         .ID = p.ID,
                                                         .NAME_VN = "[" & p.CODE & "] - " & p.NAME_VN}).ToList
            End If
            ' Tinh trang so so bhxh
            If cbxData.GET_LIST_STATUSNOBOOK Then
                cbxData.LIST_LIST_STATUSNOBOOK = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                  Where p.ACTFLG = "A" And t.CODE = "STATUS_NOBOOK" Order By p.CREATED_DATE Descending
                                                  Select New OT_OTHERLIST_DTO With {
                                                      .ID = p.ID,
                                                      .NAME_EN = p.NAME_EN,
                                                      .NAME_VN = p.NAME_VN}).ToList
            End If
            ' Tinh trang the bhyt
            If cbxData.GET_LIST_STATUSCARD Then
                cbxData.LIST_LIST_STATUSCARD = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                Where p.ACTFLG = "A" And t.CODE = "STATUS_CARD" Order By p.CREATED_DATE Descending
                                                Select New OT_OTHERLIST_DTO With {
                                                    .ID = p.ID,
                                                    .NAME_EN = p.NAME_EN,
                                                    .NAME_VN = p.NAME_VN}).ToList
            End If
            ' đơn vị đóng bảo hiểm
            If cbxData.GET_LIST_ORG_ID_INS Then
                cbxData.LIST_LIST_ORG_ID_INS = (From p In Context.OT_OTHER_LIST Where p.TYPE_ID = 2025 Order By p.CREATED_DATE Descending
                                                Select New OT_OTHERLIST_DTO With {
                                                    .ID = p.ID,
                                                    .NAME_EN = p.NAME_EN,
                                                    .NAME_VN = p.NAME_VN}).ToList
            End If
            ' Loai thay doi
            If cbxData.GET_LIST_INS_TYPE_CHANGE Then
                cbxData.LIST_LIST_INS_TYPE_CHANGE = (From p In Context.INS_TYPE_CHANGE Where p.STATUS = 1 Order By p.CREATED_DATE Descending
                                                     Select New OT_OTHERLIST_DTO With {
                                                         .ID = p.ID,
                                                         .NAME_EN = p.NAME,
                                                         .NAME_VN = p.NAME}).ToList
            End If

            If cbxData.GET_LIST_TYPE_MANUAL_LEAVE_FULL Then
                cbxData.LIST_LIST_TYPE_MANUAL_LEAVE_FULL = (From p In Context.AT_TIME_MANUAL
                                                            From F In Context.AT_FML.Where(Function(f) f.ID = p.MORNING_ID).DefaultIfEmpty
                                                            From F2 In Context.AT_FML.Where(Function(f2) f2.ID = p.AFTERNOON_ID).DefaultIfEmpty
                                                            From F3 In Context.AT_TYPE_PROCESS.Where(Function(f3) f3.ID = p.TYPE_PROSS_ID)
                                                            Where p.ACTFLG = "A" And p.INS_ID = 1 And (F.IS_LEAVE = -1 Or F2.IS_LEAVE = -1) And F3.ID = 1 Order By p.ORDERS Ascending
                                                            Select New AT_TIME_MANUALDTO With {
                                                        .ID = p.ID,
                                                        .CODE = p.CODE,
                                                        .MORNING_ID = p.MORNING_ID,
                                                        .ORDER_W = p.ORDER_W,
                                                        .IS_LEAVE = F.IS_NOTIFY,
                                                        .NOTE = F.NOTE,
                                                        .AFTERNOON_ID = p.AFTERNOON_ID,
                                                        .NAME_VN = "[" & p.CODE & "] " & p.NAME}).ToList
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".GetComboboxData")
            Return False
        Finally

        End Try
    End Function

    Public Function GetOtherList(ByVal sType As String, ByVal sLang As String, ByVal isBlank As Boolean) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_COMMON_LIST.GET_OTHER_LIST",
                                           New With {.P_TYPE = sType,
                                                     .P_ISBLANK = isBlank,
                                                     .P_LANG = sLang,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
