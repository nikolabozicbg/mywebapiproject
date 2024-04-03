using Microsoft.EntityFrameworkCore;
using MyWebApiProject.Models;

namespace MyWebApiProject.Data;

public class AppliationDbContext : DbContext
{
    public AppliationDbContext(DbContextOptions<AppliationDbContext> options) : base(options)
    {
    }

    public DbSet<Villa> Villas { get; set; }
    public DbSet<VillaNumber> VillaNumbers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Villa>().HasData(
            new Villa
            {
                Id = 1,
                Name = "Villa 1",
                Details = "Villa 1 Description",
                Rate = 1000,
                Occupancy = 20,
                SQft = 5000,
                ImageUrl = "https://via.placeholder.com/150",
                Amenity = "Villa 1 Amenity"
            }
        );
    }
}