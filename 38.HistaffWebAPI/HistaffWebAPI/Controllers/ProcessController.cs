using HistaffWebAPI.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HistaffWebAPI.Repositories;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using HistaffWebAPI.Services;
using System.Text;
//using System.Web.Script.Serialization;
namespace HistaffWebAPI.Controllers{
    [Route("api/[controller]")]
    [ApiController]
   
    public class ProcessController : ControllerBase{
        IProcess_approved process_approvedRepository;
        public ProcessController(IProcess_approved _process_approvedRepository)
        {
            process_approvedRepository = _process_approvedRepository;
        }
        [EnableCors("AllowOrigin")]
        [HttpGet ("{token}",Name="getProcess")]
        public ActionResult<object> Get(string token){
            var process=process_approvedRepository.GetProcess_ApprovesDetails(token);
            if (process == null){ return NotFound();}
            //mã hoá dữ liệu gửi đi
            List<Process_approve> _proces_approves =new List< Process_approve>();
            var json =  JsonConvert.SerializeObject(process);
            _proces_approves=JsonConvert.DeserializeObject<List<Process_approve>>(json);
            //public Process_approveEncrypt(Process_approve _processapprove){
            EncryptDecrypt decrypt=new EncryptDecrypt();
            Process_approveEncrypt dataEncrypt =new Process_approveEncrypt();
            dataEncrypt.EMPLOYEE_APPROVED=decrypt.EncryptString(_proces_approves[0].EMPLOYEE_APPROVED.ToString());
            dataEncrypt.EMPLOYEE_ID=decrypt.EncryptString(_proces_approves[0].EMPLOYEE_ID.ToString());
            dataEncrypt.FULLNAME_VN=_proces_approves[0]?.FULLNAME_VN;
            dataEncrypt.EMPLOYEE_CODE=_proces_approves[0]?.EMPLOYEE_CODE;
            dataEncrypt.PE_PERIOD_ID=decrypt.EncryptString(_proces_approves[0].PE_PERIOD_ID.ToString());
            dataEncrypt.APP_STATUS=decrypt.EncryptString(_proces_approves[0].APP_STATUS.ToString());
            dataEncrypt.ACTION=_proces_approves[0]?.ACTION;
            dataEncrypt.PROCESS_TYPE=_proces_approves[0]?.PROCESS_TYPE;
            dataEncrypt.APP_NOTES=_proces_approves[0]?.APP_NOTES;
            dataEncrypt.ID_REGGROUP=decrypt.EncryptString(_proces_approves[0].ID_REGGROUP.ToString());
            dataEncrypt.LEAVE_FROM=_proces_approves[0]?.LEAVE_FROM;
            dataEncrypt.LEAVE_TO=_proces_approves[0]?.LEAVE_TO;
            dataEncrypt.DAY_NUM=_proces_approves[0]?.DAY_NUM;
            dataEncrypt.LEAVE_TYPE=_proces_approves[0]?.LEAVE_TYPE;
            //this.TOKEN=decrypt.EncryptString(_processapprove?.TOKEN);
            return(dataEncrypt);
        }
        [HttpPost]
        public  ActionResult<object> Post(Process_approveEncrypt _proces_approveEncrypt){
          var objResulst= process_approvedRepository.PRI_PROCESS(_proces_approveEncrypt);
          return objResulst;
        }
    }
}