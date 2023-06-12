using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProcessApprove.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ProcessApprove.Controllers

{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration Configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            Configuration = configuration;
        }

        // public IActionResult Index()
        // {
        //     //ViewData = null;
        //     return View();
        // }
        public async Task<ActionResult> Index(){
            string tokenclient = HttpContext.Request.Query["token"];//Process/72FC1F59C5CCB34F7E29A71AF111E170
            string status =HttpContext.Request.Query["status"];
            var linkApi = Configuration["ApiLink"];
            if (status==null )return View();
            if (tokenclient == null) return View();
            InfApprove infProcessApprove =new InfApprove();
            using(HttpClient client = new HttpClient()){
                using(var  response = await client.GetAsync(linkApi + tokenclient)){
                    if (response.IsSuccessStatusCode){
                        string apiResponse = await response?.Content.ReadAsStringAsync();
                        infProcessApprove = JsonConvert.DeserializeObject<InfApprove>(apiResponse);
                    }
                    // else{
                    //     //infProcessApprove=new List<string>{response.StatusCode.ToString(), response.ReasonPhrase};
                    // }
                    
                }
            }
            if (infProcessApprove != null){
                infProcessApprove.APP_STATUS=status;
                infProcessApprove.TOKEN=tokenclient;
                if (infProcessApprove.APP_STATUS == "2")
                    infProcessApprove.ACTION ="KHÔNG DUYỆT";
                else
                    infProcessApprove.ACTION="DUYỆT";
                return View(infProcessApprove);
            }
            return View();
        }
        // public IActionResult Privacy()
        // {
        //     return View();
        // }
        public ViewResult CalAppProces() => View();
        [HttpPost]
        public async Task<IActionResult> CalAppProces(InfApprove _infApprove)
        {
            object receivedApproveProcess ;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(_infApprove), Encoding.UTF8, "application/json");
                var linkApi = Configuration["ApiLink"];
                using (var response = await httpClient.PostAsync(linkApi, content))
                {
                    if (response.IsSuccessStatusCode){
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        receivedApproveProcess = JsonConvert.DeserializeObject<object>(apiResponse);
                        return View(receivedApproveProcess);
                    }
                    else{
                        return View ("Lỗi trong quá trình phê duyệt");
                    }
                }
            }
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
