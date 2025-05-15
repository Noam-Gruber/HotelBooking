using Common.Entities;
using System.Data.Entity;

namespace Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("HotelBookingDb") { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Common.Entities.Booking> Bookings { get; set; }

        protected override void OnModelCreating(DbModelBuilder b)
        {
            // Booking – מפתח ראשי כבר מוגדר אוטומטית
            // אינדקס ייחודי: חדר-סוג + טווח תאריכים
            //b.Entity<Booking>()
            // .HasIndex(bk => new { bk.RoomType, bk.CheckInDate, bk.CheckOutDate })
            // .IsUnique();

            base.OnModelCreating(b);
        }

    }
}
