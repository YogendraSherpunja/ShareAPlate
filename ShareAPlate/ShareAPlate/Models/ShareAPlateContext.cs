using Microsoft.EntityFrameworkCore;
using ShareAPlate.Models.ShareAPlate.Models;
using ShareAPlate.Models;

public class ShareAPlateContext : DbContext
{
    public ShareAPlateContext(DbContextOptions<ShareAPlateContext> options)
        : base(options) { }
    // adds the User, IndividualDonation, and OrganizationDonation tables to the database
    public DbSet<User> Users { get; set; } 
    public DbSet<IndividualDonation> IndividualDonations { get; set; }
    public DbSet<OrganizationDonation> OrganizationDonations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ensures that the base class's OnModelCreating method is called first to have the base class's behavior
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IndividualDonation>()
            .HasOne(d => d.User)
            .WithMany(u => u.IndividualDonations)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);  // Deletes donations when user is deleted

        modelBuilder.Entity<OrganizationDonation>()
            .HasOne(d => d.User)
            .WithMany(u => u.OrganizationDonations)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();  // Ensures unique email addresses
    }
}