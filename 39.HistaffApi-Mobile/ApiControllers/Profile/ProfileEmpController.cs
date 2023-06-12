using HiStaffAPI.AppCommon.PortalModel;
using HiStaffAPI.AppConstants;
using HiStaffAPI.AppHelpers;
using HiStaffAPI.Extension;
using HiStaffAPI.ProfileBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace HiStaffAPI.ApiControllers.Profile
{
    public partial class ProfileController 
    {
        #region Hồ sơ nhân sự

        [HttpGet]
        [Route(ApiName.GetEmployeeImage)]
        public async Task<IHttpActionResult> GetEmployeeImageAsync(GetEmployeeImageRequest request)
        {
            var response = new BaseJsonResponse<object>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();
                var data = await profileBusinessClient.GetEmployeeImageAsync(request);
                response.Status = true;
                response.Message = request.sError;
                response.Data = new
                {
                    Image = data.GetEmployeeImageResult,
                    Error = data.sError
                };
                response.Error = HttpStatusCode.OK.ToString();
                return Json(response);
            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetEmployeeByID)]
        public async Task<IHttpActionResult> GetEmployeeByIDAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<EmployeeDTO>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetEmployeeByIDAsync(request.EmployeeId);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);

            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetContractProcess)]
        public async Task<IHttpActionResult> GetContractProccessAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<ContractDTO>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetContractProccessAsync(request.EmployeeId);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetCommendProcess)]
        public async Task<IHttpActionResult> GetCommendProccessAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<CommendDTO>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetCommendProccessAsync(request.EmployeeId);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetDisciplineProcess)]
        public async Task<IHttpActionResult> GetDisciplineProccessAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<DisciplineDTO>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetDisciplineProccessAsync(request.EmployeeId);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);

            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetPositionProcess)]
        public async Task<IHttpActionResult> GetPositionProccessAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<WorkingDTO>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetWorkingProccessAsync(request.EmployeeId, GetUserLog(RequestHelper.GetTokenInfo()));
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetProcessTraining)]
        public async Task<IHttpActionResult> GetProcessTrainingAsync(GetProcessTrainingRequest request)
        {
            var response = new BaseJsonResponse<List<HU_PRO_TRAIN_OUT_COMPANYDTO>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();
                var data = await profileBusinessClient.GetProcessTrainingAsync(request);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.GetProcessTrainingResult;
                //response.Total = data.Total;
                return Json(response);

            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetEmployeeFamily)]
        public async Task<IHttpActionResult> GetEmployeeFamilyAsync(BaseRequest<FamilyDTO> request)
        {
            var response = new BaseJsonResponse<List<FamilyDTO>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetEmployeeFamilyAsync(request.Filter);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetWorkingBefore)]
        public async Task<IHttpActionResult> GetWorkingBeforeAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<List<WorkingBeforeDTO>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetWorkingBeforeAsync(request.EmployeeId);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);

            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetEmployeeAllByID)]
        public async Task<IHttpActionResult> GetEmployeeAllByIDAsync(GetEmployeeAllByIDRequest request)
        {
            var response = new BaseJsonResponse<object>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetEmployeeAllByIDAsync(request);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = new
                {
                    EmployeeCv = data.empCV,
                    EmployeeEdu = data.empEdu,
                    EmployeeHealth = data.empHealth
                };
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }


        [HttpPost]
        [Route(ApiName.ValidateEmployee)]
        public async Task<IHttpActionResult> ValidateEmployeeAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.ValidateEmployeeAsync(request.EmployeeCode, request.StringValue, request.Type);
                response.Status = data;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.SendEmployeeEdit)]
        public async Task<IHttpActionResult> SendEmployeeEditAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await profileBusinessClient.SendEmployeeEditAsync(request.Ids, userLog);
                response.Status = data;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetEmployeeByEmployeeID)]
        public async Task<IHttpActionResult> GetEmployeeByEmployeeIDAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<EmployeeDTO>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                //var data =await profileBusinessClient.GetEmployeeByEmployeeIDAsync(request.EmployeeId, userLog);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                //response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetEmployeeEditByID)]
        public async Task<IHttpActionResult> GetEmployeeEditByIDAsync(BaseRequest<EmployeeEditDTO> request)
        {
            var response = new BaseJsonResponse<EmployeeEditDTO>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetEmployeeEditByIDAsync(request.Filter);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.InsertEmployeeEdit)]
        public async Task<IHttpActionResult> InsertEmployeeEditAsync(InsertEmployeeEditRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                request.log = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await profileBusinessClient.InsertEmployeeEditAsync(request);
                response.Status = data.InsertEmployeeEditResult;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.InsertEmployeeEditResult;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.ModifyEmployeeEdit)]
        public async Task<IHttpActionResult> ModifyEmployeeEditAsync(ModifyEmployeeEditRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                request.log = userLog;
                var data = await profileBusinessClient.ModifyEmployeeEditAsync(request);
                response.Status = data.ModifyEmployeeEditResult;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.ModifyEmployeeEditResult;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.CheckExistWorkingBeforeEdit)]
        public async Task<IHttpActionResult> CheckExistWorkingBeforeEditAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<WorkingBeforeDTOEdit>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.CheckExistWorkingBeforeEditAsync(request.Id);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.InsertWorkingBeforeEdit)]
        public async Task<IHttpActionResult> InsertWorkingBeforeEditAsync(InsertWorkingBeforeEditRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                request.log = userLog;
                var data = await profileBusinessClient.InsertWorkingBeforeEditAsync(request);
                response.Status = data.InsertWorkingBeforeEditResult;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.InsertWorkingBeforeEditResult;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.ModifyWorkingBeforeEdit)]
        public async Task<IHttpActionResult> ModifyWorkingBeforeEditAsync(ModifyWorkingBeforeEditRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                request.log = userLog;
                var data = await profileBusinessClient.ModifyWorkingBeforeEditAsync(request);
                response.Status = data.ModifyWorkingBeforeEditResult;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.ModifyWorkingBeforeEditResult;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetWorkingBeforeEdit)]
        public async Task<IHttpActionResult> GetWorkingBeforeEditAsync(BaseRequest<WorkingBeforeDTOEdit> request)
        {
            var response = new BaseJsonResponse<List<WorkingBeforeDTOEdit>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetWorkingBeforeEditAsync(request.Filter);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.SendWorkingBeforeEdit)]
        public async Task<IHttpActionResult> SendWorkingBeforeEditAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await profileBusinessClient.SendWorkingBeforeEditAsync(request.Ids, userLog);
                response.Status = data;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.CheckExistProcessTrainingEdit)]
        public async Task<IHttpActionResult> CheckExistProcessTrainingEditAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<HU_PRO_TRAIN_OUT_COMPANYDTOEDIT>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await profileBusinessClient.CheckExistProcessTrainingEditAsync(request.Id);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {
                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.InsertProcessTrainingEdit)]
        public async Task<IHttpActionResult> InsertProcessTrainingEditAsync(InsertProcessTrainingEditRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                request.log = userLog;
                var data = await profileBusinessClient.InsertProcessTrainingEditAsync(request);
                response.Status = data.InsertProcessTrainingEditResult;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.InsertProcessTrainingEditResult;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.ModifyProcessTrainingEdit)]
        public async Task<IHttpActionResult> ModifyProcessTrainingEditAsync(ModifyProcessTrainingEditRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                request.log = userLog;
                var data = await profileBusinessClient.ModifyProcessTrainingEditAsync(request);
                response.Status = data.ModifyProcessTrainingEditResult;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.ModifyProcessTrainingEditResult;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetProcessTrainingEdit)]
        public async Task<IHttpActionResult> GetProcessTrainingEditAsync(BaseRequest<HU_PRO_TRAIN_OUT_COMPANYDTOEDIT> request)
        {
            var response = new BaseJsonResponse<List<HU_PRO_TRAIN_OUT_COMPANYDTOEDIT>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetProcessTrainingEditAsync(request.Filter);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.SendProcessTrainingEdit)]
        public async Task<IHttpActionResult> SendProcessTrainingEditAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await profileBusinessClient.SendProcessTrainingEditAsync(request.Ids, userLog);
                response.Status = data;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.CheckExistFamilyEdit)]
        public async Task<IHttpActionResult> CheckExistFamilyEditAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<FamilyEditDTO>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await profileBusinessClient.CheckExistFamilyEditAsync(request.Id);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.InsertEmployeeFamilyEdit)]
        public async Task<IHttpActionResult> InsertEmployeeFamilyEditAsync(InsertEmployeeFamilyEditRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                request.log = userLog;
                var data = await profileBusinessClient.InsertEmployeeFamilyEditAsync(request);
                response.Status = data.InsertEmployeeFamilyEditResult;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.InsertEmployeeFamilyEditResult;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.ModifyEmployeeFamilyEdit)]
        public async Task<IHttpActionResult> ModifyEmployeeFamilyEditAsync(ModifyEmployeeFamilyEditRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());

                request.log = userLog;
                var data = await profileBusinessClient.ModifyEmployeeFamilyEditAsync(request);
                response.Status = data.ModifyEmployeeFamilyEditResult;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.ModifyEmployeeFamilyEditResult;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetEmployeeFamilyEdit)]
        public async Task<IHttpActionResult> GetEmployeeFamilyEditAsync(BaseRequest<FamilyEditDTO> request)
        {
            var response = new BaseJsonResponse<List<FamilyEditDTO>>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetEmployeeFamilyEditAsync(request.Filter);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.SendEmployeeFamilyEdit)]
        public async Task<IHttpActionResult> SendEmployeeFamilyEditAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var userLog = GetUserLog(RequestHelper.GetTokenInfo());
                var data = await profileBusinessClient.SendEmployeeFamilyEditAsync(request.Ids, userLog);
                response.Status = data;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpGet]
        [Route(ApiName.GetEmployeeFamilyEditByID)]
        public async Task<IHttpActionResult> GetEmployeeFamilyEditByIDAsync(BaseRequest request)
        {
            var response = new BaseJsonResponse<FamilyDTO>();
            try
            {
                using var profileBusinessClient = new ProfileBusinessClient();

                var data = await profileBusinessClient.GetEmployeeFamilyEditAsync(new FamilyEditDTO() { ID = request.Id });
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = data.ToList().ToList<FamilyDTO>().FirstOrDefault();
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        [HttpPost]
        [Route(ApiName.CheckFamilyCMND)]
        public async Task<IHttpActionResult> CheckFamilyCMNDAsync(FamilyEditDTO request)
        {
            var response = new BaseJsonResponse<bool>();
            try
            {
                //case này cần thêm service
                using var profileBusinessClient = new ProfileBusinessClient();

                //var data = await profileBusinessClient.CheckFamilyCMNDAsync(request);
                response.Status = true;
                response.Error = HttpStatusCode.OK.ToString();
                response.Data = true;
                return Json(response);
            }
            catch (Exception ex)
            {

                response.Error = HttpStatusCode.BadRequest.ToString();
                response.Message = ex.Message;
                return Json(response);
            }
        }

        #endregion

    }

}