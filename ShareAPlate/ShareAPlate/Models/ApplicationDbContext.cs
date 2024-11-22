using Microsoft.EntityFrameworkCore;

namespace ShareAPlate.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<DonorModel> Donors { get; set; }
        public DbSet<RecipientModel> Recipients { get; set; }
    }
}
