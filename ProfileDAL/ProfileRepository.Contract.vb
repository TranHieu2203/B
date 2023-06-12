Imports System.Reflection
Imports Framework.Data
Imports Framework.Data.System.Linq.Dynamic

Partial Class ProfileRepository
#Region "evaluate"
    Public Function GetTrainingEvaluateEmp(ByVal _empId As Decimal) As List(Of TrainningEvaluateDTO)

        Try
            Dim query = From p In Context.HU_TRAININGEVALUATE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From ot In Context.PE_PERIOD.Where(Function(f) f.ID = p.EVALUATE_ID).DefaultIfEmpty
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RANK_ID).DefaultIfEmpty
                        From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.CAPACITY_ID).DefaultIfEmpty
                        Where p.EMPLOYEE_ID = _empId
            ' lọc điều kiện
            Dim trainingforeign = query.Select(Function(p) New TrainningEvaluateDTO With {
                                                .ID = p.p.ID,
                                                .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                                .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                                .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                                .ORG_ID = p.e.ID,
                                                .ORG_NAME = p.o.NAME_VN,
                                                .ORG_DESC = p.o.DESCRIPTION_PATH,
                                                .TITLE_ID = p.p.TITLE_ID,
                                                .TITLE_NAME = p.t.NAME_VN,
                                                .SIGN_DATE = p.p.SIGN_DATE,
                                                .DECISION_NO = p.p.DECISION_NO,
                                                .EFFECT_DATE = p.p.EFFECT_DATE,
                                                .EVALUATE_ID = p.p.EVALUATE_ID,
                                                .EVALUATE_NAME = p.ot.NAME,
                                                .RANK_ID = p.p.RANK_ID,
                                                .YEAR = p.p.YEAR,
                                                .REMARK = p.p.REMARK,
                                                .RANK_NAME = p.ot1.NAME_VN,
                                                .CAPACITY_ID = p.p.CAPACITY_ID,
                                                .CAPACITY_NAME = p.ot2.NAME_VN,
                                                .CONTENT = p.p.CONTENT,
                                                .CREATED_DATE = p.p.CREATED_DATE
                                                })
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
    Public Function GetTrainingEvaluate(ByVal _filter As TrainningEvaluateDTO, ByVal PageIndex As Integer,
                              ByVal PageSize As Integer,
                              ByRef Total As Integer, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc",
                              Optional ByVal log As UserLog = Nothing) As List(Of TrainningEvaluateDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            'Dim query = From p In Context.HU_CONTRACT Order By p.HU_EMPLOYEE.EMPLOYEE_CODE
            '            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
            '            From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
            '            From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
            '            From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
            '            From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
            '            From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
            '                                                           f.USERNAME = log.Username.ToUpper)

            Dim query = From p In Context.HU_TRAININGEVALUATE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From ot In Context.PE_PERIOD.Where(Function(f) f.ID = p.EVALUATE_ID).DefaultIfEmpty
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RANK_ID).DefaultIfEmpty
                        From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.CAPACITY_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                            f.USERNAME = log.Username.ToUpper)
            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            ' select thuộc tính
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.SIGN_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.SIGN_DATE = _filter.SIGN_DATE)
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN = _filter.TITLE_NAME)
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN = _filter.ORG_NAME)
            End If
            If _filter.EVALUATE_ID IsNot Nothing Then
                query = query.Where(Function(p) p.p.EVALUATE_ID = _filter.EVALUATE_ID)
            End If
            If _filter.YEAR IsNot Nothing Then
                query = query.Where(Function(p) p.p.YEAR = _filter.YEAR)
            End If
            If _filter.CONTENT IsNot Nothing Then
                query = query.Where(Function(p) p.p.CONTENT = _filter.CONTENT)
            End If

            Dim trainingforeign = query.Select(Function(p) New TrainningEvaluateDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_ID = p.p.TITLE_ID,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .SIGN_DATE = p.p.SIGN_DATE,
                                            .DECISION_NO = p.p.DECISION_NO,
                                            .EFFECT_DATE = p.p.EFFECT_DATE,
                                            .EVALUATE_ID = p.p.EVALUATE_ID,
                                            .EVALUATE_NAME = p.ot.NAME,
                                            .RANK_ID = p.p.RANK_ID,
                                            .YEAR = p.p.YEAR,
                                            .REMARK = p.p.REMARK,
                                            .RANK_NAME = p.ot1.NAME_VN,
                                            .CAPACITY_ID = p.p.CAPACITY_ID,
                                            .CAPACITY_NAME = p.ot2.NAME_VN,
                                            .CONTENT = p.p.CONTENT,
            .CREATED_DATE = p.p.CREATED_DATE
                                            })

            trainingforeign = trainingforeign.OrderBy(Sorts)
            Total = trainingforeign.Count
            trainingforeign = trainingforeign.Skip(PageIndex * PageSize).Take(PageSize)
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function InsertTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_TRAININGEVALUATE
        Try
            objContractData.ID = Utilities.GetNextSequence(Context, Context.HU_TRAININGEVALUATE.EntitySet.Name)
            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.DECISION_NO = objContract.DECISION_NO
            objContractData.SIGN_DATE = objContract.SIGN_DATE
            objContractData.EFFECT_DATE = objContract.EFFECT_DATE
            objContractData.EVALUATE_ID = objContract.EVALUATE_ID
            objContractData.RANK_ID = objContract.RANK_ID
            objContractData.CAPACITY_ID = objContract.CAPACITY_ID
            objContractData.REMARK = objContract.REMARK
            objContractData.CONTENT = objContract.CONTENT
            objContractData.YEAR = objContract.YEAR
            Context.HU_TRAININGEVALUATE.AddObject(objContractData)
            ' Phê duyệt
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function ModifyTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_TRAININGEVALUATE With {.ID = objContract.ID}
        Try
            objContractData = (From p In Context.HU_TRAININGEVALUATE Where p.ID = objContract.ID).FirstOrDefault

            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.DECISION_NO = objContract.DECISION_NO
            objContractData.SIGN_DATE = objContract.SIGN_DATE
            objContractData.EFFECT_DATE = objContract.EFFECT_DATE
            objContractData.EVALUATE_ID = objContract.EVALUATE_ID
            objContractData.RANK_ID = objContract.RANK_ID
            objContractData.CAPACITY_ID = objContract.CAPACITY_ID
            objContractData.REMARK = objContract.REMARK
            objContractData.CONTENT = objContract.CONTENT
            objContractData.YEAR = objContract.YEAR
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function GetTrainingEvaluateById(ByVal _filter As TrainningEvaluateDTO) As TrainningEvaluateDTO
        Try
            Dim query = From p In Context.HU_TRAININGEVALUATE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From ot In Context.PE_PERIOD.Where(Function(f) p.EVALUATE_ID = f.ID)
                        From ot1 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.RANK_ID).DefaultIfEmpty
                        From ot2 In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.CAPACITY_ID).DefaultIfEmpty
                        Where (p.ID = _filter.ID)
                        Select New TrainningEvaluateDTO With {.ID = p.ID,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                     .ORG_ID = e.ID,
                                                     .ORG_NAME = o.NAME_VN,
                                                     .ORG_DESC = o.DESCRIPTION_PATH,
                                                     .TITLE_NAME = t.NAME_VN,
                                                     .SIGN_DATE = p.SIGN_DATE,
                                                     .CONTENT = p.CONTENT,
                                                     .DECISION_NO = p.DECISION_NO,
                                                    .EVALUATE_ID = p.EVALUATE_ID,
                                                    .EVALUATE_NAME = ot.NAME,
                                                    .RANK_ID = p.RANK_ID,
                                                    .RANK_NAME = ot1.NAME_VN,
                                                    .CAPACITY_ID = p.CAPACITY_ID,
                                                    .CAPACITY_NAME = ot2.NAME_VN,
                                                    .YEAR = p.YEAR,
                                                    .EFFECT_DATE = p.EFFECT_DATE,
                                                    .REMARK = p.REMARK
                            }
            Dim result = query.FirstOrDefault
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function DeleteTrainingEvaluate(ByVal objContract As TrainningEvaluateDTO) As Boolean
        Dim objContractData As HU_TRAININGEVALUATE
        Try
            ' Xóa  hợp đồng
            objContractData = (From p In Context.HU_TRAININGEVALUATE Where objContract.ID = p.ID).SingleOrDefault
            If Not objContractData Is Nothing Then
                Context.HU_TRAININGEVALUATE.DeleteObject(objContractData)
                Context.SaveChanges()
                Return True
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region
#Region "TRANINGMANAGE"
    Public Function GetListTrainingManageByEmpID(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)

        Try

            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
            '                     New With {.P_USERNAME = log.Username,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = _param.IS_DISSOLVE})
            'End Using
            'Dim query = From p In Context.HU_CONTRACT Order By p.HU_EMPLOYEE.EMPLOYEE_CODE
            '            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
            '            From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
            '            From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
            '            From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
            '            From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
            '            From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
            '                                                           f.USERNAME = log.Username.ToUpper)

            Dim query = From p In Context.HU_TRAININGMANAGE.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TRAINING_ID)
            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            ' select thuộc tính
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE <= _filter.TO_DATE)
            End If
            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE = _filter.START_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If
            If _filter.DEGREE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.DEGREE_DATE = _filter.DEGREE_DATE)
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN = _filter.TITLE_NAME)
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN = _filter.ORG_NAME)
            End If
            If _filter.LOCATION IsNot Nothing Then
                query = query.Where(Function(p) p.p.LOCATION = _filter.LOCATION)
            End If
            If _filter.DEGREE_EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.DEGREE_EXPIRE_DATE = _filter.DEGREE_EXPIRE_DATE)
            End If

            Dim trainingforeign = query.Select(Function(p) New TrainningManageDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .ORG_ID = p.p.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_ID = p.p.TITLE_ID,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .START_DATE = p.p.START_DATE,
                                            .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                            .DEGREE_DATE = p.p.DEGREE_DATE,
                                            .PROGRAM_TRAINING = p.p.PROGRAM_TRAINING,
                                            .TRAINNING_ID = p.p.TRAINING_ID,
                                            .TRAINNING_NAME = p.ot.NAME_VN,
                                            .CERTIFICATE = p.p.CERTIFICATE,
                                            .UNIT = p.p.UNIT,
                                            .COST = p.p.COST,
                                            .RESULT_TRAIN = p.p.RESULT_TRAIN,
                                            .DEGREE_EXPIRE_DATE = p.p.DEGREE_EXPIRE_DATE,
                                            .REMARK = p.p.REMARK,
                                            .LOCATION = p.p.LOCATION,
                                            .CREATED_DATE = p.p.CREATED_DATE
                                            })

            trainingforeign = trainingforeign.OrderBy(Sorts)
            'Total = trainingforeign.Count
            'trainingforeign = trainingforeign.Skip(PageIndex * PageSize).Take(PageSize)
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetTrainingManage(ByVal _filter As TrainningManageDTO, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of TrainningManageDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            'Dim query = From p In Context.HU_CONTRACT Order By p.HU_EMPLOYEE.EMPLOYEE_CODE
            '            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
            '            From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
            '            From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
            '            From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
            '            From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
            '            From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
            '                                                           f.USERNAME = log.Username.ToUpper)

            Dim query = From p In Context.HU_TRAININGMANAGE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TRAINING_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                  f.USERNAME = log.Username.ToUpper)
            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            ' select thuộc tính
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE <= _filter.TO_DATE)
            End If
            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE = _filter.START_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If
            If _filter.DEGREE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.DEGREE_DATE = _filter.DEGREE_DATE)
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN = _filter.TITLE_NAME)
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN = _filter.ORG_NAME)
            End If
            If _filter.LOCATION IsNot Nothing Then
                query = query.Where(Function(p) p.p.LOCATION = _filter.LOCATION)
            End If
            If _filter.DEGREE_EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.DEGREE_EXPIRE_DATE = _filter.DEGREE_EXPIRE_DATE)
            End If

            Dim trainingforeign = query.Select(Function(p) New TrainningManageDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_ID = p.p.TITLE_ID,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .START_DATE = p.p.START_DATE,
                                            .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                            .DEGREE_DATE = p.p.DEGREE_DATE,
                                            .PROGRAM_TRAINING = p.p.PROGRAM_TRAINING,
                                            .TRAINNING_ID = p.p.TRAINING_ID,
                                            .TRAINNING_NAME = p.ot.NAME_VN,
                                            .CERTIFICATE = p.p.CERTIFICATE,
                                            .UNIT = p.p.UNIT,
                                            .COST = p.p.COST,
                                            .RESULT_TRAIN = p.p.RESULT_TRAIN,
                                            .DEGREE_EXPIRE_DATE = p.p.DEGREE_EXPIRE_DATE,
                                            .REMARK = p.p.REMARK,
                                            .LOCATION = p.p.LOCATION,
                                            .CREATED_DATE = p.p.CREATED_DATE
                                            })

            trainingforeign = trainingforeign.OrderBy(Sorts)
            Total = trainingforeign.Count
            trainingforeign = trainingforeign.Skip(PageIndex * PageSize).Take(PageSize)
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertTrainingManage(ByVal objContract As TrainningManageDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_TRAININGMANAGE
        Try
            objContractData.ID = Utilities.GetNextSequence(Context, Context.HU_TRAININGMANAGE.EntitySet.Name)
            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
            objContractData.DEGREE_DATE = objContract.DEGREE_DATE
            objContractData.PROGRAM_TRAINING = objContract.PROGRAM_TRAINING
            objContractData.TRAINING_ID = objContract.TRAINNING_ID
            objContractData.CERTIFICATE = objContract.CERTIFICATE
            objContractData.UNIT = objContract.UNIT
            objContractData.RESULT_TRAIN = objContract.RESULT_TRAIN
            objContractData.REMARK = objContract.REMARK
            objContractData.DEGREE_EXPIRE_DATE = objContract.DEGREE_EXPIRE_DATE
            objContractData.COST = objContract.COST
            objContractData.LOCATION = objContract.LOCATION

            Context.HU_TRAININGMANAGE.AddObject(objContractData)
            ' Phê duyệt
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function ModifyTrainingManage(ByVal objContract As TrainningManageDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_TRAININGMANAGE With {.ID = objContract.ID}
        Try
            objContractData = (From p In Context.HU_TRAININGMANAGE Where p.ID = objContract.ID).FirstOrDefault
            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
            objContractData.DEGREE_DATE = objContract.DEGREE_DATE
            objContractData.PROGRAM_TRAINING = objContract.PROGRAM_TRAINING
            objContractData.TRAINING_ID = objContract.TRAINNING_ID
            objContractData.CERTIFICATE = objContract.CERTIFICATE
            objContractData.UNIT = objContract.UNIT
            objContractData.RESULT_TRAIN = objContract.RESULT_TRAIN
            objContractData.REMARK = objContract.REMARK
            objContractData.DEGREE_EXPIRE_DATE = objContract.DEGREE_EXPIRE_DATE
            objContractData.COST = objContract.COST
            objContractData.LOCATION = objContract.LOCATION
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function GetTrainingManageById(ByVal _filter As TrainningManageDTO) As TrainningManageDTO
        Try
            Dim query = From p In Context.HU_TRAININGMANAGE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TRAINING_ID).DefaultIfEmpty
                        Where (p.ID = _filter.ID)
                        Select New TrainningManageDTO With {.ID = p.ID,
                                                     .START_DATE = p.START_DATE,
                                                     .EXPIRE_DATE = p.EXPIRE_DATE,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                     .ORG_ID = e.ID,
                                                     .ORG_NAME = o.NAME_VN,
                                                     .ORG_DESC = o.DESCRIPTION_PATH,
                                                     .TITLE_NAME = t.NAME_VN,
                                                     .LOCATION = p.LOCATION,
                                                     .TRAINNING_ID = p.TRAINING_ID,
                                                     .TRAINNING_NAME = ot.NAME_VN,
                                                      .DEGREE_DATE = p.DEGREE_DATE,
                                                      .PROGRAM_TRAINING = p.PROGRAM_TRAINING,
                                                    .CERTIFICATE = p.CERTIFICATE,
                                                    .UNIT = p.UNIT,
                                                    .COST = p.COST,
                                                    .RESULT_TRAIN = p.RESULT_TRAIN,
                                                    .DEGREE_EXPIRE_DATE = p.DEGREE_EXPIRE_DATE,
                                                    .REMARK = p.REMARK
                            }
            Dim result = query.FirstOrDefault
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function DeleteTrainingManage(ByVal objContract As TrainningManageDTO) As Boolean
        Dim objContractData As HU_TRAININGMANAGE
        Try
            ' Xóa  hợp đồng
            objContractData = (From p In Context.HU_TRAININGMANAGE Where objContract.ID = p.ID).SingleOrDefault
            If Not objContractData Is Nothing Then
                Context.HU_TRAININGMANAGE.DeleteObject(objContractData)
                Context.SaveChanges()
                Return True
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

#End Region
#Region "TrainingForeign"
    Public Function GetTrainingForeign(ByVal _filter As TrainningForeignDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of TrainningForeignDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            'Dim query = From p In Context.HU_CONTRACT Order By p.HU_EMPLOYEE.EMPLOYEE_CODE
            '            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
            '            From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
            '            From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
            '            From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
            '            From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
            '            From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
            '                                                           f.USERNAME = log.Username.ToUpper)

            Dim query = From p In Context.HU_TRAININGFOREIGN
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TRAINNING_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                 f.USERNAME = log.Username.ToUpper)
            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            ' select thuộc tính
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE <= _filter.TO_DATE)
            End If
            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE = _filter.START_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If
            If _filter.SIGN_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.SIGN_DATE = _filter.SIGN_DATE)
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN = _filter.TITLE_NAME)
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN.ToUpper = _filter.ORG_NAME.ToUpper)
            End If
            If _filter.LOCATION IsNot Nothing Then
                query = query.Where(Function(p) p.p.LOCATION = _filter.LOCATION)
            End If
            If _filter.CONTENT IsNot Nothing Then
                query = query.Where(Function(p) p.p.CONTENT = _filter.CONTENT)
            End If

            Dim trainingforeign = query.Select(Function(p) New TrainningForeignDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_ID = p.p.TITLE_ID,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .TRAINNING_NAME = p.ot.NAME_VN,
                                            .START_DATE = p.p.START_DATE,
                                            .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                            .SIGN_DATE = p.p.SIGN_DATE,
                                            .DECISION_NO = p.p.DECISION_NO,
                                            .LOCATION = p.p.LOCATION,
                                            .CONTENT = p.p.CONTENT,
            .CREATED_DATE = p.p.CREATED_DATE
                                            })

            trainingforeign = trainingforeign.OrderBy(Sorts)
            Total = trainingforeign.Count
            trainingforeign = trainingforeign.Skip(PageIndex * PageSize).Take(PageSize)
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function InsertTrainingForeign(ByVal objContract As TrainningForeignDTO,
                                  ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_TRAININGFOREIGN
        Try
            objContractData.ID = Utilities.GetNextSequence(Context, Context.HU_TRAININGFOREIGN.EntitySet.Name)
            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
            objContractData.DECISION_NO = objContract.DECISION_NO
            objContractData.SIGN_DATE = objContract.SIGN_DATE
            objContractData.TRAINNING_ID = objContract.TRAINNING_ID
            objContractData.TRAINNING_NAME = objContract.TRAINNING_NAME
            objContractData.LOCATION = objContract.LOCATION
            objContractData.CONTENT = objContract.CONTENT
            Context.HU_TRAININGFOREIGN.AddObject(objContractData)
            ' Phê duyệt
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function ModifyTrainingForeign(ByVal objContract As TrainningForeignDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_TRAININGFOREIGN With {.ID = objContract.ID}
        Try
            objContractData = (From p In Context.HU_TRAININGFOREIGN Where p.ID = objContract.ID).FirstOrDefault

            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
            objContractData.DECISION_NO = objContract.DECISION_NO
            objContractData.SIGN_DATE = objContract.SIGN_DATE
            objContractData.TRAINNING_ID = objContract.TRAINNING_ID
            objContractData.TRAINNING_NAME = objContract.TRAINNING_NAME
            objContractData.LOCATION = objContract.LOCATION
            objContractData.CONTENT = objContract.CONTENT
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function GetTrainingForeignById(ByVal _filter As TrainningForeignDTO) As TrainningForeignDTO
        Try
            Dim query = From p In Context.HU_TRAININGFOREIGN
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) p.TRAINNING_ID = f.ID)
                        Where (p.ID = _filter.ID)
                        Select New TrainningForeignDTO With {.ID = p.ID,
                                                     .START_DATE = p.START_DATE,
                                                     .EXPIRE_DATE = p.EXPIRE_DATE,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                     .ORG_ID = e.ID,
                                                     .ORG_NAME = o.NAME_VN,
                                                     .ORG_DESC = o.DESCRIPTION_PATH,
                                                     .TITLE_NAME = t.NAME_VN,
                                                     .SIGN_DATE = p.SIGN_DATE,
                                                     .LOCATION = p.LOCATION,
                                                      .CONTENT = p.CONTENT,
                                                      .DECISION_NO = p.DECISION_NO,
                                                      .TRAINNING_ID = p.TRAINNING_ID,
                                                      .TRAINNING_NAME = ot.NAME_VN
                            }
            Dim result = query.FirstOrDefault
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function DeleteTrainingForeign(ByVal objContract As TrainningForeignDTO) As Boolean
        Dim objContractData As HU_TRAININGFOREIGN
        Try
            ' Xóa  hợp đồng
            objContractData = (From p In Context.HU_TRAININGFOREIGN Where objContract.ID = p.ID).SingleOrDefault
            If Not objContractData Is Nothing Then
                Context.HU_TRAININGFOREIGN.DeleteObject(objContractData)
                Context.SaveChanges()
                Return True
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function
#End Region
#Region "emp bank"
    Public Function GetEmpBank(ByVal _filter As EmpBankDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "EMPLOYEE_CODE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of EmpBankDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_EMPLOYEE
                        From og In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From ti In Context.HU_TITLE.Where(Function(f) f.ID = p.TITLE_ID).DefaultIfEmpty
                        From cv In Context.HU_EMPLOYEE_CV.Where(Function(f) p.ID = f.EMPLOYEE_ID).DefaultIfEmpty
                        From b In Context.HU_BANK.Where(Function(f) f.ID = cv.BANK_ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = p.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)


            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.p.WORK_STATUS.HasValue Or
                                        (p.p.WORK_STATUS.HasValue And
                                         ((p.p.WORK_STATUS <> terID) Or (p.p.WORK_STATUS = terID And p.p.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.p.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.p.ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.p.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.ORG_NAME <> "" Then
                query = query.Where(Function(p) p.og.NAME_VN.ToUpper.Contains(_filter.ORG_NAME.ToUpper))
            End If
            If _filter.TITLE_NAME <> "" Then
                query = query.Where(Function(p) p.ti.NAME_VN.ToUpper.Contains(_filter.TITLE_NAME.ToUpper))
            End If


            If _filter.BANK_NAME <> "" Then
                query = query.Where(Function(p) p.b.NAME.ToUpper.Contains(_filter.BANK_NAME.ToUpper))
            End If
            If _filter.STK <> "" Then
                query = query.Where(Function(p) p.cv.BANK_NO.ToUpper.Contains(_filter.STK.ToUpper))
            End If
            If _filter.PERSON_INHERITANCE <> "" Then
                query = query.Where(Function(p) p.cv.PERSON_INHERITANCE.ToUpper.Contains(_filter.PERSON_INHERITANCE.ToUpper))
            End If


            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.JOIN_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.JOIN_DATE <= _filter.TO_DATE)
            End If


            ' select thuộc tính
            Dim contract = query.Select(Function(p) New EmpBankDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.p.ID,
                                            .EMPLOYEE_CODE = p.p.EMPLOYEE_CODE,
                                            .EMPLOYEE_NAME = p.p.FULLNAME_VN,
                                            .ORG_NAME = p.og.NAME_VN,
                                            .ORG_DESC = p.og.DESCRIPTION_PATH,
                                            .TITLE_NAME = p.ti.NAME_VN,
                                            .STK = p.cv.BANK_NO,
                                            .BANK_NAME = p.b.NAME,
                                            .PERSON_INHERITANCE = p.cv.PERSON_INHERITANCE})

            contract = contract.OrderBy(Sorts)
            Total = contract.Count
            contract = contract.Skip(PageIndex * PageSize).Take(PageSize)

            Return contract.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function saveEmpBank(ByVal dt As DataTable,
                                Optional ByVal log As UserLog = Nothing) As Decimal
        Try
            For Each item In dt.Rows
                Dim emp_code = item("EMPLOYEE_CODE").ToString
                Dim emp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE.ToUpper = emp_code.ToUpper Select p).FirstOrDefault
                Dim objCV = (From p In Context.HU_EMPLOYEE_CV Where p.EMPLOYEE_ID = emp.ID).FirstOrDefault
                If objCV IsNot Nothing Then
                    If Not item("STK") Is DBNull.Value Then
                        objCV.BANK_NO = item("STK")
                    End If

                    If Not item("BANK_NAME_NEW") Is DBNull.Value Then
                        Dim bank_code = item("BANK_NAME_NEW").ToString
                        Dim bank = (From p In Context.HU_BANK Where p.CODE.ToUpper = bank_code.ToUpper Select p.ID).FirstOrDefault
                        If bank <> Nothing Then
                            objCV.BANK_ID = bank
                        End If
                    End If
                    If Not item("PERSON_INHERITANCE") Is DBNull.Value Then
                        objCV.PERSON_INHERITANCE = item("PERSON_INHERITANCE")
                    End If


                End If

            Next
            Context.SaveChanges(log)
            Return 1
        Catch ex As Exception
            Return 0
        End Try
    End Function
#End Region
#Region "Contract"
    'update ngay thanh ly vao hop dong
    Public Function UpdateDateToContract(ByVal id As Decimal, ByVal day As Date, ByVal remark As String) As Boolean
        Dim objContractData As New HU_CONTRACT With {.ID = id}
        Try

            objContractData = (From p In Context.HU_CONTRACT Where p.ID = id).FirstOrDefault
            If day < objContractData.START_DATE Or day > objContractData.EXPIRE_DATE Then
                Return False
            End If
            objContractData.ID = id
            objContractData.LIQUIDATION_DATE = day
            objContractData.REMARK_LIQUIDATION = remark
            Context.SaveChanges()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'check phê duyệt và đã có đính kèm file hay chưa
    'yêu cầu nếu phê duyệt thì phải có phải đính kèm
    Public Function CheckHasFileFileContract(ByVal id As List(Of Decimal)) As Decimal
        Try
            Dim filecontracts = Context.HU_FILECONTRACT.Where(Function(p) p.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID And id.Contains(p.ID)).ToList()
            For Each filecontract As HU_FILECONTRACT In filecontracts
                If filecontract.FILENAME Is Nothing Or filecontract.FILENAME = "" Then
                    Return 1
                End If
            Next
            Return 2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'check phê duyệt và đã có đính kèm file hay chưa
    'yêu cầu nếu phê duyệt thì phải có phải đính kèm
    Public Function CheckHasFileContract(ByVal id As List(Of Decimal)) As Decimal
        Try
            Dim contracts = Context.HU_CONTRACT.Where(Function(p) p.STATUS_ID = ProfileCommon.DECISION_STATUS.WAIT_APPROVE_ID And id.Contains(p.ID)).ToList()
            For Each contract As HU_CONTRACT In contracts
                If contract.FILENAME Is Nothing Or contract.FILENAME = "" Then
                    Return 1
                End If
            Next
            Return 2
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ApproveListContract(ByVal listID As List(Of Decimal), ByVal acti As String, ByVal log As UserLog) As Boolean

        Try
            Dim item As Decimal = 0
            For idx = 0 To listID.Count - 1
                item = listID(idx)
                Dim objContract = (From p In Context.HU_CONTRACT Where item = p.ID).FirstOrDefault
                Dim type = (From p In Context.HU_CONTRACT
                            From t In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.CONTRACT_TYPE_ID).DefaultIfEmpty
                            From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.TYPE_ID).DefaultIfEmpty
                            Where p.ID = objContract.ID And o.CODE = "HD"
                            Select o.CODE
                             ).FirstOrDefault

                'Dim count = (From p In Context.HU_CONTRACT
                '             From t In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.CONTRACT_TYPE_ID).DefaultIfEmpty
                '             From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = t.TYPE_ID).DefaultIfEmpty
                '             Where p.EMPLOYEE_ID = objContract.EMPLOYEE_ID _
                '             And p.STATUS_ID = 447 And o.CODE = "HD"
                '             Select New ContractDTO With {.ID = p.ID}
                '             ).Count


                Dim objContractData As New ContractDTO
                Dim objContracttype As New HU_CONTRACT_TYPE
                objContractData.ID = item
                objContractData.CONTRACT_NO = objContract.CONTRACT_NO
                objContractData.CONTRACTTYPE_ID = objContract.CONTRACT_TYPE_ID
                objContracttype.ID = objContract.CONTRACT_TYPE_ID
                objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
                objContractData.START_DATE = objContract.START_DATE
                objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
                objContractData.REMARK = objContract.REMARK
                objContractData.SIGN_DATE = objContract.SIGN_DATE
                objContractData.SIGN_ID = objContract.SIGN_ID
                objContractData.SIGNER_NAME = objContract.SIGNER_NAME
                objContractData.SIGNER_TITLE = objContract.SIGNER_TITLE
                If objContracttype.IS_HSL = 0 Then
                    objContractData.WORKING_ID = objContract.WORKING_ID
                End If

                objContract.IS_FIRST_CT = If(type = "HD" And acti = "P", -1, 0)
                objContract.STATUS_ID = If(acti = "P", 447, 446)

                objContractData.STATUS_ID = If(acti = "P", 447, 446)
                objContractData.MORNING_START = objContract.MORNING_START
                objContractData.MORNING_STOP = objContract.MORNING_STOP
                objContractData.AFTERNOON_START = objContract.AFTERNOON_START
                objContractData.AFTERNOON_STOP = objContract.AFTERNOON_STOP
                objContractData.TITLE_ID = objContract.TITLE_ID
                objContractData.ORG_ID = objContract.ORG_ID
                'call quan ly truc tiep,doi tuong cham cong,dôi tuong lao dong,bac nhan vien
                Dim objEmployee = (From p In Context.HU_EMPLOYEE Where objContractData.EMPLOYEE_ID = p.ID).FirstOrDefault
                objContractData.DIRECT_MANAGER = objEmployee.DIRECT_MANAGER
                objContractData.STAFF_RANK_ID = objEmployee.STAFF_RANK_ID
                objContractData.OBJECT_LABOUR = objEmployee.OBJECT_LABOR
                objContractData.OBJECTTIMEKEEPING = objEmployee.OBJECTTIMEKEEPING
                objContractData.COMPANY_REG = objContract.COMPANY_REG
                If objContractData.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                    'objContractData.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                    'objContract.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                    ApproveContract(objContractData)
                    If IsFirstContract(objContractData) Then
                        InsertDecision(objContractData)
                    End If
                    '--anhvn, 2020/07/24 insert filecontract if choose status APPROVE
                    Dim conType = Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = objContract.CONTRACT_TYPE_ID).FirstOrDefault()
                    'check hop dong chinh thuc moi insert filecontract
                    'Dim typeIdHDCT As Decimal = 6359
                    'If conType IsNot Nothing AndAlso conType.TYPE_ID = typeIdHDCT Then
                    '    InsertFileContractWhenApprove(objContractData, log)
                    'End If
                    Dim Count = (From p In Context.HU_CONTRACT
                                 Where p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID And p.EMPLOYEE_ID = objContractData.EMPLOYEE_ID And p.ID <> objContractData.ID).Count
                    objContract.NUMOF_CONTRACT_SIGN = Count + 1
                End If
                Context.SaveChanges(log)
            Next

            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetContract(ByVal _filter As ContractDTO, ByVal PageIndex As Integer,
                                ByVal PageSize As Integer,
                                ByRef Total As Integer, ByVal _param As ParamDTO,
                                Optional ByVal Sorts As String = "CREATED_DATE desc",
                                Optional ByVal log As UserLog = Nothing) As List(Of ContractDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using


            Dim query = From p In Context.HU_CONTRACT Order By p.HU_EMPLOYEE.EMPLOYEE_CODE
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HUV_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
                        From ot In Context.OT_OTHER_LIST.Where(Function(f) c.TYPE_ID = f.ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From l In Context.HU_LOCATION.Where(Function(f) f.ID = p.ID_SIGN_CONTRACT).DefaultIfEmpty
                        From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
                        From w In Context.HU_WORK_PLACE.Where(Function(f) f.ID = p.WORK_PLACE_ID).DefaultIfEmpty
                        From lo In Context.HU_LOCATION.Where(Function(f) f.ID = p.COMPANY_REG).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                       f.USERNAME = log.Username.ToUpper)


            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID

            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_ID <> 0 Then
                query = query.Where(Function(p) p.e.ID = _filter.EMPLOYEE_ID)
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.CONTRACTTYPE_NAME <> "" Then
                query = query.Where(Function(p) p.c.NAME.ToUpper.Contains(_filter.CONTRACTTYPE_NAME.ToUpper))
            End If
            If _filter.COMPANY_REG_NAME <> "" Then
                query = query.Where(Function(p) p.lo.NAME_VN.ToUpper.Contains(_filter.COMPANY_REG_NAME.ToUpper))
            End If

            If _filter.SIGNER_NAME <> "" Then
                query = query.Where(Function(p) p.p.SIGNER_NAME.ToUpper.Contains(_filter.SIGNER_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE <= _filter.TO_DATE)
            End If
            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE = _filter.START_DATE)
            End If
            If _filter.EXPIRE_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.EXPIRE_DATE = _filter.EXPIRE_DATE)
            End If
            If _filter.SIGN_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.SIGN_DATE = _filter.SIGN_DATE)
            End If
            If _filter.STATUS_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.status.NAME_VN.ToUpper.Contains(_filter.STATUS_NAME.ToUpper))
            End If

            If _filter.MORNING_START IsNot Nothing Then
                query = query.Where(Function(p) p.p.MORNING_START = _filter.MORNING_START)
            End If
            If _filter.MORNING_STOP IsNot Nothing Then
                query = query.Where(Function(p) p.p.MORNING_STOP = _filter.MORNING_STOP)
            End If
            If _filter.AFTERNOON_START IsNot Nothing Then
                query = query.Where(Function(p) p.p.AFTERNOON_START = _filter.AFTERNOON_START)
            End If
            If _filter.AFTERNOON_STOP IsNot Nothing Then
                query = query.Where(Function(p) p.p.AFTERNOON_STOP = _filter.AFTERNOON_STOP)
            End If

            If _filter.THEORY_PHASE_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.p.THEORY_PHASE_FROM = _filter.THEORY_PHASE_FROM)
            End If
            If _filter.THEORY_PHASE_TO IsNot Nothing Then
                query = query.Where(Function(p) p.p.THEORY_PHASE_TO = _filter.THEORY_PHASE_TO)
            End If
            If _filter.PRACTICE_PHASE_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.p.PRACTICE_PHASE_FROM = _filter.PRACTICE_PHASE_FROM)
            End If
            If _filter.PRACTICE_PHASE_TO IsNot Nothing Then
                query = query.Where(Function(p) p.p.PRACTICE_PHASE_TO = _filter.PRACTICE_PHASE_TO)
            End If
            If _filter.IDs IsNot Nothing Then
                If _filter.IDs.Any() Then
                    query = query.Where(Function(p) _filter.IDs.Contains(p.p.ID))
                End If
            End If

            If _filter.CONTRACTTYPE_ID IsNot Nothing Then
                query = query.Where(Function(p) p.p.CONTRACT_TYPE_ID = _filter.CONTRACTTYPE_ID)
            End If

            If _filter.ORG_NAME2 <> "" Then
                query = query.Where(Function(p) p.o.ORG_NAME2.ToUpper().IndexOf(_filter.ORG_NAME2.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME3 <> "" Then
                query = query.Where(Function(p) p.o.ORG_NAME3.ToUpper().IndexOf(_filter.ORG_NAME3.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME4 <> "" Then
                query = query.Where(Function(p) p.o.ORG_NAME4.ToUpper().IndexOf(_filter.ORG_NAME4.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME5 <> "" Then
                query = query.Where(Function(p) p.o.ORG_NAME5.ToUpper().IndexOf(_filter.ORG_NAME5.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME6 <> "" Then
                query = query.Where(Function(p) p.o.ORG_NAME6.ToUpper().IndexOf(_filter.ORG_NAME6.ToUpper) >= 0)
            End If

            If _filter.ORG_NAME7 <> "" Then
                query = query.Where(Function(p) p.o.ORG_NAME7.ToUpper().IndexOf(_filter.ORG_NAME7.ToUpper) >= 0)
            End If

            ' nếu select cho màn hình RC_Contract
            If _filter.IS_RECRUITMENT Then
                query = query.Where(Function(p) p.c.IS_REQUIREMENT = -1)
            End If

            If _filter.EXPIRE_FROM IsNot Nothing Then
                query = query.Where(Function(p) p.p.EXPIRE_DATE >= _filter.EXPIRE_FROM)
            End If
            If _filter.EXPIRE_TO IsNot Nothing Then
                query = query.Where(Function(p) p.p.EXPIRE_DATE <= _filter.EXPIRE_TO)
            End If

            ' select thuộc tính
            Dim contract = query.Select(Function(p) New ContractDTO With {
                                            .ID = p.p.ID,
                                            .COMPANY_REG = p.p.COMPANY_REG,
                                            .COMPANY_REG_NAME = p.lo.NAME_VN,
                                            .NUMOF_CONTRACT_SIGN = p.p.NUMOF_CONTRACT_SIGN,
                                            .CONTRACTTYPE_ID = p.p.CONTRACT_TYPE_ID,
                                            .CONTRACTTYPE_NAME = p.c.NAME,
                                            .CONTRACT_NO = p.p.CONTRACT_NO,
                                            .START_DATE = p.p.START_DATE,
                                            .EXPIRE_DATE = p.p.EXPIRE_DATE,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .ORG_ID = p.e.ID,
                                            .LIQUIDATION_DATE = p.p.LIQUIDATION_DATE,
                                            .REMARK_LIQUIDATION = p.p.REMARK_LIQUIDATION,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_NAME2 = p.o.ORG_NAME2,
                                            .ORG_NAME3 = p.o.ORG_NAME3,
                                            .ORG_NAME4 = p.o.ORG_NAME4,
                                            .ORG_NAME5 = p.o.ORG_NAME5,
                                            .ORG_NAME6 = p.o.ORG_NAME6,
                                            .ORG_NAME7 = p.o.ORG_NAME7,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .SIGN_DATE = p.p.SIGN_DATE,
                                            .SIGNER_NAME = p.p.SIGNER_NAME,
                                            .SIGNER_TITLE = p.p.SIGNER_TITLE,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .STATUS_ID = p.p.STATUS_ID,
                                            .STATUS_NAME = p.status.NAME_VN,
                                            .STATUS_CODE = p.status.CODE,
                                            .MORNING_STOP = p.p.MORNING_STOP,
                                            .MORNING_START = p.p.MORNING_START,
                                            .AFTERNOON_START = p.p.AFTERNOON_START,
                                            .AFTERNOON_STOP = p.p.AFTERNOON_STOP,
                                            .WORK_TIME = p.p.WORK_TIME,
                                            .WORK_DES = p.p.WORK_DES,
                                            .WORK_PLACE_NAME = p.w.NAME_VN,
                                            .THEORY_PHASE_FROM = p.p.THEORY_PHASE_FROM,
                                            .THEORY_PHASE_TO = p.p.THEORY_PHASE_TO,
                                            .PRACTICE_PHASE_FROM = p.p.PRACTICE_PHASE_FROM,
                                            .PRACTICE_PHASE_TO = p.p.PRACTICE_PHASE_TO,
                                            .CONTRACTTYPE_TYPE_CODE = p.ot.CODE,
                                            .CONTRACTTYPE_CODE = p.c.CODE})

            contract = contract.OrderBy(Sorts)
            Total = contract.Count
            contract = contract.Skip(PageIndex * PageSize).Take(PageSize)

            Return contract.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function GetContractByID(ByVal _filter As ContractDTO) As ContractDTO
        Try
            Dim query = From p In Context.HU_CONTRACT
                        From e In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From staffrank In Context.HU_STAFF_RANK.Where(Function(f) e.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                        From w In Context.HU_WORKING.Where(Function(f) p.WORKING_ID = f.ID).DefaultIfEmpty
                        From sal_group In Context.PA_SALARY_GROUP.Where(Function(f) w.SAL_GROUP_ID = f.ID).DefaultIfEmpty
                        From sal_level In Context.PA_SALARY_LEVEL.Where(Function(f) w.SAL_LEVEL_ID = f.ID).DefaultIfEmpty
                        From sal_rank In Context.PA_SALARY_RANK.Where(Function(f) w.SAL_RANK_ID = f.ID).DefaultIfEmpty
                        From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = w.TAX_TABLE_ID).DefaultIfEmpty
                        From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) w.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                        From lo In Context.HU_LOCATION.Where(Function(f) f.ID = p.ID_SIGN_CONTRACT).DefaultIfEmpty
                        From lo1 In Context.HU_LOCATION.Where(Function(f) f.ID = p.COMPANY_REG).DefaultIfEmpty
                        Where p.ID = _filter.ID
                        Select New ContractDTO With {.ID = p.ID,
                                                     .COMPANY_REG = p.COMPANY_REG,
                                                        .COMPANY_REG_NAME = lo1.NAME_VN,
                                                     .CONTRACTTYPE_ID = c.ID,
                                                     .CONTRACTTYPE_NAME = c.NAME,
                                                     .CONTRACTTYPE_CODE = c.CODE,
                                                     .CONTRACT_NO = p.CONTRACT_NO,
                                                     .START_DATE = p.START_DATE,
                                                     .EXPIRE_DATE = p.EXPIRE_DATE,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                     .ORG_ID = e.ORG_ID,
                                                     .ORG_NAME = o.NAME_VN,
                                                     .ORG_DESC = o.DESCRIPTION_PATH,
                                                     .TITLE_NAME = String.Concat(t.CODE, String.Concat(" - ", t.NAME_VN)),
                                                     .SIGN_ID = p.SIGN_ID,
                                                     .SIGN_ID2 = p.SIGN_ID2,
                                                     .SIGNER_NAME2 = p.SIGNER_NAME2,
                                                     .SIGNER_TITLE2 = p.SIGNER_TITLE2,
                                                     .NAME_SIGN_CONTRACT = lo.LOCATION_VN_NAME,
                                                     .ID_SIGN_CONTRACT = p.ID_SIGN_CONTRACT,
                                                     .SIGN_DATE = p.SIGN_DATE,
                                                     .SIGNER_NAME = p.SIGNER_NAME,
                                                     .SIGNER_TITLE = p.SIGNER_TITLE,
                                                     .CREATED_DATE = p.CREATED_DATE,
                                                     .STATUS_NAME = p.OT_STATUS.NAME_VN,
                                                     .WORKING_ID = w.ID,
                                                     .DECISION_NO = w.DECISION_NO,
                                                     .REMARK = p.REMARK,
                                                     .WORK_PLACE_ID = p.WORK_PLACE_ID,
                                                     .WORK_DES = p.WORK_DES,
                                                     .WORK_TIME = p.WORK_TIME,
                                                     .SAL_BASIC = w.SAL_BASIC,
                                                     .PERCENT_SALARY = w.PERCENTSALARY,
                                                     .SAL_GROUP_ID = w.SAL_GROUP_ID,
                                                     .SAL_GROUP_NAME = sal_group.NAME,
                                                     .SAL_LEVEL_ID = w.SAL_LEVEL_ID,
                                                     .SAL_LEVEL_NAME = sal_level.NAME,
                                                     .SAL_RANK_ID = w.SAL_RANK_ID,
                                                     .SAL_RANK_NAME = sal_rank.RANK,
                                                     .STATUS_ID = p.STATUS_ID,
                                                     .STAFF_RANK_ID = e.STAFF_RANK_ID,
                                                     .STAFF_RANK_NAME = staffrank.NAME,
                                                     .WORK_STATUS = e.WORK_STATUS,
                                                     .MORNING_STOP = p.MORNING_STOP,
                                                     .MORNING_START = p.MORNING_START,
                                                     .AFTERNOON_START = p.AFTERNOON_START,
                                                     .AFTERNOON_STOP = p.AFTERNOON_STOP,
                                                     .ATTACH_FILE = p.ATTACH_FILE,
                                                     .LIQUIDATION_DATE = p.LIQUIDATION_DATE,
                                                     .THEORY_PHASE_FROM = p.THEORY_PHASE_FROM,
                                                     .THEORY_PHASE_TO = p.THEORY_PHASE_TO,
                                                     .PRACTICE_PHASE_FROM = p.PRACTICE_PHASE_FROM,
                                                     .PRACTICE_PHASE_TO = p.PRACTICE_PHASE_TO,
                                                     .FILENAME = p.FILENAME
                                                   }

            Dim result = query.FirstOrDefault
            If result IsNot Nothing Then
                If result.WORKING_ID > 0 Then
                    result.Working = (From w In Context.HU_WORKING
                                      From taxTable In Context.OT_OTHER_LIST.Where(Function(f) f.ID = w.TAX_TABLE_ID).DefaultIfEmpty
                                      From sal_type In Context.PA_SALARY_TYPE.Where(Function(f) w.SAL_TYPE_ID = f.ID).DefaultIfEmpty
                                      Where w.ID = result.WORKING_ID
                                      Select New WorkingDTO With
                    {
                                .ALLOWANCE_TOTAL = w.ALLOWANCE_TOTAL,
                                .SAL_INS = w.SAL_INS,
                                .TAX_TABLE_ID = w.TAX_TABLE_ID,
                                .SAL_TOTAL = w.SAL_TOTAL,
                                .SAL_TYPE_ID = w.SAL_TYPE_ID,
                                .SAL_BASIC = w.SAL_BASIC,
                                .SALARY_BHXH = w.SALARY_BHXH,
                            .ID = w.ID,
                            .TAX_TABLE_Name = taxTable.NAME_VN,
                            .SAL_TYPE_NAME = sal_type.NAME,
                                .PERCENT_SALARY = w.PERCENTSALARY,
                                .OTHERSALARY1 = w.OTHERSALARY1,
                                .OTHERSALARY2 = w.OTHERSALARY2,
                                .OTHERSALARY3 = w.OTHERSALARY3,
                                .DECISION_NO = w.DECISION_NO,
                                .EFFECT_DATE = w.EFFECT_DATE
                                }).FirstOrDefault
                End If
                If result.Working IsNot Nothing Then
                    result.Working.lstAllowance = (From p In Context.HU_WORKING_ALLOW
                                                   From allow In Context.HU_ALLOWANCE_LIST.Where(Function(f) f.ID = p.ALLOWANCE_LIST_ID)
                                                   Where p.HU_WORKING_ID = result.Working.ID
                                                   Select New WorkingAllowanceDTO With {.ALLOWANCE_LIST_ID = p.ALLOWANCE_LIST_ID,
                                                                                 .ALLOWANCE_LIST_NAME = allow.NAME,
                                                                                 .AMOUNT = p.AMOUNT,
                                                                                 .EFFECT_DATE = p.EFFECT_DATE,
                                                                                 .EXPIRE_DATE = p.EXPIRE_DATE,
                                                                                 .IS_INSURRANCE = p.IS_INSURRANCE}).ToList
                    If result.Working.lstAllowance IsNot Nothing AndAlso result.Working.lstAllowance.Count > 0 Then
                        result.Working.ALLOWANCE_TOTAL = result.Working.lstAllowance.Sum(Function(f) f.AMOUNT)
                    End If
                End If

            End If
            result.ListAttachFiles = (From p In Context.HU_ATTACHFILES.Where(Function(f) f.FK_ID = _filter.ID)
                                      Select New AttachFilesDTO With {.ID = p.ID,
                                                                      .FK_ID = p.FK_ID,
                                                                      .FILE_TYPE = p.FILE_TYPE,
                                                                      .FILE_PATH = p.FILE_PATH,
                                                                      .CONTROL_NAME = p.CONTROL_NAME,
                                                                      .ATTACHFILE_NAME = p.ATTACHFILE_NAME}).ToList()


            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function

    Public Function ValidateContract(ByVal sType As String, ByVal _validate As ContractDTO) As Boolean
        Try
            ' note: đồng bộ phê duyệt
            Select Case sType
                Case "EXIST_EFFECT_DATE"
                    Return (From e In Context.HU_CONTRACT
                            Where e.EMPLOYEE_ID = _validate.EMPLOYEE_ID And
                            e.START_DATE >= _validate.START_DATE And
                            e.ID <> _validate.ID And
                            e.LIQUIDATION_DATE Is Nothing And
                            e.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID).Count = 0
                Case "EXIST_CONTRACT_NO"
                    Return (From p In Context.HU_CONTRACT
                            Where p.CONTRACT_NO.ToUpper = _validate.CONTRACT_NO.ToUpper _
                            And p.ID <> _validate.ID).Count = 0
            End Select
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            ' Utility.WriteExceptionLog(ex, Me.ToString() & ".ValidateContract")
            Throw ex
        End Try
    End Function
    Public Function CheckStartDate1(ByVal objContract As ContractDTO) As Boolean
        Try
            Dim q1 = (From c In Context.HU_CONTRACT
                      Where c.EMPLOYEE_ID = objContract.EMPLOYEE_ID
                      Order By c.START_DATE Descending).FirstOrDefault
            Dim q2 = (From c In Context.HU_CONTRACT
                      Where c.EMPLOYEE_ID = objContract.EMPLOYEE_ID And c.LIQUIDATION_DATE IsNot Nothing
                      Order By c.LIQUIDATION_DATE Descending).FirstOrDefault
            Dim chkDate As Date
            If q2.LIQUIDATION_DATE Is Nothing Then
                chkDate = q1.START_DATE
            ElseIf q1.START_DATE > q2.LIQUIDATION_DATE Then
                chkDate = q1.START_DATE
            Else
                chkDate = q2.LIQUIDATION_DATE
            End If
            If objContract.START_DATE <= chkDate Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
        End Try
    End Function

    Public Function CheckStartDate(ByVal objContract As ContractDTO) As Boolean
        Try

            Dim q1 = (From c In Context.HU_CONTRACT
                      Where c.EMPLOYEE_ID = objContract.EMPLOYEE_ID And c.ID <> objContract.ID
                      Order By c.START_DATE Descending).FirstOrDefault
            'Dim q2 = (From c In Context.HU_CONTRACT
            '          Where c.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            '          Order By c.EXPIRE_DATE Descending).FirstOrDefault
            Dim chkDate As Date
            If q1.EXPIRE_DATE Is Nothing Then
                chkDate = q1.START_DATE
            Else
                chkDate = q1.EXPIRE_DATE
            End If
            If q1.LIQUIDATION_DATE IsNot Nothing Then
                chkDate = q1.LIQUIDATION_DATE
            End If
            If objContract.START_DATE <= chkDate Then
                Return True
            Else
                Return False
            End If
            'Dim q3 = (From c In Context.HU_CONTRACT
            '          Where c.EMPLOYEE_ID = objContract.EMPLOYEE_ID And objContract.START_DATE >= c.START_DATE And objContract.START_DATE <= c.EXPIRE_DATE And c.ID <> objContract.ID).Count
            'Dim q4 = (From c In Context.HU_CONTRACT
            '          Where c.EMPLOYEE_ID = objContract.EMPLOYEE_ID And objContract.EXPIRE_DATE >= c.START_DATE And objContract.EXPIRE_DATE <= c.EXPIRE_DATE And c.ID <> objContract.ID).Count
            'If q3 > 0 Or q4 > 0 Then
            '    Return True
            'Else
            '    Return False
            'End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
        End Try
    End Function

    Public Function InsertContract(ByVal objContract As ContractDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_CONTRACT
        'Dim objContracttype As New HU_CONTRACT_TYPE

        Try
            objContractData.ID = Utilities.GetNextSequence(Context, Context.HU_CONTRACT.EntitySet.Name)
            objContract.ID = objContractData.ID
            objContractData.CONTRACT_NO = objContract.CONTRACT_NO
            objContractData.CONTRACT_TYPE_ID = objContract.CONTRACTTYPE_ID
            'objContracttype.ID = objContractData.CONTRACT_TYPE_ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
            objContractData.REMARK = objContract.REMARK
            objContractData.SIGN_DATE = objContract.SIGN_DATE
            objContractData.SIGN_ID = objContract.SIGN_ID
            objContractData.SIGN_ID2 = objContract.SIGN_ID2
            objContractData.SIGNER_NAME2 = objContract.SIGNER_NAME2
            objContractData.SIGNER_TITLE2 = objContract.SIGNER_TITLE2
            objContractData.SIGNER_NAME = objContract.SIGNER_NAME
            objContractData.SIGNER_TITLE = objContract.SIGNER_TITLE
            Dim objContracttype = (From o In Context.HU_CONTRACT_TYPE
                                   Where objContract.CONTRACTTYPE_ID = o.ID
                                   Select o.IS_HSL
                                                  ).FirstOrDefault
            If objContracttype = 0 Then
                objContractData.WORKING_ID = objContract.WORKING_ID
            End If
            objContractData.ID_SIGN_CONTRACT = objContract.ID_SIGN_CONTRACT

            objContractData.COMPANY_REG = objContract.COMPANY_REG

            objContractData.STATUS_ID = objContract.STATUS_ID

            Dim Type_ID_ct = (From p In Context.HU_CONTRACT_TYPE
                              From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                              Where objContract.CONTRACTTYPE_ID = p.ID
                              Select o.CODE
                                                  ).FirstOrDefault
            If Type_ID_ct = "HD" Then
                objContractData.IS_FIRST_CT = -1
            End If

            objContractData.MORNING_START = objContract.MORNING_START
            objContractData.MORNING_STOP = objContract.MORNING_STOP
            objContractData.AFTERNOON_START = objContract.AFTERNOON_START
            objContractData.AFTERNOON_STOP = objContract.AFTERNOON_STOP
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.WORK_PLACE_ID = objContract.WORK_PLACE_ID
            objContractData.WORK_TIME = objContract.WORK_TIME
            objContractData.WORK_DES = objContract.WORK_DES
            objContractData.ATTACH_FILE = objContract.ATTACH_FILE
            objContractData.FILENAME = objContract.FILENAME
            objContractData.THEORY_PHASE_FROM = objContract.THEORY_PHASE_FROM
            objContractData.THEORY_PHASE_TO = objContract.THEORY_PHASE_TO
            objContractData.PRACTICE_PHASE_FROM = objContract.PRACTICE_PHASE_FROM
            objContractData.PRACTICE_PHASE_TO = objContract.PRACTICE_PHASE_TO
            Context.HU_CONTRACT.AddObject(objContractData)
            ' Phê duyệt
            If objContract.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                Dim NUMOF_CONTRACT_SIGN = (From p In Context.HU_CONTRACT.Where(Function(f) f.EMPLOYEE_ID = objContract.EMPLOYEE_ID And _
                                                                                           f.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID And f.ID <> objContract.ID)).Count() + 1

                objContract.NUMOF_CONTRACT_SIGN = NUMOF_CONTRACT_SIGN
                ApproveContract(objContract)

                Dim is_kn = (From p In Context.HU_EMPLOYEE Where p.ID = objContract.EMPLOYEE_ID Select p.IS_KIEM_NHIEM).FirstOrDefault

                If IsFirstContract(objContract) And is_kn Is Nothing Then
                    InsertDecision(objContract)
                End If
                'Dim conType = Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = objContractData.CONTRACT_TYPE_ID).FirstOrDefault()
                'check hop dong chinh thuc moi insert filecontract
                'Dim typeIdHDCT As Decimal = 6359
                'If conType IsNot Nothing AndAlso conType.TYPE_ID = typeIdHDCT Then
                '    InsertFileContractWhenApprove(objContract, log)
                'End If
            End If
            If objContract.ListAttachFiles IsNot Nothing Then
                For Each File As AttachFilesDTO In objContract.ListAttachFiles
                    Dim objFile As New HU_ATTACHFILES
                    objFile.ID = Utilities.GetNextSequence(Context, Context.HU_ATTACHFILES.EntitySet.Name)
                    objFile.FK_ID = objContractData.ID
                    objFile.FILE_PATH = File.FILE_PATH
                    objFile.ATTACHFILE_NAME = File.ATTACHFILE_NAME
                    objFile.CONTROL_NAME = File.CONTROL_NAME
                    objFile.FILE_TYPE = File.FILE_TYPE
                    Context.HU_ATTACHFILES.AddObject(objFile)
                Next
            End If
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Private Sub InsertFileContractWhenApprove(ByVal objContract As ContractDTO, ByVal log As UserLog)
        Try
            Dim fileContract As New HU_FILECONTRACT
            If objContract IsNot Nothing Then
                fileContract.ID = Utilities.GetNextSequence(Context, Context.HU_FILECONTRACT.EntitySet.Name)
                Dim outNum = GET_NEXT_APPENDIX_ORDER(0, objContract.ID, objContract.EMPLOYEE_ID)
                Dim order = String.Format("{0}", Format(outNum, "00"))
                fileContract.ID_CONTRACT = objContract.ID
                fileContract.START_DATE = objContract.START_DATE
                fileContract.EXPIRE_DATE = objContract.EXPIRE_DATE
                fileContract.CONTRACT_NO = objContract.CONTRACT_NO
                fileContract.SIGN_DATE = objContract.SIGN_DATE
                fileContract.SIGN_ID = objContract.SIGN_ID
                fileContract.SIGN_ORG_ID = objContract.ORG_ID
                fileContract.SIGNER_NAME = objContract.SIGNER_NAME
                fileContract.SIGNER_TITLE = objContract.SIGNER_TITLE
                fileContract.SIGN_ID2 = objContract.SIGN_ID2
                fileContract.SIGNER_NAME2 = objContract.SIGNER_NAME2
                fileContract.SIGNER_TITLE2 = objContract.SIGNER_TITLE2
                fileContract.EMP_ID = objContract.EMPLOYEE_ID
                fileContract.WORKING_ID = objContract.WORKING_ID
                fileContract.STATUS_ID = 447
                fileContract.APPEND_TYPEID = 11
                fileContract.STT = outNum
                fileContract.APPEND_NUMBER = objContract.CONTRACT_NO + "-" + order
                fileContract.CREATED_DATE = Date.Now
                fileContract.CREATED_BY = log.Username
                fileContract.CREATED_LOG = log.Username
                fileContract.MODIFIED_DATE = Date.Now
                fileContract.MODIFIED_BY = log.Username
                fileContract.MODIFIED_LOG = log.Username
                Context.HU_FILECONTRACT.AddObject(fileContract)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function IsFirstContract(ByVal contractDto As ContractDTO) As Boolean
        'dong nhat loai phe duyet là 447 nen sua lai
        Return Context.HU_CONTRACT.Count(Function(p) p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID And p.EMPLOYEE_ID = contractDto.EMPLOYEE_ID) = 0
    End Function

    Private Function IsFirstOficialContract(ByVal contractDto As ContractDTO) As Boolean
        Dim query = From p In Context.HU_CONTRACT
                    From c In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.CONTRACT_TYPE_ID).DefaultIfEmpty
                    From t In Context.OT_OTHER_LIST.Where(Function(f) f.ID = c.TYPE_ID).DefaultIfEmpty
                    Where p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID And p.EMPLOYEE_ID = contractDto.EMPLOYEE_ID _
                    And t.CODE = "HD" And p.ID <> contractDto.ID
        Return Not query.Any
    End Function

    Private Sub InsertDecision(ByVal contractDto As ContractDTO)
        Dim recruitDecision = (From otherList In Context.OT_OTHER_LIST
                               From otherListType In Context.OT_OTHER_LIST_TYPE.Where(Function(f) f.CODE = ProfileCommon.OT_DECISION_TYPE.Name And f.ID = otherList.TYPE_ID And otherList.CODE = "QDTD")
                               Select otherList).FirstOrDefault

        Dim objDecision = (From wk In Context.HU_WORKING
                           Where wk.DECISION_TYPE_ID = recruitDecision.ID _
                           And wk.EMPLOYEE_ID = contractDto.EMPLOYEE_ID And wk.IS_WAGE = 0 And wk.IS_MISSION = -1
                           Select wk).FirstOrDefault 'QDTD
        If objDecision IsNot Nothing Then
            Exit Sub
            'Context.HU_WORKING.DeleteObject(objDecision)
        End If
        'neu la HD dau tien==================================================================================
        Dim objEmployee = (From p In Context.HU_EMPLOYEE Where contractDto.EMPLOYEE_ID = p.ID).FirstOrDefault
        contractDto.DIRECT_MANAGER = objEmployee.DIRECT_MANAGER
        contractDto.STAFF_RANK_ID = objEmployee.STAFF_RANK_ID
        contractDto.OBJECT_LABOUR = objEmployee.OBJECT_LABOR
        contractDto.OBJECTTIMEKEEPING = objEmployee.OBJECTTIMEKEEPING
        contractDto.OBJECT_EMPLOYEE_ID = objEmployee.OBJECT_EMPLOYEE_ID
        contractDto.OBJECT_ATTENDANT_ID = objEmployee.OBJECT_ATTENDANT_ID
        'contractDto.WORK_PLACE_ID = objEmployee.WORK_PLACE_ID
        '======================================================================================================
        Dim objContracttype = (From p In Context.HU_CONTRACT
                               From o In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.CONTRACT_TYPE_ID).DefaultIfEmpty
                               Where contractDto.CONTRACTTYPE_ID = o.ID
                               Select o.IS_HSL
                                                        ).FirstOrDefault
        If objContracttype = 0 Then
            Dim updateWorking = (From wk In Context.HU_WORKING
                                 Where (wk.STATUS_ID = 447 Or wk.STATUS_ID = 446) And wk.IS_WAGE = -1 And wk.IS_MISSION = 0 And wk.EMPLOYEE_ID = contractDto.EMPLOYEE_ID Order By wk.EFFECT_DATE Descending Select wk).FirstOrDefault
            Dim result = (From p In Context.HU_WORKING_ALLOW
                          From allow In Context.HU_ALLOWANCE_LIST.Where(Function(f) f.ID = p.ALLOWANCE_LIST_ID)
                          Where p.HU_WORKING_ID = contractDto.WORKING_ID
                          Select New WorkingAllowanceDTO With {.AMOUNT = p.AMOUNT,
                                                                .IS_INSURRANCE = p.IS_INSURRANCE}).ToList
            result.Sum(Function(f) f.AMOUNT)
            Context.HU_WORKING.AddObject(New HU_WORKING() With {.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING.EntitySet.Name),
                                     .ORG_ID = contractDto.ORG_ID,
                                     .TITLE_ID = contractDto.TITLE_ID,
                                     .EMPLOYEE_ID = contractDto.EMPLOYEE_ID,
                                     .EFFECT_DATE = contractDto.START_DATE,
                                     .STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID,
                                     .SIGN_ID = contractDto.SIGN_ID,
                                     .SIGN_TITLE = contractDto.SIGNER_TITLE,
                                     .SIGN_DATE = contractDto.SIGN_DATE,
                                      .DIRECT_MANAGER = contractDto.DIRECT_MANAGER,
                                     .STAFF_RANK_ID = contractDto.STAFF_RANK_ID,
                                     .OBJECT_LABOR = contractDto.OBJECT_LABOUR,
                                     .SIGN_NAME = contractDto.SIGNER_NAME,
                                     .ALLOWANCE_TOTAL = result.Sum(Function(f) f.AMOUNT),
                                     .SAL_INS = updateWorking.SAL_INS,
                                    .OBJECT_ATTENDANCE = contractDto.OBJECTTIMEKEEPING,
                                     .TAX_TABLE_ID = updateWorking.TAX_TABLE_ID,
                                     .SAL_TOTAL = updateWorking.SAL_TOTAL,
                                     .SAL_TYPE_ID = updateWorking.SAL_TYPE_ID,
                                      .SAL_BASIC = updateWorking.SAL_BASIC,
                                     .DECISION_TYPE_ID = recruitDecision.ID,
                                    .IS_MISSION = -1,
                                    .IS_3B = 0,
                                    .IS_PROCESS = -1,
                                     .IS_WAGE = 0,
                                     .OBJECT_ATTENDANT_ID = contractDto.OBJECT_ATTENDANT_ID,
                                     .OBJECT_EMPLOYEE_ID = contractDto.OBJECT_EMPLOYEE_ID,
                                     .WORK_PLACE_ID = objEmployee.WORK_PLACE_ID
                                     })
        Else
            Context.HU_WORKING.AddObject(New HU_WORKING() With {.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING.EntitySet.Name),
                                             .ORG_ID = contractDto.ORG_ID,
                                             .TITLE_ID = contractDto.TITLE_ID,
                                             .EMPLOYEE_ID = contractDto.EMPLOYEE_ID,
                                             .EFFECT_DATE = contractDto.START_DATE,
                                             .STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID,
                                             .SIGN_ID = contractDto.SIGN_ID,
                                             .SIGN_TITLE = contractDto.SIGNER_TITLE,
                                             .SIGN_DATE = contractDto.SIGN_DATE,
                                              .DIRECT_MANAGER = contractDto.DIRECT_MANAGER,
                                             .STAFF_RANK_ID = contractDto.STAFF_RANK_ID,
                                             .OBJECT_LABOR = contractDto.OBJECT_LABOUR,
                                             .SIGN_NAME = contractDto.SIGNER_NAME,
                                            .DECISION_TYPE_ID = recruitDecision.ID,
                                            .IS_MISSION = -1,
                                            .IS_3B = 0,
                                            .IS_PROCESS = -1,
                                             .IS_WAGE = 0,
                                             .OBJECT_ATTENDANT_ID = contractDto.OBJECT_ATTENDANT_ID,
                                             .OBJECT_EMPLOYEE_ID = contractDto.OBJECT_EMPLOYEE_ID,
                                             .WORK_PLACE_ID = objEmployee.WORK_PLACE_ID
                                             })
        End If

    End Sub

    Private Sub InsertDecision_Background(ByVal contractDto As ContractDTO)
        Dim _context As New ProfileContext
        Dim recruitDecision = (From otherList In _context.OT_OTHER_LIST
                               From otherListType In _context.OT_OTHER_LIST_TYPE.Where(Function(f) f.CODE = ProfileCommon.OT_DECISION_TYPE.Name And f.ID = otherList.TYPE_ID And otherList.CODE = "QDTD")
                               Select otherList).FirstOrDefault

        Dim objDecision = (From wk In _context.HU_WORKING
                           Where wk.DECISION_TYPE_ID = recruitDecision.ID _
                           And wk.EMPLOYEE_ID = contractDto.EMPLOYEE_ID And wk.IS_WAGE = 0 And wk.IS_MISSION = -1
                           Select wk).FirstOrDefault 'QDTD
        If objDecision IsNot Nothing Then
            Exit Sub
            '_context.HU_WORKING.DeleteObject(objDecision)
        End If
        'neu la HD dau tien==================================================================================
        Dim objEmployee = (From p In _context.HU_EMPLOYEE Where contractDto.EMPLOYEE_ID = p.ID).FirstOrDefault
        contractDto.DIRECT_MANAGER = objEmployee.DIRECT_MANAGER
        contractDto.STAFF_RANK_ID = objEmployee.STAFF_RANK_ID
        contractDto.OBJECT_LABOUR = objEmployee.OBJECT_LABOR
        contractDto.OBJECTTIMEKEEPING = objEmployee.OBJECTTIMEKEEPING
        contractDto.OBJECT_EMPLOYEE_ID = objEmployee.OBJECT_EMPLOYEE_ID
        contractDto.OBJECT_ATTENDANT_ID = objEmployee.OBJECT_ATTENDANT_ID
        'contractDto.WORK_PLACE_ID = objEmployee.WORK_PLACE_ID
        '======================================================================================================

        Dim objContracttype = (From p In Context.HU_CONTRACT
                               From o In Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = p.CONTRACT_TYPE_ID).DefaultIfEmpty
                               Where contractDto.CONTRACTTYPE_ID = o.ID
                               Select o.IS_HSL
                                                        ).FirstOrDefault
        If objContracttype = 0 Then
            Dim updateWorking = (From wk In Context.HU_WORKING
                                 Where (wk.STATUS_ID = 447 Or wk.STATUS_ID = 446) And wk.IS_WAGE = -1 And wk.IS_MISSION = 0 And wk.EMPLOYEE_ID = contractDto.EMPLOYEE_ID Order By wk.EFFECT_DATE Descending Select wk).FirstOrDefault
            Dim result = (From p In Context.HU_WORKING_ALLOW
                          From allow In Context.HU_ALLOWANCE_LIST.Where(Function(f) f.ID = p.ALLOWANCE_LIST_ID)
                          Where p.HU_WORKING_ID = contractDto.WORKING_ID
                          Select New WorkingAllowanceDTO With {.AMOUNT = p.AMOUNT,
                                                                .IS_INSURRANCE = p.IS_INSURRANCE}).ToList
            result.Sum(Function(f) f.AMOUNT)
            Context.HU_WORKING.AddObject(New HU_WORKING() With {.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING.EntitySet.Name),
                                     .ORG_ID = contractDto.ORG_ID,
                                     .TITLE_ID = contractDto.TITLE_ID,
                                     .EMPLOYEE_ID = contractDto.EMPLOYEE_ID,
                                     .EFFECT_DATE = contractDto.START_DATE,
                                     .STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID,
                                     .SIGN_ID = contractDto.SIGN_ID,
                                     .SIGN_TITLE = contractDto.SIGNER_TITLE,
                                     .SIGN_DATE = contractDto.SIGN_DATE,
                                      .DIRECT_MANAGER = contractDto.DIRECT_MANAGER,
                                     .STAFF_RANK_ID = contractDto.STAFF_RANK_ID,
                                     .OBJECT_LABOR = contractDto.OBJECT_LABOUR,
                                     .SIGN_NAME = contractDto.SIGNER_NAME,
                                     .ALLOWANCE_TOTAL = result.Sum(Function(f) f.AMOUNT),
                                     .SAL_INS = updateWorking.SAL_INS,
                                    .OBJECT_ATTENDANCE = contractDto.OBJECTTIMEKEEPING,
                                     .TAX_TABLE_ID = updateWorking.TAX_TABLE_ID,
                                     .SAL_TOTAL = updateWorking.SAL_TOTAL,
                                     .SAL_TYPE_ID = updateWorking.SAL_TYPE_ID,
                                      .SAL_BASIC = updateWorking.SAL_BASIC,
                                     .DECISION_TYPE_ID = recruitDecision.ID,
                                    .IS_MISSION = -1,
                                    .IS_3B = 0,
                                    .IS_PROCESS = -1,
                                     .IS_WAGE = 0,
                                     .OBJECT_ATTENDANT_ID = contractDto.OBJECT_ATTENDANT_ID,
                                     .OBJECT_EMPLOYEE_ID = contractDto.OBJECT_EMPLOYEE_ID,
                                     .WORK_PLACE_ID = objEmployee.WORK_PLACE_ID
                                     })
        Else
            Context.HU_WORKING.AddObject(New HU_WORKING() With {.ID = Utilities.GetNextSequence(Context, Context.HU_WORKING.EntitySet.Name),
                                             .ORG_ID = contractDto.ORG_ID,
                                             .TITLE_ID = contractDto.TITLE_ID,
                                             .EMPLOYEE_ID = contractDto.EMPLOYEE_ID,
                                             .EFFECT_DATE = contractDto.START_DATE,
                                             .STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID,
                                             .SIGN_ID = contractDto.SIGN_ID,
                                             .SIGN_TITLE = contractDto.SIGNER_TITLE,
                                             .SIGN_DATE = contractDto.SIGN_DATE,
                                              .DIRECT_MANAGER = contractDto.DIRECT_MANAGER,
                                             .STAFF_RANK_ID = contractDto.STAFF_RANK_ID,
                                             .OBJECT_LABOR = contractDto.OBJECT_LABOUR,
                                             .SIGN_NAME = contractDto.SIGNER_NAME,
                                            .DECISION_TYPE_ID = recruitDecision.ID,
                                            .IS_MISSION = -1,
                                            .IS_3B = 0,
                                            .IS_PROCESS = -1,
                                             .IS_WAGE = 0,
                                             .OBJECT_ATTENDANT_ID = contractDto.OBJECT_ATTENDANT_ID,
                                             .OBJECT_EMPLOYEE_ID = contractDto.OBJECT_EMPLOYEE_ID,
                                             .WORK_PLACE_ID = objEmployee.WORK_PLACE_ID
                                             })
        End If

        _context.SaveChanges()
    End Sub

    Public Function ModifyContract(ByVal objContract As ContractDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_CONTRACT With {.ID = objContract.ID}
        Try
            objContractData = (From p In Context.HU_CONTRACT Where p.ID = objContract.ID).FirstOrDefault

            objContractData.ID = objContract.ID
            objContractData.CONTRACT_NO = objContract.CONTRACT_NO
            objContractData.CONTRACT_TYPE_ID = objContract.CONTRACTTYPE_ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.EXPIRE_DATE = objContract.EXPIRE_DATE
            objContractData.STATUS_ID = objContract.STATUS_ID

            Dim Type_ID_ct = (From p In Context.HU_CONTRACT_TYPE
                              From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                              Where objContract.CONTRACTTYPE_ID = p.ID
                              Select o.CODE
                                                  ).FirstOrDefault
            If Type_ID_ct = "HD" Then
                objContractData.IS_FIRST_CT = -1
            End If


            objContractData.REMARK = objContract.REMARK
            objContractData.SIGN_DATE = objContract.SIGN_DATE
            objContractData.SIGN_ID = objContract.SIGN_ID
            objContractData.SIGN_ID2 = objContract.SIGN_ID2
            objContractData.SIGNER_NAME2 = objContract.SIGNER_NAME2
            objContractData.SIGNER_TITLE2 = objContract.SIGNER_TITLE2
            objContractData.SIGNER_NAME = objContract.SIGNER_NAME
            objContractData.SIGNER_TITLE = objContract.SIGNER_TITLE
            objContractData.START_DATE = objContract.START_DATE
            objContractData.WORKING_ID = objContract.WORKING_ID
            objContractData.ID_SIGN_CONTRACT = objContract.ID_SIGN_CONTRACT
            objContractData.MORNING_START = objContract.MORNING_START
            objContractData.MORNING_STOP = objContract.MORNING_STOP
            objContractData.AFTERNOON_START = objContract.AFTERNOON_START
            objContractData.AFTERNOON_STOP = objContract.AFTERNOON_STOP
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.WORK_PLACE_ID = objContract.WORK_PLACE_ID
            objContractData.WORK_TIME = objContract.WORK_TIME
            objContractData.WORK_DES = objContract.WORK_DES
            objContractData.ATTACH_FILE = objContract.ATTACH_FILE
            objContractData.FILENAME = objContract.FILENAME
            objContractData.THEORY_PHASE_FROM = objContract.THEORY_PHASE_FROM
            objContractData.THEORY_PHASE_TO = objContract.THEORY_PHASE_TO
            objContractData.PRACTICE_PHASE_FROM = objContract.PRACTICE_PHASE_FROM
            objContractData.PRACTICE_PHASE_TO = objContract.PRACTICE_PHASE_TO

            objContractData.COMPANY_REG = objContract.COMPANY_REG

            ' Phê duyệt
            If objContract.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID Then
                'Dim NUMOF_CONTRACT_SIGN = (From p In Context.HU_CONTRACT.Where(Function(f) f.EMPLOYEE_ID = objContract.EMPLOYEE_ID And _
                '                                                                          f.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID And f.ID <> objContract.ID)).Count() + 1

                'objContract.NUMOF_CONTRACT_SIGN = NUMOF_CONTRACT_SIGN
                ApproveContract(objContract)
                If IsFirstContract(objContract) Then
                    InsertDecision(objContract)
                End If
                '--anhvn, 2020/07/24 insert filecontract if choose status APPROVE
                Dim conType = Context.HU_CONTRACT_TYPE.Where(Function(f) f.ID = objContractData.CONTRACT_TYPE_ID).FirstOrDefault()
                'check hop dong chinh thuc moi insert filecontract
                'Dim typeIdHDCT As Decimal = 6359
                'If conType IsNot Nothing AndAlso conType.TYPE_ID = typeIdHDCT Then
                '    InsertFileContractWhenApprove(objContract, log)
                'End If
            End If
            'xoa nhung file attach cu
            Dim lstAtt = (From p In Context.HU_ATTACHFILES Where p.FK_ID = objContractData.ID).ToList()
            For index = 0 To lstAtt.Count - 1
                Context.HU_ATTACHFILES.DeleteObject(lstAtt(index))
            Next

            For Each File As AttachFilesDTO In objContract.ListAttachFiles
                Dim objFile As New HU_ATTACHFILES
                objFile.ID = Utilities.GetNextSequence(Context, Context.HU_ATTACHFILES.EntitySet.Name)
                objFile.FK_ID = objContractData.ID
                objFile.FILE_PATH = File.FILE_PATH
                objFile.ATTACHFILE_NAME = File.ATTACHFILE_NAME
                objFile.CONTROL_NAME = File.CONTROL_NAME
                objFile.FILE_TYPE = File.FILE_TYPE
                Context.HU_ATTACHFILES.AddObject(objFile)
            Next
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function

    Private Function IsFirstContractMax(ByVal contractDto As ContractDTO) As Boolean
        'dong nhat loai phe duyet là 447 nen sua lai
        Return Context.HU_CONTRACT.Count(Function(p) p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID And p.EMPLOYEE_ID = contractDto.EMPLOYEE_ID And p.START_DATE > contractDto.START_DATE) = 0
    End Function

    Public Sub ApproveContract(ByVal objContract As ContractDTO)
        Try
            If Format(objContract.START_DATE, "yyyyMMdd") > Format(Date.Now, "yyyyMMdd") Then
                Exit Sub
            End If
            ' Update hợp đồng mới nhất sang employee
            Dim Employee As HU_EMPLOYEE = (From p In Context.HU_EMPLOYEE Where p.ID = objContract.EMPLOYEE_ID).FirstOrDefault
            If IsFirstContractMax(objContract) Then
                Employee.CONTRACT_ID = objContract.ID
                Employee.CONTRACTED_UNIT = objContract.COMPANY_REG
            End If
            Employee.MODIFIED_DATE = Date.Now
            Dim lstCtrTypeAllow As New List(Of Decimal)
            lstCtrTypeAllow.Add(6)
            lstCtrTypeAllow.Add(7)
            lstCtrTypeAllow.Add(12)
            lstCtrTypeAllow.Add(13)
            lstCtrTypeAllow.Add(14)
            lstCtrTypeAllow.Add(287)
            Dim Type_ID_ct = (From p In Context.HU_CONTRACT_TYPE
                              From o In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                              Where objContract.CONTRACTTYPE_ID = p.ID
                              Select o.CODE
                                                  ).FirstOrDefault
            If Type_ID_ct = "HD" Then
                Dim empOther As HU_EMPLOYEE_CV = (From p In Context.HU_EMPLOYEE_CV
                                                  Where p.EMPLOYEE_ID = objContract.EMPLOYEE_ID).FirstOrDefault

                If empOther IsNot Nothing And objContract.START_DATE <= DateTime.Now Then
                    empOther.DOAN_PHI = True
                End If
            End If

            ' Update trạng thái Đang làm việc
            Employee.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.WORKING_ID

            Dim STR As ContractTypeDTO = (From p In Context.HU_CONTRACT_TYPE
                                          From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                                          Where p.ID = objContract.CONTRACTTYPE_ID
                                          Select New ContractTypeDTO With {
                                     .CODE = ot.CODE
                                     }).FirstOrDefault
            Dim checkFirstContract = (From p In Context.HU_CONTRACT Where p.EMPLOYEE_ID = objContract.EMPLOYEE_ID AndAlso
                                     p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID AndAlso p.ID <> objContract.ID).Any
            If Not checkFirstContract Then
                Employee.JOIN_DATE = objContract.START_DATE
                If Employee.COPORATION_DATE Is Nothing Then
                    Employee.COPORATION_DATE = objContract.START_DATE
                End If
            End If
            Dim checkLastestContract = (From p In Context.HU_CONTRACT Where p.EMPLOYEE_ID = objContract.EMPLOYEE_ID AndAlso
                                     p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID AndAlso p.ID <> objContract.ID AndAlso p.START_DATE > objContract.START_DATE).Any

            If Not checkLastestContract Then
                If STR.CODE = "HD" Then
                    Employee.EMP_STATUS = 9
                ElseIf STR.CODE = "HDTV" Then
                    Employee.EMP_STATUS = 8
                ElseIf STR.CODE = "HDTVU" Then
                    Employee.EMP_STATUS = 7
                ElseIf STR.CODE = "HDCTV" Then
                    Employee.EMP_STATUS = 6
                End If
            End If
            If Type_ID_ct = "HD" Then 'Type_ID_ct = "HDTV" Or
                If IsFirstOficialContract(objContract) Then
                    Employee.JOIN_DATE_STATE = objContract.START_DATE
                End If
                If Employee.COPORATION_DATE Is Nothing Then
                    If Employee.SENIORITY_DATE Is Nothing Then
                        Employee.SENIORITY_DATE = objContract.START_DATE
                    End If
                End If
            End If
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Sub

    Public Sub ApproveContract_Background(ByVal objContract As ContractDTO)
        Try
            If Format(objContract.START_DATE, "yyyyMMdd") > Format(Date.Now, "yyyyMMdd") Then
                Exit Sub
            End If
            ' Update hợp đồng mới nhất sang employee
            Dim _context As New ProfileContext
            Dim Employee As HU_EMPLOYEE = (From p In _context.HU_EMPLOYEE Where p.ID = objContract.EMPLOYEE_ID).FirstOrDefault
            Employee.CONTRACT_ID = objContract.ID
            Employee.MODIFIED_DATE = Date.Now
            Dim lstCtrTypeAllow As New List(Of Decimal)
            lstCtrTypeAllow.Add(6)
            lstCtrTypeAllow.Add(7)
            lstCtrTypeAllow.Add(12)
            lstCtrTypeAllow.Add(13)
            lstCtrTypeAllow.Add(14)
            lstCtrTypeAllow.Add(287)
            Dim Type_ID_ct = (From p In _context.HU_CONTRACT_TYPE
                              From o In _context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                              Where objContract.CONTRACTTYPE_ID = p.ID
                              Select o.CODE
                                                  ).FirstOrDefault
            'If Employee.JOIN_DATE_STATE Is Nothing And Type_ID_ct = "HD" Then
            If Type_ID_ct = "HD" Then
                'Employee.JOIN_DATE_STATE = objContract.START_DATE
                Dim empOther As HU_EMPLOYEE_CV = (From p In _context.HU_EMPLOYEE_CV
                                                  Where p.EMPLOYEE_ID = objContract.EMPLOYEE_ID).FirstOrDefault

                If empOther IsNot Nothing And objContract.START_DATE <= DateTime.Now Then
                    empOther.DOAN_PHI = True
                End If
            End If

            ' Update trạng thái Đang làm việc
            Employee.WORK_STATUS = ProfileCommon.OT_WORK_STATUS.WORKING_ID

            Dim STR As ContractTypeDTO = (From p In _context.HU_CONTRACT_TYPE
                                          From ot In _context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID).DefaultIfEmpty
                                          Where p.ID = objContract.CONTRACTTYPE_ID
                                          Select New ContractTypeDTO With {
                                     .CODE = ot.CODE
                                     }).FirstOrDefault

            Dim working = (From p In Context.HU_WORKING Where p.ID = objContract.WORKING_ID).FirstOrDefault
            Dim empType = (From p In Context.OT_OTHER_LIST Where p.ID = working.EMPLOYEE_TYPE).FirstOrDefault
            Dim checkFirstContract = (From p In Context.HU_CONTRACT Where p.EMPLOYEE_ID = objContract.EMPLOYEE_ID AndAlso
                                     p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID AndAlso p.ID <> objContract.ID).Any
            If Not checkFirstContract Then
                Employee.JOIN_DATE = objContract.START_DATE
                Employee.EMP_STATUS = 9
                If Employee.COPORATION_DATE Is Nothing Then
                    Employee.COPORATION_DATE = objContract.START_DATE
                End If
            End If
            If Type_ID_ct = "HDTV" Or Type_ID_ct = "HD" Then
                If IsFirstOficialContract(objContract) Then
                    Employee.JOIN_DATE_STATE = objContract.START_DATE
                End If
                If Employee.COPORATION_DATE Is Nothing Then
                    If Employee.SENIORITY_DATE Is Nothing Then
                        Employee.SENIORITY_DATE = objContract.START_DATE
                    End If
                End If
            End If
            If empType.CODE = "CT" Then
                If IsFirstOficialContract(objContract) Then
                    Employee.JOIN_DATE_STATE = objContract.START_DATE
                    CALL_ENTITLEMENT_EMP("CONTRACT_AUTO", objContract.START_DATE.Value.Year, objContract.EMPLOYEE_ID)
                End If
            End If
            Dim checkLastestContract = (From p In Context.HU_CONTRACT Where p.EMPLOYEE_ID = objContract.EMPLOYEE_ID AndAlso
                                     p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID AndAlso p.ID <> objContract.ID AndAlso p.START_DATE > objContract.START_DATE).Any

            If Not checkLastestContract Then
                If STR.CODE = "HD" Then
                    Employee.EMP_STATUS = 9
                ElseIf STR.CODE = "HDTV" Then
                    Employee.EMP_STATUS = 8
                ElseIf STR.CODE = "HDTVU" Then
                    Employee.EMP_STATUS = 7
                ElseIf STR.CODE = "HDCTV" Then
                    Employee.EMP_STATUS = 6
                End If
            End If
            _context.SaveChanges()
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Sub

    Public Function DeleteContract(ByVal objContract As ContractDTO) As Boolean
        Dim objContractData As HU_CONTRACT
        Try
            ' Xóa  hợp đồng
            objContractData = (From p In Context.HU_CONTRACT Where objContract.ID = p.ID).SingleOrDefault
            If Not objContractData Is Nothing Then
                Context.HU_CONTRACT.DeleteObject(objContractData)
                Context.SaveChanges()
                Return True
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CALL_ENTITLEMENT_EMP(ByVal P_USERNAME As String, ByVal P_YEAR As Decimal, ByVal P_EMP_ID As Decimal) As Boolean
        Try
            Using cls As New DataAccess.NonQueryData
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.CALL_ENTITLEMENT_EMP",
                                               New With {.P_USERNAME = P_USERNAME,
                                                         .P_YEAR = P_YEAR,
                                                         .P_EMP_ID = P_EMP_ID})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iTime")
            Throw ex

        End Try
    End Function

    Public Function CreateContractNo(ByVal objContract As ContractDTO) As String
        Try
            Dim employeeCode As String = objContract.EMPLOYEE_CODE.Trim.ToUpper
            If employeeCode.Length < 2 Or objContract.CONTRACTTYPE_ID < 1 Then
                Return String.Empty
            End If
            'If Context.HU_CONTRACT_TYPE.Any(Function(f) f.ID = objContract.CONTRACTTYPE_ID And
            '                                    f.CODE = ProfileCommon.ContractType.Probation) Then
            '    Return String.Empty
            'End If
            'Dim query = (From c In Context.HU_CONTRACT
            '             From type In Context.HU_CONTRACT_TYPE.Where(Function(p) p.CODE <> ProfileCommon.ContractType.Probation And
            '              p.ID = c.CONTRACT_TYPE_ID)
            '             Where c.EMPLOYEE_ID = objContract.EMPLOYEE_ID And c.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID)
            'Dim no = query.Count
            'If employeeCode.StartsWith("e") Or employeeCode.StartsWith("E") Then
            '    employeeCode = employeeCode.Substring(1)
            'End If
            'Return String.Format("{0}-{1:0#} / HDLD-TMF", employeeCode, no + 1)
            Dim str As String
            Dim nameTypeContract As ContractTypeDTO = (From p In Context.HU_CONTRACT_TYPE
                                                       From ot In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.TYPE_ID)
                                                       Where p.ID = objContract.CONTRACTTYPE_ID
                                                       Select New ContractTypeDTO With {
                                                .ID = p.ID,
                                                .CODE = ot.CODE}).FirstOrDefault()
            Dim codeLocation As LocationDTO = (From p In Context.HU_LOCATION Where p.ID = objContract.ID_SIGN_CONTRACT
                                               Select New LocationDTO With {
                                                     .ID = p.ID,
                                                     .CODE = p.CODE}).FirstOrDefault()
            If nameTypeContract.CODE = "HDTV" Then
                str = employeeCode.ToString + "/".ToString() + Year(objContract.START_DATE).ToString() + "/" + "HDTV".ToString() + If(codeLocation.CODE IsNot Nothing, "/".ToString(), Nothing) + codeLocation.CODE
            Else
                str = employeeCode.ToString + "/".ToString() + Year(objContract.START_DATE).ToString() + "/" + "HDLD".ToString() + If(codeLocation.CODE IsNot Nothing, "/".ToString(), Nothing) + codeLocation.CODE
            End If
            Dim query = (From ct In Context.HU_CONTRACT
                         Where ct.EMPLOYEE_ID = objContract.EMPLOYEE_ID And ct.CONTRACT_TYPE_ID = objContract.CONTRACTTYPE_ID)
            Dim no = query.Count
            Return String.Format("{0}", str)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckContractNo(ByVal objContract As ContractDTO) As Boolean
        Try
            Dim query = (From p In Context.HU_CONTRACT
                         Where p.CONTRACT_NO = objContract.CONTRACT_NO And
                         p.ID <> objContract.ID).FirstOrDefault
            Return (query Is Nothing)
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetContractEmployeeByID(ByVal gID As Decimal) As EmployeeDTO
        Dim obj As EmployeeDTO
        Try
            obj = (From p In Context.HU_EMPLOYEE
                   From staffrank In Context.HU_STAFF_RANK.Where(Function(f) p.STAFF_RANK_ID = f.ID).DefaultIfEmpty
                   From o In Context.HU_ORGANIZATION.Where(Function(f) p.ORG_ID = f.ID)
                   From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID)
                   From working In Context.HU_WORKING.Where(Function(f) f.ID = p.LAST_WORKING_ID).DefaultIfEmpty
                   Where p.ID = gID
                   Select New EmployeeDTO With {
                       .ID = p.ID,
                       .EMPLOYEE_CODE = p.EMPLOYEE_CODE,
                       .FULLNAME_VN = p.FULLNAME_VN,
                       .ORG_ID = p.ORG_ID,
                       .ORG_NAME = o.NAME_VN,
                       .ORG_CODE = o.CODE,
                       .TITLE_ID = t.ID,
                       .TITLE_NAME_VN = String.Concat(t.CODE, String.Concat(" - ", t.NAME_VN)),
                       .JOIN_DATE = p.JOIN_DATE,
                       .DIRECT_MANAGER = p.DIRECT_MANAGER,
                       .OBJECTTIMEKEEPING = p.OBJECTTIMEKEEPING,
                       .STAFF_RANK_ID = p.STAFF_RANK_ID,
                       .OBJECT_LABOR = p.OBJECT_LABOR,
                       .STAFF_RANK_NAME = staffrank.NAME,
                       .WORK_STATUS = p.WORK_STATUS,
                       .TER_EFFECT_DATE = p.TER_EFFECT_DATE,
                       .LAST_WORKING_ID = p.LAST_WORKING_ID,
                       .SAL_BASIC = working.SAL_BASIC,
                       .WORK_PLACE_ID = p.WORK_PLACE_ID,
                       .SALARY_BHXH = working.SALARY_BHXH,
                       .COST_SUPPORT = working.COST_SUPPORT}).FirstOrDefault

            Dim ctract = (From p In Context.HU_CONTRACT
                          Where p.EMPLOYEE_ID = gID And p.STATUS_ID = ProfileCommon.DECISION_STATUS.APPROVE_ID
                          Order By p.START_DATE Descending).FirstOrDefault
            If ctract IsNot Nothing Then
                obj.CONTRACT_ID = ctract.ID
                obj.CONTRACT_NO = ctract.CONTRACT_NO
                obj.CONTRACT_EFFECT_DATE = ctract.START_DATE
                obj.CONTRACT_EXPIRE_DATE = ctract.EXPIRE_DATE
            End If
            Return obj

        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function GetMaxConId(empId As Decimal) As Decimal
        Try
            Dim chuoi As Decimal = (From p In Context.HU_CONTRACT
                                    From o In Context.HU_EMPLOYEE.Where(Function(f) p.EMPLOYEE_ID = f.ID).DefaultIfEmpty
                                    Where p.EMPLOYEE_ID = empId
                                    Order By p.START_DATE Descending
                                    Select p.ID).FirstOrDefault


            Return chuoi
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
        End Try
    End Function
    Public Function UnApproveContract(ByVal objContract As ContractDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_CONTRACT With {.ID = objContract.ID}
        Try
            objContractData = (From p In Context.HU_CONTRACT Where p.ID = objContract.ID).FirstOrDefault
            objContractData.STATUS_ID = objContract.STATUS_ID
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function GetCheckContractTypeID(ByVal listID As String) As DataTable
        Using cls As New DataAccess.QueryData
            Dim dtData As DataTable = cls.ExecuteStore("PKG_PROFILE_BUSINESS.GET_CHECK_TYPE_CONTRACT",
                                           New With {.P_LIST_ID = listID,
                                                     .PV_CUR = cls.OUT_CURSOR})
            Return dtData
        End Using
        Return Nothing
    End Function

    Public Function checkFromDate(ByVal objContract As ContractDTO) As Boolean
        Try
            Dim query = (From p In Context.HU_CONTRACT
                         Where p.EMPLOYEE_ID = objContract.EMPLOYEE_ID And p.EXPIRE_DATE <= objContract.START_DATE).FirstOrDefault
            Return query Is Nothing
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

#End Region

#Region "Manage annual leave plans (ALP)"
    Public Function GetListALPByEmpID(ByVal _filter As TrainningManageDTO, ByVal _param As ParamDTO,
                              Optional ByVal Sorts As String = "CREATED_DATE desc") As List(Of TrainningManageDTO)

        Try

            'Using cls As New DataAccess.QueryData
            '    cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
            '                     New With {.P_USERNAME = log.Username,
            '                               .P_ORGID = _param.ORG_ID,
            '                               .P_ISDISSOLVE = _param.IS_DISSOLVE})
            'End Using
            'Dim query = From p In Context.HU_CONTRACT Order By p.HU_EMPLOYEE.EMPLOYEE_CODE
            '            From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
            '            From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
            '            From c In Context.HU_CONTRACT_TYPE.Where(Function(f) p.CONTRACT_TYPE_ID = f.ID)
            '            From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
            '            From status In Context.OT_OTHER_LIST.Where(Function(f) f.ID = p.STATUS_ID).DefaultIfEmpty
            '            From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
            '                                                           f.USERNAME = log.Username.ToUpper)

            Dim query = From p In Context.HU_ANNUALLEAVE_PLANS.Where(Function(f) f.EMPLOYEE_ID = _filter.EMPLOYEE_ID)
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            ' select thuộc tính
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE <= _filter.TO_DATE)
            End If
            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE = _filter.START_DATE)
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN = _filter.TITLE_NAME)
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN = _filter.ORG_NAME)
            End If

            Dim trainingforeign = query.Select(Function(p) New TrainningManageDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .ORG_ID = p.p.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_ID = p.p.TITLE_ID,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .START_DATE = p.p.START_DATE,
                                            .EXPIRE_DATE = p.p.END_DATE,
                                            .REMARK = p.p.REMARK,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .WORK_STATUS = p.p.YEAR,
                                            .COST = p.p.DAY_NUMBER
                                            })

            trainingforeign = trainingforeign.OrderBy(Sorts)
            'Total = trainingforeign.Count
            'trainingforeign = trainingforeign.Skip(PageIndex * PageSize).Take(PageSize)
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function GetALP(ByVal _filter As TrainningManageDTO, ByVal PageIndex As Integer,
                               ByVal PageSize As Integer,
                               ByRef Total As Integer, ByVal _param As ParamDTO,
                               Optional ByVal Sorts As String = "CREATED_DATE desc",
                               Optional ByVal log As UserLog = Nothing) As List(Of TrainningManageDTO)

        Try

            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_COMMON_LIST.INSERT_CHOSEN_ORG",
                                 New With {.P_USERNAME = log.Username,
                                           .P_ORGID = _param.ORG_ID,
                                           .P_ISDISSOLVE = _param.IS_DISSOLVE})
            End Using
            Dim query = From p In Context.HU_ANNUALLEAVE_PLANS
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        From chosen In Context.SE_CHOSEN_ORG.Where(Function(f) f.ORG_ID = e.ORG_ID And
                                                                  f.USERNAME = log.Username.ToUpper)
            ' lọc điều kiện
            Dim dateNow = Date.Now.Date
            Dim terID = ProfileCommon.OT_WORK_STATUS.TERMINATE_ID
            If Not _filter.IS_TER Then
                query = query.Where(Function(p) Not p.e.WORK_STATUS.HasValue Or
                                        (p.e.WORK_STATUS.HasValue And
                                         ((p.e.WORK_STATUS <> terID) Or (p.e.WORK_STATUS = terID And p.e.TER_EFFECT_DATE > dateNow))))

            End If
            ' select thuộc tính
            If _filter.EMPLOYEE_CODE <> "" Then
                query = query.Where(Function(p) p.e.EMPLOYEE_CODE.ToUpper.Contains(_filter.EMPLOYEE_CODE.ToUpper))
            End If
            If _filter.EMPLOYEE_NAME <> "" Then
                query = query.Where(Function(p) p.e.FULLNAME_VN.ToUpper.Contains(_filter.EMPLOYEE_NAME.ToUpper))
            End If
            If _filter.FROM_DATE IsNot Nothing And _filter.TO_DATE Is Nothing Then
                query = query.Where(Function(p) p.p.START_DATE >= _filter.FROM_DATE)
            End If
            If _filter.TO_DATE IsNot Nothing And _filter.FROM_DATE Is Nothing Then
                query = query.Where(Function(p) p.p.START_DATE <= _filter.TO_DATE)
            End If

            If _filter.FROM_DATE IsNot Nothing And _filter.TO_DATE IsNot Nothing Then
                query = query.Where(Function(p) (_filter.FROM_DATE >= p.p.START_DATE And p.p.END_DATE >= _filter.FROM_DATE) Or (_filter.TO_DATE >= p.p.START_DATE And p.p.END_DATE >= _filter.TO_DATE))
            End If


            If _filter.START_DATE IsNot Nothing Then
                query = query.Where(Function(p) p.p.START_DATE = _filter.START_DATE)
            End If
            If _filter.TITLE_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.t.NAME_VN = _filter.TITLE_NAME)
            End If
            If _filter.ORG_NAME IsNot Nothing Then
                query = query.Where(Function(p) p.o.NAME_VN = _filter.ORG_NAME)
            End If
            Dim trainingforeign = query.Select(Function(p) New TrainningManageDTO With {
                                            .ID = p.p.ID,
                                            .EMPLOYEE_ID = p.p.EMPLOYEE_ID,
                                            .EMPLOYEE_NAME = p.e.FULLNAME_VN,
                                            .EMPLOYEE_CODE = p.e.EMPLOYEE_CODE,
                                            .ORG_ID = p.e.ID,
                                            .ORG_NAME = p.o.NAME_VN,
                                            .ORG_DESC = p.o.DESCRIPTION_PATH,
                                            .TITLE_ID = p.p.TITLE_ID,
                                            .TITLE_NAME = p.t.NAME_VN,
                                            .START_DATE = p.p.START_DATE,
                                            .EXPIRE_DATE = p.p.END_DATE,
                                            .REMARK = p.p.REMARK,
                                            .CREATED_DATE = p.p.CREATED_DATE,
                                            .WORK_STATUS = p.p.YEAR,
                                            .COST = p.p.DAY_NUMBER
                                            })

            trainingforeign = trainingforeign.OrderBy(Sorts)
            Total = trainingforeign.Count
            trainingforeign = trainingforeign.Skip(PageIndex * PageSize).Take(PageSize)
            Return trainingforeign.ToList
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function InsertALP(ByVal objContract As TrainningManageDTO,
                                 ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_ANNUALLEAVE_PLANS
        Try
            objContractData.ID = Utilities.GetNextSequence(Context, Context.HU_ANNUALLEAVE_PLANS.EntitySet.Name)
            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.END_DATE = objContract.EXPIRE_DATE
            objContractData.DAY_NUMBER = objContract.COST
            objContractData.REMARK = objContract.REMARK
            objContractData.YEAR = objContract.WORK_STATUS
            Context.HU_ANNUALLEAVE_PLANS.AddObject(objContractData)
            ' Phê duyệt
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function ModifyALP(ByVal objContract As TrainningManageDTO,
                                   ByVal log As UserLog, ByRef gID As Decimal) As Boolean
        Dim objContractData As New HU_ANNUALLEAVE_PLANS With {.ID = objContract.ID}
        Try
            objContractData = (From p In Context.HU_ANNUALLEAVE_PLANS Where p.ID = objContract.ID).FirstOrDefault
            objContract.ID = objContractData.ID
            objContractData.EMPLOYEE_ID = objContract.EMPLOYEE_ID
            objContractData.TITLE_ID = objContract.TITLE_ID
            objContractData.ORG_ID = objContract.ORG_ID
            objContractData.START_DATE = objContract.START_DATE
            objContractData.END_DATE = objContract.EXPIRE_DATE
            objContractData.REMARK = objContract.REMARK
            objContractData.DAY_NUMBER = objContract.COST
            objContractData.YEAR = objContract.WORK_STATUS
            Context.SaveChanges(log)
            gID = objContractData.ID
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Return False
        End Try

    End Function
    Public Function GetALPById(ByVal _filter As TrainningManageDTO) As TrainningManageDTO
        Try
            Dim query = From p In Context.HU_ANNUALLEAVE_PLANS
                        From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = p.EMPLOYEE_ID)
                        From o In Context.HU_ORGANIZATION.Where(Function(f) f.ID = p.ORG_ID).DefaultIfEmpty
                        From t In Context.HU_TITLE.Where(Function(f) p.TITLE_ID = f.ID).DefaultIfEmpty
                        Where (p.ID = _filter.ID)
                        Select New TrainningManageDTO With {.ID = p.ID,
                                                     .START_DATE = p.START_DATE,
                                                     .EXPIRE_DATE = p.END_DATE,
                                                     .EMPLOYEE_ID = p.EMPLOYEE_ID,
                                                     .EMPLOYEE_CODE = e.EMPLOYEE_CODE,
                                                     .EMPLOYEE_NAME = e.FULLNAME_VN,
                                                     .ORG_ID = e.ID,
                                                     .ORG_NAME = o.NAME_VN,
                                                     .ORG_DESC = o.DESCRIPTION_PATH,
                                                     .TITLE_NAME = t.NAME_VN,
                                                    .COST = p.DAY_NUMBER,
                                                    .REMARK = p.REMARK,
                                                    .WORK_STATUS = p.YEAR
                            }
            Dim result = query.FirstOrDefault
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try


    End Function
    Public Function DeleteALP(ByVal objContract As TrainningManageDTO) As Boolean
        Dim objContractData As HU_ANNUALLEAVE_PLANS
        Try
            ' Xóa  hợp đồng
            objContractData = (From p In Context.HU_ANNUALLEAVE_PLANS Where objContract.ID = p.ID).SingleOrDefault
            If Not objContractData Is Nothing Then
                Context.HU_ANNUALLEAVE_PLANS.DeleteObject(objContractData)
                Context.SaveChanges()
                Return True
            End If
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try

    End Function

    Public Function CheckEmployee_Exits(ByVal empCode As String) As Integer
        Dim objEmp As HU_EMPLOYEE
        Dim result As Integer
        Try
            objEmp = (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = empCode.Replace(" ", "") And p.IS_KIEM_NHIEM Is Nothing).FirstOrDefault
            If objEmp IsNot Nothing Then
                result = objEmp.ID
            Else
                result = 0
            End If
            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function ImportAnnualLeave(ByVal P_DOCXML As String, ByVal P_USER As String, ByVal P_YEAR As Decimal) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_ATTENDANCE_LIST.IMPORT_ANNUALLEAVE_PLANS",
                                 New With {.P_DOCXML = P_DOCXML, .P_USER = P_USER, .P_YEAR = P_YEAR})
            End Using
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "PLHD"
    Public Function GetContractForm(ByVal formID As Decimal) As OtherListDTO
        Try
            Dim query = (From p In Context.OT_OTHER_LIST Where p.ID = formID
                         Select New OtherListDTO With {.ID = p.ID,
                                                       .CODE = p.CODE,
                                                       .NAME_VN = p.NAME_VN}).FirstOrDefault
            Return query
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GET_PROCESS_PLCONTRACT(ByVal P_EMP_CODE As String) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.GET_PROCESS_PLCONTRACT",
                                           New With {.P_EMPID = P_EMP_CODE,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function EXPORT_PLHD(ByVal _param As ParamDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.EXPORT_PLHD",
                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                     .P_ORGID = _param.ORG_ID,
                                                     .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR}, False) ' FALSE : no datatable

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function EXPORT_CONGNO(ByVal _param As ParamDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.EXPORT_CONGNO",
                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                     .P_ORGID = _param.ORG_ID,
                                                     .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR}, False) ' FALSE : no datatable

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function EXPORT_CV(ByVal _param As ParamDTO, ByVal log As UserLog) As DataSet
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataSet = cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.EXPORT_CV",
                                           New With {.P_USERNAME = log.Username.ToUpper,
                                                     .P_ORGID = _param.ORG_ID,
                                                     .P_ISDISSOLVE = _param.IS_DISSOLVE,
                                                     .P_CUR = cls.OUT_CURSOR,
                                                     .P_CUR1 = cls.OUT_CURSOR,
                                                     .P_CUR2 = cls.OUT_CURSOR,
                                                     .P_CUR3 = cls.OUT_CURSOR,
                                                     .P_CUR4 = cls.OUT_CURSOR,
                                                     .P_CUR5 = cls.OUT_CURSOR,
                                                     .P_CUR6 = cls.OUT_CURSOR,
                                                     .P_CUR7 = cls.OUT_CURSOR,
                                                     .P_CUR8 = cls.OUT_CURSOR,
                                                     .P_CUR9 = cls.OUT_CURSOR,
                                                     .P_CUR10 = cls.OUT_CURSOR,
                                                     .P_CUR11 = cls.OUT_CURSOR,
                                                     .P_CUR12 = cls.OUT_CURSOR,
                                                     .P_CUR13 = cls.OUT_CURSOR,
                                                     .P_CUR14 = cls.OUT_CURSOR,
                                                     .P_CUR15 = cls.OUT_CURSOR,
                                                     .P_CUR16 = cls.OUT_CURSOR,
                                                     .P_CUR17 = cls.OUT_CURSOR,
                                                     .P_CUR18 = cls.OUT_CURSOR,
                                                     .P_CUR19 = cls.OUT_CURSOR,
                                                     .P_CUR20 = cls.OUT_CURSOR,
                                                     .P_CUR21 = cls.OUT_CURSOR,
                                                     .P_CUR22 = cls.OUT_CURSOR,
                                                     .P_CUR23 = cls.OUT_CURSOR,
                                                     .P_CUR24 = cls.OUT_CURSOR}, False) ' FALSE : no datatable

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CHECK_EMPLOYEE(ByVal P_EMP_CODE As String) As Integer
        Try
            Dim result As Integer
            If (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = P_EMP_CODE AndAlso p.IS_KIEM_NHIEM Is Nothing).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CHECK_CONTRACT(ByVal P_ID As Decimal) As Integer
        Try
            Dim result As Integer
            If (From p In Context.HU_CONTRACT Where p.ID = P_ID).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function CHECK_CONTRACT_BY_EMP_CODE(ByVal P_ID As Decimal, ByVal P_EMPID As Decimal) As Integer
        Try
            Dim result As Integer
            If (From p In Context.HU_CONTRACT Where p.ID = P_ID And p.EMPLOYEE_ID = P_EMPID).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function
    Public Function CHECK_SALARY(ByVal P_ID As Decimal) As Integer
        Try
            Dim result As Integer
            If (From p In Context.HU_WORKING Where p.ID = P_ID).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CHECK_CONTRACT_EXITS(ByVal P_CONTRACT As Decimal, ByVal P_EMP_CODE As String, ByVal P_DATE As Date) As Integer
        Try
            Dim result As Integer
            If (From p In Context.HU_FILECONTRACT
                From c In Context.HU_CONTRACT.Where(Function(f) f.ID = p.ID_CONTRACT)
                From e In Context.HU_EMPLOYEE.Where(Function(f) f.ID = c.EMPLOYEE_ID)
                Where p.ID_CONTRACT = P_CONTRACT And p.START_DATE = P_DATE And e.EMPLOYEE_CODE = P_EMP_CODE).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function CHECK_SIGN(ByVal P_EMP_CODE As String) As Integer
        Try
            Dim result As Integer
            'If (From p In Context.HU_SIGNER Where p.SIGNER_CODE = P_EMP_CODE).Count > 0 Then
            If (From p In Context.HU_EMPLOYEE Where p.EMPLOYEE_CODE = P_EMP_CODE).Count > 0 Then
                result = 1
            Else
                result = 0
            End If

            Return result
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function

    Public Function INPORT_PLHD(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.INPORT_PLHD",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
            Return False
        End Try
    End Function

    Public Function IMPORT_CONGNO(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.IMPORT_CONGNO",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
            Return False
        End Try
    End Function

    Public Function IMPORT_NV(ByVal P_DOCXML As String, ByVal log As UserLog) As Boolean
        Try
            Using cls As New DataAccess.QueryData
                cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.IMPORT_NV",
                                 New With {.P_DOCXML = P_DOCXML,
                                           .P_USERNAME = log.Username})
            End Using
            Return True
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
            Return False
        End Try
    End Function

    Public Function GET_PROCESS_PLCONTRACT_PORTAL(ByVal P_EMP_ID As Decimal) As DataTable
        Try
            Using cls As New DataAccess.QueryData
                Dim dtData As DataTable = cls.ExecuteStore("PKG_HU_IPROFILE_EMPLOYEE.GET_PROCESS_PLCONTRACT_PORTAL",
                                           New With {.P_EMPID = P_EMP_ID,
                                                     .P_CUR = cls.OUT_CURSOR})

                Return dtData
            End Using
        Catch ex As Exception
            WriteExceptionLog(ex, MethodBase.GetCurrentMethod.Name, "iProfile")
            Throw ex
        End Try
    End Function


#End Region
End Class
