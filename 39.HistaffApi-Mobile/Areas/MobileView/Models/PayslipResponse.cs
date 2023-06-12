using HiStaffAPI.PayrollBusiness;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiStaffAPI.Areas.MobileView.Models
{
    public class PayslipResponse
    {
        public decimal? Year { get; set; }

        public List<SelectListItem> YearOptions { get; set; }

        public decimal? PeriodId { get; set; }

        public IEnumerable<SelectListItem> PeriodIdOptions { get; set; }
    }
}