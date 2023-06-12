
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace HiStaffAPI.AppCommon.PortalModel
{
    public class BaseRequest
    {
        public List<decimal> Ids { set; get; }

        public decimal Id { set; get; }

        public string IdString { set; get; }

        public string Type { set; get; }

        public decimal TypeId { set; get; }

        public string Process { set; get; }

        public string Url { set; get; }

        public decimal OrgId { set; get; }

        public decimal JobId { set; get; }

        public decimal ProvinceId { set; get; }

        public decimal DistrictId { set; get; }

        public decimal EmployeeId { set; get; }

        public string EmployeeCode { set; get; }

        public string StringValue { set; get; }

        public string Lang { set; get; }

        public bool IsApp { set; get; }

        public bool IsOM { set; get; }

        public bool IsFTEPlan { set; get; }

        public bool IsBlank { set; get; }

        public bool IsCreated { set; get; }

        public string TableName { set; get; }

        public string ColumnName { set; get; }

        public decimal AddressId { set; get; }

        public decimal PeriodId { set; get; }

        public DateTime? StartDate { set; get; }

        public DateTime? EndDate { set; get; }

        public DateTime? StartHour { set; get; }

        public DateTime? EndHour { set; get; }

        public decimal Year { set; get; }

        public string Color { set; get; }

        public decimal ApproveId { set; get; }

        public decimal StatusId { set; get; }

        public string Note { set; get; }

        public bool ReqSend { set; get; }

        public string UserName { set; get; }

        public string Password { set; get; }

        public string PasswordOld { set; get; }
    }
    public class BaseRequest<T> : BaseRequest
    {
        public T Filter { set; get; }
    }
}