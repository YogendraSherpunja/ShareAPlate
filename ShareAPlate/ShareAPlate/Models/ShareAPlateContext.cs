using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ShareAPlate.Models
{
    public class ShareAPlateContext : DbContext
    {
        public ShareAPlateContext(DbContextOptions<ShareAPlateContext> options)
        : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                UserFirstName = "Admin",
                UserLastName = "User",
                Email = "admin@example.com",
                Password = "password", // Note: Always use hashed passwords in production!
                Location = "Headquarters",
                Number = 1234567890
            });

        }

    }
}