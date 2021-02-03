using Microsoft.EntityFrameworkCore;
using RentVehicle.DataLayer.Entities;

namespace RentVehicle.DataLayer.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Rent> Rents { get; set; }
    }
}