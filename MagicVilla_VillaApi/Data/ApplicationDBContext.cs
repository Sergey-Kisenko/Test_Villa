using MagicVilla_VillaApi.Model;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
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
                    Name = "Villa1",
                    Details = "Test Details",
                    ImageUrl = "Test ImageUrl",
                    Occupancy = 5,
                    Amenity = "Amenity",
                    Rate = 200,
                    Sqft = 550,
                    DateCreate = new DateTime(2025,1,1)

                });

            modelBuilder.Entity<VillaNumber>().HasData(
                new VillaNumber
                {
                    VillaNo = 10,
                    SpecialDetails = "Villa 1",
                    DateTimeCreate = new DateTime(2025, 1, 1),
                    VillaId = 1
                });
        }
    }
}
