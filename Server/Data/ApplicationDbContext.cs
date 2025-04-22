using Common;
using System.Data.Entity;

namespace Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection") { }

        public DbSet<Booking> Bookings { get; set; }
    }
}
