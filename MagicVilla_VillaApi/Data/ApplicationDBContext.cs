using MagicVilla_VillaApi.Model;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaApi.Data
{
    public class ApplicationDBContext :DbContext 
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
        {
        }
        public DbSet<Villa> Villas { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Villa1",
                    Details = "ewwghoiwgih",
                    ImageUrl = "",
                    Occupancy = 5,
                    Amenity = "",
                    Rate = 200,
                    Sqft = 550,
                    DateCreate = new DateTime(2025,3,5)

                });
        }
    }
}
