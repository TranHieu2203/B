using System;
namespace HistaffWebAPI.Models{
    public class Process_approve{
        // .p_employee_app_id = employee_id_app, .P_EMPLOYEE_ID = employee_id, .P_PERIOD_ID = period_id, .P_STATUS = status, .P_PROCESS_TYPE = process_type, .P_NOTE = notes, .P_ID_REGGROUP = id_reggroup, .P_RESULT = cls.OUT_NUMBER}
         public decimal EMPLOYEE_APPROVED{get;set;}
        public decimal EMPLOYEE_ID{get;set;}
        public string FULLNAME_VN{get;set;}
        public string EMPLOYEE_CODE {get;set;}
        public decimal PE_PERIOD_ID {get;set;}
        public decimal APP_STATUS {get;set;}
        public string ACTION {get;set;}
        public string PROCESS_TYPE {get;set;}
        public string APP_NOTES {get;set;}
        public decimal ID_REGGROUP {get;set;}
        public string TOKEN {get;set;}
        public DateTime  LEAVE_FROM {get;set;}
        public DateTime  LEAVE_TO {get;set;}
        public decimal DAY_NUM {get;set;}
        public string LEAVE_TYPE {get;set;}
    }
}