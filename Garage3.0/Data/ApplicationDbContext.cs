﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Garage3._0.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace Garage3._0.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
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
            modelBuilder.Entity<VehicleType>().HasData(
                new VehicleType { Id = 1, VehicleTypeName = "Airplane" },
                new VehicleType { Id = 2, VehicleTypeName = "Boat" },
                new VehicleType { Id = 3, VehicleTypeName = "Bus" },
                new VehicleType { Id = 4, VehicleTypeName = "Car" },
                new VehicleType { Id = 5, VehicleTypeName = "Motorcycle" });
            //modelBuilder.Entity<ParkedVehicle>().HasData(
            //    new ParkedVehicle { Id = 1, VehicleType = VehicleType.Car, RegistrationNumber = "ERT987", Color = "Blue", Brand = "Benz", VehicleModel = "280s", Wheel = 4, ArrivalTime = DateTime.ParseExact("18/08/2018 07:22:15", "dd/MM/yyyy h:m:s", null) },
            //    new ParkedVehicle { Id = 2, VehicleType = VehicleType.Car, RegistrationNumber = "KDR536", Color = "Red", Brand = "Volvo", VehicleModel = "142", Wheel = 4, ArrivalTime = DateTime.ParseExact("19/07/2012 08:29:23", "dd/MM/yyyy h:m:s", null) },
            //    new ParkedVehicle { Id = 3, VehicleType = VehicleType.Motorcycle, RegistrationNumber = "LDT432", Color = "Green", Brand = "Honda", VehicleModel = "CGI", Wheel = 2, ArrivalTime = DateTime.ParseExact("23/05/2011 09:42:17", "dd/MM/yyyy h:m:s", null) }
            //);
        }
    }
}
