using CSVTest.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSVTest.DataAccess.Contexts;

public class CsvContext : DbContext
{
    public DbSet<Trip> Trips { get; set; }

    public CsvContext()
    {

    }
    public CsvContext(DbContextOptions<CsvContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trip>()
            .Property(e => e.StoreAndFwdFlag)
            .HasConversion<string>();

        modelBuilder.Entity<Trip>()
            .HasIndex(t => t.PuLocationId);
        
        


    }
}