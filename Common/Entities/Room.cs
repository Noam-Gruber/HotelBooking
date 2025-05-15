using System.Collections.Generic;

namespace Common.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }          // 101, 102 …
        public string RoomType { get; set; } = "";   // Single / Double …
        public int Capacity { get; set; }            // 1-4
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
