namespace HiStaffAPI.AppCommon.MobileModel
{
    public class ForgotPasswordModel
    {
        public string UserName { set; get; }

        public string PassWord { set; get; }

        public string Email { set; get; }

        /// <summary>
        /// Format yyyyMMddHHmmss
        /// </summary>
        public string ExpiryTime { set; get; }

        public int ExpiryMinutes { set; get; }

        public string ActionLink { set; get; }

        public string ActCode { set; get; }
    }
}