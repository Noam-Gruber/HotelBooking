using Common.Entities;
using System.Data.Entity;

namespace Server.Data
{
    /// <summary>
    /// Represents the Entity Framework database context for the hotel booking application.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class using the "HotelBookingDb" connection string.
        /// </summary>
        public ApplicationDbContext() : base("HotelBookingDb") { }

        /// <summary>
        /// Gets or sets the bookings table in the database.
        /// </summary>
        public DbSet<Booking> Bookings { get; set; }

        /// <summary>
        /// Configures the model and relationships using the specified model builder.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
