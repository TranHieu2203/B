using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HistaffWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
namespace HistaffWebAPI.Controllers
{
    [Produces("application/json")]
    public class Process_approvedController: Controller
    {
        IProcess_approved process_approvedRepository;
        public Process_approvedController(IProcess_approved _process_approvedRepository)
        {
            process_approvedRepository = _process_approvedRepository;
        }

        [Route("api/GetProcess_Approves")]
        public ActionResult GetProcess_Approves()
        {
            var result = process_approvedRepository.GetProcess_Approves();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("api/GetProcess_ApprovesDetails/{processID}")]
        public ActionResult GetProcess_ApprovesDetails(string token)
        {
            var result = process_approvedRepository.GetProcess_ApprovesDetails(token);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
