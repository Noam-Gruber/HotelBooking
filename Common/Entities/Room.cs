using System.Collections.Generic;

namespace Common.Entities
{
    /// <summary>
    /// Represents a hotel room with its details and associated bookings.
    /// </summary>
    public class Room
    {
        /// <summary>
        /// Gets or sets the unique identifier for the room.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the room number (e.g., 101, 102).
        /// </summary>
        public int RoomNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the room (e.g., Single, Double).
        /// </summary>
        public string RoomType { get; set; } = "";

        /// <summary>
        /// Gets or sets the maximum capacity of the room.
        /// </summary>
        public int Capacity { get; set; }            // 1-4

        /// <summary>
        /// Gets or sets the collection of bookings associated with this room.
        /// </summary>
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
