using Linked_backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Linked_backend.Data
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options)
        {
        }

        public DbSet<Bedrijf> Bedrijven { get; set; }
        public DbSet<Laadkade> Laadkades { get; set; }
        public DbSet<Leverancier> Leveranciers { get; set; }
        public DbSet<Levering> Leveringen { get; set; }
        public DbSet<Product> Producten { get; set; }
        public DbSet<Role> Rollen { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bedrijf>().ToTable("Bedrijf");
            modelBuilder.Entity<Laadkade>().ToTable("Laadkade");
            modelBuilder.Entity<Leverancier>().ToTable("Leverancier");
            modelBuilder.Entity<Levering>().ToTable("Levering");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Schedule>().ToTable("Schedule");
            modelBuilder.Entity<User>().ToTable("User");
        }
    }
}
