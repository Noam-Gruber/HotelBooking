using Common.Entities;

namespace Common.Messages
{
    public class RequestMessage
    {
        public string Command { get; set; }
        public int Id { get; set; }
        public Booking Booking { get; set; }
    }
}
