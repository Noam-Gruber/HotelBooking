
namespace Server
{
    public class ResponseMessage
    {
        public int Status { get; set; }    // 0=OK,1=NotFound,2=Error,201=Created
        public object Data { get; set; }    // Booking או List<Booking> או שגיאה
    }
}
