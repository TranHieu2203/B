Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Public Class InsuranceRepository
    Dim nvalue_id As Decimal?

#Region "Chế độ bảo hiểm"
    Public Function GetEntitledType(ByVal _filter As INS_ENTITLED_TYPE_DTO,
                                        Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_ENTITLED_TYPE_DTO)
        Try

            Dim query = From p In Context.INS_ENTITLED_TYPE
                        From c In Context.INS_GROUP_REGIMES.Where(Function(c) c.ID = p.GROUP_ARISING_TYPE_ID).DefaultIfEmpty
                        From rst In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REGIME_SALARY_TYPE).DefaultIfEmpty
                        From rdt In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.REGIME_DAY_TYPE).DefaultIfEmpty
            Dim lst = query.Select(Function(s) New INS_ENTITLED_TYPE_DTO With {
                                     .ID = s.p.ID,
                                      .GROUP_ARISING_TYPE_ID = s.p.GROUP_ARISING_TYPE_ID,
                                      .GROUP_ARISING_TYPE_NAME = s.c.REGIMES_NAME,
                                      .NAME_VN = s.p.NAME_VN,
                                      .NAME_EN = s.p.NAME_EN,
                                     .CODE = s.p.CODE,
                                      .DAY_OFF_SUMMARY = s.p.DAY_OFF_SUMMARY,
                                      .REGIME_SALARY_TYPE = s.p.REGIME_SALARY_TYPE,
                                      .REGIME_SALARY_NAME = s.rst.NAME_VN,
                                      .REGIME_DAY_TYPE = s.p.REGIME_DAY_TYPE,
                                      .REGIME_DAY_NAME = s.rdt.NAME_VN,
                                      .ENJOY_LEVEL = s.p.ENJOY_LEVEL,
                                      .STATUS = If(s.p.STATUS = "A", "Áp dụng", "Ngừng Áp dụng"),
                                      .CREATED_DATE = s.p.CREATED_DATE,
                                      .CREATED_BY = s.p.CREATED_BY,
                                      .CREATED_LOG = s.p.CREATED_LOG,
                                      .MODIFIED_DATE = s.p.MODIFIED_DATE,
                                      .MODIFIED_BY = s.p.MODIFIED_BY,
                                      .MODIFIED_LOG = s.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.GROUP_ARISING_TYPE_NAME) Then
                lst = lst.Where(Function(f) f.GROUP_ARISING_TYPE_NAME.ToLower().Contains(_filter.GROUP_ARISING_TYPE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If _filter.DAY_OFF_SUMMARY.HasValue Then
                lst = lst.Where(Function(f) f.DAY_OFF_SUMMARY.Value = _filter.DAY_OFF_SUMMARY)
            End If
            If _filter.ENJOY_LEVEL.HasValue Then
                lst = lst.Where(Function(f) f.ENJOY_LEVEL.Value = _filter.ENJOY_LEVEL)
            End If
            If Not String.IsNullOrEmpty(_filter.REGIME_SALARY_NAME) Then
                lst = lst.Where(Function(f) f.REGIME_SALARY_NAME.ToLower().Contains(_filter.REGIME_SALARY_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.REGIME_DAY_NAME) Then
                lst = lst.Where(Function(f) f.REGIME_DAY_NAME.ToLower().Contains(_filter.REGIME_DAY_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.STATUS) Then
                lst = lst.Where(Function(f) f.STATUS.ToLower().Contains(_filter.STATUS.ToLower()))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function InsertEntitledType(ByVal objTitle As INS_ENTITLED_TYPE_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_ENTITLED_TYPE
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_ENTITLED_TYPE.EntitySet.Name)
            objTitleData.CHANGE_TYPE_ID = objTitle.CHANGE_TYPE_ID
            objTitleData.GROUP_ARISING_TYPE_ID = objTitle.GROUP_ARISING_TYPE_ID
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.CODE = objTitle.CODE
            objTitleData.DAY_OFF_SUMMARY = objTitle.DAY_OFF_SUMMARY
            objTitleData.ENJOY_LEVEL = objTitle.ENJOY_LEVEL
            objTitleData.REGIME_SALARY_TYPE = objTitle.REGIME_SALARY_TYPE
            objTitleData.REGIME_DAY_TYPE = objTitle.REGIME_DAY_TYPE
            objTitleData.STATUS = objTitle.STATUS
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.INS_ENTITLED_TYPE.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ValidateEntitledType(ByVal _validate As INS_ENTITLED_TYPE_DTO)
        Dim query
        Try
            If _validate.NAME_VN <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_ENTITLED_TYPE
                             Where p.NAME_VN.ToUpper = _validate.NAME_VN.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.INS_ENTITLED_TYPE
                             Where p.NAME_VN.ToUpper = _validate.NAME_VN.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_ENTITLED_TYPE
                             Where p.ID = _validate.ID And p.STATUS.Trim.ToUpper.Contains("A")).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ModifyEntitledType(ByVal objTitle As INS_ENTITLED_TYPE_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As INS_ENTITLED_TYPE
        Try
            objTitleData = (From p In Context.INS_ENTITLED_TYPE Where p.ID = objTitle.ID).SingleOrDefault
            objTitleData.CHANGE_TYPE_ID = objTitle.CHANGE_TYPE_ID
            objTitleData.GROUP_ARISING_TYPE_ID = objTitle.GROUP_ARISING_TYPE_ID
            objTitleData.NAME_VN = objTitle.NAME_VN
            objTitleData.NAME_EN = objTitle.NAME_EN
            objTitleData.CODE = objTitle.CODE
            objTitleData.DAY_OFF_SUMMARY = objTitle.DAY_OFF_SUMMARY
            objTitleData.ENJOY_LEVEL = objTitle.ENJOY_LEVEL
            objTitleData.REGIME_SALARY_TYPE = objTitle.REGIME_SALARY_TYPE
            objTitleData.REGIME_DAY_TYPE = objTitle.REGIME_DAY_TYPE
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function ActiveEntitledType(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of INS_ENTITLED_TYPE)
        Try
            lstData = (From p In Context.INS_ENTITLED_TYPE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).STATUS = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function DeleteEntitledType(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstEntitledTypeData As List(Of INS_ENTITLED_TYPE)
        Try
            lstEntitledTypeData = (From p In Context.INS_ENTITLED_TYPE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstEntitledTypeData.Count - 1
                Context.INS_ENTITLED_TYPE.DeleteObject(lstEntitledTypeData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".EntitledType")
            Throw ex
        End Try
    End Function

#End Region

#Region "Quy đinh đối tượng"
    Public Function GetSpecifiedObjects(ByVal _filter As INS_SPECIFIED_OBJECTS_DTO,
                                       Optional ByVal PageIndex As Integer = 0,
                                        Optional ByVal PageSize As Integer = Integer.MaxValue,
                                        Optional ByRef Total As Integer = 0,
                                        Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_SPECIFIED_OBJECTS_DTO)
        Try
            Dim query = From p In Context.INS_SPECIFIED_OBJECTS
                        From r In Context.INS_REGION.Where(Function(F) F.ID = p.LOCATION_ID).DefaultIfEmpty

            Dim lst = query.Select(Function(s) New INS_SPECIFIED_OBJECTS_DTO With {
                                          .ID = s.p.ID,
                                          .EFFECTIVE_DATE = s.p.EFFECTIVE_DATE,
                                          .SI_DATE = s.p.SI_DATE,
                                          .HI_DATE = s.p.HI_DATE,
                                          .SI = s.p.SI,
                                          .HI = s.p.HI,
                                          .UI = s.p.UI,
                                          .SI_COM = s.p.SI_COM,
                                          .SI_EMP = s.p.SI_EMP,
                                          .HI_COM = s.p.HI_COM,
                                          .HI_EMP = s.p.HI_EMP,
                                          .UI_COM = s.p.UI_COM,
                                          .UI_EMP = s.p.UI_EMP,
                                          .BHTNLD_BNN_COM = s.p.BHTNLD_BNN_COM,
                                          .BHTNLD_BNN_EMP = s.p.BHTNLD_BNN_EMP,
                                          .SICK = s.p.SICK,
                                          .SALARY_MIN = s.p.SALARY_MIN,
                                          .MATERNITY = s.p.MATERNITY,
                                          .OFF_IN_HOUSE = s.p.OFF_IN_HOUSE,
                                          .OFF_TOGETHER = s.p.OFF_TOGETHER,
                                          .RETIRE_MALE = s.p.RETIRE_MALE,
                                          .RETIRE_FEMALE = s.p.RETIRE_FEMALE,
                                          .STATUS = s.p.STATUS,
                                          .SI_NN = s.p.SI_NN,
                                          .SI_EMP_NN = s.p.SI_EMP_NN,
                                          .SI_COM_NN = s.p.SI_COM_NN,
                                          .FOREIGN = s.p.FOREIGN,
                                          .FOREIGN_NAME = If(s.p.FOREIGN = -1, "X", ""),
                                          .CREATED_DATE = s.p.CREATED_DATE,
                                          .CREATED_BY = s.p.CREATED_BY,
                                          .CREATED_LOG = s.p.CREATED_LOG})

            If Not String.IsNullOrEmpty(_filter.LOCATION_NAME) Then
                lst = lst.Where(Function(f) f.LOCATION_NAME.ToLower().Contains(_filter.LOCATION_NAME.ToLower()))
            End If
            If _filter.EFFECTIVE_DATE.HasValue Then
                lst = lst.Where(Function(f) f.EFFECTIVE_DATE.Value = _filter.EFFECTIVE_DATE)
            End If
            If _filter.HI_DATE.HasValue Then
                lst = lst.Where(Function(f) f.HI_DATE.Value = _filter.HI_DATE)
            End If
            If _filter.SI_DATE.HasValue Then
                lst = lst.Where(Function(f) f.SI_DATE.Value = _filter.SI_DATE)
            End If
            If _filter.SI.HasValue Then
                lst = lst.Where(Function(f) f.SI.Value = _filter.SI)
            End If
            If _filter.SI_EMP.HasValue Then
                lst = lst.Where(Function(f) f.SI_EMP.Value = _filter.SI_EMP)
            End If
            If _filter.SI_COM.HasValue Then
                lst = lst.Where(Function(f) f.SI_COM.Value = _filter.SI_COM)
            End If
            If _filter.HI.HasValue Then
                lst = lst.Where(Function(f) f.HI_EMP.Value = _filter.HI_EMP)
            End If
            If _filter.HI_COM.HasValue Then
                lst = lst.Where(Function(f) f.HI_COM.Value = _filter.HI_COM)
            End If
            If _filter.UI.HasValue Then
                lst = lst.Where(Function(f) f.UI_EMP.Value = _filter.UI_EMP)
            End If
            If _filter.UI_COM.HasValue Then
                lst = lst.Where(Function(f) f.UI_COM.Value = _filter.UI_COM)
            End If
            If _filter.BHTNLD_BNN_EMP.HasValue Then
                lst = lst.Where(Function(f) f.BHTNLD_BNN_EMP.Value = _filter.BHTNLD_BNN_EMP)
            End If
            If _filter.BHTNLD_BNN_COM.HasValue Then
                lst = lst.Where(Function(f) f.BHTNLD_BNN_COM.Value = _filter.BHTNLD_BNN_COM)
            End If
            If _filter.SICK.HasValue Then
                lst = lst.Where(Function(f) f.SICK.Value = _filter.SICK)
            End If
            If _filter.MATERNITY.HasValue Then
                lst = lst.Where(Function(f) f.MATERNITY.Value = _filter.MATERNITY)
            End If
            If _filter.OFF_IN_HOUSE.HasValue Then
                lst = lst.Where(Function(f) f.OFF_IN_HOUSE.Value = _filter.OFF_IN_HOUSE)
            End If
            If _filter.OFF_TOGETHER.HasValue Then
                lst = lst.Where(Function(f) f.OFF_TOGETHER.Value = _filter.OFF_TOGETHER)
            End If
            If _filter.RETIRE_MALE.HasValue Then
                lst = lst.Where(Function(f) f.RETIRE_MALE.Value = _filter.RETIRE_MALE)
            End If
            If _filter.RETIRE_FEMALE.HasValue Then
                lst = lst.Where(Function(f) f.RETIRE_FEMALE.Value = _filter.RETIRE_FEMALE)
            End If
            If _filter.SALARY_MIN.HasValue Then
                lst = lst.Where(Function(f) f.SALARY_MIN.Value = _filter.SALARY_MIN)
            End If

            If _filter.SI_NN.HasValue Then
                lst = lst.Where(Function(f) f.SI_NN.Value = _filter.SI_NN)
            End If
            If _filter.SI_EMP_NN.HasValue Then
                lst = lst.Where(Function(f) f.SI_EMP_NN.Value = _filter.SI_EMP_NN)
            End If
            If _filter.SI_COM_NN.HasValue Then
                lst = lst.Where(Function(f) f.SI_COM_NN.Value = _filter.SI_COM_NN)
            End If
            If Not String.IsNullOrEmpty(_filter.FOREIGN_NAME) Then
                lst = lst.Where(Function(f) f.FOREIGN_NAME.ToUpper = _filter.FOREIGN_NAME.ToUpper)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function InsertSpecifiedObjects(ByVal objTitle As INS_SPECIFIED_OBJECTS_DTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_SPECIFIED_OBJECTS
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_SPECIFIED_OBJECTS.EntitySet.Name)
            objTitleData.LOCATION_ID = objTitle.LOCATION_ID
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.SI = objTitle.SI
            objTitleData.HI = objTitle.HI
            objTitleData.UI = objTitle.UI
            objTitleData.SI_COM = objTitle.SI_COM
            objTitleData.SI_EMP = objTitle.SI_EMP
            objTitleData.HI_COM = objTitle.HI_COM
            objTitleData.HI_EMP = objTitle.HI_EMP
            objTitleData.UI_COM = objTitle.UI_COM
            objTitleData.UI_EMP = objTitle.UI_EMP
            objTitleData.BHTNLD_BNN_COM = objTitle.BHTNLD_BNN_COM
            objTitleData.BHTNLD_BNN_EMP = objTitle.BHTNLD_BNN_EMP
            objTitleData.SICK = objTitle.SICK
            objTitleData.SALARY_MIN = objTitle.SALARY_MIN
            objTitleData.MATERNITY = objTitle.MATERNITY
            objTitleData.OFF_IN_HOUSE = objTitle.OFF_IN_HOUSE
            objTitleData.OFF_TOGETHER = objTitle.OFF_TOGETHER
            objTitleData.RETIRE_MALE = objTitle.RETIRE_MALE
            objTitleData.RETIRE_FEMALE = objTitle.RETIRE_FEMALE
            objTitleData.STATUS = objTitle.STATUS
            objTitleData.SI_DATE = objTitle.SI_DATE
            objTitleData.HI_DATE = objTitle.HI_DATE
            objTitleData.SI_NN = objTitle.SI_NN
            objTitleData.SI_EMP_NN = objTitle.SI_EMP_NN
            objTitleData.SI_COM_NN = objTitle.SI_COM_NN
            objTitleData.FOREIGN = objTitle.FOREIGN
            Context.INS_SPECIFIED_OBJECTS.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            UpdateMoneyInsArisingManual(objTitleData.ID, log.Username)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function VALIDATE_SPECIFIED_OBJECTS(ByVal effect_date As Date, ByVal Foreigner As Boolean, ByVal id As Decimal) As Boolean
        Try
            Dim isForeigner = If(Foreigner, -1, 0)
            Dim result
            If id <> -1 Then
                result = (From p In Context.INS_SPECIFIED_OBJECTS
                          Where p.EFFECTIVE_DATE = effect_date And p.FOREIGN = isForeigner And p.ID <> id).Any
            Else
                result = (From p In Context.INS_SPECIFIED_OBJECTS
                          Where p.EFFECTIVE_DATE = effect_date And p.FOREIGN = isForeigner).Any
            End If

            Return result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateMoneyInsArisingManual(ByVal P_ID As Decimal, ByVal P_USER As String) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_INS_BUSINESS.UPDATE_MONEY_INSARISING_MANUAL",
                                               New With {.P_ID = P_ID,
                                                         .P_USER = P_USER,
                                                         .CUR = cls.OUT_CURSOR})
                Return True
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ModifySpecifiedObjects(ByVal objTitle As INS_SPECIFIED_OBJECTS_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As INS_SPECIFIED_OBJECTS
        Try
            objTitleData = (From p In Context.INS_SPECIFIED_OBJECTS Where p.ID = objTitle.ID).SingleOrDefault
            objTitleData.LOCATION_ID = objTitle.LOCATION_ID
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.SI = objTitle.SI
            objTitleData.HI = objTitle.HI
            objTitleData.UI = objTitle.UI
            objTitleData.SI_COM = objTitle.SI_COM
            objTitleData.SI_EMP = objTitle.SI_EMP
            objTitleData.HI_COM = objTitle.HI_COM
            objTitleData.HI_EMP = objTitle.HI_EMP
            objTitleData.UI_COM = objTitle.UI_COM
            objTitleData.UI_EMP = objTitle.UI_EMP
            objTitleData.BHTNLD_BNN_COM = objTitle.BHTNLD_BNN_COM
            objTitleData.BHTNLD_BNN_EMP = objTitle.BHTNLD_BNN_EMP
            objTitleData.SICK = objTitle.SICK
            objTitleData.SALARY_MIN = objTitle.SALARY_MIN
            objTitleData.MATERNITY = objTitle.MATERNITY
            objTitleData.OFF_IN_HOUSE = objTitle.OFF_IN_HOUSE
            objTitleData.OFF_TOGETHER = objTitle.OFF_TOGETHER
            objTitleData.RETIRE_MALE = objTitle.RETIRE_MALE
            objTitleData.RETIRE_FEMALE = objTitle.RETIRE_FEMALE
            objTitleData.SI_DATE = objTitle.SI_DATE
            objTitleData.HI_DATE = objTitle.HI_DATE
            objTitleData.SI_NN = objTitle.SI_NN
            objTitleData.SI_EMP_NN = objTitle.SI_EMP_NN
            objTitleData.SI_COM_NN = objTitle.SI_COM_NN
            objTitleData.FOREIGN = objTitle.FOREIGN
            Context.SaveChanges(log)
            gID = objTitleData.ID
            UpdateMoneyInsArisingManual(objTitleData.ID, log.Username)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function ActiveSpecifiedObjects(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As Decimal) As Boolean
        Dim lstData As List(Of INS_SPECIFIED_OBJECTS)
        Try
            lstData = (From p In Context.INS_SPECIFIED_OBJECTS Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).STATUS = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function DeleteSpecifiedObjects(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstSpecifiedObjectsData As List(Of INS_SPECIFIED_OBJECTS)
        Try
            lstSpecifiedObjectsData = (From p In Context.INS_SPECIFIED_OBJECTS Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstSpecifiedObjectsData.Count - 1
                Context.INS_SPECIFIED_OBJECTS.DeleteObject(lstSpecifiedObjectsData(index))
            Next
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".SpecifiedObjects")
            Throw ex
        End Try
    End Function

    Public Function ObjectPayInsurrance(ByVal lstID As List(Of String), ByVal objName As String) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteSQL("UPDATE HU_CONTRACT_TYPE C SET C." & objName & "= 0 ")
                If lstID.Count > 0 Then
                    Dim str = lstID.Aggregate(Function(cur, [next]) cur & "," & [next])
                    cls.ExecuteSQL("UPDATE HU_CONTRACT_TYPE C SET C." & objName & "= 1 WHERE C.ID IN (" & str & ")")
                End If
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".SpecifiedObjects")
            Throw ex
        End Try
    End Function
#End Region

#Region "Danh mục nơi khám chữa bệnh"
    Public Function GetINS_WHEREHEALTH(ByVal _filter As INS_WHEREHEALTHDTO,
                                      Optional ByVal PageIndex As Integer = 0,
                                       Optional ByVal PageSize As Integer = Integer.MaxValue,
                                       Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_WHEREHEALTHDTO)
        Try

            Dim query = From p In Context.INS_WHEREHEALTH
                        From t In Context.HU_PROVINCE.Where(Function(F) F.ID = p.ID_PROVINCE).DefaultIfEmpty
                        From n In Context.HU_DISTRICT.Where(Function(D) D.ID = p.ID_DISTRICT).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New INS_WHEREHEALTHDTO With {
                                       .ID = p.p.ID,
                                       .CODE = p.p.CODE,
                                       .NAME_VN = p.p.NAME_VN,
                                       .ADDRESS = p.p.ADDRESS,
                                       .ID_PROVINCE = p.p.ID_PROVINCE,
                                       .PROVINCE_NAME = p.t.NAME_VN,
                                       .ID_DISTRICT = p.p.ID_DISTRICT,
                                       .DISTRICT_NAME = p.n.NAME_VN,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME_VN) Then
                lst = lst.Where(Function(f) f.NAME_VN.ToLower().Contains(_filter.NAME_VN.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ADDRESS) Then
                lst = lst.Where(Function(f) f.ADDRESS.ToLower().Contains(_filter.ADDRESS.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.PROVINCE_NAME) Then
                lst = lst.Where(Function(f) f.PROVINCE_NAME.ToLower().Contains(_filter.PROVINCE_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.DISTRICT_NAME) Then
                lst = lst.Where(Function(f) f.DISTRICT_NAME.ToLower().Contains(_filter.DISTRICT_NAME.ToLower()))
            End If

            'lst = lst.OrderBy(Sorts)
            lst = lst.OrderByDescending(Function(f) If(f.CREATED_DATE.HasValue, f.CREATED_DATE, Date.MinValue))
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetINS_WHEREEXPORT() As List(Of INS_WHEREHEALTHDTO)
        Try

            Dim query = From p In Context.INS_WHEREHEALTH
                        Order By p.CODE
            Dim lst = query.Select(Function(p) New INS_WHEREHEALTHDTO With {
                                       .ID = p.ID,
                                       .NAME_VN = "[" & p.CODE & "] - " & p.NAME_VN})
            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetStatuSo() As DataTable
        Try

            Dim query = From p In Context.OT_OTHER_LIST
                        Where p.ACTFLG = "A" And p.TYPE_ID = 2022 _
                        Order By p.NAME_VN
            Dim lst As List(Of OT_OTHERLIST_DTO) = query.Select(Function(p) New OT_OTHERLIST_DTO With {
                                       .ID = p.ID,
                                       .NAME_VN = p.NAME_VN}).ToList

            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetStatuHE() As DataTable
        Try

            Dim query = From p In Context.OT_OTHER_LIST
                        Where p.ACTFLG = "A" And p.TYPE_ID = 2023 _
                        Order By p.NAME_VN
            Dim lst As List(Of OT_OTHERLIST_DTO) = query.Select(Function(p) New OT_OTHERLIST_DTO With {
                                       .ID = p.ID,
                                       .NAME_VN = p.NAME_VN}).ToList

            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetHU_PROVINCE(Optional ByVal PageIndex As Integer = 0,
                                       Optional ByVal PageSize As Integer = Integer.MaxValue,
                                       Optional ByRef Total As Integer = 0,
                                       Optional ByVal Sorts As String = "NAME_VN desc") As DataTable
        Try

            Dim query = From p In Context.HU_PROVINCE
                        Where p.ACTFLG = "A"
                        Order By p.NAME_VN
            Dim lst As List(Of HU_PROVINCEDTO) = query.Select(Function(p) New HU_PROVINCEDTO With {
                                       .id = p.ID,
                                       .name_vn = p.NAME_VN}).ToList

            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetHU_DISTRICT(Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "NAME_VN desc") As DataTable
        Try

            Dim query = From p In Context.HU_DISTRICT
                        From t In Context.HU_PROVINCE.Where(Function(F) F.ID = p.PROVINCE_ID).DefaultIfEmpty
                        Where p.ACTFLG = "A"
                        Order By t.NAME_VN, p.NAME_VN

            Dim lst As List(Of HU_DISTRICTDTO) = query.Select(Function(p) New HU_DISTRICTDTO With {
                                       .ID = p.p.ID,
                                       .province_Name = p.t.NAME_VN,
                                       .name_vn = p.p.NAME_VN}).ToList

            Return lst.ToTable
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetDistrictByIDPro(ByVal province_ID As Decimal) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INSURANCE.GETDISTRICT",
                                           New With {.P_PROVINCE_ID = province_ID,
                                                     .CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function InsertINS_WHEREHEALTH(ByVal objTitle As INS_WHEREHEALTHDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_WHEREHEALTH
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_WHEREHEALTH.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.ADDRESS = objTitle.ADDRESS
            objTitleData.ID_DISTRICT = objTitle.ID_DISTRICT
            objTitleData.ID_PROVINCE = objTitle.ID_PROVINCE
            objTitleData.ACTFLG = objTitle.ACTFLG
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.INS_WHEREHEALTH.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function ValidateINS_WHEREHEALTH(ByVal _validate As INS_WHEREHEALTHDTO)
        Dim query
        Try
            If _validate.CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_WHEREHEALTH
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.INS_WHEREHEALTH
                             Where p.CODE.ToUpper = _validate.CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ModifyINS_WHEREHEALTH(ByVal objTitle As INS_WHEREHEALTHDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_WHEREHEALTH With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.INS_WHEREHEALTH Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME_VN = objTitle.NAME_VN.Trim
            objTitleData.ADDRESS = objTitle.ADDRESS
            objTitleData.ID_PROVINCE = objTitle.ID_PROVINCE
            objTitleData.ID_DISTRICT = objTitle.ID_DISTRICT
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ActiveINS_WHEREHEALTH(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of INS_WHEREHEALTH)
        Try
            lstData = (From p In Context.INS_WHEREHEALTH Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function DeleteINS_WHEREHEALTH(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHolidayData As List(Of INS_WHEREHEALTH)
        Try
            lstHolidayData = (From p In Context.INS_WHEREHEALTH Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHolidayData.Count - 1
                Context.INS_WHEREHEALTH.DeleteObject(lstHolidayData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function


#End Region

#Region "Biến động bảo hiểm"
    Public Function GetInsChangeType(ByVal _filter As INS_CHANGE_TYPEDTO,
                                    Optional ByRef PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_CHANGE_TYPEDTO)
        Try

            Dim query = From p In Context.INS_CHANGE_TYPE
                        From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.ARISING_TYPE).DefaultIfEmpty
                        From o1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.SIGN_ARISING).DefaultIfEmpty
                        From o2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.SIGN_ARISING_DETAIL).DefaultIfEmpty
                        From type In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_DECLARE_ID).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New INS_CHANGE_TYPEDTO With {
                                       .ID = p.p.ID,
                                       .ARISING_NAME = p.p.ARISING_NAME,
                                       .ARISING_TYPE = p.p.ARISING_TYPE,
                                       .ARISING_NAME_OT = p.o.NAME_VN,
                                       .ACTFLG = If(p.p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                                       .NOTE = p.p.NOTE,
                                       .SIGN_ARISING = p.p.SIGN_ARISING,
                                       .SIGN_ARISING_NAME = p.o1.NAME_VN,
                                       .SIGN_ARISING_DETAIL = p.p.SIGN_ARISING_DETAIL,
                                       .SIGN_ARISING_DETAIL_NAME = p.o2.NAME_VN,
                                       .CODE = p.p.CODE,
                                       .TYPE_DECLARE_ID = p.p.TYPE_DECLARE_ID,
                                       .TYPE_DECLARE_NAME = p.type.NAME_VN,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.ARISING_NAME) Then
                lst = lst.Where(Function(f) f.ARISING_NAME.ToLower().Contains(_filter.ARISING_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ARISING_NAME_OT) Then
                lst = lst.Where(Function(f) f.ARISING_NAME_OT.ToLower().Contains(_filter.ARISING_NAME_OT.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SIGN_ARISING_NAME) Then
                lst = lst.Where(Function(f) f.SIGN_ARISING_NAME.ToLower().Contains(_filter.SIGN_ARISING_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.SIGN_ARISING_DETAIL_NAME) Then
                lst = lst.Where(Function(f) f.SIGN_ARISING_DETAIL_NAME.ToLower().Contains(_filter.SIGN_ARISING_DETAIL_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.TYPE_DECLARE_NAME) Then
                lst = lst.Where(Function(f) f.TYPE_DECLARE_NAME.ToLower().Contains(_filter.TYPE_DECLARE_NAME.ToLower()))
            End If
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function InsertInsChangeType(ByVal objTitle As INS_CHANGE_TYPEDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_CHANGE_TYPE
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_CHANGE_TYPE.EntitySet.Name)
            objTitleData.ARISING_NAME = objTitle.ARISING_NAME.Trim
            objTitleData.ARISING_TYPE = objTitle.ARISING_TYPE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.DISPLAY_COMBO = "1"
            objTitleData.CODE = objTitle.CODE
            objTitleData.TYPE_DECLARE_ID = objTitle.TYPE_DECLARE_ID
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            objTitleData.SIGN_ARISING = objTitle.SIGN_ARISING
            objTitleData.SIGN_ARISING_DETAIL = objTitle.SIGN_ARISING_DETAIL
            Context.INS_CHANGE_TYPE.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function ValidateInsChangeType(ByVal _validate As INS_CHANGE_TYPEDTO)
        Dim query
        Try
            If _validate.ARISING_NAME <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_CHANGE_TYPE
                             Where p.ARISING_NAME.ToUpper = _validate.ARISING_NAME.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.INS_CHANGE_TYPE
                             Where p.ARISING_NAME.ToUpper = _validate.ARISING_NAME.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ModifyInsChangeType(ByVal objTitle As INS_CHANGE_TYPEDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_CHANGE_TYPE With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.INS_CHANGE_TYPE Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.ARISING_NAME = objTitle.ARISING_NAME.Trim
            objTitleData.ARISING_TYPE = objTitle.ARISING_TYPE
            objTitleData.NOTE = objTitle.NOTE
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            objTitleData.SIGN_ARISING = objTitle.SIGN_ARISING
            objTitleData.CODE = objTitle.CODE
            objTitleData.TYPE_DECLARE_ID = objTitle.TYPE_DECLARE_ID
            objTitleData.SIGN_ARISING_DETAIL = objTitle.SIGN_ARISING_DETAIL
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function ActiveInsChangeType(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of INS_CHANGE_TYPE)
        Try
            lstData = (From p In Context.INS_CHANGE_TYPE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function DeleteInsChangeType(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstInsChangeTypeData As List(Of INS_CHANGE_TYPE)
        Try
            lstInsChangeTypeData = (From p In Context.INS_CHANGE_TYPE Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstInsChangeTypeData.Count - 1
                Context.INS_CHANGE_TYPE.DeleteObject(lstInsChangeTypeData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function

#End Region

#Region "Danh mục vùng bảo hiểm"
    Public Function GetINS_REGION(ByVal _filter As INS_REGION_DTO,
                                    Optional ByVal PageIndex As Integer = 0,
                                      Optional ByVal PageSize As Integer = Integer.MaxValue,
                                      Optional ByRef Total As Integer = 0,
                                      Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_REGION_DTO)
        Try

            Dim query = From p In Context.INS_REGION
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.AREA_ID).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New INS_REGION_DTO With {
                                       .ID = p.p.ID,
                                       .AREA_ID = p.p.AREA_ID,
                                       .AREA_NAME = p.ot.NAME_VN,
                                       .EFFECTIVE_DATE = p.p.EFFECTIVE_DATE,
                                       .CEILING_AMOUNT = p.p.CEILING_AMOUNT,
                                       .NOTE = p.p.NOTE,
                                       .CREATED_BY = p.p.CREATED_BY,
                                       .CREATED_DATE = p.p.CREATED_DATE,
                                       .CREATED_LOG = p.p.CREATED_LOG,
                                       .MODIFIED_BY = p.p.MODIFIED_BY,
                                       .MODIFIED_DATE = p.p.MODIFIED_DATE,
                                       .MODIFIED_LOG = p.p.MODIFIED_LOG,
                                       .MIN_AMOUNT = p.p.MIN_AMOUNT})


            If _filter.CEILING_AMOUNT.HasValue Then
                lst = lst.Where(Function(f) f.CEILING_AMOUNT = _filter.CEILING_AMOUNT)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.AREA_NAME) Then
                lst = lst.Where(Function(f) f.AREA_NAME.ToLower().Contains(_filter.AREA_NAME.ToLower()))
            End If

            If _filter.MIN_AMOUNT.HasValue Then
                lst = lst.Where(Function(f) f.MIN_AMOUNT = _filter.MIN_AMOUNT)
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function InsertINS_REGION(ByVal objTitle As INS_REGION_DTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_REGION
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_REGION.EntitySet.Name)
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.AREA_ID = objTitle.AREA_ID
            objTitleData.CEILING_AMOUNT = objTitle.CEILING_AMOUNT
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.MIN_AMOUNT = objTitle.MIN_AMOUNT
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.INS_REGION.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function ValidateINS_REGION(ByVal _validate As INS_REGION_DTO)
        Try
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ModifyINS_REGION(ByVal objTitle As INS_REGION_DTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_REGION With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.INS_REGION Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.AREA_ID = objTitle.AREA_ID
            objTitleData.CEILING_AMOUNT = objTitle.CEILING_AMOUNT
            objTitleData.EFFECTIVE_DATE = objTitle.EFFECTIVE_DATE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.MIN_AMOUNT = objTitle.MIN_AMOUNT
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ActiveINS_REGION(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Try
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function DeleteINS_REGION(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHData As List(Of INS_REGION)
        Try
            lstHData = (From p In Context.INS_REGION Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHData.Count - 1
                Context.INS_REGION.DeleteObject(lstHData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
#End Region

#Region "Danh mục chi phí theo cấp"
    Public Function GetINS_COST_FOLLOW_LEVER(ByVal _filter As INS_COST_FOLLOW_LEVERDTO,
                                             Optional ByVal PageIndex As Integer = 0,
                                             Optional ByVal PageSize As Integer = Integer.MaxValue,
                                             Optional ByRef Total As Integer = 0,
                                             Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of INS_COST_FOLLOW_LEVERDTO)
        Try

            Dim query = From p In Context.INS_COST_FOLLOW_LEVER
            Dim lst = query.Select(Function(p) New INS_COST_FOLLOW_LEVERDTO With {
                           .ID = p.ID,
                           .COST_CODE = p.COST_CODE,
                           .COST_NAME = p.COST_NAME,
                           .COST_MONEY = p.COST_MONEY,
                           .EFFECTDATE_FROM = p.EFFECTDATE_FROM,
                           .EFFECTDATE_TO = p.EFFECTDATE_TO,
                           .NOTE = p.NOTE,
                           .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng"),
                           .CREATED_BY = p.CREATED_BY,
                           .CREATED_DATE = p.CREATED_DATE,
                           .CREATED_LOG = p.CREATED_LOG,
                           .MODIFIED_BY = p.MODIFIED_BY,
                           .MODIFIED_DATE = p.MODIFIED_DATE,
                           .MODIFIED_LOG = p.MODIFIED_LOG})

            If Not String.IsNullOrEmpty(_filter.COST_CODE) Then
                lst = lst.Where(Function(f) f.COST_CODE.ToLower().Contains(_filter.COST_CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.COST_NAME) Then
                lst = lst.Where(Function(f) f.COST_NAME.ToLower().Contains(_filter.COST_NAME.ToLower()))
            End If
            If _filter.COST_MONEY.HasValue Then
                lst = lst.Where(Function(f) f.COST_MONEY.Value = _filter.COST_MONEY)
            End If
            If _filter.EFFECTDATE_FROM.HasValue Then
                lst = lst.Where(Function(f) f.EFFECTDATE_FROM.Value = _filter.EFFECTDATE_FROM)
            End If
            If _filter.EFFECTDATE_TO.HasValue Then
                lst = lst.Where(Function(f) f.EFFECTDATE_TO.Value = _filter.EFFECTDATE_TO)
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function InsertINS_COST_FOLLOW_LEVER(ByVal objTitle As INS_COST_FOLLOW_LEVERDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_COST_FOLLOW_LEVER
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_COST_FOLLOW_LEVER.EntitySet.Name)
            objTitleData.COST_CODE = objTitle.COST_CODE.Trim
            objTitleData.COST_NAME = objTitle.COST_NAME.Trim
            objTitleData.COST_MONEY = objTitle.COST_MONEY
            objTitleData.EFFECTDATE_FROM = objTitle.EFFECTDATE_FROM
            objTitleData.EFFECTDATE_TO = objTitle.EFFECTDATE_TO
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.INS_COST_FOLLOW_LEVER.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function ValidateINS_COST_FOLLOW_LEVER(ByVal _validate As INS_COST_FOLLOW_LEVERDTO)
        Dim query
        Try
            If _validate.COST_CODE <> Nothing Then
                If _validate.ID <> 0 Then
                    query = (From p In Context.INS_COST_FOLLOW_LEVER
                             Where p.COST_CODE.ToUpper = _validate.COST_CODE.ToUpper _
                             And p.ID <> _validate.ID).FirstOrDefault
                Else
                    query = (From p In Context.INS_COST_FOLLOW_LEVER
                             Where p.COST_CODE.ToUpper = _validate.COST_CODE.ToUpper).FirstOrDefault
                End If
                Return (query Is Nothing)
            Else
                If _validate.ACTFLG <> "" And _validate.ID <> 0 Then
                    query = (From p In Context.INS_COST_FOLLOW_LEVER
                             Where p.ACTFLG.ToUpper = _validate.ACTFLG.ToUpper _
                             And p.ID = _validate.ID).FirstOrDefault
                    Return (Not query Is Nothing)
                End If
                If _validate.ID <> 0 And _validate.ACTFLG = "" Then
                    query = (From p In Context.INS_COST_FOLLOW_LEVER
                             Where p.ID = _validate.ID).FirstOrDefault
                    Return (query Is Nothing)
                End If
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ModifyINS_COST_FOLLOW_LEVER(ByVal objTitle As INS_COST_FOLLOW_LEVERDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_COST_FOLLOW_LEVER With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.INS_COST_FOLLOW_LEVER Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.COST_CODE = objTitle.COST_CODE.Trim
            objTitleData.COST_NAME = objTitle.COST_NAME.Trim
            objTitleData.COST_MONEY = objTitle.COST_MONEY
            objTitleData.EFFECTDATE_FROM = objTitle.EFFECTDATE_FROM
            objTitleData.EFFECTDATE_TO = objTitle.EFFECTDATE_TO
            objTitleData.NOTE = objTitle.NOTE
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ActiveINS_COST_FOLLOW_LEVER(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of INS_COST_FOLLOW_LEVER)
        Try
            lstData = (From p In Context.INS_COST_FOLLOW_LEVER Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function DeleteINS_COST_FOLLOW_LEVER(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstData As List(Of INS_COST_FOLLOW_LEVER)
        Try
            lstData = (From p In Context.INS_COST_FOLLOW_LEVER Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                Context.INS_COST_FOLLOW_LEVER.DeleteObject(lstData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteCostCenter")
            Throw ex
        End Try
    End Function
#End Region

#Region "Danh mục nhóm hưởng chế độ"
    Public Function GetINS_REGIMES(ByVal _filter As INS_GROUP_REGIMESDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "REGIMES_NAME desc") As List(Of INS_GROUP_REGIMESDTO)
        Try

            Dim query = From p In Context.INS_GROUP_REGIMES
            Dim lst = query.Select(Function(p) New INS_GROUP_REGIMESDTO With {
                                       .ID = p.ID,
                                       .REGIMES_NAME = p.REGIMES_NAME,
                                       .NOTE = p.NOTE,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng")})

            If Not String.IsNullOrEmpty(_filter.REGIMES_NAME) Then
                lst = lst.Where(Function(f) f.REGIMES_NAME.ToLower().Contains(_filter.REGIMES_NAME.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function InsertINS_REGIMES(ByVal objTitle As INS_GROUP_REGIMESDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_GROUP_REGIMES
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_GROUP_REGIMES.EntitySet.Name)
            objTitleData.REGIMES_NAME = objTitle.REGIMES_NAME.Trim
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.INS_GROUP_REGIMES.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function ModifyINS_REGIMES(ByVal objTitle As INS_GROUP_REGIMESDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_GROUP_REGIMES With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.INS_GROUP_REGIMES Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.REGIMES_NAME = objTitle.REGIMES_NAME.Trim
            objTitleData.NOTE = objTitle.NOTE
            'objTitleData.CREATED_BY = objTitle.CREATED_BY
            'objTitleData.CREATED_DATE = objTitle.CREATED_DATE
            'objTitleData.CREATED_LOG = objTitle.CREATED_LOG
            'objTitleData.MODIFIED_BY = objTitle.MODIFIED_BY
            'objTitleData.MODIFIED_DATE = objTitle.MODIFIED_DATE
            'objTitleData.MODIFIED_LOG = objTitle.MODIFIED_LOG
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ActiveINS_REGIMES(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of INS_GROUP_REGIMES)
        Try
            lstData = (From p In Context.INS_GROUP_REGIMES Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function DeleteINS_REGIMES(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHData As List(Of INS_GROUP_REGIMES)
        Try
            lstHData = (From p In Context.INS_GROUP_REGIMES Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHData.Count - 1
                Context.INS_GROUP_REGIMES.DeleteObject(lstHData(index))
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteGroupRegimes")
            Throw ex
        End Try
    End Function
#End Region

#Region "Danh mục chương trình bảo hiểm"
    Public Function GetINS_Program(ByVal _filter As INS_LIST_PROGRAMDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "CODE desc") As List(Of INS_LIST_PROGRAMDTO)
        Try

            Dim query = From p In Context.INS_LIST_PROGRAM
            Dim lst = query.Select(Function(p) New INS_LIST_PROGRAMDTO With {
                                       .ID = p.ID,
                                       .CODE = p.CODE,
                                       .NAME = p.NAME,
                                       .NOTE = p.NOTE,
                                       .IS_DELETED = p.IS_DELETED,
                                       .ACTFLG = If(p.ACTFLG = "A", "Áp dụng", "Ngừng Áp dụng")})

            If Not String.IsNullOrEmpty(_filter.CODE) Then
                lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NOTE) Then
                lst = lst.Where(Function(f) f.NOTE.ToLower().Contains(_filter.NOTE.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.ACTFLG) Then
                lst = lst.Where(Function(f) f.ACTFLG.ToLower().Contains(_filter.ACTFLG.ToLower()))
            End If
            If Not String.IsNullOrEmpty(_filter.NAME) Then
                lst = lst.Where(Function(f) f.NAME.ToLower().Contains(_filter.NAME.ToLower()))
            End If
            lst = lst.Where(Function(f) f.IS_DELETED <> 1)
            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function InsertINS_Program(ByVal objTitle As INS_LIST_PROGRAMDTO,
                                    ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_LIST_PROGRAM
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_LIST_PROGRAM.EntitySet.Name)
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME = objTitle.NAME
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.ACTFLG = objTitle.ACTFLG
            objTitleData.IS_DELETED = 0
            Context.INS_LIST_PROGRAM.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function ModifyINS_Program(ByVal objTitle As INS_LIST_PROGRAMDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_LIST_PROGRAM With {.ID = objTitle.ID}
        Try
            objTitleData = (From p In Context.INS_LIST_PROGRAM Where p.ID = objTitleData.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CODE = objTitle.CODE.Trim
            objTitleData.NAME = objTitle.NAME
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.IS_DELETED = 0
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ActiveINS_Program(ByVal lstID As List(Of Decimal), ByVal log As UserLog, ByVal bActive As String) As Boolean
        Dim lstData As List(Of INS_LIST_PROGRAM)
        Try
            lstData = (From p In Context.INS_LIST_PROGRAM Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).ACTFLG = bActive
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function DeleteINS_Program(ByVal lstID As List(Of Decimal)) As Boolean
        Dim lstHData As List(Of INS_LIST_PROGRAM)
        Try
            lstHData = (From p In Context.INS_LIST_PROGRAM Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstHData.Count - 1
                ' Context.INS_LIST_PROGRAM.DeleteObject(lstHData(index))
                lstHData(index).IS_DELETED = 1
            Next
            Context.SaveChanges()
            Return True

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".DeleteGroupRegimes")
            Throw ex
        End Try
    End Function
#End Region


#Region "Danh mục hợp đồng bảo hiểm sức khỏe"
    Public Function GetINS_LIST_CONTRACT_PKG(
                                             ByVal CONTRACT_INS_NO As String,
                                             ByVal YEAR As Integer?,
                                             ByVal ORG_INSURANCE As Integer?,
                                             Optional ByVal PAGE As Integer = 0,
                                             Optional ByVal SIZE As Integer = Integer.MaxValue,
                                             Optional ByRef Total As Integer = 0,
                                             Optional ByVal Sorts As String = "ID desc", Optional ByVal log As UserLog = Nothing) As List(Of INS_LIST_CONTRACTDTO)
        Using cls As New DataAccess.QueryData
            Dim lst As New List(Of INS_LIST_CONTRACTDTO)
            Dim dtData As DataTable = cls.ExecuteStore("PKG_INS_LIST.GETINS_LIST_CONTRACT_PKG",
                                           New With {.P_CONTRACT_INS_NO = CONTRACT_INS_NO,
                                                     .P_YEAR = YEAR,
                                                     .P_ORG_INSURANCE = ORG_INSURANCE,
                                                     .P_PAGE = PAGE,
                                                     .P_SIZE = SIZE,
                                                     .P_CUR = cls.OUT_CURSOR})
            If dtData.Rows.Count > 0 Then
                For Each dr As DataRow In dtData.Rows
                    Dim itm As New INS_LIST_CONTRACTDTO
                    itm.ID = dr("ID")
                    itm.CONTRACT_INS_NO = dr("CONTRACT_INS_NO").ToString
                    itm.YEAR = If(IsNumeric(dr("YEAR")), dr("YEAR"), Nothing)
                    itm.ORG_INSURANCE = If(IsNumeric(dr("ORG_INSURANCE")), dr("ORG_INSURANCE"), Nothing)
                    itm.START_DATE = If(IsDate(dr("START_DATE")), dr("START_DATE"), Nothing)

                    itm.EXPIRE_DATE = If(IsDate(dr("EXPIRE_DATE")), dr("EXPIRE_DATE"), Nothing)
                    itm.VAL_CO = If(IsNumeric(dr("VAL_CO")), dr("VAL_CO"), Nothing)
                    itm.BUY_DATE = If(IsDate(dr("BUY_DATE")), dr("BUY_DATE"), Nothing)
                    itm.NOTE = If(IsNumeric(dr("NOTE")), dr("NOTE"), Nothing)
                    itm.PROGRAM_ID = If(IsNumeric(dr("PROGRAM_ID")), dr("PROGRAM_ID"), Nothing)
                    itm.IS_DELETED = If(IsNumeric(dr("IS_DELETED")), dr("IS_DELETED"), Nothing)
                    itm.PROGRAM_MUL_NAME = dr("PROGRAM").ToString
                    itm.ORG_INSURANCE_NAME = dr("ORG_INSURANCE_NAME").ToString
                    lst.Add(itm)
                Next
            End If
            Total = lst.Count
            Return lst
        End Using
        Return Nothing
    End Function
    Public Function GetINS_LIST_CONTRACT(ByVal _filter As INS_LIST_CONTRACTDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "ID desc") As List(Of INS_LIST_CONTRACTDTO)
        Try

            Dim query = From p In Context.INS_LIST_CONTRACT
                        From e In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.ORG_INSURANCE).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New INS_LIST_CONTRACTDTO With {
                                       .ID = p.p.ID,
                                       .CONTRACT_INS_NO = p.p.CONTRACT_INS_NO,
                                       .YEAR = p.p.YEAR,
                                       .ORG_INSURANCE = p.p.ORG_INSURANCE,
                                       .START_DATE = p.p.START_DATE,
                                       .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                       .VAL_CO = p.p.VAL_CO,
                                       .BUY_DATE = p.p.BUY_DATE,
                                       .ORG_INSURANCE_NAME = p.e.NAME_VN,
                                       .NOTE = p.p.NOTE,
                                       .IS_DELETED = p.p.IS_DELETED})

            If Not String.IsNullOrEmpty(_filter.CONTRACT_INS_NO) Then
                lst = lst.Where(Function(f) f.CONTRACT_INS_NO.ToLower().Contains(_filter.CONTRACT_INS_NO.ToLower()))
            End If
            If _filter.YEAR IsNot Nothing Then
                lst = lst.Where(Function(f) f.YEAR = _filter.YEAR)
            End If
            If _filter.ORG_INSURANCE IsNot Nothing Then
                lst = lst.Where(Function(f) f.ORG_INSURANCE = _filter.ORG_INSURANCE)
            End If
            lst = lst.Where(Function(f) f.IS_DELETED <> 1)

            lst = lst.OrderBy(Sorts)
            Total = lst.Count
            lst = lst.Skip(PageIndex * PageSize).Take(PageSize)

            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetINS_LIST_CONTRACT_BY_ID(ID As Decimal?) As INS_LIST_CONTRACTDTO
        Try

            Dim query = From p In Context.INS_LIST_CONTRACT
                        From e In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.ORG_INSURANCE).DefaultIfEmpty
            Dim lst = query.Select(Function(p) New INS_LIST_CONTRACTDTO With {
                                       .ID = p.p.ID,
                                       .CONTRACT_INS_NO = p.p.CONTRACT_INS_NO,
                                       .YEAR = p.p.YEAR,
                                       .ORG_INSURANCE = p.p.ORG_INSURANCE,
                                       .START_DATE = p.p.START_DATE,
                                       .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                       .VAL_CO = p.p.VAL_CO,
                                       .BUY_DATE = p.p.BUY_DATE,
                                       .ORG_INSURANCE_NAME = p.e.NAME_VN,
                                       .NOTE = p.p.NOTE})

            lst = lst.Where(Function(f) f.ID = ID)


            Return lst.FirstOrDefault
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function GetINS_LIST_CONTRACT_DETAIL(ByVal _filter As INS_LIST_CONTRACT_DETAILDTO,
                                   Optional ByVal PageIndex As Integer = 0,
                                     Optional ByVal PageSize As Integer = Integer.MaxValue,
                                     Optional ByRef Total As Integer = 0,
                                     Optional ByVal Sorts As String = "ID desc") As List(Of INS_LIST_CONTRACT_DETAILDTO)
        Try

            Dim query = From p In Context.INS_LIST_CONTRACT_DETAIL
            Dim lst = query.Select(Function(p) New INS_LIST_CONTRACT_DETAILDTO With {
                                       .ID = p.ID,
                                       .CONTRACT_INS_ID = p.CONTRACT_INS_ID,
                                       .INS_PROGRAM_ID = p.INS_PROGRAM_ID,
                                       .MONEY_INS = p.MONEY_INS})

            'If Not String.IsNullOrEmpty(_filter.CODE) Then
            '    lst = lst.Where(Function(f) f.CODE.ToLower().Contains(_filter.CODE.ToLower()))
            'End If


            Return lst.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function ModifyINS_LIST_CONTRACT(ByVal objTitle As INS_LIST_CONTRACTDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As INS_LIST_CONTRACT
        Try
            objTitleData = (From p In Context.INS_LIST_CONTRACT Where p.ID = objTitle.ID).SingleOrDefault
            objTitleData.ID = objTitle.ID
            objTitleData.CONTRACT_INS_NO = objTitle.CONTRACT_INS_NO
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.ORG_INSURANCE = objTitle.ORG_INSURANCE
            objTitleData.START_DATE = objTitle.START_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.VAL_CO = objTitle.VAL_CO
            objTitleData.BUY_DATE = objTitle.BUY_DATE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.IS_DELETED = 0
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try

    End Function

    Public Function InsertINS_LIST_CONTRACT(ByVal objTitle As INS_LIST_CONTRACTDTO, ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objTitleData As New INS_LIST_CONTRACT
        Dim iCount As Integer = 0
        Try
            objTitleData.ID = Utilities.GetNextSequence(Context, Context.INS_LIST_CONTRACT.EntitySet.Name)
            objTitleData.CONTRACT_INS_NO = objTitle.CONTRACT_INS_NO
            objTitleData.YEAR = objTitle.YEAR
            objTitleData.ORG_INSURANCE = objTitle.ORG_INSURANCE
            objTitleData.START_DATE = objTitle.START_DATE
            objTitleData.EXPIRE_DATE = objTitle.EXPIRE_DATE
            objTitleData.VAL_CO = objTitle.VAL_CO
            objTitleData.BUY_DATE = objTitle.BUY_DATE
            objTitleData.NOTE = objTitle.NOTE
            objTitleData.IS_DELETED = 0
            Context.INS_LIST_CONTRACT.AddObject(objTitleData)
            Context.SaveChanges(log)
            gID = objTitleData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
    Public Function DELETE_INS_LIST_CONTRACT(ByVal lstID As List(Of Decimal), ByVal log As UserLog) As Boolean
        Dim lstData As List(Of INS_LIST_CONTRACT)
        Try
            lstData = (From p In Context.INS_LIST_CONTRACT Where lstID.Contains(p.ID)).ToList
            For index = 0 To lstData.Count - 1
                lstData(index).IS_DELETED = 1
            Next
            Context.SaveChanges(log)
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
#End Region

#Region "System"
    Public Function AutoGenCode(ByVal firstChar As String, ByVal tableName As String, ByVal colName As String) As String
        Try
            Dim str As String
            Dim Sql = "SELECT NVL(MAX(" & colName & "), '" & firstChar & "000') FROM " & tableName & " WHERE " & colName & " LIKE '" & firstChar & "%'"
            str = Context.ExecuteStoreQuery(Of String)(Sql).FirstOrDefault
            If str = "" Then
                Return firstChar & "001"
            End If
            Dim number = Decimal.Parse(str.Substring(str.IndexOf(firstChar) + firstChar.Length))
            number = number + 1
            Dim lastChar = Format(number, "000")
            Return firstChar & lastChar
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function

    Public Function CheckExistInDatabase(ByVal lstID As List(Of Decimal), ByVal table As InsuranceCommon.TABLE_NAME) As Boolean
        Dim isExist As Boolean = False
        Dim strListID As String = lstID.Select(Function(x) x.ToString).Aggregate(Function(x, y) x & "," & y)
        Dim count As Decimal = 0
        Try
            Select Case table
                Case InsuranceCommon.TABLE_NAME.INS_REGION
                    isExist = Execute_ExistInDatabase("INS_SPECIFIED_OBJECTS", "LOCATION_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("HU_EMPLOYEE_CV", "INS_REGION_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case InsuranceCommon.TABLE_NAME.INS_WHEREHEALTH
                    isExist = Execute_ExistInDatabase("INS_INFORMATION", "HEALTH_AREA_INS_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("INS_INFOOLD", "HEWHRREGISKEY", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case InsuranceCommon.TABLE_NAME.INS_COST_FOLLOW_LEVER
                    isExist = Execute_ExistInDatabase("INS_SUN_CARE", "LEVEL_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("INS_GROUP_SUN_CARE", "COST_LEVER_ID_OLD", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    isExist = Execute_ExistInDatabase("INS_GROUP_SUN_CARE", "COST_LEVER_ID_NEW", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case InsuranceCommon.TABLE_NAME.INS_GROUP_REGIMES
                    isExist = Execute_ExistInDatabase("INS_ENTITLED_TYPE", "GROUP_ARISING_TYPE_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case InsuranceCommon.TABLE_NAME.INS_CHANGE_TYPE
                    isExist = Execute_ExistInDatabase("INS_CHANGE", "CHANGE_TYPE", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case InsuranceCommon.TABLE_NAME.INS_ENTITLED_TYPE
                    isExist = Execute_ExistInDatabase("INS_REMIGE_MANAGER", "ENTITLED_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                Case InsuranceCommon.TABLE_NAME.INS_LIST_PROGRAM
                    isExist = Execute_ExistInDatabase("INS_LIST_CONTRACT_DETAIL", "INS_PROGRAM_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If

                Case InsuranceCommon.TABLE_NAME.INS_LIST_CONTRACT
                    isExist = Execute_ExistInDatabase("INS_HEALTH_INSURANCE", "INS_CONTRACT_ID", strListID)
                    If Not isExist Then
                        Return isExist
                    End If
                    'danh mục mức đóng bảo hiểm
                Case InsuranceCommon.TABLE_NAME.INS_SPECIFIED_OBJECTS
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Return False
        End Try
    End Function

    Private Function Execute_ExistInDatabase(ByVal tableName As String, ByVal colName As String, ByVal strListID As String)
        Try
            Dim count As Decimal = 0
            Dim Sql = "SELECT COUNT(" & colName & ") FROM " & tableName & " WHERE " & colName & " IN (" & strListID & ")"
            count = Context.ExecuteStoreQuery(Of Decimal)(Sql).FirstOrDefault
            If count > 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iInsurance")
            Throw ex
        End Try
    End Function
#End Region

#Region "Validate Combobox"
    Public Function ValidateCombobox(ByVal cbxData As ComboBoxDataDTO) As Boolean
        Try
            'Danh mục loại hợp đồng
            If cbxData.GET_CONTRACTTYPE Then
                Dim ID As Decimal = cbxData.LIST_CONTRACTTYPE(0).ID
                Dim list As List(Of ContractTypeDTO) = (From p In Context.HU_CONTRACT_TYPE
                                                        Where p.ACTFLG = "A" And p.ID = ID
                                                        Order By p.NAME
                                                        Select New ContractTypeDTO With {
                                                            .ID = p.ID,
                                                            .CODE = p.CODE,
                                                            .NAME = p.NAME,
                                                            .BHXH = p.BHXH,
                                                            .BHYT = p.BHYT,
                                                            .BHTN = p.BHTN,
                                                            .PERIOD = p.PERIOD}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh sách loại chế độ
            If cbxData.GET_REMIGE_TYPE Then
                Dim ID As Decimal = cbxData.LIST_REMIGE_TYPE(0).ID
                Dim list As List(Of INS_ENTITLED_TYPE_DTO) = (From s In (From p In Context.INS_ENTITLED_TYPE
                                                                         Where p.STATUS = "A" And p.ID = ID
                                                                         Order By p.CREATED_DATE Descending)
                                                              Select New INS_ENTITLED_TYPE_DTO With {
                                                                  .ID = s.ID,
                                                                  .NAME_VN = s.NAME_VN,
                                                                  .NAME_EN = s.NAME_EN}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh sách vùng
            If cbxData.GET_LOCALTION Then
                'Dim ID As Decimal = cbxData.LIST_LOCALTION(0).ID
                'Dim list As List(Of INS_REGION_DTO) = (From p In Context.INS_REGION
                '                             Where p.ACTFLG = "A" And p.ID = ID
                '                             Order By p.CREATED_DATE Descending
                '         Select New INS_REGION_DTO With {
                '             .ID = p.ID,
                '             .REGION_NAME = p.REGION_NAME}).ToList
                'If list.Count = 0 Then
                '    Return False
                'End If
            End If

            'Danh sách nhóm hưởng bảo hiểm
            If cbxData.GET_LIST_COST_LEVER Then
                Dim ID As Decimal = cbxData.LIST_LIST_COST_LEVER(0).ID
                Dim list As List(Of INS_COST_FOLLOW_LEVERDTO) = (From p In Context.INS_COST_FOLLOW_LEVER
                                                                 Where p.ACTFLG = "A" And p.ID = ID
                                                                 Order By p.COST_NAME Descending
                                                                 Select New INS_COST_FOLLOW_LEVERDTO With {
                                                                     .ID = p.ID,
                                                                     .COST_NAME = p.COST_NAME}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'DANH MỤC CHỨC DANH
            If cbxData.GET_LIST_TITLE Then
                Dim ID As Decimal = cbxData.LIST_LIST_TITLE(0).ID
                Dim list As List(Of HU_TitleDTO) = (From p In Context.HU_TITLE
                                                    Where p.ACTFLG = "A" And p.ID = ID
                                                    Order By p.NAME_VN Descending
                                                    Select New HU_TitleDTO With {
                                                        .ID = p.ID,
                                                        .NAME_VN = p.NAME_VN}).ToList
                If List.Count = 0 Then
                    Return False
                End If
            End If

            'DANH MỤC CẤP NHÂN SỰ
            If cbxData.GET_LIST_STAFF_RANK Then
                Dim ID As Decimal = cbxData.LIST_LIST_STAFF_RANK(0).ID
                Dim list As List(Of HU_STAFF_RANKDTO) = (From p In Context.HU_STAFF_RANK
                                                         Where p.ACTFLG = "A" And p.ID = ID
                                                         Order By p.NAME Descending
                                                         Select New HU_STAFF_RANKDTO With {
                                                             .ID = p.ID,
                                                             .NAME = p.NAME}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh sách loại biến động
            If cbxData.GET_LIST_CHANGETYPE Then
                Dim ID As Decimal = cbxData.LIST_LIST_CHANGETYPE(0).ID
                Dim list As List(Of INS_CHANGE_TYPEDTO) = (From p In Context.INS_CHANGE_TYPE
                                                           Where p.ACTFLG = "A" _
                                                           And p.DISPLAY_COMBO = 1 _
                                                           And p.ID = ID
                                                           Order By p.CREATED_DATE Descending
                                                           Select New INS_CHANGE_TYPEDTO With {
                                                               .ID = p.ID,
                                                               .ARISING_NAME = p.ARISING_NAME,
                                                               .ARISING_TYPE = p.ARISING_TYPE
                                                               }).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            ' danh sách nhóm chế độ
            If cbxData.GET_LIST_GROUP_REGIMES Then
                Dim ID As Decimal = cbxData.LIST_LIST_GROUP_REGIMES(0).ID
                Dim list As List(Of INS_GROUP_REGIMESDTO) = (From p In Context.INS_GROUP_REGIMES
                                                             Where p.ACTFLG = "A" And p.ID = ID
                                                             Order By p.REGIMES_NAME Descending
                                                             Select New INS_GROUP_REGIMESDTO With {
                                                                 .ID = p.ID,
                                                                 .REGIMES_NAME = p.REGIMES_NAME}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh sách nhóm biến động 
            If cbxData.GET_LIST_ARISING_TYPE Then
                Dim ID As Decimal = cbxData.LIST_LIST_ARISING_TYPE(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From s In (From p In Context.OT_OTHER_LIST
                                                                    From t In Context.OT_OTHER_LIST_TYPE.Where(Function(t) t.ID = p.TYPE_ID)
                                                                    Where p.ACTFLG = "A" And p.ID = ID And t.CODE = "GROUP_ARISING_TYPE"
                                                                    Order By p.CREATED_DATE Descending)
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = s.p.ID,
                                                             .CODE = s.p.CODE,
                                                             .NAME_EN = s.p.NAME_EN,
                                                             .NAME_VN = s.p.NAME_VN}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            'Danh sách thành phố
            If cbxData.GET_LIST_PROVINCE Then
                Dim ID As Decimal = cbxData.LIST_LIST_PROVINCE(0).id
                Dim list As List(Of HU_PROVINCEDTO) = (From p In Context.HU_PROVINCE
                                                       Where p.ACTFLG = "A" And p.ID = ID
                                                       Order By p.CREATED_DATE Descending
                                                       Select New HU_PROVINCEDTO With {
                                                           .id = p.ID,
                                                           .code = p.CODE,
                                                           .name_en = p.NAME_EN,
                                                           .name_vn = p.NAME_VN}).ToList
                If List.Count = 0 Then
                    Return False
                End If
            End If

            ' danh sách quân huyện.
            If cbxData.GET_LIST_DISTRICT Then
                Dim ID As Decimal = cbxData.LIST_LIST_DISTRICT(0).ID
                Dim list As List(Of HU_DISTRICTDTO) = (From p In Context.HU_DISTRICT Join t In Context.HU_PROVINCE On p.PROVINCE_ID Equals t.ID
                                                       Where p.ACTFLG = "A" And p.ID = ID
                                                       Order By p.CREATED_DATE Descending
                                                       Select New HU_DISTRICTDTO With {
                                                           .ID = p.ID,
                                                           .name_en = p.NAME_EN,
                                                           .name_vn = p.NAME_VN,
                                                           .province_id = p.PROVINCE_ID}).ToList
                If List.Count = 0 Then
                    Return False
                End If
            End If

            ' Danh muc noi kham chua ben
            If cbxData.GET_LIST_INS_WHEREHEALTH Then
                Dim ID As Decimal = cbxData.LIST_LIST_INS_WHEREHEALTH(0).ID
                Dim list As List(Of INS_WHEREHEALTHDTO) = (From p In Context.INS_WHEREHEALTH
                                                           Where p.ACTFLG = "A" And p.ID = ID Order By p.CREATED_DATE Descending
                                                           Select New INS_WHEREHEALTHDTO With {
                                                               .ID = p.ID,
                                                               .NAME_VN = "[" & p.CODE & "] - " & p.NAME_VN}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            ' Tinh trang so so bhxh
            If cbxData.GET_LIST_STATUSNOBOOK Then
                Dim ID As Decimal = cbxData.LIST_LIST_STATUSNOBOOK(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And p.ID = ID And t.CODE = "STATUS_NOBOOK" Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN}).ToList
                If List.Count = 0 Then
                    Return False
                End If
            End If

            ' Tinh trang the bhyt
            If cbxData.GET_LIST_STATUSCARD Then
                Dim ID As Decimal = cbxData.LIST_LIST_STATUSCARD(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Join t In Context.OT_OTHER_LIST_TYPE On p.TYPE_ID Equals t.ID
                                                         Where p.ACTFLG = "A" And p.ID = ID And t.CODE = "STATUS_CARD" Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            ' đơn vị đóng bảo hiểm
            If cbxData.GET_LIST_ORG_ID_INS Then
                Dim ID As Decimal = cbxData.LIST_LIST_ORG_ID_INS(0).ID
                Dim list As List(Of OT_OTHERLIST_DTO) = (From p In Context.OT_OTHER_LIST Where p.TYPE_ID = 2025 And p.ID = ID
                                                         Order By p.CREATED_DATE Descending
                                                         Select New OT_OTHERLIST_DTO With {
                                                             .ID = p.ID,
                                                             .NAME_EN = p.NAME_EN,
                                                             .NAME_VN = p.NAME_VN}).ToList
                If list.Count = 0 Then
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Return False
        End Try
    End Function
#End Region

End Class
