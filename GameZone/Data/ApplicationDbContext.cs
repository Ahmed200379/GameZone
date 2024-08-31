using GameZone.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace GameZone.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<GameDevice> GameDevices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDevice>()
                .HasKey(k => new {k.DeviceId,k.GameId});

            modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Name = "Action" },
               new Category { Id = 2, Name = "Adventure" },
               new Category { Id = 3, Name = "Puzzle" },
               new Category { Id = 4, Name = "RPG" }
           );

            modelBuilder.Entity<Device>().HasData(
               new Device { Id = 1, Name = "PC", Icon = "pc_icon.png" },
               new Device { Id = 2, Name = "PlayStation", Icon = "playstation_icon.png" },
               new Device { Id = 3, Name = "Xbox", Icon = "xbox_icon.png" },
               new Device { Id = 4, Name = "Nintendo", Icon = "nintendo_icon.png" }
           );
            base.OnModelCreating(modelBuilder);
        }
    }
}
