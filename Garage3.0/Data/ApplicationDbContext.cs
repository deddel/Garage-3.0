using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Garage3._0.Models.Entities;

namespace Garage3._0.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ParkedVehicle> ParkedVehicle { get; set; }

        public DbSet<VehicleType> VehicleType { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ParkedVehicle>()
                .HasOne(v => v.ApplicationUser) // A Vehicle has one User
                .WithMany(u => u.Vehicles) // A User has many Vehicles
                .HasForeignKey(v => v.ApplicationUserId) // Foreign key in the Vehicle table
                .OnDelete(DeleteBehavior.Cascade); // Optional: Cascade delete if the user is removed
        }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<ParkedVehicle>().HasData(
        //        new ParkedVehicle { Id = 1, VehicleType = VehicleType.Car, RegistrationNumber = "ERT987", Color = "Blue", Brand = "Benz", VehicleModel = "280s", Wheel = 4, ArrivalTime = DateTime.ParseExact("18/08/2018 07:22:15", "dd/MM/yyyy h:m:s", null) },
        //        new ParkedVehicle { Id = 2, VehicleType = VehicleType.Car, RegistrationNumber = "KDR536", Color = "Red", Brand = "Volvo", VehicleModel = "142", Wheel = 4, ArrivalTime = DateTime.ParseExact("19/07/2012 08:29:23", "dd/MM/yyyy h:m:s", null) },
        //        new ParkedVehicle { Id = 3, VehicleType = VehicleType.Motorcycle, RegistrationNumber = "LDT432", Color = "Green", Brand = "Honda", VehicleModel = "CGI", Wheel = 2, ArrivalTime = DateTime.ParseExact("23/05/2011 09:42:17", "dd/MM/yyyy h:m:s", null) }
        //    );
        //}
    }
}
