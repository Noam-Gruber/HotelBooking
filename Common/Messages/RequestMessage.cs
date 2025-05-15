using Common.Entities;

namespace Server
{
    public class RequestMessage
    {
        public string Command { get; set; }    // "GetAll", "Get", "Create"
        public int Id { get; set; }    // עבור "Get"
        public Booking Booking { get; set; }    // עבור "Create"
    }
}
