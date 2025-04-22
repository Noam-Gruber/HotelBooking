using System;

namespace Common
{
    public class Booking
    {
        public int Id { get; set; }
        public string GuestName { get; set; }
        public string Email { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string RoomType { get; set; }
        public int NumberOfGuests { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
    }
}
