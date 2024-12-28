using CSVTest.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSVTest.DataAccess.Contexts;

public class CsvContext:DbContext
{
    public DbSet<Trip> Trips { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trip>()
            .Property(e => e.StoreAndFwdFlag)
            .HasConversion<string>();

    }
}