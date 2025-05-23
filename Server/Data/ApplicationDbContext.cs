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
        public ApplicationDbContext() : base("HotelBookingDb")
        {
            // הגדרת Database Initializer
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }

        /// <summary>
        /// Gets or sets the bookings table in the database.
        /// </summary>
        public DbSet<Booking> Bookings { get; set; }

        /// <summary>
        /// Gets or sets the chat messages table in the database.
        /// </summary>
        public DbSet<ChatMessage> ChatMessages { get; set; }

        /// <summary>
        /// Configures the model and relationships using the specified model builder.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for the context.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // הגדרת טבלת ChatMessages
            modelBuilder.Entity<ChatMessage>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<ChatMessage>()
                .Property(c => c.SenderName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<ChatMessage>()
                .Property(c => c.Message)
                .IsRequired()
                .HasMaxLength(1000);

            modelBuilder.Entity<ChatMessage>()
                .Property(c => c.SessionId)
                .HasMaxLength(50);

            modelBuilder.Entity<ChatMessage>()
                .Property(c => c.Timestamp)
                .IsRequired();

            modelBuilder.Entity<ChatMessage>()
                .Property(c => c.IsFromAdmin)
                .IsRequired();

            // הגדרת טבלת Booking (אם נדרש)
            modelBuilder.Entity<Booking>()
                .HasKey(b => b.Id);
        }
    }
}