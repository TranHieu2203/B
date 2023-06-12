namespace HiStaffAPI.AppCommon.MobileModel
{
    public class NotificationContent
    {
        //ViewerUserID IN NUMBER,
        //ActorUserID IN NUMBER,
        //EntityTypeID IN NUMBER,
        //EntityID IN NUMBER,
        //MessageNotification IN NVARCHAR2,
        public decimal FromEmployeeID { set; get; }
        public decimal ToEmployeeID { set; get; }
        public decimal? SVALUE { set; get; }
        public decimal? NVALUE { set; get; }
        public string MessageContent { set; get; }
    }
}