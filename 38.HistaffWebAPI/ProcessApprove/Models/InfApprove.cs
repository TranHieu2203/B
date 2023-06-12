using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
  
namespace ProcessApprove.Models
{
    public class InfApprove
    {
        public string EMPLOYEE_APPROVED{get;set;}
        public string EMPLOYEE_ID{get;set;}
        public string FULLNAME_VN{get;set;}
        public string EMPLOYEE_CODE {get;set;}
        public string PE_PERIOD_ID {get;set;}
        public string APP_STATUS {get;set;}
        public string ACTION {get;set;}
        public string PROCESS_TYPE {get;set;}
        public string APP_NOTES {get;set;}
        public string ID_REGGROUP {get;set;}
        public string TOKEN {get;set;}
          public DateTime  LEAVE_FROM {get;set;}
        public DateTime  LEAVE_TO {get;set;}
        public decimal DAY_NUM {get;set;}
        public string LEAVE_TYPE {get;set;}
    }
}