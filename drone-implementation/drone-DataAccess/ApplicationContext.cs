using drone_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ddrone_DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder builder)
        //{
        //    builder.UseInMemoryDatabase(databaseName:"DroneDb");
        //}
        public DbSet<Drone> Drones { get; set; }
        public DbSet<DroneItems> DroneItems { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<DroneModel> Models { get; set; }
        public DbSet<State> States { get; set; }
    }
}
