Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class ProfileRepository

#Region "AssetMng"

    Public Function GetAssetMng(ByVal _filter As AssetMngDTO,
                                        ByVal _param As ParamDTO,
                                        ByVal PageIndex As Integer,
                                        ByVal PageSize As Integer,
                                        ByRef Total As Integer,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc",
                                        Optional ByVal log As UserLog = Nothing) As List(Of AssetMngDTO)

        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using

            Dim query = (From p In Context.HU_ASSET_MNG
                         From d In Context.HU_ASSET.Where(Function(d) d.ID = p.ASSET_DECLARE_ID)
                         From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = p.EMPLOYEE_ID)
                         From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID)
                         From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID)
                         From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And _
                                                                        f.USERNAME = log.Username.ToUpper)
                         From staffrank In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                         From assetgroup In Context.OT_OTHER_LIST.Where(Function(f) f.ID = d.GROUP_ID And _
                                                                       f.TYPE_ID = ProfileCommon.OT_ASSET_GROUP.TYPE_ID).DefaultIfEmpty
                         From assetstatus In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID And _
                                                                       f.TYPE_ID = ProfileCommon.OT_ASSET_STATUS.TYPE_ID).DefaultIfEmpty
                         Order By d.CODE)
            'query = query.Where(Function(p) _filter.ORG_IDS.Contains(p.e.ORG_ID))


            Dim asset = query.Select(Function(p) New AssetMngDTO With {
                                         .ID = p.p.ID,
                                         .ASSET_CODE = p.d.CODE,
                                         .ASSET_NAME = p.d.NAME,
                                         .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                         .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                         .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                         .ORG_ID = p.e.ORG_ID,
                                         .ORG_NAME = p.o.NAME_VN,
                                         .ORG_DESC = p.o.DESCRIPTION_PATH,
                                         .ASSET_VALUE = p.p.ASSET_VALUE,
                                         .ASSET_GROUP_CODE = p.assetgroup.CODE,
                                         .ASSET_GROUP_NAME = p.assetgroup.NAME_VN,
                                         .DEPOSITS = p.p.DEPOSITS,
                                         .DESC = p.p.SDESC,
                                         .ISSUE_DATE = p.p.ISSUE_DATE,
                                         .RETURN_DATE = p.p.RETURN_DATE,
                                         .WORK_STATUS = p.e.WORK_STATUS,
                                         .TER_LAST_DATE = p.e.TER_EFFECT_DATE,
                                         .TITLE_NAME = p.t.NAME_VN,
                                         .STAFF_RANK_NAME = p.staffrank.NAME,
                                         .ORG_TRANFER = p.p.ORG_TRANFER,
                                         .ORG_RECEIVE = p.p.ORG_RECEIVE,
                                         .ASSET_BARCODE = p.p.ASSET_BARCODE,
                                         .ASSET_SERIAL = p.p.ASSET_SERIAL,
                                         .STATUS_ID = p.p.STATUS_ID,
                                         .STATUS_NAME = p.assetstatus.NAME_VN,
                                         .CREATED_DATE = p.p.CREATED_DATE})

            Dim dateNow = Date.Now.Date
            If Not _filter.IS_TERMINATE Then
                asset = asset.Where(Function(f) f.WORK_STATUS <> 257 Or (f.WORK_STATUS = 257 And f.TER_LAST_DATE >= dateNow) Or f.WORK_STATUS Is Nothing)
            End If

            If _filter.FROM_DATE_SEARCH.HasValue Then
                asset = asset.Where(Function(f) f.ISSUE_DATE >= _filter.FROM_DATE_SEARCH)
            End If
            If _filter.TO_DATE_SEARCH.HasValue Then
                asset = asset.Where(Function(f) f.ISSUE_DATE <= _filter.TO_DATE_SEARCH)
            End If

            If _filter.EMPLOYEE_CODE IsNot Nothing Then
                asset = asset.Where(Function(p) p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If Not String.IsNullOrEmpty(_filter.EMPLOYEE_NAME) Then
                asset = asset.Where(Function(f) f.EMPLOYEE_NAME.ToLower().Contains(_filter.EMPLOYEE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TITLE_NAME) Then
                asset = asset.Where(Function(f) f.TITLE_NAME.ToLower().Contains(_filter.TITLE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ASSET_CODE) Then
                asset = asset.Where(Function(f) f.ASSET_CODE.ToLower().Contains(_filter.ASSET_CODE.ToLower()))
            End If
            If _filter.ISSUE_DATE.HasValue Then
                asset = asset.Where(Function(f) f.ISSUE_DATE.Value = _filter.ISSUE_DATE)
            End If
            If _filter.RETURN_DATE.HasValue Then
                asset = asset.Where(Function(f) f.RETURN_DATE.Value = _filter.RETURN_DATE)
            End If
            If _filter.UNIT_PRICE.HasValue Then
                asset = asset.Where(Function(f) f.UNIT_PRICE.Value = _filter.UNIT_PRICE)
            End If
            If Not String.IsNullOrEmpty(_filter.STATUS_NAME) Then
                asset = asset.Where(Function(f) f.STATUS_NAME.ToLower().Contains(_filter.STATUS_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.DESC) Then
                asset = asset.Where(Function(f) f.DESC.ToLower().Contains(_filter.DESC.ToLower()))
            End If

            asset = asset.OrderBy(Sorts)
            Total = asset.Count
            asset = asset.Skip(PageIndex * PageSize).Take(PageSize)
            ''Logger.LogInfo(asset)
            Return asset.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetAssetMngById(ByVal Id As Integer
                                        ) As AssetMngDTO
        Dim LabourProtectionMng As AssetMngDTO
        Try
            Dim query = From p In Context.HU_ASSET_MNG
                        From d In Context.HU_ASSET.Where(Function(d) d.ID = p.ASSET_DECLARE_ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(e) e.ID = p.EMPLOYEE_ID)
                        From t In Context.HU_TITLE.Where(Function(t) t.ID = e.TITLE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = e.ORG_ID)
                        From staffrank In Context.HU_STAFF_RANK.Where(Function(f) f.ID = e.STAFF_RANK_ID).DefaultIfEmpty
                        From assetgroup In Context.OT_OTHER_LIST.Where(Function(f) f.ID = d.GROUP_ID And _
                                                                       f.TYPE_ID = ProfileCommon.OT_ASSET_GROUP.TYPE_ID)
                        From assetstatus In Context.OT_OTHER_LIST.Where(Function(f) f.ID = d.GROUP_ID And _
                                                                       f.TYPE_ID = ProfileCommon.OT_ASSET_GROUP.TYPE_ID)
                        Where p.ID = Id
                        Order By p.EMPLOYEE_ID

            ' select thuộc tính
            Dim wel = query.Select(Function(p) New AssetMngDTO With {
                                     .ID = p.p.ID,
                                         .ASSET_CODE = p.d.CODE,
                                         .ASSET_NAME = p.d.NAME,
                                         .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                         .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                         .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                         .ORG_ID = p.e.ORG_ID,
                                         .ORG_NAME = p.o.NAME_VN,
                                         .ORG_DESC = p.o.DESCRIPTION_PATH,
                                         .ASSET_VALUE = p.p.ASSET_VALUE,
                                         .ASSET_DECLARE_ID = p.p.ASSET_DECLARE_ID,
                                         .DEPOSITS = p.p.DEPOSITS,
                                         .DESC = p.p.SDESC,
                                         .ISSUE_DATE = p.p.ISSUE_DATE,
                                         .RETURN_DATE = p.p.RETURN_DATE,
                                         .TITLE_NAME = p.t.NAME_VN,
                                         .ASSET_GROUP_NAME = p.assetgroup.NAME_VN,
                                         .STAFF_RANK_NAME = p.staffrank.NAME,
                                         .ORG_TRANFER = p.p.ORG_TRANFER,
                                         .ORG_RECEIVE = p.p.ORG_RECEIVE,
                                         .ASSET_BARCODE = p.p.ASSET_BARCODE,
                                         .ASSET_SERIAL = p.p.ASSET_SERIAL,
                                         .STATUS_ID = p.p.STATUS_ID,
                                         .STATUS_NAME = p.assetstatus.NAME_VN,
                                         .WORK_STATUS = p.e.WORK_STATUS,
                                     .CREATED_DATE = p.p.CREATED_DATE})
            LabourProtectionMng = wel.FirstOrDefault
            Return LabourProtectionMng
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function InsertAssetMng(ByVal objAssetMng As AssetMngDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim iCount As Integer = 0
        Dim objAssetMngData As New HU_ASSET_MNG
        Try
            objAssetMngData.ID = Utilities.GetNextSequence(Context, Context.HU_ASSET_MNG.EntitySet.Name)
            objAssetMngData.EMPLOYEE_ID = objAssetMng.EMPLOYEE_ID
            objAssetMngData.RETURN_DATE = objAssetMng.RETURN_DATE
            objAssetMngData.ISSUE_DATE = objAssetMng.ISSUE_DATE
            objAssetMngData.ASSET_VALUE = objAssetMng.ASSET_VALUE
            objAssetMngData.DEPOSITS = objAssetMng.DEPOSITS
            objAssetMngData.SDESC = objAssetMng.DESC
            objAssetMngData.ASSET_DECLARE_ID = objAssetMng.ASSET_DECLARE_ID
            objAssetMngData.ORG_TRANFER = objAssetMng.ORG_TRANFER
            objAssetMngData.ORG_RECEIVE = objAssetMng.ORG_RECEIVE
            objAssetMngData.ASSET_BARCODE = objAssetMng.ASSET_BARCODE
            objAssetMngData.ASSET_SERIAL = objAssetMng.ASSET_SERIAL
            objAssetMngData.STATUS_ID = objAssetMng.STATUS_ID
            Context.HU_ASSET_MNG.AddObject(objAssetMngData)
            Context.SaveChanges(log)
            gID = objAssetMngData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function ModifyAssetMng(ByVal objAssetMng As AssetMngDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objAssetMngData As New HU_ASSET_MNG With {.ID = objAssetMng.ID}
        Try
            objAssetMngData = (From p In Context.HU_ASSET_MNG
                               Where p.ID = objAssetMng.ID).SingleOrDefault
            If objAssetMngData IsNot Nothing Then
                objAssetMngData.ID = objAssetMng.ID
                objAssetMngData.EMPLOYEE_ID = objAssetMng.EMPLOYEE_ID
                objAssetMngData.ISSUE_DATE = objAssetMng.ISSUE_DATE
                objAssetMngData.ASSET_DECLARE_ID = objAssetMng.ASSET_DECLARE_ID
                objAssetMngData.ASSET_VALUE = objAssetMng.ASSET_VALUE
                objAssetMngData.DEPOSITS = objAssetMng.DEPOSITS
                objAssetMngData.SDESC = objAssetMng.DESC
                objAssetMngData.RETURN_DATE = objAssetMng.RETURN_DATE
                objAssetMngData.ORG_TRANFER = objAssetMng.ORG_TRANFER
                objAssetMngData.ORG_RECEIVE = objAssetMng.ORG_RECEIVE
                objAssetMngData.ASSET_BARCODE = objAssetMng.ASSET_BARCODE
                objAssetMngData.ASSET_SERIAL = objAssetMng.ASSET_SERIAL
                objAssetMngData.STATUS_ID = objAssetMng.STATUS_ID
                Context.SaveChanges(log)
            Else
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function DeleteAssetMng(ByVal lstAssetMng() As AssetMngDTO,
                                   ByVal log As UserLog) As Boolean
        Dim lstAssetMngData As List(Of HU_ASSET_MNG)
        Dim lstIDAssetMng As List(Of Decimal) = (From p In lstAssetMng.ToList Select p.ID.Value).ToList
        lstAssetMngData = (From p In Context.HU_ASSET_MNG Where lstIDAssetMng.Contains(p.ID)).ToList

        For index = 0 To lstAssetMngData.Count - 1
            Context.HU_ASSET_MNG.DeleteObject(lstAssetMngData(index))
        Next
        Context.SaveChanges(log)
        Return True
    End Function

    ''' <summary>
    ''' lay danh sach Asset, khong lay nhung asset da~ co employeeID
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAssetByEmployee(ByVal employeeId As Decimal, ByVal filter As String) As List(Of AssetDTO)
        'Dim existedAssetIds = Context.HU_ASSET_MNG.Where(Function(p) p.EMPLOYEE_ID = employeeId).Select(Function(f) f.ID).ToList
        Dim assets = From asset In Context.HU_ASSET
                     Where asset.ACTFLG = ProfileCommon.ActiveStatus And
                        Not Context.HU_ASSET_MNG.Any(Function(p) CBool(p.EMPLOYEE_ID = employeeId))
                     Select New AssetDTO With {.ID = asset.ID,
                         .NAME = asset.NAME}
        'If existedAssetIds.Any Then
        '    assets = assets.Where(Function(f) Not existedAssetIds.Contains(f.ID))
        'End If
        If Not String.IsNullOrWhiteSpace(filter) Then
            assets = assets.Where(Function(f) f.NAME.Contains(filter))
        End If
        Return assets.ToList
    End Function
#End Region

End Class
