using System;

namespace Common.Entities
{
    /// <summary>
    /// Represents a hotel booking with guest and reservation details.
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// Gets or sets the unique identifier for the booking.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the guest making the booking.
        /// </summary>
        public string GuestName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the guest.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the check-in date for the booking.
        /// </summary>
        public DateTime CheckInDate { get; set; }

        /// <summary>
        /// Gets or sets the check-out date for the booking.
        /// </summary>
        public DateTime CheckOutDate { get; set; }

        /// <summary>
        /// Gets or sets the type of room booked.
        /// </summary>
        public string RoomType { get; set; }

        /// <summary>
        /// Gets or sets the number of guests included in the booking.
        /// </summary>
        public int NumberOfGuests { get; set; }

        /// <summary>
        /// Gets or sets the name on the payment card.
        /// </summary>
        public string CardName { get; set; }

        /// <summary>
        /// Gets or sets the payment card number.
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the expiry month of the payment card.
        /// </summary>
        public int ExpiryMonth { get; set; }

        /// <summary>
        /// Gets or sets the expiry year of the payment card.
        /// </summary>
        public int ExpiryYear { get; set; }
    }
}
