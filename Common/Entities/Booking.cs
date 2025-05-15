using System;

namespace Common.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        // פרטי אורח
        public string GuestName { get; set; } = "";
        public string Email { get; set; } = "";

        // תאריכים (UI קורא להם CheckInDate / CheckOutDate)
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        // פרטי חדר – לפי ה-UI
        public string RoomType { get; set; } = "Single";
        public int NumberOfGuests { get; set; }

        // פרטי אשראי
        public string CardNumber { get; set; } = "";
        public int ExpiryMonth { get; set; }   // 1-12
        public int ExpiryYear { get; set; }   // YYYY
    }
}
