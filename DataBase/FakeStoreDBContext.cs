using FakeStoreLocalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FakeStoreLocalAPI.DataBase
{
    public class FakeStoreDBContext : DbContext
    {
        //entities
        public DbSet<AddressDetail> AddressDetail { get; set; }
        public DbSet<Cart> Cart { get; set; }

        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<CategoryDetail> CategoryDetail { get; set; }
        public DbSet<GeoLocationDetail> GeoLocationDetail { get; set; }
        public DbSet<NameDetail> NameDetail { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<RatingDetail> RatingDetail { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<Tutorial> Tutorial { get; set; }
        public DbSet<Article> Article { get; set; }

        public DbSet<UserLogin> UserLogin { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-JU73S7V;Database=FakeStoreDataDb;Trusted_Connection=True;TrustServerCertificate=True;");
            // optionsBuilder.UseSqlServer("Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;");
        }
    }
}
