using BookingApi.Models;
using Microsoft.EntityFrameworkCore;    

namespace BookingApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Booking> Bookings => Set<Booking>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Будка 1", Capacity = 4 },
                new Room { Id = 2, Name = "Будка 2", Capacity = 2 },
                new Room { Id = 3, Name = "Будка 3", Capacity = 6 }
                );
        }
    }
}
