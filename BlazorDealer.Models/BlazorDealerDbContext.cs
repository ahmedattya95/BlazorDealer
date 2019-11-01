using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorDealer.Models
{
    public class BlazorDealerDbContext : IdentityDbContext
    {

        public BlazorDealerDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Booking> Bookings { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Brand>().
                HasMany(p => p.Cars).WithOne(p => p.Brand).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<CarType>().
                HasMany(p => p.Cars).WithOne(p => p.CarType).OnDelete(DeleteBehavior.SetNull);
            builder.Entity<Car>().
                HasMany(p => p.CarImages).WithOne(p => p.Car).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Customer>().
                HasMany(p => p.Bookings).WithOne(p => p.Customer).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Car>().
                HasMany(p => p.Bookings).WithOne(p => p.Car).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CarType>().
                HasData(new CarType { ID = Guid.NewGuid().ToString(), Name = "Used" }, new CarType { ID = Guid.NewGuid().ToString(), Name = "New" });

            base.OnModelCreating(builder);
        }

    }
}
