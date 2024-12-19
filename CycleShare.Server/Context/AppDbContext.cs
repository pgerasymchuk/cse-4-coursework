using CycleShare.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace CycleShare.Server.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Bike> Bike { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<GalleryItem> GalleryItem { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<ResetPasswordToken> ResetPasswordToken { get; set; }
    }
}
