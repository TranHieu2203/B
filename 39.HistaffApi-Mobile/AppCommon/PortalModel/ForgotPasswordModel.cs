namespace HiStaffAPI.AppCommon.PortalModel
{
    public class ForgotPasswordModel
    {
        public string UserName { set; get; }

        public string Email { set; get; }

        public int ExpiryTime { set; get; }

        public int ExpiryMinutes { set; get; }

        public string ActionLink { set; get; }
    }
}