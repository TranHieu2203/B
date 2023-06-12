using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HiStaffAPI.AppCommon.PortalModel
{
    public class SelectItemRequest
    {
        public decimal? ID { get; set; }

        public string NAME{ get; set; }

        public string CODE { get; set; }

        public string ATTRIBUTE1 { get; set; }

        public string ATTRIBUTE2 { get; set; }

        public string ATTRIBUTE3 { get; set; }

        public string ATTRIBUTE4 { get; set; }
    }

}